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
using SIAC_Entidades.Gestion;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using Helper;
using SIAC_Objetos.Sistema;

namespace SSF_NET_Gestion.Formularios
{
    public partial class FrmManPlanVentasHistorico : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public bool b_cargado = false;
        public string[,] a_Datos = new string[13, 2];
        public string c_anostra;
        public DataTable dtItems = new DataTable();
        public int n_IdItem;
        public DataTable dtHistorico = new DataTable();

        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_FlexGrid funFlex = new Cls_FlexGrid();

        bool booSeEjecuto = false;
        string[,] arrCabeceraFlex1 = new string[16, 5];
        string strNumerovalidos = "1234567890." + (char)8;                                       

        public FrmManPlanVentasHistorico()
        {
            InitializeComponent();
        }
        private void CmdAddItem_Click(object sender, EventArgs e)
        {
            // AGREGAMOS EL AÑO SELECCIONADO + EL PORCENTAJE ASIGNADO
            string c_dato = "";
            double n_valor = 0;
            double n_porcen = Convert.ToDouble(funFunciones.NulosN(TxtPor.Text));
            n_porcen = ((n_porcen / 100) + 1);
            
            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Row,2));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 2, c_dato);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Row, 3));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 3, c_dato);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Row, 4));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 4, c_dato);
            
            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Row, 5));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 5, c_dato);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Row, 6));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 6, c_dato);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Row, 7));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 7, c_dato);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Row, 8));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 8, c_dato);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Row, 9));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 9, c_dato);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Row, 10));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 10, c_dato);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Row, 11));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 11, c_dato);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Row, 12));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 12, c_dato);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Row, 13));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 13, c_dato);
        }
        private void FrmManPlanVentasHistorico_Activated(object sender, EventArgs e)
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
        void ConfigurarGrid()
        {
            FgItems.Rows.Count = 2;

            arrCabeceraFlex1[0, 0] = "Año";
            arrCabeceraFlex1[0, 1] = "80";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "n_anotra";

            arrCabeceraFlex1[1, 0] = "Enero";
            arrCabeceraFlex1[1, 1] = "65";
            arrCabeceraFlex1[1, 2] = "D";
            arrCabeceraFlex1[1, 3] = "0.00";
            arrCabeceraFlex1[1, 4] = "January";

            arrCabeceraFlex1[2, 0] = "Febrero";
            arrCabeceraFlex1[2, 1] = "65";
            arrCabeceraFlex1[2, 2] = "D";
            arrCabeceraFlex1[2, 3] = "0.00";
            arrCabeceraFlex1[2, 4] = "February";

            arrCabeceraFlex1[3, 0] = "Marzo";
            arrCabeceraFlex1[3, 1] = "65";
            arrCabeceraFlex1[3, 2] = "D";
            arrCabeceraFlex1[3, 3] = "0.00";
            arrCabeceraFlex1[3, 4] = "March";

            arrCabeceraFlex1[4, 0] = "Abril";
            arrCabeceraFlex1[4, 1] = "65";
            arrCabeceraFlex1[4, 2] = "D";
            arrCabeceraFlex1[4, 3] = "0.00";
            arrCabeceraFlex1[4, 4] = "April";

            arrCabeceraFlex1[5, 0] = "Mayo";
            arrCabeceraFlex1[5, 1] = "65";
            arrCabeceraFlex1[5, 2] = "D";
            arrCabeceraFlex1[5, 3] = "0.00";
            arrCabeceraFlex1[5, 4] = "May";

            arrCabeceraFlex1[6, 0] = "Junio";
            arrCabeceraFlex1[6, 1] = "65";
            arrCabeceraFlex1[6, 2] = "D";
            arrCabeceraFlex1[6, 3] = "0.00";
            arrCabeceraFlex1[6, 4] = "June";

            arrCabeceraFlex1[7, 0] = "Julio";
            arrCabeceraFlex1[7, 1] = "65";
            arrCabeceraFlex1[7, 2] = "D";
            arrCabeceraFlex1[7, 3] = "0.00";
            arrCabeceraFlex1[7, 4] = "July";

            arrCabeceraFlex1[8, 0] = "Agosto";
            arrCabeceraFlex1[8, 1] = "65";
            arrCabeceraFlex1[8, 2] = "D";
            arrCabeceraFlex1[8, 3] = "0.00";
            arrCabeceraFlex1[8, 4] = "August";

            arrCabeceraFlex1[9, 0] = "Setiembre";
            arrCabeceraFlex1[9, 1] = "65";
            arrCabeceraFlex1[9, 2] = "D";
            arrCabeceraFlex1[9, 3] = "0.00";
            arrCabeceraFlex1[9, 4] = "September";

            arrCabeceraFlex1[10, 0] = "Octubre";
            arrCabeceraFlex1[10, 1] = "65";
            arrCabeceraFlex1[10, 2] = "D";
            arrCabeceraFlex1[10, 3] = "0.00";
            arrCabeceraFlex1[10, 4] = "October";

            arrCabeceraFlex1[11, 0] = "Noviembre";
            arrCabeceraFlex1[11, 1] = "65";
            arrCabeceraFlex1[11, 2] = "D";
            arrCabeceraFlex1[11, 3] = "0.00";
            arrCabeceraFlex1[11, 4] = "Novenber";

            arrCabeceraFlex1[12, 0] = "Diciembre";
            arrCabeceraFlex1[12, 1] = "65";
            arrCabeceraFlex1[12, 2] = "D";
            arrCabeceraFlex1[12, 3] = "0.00";
            arrCabeceraFlex1[12, 4] = "December";

            arrCabeceraFlex1[13, 0] = "Total";
            arrCabeceraFlex1[13, 1] = "70";
            arrCabeceraFlex1[13, 2] = "D";
            arrCabeceraFlex1[13, 3] = "0.00";
            arrCabeceraFlex1[13, 4] = "n_canpro";

            arrCabeceraFlex1[14, 0] = "Promedio Mensual";
            arrCabeceraFlex1[14, 1] = "70";
            arrCabeceraFlex1[14, 2] = "D";
            arrCabeceraFlex1[14, 3] = "0.00";
            arrCabeceraFlex1[14, 4] = "n_promedio";

            arrCabeceraFlex1[15, 0] = "Desv. Standar";
            arrCabeceraFlex1[15, 1] = "70";
            arrCabeceraFlex1[15, 2] = "D";
            arrCabeceraFlex1[15, 3] = "0.00";
            arrCabeceraFlex1[15, 4] = "n_dessta";

            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtItems, 2, false);
            funFlex.FlexMostrarDatos(FgDato, arrCabeceraFlex1, dtItems, 2, false);
            FgDato.Rows.Count = FgDato.Rows.Count + 1;
            FgItems.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            //FgDato.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            FgDato.AllowEditing = true;
        }
        double  HallarPromedio(string[] c_Datos)
        {
            int n_row = 0;
            int n_numele = Convert.ToInt32(c_Datos.GetLongLength(0));
            string[,] c_elementos = new string[n_numele, 4];
            double n_total = 0;
            double n_promedio = 0;
            double n_totalraiz = 0;
            double n_varianza = 0;
            double n_desviacion = 0;
            // POBLAMOS EL ARRAY
            for (n_row = 0; n_row <= Convert.ToInt32(c_Datos.GetLongLength(0))-1; n_row++)
            {
                c_elementos[n_row, 0] = c_Datos[n_row];
                n_total = n_total + Convert.ToDouble(c_Datos[n_row]);
            }
            n_promedio = (n_total / n_numele);

            // CALCULAMOS LOS DATOS PARA HALLAR LAS DESVIACION
            for (n_row = 0; n_row <= Convert.ToInt32(c_Datos.GetLongLength(0))-1; n_row++)
            {
                c_elementos[n_row, 1] = n_promedio.ToString("0.00");
                c_elementos[n_row, 2] = (Convert.ToDouble(c_elementos[n_row, 0]) - Convert.ToDouble(c_elementos[n_row, 1])).ToString("0.00");
                c_elementos[n_row, 3] = (Convert.ToDouble(c_elementos[n_row, 2]) * Convert.ToDouble(c_elementos[n_row, 2])).ToString("0.00");
                n_totalraiz = n_totalraiz + Convert.ToDouble(c_elementos[n_row, 3]);
            }
            n_varianza = (n_totalraiz / (n_numele - 1));
            n_desviacion = Math.Sqrt(n_varianza);
            return n_desviacion;
        }
        void MostrarDatosItems()
        {
            int n_row = 0;
            int n_numanos = 0;
            double n_valor;
            DataTable dtResult = new DataTable();
            CN_ges_planventas o_plan = new CN_ges_planventas();

            dtResult = funDatos.DataTableFiltrar(dtItems, "n_id = " + n_IdItem + "");
            if (dtResult.Rows.Count != 0)
            {
                TxtCodPro.Text = dtResult.Rows[0]["c_codpro"].ToString();
                TxtDes.Text = dtResult.Rows[0]["c_despro"].ToString();
                TxtUniMed.Text = dtResult.Rows[0]["c_abrpre"].ToString();
            }

            o_plan.mysConec = mysConec;
            o_plan.VentasItemxPorAnos(STU_SISTEMA.EMPRESAID, n_IdItem);
            dtResult = o_plan.dtLista;

            FgItems.Rows.Count = 2;

            if (dtResult != null)
            {
                if (dtResult.Rows.Count != 0)
                {
                    funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtResult, 2, true);
                    FgItems.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;

                    n_numanos = dtResult.Rows.Count;
                    // CALCULAMOS EL TOTAL
                    double n_totalmes = 0;
                    FgItems.Rows.Count = FgItems.Rows.Count + 1;
                    FgItems.SetData(FgItems.Rows.Count - 1, 1, "TOTAL ==>");
                    n_totalmes = funFlex.FlexSumarCol(FgItems, 2, 2, FgItems.Rows.Count - 1);
                    FgItems.SetData(FgItems.Rows.Count - 1, 2, n_totalmes.ToString("0.00"));
                    n_totalmes = funFlex.FlexSumarCol(FgItems, 3, 2, FgItems.Rows.Count - 1);
                    FgItems.SetData(FgItems.Rows.Count - 1, 3, n_totalmes.ToString("0.00"));
                    n_totalmes = funFlex.FlexSumarCol(FgItems, 4, 2, FgItems.Rows.Count - 1);
                    FgItems.SetData(FgItems.Rows.Count - 1, 4, n_totalmes.ToString("0.00"));
                    n_totalmes = funFlex.FlexSumarCol(FgItems, 5, 2, FgItems.Rows.Count - 1);
                    FgItems.SetData(FgItems.Rows.Count - 1, 5, n_totalmes.ToString("0.00"));
                    n_totalmes = funFlex.FlexSumarCol(FgItems, 6, 2, FgItems.Rows.Count - 1);
                    FgItems.SetData(FgItems.Rows.Count - 1, 6, n_totalmes.ToString("0.00"));
                    n_totalmes = funFlex.FlexSumarCol(FgItems, 7, 2, FgItems.Rows.Count - 1);
                    FgItems.SetData(FgItems.Rows.Count - 1, 7, n_totalmes.ToString("0.00"));
                    n_totalmes = funFlex.FlexSumarCol(FgItems, 8, 2, FgItems.Rows.Count - 1);
                    FgItems.SetData(FgItems.Rows.Count - 1, 8, n_totalmes.ToString("0.00"));
                    n_totalmes = funFlex.FlexSumarCol(FgItems, 9, 2, FgItems.Rows.Count - 1);
                    FgItems.SetData(FgItems.Rows.Count - 1, 9, n_totalmes.ToString("0.00"));
                    n_totalmes = funFlex.FlexSumarCol(FgItems, 10, 2, FgItems.Rows.Count - 1);
                    FgItems.SetData(FgItems.Rows.Count - 1, 10, n_totalmes.ToString("0.00"));
                    n_totalmes = funFlex.FlexSumarCol(FgItems, 11, 2, FgItems.Rows.Count - 1);
                    FgItems.SetData(FgItems.Rows.Count - 1, 11, n_totalmes.ToString("0.00"));
                    n_totalmes = funFlex.FlexSumarCol(FgItems, 12, 2, FgItems.Rows.Count - 1);
                    FgItems.SetData(FgItems.Rows.Count - 1, 12, n_totalmes.ToString("0.00"));
                    n_totalmes = funFlex.FlexSumarCol(FgItems, 13, 2, FgItems.Rows.Count - 1);
                    FgItems.SetData(FgItems.Rows.Count - 1, 13, n_totalmes.ToString("0.00"));
                    n_totalmes = funFlex.FlexSumarCol(FgItems, 14, 2, FgItems.Rows.Count - 1);
                    FgItems.SetData(FgItems.Rows.Count - 1, 14, n_totalmes.ToString("0.00"));

                    //CALCULAMOS EL PROMEDIO
                    FgItems.Rows.Count = FgItems.Rows.Count + 1;
                    FgItems.SetData(FgItems.Rows.Count - 1, 1, "PROMEDIO ==>");
                    double n_total = 0;
                    Double n_promedio = 0;

                    n_total = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 2, 2));
                    n_promedio = (n_total / n_numanos);
                    FgItems.SetData(FgItems.Rows.Count - 1, 2, n_promedio.ToString("0.00"));

                    n_total = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 2, 3));
                    n_promedio = (n_total / n_numanos);
                    FgItems.SetData(FgItems.Rows.Count - 1, 3, n_promedio.ToString("0.00"));

                    n_total = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 2, 4));
                    n_promedio = (n_total / n_numanos);
                    FgItems.SetData(FgItems.Rows.Count - 1, 4, n_promedio.ToString("0.00"));

                    n_total = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 2, 5));
                    n_promedio = (n_total / n_numanos);
                    FgItems.SetData(FgItems.Rows.Count - 1, 5, n_promedio.ToString("0.00"));

                    n_total = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 2, 6));
                    n_promedio = (n_total / n_numanos);
                    FgItems.SetData(FgItems.Rows.Count - 1, 6, n_promedio.ToString("0.00"));

                    n_total = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 2, 7));
                    n_promedio = (n_total / n_numanos);
                    FgItems.SetData(FgItems.Rows.Count - 1, 7, n_promedio.ToString("0.00"));

                    n_total = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 2, 8));
                    n_promedio = (n_total / n_numanos);
                    FgItems.SetData(FgItems.Rows.Count - 1, 8, n_promedio.ToString("0.00"));

                    n_total = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 2, 9));
                    n_promedio = (n_total / n_numanos);
                    FgItems.SetData(FgItems.Rows.Count - 1, 9, n_promedio.ToString("0.00"));

                    n_total = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 2, 10));
                    n_promedio = (n_total / n_numanos);
                    FgItems.SetData(FgItems.Rows.Count - 1, 10, n_promedio.ToString("0.00"));

                    n_total = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 2, 11));
                    n_promedio = (n_total / n_numanos);
                    FgItems.SetData(FgItems.Rows.Count - 1, 11, n_promedio.ToString("0.00"));

                    n_total = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 2, 12));
                    n_promedio = (n_total / n_numanos);
                    FgItems.SetData(FgItems.Rows.Count - 1, 12, n_promedio.ToString("0.00"));

                    n_total = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 2, 13));
                    n_promedio = (n_total / n_numanos);
                    FgItems.SetData(FgItems.Rows.Count - 1, 13, n_promedio.ToString("0.00"));

                    n_total = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 2, 14));
                    n_promedio = (n_total / n_numanos);
                    FgItems.SetData(FgItems.Rows.Count - 1, 14, n_promedio.ToString("0.00"));
                }
            }

            string[] c_datos = new string[12];
            int n_fil = 0;
            int n_ele = 0;
            string c_dato = "";
            n_row = 0;

            for (n_row = 2; n_row <= FgItems.Rows.Count - 3; n_row++)
            {
                n_ele = 0;
                for (n_fil = 2; n_fil <= FgItems.Cols.Count - 4; n_fil++)
                {
                    c_datos[n_ele] = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(n_row, n_fil))).ToString("0.00");
                    n_ele = n_ele + 1;
                }
                c_dato = HallarPromedio(c_datos).ToString("0.00");

                FgItems.SetData(n_row, FgItems.Cols.Count - 1, c_dato);
            }

            funFlex.Flex_PintarCeldas(FgItems, FgItems.Rows.Count - 2, 1, FgItems.Rows.Count - 1, 14, Color.Black, Color.Coral);
            funFlex.Flex_PintarCeldas(FgItems, 2, FgItems.Cols.Count - 2, FgItems.Rows.Count - 1, FgItems.Cols.Count - 1, Color.Black, Color.LightCoral);
            MostrarPlanVenta();
        }
        void MostrarPlanVenta()
        {
            int n_row = 0;

            for (n_row = 0; n_row <= 12; n_row++)
            {
                if (a_Datos[n_row, 0] == "Enero") { FgDato.SetData(2, 2, a_Datos[n_row, 1]); }
                if (a_Datos[n_row, 0] == "Febrero") { FgDato.SetData(2, 3, a_Datos[n_row, 1]); }
                if (a_Datos[n_row, 0] == "Marzo") { FgDato.SetData(2, 4, a_Datos[n_row, 1]); }
                if (a_Datos[n_row, 0] == "Abril") { FgDato.SetData(2, 5, a_Datos[n_row, 1]); }
                if (a_Datos[n_row, 0] == "Mayo") { FgDato.SetData(2, 6, a_Datos[n_row, 1]); }
                if (a_Datos[n_row, 0] == "Junio") { FgDato.SetData(2, 7, a_Datos[n_row, 1]); }
                if (a_Datos[n_row, 0] == "Julio") { FgDato.SetData(2, 8, a_Datos[n_row, 1]); }
                if (a_Datos[n_row, 0] == "Agosto") { FgDato.SetData(2, 9, a_Datos[n_row, 1]); }
                if (a_Datos[n_row, 0] == "Setiembre") { FgDato.SetData(2, 10, a_Datos[n_row, 1]); }
                if (a_Datos[n_row, 0] == "Octubre") { FgDato.SetData(2, 11, a_Datos[n_row, 1]); }
                if (a_Datos[n_row, 0] == "Noviembre") { FgDato.SetData(2, 12, a_Datos[n_row, 1]); }
                if (a_Datos[n_row, 0] == "Diciembre") { FgDato.SetData(2, 13, a_Datos[n_row, 1]); }
                if (a_Datos[n_row, 0] == "Total") { FgDato.SetData(2, 14, a_Datos[n_row, 1]); }
            }
        }
        private void CmdSalir_Click(object sender, EventArgs e)
        {
            b_cargado = false;
            this.Close();
        }
        private void FrmManPlanVentasHistorico_Load(object sender, EventArgs e)
        {
            this.Text = "GESTION - Historico de Ventas";
            this.Width = 1042;
            this.Height = 385;
        }
        private void FgItems_Click(object sender, EventArgs e)
        {

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void CmdDelItem_Click(object sender, EventArgs e)
        {
            // AGREGAMOS EL PROMEDIO + EL PORCENTAJE ASIGNADO
            string c_dato = "";
            double n_valor = 0;
            double n_porcen = Convert.ToDouble(funFunciones.NulosN(TxtPor.Text));
            n_porcen = ((n_porcen / 100) + 1);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count-1, 2));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 2, c_dato);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 1, 3));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 3, c_dato);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 1, 4));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 4, c_dato);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 1, 5));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 5, c_dato);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 1, 6));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 6, c_dato);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 1, 7));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 7, c_dato);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 1, 8));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 8, c_dato);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 1, 9));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 9, c_dato);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 1, 10));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 10, c_dato);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 1, 11));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 11, c_dato);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 1, 12));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 12, c_dato);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 1, 13));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 13, c_dato);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 1, 14));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 14, c_dato);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // AGREGAMOS EL TOTAL + EL PORCENTAJE ASIGNADO
            string c_dato = "";
            double n_valor = 0;
            double n_porcen = Convert.ToDouble(funFunciones.NulosN(TxtPor.Text));
            n_porcen = ((n_porcen / 100) + 1);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 2, 2));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 2, c_dato);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 2, 3));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 3, c_dato);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 2, 4));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 4, c_dato);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 2, 5));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 5, c_dato);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 2, 6));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 6, c_dato);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 2, 7));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 7, c_dato);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 2, 8));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 8, c_dato);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 2, 9));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 9, c_dato);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 2, 10));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 10, c_dato);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 2, 11));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 11, c_dato);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 2, 12));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 12, c_dato);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 2, 13));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 13, c_dato);

            n_valor = Convert.ToDouble(FgItems.GetData(FgItems.Rows.Count - 2, 14));
            c_dato = (n_valor * n_porcen).ToString("0.00");
            FgDato.SetData(2, 14, c_dato);
        }
        private void TxtPor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void TxtCodPro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtUniMed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtDes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtPor_Validated(object sender, EventArgs e)
        {
            if (funFunciones.NulosC(TxtPor.Text) == "") { return; }

            TxtPor.Text = Convert.ToDouble(TxtPor.Text).ToString("0.00");
        }
        private void CmdAddPla_Click(object sender, EventArgs e)
        {
            if (FgDato.Rows.Count == 2) { return; }

            a_Datos[0, 0] = "Enero"; a_Datos[0, 1] = funFunciones.NulosC(FgDato.GetData(2,2));
            a_Datos[1, 0] = "Febrero"; a_Datos[1, 1] = funFunciones.NulosC(FgDato.GetData(2, 3));
            a_Datos[2, 0] = "Marzo"; a_Datos[2, 1] =funFunciones.NulosC( FgDato.GetData(2, 4));
            a_Datos[3, 0] = "Abril"; a_Datos[3, 1] =funFunciones.NulosC( FgDato.GetData(2, 5));
            a_Datos[4, 0] = "Mayo"; a_Datos[4, 1] = funFunciones.NulosC(FgDato.GetData(2, 6));
            a_Datos[5, 0] = "Junio"; a_Datos[5, 1] = funFunciones.NulosC(FgDato.GetData(2, 7));
            a_Datos[6, 0] = "Julio"; a_Datos[6, 1] = funFunciones.NulosC(FgDato.GetData(2, 8));
            a_Datos[7, 0] = "Agosto"; a_Datos[7, 1] = funFunciones.NulosC(FgDato.GetData(2, 9));
            a_Datos[8, 0] = "Setiembre"; a_Datos[8, 1] = funFunciones.NulosC(FgDato.GetData(2, 10));
            a_Datos[9, 0] = "Octubre"; a_Datos[9, 1] = funFunciones.NulosC(FgDato.GetData(2, 11));
            a_Datos[10, 0] = "Noviembre"; a_Datos[10, 1] = funFunciones.NulosC(FgDato.GetData(2, 12));
            a_Datos[11, 0] = "Diciembre"; a_Datos[11, 1] = funFunciones.NulosC(FgDato.GetData(2, 13));
            a_Datos[12, 0] = "Total"; a_Datos[12, 1] = funFunciones.NulosC(FgDato.GetData(2, 14));

            b_cargado = true;
            this.Hide();
        }
        private void FgDato_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (b_cargado == true) { return; }
            if ((FgDato.Col >= 2) && (FgDato.Col <= 16))
            {
                double n_valor = funFlex.FlexSumarRow(FgDato, 2, 2, 13);
                FgDato.SetData(2, 14, n_valor.ToString("0.00"));
            }

            FgDato.Select(e.Row - 1, e.Col+1);
        }
        private void CmdAddDevStd_Click(object sender, EventArgs e)
        {
            // AGREGAMOS EL PROMEDIO + EL PORCENTAJE ASIGNADO
            string c_dato = "";
            double n_valor = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(FgItems.Row,16)));
            c_dato = n_valor.ToString("0.00");

            FgDato.SetData(2, 2, c_dato);
            FgDato.SetData(2, 3, c_dato);
            FgDato.SetData(2, 4, c_dato);
            FgDato.SetData(2, 5, c_dato);
            FgDato.SetData(2, 6, c_dato);
            FgDato.SetData(2, 7, c_dato);
            FgDato.SetData(2, 8, c_dato);
            FgDato.SetData(2, 9, c_dato);
            FgDato.SetData(2, 10, c_dato);
            FgDato.SetData(2, 11, c_dato);
            FgDato.SetData(2, 12, c_dato);
            FgDato.SetData(2, 13, c_dato);

            c_dato = (n_valor * 12).ToString("0.00");
            FgDato.SetData(2, 14, c_dato);
        }
        private void CmdAddProMen_Click(object sender, EventArgs e)
        {
            // AGREGAMOS EL PROMEDIO + EL PORCENTAJE ASIGNADO
            string c_dato = "";
            double n_valor = Convert.ToDouble(funFunciones.NulosN(FgItems.GetData(FgItems.Row, 15)));
            c_dato = n_valor.ToString("0.00");
            FgDato.SetData(2, 2, c_dato);
            FgDato.SetData(2, 3, c_dato);
            FgDato.SetData(2, 4, c_dato);
            FgDato.SetData(2, 5, c_dato);
            FgDato.SetData(2, 6, c_dato);
            FgDato.SetData(2, 7, c_dato);
            FgDato.SetData(2, 8, c_dato);
            FgDato.SetData(2, 9, c_dato);
            FgDato.SetData(2, 10, c_dato);
            FgDato.SetData(2, 11, c_dato);
            FgDato.SetData(2, 12, c_dato);
            FgDato.SetData(2, 13, c_dato);
            //FgDato.SetData(2, 14, c_dato);

            c_dato = (n_valor * 12).ToString("0.00");
            FgDato.SetData(2, 14, c_dato);
        }
        private void FgDato_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            if (b_cargado == true) { return; }
            
            if (!strNumerovalidos.Contains(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
