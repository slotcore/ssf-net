using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Ventas;
using SIAC_Objetos.Sistema;
using SIAC_Negocio.Maestros;
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

namespace SSF_NET_Ventas.Formularios
{
    public partial class FrmGuiasAnuales : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public int n_Libro;
        public int n_Origen;                                                    // INDICA EL ORIGEN DE LA GUIA     1 = VENTAS ;   2 = TRASLADO ENTRE ALMACENES
        
        DataTable dtTipExi = new DataTable();
        DataTable dtProv = new DataTable();
        DataTable dtItem = new DataTable();
        DataTable dtLista = new DataTable();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        Cls_Controles funControl = new Cls_Controles();
        CN_mae_clipro objPro = new CN_mae_clipro();

        string[,] arrCabecera1 = new string[16, 5];
        string[,] arrCabecera2 = new string[17, 5];
        string[,] arrCabeceraFlex1 = new string[2, 5];
        public FrmGuiasAnuales()
        {
            InitializeComponent();
        }

        private void FrmGuiasAnuales_Load(object sender, EventArgs e)
        {
            ConfigurarFormulario();
            CargarDT();
        }
        void CargarDT()
        {
            objPro.mysConec = mysConec;
            dtProv = objPro.ListarCliente(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);
        }
        void ConfigurarFormulario()
        {
            this.Height = 600;
            this.Width = 1094;

            Sz1.Left = 0;
            Sz1.Top = 41;
            Sz1.Height = this.Height - 83;
            Sz1.Width = this.Width - 18;

            this.Text = "VENTAS - GUIAS ANUALES";

            OptCan.Checked = true;

            FgDatos.Cols.Count = 5;
            Cabecera1();
            funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 2, false);

            arrCabeceraFlex1[0, 0] = "Cliente";
            arrCabeceraFlex1[0, 1] = "420";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_tipexides";

            arrCabeceraFlex1[1, 0] = "Id";
            arrCabeceraFlex1[1, 1] = "0";
            arrCabeceraFlex1[1, 2] = "N";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_itedes";

