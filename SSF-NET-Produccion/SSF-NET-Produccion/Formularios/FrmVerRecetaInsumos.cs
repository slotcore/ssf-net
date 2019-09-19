using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIAC_Negocio.Maestros;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Produccion;
using SIAC_Negocio.Almacen;
using SIAC_Negocio.Sunat;
using SIAC_Entidades.Produccion;
using SIAC_Entidades.Almacen;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using Helper;
using SIAC_Objetos.Sistema;

namespace SSF_NET_Produccion.Formularios
{
    public partial class FrmVerRecetaInsumos : Form
    {
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
                
        public DataTable dtItems = new DataTable();
        public DataTable dtTipExi = new DataTable();
        public DataTable dtUniMed = new DataTable();
        public DataTable dtTipPro = new DataTable();

        public BE_PRO_PRODUCTOSRECETAS entReceta = new BE_PRO_PRODUCTOSRECETAS();
        public List<BE_PRO_PRODUCTOSRECETASINSUMOS> lstRecetasIns = new List<BE_PRO_PRODUCTOSRECETASINSUMOS>();

        public bool b_Modificar;                                           // INDICA SI EN EL FORMULARIO SE PODRA MODIFICAR LA RECETA
        public bool booAgregando;
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        string[,] arrCabeceraFlexIns = new string[6, 5];

        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890-()º.,/$' !!·%/()=?¿*^" + (char)8;

        public FrmVerRecetaInsumos()
        {
            InitializeComponent();
        }

