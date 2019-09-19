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
using SIAC_Negocio.Gestion;
using SIAC_Negocio.Logistica;
using SIAC_Entidades.Gestion;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using Helper;
using SIAC_Objetos.Sistema;

namespace SSF_NET_Gestion.Formularios
{
    public partial class FrmComprasPeriodo : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public bool b_cargado = false;
        public string[,] a_Datos = new string[1, 13];
        public string c_anostra;
        public DataTable dtItems = new DataTable();
        public int n_IdItem;
        public DataTable dtHistorico = new DataTable();
        public string c_valores;

        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_FlexGrid funFlex = new Cls_FlexGrid();

        bool booSeEjecuto = false;
        string[,] arrCabeceraFlex1 = new string[16, 5];
        string strNumerovalidos = "1234567890." + (char)8;   

        public FrmComprasPeriodo()
        {
            InitializeComponent();
        }

        private void FrmComprasPeriodo_Activated(object sender, EventArgs e)
        {
            if (booSeEjecuto == false)
            {
                b_cargado = true;
                ConfigurarGrid();
                MostrarDatosItems();
                b_cargado = false;
                booSeEjecuto = true;
            }
        }
        void MostrarDatosItems()
        {
            int n_row = 0;
            int n_numanos = 0;
            double n_valor;
            DataTable dtres1 = new DataTable();
            DataTable dtres2 = new DataTable();
            CN_log_compras o_com = new CN_log_compras();

            dtres1 = funDatos.DataTableFiltrar(dtItems, "n_id = " + n_IdItem + "");
            if (dtres1.Rows.Count != 0)
            {
                TxtCodPro.Text = dtres1.Rows[0]["c_codpro"].ToString();
                TxtDes.Text = dtres1.Rows[0]["c_despro"].ToString();
                TxtUniMed.Text = dtres1.Rows[0]["c_abrpre"].ToString();
            }
            o_com.mysConec = mysConec;
            o_com.Consulta10(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, 1);
            dtres1 = o_com.dtLista;
            o_com.Consulta10(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, 2);
            dtres2 = o_com.dtLista;

            dtres1 = funDatos.DataTableFiltrar(dtres1, "n_iditem = " + n_IdItem.ToString() + "");
            dtres2 = funDatos.DataTableFiltrar(dtres2, "n_iditem = " + n_IdItem.ToString() + "");
            funFlex.FlexMostrarDatos(FgUni, arrCabeceraFlex1, dtres1, 2, true);
            funFlex.FlexMostrarDatos(FgImp, arrCabeceraFlex1, dtres2, 2, true);

            string[] c_datos = c_valores.Split(',');
            FgCanReq.Rows.Count = FgCanReq.Rows.Count + 1;
            n_valor = Convert.ToDouble(c_datos[0]);
            FgCanReq.SetData(2, 1, n_valor.ToString("0.00"));
            n_valor = Convert.ToDouble(c_datos[1]);
            FgCanReq.SetData(2, 2, n_valor.ToString("0.00"));
            n_valor = Convert.ToDouble(c_datos[2]);
            FgCanReq.SetData(2, 3, n_valor.ToString("0.00"));
            n_valor = Convert.ToDouble(c_datos[3]);
            FgCanReq.SetData(2, 4, n_valor.ToString("0.00"));
            n_valor = Convert.ToDouble(c_datos[4]);
            FgCanReq.SetData(2, 5, n_valor.ToString("0.00"));
            n_valor = Convert.ToDouble(c_datos[5]);
            FgCanReq.SetData(2, 6, n_valor.ToString("0.00"));
            n_valor = Convert.ToDouble(c_datos[6]);
            FgCanReq.SetData(2, 7, n_valor.ToString("0.00"));
            n_valor = Convert.ToDouble(c_datos[7]);
            FgCanReq.SetData(2, 8, n_valor.ToString("0.00"));
            n_valor = Convert.ToDouble(c_datos[8]);
            FgCanReq.SetData(2, 9, n_valor.ToString("0.00"));
            n_valor = Convert.ToDouble(c_datos[9]);
            FgCanReq.SetData(2, 10, n_valor.ToString("0.00"));
            n_valor = Convert.ToDouble(c_datos[10]);
            FgCanReq.SetData(2, 11, n_valor.ToString("0.00"));
            n_valor = Convert.ToDouble(c_datos[11]);
            FgCanReq.SetData(2, 12, n_valor.ToString("0.00"));
            n_valor = Convert.ToDouble(c_datos[12]);
            FgCanReq.SetData(2, 13, n_valor.ToString("0.00"));

            FgUni.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            FgImp.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            FgCanReq.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;

            o_com = null;
        }
        void ConfigurarGrid()
        {
            FgUni.Rows.Count = 2;

            arrCabeceraFlex1[0, 0] = "Enero";
            arrCabeceraFlex1[0, 1] = "65";
            arrCabeceraFlex1[0, 2] = "D";
            arrCabeceraFlex1[0, 3] = "0.00";
            arrCabeceraFlex1[0, 4] = "n_ene";

            arrCabeceraFlex1[1, 0] = "Febrero";
            arrCabeceraFlex1[1, 1] = "65";
            arrCabeceraFlex1[1, 2] = "D";
            arrCabeceraFlex1[1, 3] = "0.00";
            arrCabeceraFlex1[1, 4] = "n_feb";

            arrCabeceraFlex1[2, 0] = "Marzo";
            arrCabeceraFlex1[2, 1] = "65";
            arrCabeceraFlex1[2, 2] = "D";
            arrCabeceraFlex1[2, 3] = "0.00";
            arrCabeceraFlex1[2, 4] = "n_mar";

            arrCabeceraFlex1[3, 0] = "Abril";
            arrCabeceraFlex1[3, 1] = "65";
            arrCabeceraFlex1[3, 2] = "D";
            arrCabeceraFlex1[3, 3] = "0.00";
            arrCabeceraFlex1[3, 4] = "n_abr";

            arrCabeceraFlex1[4, 0] = "Mayo";
            arrCabeceraFlex1[4, 1] = "65";
            arrCabeceraFlex1[4, 2] = "D";
            arrCabeceraFlex1[4, 3] = "0.00";
            arrCabeceraFlex1[4, 4] = "n_may";

            arrCabeceraFlex1[5, 0] = "Junio";
            arrCabeceraFlex1[5, 1] = "65";
            arrCabeceraFlex1[5, 2] = "D";
            arrCabeceraFlex1[5, 3] = "0.00";
            arrCabeceraFlex1[5, 4] = "n_jun";

            arrCabeceraFlex1[6, 0] = "Julio";
            arrCabeceraFlex1[6, 1] = "65";
            arrCabeceraFlex1[6, 2] = "D";
            arrCabeceraFlex1[6, 3] = "0.00";
            arrCabeceraFlex1[6, 4] = "n_jul";

            arrCabeceraFlex1[7, 0] = "Agosto";
            arrCabeceraFlex1[7, 1] = "65";
            arrCabeceraFlex1[7, 2] = "D";
            arrCabeceraFlex1[7, 3] = "0.00";
            arrCabeceraFlex1[7, 4] = "n_ago";

            arrCabeceraFlex1[8, 0] = "Setiembre";
            arrCabeceraFlex1[8, 1] = "65";
            arrCabeceraFlex1[8, 2] = "D";
            arrCabeceraFlex1[8, 3] = "0.00";
            arrCabeceraFlex1[8, 4] = "n_set";

            arrCabeceraFlex1[9, 0] = "Octubre";
            arrCabeceraFlex1[9, 1] = "65";
            arrCabeceraFlex1[9, 2] = "D";
            arrCabeceraFlex1[9, 3] = "0.00";
            arrCabeceraFlex1[9, 4] = "n_oct";

            arrCabeceraFlex1[10, 0] = "Noviembre";
            arrCabeceraFlex1[10, 1] = "65";
            arrCabeceraFlex1[10, 2] = "D";
            arrCabeceraFlex1[10, 3] = "0.00";
            arrCabeceraFlex1[10, 4] = "n_nov";

            arrCabeceraFlex1[11, 0] = "Diciembre";
            arrCabeceraFlex1[11, 1] = "65";
            arrCabeceraFlex1[11, 2] = "D";
            arrCabeceraFlex1[11, 3] = "0.00";
            arrCabeceraFlex1[11, 4] = "n_dic";

            arrCabeceraFlex1[12, 0] = "Total";
            arrCabeceraFlex1[12, 1] = "70";
            arrCabeceraFlex1[12, 2] = "D";
            arrCabeceraFlex1[12, 3] = "0.00";
            arrCabeceraFlex1[12, 4] = "n_canpro";

            funFlex.FlexMostrarDatos(FgUni, arrCabeceraFlex1, dtItems, 2, false);
            funFlex.FlexMostrarDatos(FgImp, arrCabeceraFlex1, dtItems, 2, false);
            funFlex.FlexMostrarDatos(FgCanReq, arrCabeceraFlex1, dtItems, 2, false);
                        
            FgUni.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
        }

        private void CmdSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmComprasPeriodo_Load(object sender, EventArgs e)
        {
            this.Text = "GESTION - Compras Realizadas del Periodo";
        }
    }
}
