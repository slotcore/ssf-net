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
using SIAC_Negocio.Contabilidad;
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
    public partial class FrmGenCargos : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_coo_cargos objRegistros = new CN_coo_cargos();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sun_tipdoccom objTipDocCom = new CN_sun_tipdoccom();
        CN_mae_meses objMeses = new CN_mae_meses();
        CN_coo_socios objSocios = new CN_coo_socios();
        CN_coo_puestos objPuestos = new CN_coo_puestos();
        CN_coo_sociospuestos objSociosPuestos = new CN_coo_sociospuestos();
        CN_con_doccomimp objDocComImp = new CN_con_doccomimp();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_COO_CARGOS BE_Registro = new BE_COO_CARGOS();

        BE_COO_CARGOS entCargos = new BE_COO_CARGOS();
        List<BE_COO_CARGOSCAB> lstCargCab = new List<BE_COO_CARGOSCAB>();
        List<BE_COO_CARGOSDET> lstCargDet = new List<BE_COO_CARGOSDET>();

        // DATATABLE LOCALES
        DataTable dtForm = new DataTable();
        DataTable dtRegistros = new DataTable();
        DataTable dtTipDocCom = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtMeses2 = new DataTable();
        DataTable dtCarDet = new DataTable();
        DataTable dtSocios = new DataTable();
        DataTable dtPuestos = new DataTable();
        DataTable dtSociosPuestos = new DataTable();
        DataTable dtDocComImp = new DataTable();

        // VARIABLES LOCALES
        int n_QueHace = 3;                                                              // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[9, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlexCar = new string[8, 5];
        bool booSeEjecuto = false;
        double n_TASAIMP = 0;
        bool booAgregando = false;

        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890-()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmGenCargos()
        {
            InitializeComponent();
        }

        private void FrmGenCargos_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }
        void CargarCombos()
        {
            DataTableCargar();

            funDatos.ComboBoxCargarDataTable(CboMesTra, dtMeses, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboTipDoc, dtTipDocCom, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses2, "n_id", "c_des");
        }
        void ConfigurarFormulario()
        {
            this.Height = 659;
            this.Width = 1012;
            //1012, 659
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";
            this.Text = dtForm.Rows[0]["c_titfor"].ToString();

            arrCabeceraFlexCar[0, 0] = "Nº Puesto";
            arrCabeceraFlexCar[0, 1] = "80";
            arrCabeceraFlexCar[0, 2] = "C";
            arrCabeceraFlexCar[0, 3] = "";
            arrCabeceraFlexCar[0, 4] = "";

            arrCabeceraFlexCar[1, 0] = "Socio";
            arrCabeceraFlexCar[1, 1] = "400";
            arrCabeceraFlexCar[1, 2] = "C";
            arrCabeceraFlexCar[1, 3] = "";
            arrCabeceraFlexCar[1, 4] = "";

            arrCabeceraFlexCar[2, 0] = "Nº Documento";
            arrCabeceraFlexCar[2, 1] = "100";
            arrCabeceraFlexCar[2, 2] = "C";
            arrCabeceraFlexCar[2, 3] = "";
            arrCabeceraFlexCar[2, 4] = "";

            arrCabeceraFlexCar[3, 0] = "Imp. Bruto";
            arrCabeceraFlexCar[3, 1] = "80";
            arrCabeceraFlexCar[3, 2] = "D";
            arrCabeceraFlexCar[3, 3] = "";
            arrCabeceraFlexCar[3, 4] = "";

            arrCabeceraFlexCar[4, 0] = "Imp. IGV";
            arrCabeceraFlexCar[4, 1] = "80";
            arrCabeceraFlexCar[4, 2] = "D";
            arrCabeceraFlexCar[4, 3] = "";
            arrCabeceraFlexCar[4, 4] = "";

            arrCabeceraFlexCar[5, 0] = "Imp. Total";
            arrCabeceraFlexCar[5, 1] = "80";
            arrCabeceraFlexCar[5, 2] = "D";
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

            funFlex.FlexMostrarDatos(FgLisCar, arrCabeceraFlexCar, dtRegistros, 2, false);
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;
        }
        void DataTableCargar()
        {
            objRegistros.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(49);

            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(49, ref arrCabeceraDg1);

            objTipDocCom.mysConec = mysConec;
            dtTipDocCom = objTipDocCom.Listar();

            objMeses.mysConec = mysConec;
            dtMeses = objMeses.Listar();
            dtMeses2 = objMeses.Listar();

            objSocios.mysConec = mysConec;
            dtSocios = objSocios.Listar(STU_SISTEMA.EMPRESAID);

            objPuestos.mysConec = mysConec;
            dtPuestos = objPuestos.Listar(STU_SISTEMA.EMPRESAID);

            objSociosPuestos.mysConec = mysConec;
            objSociosPuestos.Consulta2(STU_SISTEMA.EMPRESAID);
            dtSociosPuestos = objSociosPuestos.dtPuestosSocios;

            objDocComImp.mysConec = mysConec;
            dtDocComImp = objDocComImp.Listar(STU_SISTEMA.EMPRESAID);
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
            lstCargCab.Clear();
            lstCargDet.Clear();

            objRegistros.mysConec = mysConec;
            objRegistros.TraerRegistro(n_IdRegistro);

            BE_Registro = objRegistros.entCargos;
            lstCargCab = objRegistros.lstCargosCab;
            lstCargDet = objRegistros.lstCargosDet;

            TxtAnoTra.Text = BE_Registro.n_anotra.ToString();
            TxtFchEmi.Text = BE_Registro.d_fchemi.ToString("dd/MM/yyyy");
            CboTipDoc.SelectedValue = BE_Registro.n_idtipdoc;
            //TxtNumSer.Text = BE_Registro.;
            //TxtNumDoc.Text = "";
            CboMesTra.SelectedValue = BE_Registro.n_mestra;
            //TxtGlosa.Text = BE_Registro.;
            TxtNumSoc.Text = BE_Registro.n_numsoc.ToString("0.00");
            TxtImpBru.Text = BE_Registro.n_impbru.ToString("0.00");
            TxtImpIgv.Text = BE_Registro.n_impigv.ToString("0.00");
            TxtImpTot.Text = BE_Registro.n_imptot.ToString("0.00");
            MostrarDatos();
        }
        void MostrarDatos()
        {
            DataTable dtResult = new DataTable();
            int n_row = 0;
            string c_dato = "";
            FgLisCar.Rows.Count = 2;

            if (lstCargCab.Count == 0)
            {
                MessageBox.Show("¡ No hay cargosa para mostrar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            TxtNumSer.Text = lstCargCab[0].c_numser.ToString();
            TxtNumDoc.Text = lstCargCab[0].c_numdoc.ToString();

            for(n_row =0;n_row<= lstCargCab.Count-1;n_row++)
            {
                FgLisCar.Rows.Count = FgLisCar.Rows.Count + 1;

                // MOSTRAMOS EL NUMERO DE PUESTO
                c_dato = lstCargCab[n_row].n_idsocpue.ToString();
                dtResult = funDatos.DataTableFiltrar(dtSociosPuestos, "n_id = "+ c_dato +"");
                if (dtResult.Rows.Count != 0)
                {
                    c_dato = dtResult.Rows[0]["n_idpue"].ToString();
                    dtResult = funDatos.DataTableFiltrar(dtPuestos, "n_id = " + c_dato + "");
                    c_dato = dtResult.Rows[0]["c_puesto"].ToString();
                    FgLisCar.SetData(FgLisCar.Rows.Count - 1, 1, c_dato);
                }
                
                // MOSTRAMOS EL NOMBRE DEL SOCIO
                c_dato = lstCargCab[n_row].n_idsoc.ToString();
                dtResult = funDatos.DataTableFiltrar(dtSocios, "n_id = " + c_dato + "");
                c_dato = dtResult.Rows[0]["c_apenom"].ToString();
                if (dtResult.Rows.Count != 0)
                {
                    FgLisCar.SetData(FgLisCar.Rows.Count - 1, 2, c_dato);
                }

                // NUMERO DE DOCUMENTO
                c_dato = lstCargCab[n_row].c_numser.ToString() + "-" + lstCargCab[n_row].c_numdoc.ToString();
                FgLisCar.SetData(FgLisCar.Rows.Count - 1, 3, c_dato);


                FgLisCar.SetData(FgLisCar.Rows.Count - 1, 4, lstCargCab[n_row].n_impbru.ToString("0.00"));
                FgLisCar.SetData(FgLisCar.Rows.Count - 1, 5, lstCargCab[n_row].n_impigv.ToString("0.00"));
                FgLisCar.SetData(FgLisCar.Rows.Count - 1, 6, lstCargCab[n_row].n_imptot.ToString("0.00"));

                FgLisCar.SetData(FgLisCar.Rows.Count - 1, 7, lstCargCab[n_row].n_idsocpue.ToString());
                FgLisCar.SetData(FgLisCar.Rows.Count - 1, 8, lstCargCab[n_row].n_idsoc.ToString());
            }
          

            //FgLisCar.SetData(FgLisCar.Rows.Count - 1, 1, dtResult.Rows[n_row]["c_puesto"].ToString());
            //FgLisCar.SetData(FgLisCar.Rows.Count - 1, 2, dtResult.Rows[n_row]["c_apenom"].ToString());
            //FgLisCar.SetData(FgLisCar.Rows.Count - 1, 3, TxtNumSer.Text + "-" + n_numdoc.ToString("0000000000"));

            //if (Convert.ToInt32(CboTipDoc.SelectedValue) == 4)
            //{
            //    FgLisCar.SetData(FgLisCar.Rows.Count - 1, 4, dtResult.Rows[n_row]["n_impbru"].ToString());
            //    FgLisCar.SetData(FgLisCar.Rows.Count - 1, 5, dtResult.Rows[n_row]["n_impbru"].ToString());
            //    FgLisCar.SetData(FgLisCar.Rows.Count - 1, 6, dtResult.Rows[n_row]["n_impbru"].ToString());
            //}
            //else
            //{
            //    FgLisCar.SetData(FgLisCar.Rows.Count - 1, 4, dtResult.Rows[n_row]["n_impbru"].ToString());
            //    FgLisCar.SetData(FgLisCar.Rows.Count - 1, 5, 0);
            //    FgLisCar.SetData(FgLisCar.Rows.Count - 1, 6, dtResult.Rows[n_row]["n_impbru"].ToString());
            //}
            //FgLisCar.SetData(FgLisCar.Rows.Count - 1, 7, dtResult.Rows[n_row]["n_idpue"].ToString());
            //FgLisCar.SetData(FgLisCar.Rows.Count - 1, 8, dtResult.Rows[n_row]["n_idsoc"].ToString());

            //n_totbru = n_totbru + Convert.ToDouble(FgLisCar.GetData(FgLisCar.Rows.Count - 1, 4));
            //n_totigv = n_totigv + Convert.ToDouble(FgLisCar.GetData(FgLisCar.Rows.Count - 1, 5));
            //n_tottot = n_tottot + Convert.ToDouble(FgLisCar.GetData(FgLisCar.Rows.Count - 1, 6));
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
            TxtAnoTra.Text = STU_SISTEMA.ANOTRABAJO.ToString();
            CboMesTra.SelectedValue = STU_SISTEMA.MESTRABAJO;
            CboMesTra.Focus();
            FgLisCar.Rows.Count = 2;
        }
        void Blanquea()
        {
            TxtAnoTra.Text = "";
            TxtFchEmi.Text = "";
            CboTipDoc.SelectedValue = 0;
            TxtNumSer.Text = "";
            TxtNumDoc.Text = "";
            CboMesTra.SelectedValue = 0;
            TxtGlosa.Text = "";
            TxtNumSoc.Text = "";
            TxtImpBru.Text = "";
            TxtImpIgv.Text = "";
            TxtImpTot.Text = "";
        }
        void Bloquea()
        {
            TxtAnoTra.Enabled = !TxtAnoTra.Enabled;
            TxtFchEmi.Enabled = !TxtFchEmi.Enabled;
            CboTipDoc.Enabled = !CboTipDoc.Enabled;
            TxtNumSer.Enabled = !TxtNumSer.Enabled;
            TxtNumDoc.Enabled = !TxtNumDoc.Enabled;
            CboMesTra.Enabled = !CboMesTra.Enabled;
            TxtGlosa.Enabled = !TxtGlosa.Enabled;
            TxtNumSoc.Enabled = !TxtNumSoc.Enabled;
            TxtImpBru.Enabled = !TxtImpBru.Enabled;
            TxtImpIgv.Enabled = !TxtImpIgv.Enabled;
            TxtImpTot.Enabled = !TxtImpTot.Enabled;
        }
        void ActivarTool()
        {
            ToolNuevo.Enabled = !ToolNuevo.Enabled;
            ToolSDDModificar.Enabled = !ToolSDDModificar.Enabled;
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
            if (dtRegistros.Rows.Count == 0)
            {
                MessageBox.Show("No hay registros para modificar", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            n_QueHace = 2;
            Tab1.TabPages[0].Enabled = false;
            Blanquea();
            Bloquea();
            ActivarTool();

            int intIdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

            VerRegistro(intIdRegistro);
            LblTitulo2.Text = "Modificando Registro";
            Tab1.SelectedIndex = 1;
            CboMesTra.Focus();
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
                    dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);
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
            Blanquea();
            LblTitulo2.Text = "Detalle del Registro";
            Tab1.TabPages[0].Enabled = true;
            Tab1.SelectedIndex = 0;
            FgLisCar.Rows.Count = 2;
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
                booResultado = objRegistros.Insertar(BE_Registro, lstCargCab, lstCargDet);
            }

            if (n_QueHace == 2)
            {
                booResultado = objRegistros.Actualizar(BE_Registro, lstCargCab, lstCargDet);
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
            int n_rowdet = 0;
            string c_key = "";
            double n_valor =0;
            lstCargCab.Clear();
            lstCargDet.Clear();

            BE_Registro.n_idemp = STU_SISTEMA.EMPRESAID;
            if (n_QueHace == 1)
            {
                BE_Registro.n_id = 0;
            }
            else
            {
                BE_Registro.n_id = BE_Registro.n_id;
            }

            BE_Registro.n_idemp = STU_SISTEMA.EMPRESAID;
            //BE_Registro.n_id
            BE_Registro.n_anotra = Convert.ToInt32(TxtAnoTra.Text);
            BE_Registro.n_mestra= Convert.ToInt32(CboMesTra.SelectedValue);
            BE_Registro.d_fchemi = Convert.ToDateTime(TxtFchEmi.Text);
            BE_Registro.d_fchini= Convert.ToDateTime("01/08/2017");
            BE_Registro.d_fchfin= Convert.ToDateTime("31/08/2017");
            BE_Registro.n_numsoc = Convert.ToInt32(TxtNumSoc.Text);
            BE_Registro.n_impbru= Convert.ToDouble(TxtImpBru.Text);
            BE_Registro.n_impigv= Convert.ToDouble(TxtImpIgv.Text);
            BE_Registro.n_imptot= Convert.ToDouble(TxtImpTot.Text);
            BE_Registro.n_idtipdoc = Convert.ToInt32(CboTipDoc.SelectedValue);

            for (n_row = 2; n_row <= FgLisCar.Rows.Count - 1; n_row++ )
            {
                // AGREGAMOS LA CABECERA
                BE_COO_CARGOSCAB entCargosCab = new BE_COO_CARGOSCAB();

                entCargosCab.n_idemp = STU_SISTEMA.EMPRESAID;
                entCargosCab.n_idcar = BE_Registro.n_id;
                entCargosCab.n_idsoc = Convert.ToInt32(FgLisCar.GetData(n_row,8));
                //if (entCargosCab.n_idsoc == 11711)
                //{
                //    MessageBox.Show("¡ ¨Ha ocurrido un un problema, no se pudo guardar el registro ! Error Nº : " + objRegistros.IntErrorNumber.ToString() + " = " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //}
                entCargosCab.n_idsocpue= Convert.ToInt32(FgLisCar.GetData(n_row,7));
                entCargosCab.n_idtipdoc = Convert.ToInt32(CboTipDoc.SelectedValue);
                entCargosCab.c_numser = FgLisCar.GetData(n_row,3).ToString().Substring(0,4);
                entCargosCab.c_numdoc = FgLisCar.GetData(n_row,3).ToString().Substring(5,10);
                entCargosCab.d_fchemi = Convert.ToDateTime(TxtFchEmi.Text);
                entCargosCab.d_fchven = Convert.ToDateTime(TxtFchEmi.Text);
                entCargosCab.n_impbru = Convert.ToDouble(FgLisCar.GetData(n_row,4));
                entCargosCab.n_impigv= Convert.ToDouble(FgLisCar.GetData(n_row,5));
                entCargosCab.n_imptot= Convert.ToDouble(FgLisCar.GetData(n_row,6));
                entCargosCab.c_glosa = TxtGlosa.Text;
                entCargosCab.n_impsal = Convert.ToDouble(FgLisCar.GetData(n_row,6));
                entCargosCab.n_anotra =  Convert.ToInt32(TxtAnoTra.Text);
                entCargosCab.n_mestra = Convert.ToInt32(CboMesTra.SelectedValue);

                lstCargCab.Add(entCargosCab);
                
                // AGREGAMOS EL DETALLE DE LA CABECERA
                n_rowdet = 0;
                DataTable dtResult = new DataTable();
                c_key = entCargosCab.n_idsoc.ToString() + "-" + entCargosCab.n_idsocpue.ToString();

                //if (c_key == "13-447")
                //{
                //    MessageBox.Show("¡ ¨Ha ocurrido un un problema, no se pudo guardar el registro ! Error Nº : " + objRegistros.IntErrorNumber.ToString() + " = " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //}
                dtResult = funDatos.DataTableFiltrar(dtCarDet,"c_llave = '" + c_key + "'");
                if (dtResult.Rows.Count != 0)
                { 
                    for (n_rowdet = 0; n_rowdet <= dtResult.Rows.Count - 1; n_rowdet++)
                    {
                        BE_COO_CARGOSDET entCargosdet = new BE_COO_CARGOSDET();
                        entCargosdet.n_idemp = STU_SISTEMA.EMPRESAID;
                        entCargosdet.n_idcar = entCargosCab.n_idcar;
                        entCargosdet.n_idsoc = entCargosCab.n_idsoc;
                        entCargosdet.n_idcon = Convert.ToInt32(dtResult.Rows[n_rowdet]["n_idcom"]);
                        entCargosdet.n_can = 1;
                                                
                        n_valor = Convert.ToDouble(funFunciones.NulosN( dtResult.Rows[n_rowdet]["n_imp"]));

                        //if (n_valor == 3)
                        //{
                        //    MessageBox.Show("¡ ¨Ha ocurrido un un problema, no se pudo guardar el registro ! Error Nº : " + objRegistros.IntErrorNumber.ToString() + " = " + objRegistros.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        //}
                        entCargosdet.n_impbru = n_valor;
                        entCargosdet.n_imptotbru = n_valor * 1;
                        
                        n_valor = n_valor * ((n_TASAIMP / 100) + 1);
                        entCargosdet.n_impnet = n_valor;
                        entCargosdet.n_imptotnet = n_valor * 1;

                        entCargosdet.n_idpue = entCargosCab.n_idsocpue;

                        lstCargDet.Add(entCargosdet);
                    }
                }
            }
        }
        bool CamposOK()
        {
            bool booEstado = true;

            //if (Convert.ToInt32(CboMesTra.SelectedValue) == 0)
            //{
            //    MessageBox.Show("¡ No ha especificado el el mes de trabajo!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    booEstado = false;
            //    CboMesTra.Focus();
            //    return booEstado;
            //}

            if (Convert.ToInt32(CboTipDoc.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de documneto !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
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

            if (TxtFchEmi.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de emision del documento !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                booEstado = false;
                TxtFchEmi.Focus();
                return booEstado;
            }

            return booEstado;
        }
        private void FrmGenCargos_Activated(object sender, EventArgs e)
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
                dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO);
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
            if (dtRegistros.Rows.Count==0)
            {
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
        private void DgLista_DoubleClick(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
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
        private void FrmGenCargos_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 82, this.Width - 20);
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
        private void CmdGenerar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(CboTipDoc.SelectedValue) == 0)
            {
                MessageBox.Show("No ha indicado el tipo de documento ", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboTipDoc.Focus();
                return;
            }
            if (TxtNumSer.Text == "")
            {
                MessageBox.Show("No ha indicado el numero de serie del documento ", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtNumSer.Focus();
                return;
            }
            if (TxtNumDoc.Text == "")
            {
                MessageBox.Show("No ha indicado el numero del documento ", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtNumDoc.Focus();
                return;
            }
            string c_fchini = "";
            string c_fchfin = "";
            DataTable dtResult = new DataTable();
            int n_row = 0;
            int n_idemp = STU_SISTEMA.EMPRESAID;
            int n_anotra = Convert.ToInt32(TxtAnoTra.Text);
            int n_mestra = Convert.ToInt32(CboMesTra.SelectedValue);
            int n_idtipdoc = Convert.ToInt32(CboTipDoc.SelectedValue);
            string c_glo = TxtGlosa.Text;
            if (n_mestra == 0)
            {
                c_fchini = "01/01/2017";
                c_fchfin = "01/01/2017";
            }
            else
            {
                c_fchini = "01/" + n_mestra.ToString() + "/" + n_anotra.ToString();
                c_fchfin = "30/" + n_mestra.ToString() + "/" + n_anotra.ToString(); ;
            }

            int n_numdoc = Convert.ToInt32(TxtNumDoc.Text);
            double n_totbru = 0;
            double n_totigv = 0;
            double n_tottot = 0;
            int n_totsoc = 0;
           
            double n_valor =0;
            FgLisCar.Rows.Count = 2;
            n_TASAIMP = 0;
            dtResult = funDatos.DataTableFiltrar(dtDocComImp, "n_idtipdoc = " + CboTipDoc.SelectedValue.ToString() + "");

            if (dtResult.Rows.Count != 0)
            {
                n_TASAIMP = Convert.ToDouble(dtResult.Rows[0]["n_portas"]);
            }

            objRegistros.booOcurrioError = false;
            objRegistros.GenerarCargos(n_idemp, n_anotra, n_mestra, c_glo, c_fchini, c_fchfin, n_idtipdoc);
            if (objRegistros.booOcurrioError == true)
            {
                // EMITIR MENSAHE DE ERROR
                return;
            }

            dtResult = objRegistros.dtCargosCab;
            dtCarDet = objRegistros.dtCargosDet;
            //dtResult = objRegistros.d
            if (dtResult.Rows.Count != 0)
            {
                for (n_row = 0; n_row <= dtResult.Rows.Count - 1; n_row++)
                {
                    FgLisCar.Rows.Count = FgLisCar.Rows.Count + 1;
                    
                    FgLisCar.SetData(FgLisCar.Rows.Count - 1, 1, dtResult.Rows[n_row]["c_puesto"].ToString());
                    FgLisCar.SetData(FgLisCar.Rows.Count - 1, 2, dtResult.Rows[n_row]["c_apenom"].ToString());
                    FgLisCar.SetData(FgLisCar.Rows.Count - 1, 3, TxtNumSer.Text + "-" + n_numdoc.ToString("0000000000"));

                    if ((Convert.ToInt32(CboTipDoc.SelectedValue) == 4) || (Convert.ToInt32(CboTipDoc.SelectedValue) == 2))
                    {
                        FgLisCar.SetData(FgLisCar.Rows.Count - 1, 4, dtResult.Rows[n_row]["n_impbru"].ToString());
                        n_valor = Convert.ToDouble(funFunciones.NulosN(dtResult.Rows[n_row]["n_impbru"]));
                        n_valor = (n_valor * ((n_TASAIMP / 100) + 1) - n_valor);
                        FgLisCar.SetData(FgLisCar.Rows.Count - 1, 5, n_valor.ToString("0.00"));
                        n_valor = Convert.ToDouble(funFunciones.NulosN(dtResult.Rows[n_row]["n_impbru"]));
                        n_valor = (n_valor * ((n_TASAIMP / 100) + 1));
                        FgLisCar.SetData(FgLisCar.Rows.Count - 1, 6, n_valor);
                    }
                    else
                    {
                        FgLisCar.SetData(FgLisCar.Rows.Count - 1, 4, dtResult.Rows[n_row]["n_impbru"].ToString());
                        FgLisCar.SetData(FgLisCar.Rows.Count - 1, 5, 0);
                        FgLisCar.SetData(FgLisCar.Rows.Count - 1, 6, dtResult.Rows[n_row]["n_impbru"].ToString());
                    }
                    FgLisCar.SetData(FgLisCar.Rows.Count - 1, 7, dtResult.Rows[n_row]["n_id"].ToString());
                    FgLisCar.SetData(FgLisCar.Rows.Count - 1, 8, dtResult.Rows[n_row]["n_idsoc"].ToString());

                    n_totbru = n_totbru + Convert.ToDouble(FgLisCar.GetData(FgLisCar.Rows.Count - 1,4));
                    n_totigv = n_totigv + Convert.ToDouble(FgLisCar.GetData(FgLisCar.Rows.Count - 1,5));
                    n_tottot = n_tottot + Convert.ToDouble(FgLisCar.GetData(FgLisCar.Rows.Count - 1, 6));
                    
                    n_totsoc = n_totsoc + 1;
                    n_numdoc = n_numdoc + 1;
                }
                TxtNumSoc.Text = n_totsoc.ToString();
                TxtImpBru.Text = n_totbru.ToString("0.00");
                TxtImpIgv.Text = n_totigv.ToString("0.00");
                TxtImpTot.Text = n_tottot.ToString("0.00");
            }
        }
        private void CboTipDoc_SelectedValueChanged(object sender, EventArgs e)
        {
            if (n_QueHace == 3)
            {
                return;
            }
            string c_numdoc = "";
            
            TxtNumSer.Text = "0001";
            CN_sun_tipdoccom objTipDoc = new CN_sun_tipdoccom();
            objTipDoc.mysConec = mysConec;
            c_numdoc = objTipDoc.UltimoNumero(STU_SISTEMA.EMPRESAID, Convert.ToInt32(CboTipDoc.SelectedValue), TxtNumSer.Text);
            TxtNumDoc.Text = c_numdoc;
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
        private void ToolImprimir_Click(object sender, EventArgs e)
        {
            int intIdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());
            objRegistros.STU_SISTEMA = STU_SISTEMA;
            objRegistros.EmitirFacturas(intIdRegistro);
        }

        private void CboMeses_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }


            dtRegistros = objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt32(CboMeses.SelectedValue));    // CARGAMOS LOS DATOS DEL FORMULARIO
            STU_SISTEMA.MESTRABAJO = Convert.ToInt32(CboMeses.SelectedValue);
            ListarItems();

            if (dtRegistros.Rows.Count == 0)
            {
                DialogResult Rpta = MessageBox.Show("No se han encontrado registros, ¿ Desea agregar uno ahora ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (DialogResult.Yes == Rpta)
                {
                    //Nuevo();
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
        private void CmdCan_Click(object sender, EventArgs e)
        {
            ToolHerramientas.Enabled = true;
            Tab1.Enabled = true;
            panel6.Visible = false;
        }
        private void modificarNumeracionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModicarNumeracion();
        }
        void ModicarNumeracion()
        {
            if (dtRegistros.Rows.Count == 0)
            {
                MessageBox.Show("No hay registros para modificar", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            ToolHerramientas.Enabled = false;
            Tab1.Enabled = false;
            panel6.Top = (this.Height - panel6.Height) / 2;
            panel6.Left = (this.Width - panel6.Width) / 2;
            panel6.Visible = true;
        }
        private void ToolSDDModificar_Click(object sender, EventArgs e)
        {
            //Modificar();
        }
        private void modificarRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Modificar();
        }

        private void TxtNumMod_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                string strCad = "0000000000" + TxtNumMod.Text;

                TxtNumMod.Text = strCad.Substring(strCad.Length - 10, 10);
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

        private void TxtNumEmp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                string strCad = "0000000000" + TxtNumEmp.Text;

                TxtNumEmp.Text = strCad.Substring(strCad.Length - 10, 10);
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

        private void CmdIni_Click(object sender, EventArgs e)
        {
            CN_coo_cargoscab funCarCab = new CN_coo_cargoscab();
            DataTable dtResult = new DataTable();
            funCarCab.mysConec = mysConec;
            int n_numeracion = Convert.ToInt32(TxtNumMod.Text);
            int n_newnumeracion = Convert.ToInt32(TxtNumEmp.Text);
            int n_row = 0;
            int intIdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            funCarCab.Consulta1(intIdRegistro, n_numeracion);
            dtResult = funCarCab.dtCargosCab;
            
            for (n_row = 0; n_row <= dtResult.Rows.Count - 1; n_row++)
            {
                dtResult.Rows[n_row]["c_numdoc"] = n_newnumeracion.ToString("0000000000");
                n_newnumeracion = n_newnumeracion + 1;
            }
                        
            if (funCarCab.ActualizarNumeracion(dtResult) == true)
            { 
                MessageBox.Show("La numeracion se actualizo con exito", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            CmdCan_Click(sender, e);
            Tab1.SelectedIndex = 0;
        }
    }
}
