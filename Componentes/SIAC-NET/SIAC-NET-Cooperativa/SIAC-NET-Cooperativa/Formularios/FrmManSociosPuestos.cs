using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Almacen;
using SIAC_Entidades.Cooperativa;
using SIAC_Negocio.Cooperativa;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Sunat;
using SIAC_Negocio.Maestros;
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
    public partial class FrmManSociosPuestos : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_coo_sociospuestos objRegistros = new CN_coo_sociospuestos();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sun_tipdocide objTipDoc = new CN_sun_tipdocide();
        CN_coo_puestos objPuestos = new CN_coo_puestos();
        CN_coo_socios objSocios = new CN_coo_socios();
        CN_coo_tiposocio objTipSoc = new CN_coo_tiposocio();
        CN_sun_tipdoccom objDocCom = new CN_sun_tipdoccom();
        CN_sys_formulario objForm = new CN_sys_formulario();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_COO_SOCIOSPUESTOS BE_Registro = new BE_COO_SOCIOSPUESTOS();
        List<BE_COO_SOCIOSPUESTOS> lstSocPue = new List<BE_COO_SOCIOSPUESTOS>();

        // DATATABLE LOCALES
        DataTable dtRegistros = new DataTable();
        DataTable dtTipDocIde = new DataTable();
        DataTable dtDocCom = new DataTable();
        DataTable dtSocios = new DataTable();
        DataTable dtTipSoc = new DataTable();
        DataTable dtPuestos = new DataTable();
        DataTable dtPuestosHabiles = new DataTable();
        DataTable dtForm = new DataTable();

        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[5, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlexCar = new string[10, 5];
        bool booSeEjecuto = false;
        bool booAgregando = false;

        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890-()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmManSociosPuestos()
        {
            InitializeComponent();
        }

        private void FrmManSociosPuestos_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }
        void CargarCombos()
        {
            DataTableCargar();

            //funDatos.ComboBoxCargarDataTable(CboTipDocIde, dtTipDocIde, "n_id", "c_des");
            //funDatos.ComboBoxCargarDataTable(CboDis, dtDistrito, "n_id", "c_des");
            //funDatos.ComboBoxCargarDataTable(CboSexo, DtSexo, "n_id", "c_des");
            //funDatos.ComboBoxCargarDataTable(CboTipSoc, dtTipSoc, "n_id", "c_des");
        }
        void ConfigurarFormulario()
        {
            this.Height = 587;
            this.Width = 930;
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";
            this.Text = dtForm.Rows[0]["c_titfor"].ToString();

            arrCabeceraFlexCar[0, 0] = "Nº Puesto";
            arrCabeceraFlexCar[0, 1] = "100";
            arrCabeceraFlexCar[0, 2] = "C";
            arrCabeceraFlexCar[0, 3] = "";
            arrCabeceraFlexCar[0, 4] = "";

            arrCabeceraFlexCar[1, 0] = "Nº Contrato";
            arrCabeceraFlexCar[1, 1] = "100";
            arrCabeceraFlexCar[1, 2] = "C";
            arrCabeceraFlexCar[1, 3] = "";
            arrCabeceraFlexCar[1, 4] = "";

            arrCabeceraFlexCar[2, 0] = "Fch. Inicio";
            arrCabeceraFlexCar[2, 1] = "80";
            arrCabeceraFlexCar[2, 2] = "F";
            arrCabeceraFlexCar[2, 3] = "dd/MM/yyyy";
            arrCabeceraFlexCar[2, 4] = "";

            arrCabeceraFlexCar[3, 0] = "Fch. Termino";
            arrCabeceraFlexCar[3, 1] = "80";
            arrCabeceraFlexCar[3, 2] = "F";
            arrCabeceraFlexCar[3, 3] = "dd/MM/yyyy";
            arrCabeceraFlexCar[3, 4] = "";

            arrCabeceraFlexCar[4, 0] = "Doc. Cobranza";
            arrCabeceraFlexCar[4, 1] = "150";
            arrCabeceraFlexCar[4, 2] = "C";
            arrCabeceraFlexCar[4, 3] = "";
            arrCabeceraFlexCar[4, 4] = "";

            arrCabeceraFlexCar[5, 0] = "Estado";
            arrCabeceraFlexCar[5, 1] = "80";
            arrCabeceraFlexCar[5, 2] = "C";
            arrCabeceraFlexCar[5, 3] = "";
            arrCabeceraFlexCar[5, 4] = "";

            arrCabeceraFlexCar[6, 0] = "idpue";
            arrCabeceraFlexCar[6, 1] = "0";
            arrCabeceraFlexCar[6, 2] = "N";
            arrCabeceraFlexCar[6, 3] = "";
            arrCabeceraFlexCar[6, 4] = "";

            arrCabeceraFlexCar[7, 0] = "idsoc";
            arrCabeceraFlexCar[7, 1] = "0";
            arrCabeceraFlexCar[7, 2] = "N";
            arrCabeceraFlexCar[7, 3] = "";
            arrCabeceraFlexCar[7, 4] = "";

            arrCabeceraFlexCar[8, 0] = "idreg";
            arrCabeceraFlexCar[8, 1] = "0";
            arrCabeceraFlexCar[8, 2] = "N";
            arrCabeceraFlexCar[8, 3] = "";
            arrCabeceraFlexCar[8, 4] = "";

            arrCabeceraFlexCar[9, 0] = "Fch Termino Ctto.";
            arrCabeceraFlexCar[9, 1] = "80";
            arrCabeceraFlexCar[9, 2] = "F";
            arrCabeceraFlexCar[9, 3] = "dd/MM/yy";
            arrCabeceraFlexCar[9, 4] = "";

            funFlex.FlexMostrarDatos(FgCtto, arrCabeceraFlexCar, dtRegistros, 2, false);
        }
        void DataTableCargar()
        {
            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(50);

            objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID);
            objRegistros.TraerPuestosHabiles(STU_SISTEMA.EMPRESAID);
            dtPuestosHabiles = objRegistros.dtPuestos;

            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(50, ref arrCabeceraDg1);

            objTipDoc.mysConec = mysConec;
            dtTipDocIde = objTipDoc.Listar();

            objSocios.mysConec = mysConec;
            dtSocios = objSocios.Listar(STU_SISTEMA.EMPRESAID);

            objTipSoc.mysConec = mysConec;
            dtTipSoc = objTipSoc.Listar(STU_SISTEMA.EMPRESAID);

            objPuestos.mysConec = mysConec;
            dtPuestos = objPuestos.Listar(STU_SISTEMA.EMPRESAID);
            
            objDocCom.mysConec = mysConec;
            dtDocCom = objDocCom.Listar();
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
            LblIdSocio.Text = n_IdRegistro.ToString();
            // ID DEL REGISTRO ES EL ID DEL SOCIOS MENDIANTE EL CUAL SE REALIZA LA BUSQUEDA
            string c_dato = "";
            DataTable dtResult = new DataTable();

            FgCtto.Rows.Count = 2;
            objRegistros.mysConec = mysConec;

            objRegistros.TraerRegistro(n_IdRegistro);
            lstSocPue = objRegistros.lstSociosPuestos;

            // MOSTRAMOS EL NOMBRE DEL SOCIO
            c_dato = n_IdRegistro.ToString();
            dtResult = funDatos.DataTableFiltrar(dtSocios, "n_id = " + c_dato + "");
            if (dtResult.Rows.Count != 0)
            { 
                TxtNomSoc.Text = dtResult.Rows[0]["c_apenom"].ToString();
                TxtNumDoc.Text = dtResult.Rows[0]["c_idenumdoc"].ToString();
                // MOSTRAMOS EL TIPO DE SOCIO
                c_dato = dtResult.Rows[0]["n_idtipsoc"].ToString();
                dtResult = funDatos.DataTableFiltrar(dtTipSoc, "n_id = " + c_dato + "");
                if (dtResult.Rows.Count != 0)
                {
                    TxtTipSoc.Text = dtResult.Rows[0]["c_des"].ToString();
                }
            }

            //MOSTRAMOS LA LISTA DE PUESTOS
            int n_row = 0;
            booAgregando = true;
            
            for (n_row = 0; n_row <= lstSocPue.Count - 1; n_row++)
            {
                FgCtto.Rows.Count = FgCtto.Rows.Count + 1;
                c_dato = lstSocPue[n_row].n_idpue.ToString();
                c_dato = funDatos.DataTableBuscar(dtPuestos, "n_id", "c_puesto", c_dato, "N").ToString();
                FgCtto.SetData(FgCtto.Rows.Count - 1, 1, c_dato);
                FgCtto.SetData(FgCtto.Rows.Count - 1, 2, lstSocPue[n_row].c_numctt.ToString());

                if (lstSocPue[n_row].d_fchini.ToString() != "")
                {
                    FgCtto.SetData(FgCtto.Rows.Count - 1, 3, lstSocPue[n_row].d_fchini);
                }
                if (lstSocPue[n_row].d_fchfin.ToString() != "")
                {
                    FgCtto.SetData(FgCtto.Rows.Count - 1, 4, lstSocPue[n_row].d_fchfin);
                }

                c_dato = lstSocPue[n_row].n_idtipdocemi.ToString();
                c_dato = funDatos.DataTableBuscar(dtDocCom, "n_id", "c_des", c_dato, "N").ToString();

                FgCtto.SetData(FgCtto.Rows.Count - 1, 5, c_dato);

                if (lstSocPue[n_row].n_activo == 1)
                {
                    c_dato = "ACTIVO";
                }
                else
                {
                    c_dato = "VENCIDO";
                }
                FgCtto.SetData(FgCtto.Rows.Count - 1, 6, c_dato);

                FgCtto.SetData(FgCtto.Rows.Count - 1, 7, lstSocPue[n_row].n_idpue.ToString());
                FgCtto.SetData(FgCtto.Rows.Count - 1, 8, lstSocPue[n_row].n_idsoc.ToString());
                FgCtto.SetData(FgCtto.Rows.Count - 1, 9, lstSocPue[n_row].n_id.ToString());

                if (lstSocPue[n_row].d_fchter.ToString() != "")
                {
                    FgCtto.SetData(FgCtto.Rows.Count - 1, 10, lstSocPue[n_row].d_fchter);
                }

            }
            booAgregando = false;
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
            
            objRegistros.TraerPuestosHabiles(STU_SISTEMA.EMPRESAID);
            dtPuestosHabiles = objRegistros.dtPuestos;

            TxtNumDoc.Focus();
        }
        void Blanquea()
        {
            TxtNomSoc.Text = "";
            TxtNumDoc.Text = "";
            TxtTipSoc.Text = "";
            FgCtto.Rows.Count = 2;
        }
        void Bloquea()
        {
            TxtNumDoc.Enabled = !TxtNumDoc.Enabled;
            CmdBusSoc.Enabled = !CmdBusSoc.Enabled;
            CmdAddCtt.Enabled = !CmdAddCtt.Enabled;
            CmdDelCtt.Enabled = !CmdDelCtt.Enabled;
            CmdTermCtto.Enabled = !CmdTermCtto.Enabled;
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

            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            
            objRegistros.TraerPuestosHabiles(STU_SISTEMA.EMPRESAID);
            dtPuestosHabiles = objRegistros.dtPuestos;

            TxtNumDoc.Focus();
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            //int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            //DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            //if (DialogResult.Yes == Rpta)
            //{
            //    if (objRegistros.Eliminar(intIdRegistro) == true)
            //    {
            //        booResult = true;
            //        MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            //        // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
            //        objRegistros.mysConec = mysConec;
            //        dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID);
            //        // MOSTRAMOS LOS DATOS EN LA GRILLA
            //        ListarItems();
            //    }
            //    else
            //    {
            //        MessageBox.Show("¡ No se pudo eliminar el registro por el siguiente motivo ! " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    }
            //}
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
                booResultado = objRegistros.Insertar(lstSocPue, Convert.ToInt32(LblIdSocio.Text));
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Insertar(lstSocPue, Convert.ToInt32(LblIdSocio.Text));
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ ¨Ha ocurrido un un problema, no se pudo guardar el registro ! Error Nº : " + objRegistros.IntErrorNumber.ToString() + " = " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

            return booResultado;
        }
        void AsignarEntidad()
        {
            if (n_QueHace == 1)
            {
            //    BE_ListaReg.n_id = 0;
            }
            else
            {
            //    BE_ListaReg.n_id = BE_Registro.n_id;
            }
            int n_row = 0;
            string c_dato = "";
            lstSocPue.Clear();
            
            for (n_row = 2; n_row <= FgCtto.Rows.Count - 1; n_row++)
            {
                BE_COO_SOCIOSPUESTOS entSocPue = new BE_COO_SOCIOSPUESTOS ();

                entSocPue.n_idemp = STU_SISTEMA.EMPRESAID;
                entSocPue.n_id = Convert.ToInt32(FgCtto.GetData(n_row, 9).ToString());
                entSocPue.n_idsoc = Convert.ToInt32(FgCtto.GetData(n_row, 8).ToString());
                entSocPue.n_idpue = Convert.ToInt32(FgCtto.GetData(n_row, 7).ToString());
                entSocPue.c_puesto = FgCtto.GetData(n_row, 1).ToString();
                entSocPue.c_numctt = FgCtto.GetData(n_row, 2).ToString();
                entSocPue.d_fchini = Convert.ToDateTime(FgCtto.GetData(n_row,3).ToString());
                entSocPue.d_fchfin = Convert.ToDateTime(FgCtto.GetData(n_row,4).ToString());
                if (FgCtto.GetData(n_row,6).ToString() == "ACTIVO")
                {
                    entSocPue.n_activo  = 1;
                }
                else
                {
                    entSocPue.n_activo  = 2;
                }
                c_dato =  FgCtto.GetData(n_row, 5).ToString();
                c_dato = funDatos.DataTableBuscar(dtDocCom, "c_des", "n_id", c_dato, "C").ToString();
                entSocPue.n_idtipdocemi = Convert.ToInt32(c_dato);
                if (funFunciones.NulosC(FgCtto.GetData(n_row, 10)) != "")
                { 
                    entSocPue.d_fchter = Convert.ToDateTime(FgCtto.GetData(n_row, 10).ToString());
                }
                lstSocPue.Add(entSocPue);
            }
        }
        bool CamposOK()
        {
            bool booEstado = true;
            int n_row = 0;

            for (n_row = 2; n_row <= FgCtto.Rows.Count - 1; n_row++)
            {
                if (FgCtto.GetData(n_row,1).ToString() == "")
                {
                    MessageBox.Show("¡ No ha especificado el numero de puesto a contratar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booEstado = false;
                    FgCtto.Focus();
                    return booEstado;
                }
                if (FgCtto.GetData(n_row, 2).ToString() == "")
                {
                    MessageBox.Show("¡ No ha especificado numero de contrato !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booEstado = false;
                    FgCtto.Focus();
                    return booEstado;
                }
                if (funFunciones.NulosC(FgCtto.GetData(n_row, 3)) == "")
                {
                    MessageBox.Show("¡ No ha especificado la fecha de inicio del contrato !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booEstado = false;
                    FgCtto.Focus();
                    return booEstado;
                }
                if (funFunciones.NulosC(FgCtto.GetData(n_row, 4)) == "")
                {
                    MessageBox.Show("¡ No ha especificado la fecha de termino del contrato !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booEstado = false;
                    FgCtto.Focus();
                    return booEstado;
                }
                if (FgCtto.GetData(n_row, 5).ToString() == "")
                {
                    MessageBox.Show("¡ No ha especificado el tipo de documento de cobranza que se aplicara al contrato !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booEstado = false;
                    FgCtto.Focus();
                    return booEstado;
                }
            }

            return booEstado;
        }

        private void FrmManSociosPuestos_Activated(object sender, EventArgs e)
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
                objRegistros.mysConec = mysConec;
                dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID);
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

        private void Tab1_SelectedIndexChanging(object sender, C1.Win.C1Command.SelectedIndexChangingEventArgs e)
        {
            if (n_QueHace != 3) { return; }

            if (e.NewIndex == 1)
            {
                int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    VerRegistro(intIdRegistro);
                }
            }
        }

        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
            Tab1.SelectedIndex = 1;
            VerRegistro(intIdRegistro);
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

        private void FrmManSociosPuestos_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 110, this.Width - 38);
        }

        private void FgCtto_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (n_QueHace == 3)
            {
                FgCtto.AllowEditing = false; return;
            }

            if (booAgregando == true) { return; }

            if (FgCtto.Col == 1)
            {
                string c_dato = "";
                c_dato = FgCtto.GetData(FgCtto.Row, 1).ToString();
                c_dato = funDatos.DataTableBuscar(dtPuestos, "c_puesto", "n_id", c_dato, "C").ToString();
                FgCtto.SetData(FgCtto.Row, 7, c_dato);
                FgCtto.SetData(FgCtto.Row, 8, LblIdSocio.Text);
                FgCtto.Select(e.Row - 1, 2);
                return;
            }
            //if (FgCtto.Col == 2)
            //{
            //    MostrarUnidadMedida(e.Row, FgCtto.GetData(e.Row, 2).ToString());
            //    GenerarNumeroLote(e.Row, FgCtto.GetData(e.Row, 2).ToString());
            //    FgItems.Select(e.Row - 1, 3);
            //    return;
            //}
            //if (FgCtto.Col == 3)
            //{
            //    FgCtto.Select(e.Row - 1, 4);
            //    return;
            //}
            //if (FgCtto.Col == 4)
            //{
            //    FgCtto.Select(e.Row - 1, 5);
            //    return;
            //}
            //if (FgCtto.Col == 5)
            //{
            //    FgCtto.SetData(FgCtto.Row, 5, FgCtto.GetData(FgCtto.Row, 5).ToString().ToUpper());
            //    FgCtto.Select(e.Row - 1, 6);
            //    return;
            //}
            //if (FgCtto.Col == 6)
            //{
            //    booAgregando = true;
            //    FgCtto.SetData(e.Row, 7, "");
            //    FgCtto.SetData(e.Row, 8, "");

            //    FgCtto.Select(e.Row - 1, 7);
            //    booAgregando = false;
            //    return;
            //}
            //if (FgItems.Col == 7)
            //{
            //    booAgregando = true;
            //    FgCtto.SetData(e.Row, 8, "");
            //    FgCtto.Select(e.Row - 1, 8);
            //    booAgregando = false;
            //    return;
            //}
            //if (FgCtto.Col == 8)
            //{
            //    FgCtto.Select(e.Row - 1, 9);
            //    return;
            //}
            //if (FgCtto.Col == 9)
            //{
            //    FgCtto.Select(e.Row - 1, 10);
            //    return;
            //}
            //if (FgCtto.Col == 10)
            //{
            //    FgItems.Select(e.Row, 2);
            //    return;
            //}
        }

        private void FgCtto_EnterCell(object sender, EventArgs e)
        {
            if (FgCtto.Rows.Count == 2) { return; }
            if (n_QueHace == 3)
            {
                FgCtto.AllowEditing = false; return;
            }
            if (booAgregando == true) { return; }
            if (FgCtto.Row <= 1) { return; }

            DataTable dtResul = new DataTable();
            DataTable DtFiltro = new DataTable();

            if (FgCtto.Col == 1)
            {
                FgCtto.AllowEditing = true;
                funFlex.FlexColumnaCombo(FgCtto, dtPuestosHabiles, "c_puesto", 1);
            }
            if (FgCtto.Col == 2)
            {
                FgCtto.AllowEditing = true;
                //funFlex.FlexColumnaCombo(FgCtto, dtPuestosHabiles, "c_puesto", 1);
            }
            if (FgCtto.Col == 5)
            {
                FgCtto.AllowEditing = true;
                funFlex.FlexColumnaCombo(FgCtto, dtDocCom, "c_des", 5);
            }
            if (FgCtto.Col == 10)
            {
                FgCtto.AllowEditing = true;
                //funFlex.FlexColumnaCombo(FgCtto, dtDocCom, "c_des", 5);
            }
        }
        private void FgCtto_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            if (e.Col == 2)
            {
                if (!strCaracteres.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            if (e.Col == 3)
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            if (e.Col == 4)
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void FgCtto_RowColChange(object sender, EventArgs e)
        {
            if (n_QueHace == 3)
            {
                FgCtto.AllowEditing = false; return;
            }

            if (booAgregando == true) { return; }
        }
        private void CmdAddCtt_Click(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            
            if (funFunciones.NulosC(FgCtto.GetData(FgCtto.Rows.Count - 1, 1)) == "")
            {
                MessageBox.Show("¡ No ha especificado el puesto para el ultimo registro agregado !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            booAgregando = true;
            FgCtto.Rows.Count = FgCtto.Rows.Count + 1;
            string c_dato = "";
            c_dato = LblIdSocio.Text;
            FgCtto.SetData(FgCtto.Rows.Count - 1, 8, c_dato);     // ID DEL SOCIO
            FgCtto.SetData(FgCtto.Rows.Count - 1, 9, "0");        // ID DEL REGISTRO
            FgCtto.SetData(FgCtto.Rows.Count - 1, 6, "ACTIVO");   // ESTADO DEL REGISTRO POR DEFAULT ACTIVO CUANDO ES NUEVO
            booAgregando = false;
        }
        private void CmdDelCtt_Click(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (FgCtto.Rows.Count == 2) { return; }

            FgCtto.Rows.Remove(FgCtto.Row);
        }
        private void TxtNumDoc_Validated(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }

            if (TxtNumDoc.Text == "") { return; }
            
            DataTable dtResult = new DataTable();
            string c_dato = "";

            dtResult = funDatos.DataTableFiltrar(dtSocios, "c_idenumdoc = '" + TxtNumDoc.Text + "'");
            if (dtResult.Rows.Count != 0)
            {
                TxtNomSoc.Text = dtResult.Rows[0]["c_apenom"].ToString();
                LblIdSocio.Text = dtResult.Rows[0]["n_id"].ToString();
                c_dato = dtResult.Rows[0]["n_idtipsoc"].ToString();
                c_dato = funDatos.DataTableBuscar(dtTipSoc, "n_id", "c_des", c_dato, "N").ToString();
                TxtTipSoc.Text = c_dato;

                if (FgCtto.Rows.Count == 2)
                { 
                    CmdAddCtt_Click(sender, e);
                }
            }
        }
        private void TxtNumDoc_KeyPress(object sender, KeyPressEventArgs e)
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
        private void ToolImprimir_Click(object sender, EventArgs e)
        {
            CN_coo_sociospuestos objPuesto = new CN_coo_sociospuestos();
            objPuesto.mysConec = mysConec;
            objPuesto.STU_SISTEMA = STU_SISTEMA;
            objPuesto.ReporteSociosPuestos(STU_SISTEMA.EMPRESAID);
        }

        private void CmdTermCtto_Click(object sender, EventArgs e)
        {
            FgCtto.SetData(FgCtto.Row, 6, "VENCIDO");
            FgCtto.SetData(FgCtto.Row, 10, DateTime.Today.ToString("dd/MM/yy"));
        }

        private void CmdBusSoc_Click(object sender, EventArgs e)
        {
            DataTable dtResul = new DataTable();
            COD_Funciones xFunCoo = new COD_Funciones();
            xFunCoo.mysConec = mysConec;
            xFunCoo.STU_SISTEMA = STU_SISTEMA;
            dtResul = xFunCoo.BuscarSocios();
            if (funFunciones.Obj_ValidarError(xFunCoo.b_OcurrioError, xFunCoo.c_ErrorMensaje)==false)  { return; }
            if (dtResul == null) { return; }
            TxtNumDoc.Text = dtResul.Rows[0]["c_idenumdoc"].ToString();
            TxtNomSoc.Text = dtResul.Rows[0]["c_apenom"].ToString();
            TxtTipSoc.Text = dtResul.Rows[0]["c_destipsoc"].ToString();
            LblIdSocio.Text = dtResul.Rows[0]["n_id"].ToString();
        }
    }
}
