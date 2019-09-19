using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Tesoreria;
using SIAC_Negocio.Sistema;
using SIAC_Objetos.Sistema;
using SIAC_Negocio.Tesoreria;
using SIAC_Negocio.Sunat;
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

namespace SSF_NET_Tesoreria.Formularios
{
    public partial class FrmConciliacionBanco : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_tes_conciliacion objRegistros = new CN_tes_conciliacion();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_sun_tipmon o_Moneda = new CN_sun_tipmon();
        CN_tes_cuentabanco o_CtaBan = new CN_tes_cuentabanco();
        CN_mae_meses o_Mes = new CN_mae_meses();
        CN_tes_bancos o_bancos = new CN_tes_bancos();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_TES_CONCILIACION BE_Registro = new BE_TES_CONCILIACION();
        List<BE_TES_CONCILIACIONDET> l_CociliaDet = new List<BE_TES_CONCILIACIONDET>();
        BE_TES_CUENTABANCO e_CtaBaco = new BE_TES_CUENTABANCO();
        BE_TES_BANCOS e_banco = new BE_TES_BANCOS();

        // DATATABLE LOCALES
        DataTable dtLista = new DataTable();           // LISTARA TODOS LOS REGISTROS DEL MES ACTUAL
        DataTable dtRegistro = new DataTable();        // ALMACENARA LOS DATOS DEL REGISTRO SELECCIONADO

        DataTable dtForm = new DataTable();
        DataTable dtMoneda = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtCtaBan = new DataTable();
        DataTable dtPer = new DataTable();

