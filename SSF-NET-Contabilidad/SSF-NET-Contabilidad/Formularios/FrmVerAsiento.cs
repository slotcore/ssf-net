using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Contabilidad;
using SIAC_Entidades.Logistica;
using SIAC_Negocio.Contabilidad;
using SIAC_Negocio.Maestros;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Logistica;
using SIAC_Negocio.Ventas;
using SIAC_Negocio.Sunat;
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

namespace SSF_NET_Contabilidad.Formularios
{
    public partial class FrmVerAsiento : Form
    {
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public string c_NumAsiento;
        public int n_IdLibro;
        public int n_AnoTrabajo;
        public int n_MesTrabajo;
        public int n_IdEmpresa;

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        Cls_Controles funControl = new Cls_Controles();

        CN_sun_libro objLib = new CN_sun_libro();

        DataTable dtLista = new DataTable();
        DataTable dtLibro = new DataTable();

        string[,] arrCabeceraFlex1 = new string[7, 5];
        public FrmVerAsiento()
        {
            InitializeComponent();
        }
        //con_diario_consultaasiento
        void ConfigurarFormulario()
        {
            this.Height = 322;
            this.Width = 770;
            
            this.Text = "CONTABILIDAD - ASIENTO CONTABLE DE LA OPERACION";
            
            arrCabeceraFlex1[0, 0] = "Nº Cuenta";
            arrCabeceraFlex1[0, 1] = "80";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_cuecon";

            arrCabeceraFlex1[1, 0] = "Descripcion Cta. Contable";
            arrCabeceraFlex1[1, 1] = "300";
            arrCabeceraFlex1[1, 2] = "C";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_des";

            arrCabeceraFlex1[2, 0] = "T.C.";
            arrCabeceraFlex1[2, 1] = "60";
            arrCabeceraFlex1[2, 2] = "D";
            arrCabeceraFlex1[2, 3] = "0.000";
            arrCabeceraFlex1[2, 4] = "n_tc";

            arrCabeceraFlex1[3, 0] = "Debe";
            arrCabeceraFlex1[3, 1] = "70";
            arrCabeceraFlex1[3, 2] = "D";
            arrCabeceraFlex1[3, 3] = "0.00";
            arrCabeceraFlex1[3, 4] = "n_impdebsol";

            arrCabeceraFlex1[4, 0] = "Haber";
            arrCabeceraFlex1[4, 1] = "70";
            arrCabeceraFlex1[4, 2] = "D";
            arrCabeceraFlex1[4, 3] = "0.00";
            arrCabeceraFlex1[4, 4] = "n_imphabsol";

            arrCabeceraFlex1[5, 0] = "Debe";
            arrCabeceraFlex1[5, 1] = "70";
            arrCabeceraFlex1[5, 2] = "D";
            arrCabeceraFlex1[5, 3] = "0.00";
            arrCabeceraFlex1[5, 4] = "n_impdebdol";

            arrCabeceraFlex1[6, 0] = "Haber";
            arrCabeceraFlex1[6, 1] = "70";
            arrCabeceraFlex1[6, 2] = "D";
            arrCabeceraFlex1[6, 3] = "0.00";
            arrCabeceraFlex1[6, 4] = "n_imphabdol";
            
            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtLista, 2, false);
            FgItems.Rows.Count = 2;

        }
        private void CmdSal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmVerAsiento_Load(object sender, EventArgs e)
        {
            CargarDatos();
            ConfigurarFormulario();
            CargarAsiento();
        }
        void CargarDatos()
        {
            objLib.mysConec = mysConec;
            objLib.Listar();
            dtLibro = objLib.dtLista;

            TxtLibro.Text = funDatos.DataTableBuscar(dtLibro, "n_id", "c_des", n_IdLibro.ToString(), "N").ToString();

            TxtNumReg.Text = funDatos.DataTableBuscar(dtLibro, "n_id", "c_codsun", n_IdLibro.ToString(), "N").ToString();
            TxtNumReg.Text = TxtNumReg.Text + n_MesTrabajo.ToString("00") + c_NumAsiento;
        }
        void CargarAsiento()
        {
            //int n_row = 0;
            CN_con_diario funDiario = new CN_con_diario();
            funDiario.mysConec = mysConec;
            funDiario.ConsultaAsiento(n_IdEmpresa,n_AnoTrabajo, n_MesTrabajo, n_IdLibro, c_NumAsiento);
            dtLista = funDiario.dtLista;
            if (dtLista.Rows.Count != 0)
            {
                funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtLista, 2, true);
            }

            double n_tot1 = 0;
            double n_tot2 = 0;
            double n_tot3 = 0;
            double n_tot4 = 0;

            n_tot1 = funFlex.FlexSumarCol(FgItems, 4, 2, FgItems.Rows.Count - 1);
            n_tot2 = funFlex.FlexSumarCol(FgItems, 5, 2, FgItems.Rows.Count - 1);
            n_tot3 = funFlex.FlexSumarCol(FgItems, 6, 2, FgItems.Rows.Count - 1);
            n_tot4 = funFlex.FlexSumarCol(FgItems, 7, 2, FgItems.Rows.Count - 1);

            TxtTot1.Text = n_tot1.ToString("0.00");
            TxtTot2.Text = n_tot2.ToString("0.00");
            TxtTot3.Text = n_tot3.ToString("0.00");
            TxtTot4.Text = n_tot4.ToString("0.00");
        }
    }
}
