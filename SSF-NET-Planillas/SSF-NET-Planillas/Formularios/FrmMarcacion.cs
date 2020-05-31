using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Planilla;
using SIAC_Objetos.Sistema;
using SIAC_Entidades.Planillas;
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

namespace SSF_NET_Planillas.Formularios
{
    public partial class FrmMarcacion : Form
    {
        // VARIABLES PUBLICAS
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        
        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        Cls_Controles funControl = new Cls_Controles();
        int n_NUMCOL = 0;

        string[,] arrCabeceraDetallado = new string[5, 5];
        string[,] arrCabecera1 = new string[3, 5];
        string[,] arrCabecera2 = new string[3, 5];
        string[,] arrCabecera3 = new string[4, 5];
        public FrmMarcacion()
        {
            InitializeComponent();
        }

        private void FrmMarcacion_Load(object sender, EventArgs e)
        {
            ConfigurarFormulario();
            CargarDT();
        }
        void CargarDT()
        {
            //objPro.mysConec = mysConec;
            //dtProv = objPro.ListarCliente(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);
        }
        void ConfigurarFormulario()
        {
            this.Height = 599;
            this.Width = 1091;

            Sz1.Left = 0;
            Sz1.Top = 41;
            Sz1.Height = this.Height - 83;
            Sz1.Width = this.Width - 18;

            this.Text = "PLANILLAS - HORAS TRABAJADAS";

            OptPen.Checked = true;

            TxtFchIni.Text = "01/01/" + STU_SISTEMA.ANOTRABAJO.ToString();

            DataTable dt = new DataTable();
            FgDatos.Cols.Count = 5;
            Cabecera1();
            funFlex.b_AlternarColor = false;
            funFlex.FlexMostrarDatos(FgDetallado, arrCabeceraDetallado, dt, 2, false);
            funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dt, 3, false);
            funFlex.FlexMostrarDatos(FgDatos2, arrCabecera2, dt, 3, false);
            funFlex.FlexMostrarDatos(FgMarca, arrCabecera3, dt, 2, false);

