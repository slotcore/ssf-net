using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Estacionamiento;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Estacionamiento;
using SIAC_Negocio.Planilla;
using SIAC_Objetos.Sistema;
using SIAC_Negocio.Tesoreria;
using SIAC_Negocio.Maestros;
using SIAC_Negocio.Sunat;
using SIAC_Negocio.Almacen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIAC_NET_Estacionamientos.Formularios
{
    public partial class FrmManServicios : Form
    {
        // VARIABLES PUBLICAS
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_sys_empresalocal o_emploc = new CN_sys_empresalocal();
        CN_sun_unimed objUniMed = new CN_sun_unimed();
        CN_sun_tipexi objTipExi = new CN_sun_tipexi();
        CN_mae_familia objFam = new CN_mae_familia();
        CN_mae_clase objCla = new CN_mae_clase();
        CN_mae_subclase objSubCla = new CN_mae_subclase();
        CN_alm_inventario objItems = new CN_alm_inventario();
        CN_sun_tipmon objMoneda = new CN_sun_tipmon();
        
        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        Helper.Cls_Controles o_control = new Helper.Cls_Controles();

        // ENTIDADES LOCALES
        BE_EST_SERVICIOS BE_Registro = new BE_EST_SERVICIOS();

        // DATATABLE LOCALES
        DataTable dtLista = new DataTable();           // LISTARA TODOS LOS REGISTROS DEL MES ACTUAL
        DataTable dtRegistro = new DataTable();        // ALMACENARA LOS DATOS DEL REGISTRO SELECCIONADO
        DataTable dtemp = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtLocal = new DataTable();
        DataTable dttipexi = new DataTable();
        DataTable dtfam = new DataTable();
        DataTable dtcla = new DataTable();
        DataTable dtsubcla = new DataTable();
        DataTable dtunimed = new DataTable();
        DataTable dtMoneda = new DataTable();
        DataTable dtCajero = new DataTable();
        
        // VARIABLES LOCALES
        double n_tasaIGV = 1.18;
        int n_QueHace = 3;
        string C_IDLOCAL;
        
        // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[8, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[4, 5];

        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^-" + (char)8;
        public FrmManServicios()
        {
            InitializeComponent();
        }

        private void FrmManServicios_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }
        void CargarCombos()
        {
            DataTableCargar();

            funDatos.ComboBoxCargarDataTable(CboTipExi, dttipexi, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboFam, dtfam, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboUniMed, dtunimed, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboPlay, dtLocal, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMoneda, dtMoneda, "n_id", "c_des");
        }
        void ConfigurarFormulario()
        {
            this.Height = 500;
            this.Width = 834;

            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            string c_nomarc = @"C:\SSF-NET\estacionamiento.ini";
            //string N_IDEMPRESA = funGen.IniLeerSeccion(c_nomarc, "INFORMACION", "EMPRESA").ToString();
            C_IDLOCAL = funDatos.IniLeerSeccion(c_nomarc, "INFORMACION", "LOCAL").ToString();

            this.Text = dtForm.Rows[0]["c_titfor"].ToString();
        }
        void DataTableCargar()
        {
            CN_est_servicios objRegistros = new CN_est_servicios(STU_SISTEMA);
            objRegistros.STU_SISTEMA = STU_SISTEMA;
            objRegistros.Listar(STU_SISTEMA.EMPRESAID);
            dtLista = objRegistros.dtListar;
            objRegistros = null;

            CN_pla_empleados o_emp = new CN_pla_empleados(STU_SISTEMA);
            o_emp.STU_SISTEMA = STU_SISTEMA;
            o_emp.Listar(STU_SISTEMA.EMPRESAID);
            dtemp = o_emp.dtLista;
            o_emp = null;

            CN_est_cajeros o_caj = new CN_est_cajeros(STU_SISTEMA);
            o_caj.STU_SISTEMA = STU_SISTEMA;
            o_caj.Listar(STU_SISTEMA.EMPRESAID);
            dtCajero = o_caj.dtListar;
            o_caj = null;

            CN_est_conecta o_conec = new CN_est_conecta(STU_SISTEMA);
            objFormVis.mysConec = o_conec.mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(86, ref arrCabeceraDg1);

            objForm.mysConec = o_conec.mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(86);

            o_emploc.mysConec = o_conec.mysConec;
            dtLocal = o_emploc.Listar(STU_SISTEMA.EMPRESAID, 0);

            objUniMed.mysConec = o_conec.mysConec;
            dtunimed = objUniMed.Listar();

            objFam.mysConec = o_conec.mysConec;
            dtfam = objFam.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);
            dtfam = funDatos.DataTableFiltrar(dtfam, "(n_idtipexi = 23)");

            objCla.mysConec = o_conec.mysConec;
            dtcla = objCla.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);

            objSubCla.mysConec = o_conec.mysConec;
            dtsubcla = objSubCla.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);

            objTipExi.mysConec = o_conec.mysConec;
            dttipexi = objTipExi.Listar();

            objMoneda.mysConec = o_conec.mysConec;
            dtMoneda = objMoneda.Listar();
            o_conec = null;
        }
        void ListarItems()
        {
            if (STU_SISTEMA.USUARIOPERFIL == 10)
            {
                DataTable dtres = new DataTable();
                dtres = funDatos.DataTableFiltrar(dtCajero, "n_idloc = " + C_IDLOCAL + "");
                if (dtres.Rows.Count != 0)
                {
                    int n_idpla = Convert.ToInt16(funDatos.DataTableBuscar(dtres, "n_idusu", "n_idloc", STU_SISTEMA.USUARIOID.ToString(), "N"));

                    dtLista = funDatos.DataTableFiltrar(dtLista, "n_idpla = " + n_idpla.ToString() + "");
                }
            }

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

            CN_est_servicios objRegistros = new CN_est_servicios(STU_SISTEMA);
            objRegistros.STU_SISTEMA = STU_SISTEMA;
            objRegistros.TraerRegistro(n_IdRegistro);
            if (objRegistros.b_OcurrioError == false)
            {
                BE_Registro = objRegistros.e_Servicio;
                booAgregando = true;
                funDatos.ComboBoxCargarDataTable(CboCla, dtcla, "n_id", "c_des");
                funDatos.ComboBoxCargarDataTable(CboSubCla, dtsubcla, "n_id", "c_des");

                CboTipExi.SelectedValue = BE_Registro.n_idtipexi;
                CboFam.SelectedValue = BE_Registro.n_idfan;
                CboCla.SelectedValue = BE_Registro.n_idcla;
                CboSubCla.SelectedValue = BE_Registro.n_idsubcla;
                CboUniMed.SelectedValue = BE_Registro.n_idunimed;
                CboMoneda.SelectedValue = BE_Registro.n_idmon;
                CboPlay.SelectedValue = BE_Registro.n_idpla;
                TxtDesc.Text = BE_Registro.c_des;
                TxtCodPro.Text = BE_Registro.c_cod;
                booAgregando = false;

                if (BE_Registro.n_pagser == 2)
                {
                    OptSiPagSer.Checked = true;
                }
                else
                {
                    OptNoPagSer.Checked = true; 
                }

                if (BE_Registro.n_idcal == 2)
                {
                    OptSiCalImp.Checked = true;
                }
                else
                {
                    OptNoCalImp.Checked = true;
                }

                TxtPrecio.Text = BE_Registro.n_impbru.ToString("0.000000");
                TxtPrecioIGV.Text = BE_Registro.n_imptot.ToString("0.00");

                TxtHorIni.Text = BE_Registro.c_horini;
                TxtHorFin.Text = BE_Registro.c_horfin;

                if (BE_Registro.n_numhorser == 0)
                {
                    TxtNumHor.Text = "";
                }
                else
                { 
                    TxtNumHor.Text = BE_Registro.n_numhorser.ToString();
                }

                if (BE_Registro.n_aplfra == 1)
                {
                    OptAplNoMedPag.Checked = true;
                }
                else
                {
                    OptAplSiMedPag.Checked = true;
                }

                if (BE_Registro.n_apltolmedpag == 1)
                {
                    OptTolMedPagNo.Checked = true;
                }
                else
                {
                    OptTolMedPagSi.Checked = true;
                }
            }
            objRegistros = null;
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
            CboTipExi.SelectedValue = 23;
            CboUniMed.SelectedValue = 59;

            OptSiPagSer.Checked = true;
            OptAplNoMedPag.Checked = true;
            OptNoCalImp.Checked = true;
            CboTipExi.Focus();
        }
        void Blanquea()
        {
            booAgregando = true;
            CboTipExi.SelectedValue = 0;
            CboFam.SelectedValue = 0;
            CboCla.SelectedValue = 0;
            CboSubCla.SelectedValue = 0;
            CboUniMed.SelectedValue = 0;
            CboMoneda.SelectedValue = 0;
            CboPlay.SelectedValue = 0;
            TxtDesc.Text = "";
            TxtCodPro.Text = "";
            TxtPrecio.Text = "";
            TxtPrecioIGV.Text = "";
            booAgregando = false;
            
            TxtHorIni.Text = "";
            TxtHorFin.Text = "";

            TxtNumHor.Text = "";
        }
        void Bloquea()
        {
            //CboTipExi.Enabled = !CboTipExi.Enabled;
            CboFam.Enabled = !CboFam.Enabled;
            CboCla.Enabled = !CboCla.Enabled;
            CboSubCla.Enabled = !CboSubCla.Enabled;
            //CboUniMed.Enabled = !CboUniMed.Enabled;
            CboMoneda.Enabled = !CboMoneda.Enabled;
            CboPlay.Enabled = !CboPlay.Enabled;
            TxtDesc.Enabled = !TxtDesc.Enabled;
            //TxtCodPro.Enabled = !TxtCodPro.Enabled;
            TxtPrecio.Enabled = !TxtPrecio.Enabled;
            
            OptSiPagSer.Enabled = !OptSiPagSer.Enabled;
            OptNoPagSer.Enabled = !OptNoPagSer.Enabled;

            OptSiCalImp.Enabled = !OptSiCalImp.Enabled;
            OptNoCalImp.Enabled = !OptNoCalImp.Enabled;

            TxtHorIni.Enabled = !TxtHorIni.Enabled;
            TxtHorFin.Enabled = !TxtHorFin.Enabled;

            TxtNumHor.Enabled = !TxtNumHor.Enabled;

            OptAplNoMedPag.Enabled = !OptAplNoMedPag.Enabled;
            OptAplSiMedPag.Enabled = !OptAplSiMedPag.Enabled;
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

            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            CboTipExi.Focus();
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                CN_est_servicios objRegistros = new CN_est_servicios(STU_SISTEMA);
                objRegistros.STU_SISTEMA = STU_SISTEMA;
                if (objRegistros.Eliminar(intIdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objRegistros.Listar(STU_SISTEMA.EMPRESAID);
                    dtLista = objRegistros.dtListar;
                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
                }
                else
                {
                    MessageBox.Show("¡ No se pudo eliminar el registro por el siguiente motivo ! " + objRegistros.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
                objRegistros = null;
            }
            return booResult;
        }
        void Cancelar()
        {
            n_QueHace = 3;
            ActivarTool();
            Bloquea();
            TxtPrecio.Enabled = true;                         // LO HABILITAMNOS PARA QUE LA FUNCION BLOQUEA LO INABILITE
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

            CN_est_servicios objRegistros = new CN_est_servicios(STU_SISTEMA);
            objRegistros.STU_SISTEMA = STU_SISTEMA;
            if (n_QueHace == 1)
            {
                booResultado = objRegistros.Insertar(BE_Registro);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Actualizar(BE_Registro);
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ ¨Ha ocurrido un un problema, no se pudo guardar el registro ! Error Nº : " + objRegistros.n_ErrorNumber.ToString() + " = " + objRegistros.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            objRegistros = null;
            return booResultado;
        }
        void AsignarEntidad()
        {
            int n_id = 0;

            if (n_QueHace == 1)
            {
                n_id = 0;
            }
            else
            {
                n_id = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
            }
            
            BE_Registro.n_idemp = STU_SISTEMA.EMPRESAID;
            BE_Registro.n_id = n_id;
            BE_Registro.n_idpla = Convert.ToInt16(CboPlay.SelectedValue);
            BE_Registro.c_des = TxtDesc.Text;
            BE_Registro.c_cod = TxtCodPro.Text;
            //BE_Registro.n_idunimed = Convert.ToInt16(CboUniMed.SelectedValue);
            BE_Registro.n_idunimed = 0;
            BE_Registro.n_impbru = Convert.ToDouble(funFunciones.NulosN(TxtPrecio.Text));
            BE_Registro.n_imptot = Convert.ToDouble(funFunciones.NulosN(TxtPrecioIGV.Text));
            BE_Registro.n_idmon = Convert.ToInt16(CboMoneda.SelectedValue);
            BE_Registro.n_idfan = Convert.ToInt16(CboFam.SelectedValue);
            BE_Registro.n_idcla = Convert.ToInt16(CboCla.SelectedValue);
            BE_Registro.n_idsubcla = Convert.ToInt16(CboSubCla.SelectedValue);
            BE_Registro.n_idtipexi = Convert.ToInt16(CboTipExi.SelectedValue);

            if (OptSiPagSer.Checked == true) { BE_Registro.n_pagser = 2; }
            if (OptNoPagSer.Checked == true) { BE_Registro.n_pagser = 1; }

            if (OptSiCalImp.Checked == true) { BE_Registro.n_idcal = 2; }
            if (OptNoCalImp.Checked == true) { BE_Registro.n_idcal = 1; }

            if (funFunciones.NulosC(TxtHorIni.Text) == ":")
            {
                BE_Registro.c_horini = "";
            }
            else
            { 
                BE_Registro.c_horini = TxtHorIni.Text;
            }

            if (funFunciones.NulosC(TxtHorFin.Text) == ":")
            { 
                BE_Registro.c_horfin ="";
            }
            else
            { 
                BE_Registro.c_horfin = TxtHorFin.Text;
            }

            if (TxtNumHor.Text == "")
            {
                BE_Registro.n_numhorser = 0;
            }
            else
            {
                BE_Registro.n_numhorser = Convert.ToInt16(TxtNumHor.Text);
            }

            if (OptAplSiMedPag.Checked == true)
            {
                BE_Registro.n_aplfra = 2;
            }
            else
            {
                BE_Registro.n_aplfra = 1;
            }
            
            if (OptTolMedPagSi.Checked == true)
            {
                BE_Registro.n_apltolmedpag = 2;
            }
            else
            {
                BE_Registro.n_apltolmedpag = 1;
            }
        }
        string GeneraCodigoProducto()
        {
            DataTable dtFiltrar = new DataTable();
            string c_CadenaFiltro;
            string c_preftipexi;
            string c_preffam;
            string c_prefcla;
            string c_prefsubcla;
            string c_numero;
            string c_codpro;

            c_numero = "";

            c_CadenaFiltro = "n_id = " + CboTipExi.SelectedValue.ToString() + "";
                dtFiltrar = funDatos.DataTableFiltrar(dttipexi, c_CadenaFiltro);
                c_preftipexi = dtFiltrar.Rows[0]["c_pre"].ToString();

                c_CadenaFiltro = "n_id = " + CboFam.SelectedValue.ToString() + "";
                dtFiltrar = funDatos.DataTableFiltrar(dtfam, c_CadenaFiltro);
                c_preffam = dtFiltrar.Rows[0]["c_pre"].ToString();

                c_CadenaFiltro = "n_id = " + CboCla.SelectedValue.ToString() + "";
                dtFiltrar = funDatos.DataTableFiltrar(dtcla, c_CadenaFiltro);
                c_prefcla = dtFiltrar.Rows[0]["c_pre"].ToString();

                c_CadenaFiltro = "n_id = " + CboSubCla.SelectedValue.ToString() + "";
                dtFiltrar = funDatos.DataTableFiltrar(dtsubcla, c_CadenaFiltro);
                c_prefsubcla = dtFiltrar.Rows[0]["c_pre"].ToString();

                CN_est_conecta o_conec = new CN_est_conecta(STU_SISTEMA);
                objItems = new CN_alm_inventario();    
                objItems.mysConec = o_conec.mysConec;
                dtFiltrar = objItems.ObtenerCodigo(STU_SISTEMA.EMPRESAID, Convert.ToInt16(CboTipExi.SelectedValue), Convert.ToInt16(CboFam.SelectedValue), Convert.ToInt16(CboCla.SelectedValue), Convert.ToInt16(CboSubCla.SelectedValue));
                objItems = null;
                o_conec = null;

                if (dtFiltrar.Rows.Count != 0)
                {
                    if (Convert.ToInt16(dtFiltrar.Rows[0]["c_numite"]) != 0)
                    {
                        c_numero = "000" + (Convert.ToInt16(dtFiltrar.Rows[0]["c_numite"]) + 1).ToString();
                        c_numero = c_numero.Substring(c_numero.Length - 3, 3);
                    }
                    else
                    {
                        c_codpro = "001";
                    }
                }
                else
                {
                    c_numero = "001";
                }

                c_codpro = c_preftipexi + c_preffam + c_prefcla + c_prefsubcla + c_numero;
            

            return c_codpro;
        }
        bool CamposOK()
        {
            bool booEstado = true;

            TxtCodPro.Text = GeneraCodigoProducto();

            if (Convert.ToInt16(CboTipExi.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de existencia !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboTipExi.Focus();
                return booEstado;
            }

            if (Convert.ToInt16(CboFam.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la familia del servicio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboFam.Focus();
                return booEstado;
            }

            if (Convert.ToInt16(CboCla.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la clase del servicio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboCla.Focus();
                return booEstado;
            }
            if (Convert.ToInt16(CboSubCla.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la sub clase del servicio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboSubCla.Focus();
                return booEstado;
            }
            //if (Convert.ToInt16(CboUniMed.SelectedValue) == 0)
            //{
            //    MessageBox.Show("¡ No ha especificado la unidad de medida del servicio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    booEstado = false;
            //    CboUniMed.Focus();
            //    return booEstado;
            //} 
            if (Convert.ToInt16(CboMoneda.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la moneda del servicio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboMoneda.Focus();
                return booEstado;
            }
            if (Convert.ToInt16(CboPlay.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la playa de venta del servicio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboPlay.Focus();
                return booEstado;
            }
            if (funFunciones.NulosC(TxtCodPro.Text) == "")
            {
                MessageBox.Show("¡ No ha especificado el codigo del servicio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtCodPro.Focus();
                return booEstado;
            }
            if (funFunciones.NulosC(TxtDesc.Text) == "")
            {
                MessageBox.Show("¡ No ha especificado la descripcion del servicio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtDesc.Focus();
                return booEstado;
            }
            if (OptSiPagSer.Checked == true)
            { 
                if (funFunciones.NulosC(TxtPrecio.Text) == "")
                {
                    MessageBox.Show("¡ No ha especificado el precio bruto del servicio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booEstado = false;
                    TxtPrecio.Focus();
                    return booEstado;
                }
            }
            return booEstado;
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
                CN_est_servicios objRegistros = new CN_est_servicios(STU_SISTEMA);
                objRegistros.STU_SISTEMA = STU_SISTEMA;

                objRegistros.Listar(STU_SISTEMA.EMPRESAID);
                dtLista = objRegistros.dtListar;
                objRegistros = null;

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
            objFormVis = null;
            this.Close();
        }
        private void FrmManServicios_Activated(object sender, EventArgs e)
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
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
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

            if (e.NewIndex == 1)
            {
                int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    booAgregando = true;
                    VerRegistro(intIdRegistro);
                    booAgregando = false;
                }
            }
        }
        private void label8_Click(object sender, EventArgs e)
        {

        }
        private void CboFam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            if (n_QueHace == 3) { return; }

            booAgregando = true;
            DataTable dtResul = new DataTable();
            dtResul = funDatos.DataTableFiltrar(dtcla, "(n_idtipexi = 23) AND (n_idfam = " + CboFam.SelectedValue.ToString() + " )");
            funDatos.ComboBoxCargarDataTable(CboCla, dtResul, "n_id", "c_des");
            booAgregando = false;
        }
        private void CboCla_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            if (n_QueHace == 3) { return; }

            DataTable dtResul = new DataTable();
            dtResul = funDatos.DataTableFiltrar(dtsubcla, "(n_idtipexi = 23)");
            dtResul = funDatos.DataTableFiltrar(dtResul, "(n_idfam = " + CboFam.SelectedValue.ToString() + " ) AND (n_idcla = " + CboCla.SelectedValue.ToString() + ")");
            funDatos.ComboBoxCargarDataTable(CboSubCla, dtResul, "n_id", "c_des");
        }
        private void CboTipExi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void CboFam_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void CboCla_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void CboSubCla_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtDesc_KeyPress(object sender, KeyPressEventArgs e)
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
        private void CboUniMed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void CboMoneda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtPrecio_KeyPress(object sender, KeyPressEventArgs e)
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
        private void CboPlay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtCodPro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtPrecioIGV_KeyPress(object sender, KeyPressEventArgs e)
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
        private void CboTipExi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            if (n_QueHace == 3) { return; }

            booAgregando = true;
            DataTable dtResul = new DataTable();
            dtResul = funDatos.DataTableFiltrar(dtfam, "(n_idtipexi = " + CboTipExi.SelectedValue.ToString() + " )");
            funDatos.ComboBoxCargarDataTable(CboFam, dtResul, "n_id", "c_des");
            booAgregando = false;
        }
        private void TxtPrecio_Validated(object sender, EventArgs e)
        {
            if (Convert.ToDouble(funFunciones.NulosN(TxtPrecio.Text)) != 0)
            {
                TxtPrecioIGV.Text = (Convert.ToDouble(funFunciones.NulosN(TxtPrecio.Text)) * n_tasaIGV).ToString("0.00");
            }
        }
        private void TxtHorIni_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtHorFin_KeyPress(object sender, KeyPressEventArgs e)
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
        private void OptNoPagSer_CheckedChanged(object sender, EventArgs e)
        {
            if (OptNoPagSer.Checked == true)
            {
                TxtPrecio.Text = "";
                TxtPrecioIGV.Text = "";
                TxtPrecio.Enabled = false;
            }
        }
        private void OptSiPagSer_CheckedChanged(object sender, EventArgs e)
        {
            if (OptSiPagSer.Checked == true)
            {
                TxtPrecio.Text = "";
                TxtPrecioIGV.Text = "";
                TxtPrecio.Enabled = true;
            }
        }
        private void ToolHerramientas_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void TxtHorIni_Validated(object sender, EventArgs e)
        {
            if (funFunciones.NulosC(TxtHorIni.Text) != ":")
            {
                TxtNumHor.Text = "";
            }
        }
        private void TxtHorFin_Validated(object sender, EventArgs e)
        {
            if (funFunciones.NulosC(TxtHorFin.Text) != ":")
            {
                TxtNumHor.Text = "";
            }
        }
        private void TxtNumHor_KeyPress(object sender, KeyPressEventArgs e)
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
        private void TxtNumHor_Validated(object sender, EventArgs e)
        {
            if (funFunciones.NulosC(TxtNumHor.Text) != "")
            {
                TxtHorIni.Text = "";
                TxtHorFin.Text = "";
            }
        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void OptAplNoMedPag_CheckedChanged(object sender, EventArgs e)
        {
            if (OptAplNoMedPag.Checked == true)
            { 
                groupBox3.Enabled = false;
            }
        }

        private void OptAplSiMedPag_CheckedChanged(object sender, EventArgs e)
        {
            if (OptAplNoMedPag.Checked == true)
            {
                groupBox3.Enabled = true;
            }
        }
    }
}