        // VARIABLES LOCALES
        //int n_NumFilasDocumento = 30;                                                 // LE INDICAMOS AL FORMULARIO EL NUMERO MAXIMO DE FILAS PARA EL DETALLE
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[10, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[12, 5];

        bool booSeEjecuto = false;
        bool b_Agregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmConciliacionBanco()
        {
            InitializeComponent();
        }

        private void FrmConciliacionBanco_Load(object sender, EventArgs e)
        {
            b_Agregando = true;
            CargarCombos();
            ConfigurarFormulario();
            b_Agregando = false;
        }
        void CargarCombos()
        {
            DataTableCargar();

            funDatos.ComboBoxCargarDataTable(CboMon, dtMoneda, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboPer, dtPer, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboCta, dtCtaBan, "n_id", "c_numcue");
        }
        void ConfigurarFormulario()
        {
            //this.Height = 622;
            //this.Width = 880;
            this.Width = 924;
            this.Height = 621;

            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;
            this.Text = dtForm.Rows[0]["c_titfor"].ToString();

            arrCabeceraFlex1[0, 0] = "Nº Registro";
            arrCabeceraFlex1[0, 1] = "60";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_numreg";

            arrCabeceraFlex1[1, 0] = "T.D.";
            arrCabeceraFlex1[1, 1] = "40";
            arrCabeceraFlex1[1, 2] = "C";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_abr";

            arrCabeceraFlex1[2, 0] = "Nº Documento";
            arrCabeceraFlex1[2, 1] = "110";
            arrCabeceraFlex1[2, 2] = "C";
            arrCabeceraFlex1[2, 3] = "";
            arrCabeceraFlex1[2, 4] = "c_openumdoc";

            arrCabeceraFlex1[3, 0] = "Fch. Operacion";
            arrCabeceraFlex1[3, 1] = "70";
            arrCabeceraFlex1[3, 2] = "F";
            arrCabeceraFlex1[3, 3] = "dd/MM/yyyy";
            arrCabeceraFlex1[3, 4] = "d_fchope";

            arrCabeceraFlex1[4, 0] = "Medio Pago";
            arrCabeceraFlex1[4, 1] = "150";
            arrCabeceraFlex1[4, 2] = "C";
            arrCabeceraFlex1[4, 3] = "";
            arrCabeceraFlex1[4, 4] = "c_des3";

            arrCabeceraFlex1[5, 0] = "Glosa";
            arrCabeceraFlex1[5, 1] = "150";
            arrCabeceraFlex1[5, 2] = "C";
            arrCabeceraFlex1[5, 3] = "";
            arrCabeceraFlex1[5, 4] = "c_glo";

            arrCabeceraFlex1[6, 0] = "Debe";
            arrCabeceraFlex1[6, 1] = "70";
            arrCabeceraFlex1[6, 2] = "D";
            arrCabeceraFlex1[6, 3] = "0.00";
            arrCabeceraFlex1[6, 4] = "n_debe";

            arrCabeceraFlex1[7, 0] = "Haber";
            arrCabeceraFlex1[7, 1] = "70";
            arrCabeceraFlex1[7, 2] = "D";
            arrCabeceraFlex1[7, 3] = "0.00";
            arrCabeceraFlex1[7, 4] = "n_haber";

            arrCabeceraFlex1[8, 0] = "Saldo";
            arrCabeceraFlex1[8, 1] = "70";
            arrCabeceraFlex1[8, 2] = "D";
            arrCabeceraFlex1[8, 3] = "0.00";
            arrCabeceraFlex1[8, 4] = "";

            arrCabeceraFlex1[9, 0] = "Check";
            arrCabeceraFlex1[9, 1] = "40";
            arrCabeceraFlex1[9, 2] = "B";
            arrCabeceraFlex1[9, 3] = "";
            arrCabeceraFlex1[9, 4] = "b_check";

            arrCabeceraFlex1[10, 0] = "n_idtes";
            arrCabeceraFlex1[10, 1] = "0";
            arrCabeceraFlex1[10, 2] = "N";
            arrCabeceraFlex1[10, 3] = "";
            arrCabeceraFlex1[10, 4] = "n_idtes";
            
            arrCabeceraFlex1[11, 0] = "n_idori";
            arrCabeceraFlex1[11, 1] = "0";
            arrCabeceraFlex1[11, 2] = "N";
            arrCabeceraFlex1[11, 3] = "";
            arrCabeceraFlex1[11, 4] = "n_idori";

            funFlex.FlexMostrarDatos(FgDato, arrCabeceraFlex1, dtLista, 2, false);
            //ConfigurarFlex();
            //CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;
        }
        //void ConfigurarFlex()
        //{
        //    int n_numcol = FgDato.Cols.Count - 1;
        //    int n_col = 0;
        //    for (n_col = 1; n_col <= n_numcol; n_col++)
        //    {
        //        funFlex.Flex_UnirFilas(FgDato, n_col, 0, 1, FgDato.GetData(0, n_col).ToString(), FgDato.Cols[n_col].Width);
        //    }
        //}
        void DataTableCargar()
        {
            objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            objRegistros.Listar(STU_SISTEMA.EMPRESAID,STU_SISTEMA.ANOTRABAJO,Convert.ToInt16(STU_SISTEMA.MESTRABAJO));
            dtLista = objRegistros.dtLista;

            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(79, ref arrCabeceraDg1);

            objForm.mysConec = mysConec;                                         // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(79);

            o_Moneda.mysConec = mysConec;
            dtMoneda = o_Moneda.Listar();

            o_CtaBan.mysConec = mysConec;
            dtCtaBan = o_CtaBan.Listar(STU_SISTEMA.EMPRESAID);

            o_Mes.mysConec = mysConec;
            dtMeses = o_Mes.Listar();
            dtPer = o_Mes.Listar();
            dtPer = funDatos.DataTableFiltrar(dtPer, "n_id IN (1,2,3,4,5,6,7,8,9,10,11,12)");
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
            b_Agregando = true;
            DataTable dtConciDet = new DataTable();
            CN_tes_conciliacion objRegistros = new CN_tes_conciliacion();
            objRegistros.mysConec = mysConec;

            objRegistros.TraerRegistro(n_IdRegistro);
            if (objRegistros.b_OcurrioError == false)
            {
                BE_Registro = objRegistros.e_Conci;
                dtConciDet = objRegistros.dtListaDet;

                LblNumOpe.Text = dtConciDet.Rows.Count.ToString();

                CboCta.SelectedValue = BE_Registro.n_idcue;
                CboPer.SelectedValue = BE_Registro.n_idper;
                TxtSalIni.Text = BE_Registro.n_saliniban.ToString("0.00");
                TxtSalfinBan.Text = BE_Registro.n_salfinban.ToString("0.00");
                TxtFchEmi.Text = Convert.ToDateTime(BE_Registro.d_fchcon).ToString("dd/MM/yyyy");
                TxtObs.Text = BE_Registro.c_glo.ToString();
                TxtSalFin.Text = BE_Registro.n_salfin.ToString("0.00");

                o_CtaBan.mysConec = mysConec;
                o_CtaBan.TraerRegistro(Convert.ToInt16(CboCta.SelectedValue));
                e_CtaBaco = o_CtaBan.entBancos;

                CboMon.SelectedValue = e_CtaBaco.n_idmon;

                o_bancos.mysConec = mysConec;
                o_bancos.TraerRegistro(e_CtaBaco.n_idban);
                e_banco = o_bancos.entBancos;
                LblNomBan.Text = e_banco.c_des;

                funFlex.FlexMostrarDatos(FgDato, arrCabeceraFlex1, dtConciDet, 2, true);
                //ConfigurarFlex();
                CalcularSaldo();
            }
        }
        void CalcularSaldo()
        {
            int n_row = 0;
            double n_salini = Convert.ToDouble(TxtSalIni.Text);
            double n_salinicon = Convert.ToDouble(TxtSalIni.Text);
            //double n_salact = 0;
            double n_impdebcon = 0;
            double n_imphabcon = 0;
            double n_totimpdebcon = 0;
            double n_totimphabcon = 0;
            
            double n_impdeb = 0;
            double n_imphab = 0;
            double n_totimpdeb = 0;
            double n_totimphab = 0;

            for (n_row = 2; n_row <= FgDato.Rows.Count - 1; n_row++)
            { 
                n_impdeb = Convert.ToDouble(FgDato.GetData(n_row,7));
                n_imphab = Convert.ToDouble(FgDato.GetData(n_row,8));
                n_salini = ((n_salini + n_impdeb) - n_imphab);

                n_totimpdeb = n_totimpdeb + n_impdeb;
                n_totimphab = n_totimphab + n_imphab;
                FgDato.SetData(n_row, 9, n_salini.ToString("0.00"));

                if (FgDato.GetData(n_row, 10).ToString() == "True")
                {
                    n_impdebcon = Convert.ToDouble(FgDato.GetData(n_row, 7));
                    n_imphabcon = Convert.ToDouble(FgDato.GetData(n_row, 8));
                    n_salinicon = ((n_salinicon + n_impdebcon) - n_imphabcon);

                    n_totimpdebcon = n_totimpdebcon + n_impdebcon;
                    n_totimphabcon = n_totimphabcon + n_imphabcon;
                }
            }

            TxtTotDeb.Text = n_totimpdeb.ToString("0.00");
            TxtTotHab.Text = n_totimphab.ToString("0.00");
            TxtSalFin.Text = n_salini.ToString("0.00");

            TxtTotDebCon.Text = n_totimpdebcon.ToString("0.00");
            TxtTotHabCon.Text = n_totimphabcon.ToString("0.00");
            TxtSalFinCon.Text = n_salinicon.ToString("0.00");

            TxtDifDeb.Text = (n_totimpdeb - n_totimpdebcon).ToString("0.00");
            TxtDifHab.Text = (n_totimphab - n_totimphabcon).ToString("0.00"); ;
            TxtDifSal.Text = (Convert.ToDouble(TxtDifDeb.Text) - Convert.ToDouble(TxtDifHab.Text)).ToString("0.00");
        }
        void Nuevo()
        {
            b_Agregando = true;
            n_QueHace = 1;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();
            LblTitulo2.Text = "Agregando Nuevo Registro";
            CmdCargar.Text = "Cargar Mov.";
            Tab1.SelectedIndex = 1;
            CboCta.Focus();
            b_Agregando = false;
        }
        void Blanquea()
        {
            CboCta.SelectedValue = 0;
            LblNomBan.Text = "";
            CboMon.SelectedValue=0;
            CboPer.SelectedValue=0;
            TxtFchEmi.Text ="";
            TxtSalIni.Text = "";
            TxtSalfinBan.Text = "";
            TxtObs.Text = "";
            LblNumOpe.Text = "";
            TxtSalFin.Text = "";
            FgDato.Rows.Count = 2;
            l_CociliaDet.Clear();
        }
        void Bloquea()
        {
            CboCta.Enabled = !CboCta.Enabled;
            //LblNomBan.Enabled = !LblNomBan.Enabled;
            //CboMon.Enabled = !CboMon.Enabled;
            CboPer.Enabled = !CboPer.Enabled;
            TxtFchEmi.Enabled = !TxtFchEmi.Enabled;
            TxtSalIni.Enabled = !TxtSalIni.Enabled;
            TxtSalfinBan.Enabled = !TxtSalfinBan.Enabled;
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
            ToolExportar.Enabled = !ToolExportar.Enabled;
            ToolSalir.Enabled = !ToolSalir.Enabled;
        }
        void Modificar()
        {
            b_Agregando = true;
            n_QueHace = 2;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();

            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            CmdCargar.Text = "Recargar Mov.";
            Tab1.SelectedIndex = 1;
            CboCta.Focus();
            FgDato.AllowEditing = true;
            b_Agregando = false;
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                if (objRegistros.Eliminar(intIdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objRegistros.mysConec = mysConec;
                    objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt16(CboMeses.SelectedValue));
                    dtLista = objRegistros.dtLista;
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
            b_Agregando = true;
            n_QueHace = 3;
            ActivarTool();
            Bloquea();
            LblTitulo2.Text = "Detalle del Registro";
            Tab1.TabPages[0].Enabled = true;
            Tab1.SelectedIndex = 0;
            DgLista.Focus();
            b_Agregando = false;
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
                booResultado = objRegistros.Insertar(BE_Registro, l_CociliaDet);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Actualizar(BE_Registro, l_CociliaDet);
            }

            if (booResultado == false)
            {
                MessageBox.Show("¡ ¨Ha ocurrido un un problema, no se pudo guardar el registro ! Error Nº : " + objRegistros.n_ErrorNumber.ToString() + " = " + objRegistros.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }

            return booResultado;
        }
        void AsignarEntidad()
        {
            int n_row = 0;
            int n_check = 0;
            //double n_totingnocon = 0;
            //double n_totegrnocon = 0;

            BE_Registro.n_idemp = STU_SISTEMA.EMPRESAID;
            if (n_QueHace == 1) { BE_Registro.n_id = 0; }
		    
		    BE_Registro.n_ano = STU_SISTEMA.ANOTRABAJO;
		    BE_Registro.n_idmes = Convert.ToInt16(CboMeses.SelectedValue);
		    BE_Registro.n_idcue = Convert.ToInt16(CboCta.SelectedValue);
		    BE_Registro.n_saliniban = Convert.ToDouble(TxtSalIni.Text);
            BE_Registro.n_salfinban = Convert.ToDouble(TxtSalfinBan.Text);
		    BE_Registro.d_fchcon = Convert.ToDateTime(TxtFchEmi.Text);
		    BE_Registro.c_glo = TxtObs.Text;
            BE_Registro.n_idper = Convert.ToInt16(CboPer.SelectedValue);
            BE_Registro.n_salfin = Convert.ToDouble(funFunciones.NulosN(TxtSalFinCon.Text));

            BE_Registro.n_toting = Convert.ToDouble(TxtTotDebCon.Text);  // HallarTotIng();
            BE_Registro.n_totegr = Convert.ToDouble(TxtTotHabCon.Text);  // HallarTotEgr();

            BE_Registro.n_totingnocon = Convert.ToDouble(TxtTotDeb.Text) - Convert.ToDouble(TxtTotDebCon.Text);
            BE_Registro.n_totegrnocon = Convert.ToDouble(TxtTotHab.Text) - Convert.ToDouble(TxtTotHabCon.Text);

            l_CociliaDet.Clear();
            double n_valor = 0;
            for (n_row = 2; n_row <= FgDato.Rows.Count - 1; n_row++)
            {
                BE_TES_CONCILIACIONDET e_ConciDet = new BE_TES_CONCILIACIONDET();

                e_ConciDet.n_idcon = 0;
		        e_ConciDet.n_idtes = Convert.ToInt32(FgDato.GetData(n_row, 11));
		        e_ConciDet.n_idori = Convert.ToInt32(FgDato.GetData(n_row, 12));

                n_check = Convert.ToInt32(FgDato.GetData(n_row, 10));
                if (n_check == 1) { e_ConciDet.n_check = 1; }
                if (n_check == 0) { e_ConciDet.n_check = 0; }

                e_ConciDet.d_opefch = Convert.ToDateTime(FgDato.GetData(n_row, 4));
		        e_ConciDet.c_opeglo = FgDato.GetData(n_row, 6).ToString();
		        e_ConciDet.c_openumreg = FgDato.GetData(n_row, 1).ToString();
		        e_ConciDet.c_openumdoc = FgDato.GetData(n_row, 3).ToString();

                n_valor = Convert.ToDouble(FgDato.GetData(n_row, 7)) + Convert.ToDouble(FgDato.GetData(n_row, 8));
                e_ConciDet.n_opeimp = n_valor;
                if (Convert.ToDouble(FgDato.GetData(n_row, 7)) != 0) { e_ConciDet.n_tipreg = 1; }
                if (Convert.ToDouble(FgDato.GetData(n_row, 8)) != 0) { e_ConciDet.n_tipreg = 2; }
                
                l_CociliaDet.Add(e_ConciDet);
            }
        }
        double HallarTotIngNoCon()
        {
            int n_row = 0;
            double n_valor = 0;
            for (n_row = 2; n_row <= FgDato.Rows.Count - 1; n_row++)
            {
                if (FgDato.GetData(n_row, 10).ToString() != "True")
                {
                    n_valor = n_valor + Convert.ToDouble(FgDato.GetData(n_row, 7));
                }
            }
            return n_valor;
        }
        double HallarTotEgrNoCon()
        {
            int n_row = 0;
            double n_valor = 0;
            for (n_row = 2; n_row <= FgDato.Rows.Count - 1; n_row++)
            {
                if (FgDato.GetData(n_row, 10).ToString() != "True")
                {
                    n_valor = n_valor + Convert.ToDouble(FgDato.GetData(n_row, 8));
                }
            }
            return n_valor;
        }

        //double HallarTotIng()
        //{
        //    int n_row = 0;
        //    double n_valor = 0;
        //    for (n_row = 2; n_row <= FgDato.Rows.Count - 1; n_row++)
        //    {
        //        if (FgDato.GetData(n_row, 10).ToString() == "True")
        //        { 
        //            n_valor = n_valor + Convert.ToDouble(FgDato.GetData(n_row, 7));
        //        }
        //    }
        //    return n_valor;
        //}
        //double HallarTotEgr()
        //{
        //    int n_row = 0;
        //    double n_valor = 0;
        //    for (n_row = 2; n_row <= FgDato.Rows.Count - 1; n_row++)
        //    {
        //        if (FgDato.GetData(n_row, 10).ToString() == "True")
        //        {
        //            n_valor = n_valor + Convert.ToDouble(FgDato.GetData(n_row, 8));
        //        }
        //    }
        //    return n_valor;
        //}

        bool CamposOK()
        {
            bool booEstado = true;

            if (Convert.ToInt16(CboCta.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el numero de cuenta a conciliar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboCta.Focus();
                return booEstado;
            }
            if (TxtFchEmi.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de la conciliacion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtFchEmi.Focus();
                return booEstado;
            }
            if (Convert.ToInt16(CboPer.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificadoel periodo de la conciliacion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                CboPer.Focus();
                return booEstado;
            }
            if (TxtSalIni.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el saldo inicial para empezar la conciliacion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtSalIni.Focus();
                return booEstado;
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
                objRegistros.mysConec = mysConec;
                objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt16(CboMeses.SelectedValue));
                dtLista = objRegistros.dtLista;
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
        private void FrmConciliacionBanco_Activated(object sender, EventArgs e)
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
        private void DgLista_Click(object sender, EventArgs e)
        {

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
            if (b_Agregando == true) { return; }
            if (n_QueHace != 3) { return; }

            if (DgLista.RowCount == 0)
            {
                MessageBox.Show("¡ No hay registros que mostrar!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                e.Cancel = true;
                Blanquea();
                FgDato.Rows.Count = 2;
                return;
            }

            if (e.NewIndex == 1)
            {
                int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    b_Agregando = true;
                    VerRegistro(intIdRegistro);
                    b_Agregando = false;
                }
            }
        }
        private void CmdCargar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(CboCta.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha indicado el numero de cuenta a conciliar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboMeses.Focus();
                return;
            }
            if (Convert.ToInt16(CboMeses.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha indicado el mes a conciliar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboMeses.Focus();
                return;
            }
            if (Convert.ToDouble(funFunciones.NulosN(TxtSalIni.Text)) == 0)
            {
                MessageBox.Show("¡ No ha indicado el saldo inicial del banco !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtSalIni.Focus();
                return;
            }
            if (Convert.ToDouble(funFunciones.NulosN(TxtSalfinBan.Text)) == 0)
            {
                MessageBox.Show("¡ No ha indicado el saldo final del banco !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtSalFin.Focus();
                return;
            }
            DataTable dtResult = new DataTable();

            if (CmdCargar.Text == "Cargar Mov.") 
            { 
                objRegistros.TraerParaConciliacion(STU_SISTEMA.EMPRESAID, Convert.ToInt16(CboPer.SelectedValue), Convert.ToInt16(CboCta.SelectedValue), STU_SISTEMA.ANOTRABAJO);
                dtResult = objRegistros.dtLista;
            }
            else
            {
                objRegistros.TraerParaConciliacion(STU_SISTEMA.EMPRESAID, Convert.ToInt16(CboPer.SelectedValue), Convert.ToInt16(CboCta.SelectedValue), STU_SISTEMA.ANOTRABAJO);
                dtResult = objRegistros.dtLista;
                dtResult = MostrarConciliacionPrevia(dtResult);
            }
            
            FgDato.Rows.Count = 2;
            funFlex.FlexMostrarDatos(FgDato, arrCabeceraFlex1, dtResult, 2, true);
            //ConfigurarFlex();
            CalcularSaldo();
            LblNumOpe.Text = (dtResult.Rows.Count).ToString();
            MessageBox.Show("¡ Los datos se mostraron con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            if (n_QueHace != 3) { FgDato.AllowEditing = true; }
        }
        DataTable MostrarConciliacionPrevia(DataTable dtresultado)
        {
            int n_row = 0;
            int n_rowdat = 0;
            int n_idtes = 0;
            for (n_row = 2; n_row <= FgDato.Rows.Count - 1; n_row++)
            {
                if (FgDato.GetData(n_row, 10).ToString() == "True")
                {
                    n_idtes = Convert.ToInt32(FgDato.GetData(n_row,11).ToString());

                    for (n_rowdat = 0; n_rowdat <= dtresultado.Rows.Count - 1; n_rowdat++)
                    {
                        if (Convert.ToInt32(dtresultado.Rows[n_rowdat]["n_idtes"]) == n_idtes)
                        {
                            dtresultado.Rows[n_rowdat]["b_check"] = 1;
                            break;
                        }
                    }
                }
            }
            return dtresultado;
        }
        private void CboCta_SelectedValueChanged(object sender, EventArgs e)
        {
            if (b_Agregando == true) { return; }
            o_CtaBan.mysConec = mysConec;
            o_CtaBan.TraerRegistro(Convert.ToInt16(CboCta.SelectedValue));
            e_CtaBaco = o_CtaBan.entBancos;

            CboMon.SelectedValue = e_CtaBaco.n_idmon;

            o_bancos.mysConec = mysConec;
            o_bancos.TraerRegistro(e_CtaBaco.n_idban);
            e_banco = o_bancos.entBancos;
            LblNomBan.Text = e_banco.c_des;
        }
        private void FgDato_EnterCell(object sender, EventArgs e)
        {
            if (FgDato.Col == 10)
            {
                FgDato.AllowEditing = true;
            }
            else
            {
                FgDato.AllowEditing = false;
            }
        }
        private void CmdAddOtrMes_Click(object sender, EventArgs e)
        {
           
            DataTable dtresult  = new DataTable();
            objRegistros.STU_SISTEMA = STU_SISTEMA;
            objRegistros.mysConec = mysConec;
            dtresult = objRegistros.PendientesOtrosMeses(STU_SISTEMA.EMPRESAID, Convert.ToInt16(CboCta.SelectedValue), Convert.ToInt16(CboPer.SelectedValue));

            if (dtresult != null)
            { 
                if (dtresult.Rows.Count != 0)
                {
                    funFlex.FlexMostrarDatos(FgDato, arrCabeceraFlex1, dtresult);
                    //ConfigurarFlex();
                    MessageBox.Show("¡ Movimientos de otro periodo agregados con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            if (DgLista.RowCount == 0)
            {
                MessageBox.Show("¡ No hay registros que mostrar!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
            Tab1.SelectedIndex = 1;
            b_Agregando = true;
            VerRegistro(intIdRegistro);
            b_Agregando = false;
        }

        private void CboCta_KeyPress(object sender, KeyPressEventArgs e)
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

        private void CboPer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtFchEmi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtSalIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                TxtSalIni.Text = Convert.ToDouble(TxtSalIni.Text).ToString("0.00");
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

        private void CboMeses_SelectedValueChanged(object sender, EventArgs e)
        {
            DataTable dtResul = new DataTable();
            if (b_Agregando == true) { return; }

            objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt32(CboMeses.SelectedValue));    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtLista = objRegistros.dtLista;
            LblNumReg.Text = (dtLista.Rows.Count).ToString();
            STU_SISTEMA.MESTRABAJO = Convert.ToInt32(CboMeses.SelectedValue);
            //MostrarEstadoMes(STU_SISTEMA.EMPRESAID, STU_SISTEMA.MESTRABAJO);
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

        private void ToolExportar_Click(object sender, EventArgs e)
        {
            Tab1.SelectedIndex = 1;

            string c_nomarch = STU_SISTEMA.EMPRESARUC + "-" + STU_SISTEMA.ANOTRABAJO.ToString() + "-" + STU_SISTEMA.MESTRABAJO.ToString() + "-01-CONCILIACION-BANCOS-" + CboCta.Text + ".xls";

            funFlex.ExportToExcel(FgDato, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "CONCILIACION BANCARIA - CLIENTES", "CUENTA Nº :" + CboCta.Text, c_nomarch);
        }

        private void ToolHerramientas_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void FrmConciliacionBanco_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
        }

        private void emitirGuiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImprimirConciliacion();
        }
        void ImprimirConciliacion()
        {
            CN_tes_conciliacion objConci = new CN_tes_conciliacion();
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

            objConci.STU_SISTEMA = STU_SISTEMA;
            objConci.mysConec = mysConec;
            objConci.ImprimirConciliacion(intIdRegistro);
        }

        private void CmdCalSal_Click(object sender, EventArgs e)
        {
            CalcularSaldo();
        }

        private void ChkMarcarTodo_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkMarcarTodo.Checked == true)
            {
                int n_row = 0;
                for (n_row = 2; n_row <= FgDato.Rows.Count - 1; n_row++)
                {
                    FgDato.SetData(n_row, 10, true);
                }
            }
            else
            {
                int n_row = 0;
                for (n_row = 2; n_row <= FgDato.Rows.Count - 1; n_row++)
                {
                    FgDato.SetData(n_row, 10, false);
                }
            }
        }

        private void ToolImprimir_ButtonClick(object sender, EventArgs e)
        {

        }
    }
}
