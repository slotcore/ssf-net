﻿using System;
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
    public partial class FrmBusqueda : Form
    {
        public string[,] arrCabeceraDg1;
        public DataTable dtConsulta = new DataTable();
        public DataTable dtResul = new DataTable();
        //public int n_ColumnaBusqueda;
        public string c_titulo;
        public string c_campoorden;
        public string Buscar_CampoBusqueda;
        public string c_CadFiltro;

        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        Genericas fungen = new Genericas();
        public FrmBusqueda()
        {
            InitializeComponent();
        }
        private void FrmBusqueda_Load(object sender, EventArgs e)
        {
            // CONFIGURAMOS EL FORMULARIO
            this.Text = c_titulo;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            int n_fila = 0;
            int n_ancho = 0;
            int n_NumeroElementos = Convert.ToInt32(arrCabeceraDg1.GetLongLength(0)) - 1;
            for (n_fila = 0; n_fila <= n_NumeroElementos; n_fila++)
            {
                n_ancho = n_ancho + Convert.ToInt32(arrCabeceraDg1[n_fila, 1].ToString());
            }
            this.Width = n_ancho + 67;
            C1Sizer1.Width = this.Width - 20;
            C1Sizer1.Height = this.Height - 40;

            //ORDENAMOS EL DATATABLE 
            dtResul = fungen.DataTableOrdenar(dtConsulta, c_campoorden);
            dtConsulta = dtResul;
            dtResul = null;                                                             // LIMPIAMOS EL DATATABLE DE RESULTADOS, SE LIMPIA PORQUE ESTE DATATABLE SE PASA COMO REFERENCIA A LA CLASE QUE LLAMA AL FORMULARUIO DE BUSQUEDA
            funDbGrid.DG_FormatearGrid(DgLista, arrCabeceraDg1, dtConsulta, true);
            LblNumReg.Text = dtConsulta.Rows.Count.ToString();
            CmdEsc.Left = 2000;
            LblOrden.Text = MostrarCampoOrden(c_campoorden);
            if (c_CadFiltro != "") 
            {
                DgLista.Columns[2].FilterText = c_CadFiltro;
                FiltrarCondicion();
            }
            else
            {
                //DgLista.Columns[2].FilterText = "";
            }
            this.CenterToScreen();
            //DgLista.Select();
            
            DgLista.SetActiveCell(0,1);            //DgLista.Refresh();
            DgLista.Focus();
        }
        string MostrarCampoOrden(string c_camord)
        {
            int n_fila = 0;
            string c_resul = "";
            int n_NumeroElementos = Convert.ToInt32(arrCabeceraDg1.GetLongLength(0)) - 1;

            for (n_fila = 0; n_fila <= n_NumeroElementos; n_fila++)
            {
                if (c_camord == arrCabeceraDg1[n_fila, 3].ToString())
                {
                    c_resul = arrCabeceraDg1[n_fila, 0].ToString();
                    break;
                }
            }
            return c_resul;
        }
        private void CmdAceptar_Click(object sender, EventArgs e)
        {
            Aceptar();
        }
        void Aceptar()
        {
            DataTable DtResultado = new DataTable();
            Genericas xFun = new Genericas();
            string c_criterio = "";
            //string c_campobusqueda = "";
            int n_Fila;
            int IntNumeroElementos = Convert.ToInt32(arrCabeceraDg1.GetLongLength(0)) - 1;

            for (n_Fila = 0; n_Fila <= IntNumeroElementos; n_Fila++)
            {
                if (Buscar_CampoBusqueda == arrCabeceraDg1[n_Fila, 3])
                {
                    if (arrCabeceraDg1[n_Fila, 2] == "N")
                    {
                        int intIdRegistro = Convert.ToInt32(DgLista.Columns[Buscar_CampoBusqueda].CellValue(DgLista.Row).ToString());
                        c_criterio = "(" + Buscar_CampoBusqueda.Trim() + " = " + intIdRegistro.ToString() + ")";
                    }
                    if (arrCabeceraDg1[n_Fila, 2] == "C")
                    {
                        string c_IdRegistro = DgLista.Columns[Buscar_CampoBusqueda].CellValue(DgLista.Row).ToString();
                        c_criterio = "(" + Buscar_CampoBusqueda.Trim() + " = '" + c_IdRegistro.ToString() + "')";
                    }
                    break;
                }
                //c_campobusqueda = arrCabeceraDg1[n_Fila, 3];
            }
            if (c_criterio != "")
            {
                DtResultado = xFun.DataTableFiltrar(dtConsulta, c_criterio);
            }
            dtResul = DtResultado;
            this.Hide();
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            dtResul = null;
            this.Hide();
        }
        private void DgLista_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (DgLista.EditActive == true)
                {
                    FiltrarCondicion();
                }
                else
                { 
                    CmdAceptar_Click(sender, e);
                }
            }
        }
        void FiltrarCondicion()
        {
            string c_CadFiltro = funDbGrid.DG_LeerCondicionesFiltro(DgLista);

            dtResul = funDbGrid.DG_Filtrar(dtConsulta, c_CadFiltro, DgLista);
            DgLista.DataSource = dtResul;
            LblNumReg.Text = (dtResul.Rows.Count).ToString();
        }
        private void DgLista_HeadClick(object sender, C1.Win.C1TrueDBGrid.ColEventArgs e)
        {
            string c_nomcampo = e.Column.Name;
            LblOrden.Text = c_nomcampo;
        }
        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            Aceptar();
        }
        private void FrmBusqueda_Activated(object sender, EventArgs e)
        {
            DgLista.SelectedRows.Clear();
        }
        private void DgLista_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode.ToString() == "13")
            //{
            //    FiltrarCondicion();
            //}
        }
    }
}