            //SetearCabecera1();
        }
        void Cabecera1()
        {
            arrCabeceraDetallado[0, 0] = "Id";
            arrCabeceraDetallado[0, 1] = "0";
            arrCabeceraDetallado[0, 2] = "C";
            arrCabeceraDetallado[0, 3] = "";
            arrCabeceraDetallado[0, 4] = "n_id";

            arrCabeceraDetallado[1, 0] = "Nº Documento";
            arrCabeceraDetallado[1, 1] = "70";
            arrCabeceraDetallado[1, 2] = "C";
            arrCabeceraDetallado[1, 3] = "";
            arrCabeceraDetallado[1, 4] = "c_numdocide";

            arrCabeceraDetallado[2, 0] = "Apellidos y Nombres";
            arrCabeceraDetallado[2, 1] = "300";
            arrCabeceraDetallado[2, 2] = "C";
            arrCabeceraDetallado[2, 3] = "";
            arrCabeceraDetallado[2, 4] = "c_apenom";


            arrCabeceraDetallado[3, 0] = "Fecha";
            arrCabeceraDetallado[3, 1] = "70";
            arrCabeceraDetallado[3, 2] = "F";
            arrCabeceraDetallado[3, 3] = "dd/MM/yyyy";
            arrCabeceraDetallado[3, 4] = "d_fecha";

            arrCabeceraDetallado[4, 0] = "Hora";
            arrCabeceraDetallado[4, 1] = "70";
            arrCabeceraDetallado[4, 2] = "H";
            arrCabeceraDetallado[4, 3] = "";
            arrCabeceraDetallado[4, 4] = "c_hor";

            arrCabecera1[0, 0] = "Id";
            arrCabecera1[0, 1] = "0";
            arrCabecera1[0, 2] = "C";
            arrCabecera1[0, 3] = "";
            arrCabecera1[0, 4] = "n_id";

            arrCabecera1[1, 0] = "Nº Documento";
            arrCabecera1[1, 1] = "70";
            arrCabecera1[1, 2] = "C";
            arrCabecera1[1, 3] = "";
            arrCabecera1[1, 4] = "c_numdocide";

            arrCabecera1[2, 0] = "Apellidos y Nombres";
            arrCabecera1[2, 1] = "300";
            arrCabecera1[2, 2] = "C";
            arrCabecera1[2, 3] = "";
            arrCabecera1[2, 4] = "c_apenom";

            arrCabecera2[0, 0] = "Id";
            arrCabecera2[0, 1] = "0";
            arrCabecera2[0, 2] = "C";
            arrCabecera2[0, 3] = "";
            arrCabecera2[0, 4] = "n_id";

            arrCabecera2[1, 0] = "Nº Documento";
            arrCabecera2[1, 1] = "70";
            arrCabecera2[1, 2] = "C";
            arrCabecera2[1, 3] = "";
            arrCabecera2[1, 4] = "c_numdocide";

            arrCabecera2[2, 0] = "Apellidos y Nombres";
            arrCabecera2[2, 1] = "300";
            arrCabecera2[2, 2] = "C";
            arrCabecera2[2, 3] = "";
            arrCabecera2[2, 4] = "c_apenom";


            arrCabecera3[0, 0] = "Fecha";
            arrCabecera3[0, 1] = "70";
            arrCabecera3[0, 2] = "F";
            arrCabecera3[0, 3] = "dd/MM/yyyy";
            arrCabecera3[0, 4] = "d_fecha";

            arrCabecera3[1, 0] = "Hora Ingreso";
            arrCabecera3[1, 1] = "70";
            arrCabecera3[1, 2] = "H";
            arrCabecera3[1, 3] = "";
            arrCabecera3[1, 4] = "c_horing";

            arrCabecera3[2, 0] = "Hora Salida";
            arrCabecera3[2, 1] = "70";
            arrCabecera3[2, 2] = "H";
            arrCabecera3[2, 3] = "";
            arrCabecera3[2, 4] = "c_horsal";

            arrCabecera3[3, 0] = "Horas Trabajadas";
            arrCabecera3[3, 1] = "70";
            arrCabecera3[3, 2] = "H";
            arrCabecera3[3, 3] = "";
            arrCabecera3[3, 4] = "h_hortra";
        }

        private void FrmMarcacion_Resize(object sender, EventArgs e)
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
       	private void ArrayRedimensionar(ref string[,] original, int cols, int rows)
        {
            //create a new 2 dimensional array with
            //the size we want
            string[,] newArray = new string[rows, cols];
            //copy the contents of the old array to the new one
            Array.Copy(original, newArray,0);
            //set the original to the new array
            original = newArray;
        }
        void ConfigurarCabecera()
        {
            DateTime d_fchini = Convert.ToDateTime(TxtFchIni.Text);
            DateTime d_fchfin = Convert.ToDateTime(TxtFchFin.Text);
            DateTime d_fchact = Convert.ToDateTime(TxtFchIni.Text);
            int n_row = 0;
            int n_numcol = 1;

            for(n_row = 0; n_row <= 100; n_row++)
            {
                if (d_fchact >= d_fchfin)
                {
                    break;
                }
                d_fchact= d_fchact.AddDays(1);
                n_numcol = n_numcol + 1;
            }

            ArrayRedimensionar(ref arrCabecera1, 5, 3);
            ArrayRedimensionar(ref arrCabecera1, 5, 3 + n_numcol);

            ArrayRedimensionar(ref arrCabecera2, 5, 3);
            ArrayRedimensionar(ref arrCabecera2, 5, 3 + n_numcol);

            Cabecera1();

            int n_fil = 3;
            n_NUMCOL = 0;
            d_fchact = d_fchini;
            for (n_row = 1; n_row <= n_numcol ; n_row++)
            {
                arrCabecera1[n_fil, 0] = Convert.ToDateTime(d_fchact).ToString("dd/MM/yyyy");
                arrCabecera1[n_fil, 1] = "70";
                arrCabecera1[n_fil, 2] = "D";
                arrCabecera1[n_fil, 3] = "0.00";
                arrCabecera1[n_fil, 4] = d_fchact.ToString("yyyy-MM-dd");

                arrCabecera2[n_fil, 0] = Convert.ToDateTime(d_fchact).ToString("dd/MM/yyyy");
                arrCabecera2[n_fil, 1] = "70";
                arrCabecera2[n_fil, 2] = "C";
                arrCabecera2[n_fil, 3] = "";
                arrCabecera2[n_fil, 4] = d_fchact.ToString("yyyy-MM-dd");

                d_fchact = d_fchact.AddDays(1);
                n_fil = n_fil + 1;
                n_NUMCOL = n_NUMCOL + 1;
            }
        }
        
