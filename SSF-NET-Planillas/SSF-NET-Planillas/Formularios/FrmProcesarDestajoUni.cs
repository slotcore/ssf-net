using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIAC_Negocio.Maestros;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Planilla;
using SIAC_Negocio.Almacen;
using SIAC_Negocio.Sunat;
using SIAC_Negocio.Produccion;
using SIAC_Negocio.Ventas;
using SIAC_Entidades.Planillas;
using SIAC_Entidades.Almacen;
using SIAC_Entidades.Produccion;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using Helper;
using SIAC_Objetos.Sistema;

namespace SSF_NET_Planillas.Formularios
{
    public partial class FrmProcesarDestajoUni : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        BE_PLA_DESTAJOUNI entCargos = new BE_PLA_DESTAJOUNI();
        List<BE_PLA_DESTAJOUNIDET> lstCargosDet = new List<BE_PLA_DESTAJOUNIDET>();
        List<BE_PLA_DESTAJOUNIPLA> lstCargosPla = new List<BE_PLA_DESTAJOUNIPLA>();

        // OBJETOS LOCALES
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_mae_meses objMeses = new CN_mae_meses();
        CN_pla_destajouni objRegistro = new CN_pla_destajouni();
        CN_sun_tipdoccom objTipDoc = new CN_sun_tipdoccom();
        
        CN_pla_personal objPersonal = new CN_pla_personal();
        CN_sys_empresalocal o_Local = new CN_sys_empresalocal();
        CN_pla_destajo o_plades = new CN_pla_destajo();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // DATATABLE LOCALES
        DataTable dtForm = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtListar = new DataTable();
        DataTable dtTipDoc = new DataTable();
        DataTable dtNumDocRef = new DataTable();
        DataTable dtPerPro = new DataTable();
        DataTable dtTipoDocumento = new DataTable();
        DataTable dtEmpleados = new DataTable();
        DataTable dtPersonal = new DataTable();
        DataTable dtLocal = new DataTable();
        DataTable dtPlaDes = new DataTable();

        // VARIABLES LOCALES
        int n_QueHace = 3;                                                               // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[10, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlexPagos = new string[11, 5];
        string[,] arrCabeceraFlexHoras = new string[10, 5];
        string[,] arrCabeceraFlexPla = new string[2, 5];

        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890-()º.,/$' !!·%/()=?¿*^" + (char)8;

        public FrmProcesarDestajoUni()
        {
            InitializeComponent();
        }

