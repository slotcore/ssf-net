using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Produccion;
using SIAC_Objetos.Sistema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIAC_Negocio.Tesoreria;
using C1.Win.C1FlexGrid;
using SIAC_Negocio.Almacen;

namespace SSF_NET_Produccion.Formularios
{
    public partial class FrmConsultaTareas : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public int n_Libro;
        public int n_Origen;                                                    // INDICA EL ORIGEN DE LA GUIA     1 = VENTAS ;   2 = TRASLADO ENTRE ALMACENES

        CN_alm_inventario objIte = new CN_alm_inventario();
        
        DataTable dtTipExi = new DataTable();
        DataTable dtProv = new DataTable();
        DataTable dtItem = new DataTable();
        DataTable dtLista = new DataTable();
        DataTable dtPro = new DataTable();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        Cls_Controles funControl = new Cls_Controles();

        string[,] arrCabecera1 = new string[9, 5];
        string[,] arrCabeceraFlex1 = new string[2, 5];
        public FrmConsultaTareas()
        {
            InitializeComponent();
        }

        private void FrmConsultaTareas_Load(object sender, EventArgs e)
        {
            ConfigurarFormulario();
            CargarDT();
        }
        void CargarDT()
        {
            objIte.mysConec = mysConec;
            dtPro = objIte.Listar(STU_SISTEMA.EMPRESAID, 1, 1);
            dtPro = funDatos.DataTableFiltrar(dtPro, "n_idtipexi = 2");

        }
        void ConfigurarFormulario()
        {
            this.Height = 600;
            this.Width = 1094;

            Sz1.Left = 0;
            Sz1.Top = 41;
            Sz1.Height = this.Height - 83;
            Sz1.Width = this.Width - 18;

            this.Text = "PRODUCCION - CONSULTA DE TAREAS";

            OptPen.Checked = true;

            TxtFchIni.Text = "01/01/" + STU_SISTEMA.ANOTRABAJO.ToString();

            FgDatos.Cols.Count = 5;
            Cabecera1();

            arrCabeceraFlex1[0, 0] = "Producto";
            arrCabeceraFlex1[0, 1] = "395";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_tipexides";

            arrCabeceraFlex1[1, 0] = "Id";
            arrCabeceraFlex1[1, 1] = "0";
            arrCabeceraFlex1[1, 2] = "N";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_itedes";

            funFlex.FlexMostrarDatos(FgPro, arrCabeceraFlex1, dtLista, 1, false);

            funFlex.b_AlternarColor = false;
            funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 2, false);
            
