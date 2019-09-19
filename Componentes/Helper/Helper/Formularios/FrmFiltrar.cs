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
    public partial class FrmFiltrar : Form
    {
        public string[,] arrCabeceraFlex;

        public DataTable dtConsulta = new DataTable();
        public DataTable dtResultado = new DataTable();
   
        public string c_titulo;
        public string c_campoorden;
        //public string Buscar_CampoBusqueda;
        public string c_campobusqueda;
        public int n_columnabusqueda;
        public int n_columnacheck;
        public string c_CadFiltro;
        public bool b_ConFiltro = true;

        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Genericas fungen = new Genericas();
        Comunes.Funciones funfun  = new Comunes.Funciones();

        int n_colseleccion = 0;
        bool b_agregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        string strCarFecha = "1234567890/" + (char)8;
        public FrmFiltrar()
        {
            InitializeComponent();
        }
        private void FrmFiltrar_Load(object sender, EventArgs e)
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
            c1Sizer1.Top = 0;
            c1Sizer1.Width = n_ancho + 35;
            c1Sizer1.Height = this.Height - 20;

            this.Width = n_ancho + 50;
            FgFiltro.Width = n_ancho;
            FgFiltro.Height = this.Height - 150;
            FgFix.Width = n_ancho;

            //ORDENAMOS EL DATATABLE 
            dtResul = fungen.DataTableOrdenar(dtConsulta, c_campoorden);
            dtConsulta = dtResul;
            dtResul = null;                                                             // LIMPIAMOS EL DATATABLE DE RESULTADOS, SE LIMPIA PORQUE ESTE DATATABLE SE PASA COMO REFERENCIA A LA CLASE QUE LLAMA AL FORMULARUIO DE BUSQUEDA
            b_agregando = true;

            funFlex.FlexMostrarDatos(FgFiltro, arrCabeceraFlex, dtConsulta, 0, true);
            funFlex.FlexMostrarDatos(FgFix, arrCabeceraFlex, dtConsulta, 2, false);

            FgFix.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;//  = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            FgFix.AllowEditing = true;
            if (b_ConFiltro == true)
            {
                FgFix.Rows.Count = FgFix.Rows.Count + 1;
            }

            LblNumReg.Text = dtConsulta.Rows.Count.ToString();
            CmdEsc.Left = 2000;
            LblOrden.Text = MostrarCampoOrden(c_campoorden);
            b_agregando = false;
            this.CenterToScreen();
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
        private void Button2_Click(object sender, EventArgs e)
        {
            dtResultado = null;
            this.Hide();
        }
        private void CmdAceptar_Click(object sender, EventArgs e)
        {
            Aceptar();
        }
        void Aceptar()
        {
            int n_row = 0;
            DataTable dtResult = new DataTable();
            string strCondicionFiltro = "";
            string c_dato = "";
            string c_tipdat = "";
            Helper.Comunes.Funciones fun = new Helper.Comunes.Funciones();
            int n_NumeroElementos = Convert.ToInt32(arrCabeceraFlex.GetLongLength(0)) - 1;
            dtResult = dtConsulta.Clone();
            

            for (n_row = 0; n_row <= n_NumeroElementos; n_row++)
            {
                if (arrCabeceraFlex[n_row, 4] == c_campobusqueda)
                {
                    c_tipdat = arrCabeceraFlex[n_row, 2];
                }
            }

                for (n_row = 0; n_row <= FgFiltro.Rows.Count - 1; n_row++)
                {
                    if (fun.NulosC(FgFiltro.GetData(n_row, n_columnacheck)) == "True")
                    {
                        c_dato = FgFiltro.GetData(n_row, n_columnabusqueda).ToString();

                        if (c_tipdat == "N")
                        { 
                            strCondicionFiltro = "" + c_campobusqueda + " = " + c_dato + "";
                        }
                        if (c_tipdat == "C")
                        {
                            strCondicionFiltro = "" + c_campobusqueda + " = '" + c_dato + "'";
                        }

                        DataRow[] result = dtConsulta.Select(strCondicionFiltro);
                        foreach (DataRow row in result)
                        {
                            dtResult.ImportRow(row);
                        }
                    }
                }
            dtResultado = dtResult;
            this.Hide();
        }

        private void FgFix_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            DataTable dtResult = new DataTable();

            if (b_agregando == true) { return; }
            //if ((funfun.NulosC(FgFix.GetData(FgFix.Row, FgFix.Col)).ToString() == "") || (funfun.NulosN(FgFix.GetData(FgFix.Row, FgFix.Col)).ToString() == "0") )
            //{
            //    dtResult = dtConsulta;
            //    FgFix.SetData(FgFix.Row, FgFix.Col, null);
            //    funFlex.FlexMostrarDatos(FgFiltro, arrCabeceraFlex, dtResult, 0, true);
            //    return; 
            //}

            int n_col = FgFix.Col - 1;  // RESTAMOS 1 PARA IGUALARLO AL INDICE DEL ARRAY DEL FILTRO
            string c_cadenafiltro = "";
            
            string c_condicion = funfun.NulosC(FgFix.GetData(FgFix.Row, FgFix.Col));

            //if (arrCabeceraFlex[n_col, 2].ToString() == "C")
            //{
            //    c_cadenafiltro = arrCabeceraFlex[n_col, 4].ToString() + " LIKE '*" + c_condicion + "*'";
            //}

            //if ((arrCabeceraFlex[n_col, 2].ToString() == "N") || (arrCabeceraFlex[n_col, 2].ToString() == "D"))
            //{
            //    c_cadenafiltro = arrCabeceraFlex[n_col, 4].ToString() + " = " + c_condicion + "";
            //}

            string c_nomcol = "";
            int n_fil = 0;
            int n_numcon = 0;
            c_cadenafiltro = "";
            //Helper.Comunes.Funciones funFunciones = new Helper.Comunes.Funciones();

            for (n_col = 1; n_col <= FgFix.Cols.Count - 1; n_col++)
            {
                c_nomcol = FgFix.GetData(0, n_col).ToString();
                c_condicion = funfun.NulosC(FgFix.GetData(2, n_col));
                if (c_condicion != "")
                {
                    n_numcon = n_numcon + 1;

                    if (n_numcon > 1) { c_cadenafiltro = c_cadenafiltro + " AND"; }
                    for (n_fil = 1; n_fil <= FgFix.Cols.Count - 1; n_fil++)
                    { 
                        if (arrCabeceraFlex[n_fil-1, 0] == c_nomcol)
                        {
                            if (arrCabeceraFlex[n_fil - 1, 2].ToString() == "C")
                            {
                                c_cadenafiltro = c_cadenafiltro +" " + arrCabeceraFlex[n_fil - 1, 4].ToString() + " LIKE '*" + c_condicion + "*'";
                            }

                            if ((arrCabeceraFlex[n_fil - 1, 2].ToString() == "N") || (arrCabeceraFlex[n_fil, 2].ToString() == "D"))
                            {
                                c_cadenafiltro = c_cadenafiltro + " " + arrCabeceraFlex[n_col, 4].ToString() + " = " + c_condicion + "";
                            }
                            break;
                        }
                    }
                }
            }

            if (c_cadenafiltro == "")
            {
                dtResult = dtConsulta;
                FgFix.SetData(FgFix.Row, FgFix.Col, null);
                funFlex.FlexMostrarDatos(FgFiltro, arrCabeceraFlex, dtResult, 0, true);
            }
            else 
            { 
                dtResult = fungen.DataTableFiltrar(dtConsulta, c_cadenafiltro);
                funFlex.FlexMostrarDatos(FgFiltro, arrCabeceraFlex, dtResult, 0, true);
            }
        }

        private void FgFix_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            int n_col = FgFix.Col - 1;  // RESTAMOS 1 PARA IGUALARLO AL INDICE DEL ARRAY DEL FILTRO

            if ((arrCabeceraFlex[n_col, 2].ToString() == "N") || (arrCabeceraFlex[n_col, 2].ToString() == "D"))
            { 
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

            if (arrCabeceraFlex[n_col, 2].ToString() == "C") 
            {
                if (!strCaracteres.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            if (arrCabeceraFlex[n_col, 2].ToString() == "F")
            {
                if (!strCarFecha.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void FgFix_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete")
            {
                FgFix.SetData(FgFix.Row, FgFix.Col, null);
            }
        }
        private void FgFiltro_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode.ToString() != "Insert") && (e.KeyCode.ToString() != "Delete"))
            {
                return;
            }
            
            if (e.KeyCode.ToString() == "Insert")
            {
                FgFiltro.SetData(FgFiltro.Row, n_colseleccion, true);
            }

            if (e.KeyCode.ToString() == "Delete")
            {
                FgFiltro.SetData(FgFiltro.Row, n_colseleccion, false);
            }
        }
        private void FgFiltro_EnterCell(object sender, EventArgs e)
        {
            if (FgFiltro.Col == n_colseleccion)
            {
                FgFiltro.AllowEditing = true;
            }
            else
            { 
                FgFiltro.AllowEditing = false;
            }
        }

        private void Label18_Click(object sender, EventArgs e)
        {

        }

        private void ChkAll_CheckedChanged(object sender, EventArgs e)
        {
            int n_row =0;

            if (ChkAll.Checked == true)
            { 
                for (n_row=0;n_row<=FgFiltro.Rows.Count-1; n_row++)
                {
                    FgFiltro.SetData(n_row, FgFiltro.Cols.Count - 2, 1);
                }
            }
        }
    }
}
