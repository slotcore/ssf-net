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
    public partial class FrmRepAsistenciaPersona : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_mae_meses objMeses = new CN_mae_meses();

        CN_TEMPUS_marcacion objCabecera = new CN_TEMPUS_marcacion();                            // CABECERA DEL REGISTRO

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // DATATABLE LOCALES
        DataTable dtForm = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtMeses1 = new DataTable();
        DataTable dtAnosTra = new DataTable();
        DataTable dtEmpresas = new DataTable();

        // VARIABLES LOCALES
        string strNumerovalidos = "1234567890./" + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;

        public FrmRepAsistenciaPersona()
        {
            InitializeComponent();
        }
        private void FrmRepAsistenciaPersona_Load(object sender, EventArgs e)
        {
            lblcodpersona.Text = "";
            TxtApeNom.Text = "";
            CargarCombos();
        }
        void CargarCombos()
        {
            DataTableCargar();
            funDatos.ComboBoxCargarDataTable(CboMesIni, dtMeses, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMesFin, dtMeses1, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboEmp, dtEmpresas, "EMPRESA", "NOMBRE");
            funDatos.ComboBoxCargarDataTable(CboAnoTra, dtAnosTra, "ANOTRA", "IDANOTRA");
        }
        void DataTableCargar()
        {
            objMeses.mysConec = mysConec;
            dtMeses = objMeses.Listar();
            dtMeses1 = objMeses.Listar();
            objCabecera.mysConec = mysConec;
            dtAnosTra = objCabecera.AnosTrabajo();
            dtEmpresas = objCabecera.Empresas();
        }
        private void CmdImp_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(CboEmp.SelectedValue) == 0)
            {
               MessageBox.Show("No se ha especificado la empresa a consultar", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
               CboEmp.Focus();
               return;
            }
            if (Convert.ToInt32(CboAnoTra.SelectedValue) == 0)
            {
                MessageBox.Show("No se ha especificado el año de trabajo", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboMesIni.Focus();
                return;
            }
            if (Convert.ToInt32(CboMesIni.SelectedValue) == 0)
            {
                MessageBox.Show("No se ha especificado el mes de inicio", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboMesIni.Focus();
                return;
            }
            if (Convert.ToInt32(CboMesFin.SelectedValue) == 0)
            {
                MessageBox.Show("No se ha especificado el mes de termino", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboMesIni.Focus();
                return;
            }
            if (lblcodpersona.Text == "")
            {
                MessageBox.Show("No se ha especificado la persona que desea consultar", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtApeNom.Focus();
                return;
            }
            if (Convert.ToInt32(CboMesIni.SelectedValue) > Convert.ToInt32(CboMesFin.SelectedValue))
            {
                MessageBox.Show("El mes de termino no puede ser menor al mes de inicio", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboMesIni.Focus();
                return;
            }
            objCabecera.STU_SISTEMA = STU_SISTEMA;
            objCabecera.mysConec = mysConec;
            objCabecera.RptAsistenciaPersona(Convert.ToInt32(CboAnoTra.SelectedValue), Convert.ToInt32(CboMesIni.SelectedValue), Convert.ToInt32(CboMesFin.SelectedValue), Convert.ToString(CboEmp.SelectedValue), lblcodpersona.Text);
        }
        private void CmdSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(CboEmp.SelectedValue) == 0)
            {
                MessageBox.Show("No se ha especificado el nombre de la empresa", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtApeNom.Focus();
                return;
            }
            string[,] arrCabeceraDg1 = new string[2, 4];
            DataTable dtResult = new DataTable();
            DataTable dtresulpre = new DataTable();
            int n_Fila;
            DataTable DtProdDet = new DataTable();
            CN_TEMPUS_marcacion objPersona = new CN_TEMPUS_marcacion();

            objPersona.mysConec = mysConec;
            DtProdDet = objPersona.ListarPersonal(CboEmp.SelectedValue.ToString());

            arrCabeceraDg1[0, 0] = "Apellidos y Nombres";
            arrCabeceraDg1[0, 1] = "400";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "c_apenom";

            arrCabeceraDg1[1, 0] = "D.N.I.";
            arrCabeceraDg1[1, 1] = "80";
            arrCabeceraDg1[1, 2] = "C";
            arrCabeceraDg1[1, 3] = "c_dni";

            Genericas xFun = new Genericas();
            xFun.Buscar_CampoBusqueda = "c_dni";
            dtResult = xFun.DataTableFiltrar(DtProdDet, "c_codemp = '"+ CboEmp.SelectedValue.ToString() +"'");
            xFun.Buscar_CadFiltro = "";
            xFun.Buscar_CampoOrden = "c_apenom";
            dtResult = xFun.Buscar(arrCabeceraDg1, dtResult);


            //Genericas xFun = new Genericas();
            //xFun.Buscar_CampoBusqueda = "c_id";
            //xFun.Buscar_CadFiltro = "";
            //xFun.Buscar_CampoOrden = "dia";
            //dtResult = xFun.Buscar(arrCabeceraDg1, DtSolMat);



            if (dtResult == null) { return; }
            if (dtResult.Rows.Count == 0) { return; }
                        
            for (n_Fila = 0; n_Fila <= (dtResult.Rows.Count - 1); n_Fila++)
            {
                lblcodpersona.Text = dtResult.Rows[n_Fila]["c_dni"].ToString();
                TxtApeNom.Text = dtResult.Rows[n_Fila]["c_apenom"].ToString();
            }
            
        }
    }
}
