using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Contabilidad;
using SIAC_Negocio.Contabilidad;
using SIAC_Negocio.Maestros;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Sunat;
using SIAC_Objetos.Sistema;
using SIAC_Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SSF_NET_Contabilidad.Formularios
{
    public partial class FrmCerrarModulo : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_con_cerrarmes objRegistros = new CN_con_cerrarmes();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_modulos objModulo = new CN_sys_modulos();
        CN_sys_formulario objForm = new CN_sys_formulario();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Helper.Comunes.Funciones funFunciones = new Helper.Comunes.Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        SIAC_Objetos.Funciones objFunciones = new SIAC_Objetos.Funciones();

        DataTable dtLista = new DataTable();
        DataTable dtPlaCue = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtModulo = new DataTable();
        DataTable dtMes = new DataTable();

        bool booAgregando;

        BE_CON_PROVICIONES e_Proviciones = new BE_CON_PROVICIONES();
        List<BE_CON_PROVICIONESDET> l_ProvicionesDet = new List<BE_CON_PROVICIONESDET>();
        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraFlex1 = new string[7, 5];
        string[,] arrCabeceraDg1 = new string[10, 5];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL

        bool booSeEjecuto = false;

        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmCerrarModulo()
        {
            InitializeComponent();
        }

        private void FrmCerrarModulo_Load(object sender, EventArgs e)
        {
            CargarCombos();
            ConfigurarFormulario();
        }
        void CargarCombos()
        {
            DataTableCargar();
            booAgregando = true;
            funDatos.ComboBoxCargarDataTable(CboModulo, dtModulo, "n_id", "c_des");

            booAgregando = false;
        }
        void ConfigurarFormulario()
        {
            this.Height = 441;
            this.Width = 452;
            
            this.Text = dtForm.Rows[0]["c_titfor"].ToString();

            arrCabeceraFlex1[0, 0] = "Mes";
            arrCabeceraFlex1[0, 1] = "200";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_tipexides";

            arrCabeceraFlex1[1, 0] = "Cerrado";
            arrCabeceraFlex1[1, 1] = "50";
            arrCabeceraFlex1[1, 2] = "B";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_desunimed";

            arrCabeceraFlex1[2, 0] = "IdMes";
            arrCabeceraFlex1[2, 1] = "0";
            arrCabeceraFlex1[2, 2] = "N";
            arrCabeceraFlex1[2, 3] = "";
            arrCabeceraFlex1[2, 4] = "c_desunimed";

            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtLista, 2, false);
            FgItems.Rows.Count = FgItems.Rows.Count + 1;
        }
        void DataTableCargar()
        {
            objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            objRegistros.Listar(STU_SISTEMA.EMPRESAID);
            dtLista = objRegistros.dtLista;

            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(63, ref arrCabeceraDg1);

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(63);

            objModulo.mysConec = mysConec;
            objModulo.Listar();
            dtModulo = objModulo.dtLista;
        }

        private void FrmCerrarModulo_Activated(object sender, EventArgs e)
        {
            if (booSeEjecuto == false)
            {
                booSeEjecuto = true;
                booAgregando = true;
                CboModulo.SelectedValue = 0;
                booAgregando = false;
                ListarEstado();

                if (dtLista.Rows.Count == 0)
                {
                    DialogResult Rpta = MessageBox.Show("No se han encontrado registros, ¿ Desea agregar uno ahora ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (DialogResult.Yes == Rpta)
                    {
                       // Nuevo();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    FgItems.Focus();
                }
            }  
        }
        void ListarEstado()
        {
            if (Convert.ToInt16(CboModulo.SelectedValue) == 0) { return; }
            int n_idmodulo = Convert.ToInt16(CboModulo.SelectedValue);
            int n_row = 0;
            string c_dato = "";
            int n_valor = 0;
            if (n_idmodulo == 0) { return; }

            FgItems.Rows.Count = 2;
            DataTable dtresult = new DataTable();
            dtresult = funDatos.DataTableFiltrar(dtLista, "n_idmod = " + n_idmodulo.ToString() + "");

            if (dtresult.Rows.Count != 0)
            {
                for(n_row=0; n_row<=dtresult.Rows.Count-1; n_row++)
                {
                    FgItems.Rows.Count = FgItems.Rows.Count + 1;
                    c_dato = dtresult.Rows[n_row]["c_mesdes"].ToString();
                    FgItems.SetData(FgItems.Rows.Count - 1, 1, c_dato);

                    n_valor = Convert.ToInt16(dtresult.Rows[n_row]["n_estado"]);
                    if (n_valor == 0) { FgItems.SetData(FgItems.Rows.Count - 1, 2, false); }
                    if (n_valor == 1) { FgItems.SetData(FgItems.Rows.Count - 1, 2, true); }

                    c_dato = dtresult.Rows[n_row]["n_idmes"].ToString();
                    FgItems.SetData(FgItems.Rows.Count - 1, 3, c_dato);
                }
            }
        }

        private void CboModulo_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            ListarEstado();
        }

        private void ToolSalir_Click(object sender, EventArgs e)
        {
            objRegistros = null;
            objModulo = null;
            objFormVis = null;
            objForm = null;
            this.Close();
        }

        private void ToolGrabar_Click(object sender, EventArgs e)
        {
            List<BE_CON_CERRARMES> l_cerrarmes = new List<BE_CON_CERRARMES>();
            int n_row = 0;
            int n_idmodulo = Convert.ToInt16(CboModulo.SelectedValue);
            int n_idmes = 0;
            bool b_estado = false;

            for(n_row =2; n_row<= FgItems.Rows.Count-1; n_row++)
            {
                BE_CON_CERRARMES e_cerrarmes = new BE_CON_CERRARMES();
                n_idmes = Convert.ToInt16(FgItems.GetData(n_row, 3));
                b_estado = Convert.ToBoolean(FgItems.GetData(n_row, 2));
                e_cerrarmes.n_idmod = n_idmodulo;
                e_cerrarmes.n_idmes = n_idmes;
                e_cerrarmes.n_idemp = STU_SISTEMA.EMPRESAID;
                if (b_estado == false) { e_cerrarmes.n_estado = 0; }
                if (b_estado == true) { e_cerrarmes.n_estado = 1; }
                
                l_cerrarmes.Add(e_cerrarmes);
            }

            if (objRegistros.Actualizar(l_cerrarmes) == true)
            {
                objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
                objRegistros.Listar(STU_SISTEMA.EMPRESAID);
                dtLista = objRegistros.dtLista;
                ListarEstado();
                MessageBox.Show("¡ El estado del modulo " + CboModulo.Text + " se actualizo con exito ! ","", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1); 
            }
            else
            {
                MessageBox.Show("¡ No se pudo actualizar el registro por el siguiente motivo ! " + objRegistros.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void CboModulo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FgItems_EnterCell(object sender, EventArgs e)
        {
            if (FgItems.Col == 1) { FgItems.AllowEditing = false; }
            if (FgItems.Col == 2) { FgItems.AllowEditing = true; }
        }
    }
}