            funFlex.FlexMostrarDatos(FgCli, arrCabeceraFlex1, dtProv, 1, false);
            TxtFchIni.Text = "01/01/" + STU_SISTEMA.ANOTRABAJO.ToString();
            FgCli.Cols[1].ComboList = "...";
            FgCli.AllowEditing = true;
        }
        void Cabecera1()
        {
            arrCabecera1[0, 0] = "Codigo";
            arrCabecera1[0, 1] = "100";
            arrCabecera1[0, 2] = "C";
            arrCabecera1[0, 3] = "";
            arrCabecera1[0, 4] = "c_codpro";

            arrCabecera1[1, 0] = "Descripcion del Item";
            arrCabecera1[1, 1] = "300";
            arrCabecera1[1, 2] = "C";
            arrCabecera1[1, 3] = "";
            arrCabecera1[1, 4] = "c_despro";

            arrCabecera1[2, 0] = "Uni. Med.";
            arrCabecera1[2, 1] = "40";
            arrCabecera1[2, 2] = "C";
            arrCabecera1[2, 3] = "";
            arrCabecera1[2, 4] = "c_abrpre";

            arrCabecera1[3, 0] = "Enero";
            arrCabecera1[3, 1] = "70";
            arrCabecera1[3, 2] = "D";
            arrCabecera1[3, 3] = "";
            arrCabecera1[3, 4] = "January";

            arrCabecera1[4, 0] = "Febrero";
            arrCabecera1[4, 1] = "70";
            arrCabecera1[4, 2] = "D";
            arrCabecera1[4, 3] = "";
            arrCabecera1[4, 4] = "February";

            arrCabecera1[5, 0] = "Marzo";
            arrCabecera1[5, 1] = "70";
            arrCabecera1[5, 2] = "D";
            arrCabecera1[5, 3] = "";
            arrCabecera1[5, 4] = "March";

            arrCabecera1[6, 0] = "Abril";
            arrCabecera1[6, 1] = "70";
            arrCabecera1[6, 2] = "D";
            arrCabecera1[6, 3] = "";
            arrCabecera1[6, 4] = "April";

            arrCabecera1[7, 0] = "Mayo";
            arrCabecera1[7, 1] = "70";
            arrCabecera1[7, 2] = "D";
            arrCabecera1[7, 3] = "";
            arrCabecera1[7, 4] = "May";

            arrCabecera1[8, 0] = "Junio";
            arrCabecera1[8, 1] = "70";
            arrCabecera1[8, 2] = "D";
            arrCabecera1[8, 3] = "";
            arrCabecera1[8, 4] = "June";

            arrCabecera1[9, 0] = "Julio";
            arrCabecera1[9, 1] = "70";
            arrCabecera1[9, 2] = "D";
            arrCabecera1[9, 3] = "";
            arrCabecera1[9, 4] = "July";

            arrCabecera1[10, 0] = "Agosto";
            arrCabecera1[10, 1] = "70";
            arrCabecera1[10, 2] = "D";
            arrCabecera1[10, 3] = "";
            arrCabecera1[10, 4] = "August";

            arrCabecera1[11, 0] = "Setiembre";
            arrCabecera1[11, 1] = "70";
            arrCabecera1[11, 2] = "D";
            arrCabecera1[11, 3] = "";
            arrCabecera1[11, 4] = "September";

            arrCabecera1[12, 0] = "Octubre";
            arrCabecera1[12, 1] = "70";
            arrCabecera1[12, 2] = "D";
            arrCabecera1[12, 3] = "";
            arrCabecera1[12, 4] = "October";

            arrCabecera1[13, 0] = "Noviembre";
            arrCabecera1[13, 1] = "70";
            arrCabecera1[13, 2] = "D";
            arrCabecera1[13, 3] = "";
            arrCabecera1[13, 4] = "November";

            arrCabecera1[14, 0] = "Diciembre";
            arrCabecera1[14, 1] = "70";
            arrCabecera1[14, 2] = "D";
            arrCabecera1[14, 3] = "";
            arrCabecera1[14, 4] = "December";

            arrCabecera1[15, 0] = "Total";
            arrCabecera1[15, 1] = "70";
            arrCabecera1[15, 2] = "D";
            arrCabecera1[15, 3] = "";
            arrCabecera1[15, 4] = "n_total";
        }
        void Cabecera2()
        {
            arrCabecera2[0, 0] = "Cliente";
            arrCabecera2[0, 1] = "250";
            arrCabecera2[0, 2] = "C";
            arrCabecera2[0, 3] = "";
            arrCabecera2[0, 4] = "c_nombre";

            arrCabecera2[1, 0] = "Codigo";
            arrCabecera2[1, 1] = "100";
            arrCabecera2[1, 2] = "C";
            arrCabecera2[1, 3] = "";
            arrCabecera2[1, 4] = "c_codpro";

            arrCabecera2[2, 0] = "Descripcion del Item";
            arrCabecera2[2, 1] = "300";
            arrCabecera2[2, 2] = "C";
            arrCabecera2[2, 3] = "";
            arrCabecera2[2, 4] = "c_despro";

            arrCabecera2[3, 0] = "Uni. Med.";
            arrCabecera2[3, 1] = "40";
            arrCabecera2[3, 2] = "C";
            arrCabecera2[3, 3] = "";
            arrCabecera2[3, 4] = "c_abrpre";

            arrCabecera2[4, 0] = "Enero";
            arrCabecera2[4, 1] = "70";
            arrCabecera2[4, 2] = "D";
            arrCabecera2[4, 3] = "";
            arrCabecera2[4, 4] = "January";

            arrCabecera2[5, 0] = "Febrero";
            arrCabecera2[5, 1] = "70";
            arrCabecera2[5, 2] = "D";
            arrCabecera2[5, 3] = "";
            arrCabecera2[5, 4] = "February";

            arrCabecera2[6, 0] = "Marzo";
            arrCabecera2[6, 1] = "70";
            arrCabecera2[6, 2] = "D";
            arrCabecera2[6, 3] = "";
            arrCabecera2[6, 4] = "March";

            arrCabecera2[7, 0] = "Abril";
            arrCabecera2[7, 1] = "70";
            arrCabecera2[7, 2] = "D";
            arrCabecera2[7, 3] = "";
            arrCabecera2[7, 4] = "April";

            arrCabecera2[8, 0] = "Mayo";
            arrCabecera2[8, 1] = "70";
            arrCabecera2[8, 2] = "D";
            arrCabecera2[8, 3] = "";
            arrCabecera2[8, 4] = "May";

            arrCabecera2[9, 0] = "Junio";
            arrCabecera2[9, 1] = "70";
            arrCabecera2[9, 2] = "D";
            arrCabecera2[9, 3] = "";
            arrCabecera2[9, 4] = "June";

            arrCabecera2[10, 0] = "Julio";
            arrCabecera2[10, 1] = "70";
            arrCabecera2[10, 2] = "D";
            arrCabecera2[10, 3] = "";
            arrCabecera2[10, 4] = "July";

            arrCabecera2[11, 0] = "Agosto";
            arrCabecera2[11, 1] = "70";
            arrCabecera2[11, 2] = "D";
            arrCabecera2[11, 3] = "";
            arrCabecera2[11, 4] = "August";

            arrCabecera2[12, 0] = "Setiembre";
            arrCabecera2[12, 1] = "70";
            arrCabecera2[12, 2] = "D";
            arrCabecera2[12, 3] = "";
            arrCabecera2[12, 4] = "September";

            arrCabecera2[13, 0] = "Octubre";
            arrCabecera2[13, 1] = "70";
            arrCabecera2[13, 2] = "D";
            arrCabecera2[13, 3] = "";
            arrCabecera2[13, 4] = "October";

            arrCabecera2[14, 0] = "Noviembre";
            arrCabecera2[14, 1] = "70";
            arrCabecera2[14, 2] = "D";
            arrCabecera2[14, 3] = "";
            arrCabecera2[15, 4] = "November";

            arrCabecera2[15, 0] = "Diciembre";
            arrCabecera2[15, 1] = "70";
            arrCabecera2[15, 2] = "D";
            arrCabecera2[15, 3] = "";
            arrCabecera2[15, 4] = "December";

            arrCabecera2[16, 0] = "Total";
            arrCabecera2[16, 1] = "70";
            arrCabecera2[16, 2] = "D";
            arrCabecera2[16, 3] = "";
            arrCabecera2[16, 4] = "n_total";
        }
        private void FRmVentasAnuales_Resize(object sender, EventArgs e)
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

            string c_CadINCli = funFlex.Flex_CadenaIN(FgCli, 2, 1);

            if (c_CadINCli == "") { Cabecera1(); }
            if (c_CadINCli != "") { Cabecera2(); }

            int m_tiprep = 0;
            CN_vta_guias o_ventas = new CN_vta_guias();
            o_ventas.mysConec = mysConec;
            o_ventas.STU_SISTEMA = STU_SISTEMA;

            if (OptImp.Checked == true) { m_tiprep = 1; }
            if (OptCan.Checked == true) { m_tiprep = 2; }

            o_ventas.GuiasAnuales(STU_SISTEMA.EMPRESAID, 2, Convert.ToDateTime(d_fchfin).Year, n_Origen, c_CadINCli);
            dtLista = o_ventas.dtLista;

            //funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 2, true);

            if (c_CadINCli == "") { funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 2, true); }
            if (c_CadINCli != "") { funFlex.FlexMostrarDatos(FgDatos, arrCabecera2, dtLista, 2, true); }
            MostrarTotales();
            MessageBox.Show("¡ Los datos se mostraron con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            FgDatos.Focus();
        }
        void MostrarTotales()
        {
            double n_valor = 0;

            FgDatos.Rows.Count = FgDatos.Rows.Count + 1;
            FgDatos.SetData(FgDatos.Rows.Count - 1, 2, "TOTALES ==>");

            n_valor = funFlex.FlexSumarCol(FgDatos, 4, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 4, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 5, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 5, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 6, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 6, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 7, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 7, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 8, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 8, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 9, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 9, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 10, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 10, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 11, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 11, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 12, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 12, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 13, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 13, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 14, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 14, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 15, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 15, n_valor.ToString("0.00"));

            funFlex.Flex_PintarCeldas(FgDatos, FgDatos.Rows.Count - 1, 1, FgDatos.Rows.Count - 1, FgDatos.Cols.Count - 1, Color.Black, Color.PaleGreen);
            funFlex.Flex_PintarCeldas(FgDatos, 1, FgDatos.Cols.Count - 1, FgDatos.Rows.Count - 1, FgDatos.Cols.Count - 1, Color.Black, Color.Pink);
        }
        private void ToolExportar_Click(object sender, EventArgs e)
        {
            string c_cad = "";
            string c_nomarch = STU_SISTEMA.EMPRESARUC + "-" + STU_SISTEMA.ANOTRABAJO.ToString() + "-" + STU_SISTEMA.MESTRABAJO.ToString() + "-GUIAS_ANUALES-" + ".xls";
            if (OptImp.Checked == true) { c_cad = "IMPORTES"; }
            if (OptCan.Checked == true) { c_cad = "CANTIDADES"; }
            funFlex.ExportToExcel(FgDatos, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "GUIAS ANUALES - CLIENTES", c_cad, c_nomarch);
        }

        private void FrmGuiasAnuales_Resize(object sender, EventArgs e)
        {
            Sz1.Left = 0;
            Sz1.Top = 41;
            Sz1.Height = this.Height - 83;
            Sz1.Width = this.Width - 18;
        }

        private void CmdAddPro_Click(object sender, EventArgs e)
        {
            if (funFunciones.NulosC(FgCli.GetData(FgCli.Rows.Count - 1, 1)) == "")
            {
                MessageBox.Show("¡ No ha especificado un proveedor en la ultima fila !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                FgCli.Focus();
                return;
            }
            FgCli.Rows.Count = FgCli.Rows.Count + 1;
        }

        private void CmdDelPro_Click(object sender, EventArgs e)
        {
            if (FgCli.Rows.Count == 1) { return; }
            FgCli.RemoveItem(FgCli.Row);
        }

        private void FgCli_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (FgCli.Col == 1)
            {
                DataTable dtResult = new DataTable();
                string c_dato = "";
                objPro.mysConec = mysConec;
                dtResult = objPro.BuscarCliPro(dtProv, 1, "n_id", "");
                if (dtResult != null)
                {
                    if (dtResult.Rows.Count != 0)
                    {
                        c_dato = dtResult.Rows[0]["c_nombre"].ToString();
                        FgCli.SetData(FgCli.Row, 1, c_dato);

                        c_dato = dtResult.Rows[0]["n_id"].ToString();
                        FgCli.SetData(FgCli.Row, 2, c_dato);

                        if (funFunciones.NulosC(FgCli.GetData(FgCli.Rows.Count - 1, 1)) != "")
                        {
                            FgCli.Rows.Count = FgCli.Rows.Count + 1;
                        }
                    }
                }
            }
        }
    }
}
