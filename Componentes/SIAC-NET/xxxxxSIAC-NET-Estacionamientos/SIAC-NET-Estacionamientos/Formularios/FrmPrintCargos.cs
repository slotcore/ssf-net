using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Negocio.Estacionamiento;
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

namespace SIAC_NET_Estacionamientos.Formularios
{
    public partial class FrmPrintCargos : Form
    {
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public int N_IDPLAYA =0;
        public int N_IDCAJERO = 0;
        public int N_IDTIPDOC = 0;
        public string C_LOCAL;
        public string C_CAJERO;

        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        string[,] arrCabeceraFlexFil = new string[9, 5];

        public FrmPrintCargos()
        {
            InitializeComponent();
        }
        private void c1Sizer1_Click(object sender, EventArgs e)
        {

        }
        private void FrmPrintCargos_Load(object sender, EventArgs e)
        {
            DataTable dtresult = new DataTable();

            this.Text = "Consulta de cobranza a Abonados";
            this.Width = 890;
            this.Height = 560;
                
            TxtNumReg.Text = "";
            TxtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");

            // FLEX GRID DE LOS TAREAS
            arrCabeceraFlexFil[0, 0] = "Nº Doc. Identidad";
            arrCabeceraFlexFil[0, 1] = "65";
            arrCabeceraFlexFil[0, 2] = "C";
            arrCabeceraFlexFil[0, 3] = "";
            arrCabeceraFlexFil[0, 4] = "c_clinumdocide";

            arrCabeceraFlexFil[1, 0] = "Apellidos y Nombres";
            arrCabeceraFlexFil[1, 1] = "200";
            arrCabeceraFlexFil[1, 2] = "C";
            arrCabeceraFlexFil[1, 3] = "";
            arrCabeceraFlexFil[1, 4] = "c_clinom";

            arrCabeceraFlexFil[2, 0] = "Servicio";
            arrCabeceraFlexFil[2, 1] = "300";
            arrCabeceraFlexFil[2, 2] = "C";
            arrCabeceraFlexFil[2, 3] = "";
            arrCabeceraFlexFil[2, 4] = "c_desser";

            arrCabeceraFlexFil[3, 0] = "Tip. Doc.";
            arrCabeceraFlexFil[3, 1] = "35";
            arrCabeceraFlexFil[3, 2] = "C";
            arrCabeceraFlexFil[3, 3] = "";
            arrCabeceraFlexFil[3, 4] = "c_tipdocdes";

            arrCabeceraFlexFil[4, 0] = "Nº Documento";
            arrCabeceraFlexFil[4, 1] = "105";
            arrCabeceraFlexFil[4, 2] = "C";
            arrCabeceraFlexFil[4, 3] = "";
            arrCabeceraFlexFil[4, 4] = "c_numdoc";

            arrCabeceraFlexFil[5, 0] = "Fecha Documento";
            arrCabeceraFlexFil[5, 1] = "70";
            arrCabeceraFlexFil[5, 2] = "F";
            arrCabeceraFlexFil[5, 3] = "dd/MM/yyy";
            arrCabeceraFlexFil[5, 4] = "d_fchdoc";

            arrCabeceraFlexFil[6, 0] = "Importe";
            arrCabeceraFlexFil[6, 1] = "60";
            arrCabeceraFlexFil[6, 2] = "D";
            arrCabeceraFlexFil[6, 3] = "0.00";
            arrCabeceraFlexFil[6, 4] = "n_imptotven";

            arrCabeceraFlexFil[7, 0] = "Id";
            arrCabeceraFlexFil[7, 1] = "0";
            arrCabeceraFlexFil[7, 2] = "N";
            arrCabeceraFlexFil[7, 3] = "";
            arrCabeceraFlexFil[7, 4] = "n_venid";

            arrCabeceraFlexFil[8, 0] = "Idtipdoc";
            arrCabeceraFlexFil[8, 1] = "0";
            arrCabeceraFlexFil[8, 2] = "N";
            arrCabeceraFlexFil[8, 3] = "";
            arrCabeceraFlexFil[8, 4] = "n_idtipdoc";

            funFlex.FlexMostrarDatos(FgReg, arrCabeceraFlexFil, dtresult, 2, true);
            MostrarCobranza();
        }
        void MostrarCobranza()
        {
            DataTable dtresult = new DataTable();
            CN_est_movimientos o_mov = new CN_est_movimientos(STU_SISTEMA);
            
            o_mov.Consulta2(STU_SISTEMA.EMPRESAID, N_IDPLAYA, N_IDCAJERO, TxtFecha.Text, TxtFecha.Text);
            if (o_mov.b_OcurrioError == true)
            {
                o_mov = null;
                return;
            }

            dtresult = o_mov.dtListar;
            if (dtresult.Rows.Count == 0)
            {
                o_mov = null;
                return;
            }

            funFlex.FlexMostrarDatos(FgReg, arrCabeceraFlexFil, dtresult, 2, true);
            TxtNumReg.Text = (dtresult.Rows.Count - 1).ToString();
        }
        private void TooNuevo_Click(object sender, EventArgs e)
        {
            if (FgReg.Rows.Count == 2) { return; }
            int n_idvta = Convert.ToInt32(FgReg.GetData(FgReg.Row, 8));
            CN_est_movimientos objRegistro = new CN_est_movimientos(STU_SISTEMA);
            objRegistro.STU_SISTEMA = STU_SISTEMA;
            objRegistro.ImprimirComprobantePago(STU_SISTEMA.EMPRESAID, n_idvta, "", 0, 2,1);
            objRegistro = null;

            //int n_idvta = Convert.ToInt32(FgReg.GetData(FgReg.Row, 7));
            //string c_dato = "" + "|" + FgReg.GetData(FgReg.Row, 2) + "|" + DateTime.Now.ToString("dd/MM/yyyy") + "|" + "" + "|" + "" + "|" + C_CAJERO + "|" + C_LOCAL + "|" + FgReg.GetData(FgReg.Row, 4) + "|" + FgReg.GetData(FgReg.Row, 6) + "|" + FgReg.GetData(FgReg.Row, 3);

            //CN_est_movimientos objRegistro = new CN_est_movimientos(STU_SISTEMA);
            //objRegistro.STU_SISTEMA = STU_SISTEMA;
            //objRegistro.ImprimirComprobantePago(STU_SISTEMA.EMPRESAID, n_idvta, c_dato, Convert.ToInt32(FgReg.GetData(FgReg.Row, 8)), 2);
            //objRegistro = null;
        }
        private void ToolSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TxtFecha_ValueChanged(object sender, EventArgs e)
        {
            MostrarCobranza();
        }

        private void ToolPdf_Click(object sender, EventArgs e)
        {
            if (FgReg.Rows.Count == 2) { return; }
            int n_idvta = Convert.ToInt32(FgReg.GetData(FgReg.Row, 8));
            CN_est_movimientos objRegistro = new CN_est_movimientos(STU_SISTEMA);
            objRegistro.STU_SISTEMA = STU_SISTEMA;
            objRegistro.ExportarPdf(STU_SISTEMA.EMPRESAID, n_idvta);
            objRegistro = null;
        }
    }
}