            FgPro.Rows.Count = FgPro.Rows.Count + 1;
            FgPro.Cols[1].ComboList = "...";
            FgPro.AllowEditing = true;
        }
        void Cabecera1()
        {
            arrCabecera1[0, 0] = "Nº Doc Identidad";
            arrCabecera1[0, 1] = "80";
            arrCabecera1[0, 2] = "C";
            arrCabecera1[0, 3] = "";
            arrCabecera1[0, 4] = "c_numdocide";

            arrCabecera1[1, 0] = "Empleado";
            arrCabecera1[1, 1] = "300";
            arrCabecera1[1, 2] = "C";
            arrCabecera1[1, 3] = "";
            arrCabecera1[1, 4] = "c_traapenom";

            arrCabecera1[2, 0] = "Hora Inicio";
            arrCabecera1[2, 1] = "60";
            arrCabecera1[2, 2] = "C";
            arrCabecera1[2, 3] = "";
            arrCabecera1[2, 4] = "c_horini";

            arrCabecera1[3, 0] = "Hora Final";
            arrCabecera1[3, 1] = "60";
            arrCabecera1[3, 2] = "C";
            arrCabecera1[3, 3] = "";
            arrCabecera1[3, 4] = "c_horter";

            arrCabecera1[4, 0] = "Nº Hor. Trabajadas";
            arrCabecera1[4, 1] = "60";
            arrCabecera1[4, 2] = "C";
            arrCabecera1[4, 3] = "";
            arrCabecera1[4, 4] = "n_numhortra";

            arrCabecera1[5, 0] = "Cantidad";
            arrCabecera1[5, 1] = "70";
            arrCabecera1[5, 2] = "D";
            arrCabecera1[5, 3] = "";
            arrCabecera1[5, 4] = "n_can";

            arrCabecera1[6, 0] = "Prom. x Hora";
            arrCabecera1[6, 1] = "70";
            arrCabecera1[6, 2] = "D";
            arrCabecera1[6, 3] = "";
            arrCabecera1[6, 4] = "n_prohor";

            arrCabecera1[7, 0] = "Fch. Trabajo";
            arrCabecera1[7, 1] = "80";
            arrCabecera1[7, 2] = "F";
            arrCabecera1[7, 3] = "";
            arrCabecera1[7, 4] = "d_fchtra2";

            arrCabecera1[8, 0] = "Dato";
            arrCabecera1[8, 1] = "0";
            arrCabecera1[8, 2] = "N";
            arrCabecera1[8, 3] = "";
            arrCabecera1[8, 4] = "n_orden";
        }

        private void FrmCtaCtePedidos_Resize(object sender, EventArgs e)
        {
            Sz1.Left = 0;
            Sz1.Top = 41;
            Sz1.Height = this.Height - 83;
            Sz1.Width = this.Width - 18;
        }

        private void ToolSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ToolBuscar_Click(object sender, EventArgs e)
        {
            EjecutarConsulta();
        }
        void EjecutarConsulta()
        {
            if (funFunciones.NulosC(TxtFchIni.Text) == "")
            {
                MessageBox.Show("¡ No ha indicado la fecha de inicio para la consulta !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchIni.Focus();
                return;
            }
            if (funFunciones.NulosC(TxtFchFin.Text) == "")
            {
                MessageBox.Show("¡ No ha indicado la fecha final para la consulta !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchFin.Focus();
                return;
            }

            DateTime d_fchini = Convert.ToDateTime(TxtFchIni.Text);
            DateTime d_fchfin = Convert.ToDateTime(TxtFchFin.Text);

            if (d_fchini > d_fchfin)
            {
                MessageBox.Show("¡ La fecha de inicio no puede ser mayor a la fecha final de la consulta !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchIni.Focus();
                return;
            }

            int m_tiprep = 0;
            CN_pro_solicitudtareas o_protar = new CN_pro_solicitudtareas();
            o_protar.mysConec = mysConec;
            o_protar.STU_SISTEMA = STU_SISTEMA;

            string c_CadINIte = funFlex.Flex_CadenaIN(FgPro, 2, 1);

            o_protar.Consulta3(STU_SISTEMA.EMPRESAID, TxtFchIni.Text, TxtFchFin.Text, c_CadINIte);
            dtLista = o_protar.dtLista;

            //funFlex.b_AlternarColor = true;
            funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 2, true);
            PintarDatos();
            MessageBox.Show("¡ Los datos se mostraron con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            FgDatos.Focus();
        }
        void PintarDatos()
        { 
            int n_row = 0;
            int n_numdat = 0;
            string c_dato = "";
            Color o_color;
            Helper.Comunes.Convertir funCon = new Helper.Comunes.Convertir();

            for (n_row = 2; n_row <= FgDatos.Rows.Count-1; n_row++)
            {
                n_numdat = Convert.ToInt32(FgDatos.GetData(n_row, 9));
                
                if (n_numdat == 0)
                {
                    funFlex.Flex_PintarCeldas(FgDatos, n_row, 2, n_row, 2, Color.Blue, Color.Transparent);
                }
                if (n_numdat == 1)
                {
                    funFlex.Flex_PintarCeldas(FgDatos, n_row, 2, n_row, 2, Color.Green, Color.Transparent);
                }
                if (n_numdat == 2)
                {
                    c_dato = funCon.DecimalEnHoras(Convert.ToDouble(FgDatos.GetData(n_row, 5)));
                    FgDatos.SetData(n_row, 5, c_dato);
                }
                if (n_numdat == 3)
                {
                    funFlex.Flex_PintarCeldas(FgDatos, n_row, 2, n_row, 2, Color.Green, Color.Transparent);
                }
            }
            
        }
        void SetearCabecera1()
        {
            funFlex.Flex_UniColumnas(FgDatos, 0, 1, 9, "DATOS DEL PEDIDO", 1);
            funFlex.Flex_UniColumnas(FgDatos, 0, 10, 13, "DATOS DE FACTURACION", 1);
        }

        private void ToolExportar_Click(object sender, EventArgs e)
        {
            string c_cad = "";
            string c_nomarch = STU_SISTEMA.EMPRESARUC + "-" + STU_SISTEMA.ANOTRABAJO.ToString() + "-" + STU_SISTEMA.MESTRABAJO.ToString() + "-TAREAS-" + TxtFchIni.Text+"-" + TxtFchFin.Text + ".xls";
            //if (OpTod.Checked == true) { c_cad = "IMPORTES"; }
            //if (OptPen.Checked == true) { c_cad = "CANTIDADES"; }
            funFlex.ExportToExcel(FgDatos, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "TAREAS REALIZADAS DEL "+TxtFchIni.Text +" AL "+ TxtFchFin.Text, c_cad, c_nomarch);
        }

        private void FrmConsultaTareas_Resize(object sender, EventArgs e)
        {
            Sz1.Left = 0;
            Sz1.Top = 41;
            Sz1.Height = this.Height - 83;
            Sz1.Width = this.Width - 18;
        }

        private void FgPro_CellButtonClick(object sender, RowColEventArgs e)
        {
            if (FgPro.Col == 1)
            {
                DataTable dtResult = new DataTable();
                string c_dato = "";
                objIte.mysConec = mysConec;
                dtResult = objIte.BuscarItem("", "n_id", dtPro, 0);
                if (dtResult != null)
                {
                    if (dtResult.Rows.Count != 0)
                    {
                        c_dato = dtResult.Rows[0]["c_despro"].ToString();
                        FgPro.SetData(FgPro.Row, 1, c_dato);

                        c_dato = dtResult.Rows[0]["n_id"].ToString();
                        FgPro.SetData(FgPro.Row, 2, c_dato);

                        if (funFunciones.NulosC(FgPro.GetData(FgPro.Rows.Count - 1, 1)) != "")
                        {
                            FgPro.Rows.Count = FgPro.Rows.Count + 1;
                        }
                    }
                }
            }
        }

        private void FgPro_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString("") == "Delete")
            {
                FgPro.RemoveItem(FgPro.Row);
            }

            if (e.KeyCode.ToString("") == "Insert")
            {
                if (funFunciones.NulosC(FgPro.GetData(FgPro.Row, 1)).ToString() != "")
                {
                    FgPro.Rows.Count = FgPro.Rows.Count + 1;
                }
            }
        }
    }
}
