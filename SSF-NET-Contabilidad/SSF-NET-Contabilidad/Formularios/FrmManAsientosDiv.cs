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
    public partial class FrmManAsientosDiv : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        SIAC_Objetos.Funciones obj_fungen = new SIAC_Objetos.Funciones();
        CN_con_proviciones objRegistros = new CN_con_proviciones();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_con_pc objPlaCue = new CN_con_pc();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_sun_tipdoccom objTipDoc = new CN_sun_tipdoccom();
        CN_sun_tipmon objMon = new CN_sun_tipmon();
        CN_sun_libro objLib = new CN_sun_libro();
        CN_sun_librosub objSubLib = new CN_sun_librosub();
        CN_con_tc objTc = new CN_con_tc();
        CN_mae_meses objMes = new CN_mae_meses();
        CN_sun_tipcam ObjTC = new CN_sun_tipcam();
        
        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Helper.Comunes.Funciones funFunciones = new Helper.Comunes.Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        SIAC_Objetos.Funciones objFunciones = new SIAC_Objetos.Funciones();

        DataTable dtLista = new DataTable();
        DataTable dtPlaCue = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtLibro = new DataTable();
        DataTable dtSubLibro = new DataTable();
        DataTable dtMoneda = new DataTable();
        DataTable dtTipDoc = new DataTable();
        //DataTable dtTc = new DataTable();
        DataTable dtMes = new DataTable();
        DataTable dtTC = new DataTable();
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
        public FrmManAsientosDiv()
        {
            InitializeComponent();
        }

        private void FrmManAsientosDiv_Load(object sender, EventArgs e)
        {
            CargarCombos();
            ConfigurarFormulario();
        }
        void CargarCombos()
        {
            DataTableCargar();
            booAgregando = true;
            funDatos.ComboBoxCargarDataTable(CboLib, dtLibro, "n_id", "c_des");
            //funDatos.ComboBoxCargarDataTable(CboSubLib, dtSubLibro, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMon, dtMoneda, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipDoc, dtTipDoc, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMes, "n_id", "c_des");

            booAgregando = false;
        }
        void ConfigurarFormulario()
        {
            this.Height = 579;
            this.Width = 955;

            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            this.Text = dtForm.Rows[0]["c_titfor"].ToString();
            
            arrCabeceraFlex1[0, 0] = "Nº Cuenta";
            arrCabeceraFlex1[0, 1] = "80";
            arrCabeceraFlex1[0, 2] = "S";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_tipexides";

            arrCabeceraFlex1[1, 0] = "Descripcion";
            arrCabeceraFlex1[1, 1] = "340";
            arrCabeceraFlex1[1, 2] = "S";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_desunimed";

            arrCabeceraFlex1[2, 0] = "Debe S/.";
            arrCabeceraFlex1[2, 1] = "80";
            arrCabeceraFlex1[2, 2] = "D";
            arrCabeceraFlex1[2, 3] = "0.00";
            arrCabeceraFlex1[2, 4] = "c_itedes";

            arrCabeceraFlex1[3, 0] = "Haber S/.";
            arrCabeceraFlex1[3, 1] = "80";
            arrCabeceraFlex1[3, 2] = "D";
            arrCabeceraFlex1[3, 3] = "0.00";
            arrCabeceraFlex1[3, 4] = "c_itepredes";

            arrCabeceraFlex1[4, 0] = "Debe $";
            arrCabeceraFlex1[4, 1] = "80";
            arrCabeceraFlex1[4, 2] = "D";
            arrCabeceraFlex1[4, 3] = "0.00";
            arrCabeceraFlex1[4, 4] = "n_can";

            arrCabeceraFlex1[5, 0] = "Haber $";
            arrCabeceraFlex1[5, 1] = "80";
            arrCabeceraFlex1[5, 2] = "D";
            arrCabeceraFlex1[5, 3] = "0.00";
            arrCabeceraFlex1[5, 4] = "n_numlot";

            arrCabeceraFlex1[6, 0] = "n_idcuecon";
            arrCabeceraFlex1[6, 1] = "0";
            arrCabeceraFlex1[6, 2] = "N";
            arrCabeceraFlex1[6, 3] = "0.00";
            arrCabeceraFlex1[6, 4] = "n_numlot";

            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtLista, 2, false);
            FgItems.Rows.Count = FgItems.Rows.Count + 30;
        }
        void DataTableCargar()
        {
            objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);
            dtLista = objRegistros.dtLista;

            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(63, ref arrCabeceraDg1);

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(63);

            objPlaCue.mysConec = mysConec;
            objPlaCue.Listar(STU_SISTEMA.EMPRESAID);
            dtPlaCue = objPlaCue.dtLista;

            //objTc.mysConec = mysConec;
            //objTc.Listartcano(151, STU_SISTEMA.ANOTRABAJO.ToString());
            //dtTc = objTc.dtLista;
            ObjTC.mysConec = mysConec;
            dtTC = ObjTC.Listartcano(151, STU_SISTEMA.ANOTRABAJO.ToString());

            objTipDoc.mysConec = mysConec;
            dtTipDoc = objTipDoc.Listar();
            //dtTipDoc = funDatos.DataTableFiltrar(dtTipDoc, "n_id IN (2,4,5,11)");

            objMon.mysConec = mysConec;
            dtMoneda = objMon.Listar();
            dtMoneda = funDatos.DataTableFiltrar(dtMoneda, "n_id IN(115, 151)");

            objLib.mysConec = mysConec;
            objLib.Listar();
            dtLibro = objLib.dtLista;

            objSubLib.mysConec = mysConec;
            objSubLib.Listar();
            dtSubLibro = objSubLib.dtLista;

            objMes.mysConec = mysConec;
            dtMes = objMes.Listar();
        }
        void ListarItems()
        {
            // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
            MostrarEstadoMes(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO);
            objRegistros.mysConec = mysConec;
            objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);
            dtLista = objRegistros.dtLista;

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
            int n_row = 0;
            string c_dato = "";
            double n_valor = 0;
            objRegistros.mysConec = mysConec;
            objRegistros.TraerRegistro(n_IdRegistro);
            e_Proviciones = objRegistros.e_Provicion;
            l_ProvicionesDet = objRegistros.l_ProvicionDet;

            CboLib.SelectedValue = e_Proviciones.n_idlib;
            CboSubLib.SelectedValue = e_Proviciones.n_idsublib;
            CboMon.SelectedValue = e_Proviciones.n_idmon;
            TxtFchDoc.Text = e_Proviciones.d_fchdoc.ToString("dd/MM/yyyy");
            CboTipDoc.SelectedValue = e_Proviciones.n_idtipdoc;
            TxtNumSer.Text = e_Proviciones.c_numser;
            TxtNumDoc.Text = e_Proviciones.c_numdoc;
            TxtGlosa.Text = e_Proviciones.c_glosa;
            LblNumRegCon.Text = e_Proviciones.c_numreg;
            LblTc.Text = e_Proviciones.n_tc.ToString("0.000");

            FgItems.Rows.Count = 2;
            booAgregando = true;
            for (n_row = 0; n_row <= l_ProvicionesDet.Count - 1; n_row++)
            {
                FgItems.Rows.Count = FgItems.Rows.Count + 1;

                c_dato = funDatos.DataTableBuscar(dtPlaCue, "n_id", "c_cuecon", l_ProvicionesDet[n_row].n_idcuecon.ToString(), "N").ToString();
                FgItems.SetData(FgItems.Rows.Count - 1, 1, c_dato);

                c_dato = funDatos.DataTableBuscar(dtPlaCue, "n_id", "c_des", l_ProvicionesDet[n_row].n_idcuecon.ToString(), "N").ToString();
                FgItems.SetData(FgItems.Rows.Count - 1, 2, c_dato);

                //if (e_Proviciones.n_idmon == 115)
                //{
                    // MOSTRANOS EL DATO EN SOLES Y HACEMOS LA CONVERCION A DOLARES
                    if (l_ProvicionesDet[n_row].n_tipo == 1)
                    {
                        FgItems.SetData(FgItems.Rows.Count - 1, 3, l_ProvicionesDet[n_row].n_impsol.ToString("0.00"));
                        FgItems.SetData(FgItems.Rows.Count - 1, 4, "0.00");

                        //n_valor =(l_ProvicionesDet[n_row].n_importe / e_Proviciones.n_tc);
                        FgItems.SetData(FgItems.Rows.Count - 1, 5, l_ProvicionesDet[n_row].n_impdol.ToString("0.00"));
                        FgItems.SetData(FgItems.Rows.Count - 1, 6, "0.00");
                    }
                    else 
                    {
                        FgItems.SetData(FgItems.Rows.Count - 1, 3, "0.00");
                        FgItems.SetData(FgItems.Rows.Count - 1, 4, l_ProvicionesDet[n_row].n_impsol.ToString("0.00"));

                        //n_valor = (l_ProvicionesDet[n_row].n_importe / e_Proviciones.n_tc);
                        FgItems.SetData(FgItems.Rows.Count - 1, 5, "0.00");
                        FgItems.SetData(FgItems.Rows.Count - 1, 6, l_ProvicionesDet[n_row].n_impdol.ToString("0.00"));
                    }
                //}
                //else
                //{
                //    // MOSTRANOS EL DATO EN DOLARES Y HACEMOS LA CONVERCION A SOLES
                //    if (l_ProvicionesDet[n_row].n_tipo == 1)
                //    {
                //        FgItems.SetData(FgItems.Rows.Count - 1, 5, l_ProvicionesDet[n_row].n_importe.ToString("0.00"));
                //        FgItems.SetData(FgItems.Rows.Count - 1, 6, "0.00");

                //        n_valor = (l_ProvicionesDet[n_row].n_importe / e_Proviciones.n_tc);
                //        FgItems.SetData(FgItems.Rows.Count - 1, 3, n_valor.ToString("0.00"));
                //        FgItems.SetData(FgItems.Rows.Count - 1, 4, "0.00");
                //    }
                //    else
                //    {
                //        FgItems.SetData(FgItems.Rows.Count - 1, 5, "0.00");
                //        FgItems.SetData(FgItems.Rows.Count - 1, 6, l_ProvicionesDet[n_row].n_importe.ToString("0.00"));

                //        n_valor = (l_ProvicionesDet[n_row].n_importe / e_Proviciones.n_tc);
                //        FgItems.SetData(FgItems.Rows.Count - 1, 3, "0.00");
                //        FgItems.SetData(FgItems.Rows.Count - 1, 4, n_valor.ToString("0.00"));
                //    }
                //}
            }
            booAgregando = false;
            SumarColumnas();
        }
        void Nuevo()
        {
            n_QueHace = 1;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();
            LblTitulo2.Text = "Agregando Nuevo Registro";
            FgItems.Cols[1].ComboList = "...";
            if (Convert.ToInt32(CboMeses.SelectedValue) == 0)
            {
                DataTable dttctem = new DataTable();
                ObjTC.mysConec = mysConec;
                string c_fch = "31/12/" + (STU_SISTEMA.ANOTRABAJO - 1).ToString();
                TxtFchDoc.Text = c_fch;
                int n_ano = Convert.ToDateTime(TxtFchDoc.Text).Year;
                dttctem = ObjTC.Listartcano(151, n_ano.ToString());
                LblTc.Text = objFunciones.ObtenerTC(dttctem, TxtFchDoc.Text).ToString("0.000");
            }
            else
            {
                LblTc.Text = objFunciones.ObtenerTC(dtTC, DateTime.Now.ToString("dd/MM/yyyy")).ToString("0.000");
            }
            CboMon.SelectedValue = 115;
            CboLib.SelectedValue = 5;

            booAgregando = true;
            CboSubLib.Enabled = true;
            DataTable dtResul = new DataTable();
            dtResul = funDatos.DataTableFiltrar(dtSubLibro, "n_idlib = " + Convert.ToInt32(CboLib.SelectedValue) + "");
            funDatos.ComboBoxCargarDataTable(CboSubLib, dtResul, "n_id", "c_des");
            booAgregando = false;

            Tab1.SelectedIndex = 1;
            CboLib.Focus();
        }
        void Blanquea()
        {
            CboLib.SelectedValue = 0;
            CboSubLib.SelectedValue = 0;
            CboMon.SelectedValue = 0;
            TxtFchDoc.Text = "";
            CboTipDoc.SelectedValue = 0;
            TxtNumSer.Text = "";
            TxtNumDoc.Text = "";
            TxtGlosa.Text = "";
            LblNumRegCon.Text = "";
            LblTc.Text = "";

            LblDebSol.Text = "";
            LblHabSol.Text = "";
            LblDebDol.Text = "";
            LblHabDol.Text = "";
            FgItems.Rows.Count = 2;
            FgItems.Rows.Count = 30;
        }
        void Bloquea()
        {
            CboLib.Enabled = !CboLib.Enabled;
            //CboSubLib.Enabled = !CboSubLib.Enabled;
            CboMon.Enabled = !CboMon.Enabled;
            TxtFchDoc.Enabled = !TxtFchDoc.Enabled;
            CboTipDoc.Enabled = !CboTipDoc.Enabled;
            TxtNumSer.Enabled = !TxtNumSer.Enabled;
            TxtNumDoc.Enabled = !TxtNumDoc.Enabled;
            TxtGlosa.Enabled = !TxtGlosa.Enabled;
            LblNumRegCon.Enabled = !LblNumRegCon.Enabled;
            LblTc.Enabled = !LblTc.Enabled;

            CmdDelIte.Enabled = !CmdDelIte.Enabled;
            CmdAddIte.Enabled = !CmdAddIte.Enabled;
        }
        void MostrarEstadoMes(int n_IdEmpresa, int n_IdMes)
        {
            obj_fungen.mysConec = mysConec;
            if (obj_fungen.EstadoPeriodo(n_IdEmpresa, n_IdMes, 11) == true)
            {
                ToolNuevo.Visible = false;
                ToolModificar.Visible = false;
                ToolEliminar.Visible = false;

                ToolGrabar.Visible = false;
                ToolCancelar.Visible = false;

                toolStripSeparator1.Visible = false;
                toolStripSeparator2.Visible = false;

                PicClos1.Visible = true;
                PicClos2.Visible = true;
            }
            else
            {
                ToolNuevo.Visible = true;
                ToolModificar.Visible = true;
                ToolEliminar.Visible = true;

                ToolGrabar.Visible = true;
                ToolCancelar.Visible = true;

                toolStripSeparator1.Visible = true;
                toolStripSeparator2.Visible = true;

                PicClos1.Visible = false;
                PicClos2.Visible = false;
            }
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

            int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            FgItems.Cols[1].ComboList = "...";

            booAgregando = true;
            CboSubLib.Enabled = true;
            DataTable dtResul = new DataTable();
            dtResul = funDatos.DataTableFiltrar(dtSubLibro, "n_idlib = " + Convert.ToInt32(CboLib.SelectedValue) + "");
            funDatos.ComboBoxCargarDataTable(CboSubLib, dtResul, "n_id", "c_des");

            booAgregando = false;

            Tab1.SelectedIndex = 1;
            CboLib.Focus();
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int n_IdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            e_Proviciones.n_id = n_IdRegistro;
            e_Proviciones.n_idlib = 5;
            e_Proviciones.n_ano = STU_SISTEMA.ANOTRABAJO;
            e_Proviciones.n_mes = STU_SISTEMA.MESTRABAJO;
            e_Proviciones.n_idemp = STU_SISTEMA.EMPRESAID;
            e_Proviciones.c_numreg = funDatos.DataTableBuscar(dtLista, "n_id", "c_numreg", n_IdRegistro.ToString(), "N").ToString();

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objRegistros.Eliminar(e_Proviciones) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

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
            objRegistros.mysConec = mysConec;
            if (n_QueHace == 1)
            {
                booResultado = objRegistros.Insertar(e_Proviciones, l_ProvicionesDet);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Actualizar(e_Proviciones, l_ProvicionesDet);
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ ¨Ha ocurrido un un problema, no se pudo guardar el registro ! Error Nº : " + objRegistros.n_ErrorNumber.ToString() + " = " + objRegistros.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

            return booResultado;
        }
        void AsignarEntidad()
        {
            if (n_QueHace == 1)
            {
                e_Proviciones.n_id = 0;
            }
            else
            {
                e_Proviciones.n_id = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            }

            //e_Proviciones.n_id
            e_Proviciones.n_idlib= Convert.ToInt32(CboLib.SelectedValue);
            e_Proviciones.n_idsublib = Convert.ToInt32(CboSubLib.SelectedValue);
            e_Proviciones.n_ano = STU_SISTEMA.ANOTRABAJO;
            e_Proviciones.n_mes = STU_SISTEMA.MESTRABAJO;
            e_Proviciones.d_fchreg = DateTime.Now;
            e_Proviciones.d_fchdoc = Convert.ToDateTime(TxtFchDoc.Text);
            e_Proviciones.n_idtipdoc = Convert.ToInt32(CboTipDoc.SelectedValue);
            e_Proviciones.c_numser = TxtNumSer.Text;
            e_Proviciones.c_numdoc = TxtNumDoc.Text;
            e_Proviciones.n_idcli = 0;
            e_Proviciones.c_nomcli = "";
            e_Proviciones.n_idmon = Convert.ToInt32(CboMon.SelectedValue);
            
            if (e_Proviciones.n_idmon == 115)
            {
                e_Proviciones.n_imp = Convert.ToDouble(LblDebSol.Text);
            }
            else
            {
                e_Proviciones.n_imp = Convert.ToDouble(LblDebDol.Text);
            }

            e_Proviciones.c_glosa = TxtGlosa.Text;
            e_Proviciones.c_numreg = LblNumRegCon.Text;
            e_Proviciones.n_tc = Convert.ToDouble(LblTc.Text);
            e_Proviciones.n_ajuste = 0;
            e_Proviciones.n_idemp = STU_SISTEMA.EMPRESAID;

            int n_row = 0;
            string c_dato = ""; 
            l_ProvicionesDet.Clear();

            for(n_row = 2; n_row <= FgItems.Rows.Count - 1; n_row++)
            {
                if (funFunciones.NulosC(FgItems.GetData(n_row, 1)).ToString() != "")
                { 
                    BE_CON_PROVICIONESDET e_Det = new BE_CON_PROVICIONESDET();

                    e_Det.n_idpro=0;

                    c_dato = funFunciones.NulosC(FgItems.GetData(n_row, 1)).ToString();
                    c_dato = funDatos.DataTableBuscar(dtPlaCue, "c_cuecon", "n_id", c_dato, "C").ToString();
                    e_Det.n_idcuecon = Convert.ToInt32(c_dato);

                    if (Convert.ToInt32(CboMon.SelectedValue) == 151)
                    {
                        c_dato = funFunciones.NulosN(FgItems.GetData(n_row, 5)).ToString();
                        if (Convert.ToDouble(funFunciones.NulosN(c_dato)) != 0)
                        {
                            e_Det.n_tipo = 1;
                            e_Det.n_impsol = Convert.ToDouble(funFunciones.NulosN(c_dato)) * Convert.ToDouble(LblTc.Text);
                            e_Det.n_impdol = Convert.ToDouble(funFunciones.NulosN(c_dato));
                        }
                        else
                        {
                            c_dato = funFunciones.NulosN(FgItems.GetData(n_row, 6)).ToString();
                            if (Convert.ToDouble(funFunciones.NulosN(c_dato)) != 0)
                            {
                                e_Det.n_tipo = 2;
                                e_Det.n_impsol = Convert.ToDouble(funFunciones.NulosN(c_dato)) * Convert.ToDouble(LblTc.Text);
                                e_Det.n_impdol = Convert.ToDouble(funFunciones.NulosN(c_dato));
                            }
                        }
                    }
                    else
                    {
                        c_dato = funFunciones.NulosN(FgItems.GetData(n_row, 3)).ToString();
                        if (Convert.ToDouble(funFunciones.NulosN(c_dato)) != 0)
                        {
                            e_Det.n_tipo = 1;
                            e_Det.n_impdol = Convert.ToDouble(funFunciones.NulosN(c_dato)) / Convert.ToDouble(LblTc.Text);
                            e_Det.n_impsol = Convert.ToDouble(funFunciones.NulosN(c_dato));
                        }
                        else
                        {
                            c_dato = funFunciones.NulosN(FgItems.GetData(n_row, 4)).ToString();
                            if (Convert.ToDouble(funFunciones.NulosN(c_dato)) != 0)
                            {
                                e_Det.n_tipo = 2;
                                e_Det.n_impdol = Convert.ToDouble(funFunciones.NulosN(c_dato)) / Convert.ToDouble(LblTc.Text);
                                e_Det.n_impsol = Convert.ToDouble(funFunciones.NulosN(c_dato));
                            }
                        }
                    }
                    l_ProvicionesDet.Add(e_Det);
                }
            }           
        }
        bool CamposOK()
        {
            bool booEstado = true;
            if (Convert.ToInt32(CboLib.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el libro !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboLib.Focus();
                return booEstado;
            }
            //if (Convert.ToInt32(CboSubLib.SelectedValue) == 0)
            //{
            //    MessageBox.Show("¡ No ha especificado el sub libro !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    booEstado = false;
            //    CboSubLib.Focus();
            //    return booEstado;
            //}
            if (Convert.ToInt32(CboMon.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado la moneda !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboMon.Focus();
                return booEstado;
            }
            if (TxtFchDoc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha del documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtFchDoc.Focus();
                return booEstado;
            }
            if (Convert.ToInt32(CboTipDoc.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboTipDoc.Focus();
                return booEstado;
            }
            if (TxtNumSer.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de serie del documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtNumSer.Focus();
                return booEstado;
            }
            if (TxtNumDoc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero del documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtNumDoc.Focus();
                return booEstado;
            }
            return booEstado;
        }
        private void FrmManAsientosDiv_Activated(object sender, EventArgs e)
        {
            if (booSeEjecuto == false)
            {
                booSeEjecuto = true;
                booAgregando = true;
                CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;
                booAgregando = false;
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
                // dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);
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
            objFormVis = null;
            this.Close();
        }
        private void Tab1_SelectedIndexChanging(object sender, C1.Win.C1Command.SelectedIndexChangingEventArgs e)
        {
            if (n_QueHace != 3) { return; }

            if (e.NewIndex == 1)
            {
                int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    VerRegistro(intIdRegistro);
                }
            }
        }
        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt32(DgLista.Columns["n_id"].CellValue(DgLista.Row).ToString());
            Tab1.SelectedIndex = 1;
            VerRegistro(intIdRegistro);
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
        private void FrmManAsientosDiv_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void FgItems_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (booAgregando == true) { return; }

            if (Convert.ToDouble(funFunciones.NulosN(LblTc.Text)) == 0)
            {
                MessageBox.Show("! No ha especificado el tipo de cambio ", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if (FgItems.Col == 1)
            {
                DataTable dtResult = new DataTable();
                dtResult = objPlaCue.BuscarCuenta(dtPlaCue);
                if (dtResult != null)
                {
                    FgItems.SetData(FgItems.Row,1, dtResult.Rows[0]["c_cuecon"].ToString());
                    FgItems.SetData(FgItems.Row, 2, dtResult.Rows[0]["c_des"].ToString());
                    FgItems.SetData(FgItems.Row, 7, dtResult.Rows[0]["n_id"].ToString());
                }
            }
        }
        private void FgItems_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (n_QueHace == 3)
            {
                FgItems.AllowEditing = false; return;
            }

            if (booAgregando == true) { return; }

            string c_dato = "";
            if (FgItems.Col == 3)
            {
                c_dato = FgItems.GetData(FgItems.Row, 3).ToString();
                c_dato = (Convert.ToDouble(c_dato) / Convert.ToDouble(LblTc.Text)).ToString("0.00");
                FgItems.SetData(FgItems.Row, 5, c_dato);
            }
            if (FgItems.Col == 4)
            {
                c_dato = FgItems.GetData(FgItems.Row, 4).ToString();
                c_dato = (Convert.ToDouble(c_dato) / Convert.ToDouble(LblTc.Text)).ToString("0.00");
                FgItems.SetData(FgItems.Row, 6, c_dato);
            }
            if (FgItems.Col == 5)
            {
                c_dato = FgItems.GetData(FgItems.Row, 5).ToString();
                c_dato = (Convert.ToDouble(c_dato) * Convert.ToDouble(LblTc.Text)).ToString("0.00");
                FgItems.SetData(FgItems.Row, 3, c_dato);
            }
            if (FgItems.Col == 6)
            {
                c_dato = FgItems.GetData(FgItems.Row, 6).ToString();
                c_dato = (Convert.ToDouble(c_dato) * Convert.ToDouble(LblTc.Text)).ToString("0.00");
                FgItems.SetData(FgItems.Row, 4, c_dato);
            }
            SumarColumnas();
        }
        void SumarColumnas()
        {

            LblDebSol.Text = funFlex.FlexSumarCol(FgItems,3,2,FgItems.Rows.Count-1).ToString("0.00");
            LblHabSol.Text = funFlex.FlexSumarCol(FgItems, 4, 2, FgItems.Rows.Count - 1).ToString("0.00");
            LblDebDol.Text = funFlex.FlexSumarCol(FgItems, 5, 2, FgItems.Rows.Count - 1).ToString("0.00");
            LblHabDol.Text = funFlex.FlexSumarCol(FgItems, 6, 2, FgItems.Rows.Count - 1).ToString("0.00");
        }
        private void FgItems_EnterCell(object sender, EventArgs e)
        {
            if (FgItems.Rows.Count == 2) { return; }
            if (n_QueHace == 3)
            {
                FgItems.AllowEditing = false; return;
            }
            if (booAgregando == true) { return; }

            FgItems.AllowEditing = false;

            if (FgItems.Col == 1)
            {
                FgItems.AllowEditing = true;
                return;
            }

            if (FgItems.Col >= 3)
            {
                if (Convert.ToInt32(CboMon.SelectedValue) == 151)
                {
                    if ((FgItems.Col == 3) || (FgItems.Col == 4))
                    {
                        FgItems.AllowEditing = false;
                    }
                    if ((FgItems.Col == 5) || (FgItems.Col == 6))
                    {
                        FgItems.AllowEditing = true;
                    }
                }
                else
                {
                    if ((FgItems.Col == 3) || (FgItems.Col == 4))
                    {
                        FgItems.AllowEditing = true;
                    }
                    if ((FgItems.Col == 5) || (FgItems.Col == 6))
                    {
                        FgItems.AllowEditing = false;
                    }
                }
            }
        }
        private void FgItems_KeyUp(object sender, KeyEventArgs e)
        {
            if (FgItems.Col == 1)
            {
                if (e.KeyCode.ToString() == "Delete")
                {
                    booAgregando = true;
                    FgItems.SetData(FgItems.Row, 1, "");
                    FgItems.SetData(FgItems.Row, 2, "");
                    FgItems.SetData(FgItems.Row, 3, "");
                    FgItems.SetData(FgItems.Row, 4, "");
                    FgItems.SetData(FgItems.Row, 5, "");
                    FgItems.SetData(FgItems.Row, 6, "");

                    //Calcularfila(FgItems.Row);
                    booAgregando = false;
                }
            }
            if (e.KeyCode.ToString() == "Insert")
            {
                if (funFunciones.NulosC(FgItems.GetData(FgItems.Rows.Count - 1, 1)) == "") { return; }
                FgItems.Rows.Count = FgItems.Rows.Count + 1;
            }

            if (e.KeyCode.ToString() == "Delete")
            {
                if (FgItems.Rows.Count <= 2) { return; }
                FgItems.RemoveItem(FgItems.Row);
            }
        }
        private void FgItems_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            if ((e.Col == 1)  || (e.Col == 3) || (e.Col == 4) || (e.Col == 5) || (e.Col == 6))
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void TxtFchDoc_Validated(object sender, EventArgs e)
        {
            //LblTc.Text = objFunciones.ObtenerTC(dtTc, TxtFchDoc.Text).ToString("0.000");
            if (STU_SISTEMA.MESTRABAJO == 0)
            {
                DataTable dttctem = new DataTable();
                ObjTC.mysConec = mysConec;
                int n_ano = Convert.ToDateTime(TxtFchDoc.Text).Year;
                dttctem = ObjTC.Listartcano(151, n_ano.ToString());
                LblTc.Text = objFunciones.ObtenerTC(dttctem, TxtFchDoc.Text).ToString("0.000");
            }
            else
            {
                LblTc.Text = objFunciones.ObtenerTC(dtTC, TxtFchDoc.Text).ToString("0.000");
            }
        }

        private void TxtNumSer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                string strCad = "0000" + TxtNumSer.Text;

                TxtNumSer.Text = strCad.Substring(strCad.Length - 4, 4);
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

        private void TxtNumDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                string strCad = "0000000000" + TxtNumDoc.Text;

                TxtNumDoc.Text = strCad.Substring(strCad.Length - 10, 10);
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

        private void TxtNumSer_Validated(object sender, EventArgs e)
        {
            if (TxtNumSer.Text != "")
            {
                string strCad = "0000" + TxtNumSer.Text;

                TxtNumSer.Text = "0" + strCad.Substring(strCad.Length - 3, 3);
                //TxtNumDoc.Text = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboTipDoc.SelectedValue), TxtNumSer.Text);
            }
        }

        private void TxtNumDoc_Validated(object sender, EventArgs e)
        {
            if (TxtNumDoc.Text != "")
            {
                string strCad = "0000000000" + TxtNumDoc.Text;
                TxtNumDoc.Text = strCad.Substring(strCad.Length - 10, 10);
            }
        }

        private void CboLib_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void CboSubLib_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void CboMon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtFchDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void CboTipDoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtGlosa_KeyPress(object sender, KeyPressEventArgs e)
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

        private void CboLib_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
            if (n_QueHace == 3) { return; }

            DataTable dtResul = new DataTable();
            CboSubLib.Enabled = true;

            dtResul = funDatos.DataTableFiltrar(dtSubLibro, "n_idlib = "+ Convert.ToInt32(CboLib.SelectedValue) +"");
            funDatos.ComboBoxCargarDataTable(CboSubLib, dtResul, "n_id", "c_des");
        }

        private void CboMeses_SelectedValueChanged(object sender, EventArgs e)
        {
            DataTable dtResul = new DataTable();
            if (booAgregando == true) { return; }
                        
            //objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO,  Convert.ToInt32(CboMeses.SelectedValue));    // CARGAMOS LOS DATOS DEL FORMULARIO
            //dtLista = objRegistros.dtLista;
            STU_SISTEMA.MESTRABAJO = Convert.ToInt32(CboMeses.SelectedValue);
            ListarItems();

            DgLista.DataSource = dtLista;
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

        private void CmdDelIte_Click(object sender, EventArgs e)
        {
            if (FgItems.Rows.Count <= 2) { return; }
            FgItems.RemoveItem(FgItems.Row);
        }

        private void CmdAddIte_Click(object sender, EventArgs e)
        {
            if (funFunciones.NulosC(FgItems.GetData(FgItems.Rows.Count - 1, 1)) == "") { return; }
            FgItems.Rows.Count = FgItems.Rows.Count + 1;
        }

    }
}
