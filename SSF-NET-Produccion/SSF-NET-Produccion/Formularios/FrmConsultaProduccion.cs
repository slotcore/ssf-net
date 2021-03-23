using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Maestros;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Almacen;
using SIAC_Negocio.Produccion;
using SIAC_Objetos.Sistema;
using SIAC_Negocio.Sunat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace SSF_NET_Produccion.Formularios
{
    public partial class FrmConsultaProduccion : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public int n_Libro;

        DataTable dtPro = new DataTable();
        DataTable dtIns = new DataTable();
        DataTable dtLista = new DataTable();

        CN_alm_inventario objIte = new CN_alm_inventario();
        //CN_sun_tipexi o_tipexi = new CN_sun_tipexi();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        Cls_Controles funControl = new Cls_Controles();
        //CN_mae_clipro objPro = new CN_mae_clipro();
        CN_alm_inventario objItems = new CN_alm_inventario();

        string[,] arrCabeceraFlex1 = new string[2, 5];
        string[,] arrCabeceraFlex2 = new string[2, 5];
        string[,] arrCabecera1 = new string[12, 5];
        string[,] arrCabecera2 = new string[12, 5];
        //string[,] arrCabecera3 = new string[8, 5];

        public FrmConsultaProduccion()
        {
            InitializeComponent();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void TxtFchFin_ValueChanged(object sender, EventArgs e)
        {

        }
        private void ToolBuscar_Click(object sender, EventArgs e)
        {
            EjecutarConsulta();
        }
        void EjecutarConsulta()
        {
            int n_tiprep = 0;
            int n_tipgru = 0;

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

            if (OptTip1.Checked == true) { n_tiprep = 1; }
            if (OptTip2.Checked == true) { n_tiprep = 2; }

            if (OptGru1.Checked == true) { n_tipgru = 1; }
            if (OptGru2.Checked == true) { n_tipgru = 2; }
            if (OptGru3.Checked == true) { n_tipgru = 3; }

            CN_pro_produccion funCom = new CN_pro_produccion();
            funCom.mysConec = mysConec;
            funCom.STU_SISTEMA = STU_SISTEMA;

            string c_CadINIte = funFlex.Flex_CadenaIN(FgPro, 2, 1);
            string c_CadINIns = funFlex.Flex_CadenaIN(FgIns, 2, 1); ;
            FgDatos.Rows.Count = 2;

            if (n_tiprep == 1)
            {
                FgDatos.Cols.Count = 13;
                funCom.Consulta4(STU_SISTEMA.EMPRESAID, TxtFchIni.Text, TxtFchFin.Text, c_CadINIte, c_CadINIns); //, n_tiprep, n_tipgru, c_CadINCli, c_CadINIte);
                dtLista = funCom.dtListar;

                Cabecera2();
                funFlex.FlexMostrarDatos(FgDatos, arrCabecera2, dtLista, 2, true);
            }
            if (n_tiprep == 2)
            {
                FgDatos.Cols.Count = 13;
                funCom.Consulta1(STU_SISTEMA.EMPRESAID, TxtFchIni.Text, TxtFchFin.Text, c_CadINIte, c_CadINIns); //, n_tiprep, n_tipgru, c_CadINCli, c_CadINIte);
                dtLista = funCom.dtListar;

                Cabecera1();
                funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 2, true);
            }
        }
        private void FrmConsultaProduccion_Load(object sender, EventArgs e)
        {
            ConfigurarFormulario();
            CargarDT();
        }
        void CargarDT()
        {
            objIte.mysConec = mysConec;
            dtPro = objIte.Listar(STU_SISTEMA.EMPRESAID, 1, 1);
            dtPro = funDatos.DataTableFiltrar(dtPro, "n_idtipexi = 2");

            dtIns = objIte.Listar(STU_SISTEMA.EMPRESAID, 1, 1);
            dtIns = funDatos.DataTableFiltrar(dtIns, "n_idtipexi IN (3, 6, 8)");

            //funDatos.ComboBoxCargarDataTable(CboTipPro, dttipexi, "n_id", "c_des");
        }
        void ConfigurarFormulario()
        {
            this.Height = 669;
            this.Width = 1094;
            this.Text = "PRODUCCION - CONSULTA DE PRODUCCION";
            Sz1.Left = 0;
            Sz1.Top = 41;
            Sz1.Height = this.Height - 83;
            Sz1.Width = this.Width - 18;

            OptTip2.Checked = true;
            OptGru1.Checked = true;

            TxtFchIni.Text = "01/01/" + STU_SISTEMA.ANOTRABAJO.ToString();
            TxtFchFin.Text = DateTime.Now.ToString("dd/MM/yyyy");

            arrCabeceraFlex1[0, 0] = "Producto";
            arrCabeceraFlex1[0, 1] = "320";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_tipexides";

            arrCabeceraFlex1[1, 0] = "Id";
            arrCabeceraFlex1[1, 1] = "0";
            arrCabeceraFlex1[1, 2] = "N";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_itedes";

            funFlex.FlexMostrarDatos(FgPro, arrCabeceraFlex1, dtLista, 1, false);

            arrCabeceraFlex2[0, 0] = "Insumo";
            arrCabeceraFlex2[0, 1] = "320";
            arrCabeceraFlex2[0, 2] = "C";
            arrCabeceraFlex2[0, 3] = "";
            arrCabeceraFlex2[0, 4] = "c_tipexides";

            arrCabeceraFlex2[1, 0] = "Id";
            arrCabeceraFlex2[1, 1] = "0";
            arrCabeceraFlex2[1, 2] = "N";
            arrCabeceraFlex2[1, 3] = "";
            arrCabeceraFlex2[1, 4] = "c_itedes";

            funFlex.FlexMostrarDatos(FgIns, arrCabeceraFlex2, dtLista, 1, false);

            FgPro.Rows.Count = FgPro.Rows.Count + 1;
            FgIns.Rows.Count = FgIns.Rows.Count + 1;

            FgPro.Cols[1].ComboList = "...";
            FgIns.Cols[1].ComboList = "...";

            FgPro.AllowEditing = true;
            FgIns.AllowEditing = true;

            FgDatos.Cols.Count = 13;
            Cabecera1();
            funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 2, false);
        }
        void Cabecera1()
        {
            arrCabecera1[0, 0] = "Dia";
            arrCabecera1[0, 1] = "70";
            arrCabecera1[0, 2] = "F";
            arrCabecera1[0, 3] = "dd/MM/yyyy";
            arrCabecera1[0, 4] = "d_fchpro";

            arrCabecera1[1, 0] = "Nº Produccion";
            arrCabecera1[1, 1] = "110";
            arrCabecera1[1, 2] = "C";
            arrCabecera1[1, 3] = "";
            arrCabecera1[1, 4] = "c_numdocpro";

            arrCabecera1[2, 0] = "Uni. Med.";
            arrCabecera1[2, 1] = "40";
            arrCabecera1[2, 2] = "C";
            arrCabecera1[2, 3] = "";
            arrCabecera1[2, 4] = "c_abrpre";

            arrCabecera1[3, 0] = "Can. Producida";
            arrCabecera1[3, 1] = "80";
            arrCabecera1[3, 2] = "D";
            arrCabecera1[3, 3] = "0.00";
            arrCabecera1[3, 4] = "n_canprorea";

            arrCabecera1[4, 0] = "Responsable";
            arrCabecera1[4, 1] = "200";
            arrCabecera1[4, 2] = "C";
            arrCabecera1[4, 3] = "";
            arrCabecera1[4, 4] = "c_apenomres";

            arrCabecera1[5, 0] = "Receta";
            arrCabecera1[5, 1] = "80";
            arrCabecera1[5, 2] = "C";
            arrCabecera1[5, 3] = "";
            arrCabecera1[5, 4] = "c_codrec";

            arrCabecera1[6, 0] = "Insumo";
            arrCabecera1[6, 1] = "200";
            arrCabecera1[6, 2] = "";
            arrCabecera1[6, 3] = "";
            arrCabecera1[6, 4] = "c_insdespro";

            arrCabecera1[7, 0] = "Uni. Med.";
            arrCabecera1[7, 1] = "40";
            arrCabecera1[7, 2] = "C";
            arrCabecera1[7, 3] = "";
            arrCabecera1[7, 4] = "c_insabrpre";

            arrCabecera1[8, 0] = "Can. Teorica";
            arrCabecera1[8, 1] = "70";
            arrCabecera1[8, 2] = "D";
            arrCabecera1[8, 3] = "0.00";
            arrCabecera1[8, 4] = "n_canteo";

            arrCabecera1[9, 0] = "Can. Real";
            arrCabecera1[9, 1] = "70";
            arrCabecera1[9, 2] = "D";
            arrCabecera1[9, 3] = "0.00";
            arrCabecera1[9, 4] = "n_inscanent";

            arrCabecera1[10, 0] = "Desvio";
            arrCabecera1[10, 1] = "70";
            arrCabecera1[10, 2] = "D";
            arrCabecera1[10, 3] = "0.00";
            arrCabecera1[10, 4] = "n_desvio";

            arrCabecera1[11, 0] = "% Desvio";
            arrCabecera1[11, 1] = "70";
            arrCabecera1[11, 2] = "D";
            arrCabecera1[11, 3] = "0.00";
            arrCabecera1[11, 4] = "n_pordesvio";
        }
        void Cabecera2()
        {
            arrCabecera2[0, 0] = "Producto";
            arrCabecera2[0, 1] = "300";
            arrCabecera2[0, 2] = "C";
            arrCabecera2[0, 3] = "";
            arrCabecera2[0, 4] = "c_despro";

            arrCabecera2[1, 0] = "Receta";
            arrCabecera2[1, 1] = "70";
            arrCabecera2[1, 2] = "C";
            arrCabecera2[1, 3] = "";
            arrCabecera2[1, 4] = "c_codrec";

            arrCabecera2[2, 0] = "Uni. Med.";
            arrCabecera2[2, 1] = "40";
            arrCabecera2[2, 2] = "C";
            arrCabecera2[2, 3] = "";
            arrCabecera2[2, 4] = "c_abrpre";

            arrCabecera2[3, 0] = "Can. Teorica";
            arrCabecera2[3, 1] = "80";
            arrCabecera2[3, 2] = "D";
            arrCabecera2[3, 3] = "";
            arrCabecera2[3, 4] = "n_canpropro";

            arrCabecera2[4, 0] = "Can. Producida";
            arrCabecera2[4, 1] = "80";
            arrCabecera2[4, 2] = "D";
            arrCabecera2[4, 3] = "";
            arrCabecera2[4, 4] = "n_canprorea";

            arrCabecera2[5, 0] = "% Desv.";
            arrCabecera2[5, 1] = "40";
            arrCabecera2[5, 2] = "D";
            arrCabecera2[5, 3] = "";
            arrCabecera2[5, 4] = "n_pordespro";

            arrCabecera2[6, 0] = "Insumo";
            arrCabecera2[6, 1] = "200";
            arrCabecera2[6, 2] = "";
            arrCabecera2[6, 3] = "";
            arrCabecera2[6, 4] = "c_insdespro";

            arrCabecera2[7, 0] = "Uni. Med.";
            arrCabecera2[7, 1] = "40";
            arrCabecera2[7, 2] = "C";
            arrCabecera2[7, 3] = "";
            arrCabecera2[7, 4] = "c_insabrpre";

            arrCabecera2[8, 0] = "Can. Teorica";
            arrCabecera2[8, 1] = "70";
            arrCabecera2[8, 2] = "D";
            arrCabecera2[8, 3] = "0.00";
            arrCabecera2[8, 4] = "n_canteo";

            arrCabecera2[9, 0] = "Can. Real";
            arrCabecera2[9, 1] = "70";
            arrCabecera2[9, 2] = "D";
            arrCabecera2[9, 3] = "0.00";
            arrCabecera2[9, 4] = "n_inscanent";

            arrCabecera2[10, 0] = "Desvio";
            arrCabecera2[10, 1] = "70";
            arrCabecera2[10, 2] = "D";
            arrCabecera2[10, 3] = "0.00";
            arrCabecera2[10, 4] = "n_desvio";

            arrCabecera2[11, 0] = "% Desvio";
            arrCabecera2[11, 1] = "40";
            arrCabecera2[11, 2] = "D";
            arrCabecera2[11, 3] = "0.00";
            arrCabecera2[11, 4] = "n_pordesvio";
        }
        private void ToolSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ToolExportar_Click(object sender, EventArgs e)
        {
            //funFlex.ExportToExcel_NumFilaCabecera = 2;
            //string c_nomarch = STU_SISTEMA.EMPRESARUC + "-" + STU_SISTEMA.ANOTRABAJO.ToString() + "-" + "-REPORTEPRODUCCION-" + DateTime.Now.ToString("dd-mm-yyyy") + ".xls";
            //funFlex.ExportToExcel(FgDatos, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "CONSULTA DE PRODUCCION POR PERIODO", "DEL " + TxtFchIni.Text + " AL " + TxtFchFin.Text, c_nomarch);

            List<string> columnGridNames = new List<string>();
            List<string> columnHeaderNames = new List<string>();
            DataTable dtResult = new DataTable();

            if (OptTip1.Checked == true)
            {
                var results_dtTodoIns = dtLista.AsEnumerable().Select(i => new
                {
                    c_despro = i["c_despro"],
                    c_codrec = i["c_codrec"],
                    c_abrpre = i["c_abrpre"],
                    n_canpropro = i["n_canpropro"],
                    n_canprorea = i["n_canprorea"],
                    n_pordespro = i["n_pordespro"],
                    c_insdespro = i["c_insdespro"],
                    c_insabrpre = i["c_insabrpre"],
                    n_canteo = i["n_canteo"],
                    n_inscanent = i["n_inscanent"],
                    n_desvio = i["n_desvio"],
                    n_pordesvio = i["n_pordesvio"]
                }).ToList();
                string jsonResults_dtTodoIns = JsonConvert.SerializeObject(results_dtTodoIns);
                dtResult = JsonConvert.DeserializeObject<DataTable>(jsonResults_dtTodoIns);

                columnHeaderNames.AddRange(new string[]{
                    "Producto"
                    , "Receta"
                    , "Uni. Med. Prod."
                    , "Can. Teorica Prod."
                    , "Can. Producida"
                    , "% Desv."
                    , "Insumo"
                    , "Uni. Med."
                    , "Can. Teorica"
                    , "Can. Real"
                    , "Desvio"
                    , "% Desvio"});
            }
            if (OptTip2.Checked == true)
            {
                var results_dtTodoIns = dtLista.AsEnumerable().Select(i => new
                {
                    d_fchpro = i["d_fchpro"],
                    c_numdocpro = i["c_numdocpro"],
                    c_abrpre = i["c_abrpre"],
                    n_canprorea = i["n_canprorea"],
                    c_apenomres = i["c_apenomres"],
                    c_codrec = i["c_codrec"],
                    c_insdespro = i["c_insdespro"],
                    c_insabrpre = i["c_insabrpre"],
                    n_canteo = i["n_canteo"],
                    n_inscanent = i["n_inscanent"],
                    n_desvio = i["n_desvio"],
                    n_pordesvio = i["n_pordesvio"]
                }).ToList();
                string jsonResults_dtTodoIns = JsonConvert.SerializeObject(results_dtTodoIns);
                dtResult = JsonConvert.DeserializeObject<DataTable>(jsonResults_dtTodoIns);

                columnHeaderNames.AddRange(new string[]{
                    "Dia"
                    , "Nº Produccion"
                    , "Uni. Med. Prod."
                    , "Can. Producida"
                    , "Responsable"
                    , "Receta"
                    , "Insumo"
                    , "Uni. Med."
                    , "Can. Teorica"
                    , "Can. Real"
                    , "Desvio"
                    , "% Desvio"});
            }
            funFlex.ExportToExcel_v2(dtResult, columnHeaderNames.ToArray());
        }
        private void ToolImprimir_Click(object sender, EventArgs e)
        {

        }
        private void FgPro_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (FgPro.Col == 1)
            {
                DataTable dtResult = new DataTable();
                string c_dato = "";
                objItems.mysConec = mysConec;
                dtResult = objItems.BuscarItem("", "n_id", dtPro, 0);
                if (dtResult != null)
                {
                    if (dtResult.Rows.Count != 0)
                    {
                        c_dato = dtResult.Rows[0]["c_despro"].ToString();
                        FgPro.SetData(FgPro.Row, 1, c_dato);

                        c_dato = dtResult.Rows[0]["n_id"].ToString();
                        FgPro.SetData(FgPro.Row, 2, c_dato);

                        if (funFunciones.NulosC(FgPro.GetData(FgPro.Rows.Count - 1, 1)) != "")
                        {
                            FgPro.Rows.Count = FgPro.Rows.Count + 1;
                        }
                    }
                }
            }
        }
        private void FgIns_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (FgIns.Col == 1)
            {
                DataTable dtResult = new DataTable();
                string c_dato = "";
                objItems.mysConec = mysConec;
                dtResult = objItems.BuscarItem("", "n_id", dtIns, 0);
                if (dtResult != null)
                {
                    if (dtResult.Rows.Count != 0)
                    {
                        c_dato = dtResult.Rows[0]["c_despro"].ToString();
                        FgIns.SetData(FgIns.Row, 1, c_dato);

                        c_dato = dtResult.Rows[0]["n_id"].ToString();
                        FgIns.SetData(FgIns.Row, 2, c_dato);

                        if (funFunciones.NulosC(FgIns.GetData(FgIns.Rows.Count - 1, 1)) != "")
                        {
                            FgIns.Rows.Count = FgIns.Rows.Count + 1;
                        }
                    }
                }
            }
        }
        private void FrmConsultaProduccion_Resize(object sender, EventArgs e)
        {
            Sz1.Left = 0;
            Sz1.Top = 41;
            Sz1.Height = this.Height - 83;
            Sz1.Width = this.Width - 18;
        }
        private void FgPro_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString("") == "Delete")
            {
                FgPro.RemoveItem(FgPro.Row);
            }

            if (e.KeyCode.ToString("") == "Insert")
            {
                if (funFunciones.NulosC(FgPro.GetData(FgPro.Row, 1)).ToString() != "")
                {
                    FgPro.Rows.Count = FgPro.Rows.Count + 1;
                }
            }
        }
        private void FgIns_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString("") == "Delete")
            {
                FgIns.RemoveItem(FgIns.Row);
            }

            if (e.KeyCode.ToString("") == "Insert")
            {
                if (funFunciones.NulosC(FgIns.GetData(FgIns.Row, 1)).ToString() != "")
                {
                    FgIns.Rows.Count = FgIns.Rows.Count + 1;
                }
            }
        }
        private void OptTip2_CheckedChanged(object sender, EventArgs e)
        {
            Cabecera1();
            funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 2, false);
            if (OptTip2.Checked == true)
            { 
                OptGru2.Enabled = false;
                OptGru3.Enabled = false;
            }
        }
        private void OptTip1_CheckedChanged(object sender, EventArgs e)
        {
            Cabecera2();
            funFlex.FlexMostrarDatos(FgDatos, arrCabecera2, dtLista, 2, false);
            if (OptTip1.Checked == true)
            {
                OptGru2.Enabled = true;
                OptGru3.Enabled = true;
            }
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