        void EjecutarConsulta()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                ConfigurarCabecera();
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

                DataTable dtListaDetallado = new DataTable();
                DataTable dtLista = new DataTable();
                DataTable dtLista2 = new DataTable();
                int m_tiprep = 0;
                CN_pla_empleados o_emple = new CN_pla_empleados(STU_SISTEMA);
                o_emple.STU_SISTEMA = STU_SISTEMA;

                if (OpTod.Checked == true) { m_tiprep = 1; }
                if (OptPen.Checked == true) { m_tiprep = 2; }
                if (OptEnt.Checked == true) { m_tiprep = 3; }

                o_emple.ListarAsistenciaDetallado(TxtFchIni.Text, TxtFchFin.Text);
                dtListaDetallado = o_emple.dtLista;

                o_emple.ListarAsistenciaDec(TxtFchIni.Text, TxtFchFin.Text);
                dtLista = o_emple.dtLista;

                o_emple.ListarAsistenciaHor(TxtFchIni.Text, TxtFchFin.Text);
                dtLista2 = o_emple.dtLista;

                funFlex.b_AlternarColor = true;

                funFlex.FlexMostrarDatos(FgDetallado, arrCabeceraDetallado, dtListaDetallado, 2, true);
                funFlex = new Cls_FlexGrid();
                funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 3, true);
                funFlex = new Cls_FlexGrid();
                funFlex.FlexMostrarDatos(FgDatos2, arrCabecera2, dtLista2, 3, true);

                SetearCabecera1();
                ResaltarErrores();
                MessageBox.Show("¡ Los datos se mostraron con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                FgDetallado.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        void ResaltarErrores()
        { 
            int n_row = 0;
            int n_col = 0;
            double n_valor = 0;
            string c_valor = "";

            for (n_col = 4; n_col <= FgDatos.Cols.Count - 1; n_col++)
            {
                for (n_row = 3; n_row <= FgDatos.Rows.Count - 1; n_row++)
                { 
                    n_valor = Convert.ToDouble(funFunciones.NulosN(FgDatos.GetData(n_row, n_col)));
                    if (n_valor == 0)
                    {
                        funFlex.Flex_PintarCeldas(FgDatos, n_row, n_col, n_row, n_col, Color.Red);
                    }
                }
            }

            for (n_col = 4; n_col <= FgDatos2.Cols.Count - 1; n_col++)
            {
                for (n_row = 3; n_row <= FgDatos2.Rows.Count - 1; n_row++)
                {
                    c_valor = funFunciones.NulosC(FgDatos2.GetData(n_row, n_col)).ToString();
                    if (c_valor == "00:00:00")
                    {
                        funFlex.Flex_PintarCeldas(FgDatos2, n_row, n_col, n_row, n_col, Color.Red);
                    }
                }
            }
        }
        void SetearCabecera1()
        {
            funFlex.Flex_FixUniColumnas(FgDatos, 0, 4, 4 + (n_NUMCOL - 1), "DIAS LABORADOS", 1);
            funFlex.Flex_FixUniColumnas(FgDatos2, 0, 4, 4 + (n_NUMCOL - 1), "DIAS LABORADOS", 1);
        }

        private void ToolExportar_Click(object sender, EventArgs e)
        {
            string c_cad = "DEL " + TxtFchIni.Text + " AL " + TxtFchFin.Text;
            string c_nomarch = STU_SISTEMA.EMPRESARUC + "-" + STU_SISTEMA.ANOTRABAJO.ToString() + "-" + STU_SISTEMA.MESTRABAJO.ToString() + "-MARCACION-" + ".xls";
            funFlex.ExportToExcel(FgDatos, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "MARCACION DEL PERSONAL", c_cad, c_nomarch);
        }

        private void ToolHerramientas_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void CmdActDat_Click(object sender, EventArgs e)
        {
            if (FgDatos.Rows.Count == 3)
            {
                MessageBox.Show("¡ No hay datos para vizualizar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            Sz1.Enabled = false;
            ToolHerramientas.Enabled = false;
            LblIdEmple.Text = funFunciones.NulosC(FgDatos.GetData(FgDatos.Row, 2));
            LblApeNom.Text = funFunciones.NulosC(FgDatos.GetData(FgDatos.Row, 3));
            TxtFchIni2.Value = Convert.ToDateTime(TxtFchIni.Text);
            TxtFchFin2.Value = Convert.ToDateTime(TxtFchFin.Text);
            Panel2.Left = ((this.Width - Panel2.Width) / 2);
            Panel2.Top = ((this.Height - Panel2.Height) / 2);
            Panel2.Refresh();
            CargarMarcacion();
            Panel2.Visible = true;
        }
        void CargarMarcacion()
        {
            DataTable dtlis = new DataTable();
            CN_pla_empleados o_pla = new CN_pla_empleados(STU_SISTEMA);
            o_pla.STU_SISTEMA = STU_SISTEMA;
            o_pla.ListarMarcacion(LblIdEmple.Text, TxtFchIni2.Value.ToString("dd/MM/yyyy"), TxtFchFin2.Value.ToString("dd/MM/yyyy"));
            dtlis = o_pla.dtLista;
            funFlex.FlexMostrarDatos(FgMarca, arrCabecera3, dtlis, 2, true);
            o_pla = null;

            int n_row = 0;
            for (n_row = 2; n_row <= FgMarca.Rows.Count - 1; n_row++)
            {
                if (funFunciones.NulosC(FgMarca.GetData(n_row, 4)) == "00:00:00")
                {
                    funFlex.Flex_PintarCeldas(FgMarca, n_row, 4, n_row, 4, Color.Red);
                }
            }
        }
        private void CmdSalir_Click(object sender, EventArgs e)
        {
            Sz1.Enabled = true;
            ToolHerramientas.Enabled = true;
            Panel2.Visible = false;
        }

        private void CmdImp_Click(object sender, EventArgs e)
        {
            int n_row = 0;
            List<BE_PLA_MARCACION2> l_pla = new List<BE_PLA_MARCACION2>();
            for (n_row = 2; n_row <= FgMarca.Rows.Count - 1; n_row++)
            {
                BE_PLA_MARCACION2 e_pla = new BE_PLA_MARCACION2();

                e_pla.n_id = 0;
                e_pla.c_numdoc = LblIdEmple.Text;
                e_pla.c_nomemp = LblApeNom.Text;
                e_pla.d_fecha = Convert.ToDateTime(Convert.ToDateTime(funFunciones.NulosC(FgMarca.GetData(n_row,1)))+ " " + funFunciones.NulosC(FgMarca.GetData(n_row,2)));
                e_pla.n_tipo = 1;
                l_pla.Add(e_pla);

                BE_PLA_MARCACION2 e_pla2 = new BE_PLA_MARCACION2();

                e_pla2.n_id = 0;
                e_pla2.c_numdoc = LblIdEmple.Text;
                e_pla2.c_nomemp = LblApeNom.Text;
                e_pla2.d_fecha = Convert.ToDateTime(Convert.ToDateTime(funFunciones.NulosC(FgMarca.GetData(n_row, 1))) + " " + funFunciones.NulosC(FgMarca.GetData(n_row, 3)));
                e_pla2.n_tipo = 2;
                l_pla.Add(e_pla2);
            }
            CN_pla_marcacion2 o_mar = new CN_pla_marcacion2(STU_SISTEMA);
            o_mar.STU_SISTEMA = STU_SISTEMA;
            if (o_mar.Insertar(l_pla, TxtFchIni.Text, TxtFchFin.Text) == false)
            {
                MessageBox.Show("¡ No se pudieron guardar los cambios por el siguiente motivo: " + o_mar.c_ErrorMensaje + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            MessageBox.Show("¡ Los datos se guardaron con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            CmdSalir_Click(sender, e);
        }
    }
}
