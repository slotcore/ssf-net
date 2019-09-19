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
    public partial class FrmVerDatos : Form
    {
        public string[,] arrCabeceraFlex;
        public string[,] arrCabeceraFlexFix;

        public DataTable dtConsulta = new DataTable();
        public DataTable dtResultado = new DataTable();

        public string c_titulo;
        public int n_num_filas_cabecera;
        
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Genericas fungen = new Genericas();
        Comunes.Funciones funfun = new Comunes.Funciones();

        int n_colseleccion = 0;
        bool b_agregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        string strCarFecha = "1234567890/" + (char)8;

        public FrmVerDatos()
        {
            InitializeComponent();
        }

        private void FrmVerDatos_Load(object sender, EventArgs e)
        {
            // CONFIGURAMOS EL FORMULARIO
            DataTable dtResul = new DataTable();
            this.Text = c_titulo;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            int n_fila = 0;
            int n_ancho = 0;
            int n_NumeroElementos = Convert.ToInt32(arrCabeceraFlex.GetLongLength(0)) - 1;

            this.Height = 527;
            //this.Width = 517;

            for (n_fila = 0; n_fila <= n_NumeroElementos; n_fila++)
            {
                n_ancho = n_ancho + Convert.ToInt16(arrCabeceraFlex[n_fila, 1].ToString());

                if (arrCabeceraFlex[n_fila, 2].ToString() == "B")
                {
                    n_colseleccion = n_fila + 1;
                }
            }
            c1Sizer1.Left = 0;
            c1Sizer1.Top = 40;
            c1Sizer1.Width = n_ancho + 35;
            c1Sizer1.Height = this.Height - 78;

            this.Width = n_ancho + 50;
            FgFiltro.Width = n_ancho;
            FgFiltro.Height = this.Height - 150;

            ////ORDENAMOS EL DATATABLE 
            //dtResul = fungen.DataTableOrdenar(dtConsulta, c_campoorden);
            //dtConsulta = dtResul;
            //dtResul = null;                                                             // LIMPIAMOS EL DATATABLE DE RESULTADOS, SE LIMPIA PORQUE ESTE DATATABLE SE PASA COMO REFERENCIA A LA CLASE QUE LLAMA AL FORMULARUIO DE BUSQUEDA
            b_agregando = true;

            funFlex.FlexMostrarDatos(FgFiltro, arrCabeceraFlex, dtConsulta, n_num_filas_cabecera, true);
            SetearCabecera();
            LblNumReg.Text = dtConsulta.Rows.Count.ToString();
            CmdEsc.Left = 2000;
            //LblOrden.Text = MostrarCampoOrden(c_campoorden);
            b_agregando = false;
            this.CenterToScreen();
        }
        void SetearCabecera()
        {
            int n_numcol = Convert.ToInt32(arrCabeceraFlex.GetLongLength(0)) - 1;
            int n_col =0;
            string c_titu = "";

            //for (n_col = 1; n_col <= FgFiltro.Cols.Count-1; n_col++)
            //{
            //    c_titu = FgFiltro.GetData(0, n_col).ToString();
            //    funFlex.Flex_UnirFilas(FgFiltro, n_col, 0, n_num_filas_cabecera - 1, c_titu, FgFiltro.Cols[n_col].Width);
            //}

            if (arrCabeceraFlexFix == null) { return; }

            int n_fila = 0;
            int n_col1 = 0;
            int n_col2 = 0;
            int n_row = 0;

            int n_NumeroElementos = Convert.ToInt32(arrCabeceraFlexFix.GetLongLength(0)) - 1;
            for (n_row = 0; n_row <= n_NumeroElementos; n_row++)
            {
                n_fila = Convert.ToInt16(arrCabeceraFlexFix[n_row, 0]);
                n_col1 = Convert.ToInt16(arrCabeceraFlexFix[n_row, 1]);
                n_col2 = Convert.ToInt16(arrCabeceraFlexFix[n_row, 2]);
                c_titu = funfun.NulosC(arrCabeceraFlexFix[n_row, 3]).ToString();
                funFlex.Flex_FixUniColumnas(FgFiltro, n_fila, n_col1, n_col2, c_titu, 1);
            }
        }
        string MostrarCampoOrden(string c_camord)
        {
            int n_fila = 0;
            string c_resul = "";
            int n_NumeroElementos = Convert.ToInt32(arrCabeceraFlex.GetLongLength(0)) - 1;

            for (n_fila = 0; n_fila <= n_NumeroElementos; n_fila++)
            {
                if (c_camord == arrCabeceraFlex[n_fila, 4].ToString())
                {
                    c_resul = arrCabeceraFlex[n_fila, 0].ToString();
                    break;
                }
            }
            return c_resul;
        }
        private void ToolSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ToolHerramientas_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void FrmVerDatos_Resize(object sender, EventArgs e)
        {
            c1Sizer1.Left = 0;
            c1Sizer1.Top = 41;
            c1Sizer1.Height = this.Height - 83;
            c1Sizer1.Width = this.Width - 18;
        }

        private void ToolExportar_Click(object sender, EventArgs e)
        {
            Cls_FlexGrid funFlex = new Cls_FlexGrid();
            string c_nomarch = "VISTA0001.XLS";
            funFlex.ExportToExcel_NumFilaCabecera = 3;
            funFlex.ExportToExcel(FgFiltro, "", "", c_titulo, "", c_nomarch);
        }
    }
}