        private void FrmVerRecetaInsumos_Load(object sender, EventArgs e)
        {
            ConfigurarFormulario();
            CargarCombos();
            MostrarReceta();
        }
        void CargarCombos()
        {
            funDatos.ComboBoxCargarDataTable(CboUniMed, dtUniMed, "n_id", "c_des");
        }
        void ConfigurarFormulario()
        {
            this.Width = 972;
            this.Height = 460;
            // FLEX GRID DE LOS INSUMOS
            arrCabeceraFlexIns[0, 0] = "Tipo Existencia";
            arrCabeceraFlexIns[0, 1] = "150";
            arrCabeceraFlexIns[0, 2] = "C";
            arrCabeceraFlexIns[0, 3] = "";
            arrCabeceraFlexIns[0, 4] = "c_codrec";

            arrCabeceraFlexIns[1, 0] = "Codigo";
            arrCabeceraFlexIns[1, 1] = "130";
            arrCabeceraFlexIns[1, 2] = "C";
            arrCabeceraFlexIns[1, 3] = "";
            arrCabeceraFlexIns[1, 4] = "c_codrec";

            arrCabeceraFlexIns[2, 0] = "Descripcion";
            arrCabeceraFlexIns[2, 1] = "400";
            arrCabeceraFlexIns[2, 2] = "C";
            arrCabeceraFlexIns[2, 3] = "";
            arrCabeceraFlexIns[2, 4] = "c_codrec";

            arrCabeceraFlexIns[3, 0] = "Uni. Med.";
            arrCabeceraFlexIns[3, 1] = "60";
            arrCabeceraFlexIns[3, 2] = "C";
            arrCabeceraFlexIns[3, 3] = "";
            arrCabeceraFlexIns[3, 4] = "c_codrec";

            arrCabeceraFlexIns[4, 0] = "Cantidad Teorica";
            arrCabeceraFlexIns[4, 1] = "75";
            arrCabeceraFlexIns[4, 2] = "D";
            arrCabeceraFlexIns[4, 3] = "0.000000";
            arrCabeceraFlexIns[4, 4] = "c_codrec";

            arrCabeceraFlexIns[5, 0] = "Insumo Principal";
            arrCabeceraFlexIns[5, 1] = "70";
            arrCabeceraFlexIns[5, 2] = "B";
            arrCabeceraFlexIns[5, 3] = "";
            arrCabeceraFlexIns[5, 4] = "c_codrec";

            funFlex.FlexMostrarDatos(FgRec, arrCabeceraFlexIns, dtItems, 2, false);

            Sizer_Posicionar(Eo1,1,1);
            Sizer_Dimensionar(Eo1, this.Height - 40, this.Width - 18);

            if (b_Modificar == true)
            {
                CmdAddIns.Enabled = true;
                CmdDelIns.Enabled = true;

                CmdVolver.Visible = false;

                CmdAce.Visible = true;
                CmdCan.Visible = true;

                this.Text = "IMSUMOS DE LA RECETA - Edicion de Receta";
            }
            else
            {
                this.Text = "IMSUMOS DE LA RECETA";
            }
        }
        void MostrarReceta()
        {
            int n_row = 0;
            string c_dato = "";
            int n_fila = 0;
            FgRec.Rows.Count = 2;
            n_fila = 2;

            TxtCodRec.Text = entReceta.c_codrec;
            TxtDesReceta.Text = entReceta.c_des;
            CboUniMed.SelectedValue = entReceta.n_idunimed;
            TxtCan.Text = entReceta.n_can.ToString("0.00");
            TxtObs.Text = entReceta.c_obs;
            booAgregando = true;
            if (lstRecetasIns.Count == 0)
            {
                MessageBox.Show("¡ La receta no tiene insumos asignados !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booAgregando = false;
                return;
            }

            for (n_row = 0; n_row <= lstRecetasIns.Count - 1; n_row++)
            {
                FgRec.Rows.Count = FgRec.Rows.Count + 1;

                // TIPO DE EXISTENCIA
                c_dato = funDatos.DataTableBuscar(dtItems, "n_id", "n_idtipexi", lstRecetasIns[n_row].n_idite.ToString(), "N").ToString();
                c_dato = funDatos.DataTableBuscar(dtTipExi, "n_id", "c_des", c_dato, "N").ToString();
                FgRec.SetData(n_fila, 1, c_dato);

                // CODIGO DEL PRODUCTO
                c_dato = funDatos.DataTableBuscar(dtItems, "n_id", "c_codpro", lstRecetasIns[n_row].n_idite.ToString(), "N").ToString();
                FgRec.SetData(n_fila, 2, c_dato);
                
                // DESCRIPCION DEL PRODUCTO
                c_dato = funDatos.DataTableBuscar(dtItems, "n_id", "c_despro", lstRecetasIns[n_row].n_idite.ToString(), "N").ToString();
                FgRec.SetData(n_fila, 3, c_dato);

                // UNIDAD DE MEDIDA DEL PRODUCTO
                c_dato = funDatos.DataTableBuscar(dtUniMed, "n_id", "c_abr", lstRecetasIns[n_row].n_idunimed.ToString(), "N").ToString();
                FgRec.SetData(n_fila, 4, c_dato);

                // CANTIDAD TEORICA
                FgRec.SetData(n_fila, 5, lstRecetasIns[n_row].n_can.ToString("0.000000"));
                
                // INNDICA SI ES EL INSUMO PRINCIPAL
                FgRec.SetData(n_fila, 6, lstRecetasIns[n_row].n_inspri.ToString());
                n_fila = n_fila + 1;
            }
            booAgregando = false;
        }

        private void FrmVerRecetaInsumos_Resize(object sender, EventArgs e)
        {
            Sizer_Dimensionar(Eo1, this.Height - 40, this.Width - 18);
        }
        void Sizer_Dimensionar()
        {
        }
        void Sizer_Dimensionar(C1.Win.C1Sizer.C1Sizer Eo, int intAlto, int intAncho)
        {
            Eo.Height = intAlto;
            Eo.Width = intAncho;
        }
        void Sizer_Posicionar(C1.Win.C1Sizer.C1Sizer Eo, int intPosX, int intPosY)
        {
            Eo.Left = intPosX;
            Eo.Top = intPosY;
        }

        private void CmdVolver_Click(object sender, EventArgs e)
        {
            Cerrar();
        }

        private void CmdAddIns_Click(object sender, EventArgs e)
        {
            if (FgRec.Rows.Count >=2)
            {
                if (funFunciones.NulosC(FgRec.GetData(FgRec.Rows.Count - 1, 1)) == "")
                {
                    MessageBox.Show("¡ No ha especificado el tipo de item !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }

                if (FgRec.GetData(FgRec.Rows.Count - 1, 2).ToString() == "")
                {
                    MessageBox.Show("¡ No ha especificado la descripcion de item !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }
            }

            FgRec.Rows.Count = FgRec.Rows.Count + 1;
            FgRec.SetData(FgRec.Rows.Count-1, 6, "0");
            FgRec.Select(1, 1);
        }

        private void CmdDelIns_Click(object sender, EventArgs e)
        {
            if (FgRec.Rows.Count == 2)
            {
                MessageBox.Show("¡ La receta no tiene insumos para eliminar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            int n_row = 0;
            string c_dato = "";
            int n_idite = 0;

            c_dato = funFunciones.NulosC(FgRec.GetData(FgRec.Row, 3)).ToString();
            if (c_dato != "")
            { 
                n_idite = Convert.ToInt32(funDatos.DataTableBuscar(dtItems, "c_despro", "n_id", c_dato, "C"));

                for(n_row=0; n_row <= lstRecetasIns.Count-1; n_row++)
                {
                    if ((lstRecetasIns[n_row].n_idite == n_idite) && ((lstRecetasIns[n_row].n_idrec == entReceta.n_id)))
                    {
                        lstRecetasIns.RemoveAt(n_row);
                        break;
                    }
                }
            }
            FgRec.RemoveItem(FgRec.Row);
        }

        private void FrmVerRecetaInsumos_Activated(object sender, EventArgs e)
        {
         
        }

        private void CmdCan_Click(object sender, EventArgs e)
        {
            Cerrar();
        }

        private void FgRec_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (b_Modificar == false)
            {
                FgRec.AllowEditing = false; return;
            }

            if (booAgregando == true) { return; }

            if (FgRec.Col == 1)                     // TIPO DE EXISTENCIA
            {
            }

            if (FgRec.Col == 2)                     // CODIGO DEL PRODUCTO
            {
            }

            if (FgRec.Col == 3)                     // DESCRIPCION DEL PRODUCTO
            {
                string c_dato = FgRec.GetData(FgRec.Row, 3).ToString();
                c_dato = funDatos.DataTableBuscar(dtItems, "c_despro", "c_codpro", c_dato, "C").ToString();
                FgRec.SetData(FgRec.Row, 2, c_dato);
            }

            if (FgRec.Col == 4)                     // UNIDAD DE MEDIDA
            {
            }

            if (FgRec.Col == 5)                     // CANTIDAD
            {
                double n_valor = Convert.ToDouble(FgRec.GetData(FgRec.Row,5));
                FgRec.SetData(FgRec.Row,5,n_valor.ToString("0.000000"));
            }
            if (FgRec.Col == 6)                     // INSUMO PRINCIPAL
            {
                booAgregando = true;
                ActualizarCkeck();
                FgRec.SetData(FgRec.Row, 6, "1");
                booAgregando = false;
            }
        }
        void ActualizarCkeck()
        {
            int n_fila = 0;

            for (n_fila = 2; n_fila <= FgRec.Rows.Count - 1; n_fila++)
            {
                FgRec.SetData(n_fila, 6, "0");
            }
        }
        private void FgRec_EnterCell(object sender, EventArgs e)
        {
            if (b_Modificar == false)
            {
                FgRec.AllowEditing = false; return;
            }
            if (FgRec.Rows.Count == 2) { return; }

            if (booAgregando == true) { return; }

            DataTable dtResul = new DataTable();

            if (FgRec.Col == 1)                                           // TIPO DE EISTENCIA
            {
                funFlex.FlexColumnaCombo(FgRec, dtTipExi, "c_des", 1);    // ITEMS
            }

            if (FgRec.Col == 2)                                           // CODIGO DEL PRODUCTO
            {
            }

            if (FgRec.Col == 3)                                          // DESCRIPCION DEL PRODUCTO
            {
                if (funFunciones.NulosC(FgRec.GetData(FgRec.Row, 1)).ToString() != "")
                {
                    string c_dato = FgRec.GetData(FgRec.Row, 1).ToString();
                    int n_idtipexi = Convert.ToInt32(funDatos.DataTableBuscar(dtTipExi, "c_des", "n_id", c_dato, "C"));
                    dtResul = funDatos.DataTableFiltrar(dtItems, "n_idtipexi = " + n_idtipexi + "");
                    funFlex.FlexColumnaCombo(FgRec, dtResul, "c_despro", 3);     // ITEMS
                }
            }

            if (FgRec.Col == 4)                                          // UNIDAD DE MEDIDA
            {
                funFlex.FlexColumnaCombo(FgRec, dtUniMed, "c_abr", 4);   // ITEMS
            }

            if (FgRec.Col == 5)                                         // CANTIDAD
            {
            }
            if (FgRec.Col == 6)                                         // INSUMO PRINCIPAL
            {
            }  
            FgRec.AllowEditing = true;
        }

        private void FgRec_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            //if ((e.Col == 1) || (e.Col == 2) || (e.Col == 6))
            //{
            //    if (!strCaracteres.Contains(e.KeyChar))
            //    {
            //        e.Handled = true;
            //    }
            //}
            if (e.Col == 5)
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void FgRec_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            //if (b_Modificar == true)
            //{
            //    FgRec.AllowEditing = false; return;
            //}

            //if (booAgregando == true) { return; }

            ////if (FgRec.Col == 1)                     // TIPO DE EXISTENCIA
            ////{
            ////}

            ////if (FgRec.Col == 2)                     // CODIGO DEL PRODUCTO
            ////{
            ////}

            //if (FgRec.Col == 3)                     // DESCRIPCION DEL PRODUCTO
            //{
            //    string c_dato = FgRec.GetData(FgRec.Row, 3).ToString();
            //    c_dato = funDatos.DataTableBuscar(dtItems, "c_des", "c_cod", c_dato, "C").ToString();
            //    FgRec.SetData(FgRec.Row, 2, c_dato);
            //}

            //if (FgRec.Col == 4)                     // UNIDAD DE MEDIDA
            //{
            //}

            //if (FgRec.Col == 5)                     // CANTIDAD
            //{
            //}
        }

        private void CmdAce_Click(object sender, EventArgs e)
        {
            if (FgRec.Rows.Count == 0)
            {
                MessageBox.Show("¡ La receta no tiene insumos, debe de especificar al menos un insumo !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            lstRecetasIns.Clear();
            int n_fila = 0;
            bool b_seprincipal = false;
            double n_can = 0;
            int n_valor = 0;
            string c_dato = "";

            // RECORREMOS EL GRID PARA BUSCAR SI SE HA MARCADO EL INSUMO PRINCIPAL
            for (n_fila = 2; n_fila <= FgRec.Rows.Count - 1; n_fila++)
            {
                if (FgRec.GetData(n_fila, 6).ToString() == "True")
                {
                    b_seprincipal = true;
                    break;
                }
            }

            if (b_seprincipal == false)
            {
                MessageBox.Show("¡ No ha especificado el insumo principal para la receta, debe de indicar cual sera el insumo principal !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
                        
            for (n_fila = 2; n_fila <= FgRec.Rows.Count - 1; n_fila++)
            {
                BE_PRO_PRODUCTOSRECETASINSUMOS entInsumo = new BE_PRO_PRODUCTOSRECETASINSUMOS();

                
                entInsumo.n_idpro = entReceta.n_idpro;
                entInsumo.n_idrec = entReceta.n_id;

                c_dato = FgRec.GetData(n_fila, 3).ToString();
                n_valor = Convert.ToInt32(funDatos.DataTableBuscar(dtItems, "c_despro", "n_id", c_dato, "C").ToString());                
                entInsumo.n_idite = n_valor;

                c_dato = FgRec.GetData(n_fila, 4).ToString();
                n_valor = Convert.ToInt32(funDatos.DataTableBuscar(dtUniMed, "c_abr", "n_id", c_dato, "C").ToString());                
                entInsumo.n_idunimed = n_valor;

                n_can = Convert.ToDouble(FgRec.GetData(n_fila, 5).ToString());
                entInsumo.n_can = n_can;

                n_valor = 0;
                if (FgRec.GetData(n_fila, 6).ToString() == "True")
                {
                    n_valor = 1;
                }
                entInsumo.n_inspri = n_valor;
                lstRecetasIns.Add(entInsumo);
            }

            Cerrar();
        }
        void Cerrar()
        {
            funFlex = null;
            funFunciones = null;
            funDbGrid = null;
            funDatos = null;

            dtItems = null;
            dtTipExi = null;
            dtUniMed = null;
            dtTipPro = null;

            this.Hide();
        }
    }
}
