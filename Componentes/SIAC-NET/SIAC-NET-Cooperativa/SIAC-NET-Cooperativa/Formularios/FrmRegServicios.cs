using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Almacen;
using SIAC_Entidades.Cooperativa;
using SIAC_Negocio.Cooperativa;
using SIAC_Negocio.Sistema;
using SIAC_Objetos.Sistema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIAC_NET_Cooperativa.Formularios
{
    public partial class FrmRegServicios : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_coo_servicios objRegistros = new CN_coo_servicios();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_coo_tiposervicio objTipSer = new CN_coo_tiposervicio();
        CN_coo_sociospuestos objSociosPuestos = new CN_coo_sociospuestos();
        CN_coo_socios objSocios = new CN_coo_socios();
        CN_sys_formulario objForm = new CN_sys_formulario();
        
        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        Cls_Controles funControl = new Cls_Controles();

        // ENTIDADES LOCALES
        BE_COO_SERVICIOS entRegistro = new BE_COO_SERVICIOS();
        List<BE_COO_SERVICIOSDET> lstRegistrosDet = new List<BE_COO_SERVICIOSDET>();

        // DATATABLE LOCALES
        DataTable dtRegistros = new DataTable();
        DataTable dtTipSer = new DataTable();
        DataTable dtSocPue = new DataTable();
        DataTable dtSocios = new DataTable();
        DataTable dtForm = new DataTable();

        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[7, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlexCar = new string[9, 5];
        bool booSeEjecuto = false;

        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890-()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmRegServicios()
        {
            InitializeComponent();
        }

        private void FrmRegServicios_Load(object sender, EventArgs e)
        {
            CargarCombos();
            ConfigurarFormulario();
        }
        void CargarCombos()
        {
            DataTableCargar();

            funDatos.ComboBoxCargarDataTable(CboTipSer, dtTipSer, "n_id", "c_des");
        }
        void ConfigurarFormulario()
        {
            this.Height = 642;
            this.Width = 940;
            
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 40);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";
            this.Text = dtForm.Rows[0]["c_titfor"].ToString();

            arrCabeceraFlexCar[0, 0] = "Nº Puesto";
            arrCabeceraFlexCar[0, 1] = "50";
            arrCabeceraFlexCar[0, 2] = "C";
            arrCabeceraFlexCar[0, 3] = "";
            arrCabeceraFlexCar[0, 4] = "";

            arrCabeceraFlexCar[1, 0] = "Nº DNI";
            arrCabeceraFlexCar[1, 1] = "80";
            arrCabeceraFlexCar[1, 2] = "C";
            arrCabeceraFlexCar[1, 3] = "";
            arrCabeceraFlexCar[1, 4] = "";

            arrCabeceraFlexCar[2, 0] = "Nombre Socio";
            arrCabeceraFlexCar[2, 1] = "200";
            arrCabeceraFlexCar[2, 2] = "C";
            arrCabeceraFlexCar[2, 3] = "";
            arrCabeceraFlexCar[2, 4] = "";

            arrCabeceraFlexCar[3, 0] = "Lectura Inicial";
            arrCabeceraFlexCar[3, 1] = "80";
            arrCabeceraFlexCar[3, 2] = "C";
            arrCabeceraFlexCar[3, 3] = "";
            arrCabeceraFlexCar[3, 4] = "";

            arrCabeceraFlexCar[4, 0] = "Lectura Final";
            arrCabeceraFlexCar[4, 1] = "80";
            arrCabeceraFlexCar[4, 2] = "C";
            arrCabeceraFlexCar[4, 3] = "";
            arrCabeceraFlexCar[4, 4] = "";

            arrCabeceraFlexCar[5, 0] = "Importe";
            arrCabeceraFlexCar[5, 1] = "80";
            arrCabeceraFlexCar[5, 2] = "D";
            arrCabeceraFlexCar[5, 3] = "";
            arrCabeceraFlexCar[5, 4] = "";

            arrCabeceraFlexCar[6, 0] = "Observaciones";
            arrCabeceraFlexCar[6, 1] = "200";
            arrCabeceraFlexCar[6, 2] = "C";
            arrCabeceraFlexCar[6, 3] = "";
            arrCabeceraFlexCar[6, 4] = "";

            arrCabeceraFlexCar[7, 0] = "idpuesto";
            arrCabeceraFlexCar[7, 1] = "0";
            arrCabeceraFlexCar[7, 2] = "N";
            arrCabeceraFlexCar[7, 3] = "";
            arrCabeceraFlexCar[7, 4] = "";

            arrCabeceraFlexCar[8, 0] = "idsocio";
            arrCabeceraFlexCar[8, 1] = "0";
            arrCabeceraFlexCar[8, 2] = "N";
            arrCabeceraFlexCar[8, 3] = "";
            arrCabeceraFlexCar[8, 4] = "";

            funFlex.FlexMostrarDatos(FgSocios, arrCabeceraFlexCar, dtRegistros, 2, false);
        }
        void DataTableCargar()
        {
            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(53);

            objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            objRegistros.Listar(STU_SISTEMA.EMPRESAID);
            dtRegistros = objRegistros.dtListar;

            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(53, ref arrCabeceraDg1);

            objTipSer.mysConec = mysConec;
            objTipSer.Listar(STU_SISTEMA.EMPRESAID);
            dtTipSer = objTipSer.dtListar;

            objSociosPuestos.mysConec = mysConec;
            objSociosPuestos.Consulta1(STU_SISTEMA.EMPRESAID, 1);
            dtSocPue = objSociosPuestos.dtPuestosSocios;

            objSocios.mysConec = mysConec;
            dtSocios = objSocios.Listar(STU_SISTEMA.EMPRESAID);
        }
        void ListarItems()
        {
            LblNumReg.Text = (dtRegistros.Rows.Count).ToString();
            funDbGrid.DG_FormatearGrid(DgLista, arrCabeceraDg1, dtRegistros, true);
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
            int n_row = 0;
            string c_dato = "";
            string c_idsoc = "";

            objRegistros.mysConec = mysConec;
            objRegistros.TraerRegistro(n_IdRegistro);
            entRegistro = objRegistros.entServicios;
            lstRegistrosDet = objRegistros.lstServicios;

            TxtFchIni.Text = Convert.ToDateTime(entRegistro.d_fchini).ToString("dd/MM/yyyy");
            TxtFchFin.Text = Convert.ToDateTime(entRegistro.d_fchfin).ToString("dd/MM/yyyy");
            CboTipSer.SelectedValue = Convert.ToInt32(entRegistro.n_idtipser);
            TxtImpTotSer.Text = Convert.ToDouble(entRegistro.n_imptot).ToString("0.00");
            TxtObs.Text = entRegistro.c_obs;

            FgSocios.Rows.Count = 2;

            for (n_row = 0; n_row <= lstRegistrosDet.Count - 1; n_row++)
            {
                FgSocios.Rows.Count = FgSocios.Rows.Count + 1;

                c_dato = funDatos.DataTableBuscar(dtSocPue, "n_id", "c_puesto", lstRegistrosDet[n_row].n_idpue.ToString(), "N").ToString();
                FgSocios.SetData(FgSocios.Rows.Count - 1, 1, c_dato);                         // PUESTO DEL SOCIOS

                c_idsoc = funDatos.DataTableBuscar(dtSocPue, "n_id", "n_idsoc", lstRegistrosDet[n_row].n_idpue.ToString(), "N").ToString();
                c_dato = funDatos.DataTableBuscar(dtSocios, "n_id", "c_idenumdoc", c_idsoc, "N").ToString();
                FgSocios.SetData(FgSocios.Rows.Count - 1, 2, c_dato);                         // NUMERO DE DOCUMENTO DEL SOCIOS

                c_dato = funDatos.DataTableBuscar(dtSocios, "n_id", "c_apenom", c_idsoc, "N").ToString();
                FgSocios.SetData(FgSocios.Rows.Count - 1, 3, c_dato);                         // APELLIDOS Y NOMBRES

                FgSocios.SetData(FgSocios.Rows.Count - 1, 4, lstRegistrosDet[n_row].c_numlecini.ToString());
                FgSocios.SetData(FgSocios.Rows.Count - 1, 5, lstRegistrosDet[n_row].c_numlecfin.ToString());
                FgSocios.SetData(FgSocios.Rows.Count - 1, 6, lstRegistrosDet[n_row].n_impcon.ToString("0.00"));
                FgSocios.SetData(FgSocios.Rows.Count - 1, 7, lstRegistrosDet[n_row].c_obs.ToString());
                FgSocios.SetData(FgSocios.Rows.Count - 1, 8, lstRegistrosDet[n_row].n_idpue.ToString());
                FgSocios.SetData(FgSocios.Rows.Count - 1, 9, c_idsoc);
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
            CboTipSer.Focus();
        }
        void Blanquea()
        {
            funControl.dtpBlanquea(TxtFchIni);
            funControl.dtpBlanquea(TxtFchFin);
            CboTipSer.SelectedValue = 0;
            TxtObs.Text = "";
            TxtImpTotSer.Text = "";

            FgSocios.Rows.Count = 2;
        }
        void Bloquea()
        {
            TxtFchIni.Enabled = !TxtFchIni.Enabled;
            TxtFchFin.Enabled = !TxtFchFin.Enabled;
            CboTipSer.Enabled = !CboTipSer.Enabled;
            TxtImpTotSer.Enabled = !TxtImpTotSer.Enabled;
            TxtObs.Enabled = !TxtObs.Enabled;
        }
        void ActivarTool()
        {
            ToolNuevo.Enabled = !ToolNuevo.Enabled;
            ToolModificar.Enabled = !ToolModificar.Enabled;
            ToolEliminar.Enabled = !ToolEliminar.Enabled;
            ToolGrabar.Enabled = !ToolGrabar.Enabled;
            ToolCancelar.Enabled = !ToolCancelar.Enabled;
            ToolImprimir.Enabled = !ToolImprimir.Enabled;
            //ToolExportar.Enabled = !ToolExportar.Enabled;
            ToolSalir.Enabled = !ToolSalir.Enabled;
        }
        void Modificar()
        {
            n_QueHace = 2;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();

            int intIdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            CboTipSer.Focus();
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objRegistros.Eliminar(intIdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objRegistros.mysConec = mysConec;
                    objRegistros.Listar(STU_SISTEMA.EMPRESAID);
                    dtRegistros = objRegistros.dtListar;
                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
                }
                else
                {
                    MessageBox.Show("¡ No se pudo eliminar el registro por el siguiente motivo ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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
                booResultado = objRegistros.Insertar(entRegistro, lstRegistrosDet);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Actualizar(entRegistro, lstRegistrosDet);
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ ¨Ha ocurrido un un problema, no se pudo guardar el registro ! Error Nº : " + objRegistros.IntErrorNumber.ToString() + " = " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

            return booResultado;
        }
        void AsignarEntidad()
        {
            int n_row = 0;

            if (n_QueHace == 1)
            {
                entRegistro.n_id = 0;
            }
            else
            {
                entRegistro.n_id = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString()); ;
            }
            
            entRegistro.n_idemp = STU_SISTEMA.EMPRESAID;
            entRegistro.n_anotra = STU_SISTEMA.ANOTRABAJO;
            entRegistro.n_mestra = STU_SISTEMA.MESTRABAJO;
            entRegistro.d_fchini = Convert.ToDateTime(TxtFchIni.Text);
            entRegistro.d_fchfin = Convert.ToDateTime(TxtFchFin.Text);
            entRegistro.n_imptot = Convert.ToDouble(TxtImpTotSer.Text);
            entRegistro.n_idtipser = Convert.ToInt32(CboTipSer.SelectedValue);
            entRegistro.c_obs = TxtObs.Text;

            // ELIMINAMOS LOS REGISTROS QUE EL IMPORTE DEL CARGO SEA 0
            for (n_row = 2; n_row <= FgSocios.Rows.Count - 1; n_row++)
            {
                if (Convert.ToDouble(funFunciones.NulosN(FgSocios.GetData(n_row, 6))) == 0)
                {
                    FgSocios.RemoveItem(n_row);
                    n_row = n_row - 1;
                }
            }

            // ALMACENAMOS LOS CARGOS
            lstRegistrosDet.Clear();
            for (n_row = 2; n_row <= FgSocios.Rows.Count - 1; n_row++)
            {
                BE_COO_SERVICIOSDET entDetalle = new BE_COO_SERVICIOSDET();

                entDetalle.n_idser = entRegistro.n_id;
                entDetalle.n_idpue = Convert.ToInt32(FgSocios.GetData(n_row,8));
                entDetalle.c_numlecini = funFunciones.NulosC(FgSocios.GetData(n_row, 4)).ToString();
                entDetalle.c_numlecfin = funFunciones.NulosC(FgSocios.GetData(n_row, 5)).ToString();
                entDetalle.n_impcon = Convert.ToDouble(FgSocios.GetData(n_row,6));
                entDetalle.c_obs =funFunciones.NulosC(FgSocios.GetData(n_row, 7));

                lstRegistrosDet.Add(entDetalle);
            }
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (TxtFchIni.Text == " ")
            {
                MessageBox.Show("¡ No ha especificado la fecha de inicio del periodo a facturar!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtFchIni.Focus();
                return booEstado;
            }
            if (TxtFchFin.Text == " ")
            {
                MessageBox.Show("¡ No ha especificado la fecha de termino del periodo a facturar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtFchFin.Focus();
                return booEstado;
            }
            if (Convert.ToInt32(CboTipSer.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de servicio a facturar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboTipSer.Focus();
                return booEstado;
            }
            if (TxtImpTotSer.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el importe total del cargo !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtImpTotSer.Focus();
                return booEstado;
            }

            return booEstado;
        }
        private void FrmRegServicios_Activated(object sender, EventArgs e)
        {
            if (booSeEjecuto == false)
            {
                booSeEjecuto = true;
                ListarItems();

                if (dtRegistros.Rows.Count == 0)
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
                objRegistros.Listar(STU_SISTEMA.EMPRESAID);
                dtRegistros = objRegistros.dtListar;
                DialogResult Rpta;

                // MOSTRAMOS LOS DATOS EN LA GRILLA
                ListarItems();

                if (n_QueHace == 1)
                {
                    Rpta = MessageBox.Show("! El registro se agrego con exito ¡ ¿Desea agregar otro registro? ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    Rpta = MessageBox.Show("! El registro se actualizo con exito ¡ ¿Desea agregar otro registro? ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                }

                if (DialogResult.Yes == Rpta)
                {
                    Nuevo();
                    ActivarTool();           // LLAMAMOS DE NUEVO A ESTA FUNCION PORQUE SE LLAMA DENTRO DE LA FUNCION NUEVO
                    Bloquea();               // LLAMAMOS DE NUEVO A ESTA FUNCION PORQUE SE LLAMA DENTRO DE LA FUNCION NUEVO
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
            objRegistros = null;
            dtRegistros = null;

            objFormVis = null;

            this.Close();
        }
        private void TxtObs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (!strCaracteres.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void TxtImpTotSer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void TxtFchIni_ValueChanged(object sender, EventArgs e)
        {
            TxtFchIni.CustomFormat = "dd/MM/yyyy";
            TxtFchIni.Format = DateTimePickerFormat.Custom;
        }
        private void TxtFchFin_ValueChanged(object sender, EventArgs e)
        {
            TxtFchFin.CustomFormat = "dd/MM/yyyy";
            TxtFchFin.Format = DateTimePickerFormat.Custom;
        }
        private void TxtFchIni_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete")
            {
                funControl.dtpBlanquea(TxtFchIni);
            }
        }
        private void TxtFchFin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete")
            {
                funControl.dtpBlanquea(TxtFchFin);
            }
        }
        private void CboTipSer_SelectedValueChanged(object sender, EventArgs e)
        {


        }
        private void CmdCarPue_Click(object sender, EventArgs e)
        {
            int n_row = 0;
            DataTable dtResult = new DataTable();

            objRegistros.Consulta2(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboTipSer.SelectedValue));
            dtResult = objRegistros.dtListar;

            FgSocios.Rows.Count = 2;

            if (objRegistros.booOcurrioError==false)
            {
                for (n_row=0; n_row <= dtResult.Rows.Count-1; n_row++)
                {
                    FgSocios.Rows.Count = FgSocios.Rows.Count +1;
                    FgSocios.SetData(FgSocios.Rows.Count-1, 1, dtResult.Rows[n_row]["c_puesto"].ToString());
                    FgSocios.SetData(FgSocios.Rows.Count-1, 2, dtResult.Rows[n_row]["c_idenumdoc"].ToString());
                    FgSocios.SetData(FgSocios.Rows.Count-1, 3, dtResult.Rows[n_row]["c_apenom"].ToString());

                    FgSocios.SetData(FgSocios.Rows.Count-1, 8, dtResult.Rows[n_row]["n_id"].ToString());
                    FgSocios.SetData(FgSocios.Rows.Count-1, 9, dtResult.Rows[n_row]["n_idsoc"].ToString());
                }
            }
            else
            {
            }
        }
        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
            Tab1.SelectedIndex = 1;
            VerRegistro(intIdRegistro);
        }
        private void Tab1_SelectedIndexChanging(object sender, C1.Win.C1Command.SelectedIndexChangingEventArgs e)
        {
            if (n_QueHace != 3) { return; }
            if (dtRegistros.Rows.Count == 0) 
            {
                //e.Cancel = true;
                return; 
            }
            if (e.NewIndex == 1)
            {
                int intIdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    VerRegistro(intIdRegistro);
                }
            }
        }
        private void DgLista_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                DataTable dtResult = new DataTable();

                string c_CadFiltro = funDbGrid.DG_LeerCondicionesFiltro(DgLista);
                dtResult = funDbGrid.DG_Filtrar(dtRegistros, c_CadFiltro, DgLista);
                DgLista.DataSource = dtResult;
                LblNumReg.Text = (dtResult.Rows.Count).ToString();
            }
        }
        private void FrmRegServicios_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 80, this.Width - 18);
        }

        private void ToolImprimir_Click(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
            CN_coo_servicios FunSol = new CN_coo_servicios();
            FunSol.mysConec = mysConec;
            FunSol.STU_SISTEMA = STU_SISTEMA;
            FunSol.ImprimirCargosServicios(intIdRegistro);
        }

        private void TxtImpTotSer_Validated(object sender, EventArgs e)
        {
            if (funFunciones.NulosC(TxtImpTotSer.Text) == "") { return; }
            
            TxtImpTotSer.Text = Convert.ToDouble(TxtImpTotSer.Text).ToString("0.00");
        }
    }
}
