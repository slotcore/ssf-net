using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Gestion;
using SIAC_Negocio.Maestros;
using SIAC_Negocio.Planilla;
using SIAC_Negocio.Sistema;
using SIAC_Objetos.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SSF_NET_Planillas.Formularios
{
    public partial class FrmRepCumpleaños : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_mae_meses objMeses = new CN_mae_meses();
        CN_sys_empresa objEmpresa = new CN_sys_empresa();
        

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_GES_PLANVENTAS BE_CABECERA = new BE_GES_PLANVENTAS();
        List<BE_GES_PLANVENTASDET> LS_DETALLE = new List<BE_GES_PLANVENTASDET>();

        // DATATABLE LOCALES
        DataTable dtCabecera = new DataTable();                                        // CABECERA DEL REGISTRO
        DataTable dtDetalle = new DataTable();                                         // DETALLE DEL REGISTRO
        DataTable dtForm = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtAnosTra = new DataTable();
        DataTable dtEmpresas = new DataTable();
        DataTable dtempresa = new DataTable();
        // VARIABLES LOCALES
        //int n_NumFilasDocumento = 30;                                                   // LE INDICAMOS AL FORMULARIO EL NUMERO MAXIMO DE FILAS PARA EL DETALLE
        //int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[7, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[3, 5];
        //bool booSeEjecuto = false;
        //bool booAgregando = false;
        string strNumerovalidos = "1234567890./" + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        //int n_idformulario = 0;  
        public FrmRepCumpleaños()
        {
            InitializeComponent();
        }
        private void CmdSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmRepCumpleaños_Load(object sender, EventArgs e)
        {
            CargarCombos();
            this.Text = "PLANILLAS - REPORTE DE CUMPLEAÑOS";
            //OptTodMes.Checked = true;
        }
        void CargarCombos()
        {
            DataTableCargar();
            funDatos.ComboBoxCargarDataTable(CboMes, dtMeses, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboEmp, dtempresa, "n_id", "c_nomemp");
        }
        void DataTableCargar()
        {
            objMeses.mysConec = mysConec;
            objEmpresa.mysConec = mysConec;

            dtMeses = objMeses.Listar();
            dtMeses = funDatos.DataTableFiltrar(dtMeses, "n_id IN (1,2,3,4,5,6,7,8,9,10,11,12)");
            //objCabecera.mysConec = mysConec;
            //dtAnosTra = objCabecera.AnosTrabajo();

            objEmpresa.Listar();
            dtempresa = objEmpresa.dtLista;
        }
        private void CmdImp_Click(object sender, EventArgs e)
        {
            //if (OptSelMes.Checked == true)
            //{
            //    if (Convert.ToInt16(CboMes.SelectedValue) == 0)
            //    {
            //        MessageBox.Show("No se ha especificado el mes a consultar", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //        CboMes.Focus();
            //        return;
            //    }
            //}
            bool b_destacado = false;

            if (ChkDes.Checked == true) { b_destacado = true; }

            CN_pla_empleados objCabecera = new CN_pla_empleados(STU_SISTEMA);                            // CABECERA DEL REGISTRO
            objCabecera.STU_SISTEMA = STU_SISTEMA;
            //objCabecera.mysConec = mysConec;
            objCabecera.ReporteCumpleaños(Convert.ToInt16(CboEmp.SelectedValue), Convert.ToInt16(CboMes.SelectedValue), b_destacado);
        }
        private void OptTodMes_CheckedChanged(object sender, EventArgs e)
        {
            //if (OptTodMes.Checked == true)
            //{
            //    CboMes.SelectedValue = 0;
            //    CboMes.Enabled = false;
            //}
        }
        private void OptSelMes_CheckedChanged(object sender, EventArgs e)
        {
            //if (OptSelMes.Checked == true)
            //{
            //    CboMes.Enabled = true;
            //}
        }
    }
}