        private void CmdPro_Click(object sender, EventArgs e)
        {
            if (TxtFchIni.Text == " ")
            {
                MessageBox.Show("¡ No ha especificado la fecha de inicio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchIni.Focus();
                return;
            }

            if (TxtFchFin.Text == " ")
            {
                MessageBox.Show("¡ No ha especificado la fecha de termino !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchFin.Focus();
                return;
            }

            bool booResultado = false;
            string c_CadIn = funFlex.Flex_CadenaIN(c1FlexGrid1,2,1);
            if (CamposOK() == false)
            {
                return;
            }
            AsignarEntidad();

            if (n_QueHace == 1)
            {
                booResultado = objRegistro.Insertar(entCargos, c_CadIn, lstCargosPla);
            }
            if (n_QueHace == 2)
            {
                booResultado = objRegistro.Actualizar(entCargos, c_CadIn, lstCargosPla);
            }
            if (booResultado == true)
            {
                if (objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO) == true)
                {
                    dtListar = objRegistro.dtLista;
                }
                // MOSTRAMOS LOS DATOS EN LA GRILLA
                ListarItems();

                if (n_QueHace == 1)
                {
                    MessageBox.Show("¡ La planilla se genero con exito ! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    MessageBox.Show("¡ La planilla se actualizo con exito ! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
                Cancelar();
            }
            else 
            {
                MessageBox.Show("¡ No se pudo guardar la planilla por el siguiente motivo: " + objRegistro.c_ErrorMensaje + " ! ", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void FrmProcesarDestajoUni_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            Tab1.SelectedIndex = 0;
            ConfigurarFormulario();
            booAgregando = false;
        }
        void CargarCombos()
        {
            DataTableCargar();
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboRes, dtPersonal, "n_id", "c_apenom");
            funDatos.ComboBoxCargarDataTable(CboLocal, dtLocal, "n_id", "c_des");

        }
        void ConfigurarFormulario()
        {
            this.Height = 700;
            this.Width = 1060;

            FgLisPer.AllowEditing = false;
            FgLisHor.AllowEditing = false;
            //FgLisPer.AllowSorting = false;
            //FgLisHor.AllowSorting = false;

            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;

            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            // FLEX GRID DE LOS INSUMOS
            arrCabeceraFlexPagos[0, 0] = "Id_Per";
            arrCabeceraFlexPagos[0, 1] = "0";
            arrCabeceraFlexPagos[0, 2] = "N";
            arrCabeceraFlexPagos[0, 3] = "";
            arrCabeceraFlexPagos[0, 4] = "c_codrec";

            arrCabeceraFlexPagos[1, 0] = "Nº";
            arrCabeceraFlexPagos[1, 1] = "35";
            arrCabeceraFlexPagos[1, 2] = "N";
            arrCabeceraFlexPagos[1, 3] = "";
            arrCabeceraFlexPagos[1, 4] = "c_codrec";

            arrCabeceraFlexPagos[2, 0] = "Nº DNI";
            arrCabeceraFlexPagos[2, 1] = "80";
            arrCabeceraFlexPagos[2, 2] = "C";
            arrCabeceraFlexPagos[2, 3] = "";
            arrCabeceraFlexPagos[2, 4] = "c_codrec";

            arrCabeceraFlexPagos[3, 0] = "Colaborador";
            arrCabeceraFlexPagos[3, 1] = "300";
            arrCabeceraFlexPagos[3, 2] = "C";
            arrCabeceraFlexPagos[3, 3] = "";
            arrCabeceraFlexPagos[3, 4] = "c_despro";

            arrCabeceraFlexPagos[4, 0] = "Dia-1";
            arrCabeceraFlexPagos[4, 1] = "70";
            arrCabeceraFlexPagos[4, 2] = "D";
            arrCabeceraFlexPagos[4, 3] = "0.00";
            arrCabeceraFlexPagos[4, 4] = "c_unimed";

            arrCabeceraFlexPagos[5, 0] = "Dia-2";
            arrCabeceraFlexPagos[5, 1] = "70";
            arrCabeceraFlexPagos[5, 2] = "D";
            arrCabeceraFlexPagos[5, 3] = "0.00";
            arrCabeceraFlexPagos[5, 4] = "n_can";

            arrCabeceraFlexPagos[6, 0] = "Dia-3";
            arrCabeceraFlexPagos[6, 1] = "70";
            arrCabeceraFlexPagos[6, 2] = "D";
            arrCabeceraFlexPagos[6, 3] = "0.00";
            arrCabeceraFlexPagos[6, 4] = "d_fchent";

            arrCabeceraFlexPagos[7, 0] = "Dia-4";
            arrCabeceraFlexPagos[7, 1] = "70";
            arrCabeceraFlexPagos[7, 2] = "D";
            arrCabeceraFlexPagos[7, 3] = "0.00";
            arrCabeceraFlexPagos[7, 4] = "n_numarm";

            arrCabeceraFlexPagos[8, 0] = "Dia-5";
            arrCabeceraFlexPagos[8, 1] = "70";
            arrCabeceraFlexPagos[8, 2] = "D";
            arrCabeceraFlexPagos[8, 3] = "0.00";
            arrCabeceraFlexPagos[8, 4] = "n_idpro";

            arrCabeceraFlexPagos[9, 0] = "Dia-6";
            arrCabeceraFlexPagos[9, 1] = "70";
            arrCabeceraFlexPagos[9, 2] = "D";
            arrCabeceraFlexPagos[9, 3] = "0.00";
            arrCabeceraFlexPagos[9, 4] = "n_idrec";

            arrCabeceraFlexPagos[10, 0] = "Total Pagar";
            arrCabeceraFlexPagos[10, 1] = "70";
            arrCabeceraFlexPagos[10, 2] = "D";
            arrCabeceraFlexPagos[10, 3] = "0.00";
            arrCabeceraFlexPagos[10, 4] = "n_idrec";

            funFlex.FlexMostrarDatos(FgLisPer, arrCabeceraFlexPagos, dtListar, 2, false);
            funFlex.FlexMostrarDatos(FgLisHor, arrCabeceraFlexPagos, dtListar, 2, false);

            arrCabeceraFlexPla[0, 0] = "Planilla";
            arrCabeceraFlexPla[0, 1] = "400";
            arrCabeceraFlexPla[0, 2] = "C";
            arrCabeceraFlexPla[0, 3] = "";
            arrCabeceraFlexPla[0, 4] = "";

            arrCabeceraFlexPla[1, 0] = "n_id";
            arrCabeceraFlexPla[1, 1] = "0";
            arrCabeceraFlexPla[1, 2] = "N";
            arrCabeceraFlexPla[1, 3] = "";
            arrCabeceraFlexPla[1, 4] = "";

            funFlex.FlexMostrarDatos(c1FlexGrid1, arrCabeceraFlexPla, dtListar, 1, false);
            
            this.Text = dtForm.Rows[0]["c_titfor"].ToString() +" UNIFICADO";
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;

            objFormVis.mysConec = mysConec;                                 // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(46, ref arrCabeceraDg1);
        }
        void DataTableCargar()
        {
            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(46);                             // INDICAMOS QUE ES EL FORMULARIO MAESTRO DE RECETAS

            objMeses.mysConec = mysConec;
            dtMeses = objMeses.Listar();

            objRegistro.mysConec = mysConec;
            if (objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO) == true)
            {
                dtListar = objRegistro.dtLista;
            }

            CN_pla_empleados objEmpleados = new CN_pla_empleados(STU_SISTEMA);                                    // CARGAMOS LA UNIDAD DE MEDIDA DEL INVENTARIO PORQUE LA ORDEN DE REQUETRIMIENTO Y PEDIDO UTILIZAN ESA UNIDAD DE MEDIDA
            //objEmpleados.mysConec = mysConec;
            if (objEmpleados.Listar(STU_SISTEMA.EMPRESAID) == true)
            {
                dtEmpleados = objEmpleados.dtLista;
            }

            objPersonal.mysConec = mysConec;
            if (objPersonal.Listar(STU_SISTEMA.EMPRESAID) == true)
            {
                dtPersonal = objPersonal.dtLista;
            }

            objTipDoc.mysConec = mysConec;
            dtTipoDocumento = objTipDoc.Listar();

            o_Local.mysConec = mysConec;
            dtLocal = o_Local.Listar(0, 0);
        }
        void ListarItems()
        {
            LblNumReg.Text = (dtListar.Rows.Count).ToString();
            funDbGrid.DG_FormatearGrid(DgLista, arrCabeceraDg1, dtListar, true);
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
            DataTable dtResult = new DataTable();
            DataTable dtPago = new DataTable();
            DataTable dtHora = new DataTable();
            DataTable dtPlaOri = new DataTable();

            objRegistro.TraerRegistro(n_IdRegistro);
            entCargos = objRegistro.e_Cargos;
            dtPago = objRegistro.dtPago;
            dtHora = objRegistro.dtHora;
            dtPlaOri = objRegistro.dtPlanillas;

            FgLisPer.Rows.Count = 2;

            TxtNumSer.Text = entCargos.c_numser;
            TxtNumDoc.Text = entCargos.c_numdoc;
            TxtFchReg.Text = entCargos.d_fchreg.ToString();
            TxtFchIni.Text = entCargos.d_fchini.ToString();
            TxtFchFin.Text = entCargos.d_fchfin.ToString();
            CboRes.SelectedValue = entCargos.n_idres;
            TxtObs.Text = entCargos.c_glo.ToString();
            CboLocal.SelectedValue = entCargos.n_idlocal;

            MostrarPago(dtPago);
            MostrarHora(dtHora);
            c1FlexGrid1.Rows.Count = 1;
            if (dtPlaOri != null)
            {
                for (n_row = 0; n_row <= dtPlaOri.Rows.Count - 1; n_row++)
                {
                    c1FlexGrid1.Rows.Count = c1FlexGrid1.Rows.Count + 1;
                    c_dato = dtPlaOri.Rows[n_row]["c_despla"].ToString();
                    c1FlexGrid1.SetData(c1FlexGrid1.Rows.Count - 1, 1, c_dato);

                    c_dato = dtPlaOri.Rows[n_row]["n_id"].ToString();
                    c1FlexGrid1.SetData(c1FlexGrid1.Rows.Count - 1, 2, c_dato);
                }
            }

            TabDetalle.SelectedIndex = 0;
        }
        void MostrarPago(DataTable dtPagos)
        {
            double n_total = 0;
            int n_row = 0;
            int n_fil = 2;
            string c_dato = "";
            int n_col = 0;
            int n_numdias = Convert.ToInt16((Convert.ToDateTime(TxtFchFin.Text) - Convert.ToDateTime(TxtFchIni.Text)).ToString(@"dd"));
            n_numdias = n_numdias + 1;
            DateTime d_fecha = Convert.ToDateTime(TxtFchIni.Text);
            FgLisPer.Cols.Count = 5;

            for (n_col = 1; n_col <= n_numdias; n_col++)
            {
                FgLisPer.Cols.Count = FgLisPer.Cols.Count + 1;
                FgLisPer.Cols[FgLisPer.Cols.Count - 1].DataType = typeof(Double);
                FgLisPer.Cols[FgLisPer.Cols.Count - 1].Format = "0.00";
                FgLisPer.Cols[FgLisPer.Cols.Count - 1].Width = 70;
                c_dato = d_fecha.ToString("yyy-MM-dd");
                FgLisPer.SetData(0, FgLisPer.Cols.Count - 1, "Dia " + n_col.ToString());
                FgLisPer.SetData(1, FgLisPer.Cols.Count - 1, c_dato);
                d_fecha = d_fecha.AddDays(1);
            }

            //MOSTRAMOS EL IMPORTE A PAGAR
            for (n_row = 0; n_row <= dtPagos.Rows.Count - 1; n_row++)
            {
                FgLisPer.Rows.Count = FgLisPer.Rows.Count + 1;

                c_dato = dtPagos.Rows[n_row]["n_idper"].ToString();
                FgLisPer.SetData(n_fil, 1, c_dato);

                c_dato = (n_fil - 1).ToString();
                FgLisPer.SetData(n_fil, 2, c_dato);

                c_dato = dtPagos.Rows[n_row]["c_numdocide"].ToString();
                FgLisPer.SetData(n_fil, 3, c_dato);

                c_dato = dtPagos.Rows[n_row]["c_apenom"].ToString();
                FgLisPer.SetData(n_fil, 4, c_dato);

                int n_col2 = 5;
                for (n_col2 = 5; n_col2 <= FgLisPer.Cols.Count - 1; n_col2++)
                {
                    c_dato = FgLisPer.GetData(1, n_col2).ToString();
                    if (ExisteColumna(c_dato, dtPagos) == true)
                    {
                        c_dato = Convert.ToDouble(funFunciones.NulosN(dtPagos.Rows[n_row][c_dato])).ToString("0.00");
                        FgLisPer.SetData(n_fil, n_col2, c_dato);
                    }
                }

                n_fil = n_fil + 1;
            }

            FgLisPer.Cols.Count = FgLisPer.Cols.Count + 1;
            FgLisPer.Cols[FgLisPer.Cols.Count - 1].DataType = typeof(Double);
            FgLisPer.Cols[FgLisPer.Cols.Count - 1].Format = "0.00";
            funFlex.Flex_FixUnirFilas(FgLisPer, FgLisPer.Cols.Count - 1, 0, 1, "Total", 70);

            for (n_row = 2; n_row <= FgLisPer.Rows.Count - 1; n_row++)
            {
                n_total = 0;
                n_total = funFlex.FlexSumarRow(FgLisPer, n_row, 5, FgLisPer.Cols.Count - 2);
                FgLisPer.SetData(n_row, FgLisPer.Cols.Count - 1, n_total.ToString("0.00"));
            }

            n_total = funFlex.FlexSumarCol(FgLisPer, FgLisPer.Cols.Count - 1, 2, FgLisPer.Rows.Count - 1);

            FgLisPer.Rows.Count = FgLisPer.Rows.Count + 1;
            FgLisPer.SetData(FgLisPer.Rows.Count - 1, FgLisPer.Cols.Count - 2, "TOTAL ==>");
            FgLisPer.SetData(FgLisPer.Rows.Count - 1, FgLisPer.Cols.Count - 1, n_total.ToString("0.00"));
            FgLisPer.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn;
            //double n_total = 0;
            //int n_row = 0;
            ////int n_col = 0;
            //int n_fil = 2;
            //string c_dato = "";

            //FgLisPer.Rows.Count = 2;
            //dtPagos = funDatos.DataTableOrdenar(dtPagos, "c_apenom");

            //funFlex.Flex_FixUniColumnas(FgLisPer, 0, 4, 9, "DIAS TRABAJADOS", 1);

            //c_dato = Convert.ToDateTime(TxtFchIni.Text).ToString("dd-MM-yy");
            //FgLisPer.SetData(1, 4, c_dato);

            //c_dato = funFunciones.FechaSumarDias(Convert.ToDateTime(TxtFchIni.Text), 1).ToString("dd-MM-yy");
            //FgLisPer.SetData(1, 5, c_dato);

            //c_dato = funFunciones.FechaSumarDias(Convert.ToDateTime(TxtFchIni.Text), 2).ToString("dd-MM-yy");
            //FgLisPer.SetData(1, 6, c_dato);

            //c_dato = funFunciones.FechaSumarDias(Convert.ToDateTime(TxtFchIni.Text), 3).ToString("dd-MM-yy");
            //FgLisPer.SetData(1, 7, c_dato);

            //c_dato = funFunciones.FechaSumarDias(Convert.ToDateTime(TxtFchIni.Text), 4).ToString("dd-MM-yy");
            //FgLisPer.SetData(1, 8, c_dato);

            //c_dato = funFunciones.FechaSumarDias(Convert.ToDateTime(TxtFchIni.Text), 5).ToString("dd-MM-yy");
            //FgLisPer.SetData(1, 9, c_dato);

            ////MOSTRAMOS EL IMPORTE A PAGAR
            //for (n_row = 0; n_row <= dtPagos.Rows.Count - 1; n_row++)
            //{
            //    FgLisPer.Rows.Count = FgLisPer.Rows.Count + 1;

            //    c_dato = (n_fil - 1).ToString();
            //    FgLisPer.SetData(n_fil, 1, c_dato);

            //    c_dato = dtPagos.Rows[n_row]["c_numdocide"].ToString();
            //    FgLisPer.SetData(n_fil, 2, c_dato);

            //    c_dato = dtPagos.Rows[n_row]["c_apenom"].ToString();
            //    FgLisPer.SetData(n_fil, 3, c_dato);

            //    c_dato = Convert.ToDateTime(TxtFchIni.Text).ToString("yyyy-MM-dd");
            //    if (ExisteColumna(c_dato, dtPagos) == true)
            //    {
            //        c_dato = Convert.ToDouble(funFunciones.NulosN(dtPagos.Rows[n_row][c_dato])).ToString("0.00");
            //        FgLisPer.SetData(n_fil, 4, c_dato);
            //    }

            //    c_dato = funFunciones.FechaSumarDias(Convert.ToDateTime(TxtFchIni.Text), 1).ToString("yyyy-MM-dd");
            //    if (ExisteColumna(c_dato, dtPagos) == true)
            //    {
            //        c_dato = Convert.ToDouble(funFunciones.NulosN(dtPagos.Rows[n_row][c_dato])).ToString("0.00");
            //        FgLisPer.SetData(n_fil, 5, c_dato);
            //    }

            //    c_dato = funFunciones.FechaSumarDias(Convert.ToDateTime(TxtFchIni.Text), 2).ToString("yyyy-MM-dd");
            //    if (ExisteColumna(c_dato, dtPagos) == true)
            //    {
            //        c_dato = Convert.ToDouble(funFunciones.NulosN(dtPagos.Rows[n_row][c_dato])).ToString("0.00");
            //        FgLisPer.SetData(n_fil, 6, c_dato);
            //    }

            //    c_dato = funFunciones.FechaSumarDias(Convert.ToDateTime(TxtFchIni.Text), 3).ToString("yyyy-MM-dd");
            //    if (ExisteColumna(c_dato, dtPagos) == true)
            //    {
            //        c_dato = Convert.ToDouble(funFunciones.NulosN(dtPagos.Rows[n_row][c_dato])).ToString("0.00");
            //        FgLisPer.SetData(n_fil, 7, c_dato);
            //    }

            //    c_dato = funFunciones.FechaSumarDias(Convert.ToDateTime(TxtFchIni.Text), 4).ToString("yyyy-MM-dd");
            //    if (ExisteColumna(c_dato, dtPagos) == true)
            //    {
            //        c_dato = Convert.ToDouble(funFunciones.NulosN(dtPagos.Rows[n_row][c_dato])).ToString("0.00");
            //        FgLisPer.SetData(n_fil, 8, c_dato);
            //    }

            //    c_dato = funFunciones.FechaSumarDias(Convert.ToDateTime(TxtFchIni.Text), 5).ToString("yyyy-MM-dd");
            //    if (ExisteColumna(c_dato, dtPagos) == true)
            //    {
            //        c_dato = Convert.ToDouble(funFunciones.NulosN(dtPagos.Rows[n_row][c_dato])).ToString("0.00");
            //        FgLisPer.SetData(n_fil, 9, c_dato);
            //    }

            //    c_dato = Convert.ToInt16(funFunciones.NulosN(dtPagos.Rows[n_row]["n_idper"])).ToString();
            //    FgLisPer.SetData(n_fil, 11, c_dato);
            //    n_fil = n_fil + 1;
            //}

            //for (n_row = 2; n_row <= FgLisPer.Rows.Count - 1; n_row++)
            //{
            //    n_total = 0;
            //    n_total = funFlex.FlexSumarRow(FgLisPer, n_row, 4, 9);
            //    FgLisPer.SetData(n_row, 10, n_total.ToString("0.00"));
            //}

            //n_total = funFlex.FlexSumarCol(FgLisPer, 10, 2, FgLisPer.Rows.Count - 1);

            //FgLisPer.Rows.Count = FgLisPer.Rows.Count + 1;
            //FgLisPer.SetData(FgLisPer.Rows.Count - 1, 9, "TOTAL ==>");
            //FgLisPer.SetData(FgLisPer.Rows.Count - 1, 10, n_total.ToString("0.00"));

        }

        bool ExisteColumna(string c_NombreColumna, DataTable dtDatos)
        {
            bool b_result = false;
            int n_numcol = dtDatos.Columns.Count;
            int n_col = 0;

            for (n_col = 0; n_col <= n_numcol - 1; n_col++)
            {

                if (dtDatos.Columns[n_col].ToString() == c_NombreColumna)
                {
                    b_result = true;
                    break;
                }
            }

            return b_result;
        }
        void MostrarHora(DataTable dtHoras)
        {
            double n_total = 0;
            int n_row = 0;
            int n_fil = 2;
            string c_dato = "";
            int n_col = 0;
            int n_numdias = Convert.ToInt16((Convert.ToDateTime(TxtFchFin.Text) - Convert.ToDateTime(TxtFchIni.Text)).ToString(@"dd"));
            n_numdias = n_numdias + 1;
            DateTime d_fecha = Convert.ToDateTime(TxtFchIni.Text);
            FgLisHor.Rows.Count = 2;
            FgLisHor.Cols.Count = 5;

            for (n_col = 1; n_col <= n_numdias; n_col++)
            {
                FgLisHor.Cols.Count = FgLisHor.Cols.Count + 1;
                FgLisHor.Cols[FgLisHor.Cols.Count - 1].DataType = typeof(Double);
                FgLisHor.Cols[FgLisHor.Cols.Count - 1].Format = "0.00";
                FgLisHor.Cols[FgLisHor.Cols.Count - 1].Width = 70;
                c_dato = d_fecha.ToString("yyy-MM-dd");
                FgLisHor.SetData(0, FgLisHor.Cols.Count - 1, "Dia " + n_col.ToString());
                FgLisHor.SetData(1, FgLisHor.Cols.Count - 1, c_dato);
                d_fecha = d_fecha.AddDays(1);
            }

            //MOSTRAMOS EL IMPORTE A PAGAR
            for (n_row = 0; n_row <= dtHoras.Rows.Count - 1; n_row++)
            {
                FgLisHor.Rows.Count = FgLisHor.Rows.Count + 1;

                c_dato = dtHoras.Rows[n_row]["n_idper"].ToString();
                FgLisHor.SetData(n_fil, 1, c_dato);

                c_dato = (n_fil - 1).ToString();
                FgLisHor.SetData(n_fil, 2, c_dato);

                c_dato = dtHoras.Rows[n_row]["c_numdocide"].ToString();
                FgLisHor.SetData(n_fil, 3, c_dato);

                c_dato = dtHoras.Rows[n_row]["c_apenom"].ToString();
                FgLisHor.SetData(n_fil, 4, c_dato);

                int n_col2 = 5;
                for (n_col2 = 5; n_col2 <= FgLisHor.Cols.Count - 1; n_col2++)
                {
                    c_dato = FgLisHor.GetData(1, n_col2).ToString();
                    if (ExisteColumna(c_dato, dtHoras) == true)
                    {
                        c_dato = Convert.ToDouble(funFunciones.NulosN(dtHoras.Rows[n_row][c_dato])).ToString("0.00");
                        FgLisHor.SetData(n_fil, n_col2, c_dato);
                    }
                }

                n_fil = n_fil + 1;
            }

            FgLisHor.Cols.Count = FgLisHor.Cols.Count + 1;
            FgLisHor.Cols[FgLisHor.Cols.Count - 1].DataType = typeof(Double);
            FgLisHor.Cols[FgLisHor.Cols.Count - 1].Format = "0.00";
            funFlex.Flex_FixUnirFilas(FgLisHor, FgLisHor.Cols.Count - 1, 0, 1, "Total", 70);

            for (n_row = 2; n_row <= FgLisHor.Rows.Count - 1; n_row++)
            {
                n_total = 0;
                n_total = funFlex.FlexSumarRow(FgLisHor, n_row, 5, FgLisHor.Cols.Count - 2);
                FgLisHor.SetData(n_row, FgLisHor.Cols.Count - 1, n_total.ToString("0.00"));
            }

            n_total = funFlex.FlexSumarCol(FgLisHor, FgLisHor.Cols.Count - 1, 2, FgLisHor.Rows.Count - 1);

            FgLisHor.Rows.Count = FgLisHor.Rows.Count + 1;
            FgLisHor.SetData(FgLisHor.Rows.Count - 1, FgLisHor.Cols.Count - 2, "TOTAL ==>");
            FgLisHor.SetData(FgLisHor.Rows.Count - 1, FgLisHor.Cols.Count - 1, n_total.ToString("0.00"));
            FgLisHor.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn;

            //double n_total = 0;
            //int n_row = 0;
            ////int n_col = 0;
            //int n_fil = 2;
            //string c_dato = "";

            //FgLisHor.Rows.Count = 2;
            //dtHoras = funDatos.DataTableOrdenar(dtHoras, "c_apenom");

            //funFlex.Flex_FixUniColumnas(FgLisHor, 0, 4, 9, "DIAS TRABAJADOS", 1);

            //c_dato = Convert.ToDateTime(TxtFchIni.Text).ToString("dd-MM-yy");
            //FgLisHor.SetData(1, 4, c_dato);

            //c_dato = funFunciones.FechaSumarDias(Convert.ToDateTime(TxtFchIni.Text), 1).ToString("dd-MM-yy");
            //FgLisHor.SetData(1, 5, c_dato);

            //c_dato = funFunciones.FechaSumarDias(Convert.ToDateTime(TxtFchIni.Text), 2).ToString("dd-MM-yy");
            //FgLisHor.SetData(1, 6, c_dato);

            //c_dato = funFunciones.FechaSumarDias(Convert.ToDateTime(TxtFchIni.Text), 3).ToString("dd-MM-yy");
            //FgLisHor.SetData(1, 7, c_dato);

            //c_dato = funFunciones.FechaSumarDias(Convert.ToDateTime(TxtFchIni.Text), 4).ToString("dd-MM-yy");
            //FgLisHor.SetData(1, 8, c_dato);

            //c_dato = funFunciones.FechaSumarDias(Convert.ToDateTime(TxtFchIni.Text), 5).ToString("dd-MM-yy");
            //FgLisHor.SetData(1, 9, c_dato);

            ////MOSTRAMOS EL IMPORTE A PAGAR
            //for (n_row = 0; n_row <= dtHoras.Rows.Count - 1; n_row++)
            //{
            //    FgLisHor.Rows.Count = FgLisHor.Rows.Count + 1;

            //    c_dato = (n_fil - 1).ToString();
            //    FgLisHor.SetData(n_fil, 1, c_dato);

            //    c_dato = dtHoras.Rows[n_row]["c_numdocide"].ToString();
            //    FgLisHor.SetData(n_fil, 2, c_dato);

            //    c_dato = dtHoras.Rows[n_row]["c_apenom"].ToString();
            //    FgLisHor.SetData(n_fil, 3, c_dato);

            //    c_dato = Convert.ToDateTime(TxtFchIni.Text).ToString("yyyy-MM-dd");
            //    if (ExisteColumna(c_dato, dtHoras) == true)
            //    {
            //        c_dato = Convert.ToDouble(funFunciones.NulosN(dtHoras.Rows[n_row][c_dato])).ToString("0.00");
            //        FgLisHor.SetData(n_fil, 4, c_dato);
            //    }

            //    c_dato = funFunciones.FechaSumarDias(Convert.ToDateTime(TxtFchIni.Text), 1).ToString("yyyy-MM-dd");
            //    if (ExisteColumna(c_dato, dtHoras) == true)
            //    {
            //        c_dato = Convert.ToDouble(funFunciones.NulosN(dtHoras.Rows[n_row][c_dato])).ToString("0.00");
            //        FgLisHor.SetData(n_fil, 5, c_dato);
            //    }

            //    c_dato = funFunciones.FechaSumarDias(Convert.ToDateTime(TxtFchIni.Text), 2).ToString("yyyy-MM-dd");
            //    if (ExisteColumna(c_dato, dtHoras) == true)
            //    {
            //        c_dato = Convert.ToDouble(funFunciones.NulosN(dtHoras.Rows[n_row][c_dato])).ToString("0.00");
            //        FgLisHor.SetData(n_fil, 6, c_dato);
            //    }

            //    c_dato = funFunciones.FechaSumarDias(Convert.ToDateTime(TxtFchIni.Text), 3).ToString("yyyy-MM-dd");
            //    if (ExisteColumna(c_dato, dtHoras) == true)
            //    {
            //        c_dato = Convert.ToDouble(funFunciones.NulosN(dtHoras.Rows[n_row][c_dato])).ToString("0.00");
            //        FgLisHor.SetData(n_fil, 7, c_dato);
            //    }

            //    c_dato = funFunciones.FechaSumarDias(Convert.ToDateTime(TxtFchIni.Text), 4).ToString("yyyy-MM-dd");
            //    if (ExisteColumna(c_dato, dtHoras) == true)
            //    {
            //        c_dato = Convert.ToDouble(funFunciones.NulosN(dtHoras.Rows[n_row][c_dato])).ToString("0.00");
            //        FgLisHor.SetData(n_fil, 8, c_dato);
            //    }

            //    c_dato = funFunciones.FechaSumarDias(Convert.ToDateTime(TxtFchIni.Text), 5).ToString("yyyy-MM-dd");
            //    if (ExisteColumna(c_dato, dtHoras) == true)
            //    {
            //        c_dato = Convert.ToDouble(funFunciones.NulosN(dtHoras.Rows[n_row][c_dato])).ToString("0.00");
            //        FgLisHor.SetData(n_fil, 9, c_dato);
            //    }

            //    c_dato = Convert.ToInt16(funFunciones.NulosN(dtHoras.Rows[n_row]["n_idper"])).ToString();
            //    FgLisHor.SetData(n_fil, 11, c_dato);
            //    n_fil = n_fil + 1;
            //}

            //for (n_row = 2; n_row <= FgLisHor.Rows.Count - 1; n_row++)
            //{
            //    n_total = 0;
            //    n_total = funFlex.FlexSumarRow(FgLisHor, n_row, 4, 9);
            //    FgLisHor.SetData(n_row, 10, n_total.ToString("0.00"));
            //}

            //n_total = funFlex.FlexSumarCol(FgLisHor, 10, 2, FgLisHor.Rows.Count - 1);

            //FgLisHor.Rows.Count = FgLisHor.Rows.Count + 1;
            //FgLisHor.SetData(FgLisHor.Rows.Count - 1, 9, "TOTAL ==>");
            //FgLisHor.SetData(FgLisHor.Rows.Count - 1, 10, n_total.ToString("0.00"));
        }
        void Nuevo()
        {
            booAgregando = true;
            n_QueHace = 1;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();
            LblTitulo2.Text = "Agregando Nuevo Registro";
            Tab1.SelectedIndex = 1;
            booAgregando = false;

            // MOSTRAMOS EL ULTIMO NUMERO DE REQUERIMIENTO
            string c_numdoc = "";
            c_numdoc = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, 80, "0001");            // DOCUMENTO PLANILLA DE JORNALES
            TxtNumSer.Text = "0001";
            TxtNumDoc.Text = c_numdoc;
            c1FlexGrid1.Rows.Count = c1FlexGrid1.Rows.Count + 1;
            c1FlexGrid1.Cols[1].ComboList = "...";
            c1FlexGrid1.AllowEditing = true;
            TxtFchReg.Focus();
        }
        void Blanquea()
        {
            TxtNumSer.Text = "";
            TxtNumDoc.Text = "";

            //TxtFchReg.Text = "";
            //TxtFchReg.CustomFormat = " ";
            //TxtFchReg.Format = DateTimePickerFormat.Custom;

            TxtFchIni.Text = "";
            TxtFchIni.CustomFormat = " ";
            TxtFchIni.Format = DateTimePickerFormat.Custom;

            TxtFchFin.Text = "";
            TxtFchFin.CustomFormat = " ";
            TxtFchFin.Format = DateTimePickerFormat.Custom;

            CboRes.SelectedValue = 0;
            TxtObs.Text = "";

            FgLisHor.Rows.Count = 2;
            FgLisPer.Rows.Count = 2;
            c1FlexGrid1.Rows.Count = 1;
        }
        void Bloquea()
        {
            TxtNumSer.Enabled = !TxtNumSer.Enabled;
            TxtNumDoc.Enabled = !TxtNumDoc.Enabled;
            //TxtFchReg.Enabled = !TxtFchReg.Enabled;
            TxtFchIni.Enabled = !TxtFchIni.Enabled;
            TxtFchFin.Enabled = !TxtFchFin.Enabled;
            CboRes.Enabled = !CboRes.Enabled;
            TxtObs.Enabled = !TxtObs.Enabled;
            CboLocal.Enabled = !CboLocal.Enabled;
            CmdPro.Enabled = !CmdPro.Enabled;
        }
        void ActivarTool()
        {
            ToolNuevo.Enabled = !ToolNuevo.Enabled;
            ToolModificar.Enabled = !ToolModificar.Enabled;
            ToolEliminar.Enabled = !ToolEliminar.Enabled;
            ToolGrabar.Enabled = !ToolGrabar.Enabled;
            ToolCancelar.Enabled = !ToolCancelar.Enabled;
            ToolMenuImprimir.Enabled = !ToolMenuImprimir.Enabled;
            ToolExportar.Enabled = !ToolExportar.Enabled;
            ToolSalir.Enabled = !ToolSalir.Enabled;
        }
        void Modificar()
        {
            booAgregando = true;
            n_QueHace = 2;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();

            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            TxtFchReg.Focus();
            booAgregando = false;
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR


            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                objRegistro.mysConec = mysConec;
                if (objRegistro.ELiminar(intIdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objRegistro.mysConec = mysConec;
                    if (objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO) == true)
                    {
                        dtListar = objRegistro.dtLista;
                    }
                    // MOSTRAMOS LOS DATOS EN LA GRILLA
                    ListarItems();
                }
                else
                {
                    MessageBox.Show("¡ No se pudo eliminar el registro por el siguiente motivo ! " + objRegistro.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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

            //if (n_QueHace == 1)
            //{
            //    booResultado = objRegistro.Insertar(entCargos, lstCargosDet);
            //}
            //if (n_QueHace == 2)
            //{
            //    booResultado = objRegistro.Actualizar(entCargos, lstCargosDet);
            //}

            if (booResultado == false)
            {
                MessageBox.Show("¡ No se pudo guardar el registro por el siguiente motivo ! " + objRegistro.c_ErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            return booResultado;
        }
        void AsignarEntidad()
        {
            int n_fila = 0;
            //int n_col = 0;
            //string c_data = "";

            lstCargosDet.Clear();
            lstCargosPla.Clear();
            entCargos.n_id = 0;

            if (n_QueHace == 2) { entCargos.n_id = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString()); }

            entCargos.n_idemp = STU_SISTEMA.EMPRESAID;
            entCargos.n_idtipdoc = 80;
            entCargos.c_numser = TxtNumSer.Text;
            entCargos.c_numdoc = TxtNumDoc.Text;
            entCargos.n_anotra = STU_SISTEMA.ANOTRABAJO;
            entCargos.n_mestra = STU_SISTEMA.MESTRABAJO;
            entCargos.d_fchini = Convert.ToDateTime(TxtFchIni.Text);
            entCargos.d_fchfin = Convert.ToDateTime(TxtFchFin.Text);
            entCargos.d_fchreg = Convert.ToDateTime(TxtFchReg.Text);
            entCargos.n_numtra = 0;
            entCargos.n_imp = 0;
            entCargos.c_glo = TxtObs.Text;
            entCargos.n_idres = Convert.ToInt32(CboRes.SelectedValue);
            entCargos.n_idlocal = Convert.ToInt32(CboLocal.SelectedValue);


            for (n_fila = 1; n_fila<=c1FlexGrid1.Rows.Count-1;n_fila++)
            {
                if (Convert.ToInt32(c1FlexGrid1.GetData(n_fila,2)) != 0)
                {
                    BE_PLA_DESTAJOUNIPLA e_CargosPla = new BE_PLA_DESTAJOUNIPLA();

                    e_CargosPla.n_idplaori = Convert.ToInt32(c1FlexGrid1.GetData(n_fila,2));
                    lstCargosPla.Add(e_CargosPla);
                }
            }
            
        }
        bool CamposOK()
        {
            bool booEstado = true;

            if (TxtNumSer.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de serie del proceso !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtNumSer.Focus();
                booEstado = false;
                return booEstado;
            }

            if (TxtNumDoc.Text == "")
            {
                MessageBox.Show("¡ No ha especificado el numero de documento del proceso !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtNumDoc.Focus();
                booEstado = false;
                return booEstado;
            }

            if (TxtFchReg.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de registro !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchReg.Focus();
                booEstado = false;
                return booEstado;
            }

            if (TxtFchIni.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de inicio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchIni.Focus();
                booEstado = false;
                return booEstado;
            }

            if (TxtFchFin.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de termino !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchFin.Focus();
                booEstado = false;
                return booEstado;
            }

            if (Convert.ToInt16(CboRes.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el nombre de la persona responsable de este proceso !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboRes.Focus();
                booEstado = false;
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
                objRegistro.mysConec = mysConec;
                if (objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO) == true)
                {
                    dtListar = objRegistro.dtLista;
                }
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
            objFormVis = null;

            this.Close();
        }

        private void FrmProcesarDestajoUni_Activated(object sender, EventArgs e)
        {
            if (booSeEjecuto == false)
            {
                booSeEjecuto = true;
                ListarItems();

                if (dtListar.Rows.Count == 0)
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
                dtResult = funDbGrid.DG_Filtrar(dtListar, c_CadFiltro, DgLista);
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

        private void FrmProcesarDestajoUni_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 82, this.Width - 18);
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

        private void TxtFchIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtFchFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtFchIni_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete")
            {
                TxtFchIni.Text = "";
                TxtFchIni.CustomFormat = " ";
                TxtFchIni.Format = DateTimePickerFormat.Custom;
            }
        }

        private void TxtFchFin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete")
            {
                TxtFchFin.Text = "";
                TxtFchFin.CustomFormat = " ";
                TxtFchFin.Format = DateTimePickerFormat.Custom;
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

        private void CboRes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtFchReg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void TxtFchReg_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete")
            {
                TxtFchReg.Text = "";
                TxtFchReg.CustomFormat = " ";
                TxtFchReg.Format = DateTimePickerFormat.Custom;
            }
        }

        private void TxtNumSer_Validated(object sender, EventArgs e)
        {
            if (TxtNumSer.Text != "")
            {
                string strCad = "0000" + TxtNumSer.Text;

                TxtNumDoc.Text = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, 80, TxtNumSer.Text);
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

        private void CboMeses_SelectedValueChanged(object sender, EventArgs e)
        {
            DataTable dtResul = new DataTable();
            if (booAgregando == true) { return; }


            if (objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt32(CboMeses.SelectedValue)) == true)
            {
                dtListar = objRegistro.dtLista;
                LblNumReg.Text = (dtListar.Rows.Count).ToString();
                STU_SISTEMA.MESTRABAJO = Convert.ToInt32(CboMeses.SelectedValue);
                DgLista.DataSource = dtListar;
            }

            if (dtListar.Rows.Count == 0)
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

        private void CmdVerTareas_Click(object sender, EventArgs e)
        {
            int n_idpla = 0;
            string c_fchcon = "";
            int n_idper = 0;
            
            n_idpla = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

            if (TabDetalle.SelectedIndex == 0)
            {
                if (FgLisPer.Col <= 3) { return; }
                if (FgLisPer.Col == 11) { return; }
                c_fchcon = FgLisPer.GetData(1, FgLisPer.Col).ToString();
                n_idper = Convert.ToInt32(FgLisPer.GetData(FgLisPer.Row, 1).ToString());
            }
            else
            {
                if (FgLisHor.Col <= 3) { return; }
                if (FgLisHor.Col == 10) { return; }
                c_fchcon = FgLisHor.GetData(1, FgLisHor.Col).ToString();
                n_idper = Convert.ToInt32(FgLisHor.GetData(FgLisHor.Row, 1).ToString());
            }

            objRegistro.VerTareasDia(n_idpla, n_idper, Convert.ToDateTime(c_fchcon).ToString("dd/MM/yyyy"));
        }

        private void exportarPagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tab1.SelectedIndex = 1;
            string c_NomArchivo = STU_SISTEMA.EMPRESARUC + "-PLA-PLANILLA DESTAJO IMPORTES UNIFICADO-" + STU_SISTEMA.ANOTRABAJO.ToString() + Convert.ToInt16(CboMeses.SelectedValue).ToString("00") + ".xls";

            if (FgLisPer.Rows.Count == 2)
            {
                MessageBox.Show("! No hay datos para exportar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            string c_titu2 = "FECHA INICIO  : " + TxtFchIni.Text + "        FECHA FINAL : " + TxtFchFin.Text;

            funFlex.ExportToExcel(FgLisPer, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "PLANILLA DE DESTAJOS UNIFICADA - IMPORTES", c_titu2, c_NomArchivo);
        }

        private void exportarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tab1.SelectedIndex = 1;
            string c_NomArchivo = STU_SISTEMA.EMPRESARUC + "-PLA-PLANILLA DESTAJO HORAS UNIFICADO-" + STU_SISTEMA.ANOTRABAJO.ToString() + Convert.ToInt16(CboMeses.SelectedValue).ToString("00") + ".xls";

            if (FgLisHor.Rows.Count == 2)
            {
                MessageBox.Show("! No hay datos para exportar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            string c_titu2 = "FECHA INICIO  : " + TxtFchIni.Text + "        FECHA FINAL : " + TxtFchFin.Text;

            funFlex.ExportToExcel(FgLisHor, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "PLANILLA DE DESTAJOS UNIFICADA - HORAS", c_titu2, c_NomArchivo);
        }

        private void c1FlexGrid1_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (c1FlexGrid1.Col == 1)
            {
                BuscarPlanillasPendientes();
            }
        }
        private void CmdAddPla_Click(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (funFunciones.NulosC(c1FlexGrid1.GetData(c1FlexGrid1.Rows.Count - 1, 1)) == "") { return; }

            c1FlexGrid1.Rows.Count = c1FlexGrid1.Rows.Count + 1;
            BuscarPlanillasPendientes();
        }
        void BuscarPlanillasPendientes()
        {
            int n_row = 0;
            string c_dato = "";
            DataTable dtResult = new DataTable();
            o_plades.mysConec = mysConec;
            o_plades.ListarPendUni();
            dtResult = o_plades.dtLista;
            if (dtResult != null)
            {
                for (n_row = 0; n_row <= dtResult.Rows.Count - 1; n_row++)
                {
                    c_dato = dtResult.Rows[n_row]["c_despla"].ToString();
                    c1FlexGrid1.SetData(c1FlexGrid1.Rows.Count - 1, 1, c_dato);

                    c_dato = dtResult.Rows[n_row]["n_id"].ToString();
                    c1FlexGrid1.SetData(c1FlexGrid1.Rows.Count - 1, 2, c_dato);

                    if (n_row <= dtResult.Rows.Count - 1)
                    { 
                        c1FlexGrid1.Rows.Count = c1FlexGrid1.Rows.Count + 1;
                    }
                }
            }
        }
        private void CmdDelPla_Click(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (c1FlexGrid1.Rows.Count == 1 ) { return; }
            c1FlexGrid1.RemoveItem(c1FlexGrid1.Row);
        }

        private void imprimirRecetaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
