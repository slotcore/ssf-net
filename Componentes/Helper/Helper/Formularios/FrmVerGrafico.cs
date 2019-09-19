using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Helper.Formularios
{
    public partial class FrmVerGrafico : Form
    {
        public string[] c_TitulosY = {};
        public double[,] n_ValoresX = {};
        public string[,] c_Series = {};
        public string c_TituloGrafico;
        public string c_PieGrafico;
        public bool b_Legenda;
        public bool b_Cabecera;
        public bool b_Pie;
        public FrmVerGrafico()
        {
            InitializeComponent();
        }

        private void FrmVerGrafico_Load(object sender, EventArgs e)
        {
            int n_numelemento = 0;
            int n_numcolumnas = 0;
            int n_fila = 0;
            int n_col = 0;
            int n_fila2 = 0;
            double[] n_newvaloresX = {};
            C1.Win.C1Chart.ChartDataSeries ds1 = new C1.Win.C1Chart.ChartDataSeries();
                        
            n_numelemento = Convert.ToInt32(c_Series.GetLongLength(0));                        // OBTENEMOS EL NUMERO DE ELEMENTOS DE LA SERIE
            n_numcolumnas = Convert.ToInt32(n_ValoresX.Length);                                // OBTENEMOS EL NUMERO DE COLUMNAS 

            Array.Resize(ref n_newvaloresX, n_numcolumnas);
            // LIMPIAMOS CUALQUIER INFORMACION PREVIA EN EL CONTROL
            Graph1.ChartGroups[0].ChartData.SeriesList.Clear();

            // Add Data 
            //string[] ProductNames = { "ENE", "FEB", "MAR", "ABR", "MAY", "JUN", "JUL", "AGO", "SET", "OCT", "NOV", "DIC" };
            //int[] PriceX = { 80, 400, 20, 60, 150, 300, 130, 500, 250, 300, 550, 50 };
            //int[] Price2X = { 90, 300, 30, 50, 160, 290, 140, 490, 260, 290, 560, 40 };

            // AÑADIMOS LOS DATOS
            for (n_fila = 0; n_fila <= n_numelemento-1; n_fila++)
            {
                ds1 = Graph1.ChartGroups[0].ChartData.SeriesList.AddNewSeries();
                //ds1.Label = c_Series[n_fila].ToString();
                ds1.X.CopyDataIn(c_TitulosY);

                n_fila2 = 0;
                for (n_col = 0; n_col <= n_numcolumnas-1; n_col++)
                {
                    n_newvaloresX[n_fila2] = n_ValoresX[n_fila, n_col];
                    n_fila2 = n_fila2 + 1;
                }
                ds1.Y.CopyDataIn(n_newvaloresX);
            }

            //c1Chart1.ChartGroups[0].ChartData[0].LineStyle.Color = Color.Red;
            //c1Chart1.ChartGroups[0].ChartData[1].LineStyle.Color = Color.Tan;

            // Set chart type 
            Graph1.ChartArea.Inverted = false;
            //Graph1.ChartGroups[0].ChartType = C1.Win.C1Chart.Chart2DTypeEnum.Bar;
            Graph1.ChartGroups[0].ChartType = C1.Win.C1Chart.Chart2DTypeEnum.Bar;

            // Set axes titles
            Graph1.ChartArea.AxisX.Text = "MESES";
            Graph1.ChartArea.AxisY.Text = "KILOGRAMOS";

            Graph1.Legend.Visible = b_Legenda;
            Graph1.Header.Visible = b_Cabecera;
            Graph1.Footer.Visible = b_Pie;
            Graph1.Header.Text = c_TituloGrafico ;
            Graph1.Footer.Text = c_PieGrafico;
            //Set format for the Y-axis annotation
            Graph1.ChartArea.AxisY.AnnoFormat = C1.Win.C1Chart.FormatEnum.NumericCurrency;           
        }

        private void Graph1_Load(object sender, EventArgs e)
        {

        }


    }
}
