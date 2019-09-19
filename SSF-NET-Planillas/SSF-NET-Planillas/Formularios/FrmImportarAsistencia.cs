using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Planillas;
using SIAC_Entidades.Maestros;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Sunat;
using SIAC_Negocio.Maestros;
using SIAC_Objetos.Sistema;
using SIAC_Negocio.Planilla;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace SSF_NET_Planillas.Formularios
{
    public partial class FrmImportarAsistencia : Form
    {
        // VARIABLES PUBLICAS
        public SqlConnection sqlCon = new SqlConnection();
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES

        CN_pla_marcacion objRegistros = new CN_pla_marcacion();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_mae_meses objMeses = new CN_mae_meses();
        CN_pla_personal objPersonal = new CN_pla_personal();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        Cls_IO funIO = new Cls_IO ();

        // ENTIDADES LOCALES
        BE_PLA_MARCACION BE_Registro = new BE_PLA_MARCACION();
        List<BE_PLA_MARCACIONDET> lstMarcDet = new List<BE_PLA_MARCACIONDET>();

        // DATATABLE LOCALES
        DataTable dtLista = new DataTable();           // LISTARA TODOS LOS REGISTROS DEL MES ACTUAL
        DataTable dtRegistro = new DataTable();        // ALMACENARA LOS DATOS DEL REGISTRO SELECCIONADO
        DataTable dtPersonal = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtUniMed = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtMarcaciones = new DataTable();

        // VARIABLES LOCALES
        //int n_NumFilasDocumento = 30;                                                   // LE INDICAMOS AL FORMULARIO EL NUMERO MAXIMO DE FILAS PARA EL DETALLE
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO

        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;

        string[,] arrCabeceraDg1 = new string[6, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[3, 5];

        public FrmImportarAsistencia()
        {
            InitializeComponent();
        }

        private void FrmImportarAsistencia_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }
        void CargarCombos()
        {
            DataTableCargar();

            //funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboRes, dtPersonal, "n_id", "c_apenom");
        }
        void ConfigurarFormulario()
        {
            this.Height = 502;
            this.Width = 720;
            
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            this.Text = dtForm.Rows[0]["c_titfor"].ToString();
            //CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;

            arrCabeceraFlex1[0, 0] = "Nº DNI";
            arrCabeceraFlex1[0, 1] = "100";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_despro";

            arrCabeceraFlex1[1, 0] = "Fecha Marcacion";
            arrCabeceraFlex1[1, 1] = "100";
            arrCabeceraFlex1[1, 2] = "C";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_codrec";

            arrCabeceraFlex1[2, 0] = "Hora Marcacion";
            arrCabeceraFlex1[2, 1] = "100";
            arrCabeceraFlex1[2, 2] = "C";
            arrCabeceraFlex1[2, 3] = "";
            arrCabeceraFlex1[2, 4] = "c_unimed";

            funFlex.FlexMostrarDatos(FgLisPer, arrCabeceraFlex1, dtLista, 2, false);
        }
        void DataTableCargar()
        {
            objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            if (objRegistros.Listar (STU_SISTEMA.EMPRESAID) ==true)
            {
                dtLista = objRegistros.dtLista;
            }

            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(48, ref arrCabeceraDg1);

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(48);

            objPersonal.mysConec = mysConec;
            if (objPersonal.Listar(STU_SISTEMA.EMPRESAID) == true)
            {
                dtPersonal = objPersonal.dtLista;
            }
        }
        void ListarItems()
        {
            LblNumReg.Text = (dtLista.Rows.Count).ToString();
            funDbGrid.DG_FormatearGrid(DgLista, arrCabeceraDg1, dtLista, true);
        }
        void Tab_Dimensionar(C1.Win.C1Command.C1DockingTab dokTab, int intAlto, int intAncho)
        {
            Tab1.Height = intAlto;
            Tab1.Width = intAncho;
        }
        void Tab_Posicionar(C1.Win.C1Command.C1DockingTab dokTab, int intPosX, int intPosY)
        {
            dokTab.Left = intPosX;
            dokTab.Top = intPosY;
        }
        void VerRegistro(int n_IdRegistro)
        {
            booAgregando = true;

            objRegistros.mysConec = mysConec;

            objRegistros.TraerRegistro(n_IdRegistro);
            if (objRegistros.b_OcurrioError == false)
            {
                dtMarcaciones = objRegistros.dtMarcaciones;
                BE_Registro = objRegistros.entMarcacion;

                CboRes.SelectedValue = BE_Registro.n_idper;
                TxtFchIni.Text = Convert.ToDateTime(BE_Registro.d_fchini).ToString("dd/MM/yyyy");
                TxtFchFin.Text = Convert.ToDateTime(BE_Registro.d_fchfin).ToString("dd/MM/yyyy");
                TxtFchImp.Text = Convert.ToDateTime(BE_Registro.d_fchimp).ToString("dd/MM/yyyy");

                MostrarMarcaciones(dtMarcaciones);
            }
        }
        void Nuevo()
        {
            n_QueHace = 1;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();
            LblTitulo2.Text = "Agregando Nuevo Registro";
            Tab1.SelectedIndex = 1;
            CboRes.Focus();
        }
        void Blanquea()
        {
            TxtFchIni.Text = "";
            TxtFchFin.Text = "";
            TxtFchImp.Text = "";
            CboRes.SelectedValue = 0;
        }
        void Bloquea()
        {
            TxtFchIni.Enabled = !TxtFchIni.Enabled;
            TxtFchFin.Enabled = !TxtFchFin.Enabled;
            TxtFchImp.Enabled = !TxtFchImp.Enabled;
            CboRes.Enabled = !CboRes.Enabled;
        }
        void ActivarTool()
        {
            ToolNuevo.Enabled = !ToolNuevo.Enabled;
            ToolModificar.Enabled = !ToolModificar.Enabled;
            ToolEliminar.Enabled = !ToolEliminar.Enabled;
            ToolGrabar.Enabled = !ToolGrabar.Enabled;
            ToolCancelar.Enabled = !ToolCancelar.Enabled;
            ToolImprimir.Enabled = !ToolImprimir.Enabled;
            ToolSalir.Enabled = !ToolSalir.Enabled;
        }
        void Modificar()
        {
            n_QueHace = 2;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();

            int intIdRegistro = Convert.ToInt16(DgLista.Columns[1].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            CboRes.Focus();
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[1].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objRegistros.ELiminar(intIdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objRegistros.mysConec = mysConec;

                    if (objRegistros.Listar(STU_SISTEMA.EMPRESAID) == true)
                    {
                        dtLista = objRegistros.dtLista;
                    }
                    //dtLista = objRegistros.Listar(STU_SISTEMA.EMPRESAID);
                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
                }
                else
                {
                    MessageBox.Show("¡ No se pudo eliminar el registro por el siguiente motivo ! " + objRegistros.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            return booResult;
        }
        void Cancelar()
        {
            n_QueHace = 3;
            ActivarTool();
            Bloquea();
            LblTitulo2.Text = "Detalle del Registro";
            Tab1.TabPages[0].Enabled = true;
            Tab1.SelectedIndex = 0;
            DgLista.Focus();
        }
        bool Grabar()
        {
            bool booResultado = false;
            if (CamposOK() == false)
            {
                return booResultado;
            }
            AsignarEntidad();

            if (n_QueHace == 1)
            {
                booResultado = objRegistros.Insertar(BE_Registro, dtMarcaciones);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Actualizar(BE_Registro);
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ ¨Ha ocurrido un un problema, no se pudo guardar el registro ! Error Nº : " + objRegistros.c_ErrorMensaje.ToString() + " = " + objRegistros.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

            return booResultado;
        }
        void AsignarEntidad()
        {
            int n_row = 0;

            BE_Registro.n_idemp = STU_SISTEMA.EMPRESAID;
		    BE_Registro.n_id = 0;
		    BE_Registro.d_fchini = Convert.ToDateTime(TxtFchIni.Text);
		    BE_Registro.d_fchfin = Convert.ToDateTime(TxtFchFin.Text);
		    BE_Registro.d_fchimp = Convert.ToDateTime(TxtFchImp.Text);
		    BE_Registro.n_idper = Convert.ToInt32(CboRes.SelectedValue);
            BE_Registro.n_nummar = 0;  
            
            for (n_row =2; n_row <= FgLisPer.Rows.Count-1; n_row++)
            {
                BE_PLA_MARCACIONDET entCarDet = new BE_PLA_MARCACIONDET();

                entCarDet.n_idmar = 0;
                entCarDet.c_codemp = FgLisPer.GetData(n_row, 1).ToString();
                entCarDet.d_fchmar = Convert.ToDateTime(FgLisPer.GetData(n_row,2));
                entCarDet.c_hormar = FgLisPer.GetData(n_row, 3).ToString();

                lstMarcDet.Add(entCarDet);
            }
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (Convert.ToInt16(CboRes.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el responsable de la importacion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboRes.Focus();
                return booEstado;
            }
            
            if (TxtFchIni.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de inicio de la importacion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtFchIni.Focus();
                return booEstado;
            }
            if (TxtFchFin.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha final de la importacion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtFchFin.Focus();
                return booEstado;
            }
            if (TxtFchImp.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el dia en que se importo la asistencia !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtFchImp.Focus();
                return booEstado;
            }

            return booEstado;
        }
        private void CmdCan_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ToolNuevo_Click(object sender, EventArgs e)
        {
            Nuevo();
        }

        private void ToolModificar_Click(object sender, EventArgs e)
        {
            Modificar();
        }

        private void ToolEliminar_Click(object sender, EventArgs e)
        {
            EliminarRegistro();
        }

        private void ToolGrabar_Click(object sender, EventArgs e)
        {
            if (Grabar() == true)
            {
                // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                objRegistros.mysConec = mysConec;
                if (objRegistros.Listar(STU_SISTEMA.EMPRESAID) == true)
                {
                    dtLista = objRegistros.dtLista;
                }
                //dtLista = objRegistros.Listar(STU_SISTEMA.EMPRESAID);
                // MOSTRAMOS LOS DATOS EN LA GRILLA
                ListarItems();

                DialogResult Rpta = MessageBox.Show("! El registro se agrego con exito ¡ ¿Desea agregar otro registro? ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (DialogResult.Yes == Rpta)
                {
                    Nuevo();
                    return;
                }
                else
                {
                    Cancelar();
                }
            }
        }

        private void ToolCancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        private void ToolSalir_Click(object sender, EventArgs e)
        {
            //objUniMed = null;
            objFormVis = null;
            this.Close();
        }

        private void FrmImportarAsistencia_Activated(object sender, EventArgs e)
        {
            if (booSeEjecuto == false)
            {
                booSeEjecuto = true;
                ListarItems();

                if (dtLista.Rows.Count == 0)
                {
                    DialogResult Rpta = MessageBox.Show("No se han encontrado registros, ¿ Desea agregar uno ahora ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (DialogResult.Yes == Rpta)
                    {
                        Nuevo();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    DgLista.Focus();
                }
            }  
        }

        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[1].CellValue(DgLista.Row).ToString());
            Tab1.SelectedIndex = 1;
            booAgregando = true;
            VerRegistro(intIdRegistro);
            booAgregando = false;
        }

        private void DgLista_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                DataTable dtResult = new DataTable();

                string c_CadFiltro = funDbGrid.DG_LeerCondicionesFiltro(DgLista);
                dtResult = funDbGrid.DG_Filtrar(dtLista, c_CadFiltro, DgLista);
                DgLista.DataSource = dtResult;
                LblNumReg.Text = (dtResult.Rows.Count).ToString();
            }
        }

        private void Tab1_SelectedIndexChanging(object sender, C1.Win.C1Command.SelectedIndexChangingEventArgs e)
        {
            if (n_QueHace != 3) { return; }
           // if (DgLista.Rows.Count == 0) { return; }
            if (e.NewIndex == 1)
            {
                int intIdRegistro = Convert.ToInt16(DgLista.Columns[1].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    booAgregando = true;
                    VerRegistro(intIdRegistro);
                    booAgregando = false;
                }
            }
        }

        private void CmdImp_Click(object sender, EventArgs e)
        {
            DataTable dtResul = new DataTable();
            List<BE_PLA_MARCACION> lstMarca = new List<BE_PLA_MARCACION>();
            CN_TEMPUS_MARCACIONES objMar = new CN_TEMPUS_MARCACIONES();
            objMar.sqlConec = sqlCon;
            objMar.TraerMarcacion(TxtFchIni.Text, TxtFchFin.Text);

            if (objMar.n_ErrorNumber != 0)
            {
                MessageBox.Show("¡ No se pudo importar la asistencia por el siguiente error ! " + objMar.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            dtMarcaciones = objMar.dtLista;

            if (dtMarcaciones.Rows.Count == 0)
            {
                MessageBox.Show("¡ No se encontraron registros para importar ! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            MostrarMarcaciones(dtMarcaciones);
        }
        void MostrarMarcaciones(DataTable dtMarcaciones)
        {
            int n_row = 0;
            FgLisPer.Rows.Count = 2;

            for (n_row = 0; n_row <= dtMarcaciones.Rows.Count - 1; n_row++)
            {
                FgLisPer.Rows.Count = FgLisPer.Rows.Count + 1;
                FgLisPer.SetData(FgLisPer.Rows.Count - 1, 1, dtMarcaciones.Rows[n_row]["c_codemp"].ToString());
                FgLisPer.SetData(FgLisPer.Rows.Count - 1, 2, dtMarcaciones.Rows[n_row]["d_fchmar"].ToString());
                FgLisPer.SetData(FgLisPer.Rows.Count - 1, 3, dtMarcaciones.Rows[n_row]["c_hormar"].ToString());
            }

        }
    }
}
