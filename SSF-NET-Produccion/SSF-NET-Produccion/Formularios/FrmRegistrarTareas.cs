using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Produccion;
using SIAC_Entidades.Maestros;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Planilla;
using SIAC_Negocio.Maestros;
using SIAC_Objetos.Sistema;
using SIAC_Negocio.Produccion;
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

namespace SSF_NET_Produccion.Formularios
{
    public partial class FrmRegistrarTareas : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_pro_producciontareas objRegistros = new CN_pro_producciontareas();

        CN_pro_produccion objProduccion = new CN_pro_produccion();
        CN_pro_producciontareas objProdTar = new CN_pro_producciontareas();
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_alm_inventariounimed objAlmUniMed = new CN_alm_inventariounimed();
        CN_mae_meses objMeses = new CN_mae_meses();
        CN_alm_inventario objItems = new CN_alm_inventario();
        CN_pro_tareas objTareas = new CN_pro_tareas();
        CN_pla_empleados objPersonal = new CN_pla_empleados();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_PRO_PRODUCCION BE_Produccion = new BE_PRO_PRODUCCION();
        List<BE_PRO_PRODUCCIONTAREAS> lstTar = new List<BE_PRO_PRODUCCIONTAREAS>();
        List<BE_PRO_PRODUCCIONTAREASDET> lstTarDet = new List<BE_PRO_PRODUCCIONTAREASDET>();

        // DATATABLE LOCALES
        DataTable dtLista = new DataTable();           // LISTARA TODOS LOS REGISTROS DEL MES ACTUAL
        DataTable dtRegistro = new DataTable();        // ALMACENARA LOS DATOS DEL REGISTRO SELECCIONADO
        DataTable dtItems = new DataTable();
        DataTable dtForm = new DataTable();
        DataTable dtAlmUniMed = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtTareas = new DataTable();
        DataTable dtPersonal = new DataTable();

        // VARIABLES LOCALES
        int n_QueHace = 3;                                                               // INDICA EN QUE ESTADO SE ENCUENTRA EL FORMULARIO
        string[,] arrCabeceraDg1 = new string[8, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[4, 5];
        string[,] arrCabeceraFlexTar = new string[9, 5];
        string[,] arrCabeceraFlexPer = new string[6, 5];

        int n_FilaTareaActual = 0;
        bool booSeEjecuto = false;
        bool booAgregando = false;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmRegistrarTareas()
        {
            InitializeComponent();
        }

        private void FrmRegistrarTareas_Load(object sender, EventArgs e)
        {
            booAgregando = true;
            CargarCombos();
            ConfigurarFormulario();
            booAgregando = false;
        }
        void CargarCombos()
        {
            DataTableCargar();

            funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboUniMed, dtAlmUniMed, "n_id", "c_abrpre");
        }
        void ConfigurarFormulario()
        {
            this.Height = 650;
            this.Width = 970;
            
            Tab_Dimensionar(Tab1, this.Height - 83, this.Width - 18);
            Tab_Posicionar(Tab1, 1, 42);
            Tab1.SelectedIndex = 0;
            LblTitulo2.Text = "DETALLE DEL REGISTRO";

            this.Text = dtForm.Rows[0]["c_titfor"].ToString();
            //CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;

            // FLEX GRID DE LOS TAREAS
            arrCabeceraFlexTar[0, 0] = "Tarea";
            arrCabeceraFlexTar[0, 1] = "200";
            arrCabeceraFlexTar[0, 2] = "C";
            arrCabeceraFlexTar[0, 3] = "";
            arrCabeceraFlexTar[0, 4] = "";

            arrCabeceraFlexTar[1, 0] = "Fch. Proceso";
            arrCabeceraFlexTar[1, 1] = "70";
            arrCabeceraFlexTar[1, 2] = "F";
            arrCabeceraFlexTar[1, 3] = "";
            arrCabeceraFlexTar[1, 4] = "";

            arrCabeceraFlexTar[2, 0] = "Hora Inicio";
            arrCabeceraFlexTar[2, 1] = "50";
            arrCabeceraFlexTar[2, 2] = "H";
            arrCabeceraFlexTar[2, 3] = "";
            arrCabeceraFlexTar[2, 4] = "";

            arrCabeceraFlexTar[3, 0] = "Hora Termino";
            arrCabeceraFlexTar[3, 1] = "50";
            arrCabeceraFlexTar[3, 2] = "H";
            arrCabeceraFlexTar[3, 3] = "";
            arrCabeceraFlexTar[3, 4] = "";

            arrCabeceraFlexTar[4, 0] = "Nº Personas";
            arrCabeceraFlexTar[4, 1] = "70";
            arrCabeceraFlexTar[4, 2] = "N";
            arrCabeceraFlexTar[4, 3] = "";
            arrCabeceraFlexTar[4, 4] = "";

            arrCabeceraFlexTar[5, 0] = "Unidad Medida";
            arrCabeceraFlexTar[5, 1] = "60";
            arrCabeceraFlexTar[5, 2] = "C";
            arrCabeceraFlexTar[5, 3] = "";
            arrCabeceraFlexTar[5, 4] = "";

            arrCabeceraFlexTar[6, 0] = "Cantidad";
            arrCabeceraFlexTar[6, 1] = "70";
            arrCabeceraFlexTar[6, 2] = "D";
            arrCabeceraFlexTar[6, 3] = "";
            arrCabeceraFlexTar[6, 4] = "";

            arrCabeceraFlexTar[7, 0] = "Costo Tarea";
            arrCabeceraFlexTar[7, 1] = "70";
            arrCabeceraFlexTar[7, 2] = "D";
            arrCabeceraFlexTar[7, 3] = "";
            arrCabeceraFlexTar[7, 4] = "";

            arrCabeceraFlexTar[8, 0] = "Id Tarea";
            arrCabeceraFlexTar[8, 1] = "0";
            arrCabeceraFlexTar[8, 2] = "N";
            arrCabeceraFlexTar[8, 3] = "";
            arrCabeceraFlexTar[8, 4] = "";

            funFlex.FlexMostrarDatos(FgTar, arrCabeceraFlexTar, dtLista, 3, false);

            arrCabeceraFlexPer[0, 0] = "Nº";
            arrCabeceraFlexPer[0, 1] = "30";
            arrCabeceraFlexPer[0, 2] = "N";
            arrCabeceraFlexPer[0, 3] = "";
            arrCabeceraFlexPer[0, 4] = "";

            arrCabeceraFlexPer[1, 0] = "Persona";
            arrCabeceraFlexPer[1, 1] = "300";
            arrCabeceraFlexPer[1, 2] = "C";
            arrCabeceraFlexPer[1, 3] = "";
            arrCabeceraFlexPer[1, 4] = "";

            arrCabeceraFlexPer[2, 0] = "Hora Inicio";
            arrCabeceraFlexPer[2, 1] = "60";
            arrCabeceraFlexPer[2, 2] = "H";
            arrCabeceraFlexPer[2, 3] = "";
            arrCabeceraFlexPer[2, 4] = "";

            arrCabeceraFlexPer[3, 0] = "Hora Termino";
            arrCabeceraFlexPer[3, 1] = "70";
            arrCabeceraFlexPer[3, 2] = "H";
            arrCabeceraFlexPer[3, 3] = "";
            arrCabeceraFlexPer[3, 4] = "";

            arrCabeceraFlexPer[4, 0] = "Cantidad";
            arrCabeceraFlexPer[4, 1] = "70";
            arrCabeceraFlexPer[4, 2] = "D";
            arrCabeceraFlexPer[4, 3] = "";
            arrCabeceraFlexPer[4, 4] = "";

            arrCabeceraFlexPer[5, 0] = "id Persoma";
            arrCabeceraFlexPer[5, 1] = "0";
            arrCabeceraFlexPer[5, 2] = "N";
            arrCabeceraFlexPer[5, 3] = "";
            arrCabeceraFlexPer[5, 4] = "";

            funFlex.FlexMostrarDatos(FgPer, arrCabeceraFlexPer, dtLista, 3, false);

            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;
        }
        void DataTableCargar()
        {
            objProduccion.mysConec = mysConec;
            if (objProduccion.Consulta2(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO) == true) 
            {
                dtLista = objProduccion.dtListar;
            }

            objFormVis.mysConec = mysConec;                                      // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(45, ref arrCabeceraDg1);

            objForm.mysConec = mysConec;                                         // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(45);

            objAlmUniMed.mysConec = mysConec;
            dtAlmUniMed = objAlmUniMed.Listar();

            objItems.mysConec = mysConec;
            dtItems =  objItems.Listar(STU_SISTEMA.EMPRESAID);

            objTareas.mysConec = mysConec;
            dtTareas = objTareas.Listar(STU_SISTEMA.EMPRESAID);

            objPersonal.mysConec = mysConec;
            if (objPersonal.Consulta1(STU_SISTEMA.EMPRESAID) == true)
            {
                dtPersonal = objPersonal.dtLista;
            }

            objMeses.mysConec = mysConec;
            dtMeses = objMeses.Listar();
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
            string c_dato = "";
            int n_row = 0;
            DataTable dtResult = new DataTable();

            lstTarDet.Clear();
            FgTar.Rows.Count = 2;
            FgPer.Rows.Count = 2;

            objProduccion.mysConec = mysConec;
            objProduccion.TraerRegistro(n_IdRegistro);

            objProdTar.mysConec = mysConec;
            objProdTar.Listar(n_IdRegistro);
            dtResult = objProdTar.dtListar;
            lstTarDet = objProdTar.lstTareaDet;

            if (objProduccion.booOcurrioError == false)
            {
                BE_Produccion = objProduccion.EntProduccion;
                
                TxtNumPro.Text = BE_Produccion.c_numser + "-" + BE_Produccion.c_numdoc;

                c_dato = BE_Produccion.n_idpro.ToString();
                c_dato = funDatos.DataTableBuscar(dtItems, "n_id", "c_despro", c_dato, "N").ToString();
                TxtPro.Text = c_dato;

                TxtCan.Text = BE_Produccion.n_canpro.ToString("0.00");
                TxtNumLot.Text = BE_Produccion.c_numlot;
                CboUniMed.SelectedValue = BE_Produccion.n_idunimed;
                TxtFchPro.Text = BE_Produccion.d_fchpro.ToString("dd/MM/yyyy");
            }

            // MOSTRAMOS LAS TAREAS DE LA PRODUCCION
            if (dtResult.Rows.Count != 0)
            {
                for (n_row = 0; n_row <= dtResult.Rows.Count - 1; n_row++)
                {
                    FgTar.Rows.Count = FgTar.Rows.Count + 1;

                    c_dato = dtResult.Rows[n_row]["n_idtar"].ToString();
                    FgTar.SetData(FgTar.Rows.Count - 1, 9, c_dato);
                    c_dato = funDatos.DataTableBuscar(dtTareas, "n_id", "c_des", c_dato, "N").ToString();
                    FgTar.SetData(FgTar.Rows.Count - 1, 1, c_dato);

                    c_dato = Convert.ToDateTime(dtResult.Rows[n_row]["d_fchfab"]).ToString("dd/MM/yyyy");
                    FgTar.SetData(FgTar.Rows.Count - 1, 2, c_dato);

                    c_dato = dtResult.Rows[n_row]["c_horini"].ToString();
                    FgTar.SetData(FgTar.Rows.Count - 1, 3, c_dato);

                    c_dato = dtResult.Rows[n_row]["c_horter"].ToString();
                    FgTar.SetData(FgTar.Rows.Count - 1, 4, c_dato);

                    c_dato = Convert.ToInt32(dtResult.Rows[n_row]["n_numper"].ToString()).ToString("0");
                    FgTar.SetData(FgTar.Rows.Count - 1, 5, c_dato);

                    c_dato = funDatos.DataTableBuscar(dtTareas, "n_id", "c_unimedabr", c_dato, "N").ToString();
                    FgTar.SetData(FgTar.Rows.Count - 1, 6, c_dato);                     // UNIDAD DE MEDIDA

                    c_dato = dtResult.Rows[n_row]["n_can"].ToString();
                    FgTar.SetData(FgTar.Rows.Count - 1, 7, c_dato);

                }
            }

            int n_numeracion = 0;
            for(n_row =0; n_row <= lstTarDet.Count-1;n_row++)
            {
                n_numeracion = n_numeracion + 1;
                lstTarDet[n_row].n_idpro=n_numeracion;
            }
            

            MostrarPersonas(2);
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
            TxtNumPro.Focus();
        }
        void Blanquea()
        {
            TxtNumPro.Text = "";
            TxtPro.Text = "";
            TxtCan.Text = "";
            CboUniMed.SelectedValue = 0;
            TxtNumLot.Text = "";
        }
        void Bloquea()
        {
            TxtNumPro.Enabled = !TxtNumPro.Enabled;
            TxtPro.Enabled = !TxtPro.Enabled;
            TxtCan.Enabled = !TxtCan.Enabled;
            CboUniMed.Enabled = !CboUniMed.Enabled;
            TxtNumLot.Enabled = !TxtNumLot.Enabled;
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
            FgTar.AllowEditing = true;
            FgPer.AllowEditing = true;
            TxtNumPro.Focus();
        }
        bool EliminarRegistro()
        {
            bool booResult = false;
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());       // OBTENEMOS EL ID DEL REGISTRO QUE SE DESEA ELIMINAR

            DialogResult Rpta = MessageBox.Show("Esta seguro de eliminar el registro seleccionado", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                objRegistros.mysConec = mysConec;
                if (objRegistros.Eliminar(intIdRegistro) == true)
                {
                    booResult = true;
                    MessageBox.Show("¡ El registro se elimino con exito !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    // VOLVEMOS A CARGAR EL DATATABLE dtItems CON LOS DATOS DEL SERVIDOR
                    objProduccion.mysConec = mysConec;
                    if (objProduccion.Consulta2(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO) == true)
                    {
                        dtLista = objProduccion.dtListar;
                    }
                   //objRegistros.mysConec = mysConec;

                   // if (objRegistros.Listar(STU_SISTEMA.EMPRESAID) == true)
                   // {
                   //     dtLista = objRegistros.dtListar;
                   // }
                    
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
                //booResultado = objRegistros.Insertar(BE_Registro);
            }

            if (n_QueHace == 2)
            {
                objRegistros.mysConec = mysConec;
                booResultado = objRegistros.Actualizar(lstTar, lstTarDet);
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
            int n_dato = 0;
            double n_valor = 0;
            string c_dato = "";
            int intIdRegistro = Convert.ToInt16(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

            lstTar.Clear();

            for (n_row = 2; n_row <= FgTar.Rows.Count-1; n_row++)
            {
                BE_PRO_PRODUCCIONTAREAS entTarea = new BE_PRO_PRODUCCIONTAREAS();

                entTarea.n_idpro = intIdRegistro;
                
                n_dato = Convert.ToInt32(FgTar.GetData(n_row,9));
                entTarea.n_idtar = n_dato;

                n_valor = Convert.ToDouble(FgTar.GetData(n_row,7));
                entTarea.n_can = n_valor;

                c_dato =  FgTar.GetData(n_row,3).ToString();
                entTarea.c_horini = c_dato;

                c_dato =  FgTar.GetData(n_row,4).ToString();
                entTarea.c_horter = c_dato;

                c_dato =  FgTar.GetData(n_row,2).ToString();
                entTarea.d_fchfab = Convert.ToDateTime(c_dato);

                n_dato = Convert.ToInt32(FgTar.GetData(n_row, 5));
                entTarea.n_numper = n_dato;

                n_valor = Convert.ToDouble(FgTar.GetData(n_row,9));
                entTarea.n_impcostar = n_valor;

                lstTar.Add(entTarea);
            }   
        }
        bool CamposOK()
        {
            bool booEstado = true;
            
            int n_row = 0;
            string c_dato = "";
            string c_nomtar = "";
            // VERIFICAMOS QUE LOS DATOS DE LAS TAREAS ESTEN COMPLETOS
            for (n_row = 2; n_row <= FgTar.Rows.Count - 1; n_row++)
            {
                c_nomtar = FgTar.GetData(n_row, 1).ToString();
                c_dato = FgTar.GetData(n_row, 3).ToString();
                if (funFunciones.NulosC(c_dato) == "")
                {
                    MessageBox.Show("¡ No ha especificado la hora de inicio de la tarea " + c_nomtar + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booEstado = false;
                    FgTar.Focus();
                    return booEstado;
                }
                c_dato = FgTar.GetData(n_row, 4).ToString();
                if (funFunciones.NulosC(c_dato) == "")
                {
                    MessageBox.Show("¡ No ha especificado la hora de termino de la tarea " + c_nomtar + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booEstado = false;
                    FgTar.Focus();
                    return booEstado;
                }
                c_dato = FgTar.GetData(n_row, 5).ToString();
                if (funFunciones.NulosC(c_dato) == "")
                {
                    MessageBox.Show("¡ No ha especificado el numero de personas que intervienen en la tarea " + c_nomtar + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booEstado = false;
                    FgTar.Focus();
                    return booEstado;
                }
                //c_dato = FgTar.GetData(n_row, 7).ToString();
                //if (Convert.ToDouble(c_dato) == 0)
                //{
                //    MessageBox.Show("¡ No ha especificado la cantidad procesada en la tarea " + c_nomtar + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //    booEstado = false;
                //    FgTar.Focus();
                //    return booEstado;
                //}
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
                objProduccion.mysConec = mysConec;
                if (objProduccion.Consulta2(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO) == true)
                {
                    dtLista = objProduccion.dtListar;
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
            this.Close();
        }
        private void FrmRegistrarTareas_Activated(object sender, EventArgs e)
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
                int intIdRegistro = Convert.ToInt32(DgLista.Columns[0].CellValue(DgLista.Row).ToString());

                if (n_QueHace != 1)
                {
                    booAgregando = true;
                    VerRegistro(intIdRegistro);
                    booAgregando = false;
                }
            }
        }
        private void FrmRegistrarTareas_Resize(object sender, EventArgs e)
        {
            Tab_Dimensionar(Tab1, this.Height - 82, this.Width - 18);
        }
        private void ToolHerramientas_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void CmdDelPer_Click(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (booAgregando == true) { return; }
            if (FgTar.Rows.Count == 2) { return; }

            int n_row = 0;
            int n_idper = 0;
            string c_dato = "";
            int n_Fila = 0;

            n_Fila = FgPer.Row;

            for (n_row = 0; n_row <= lstTarDet.Count - 1; n_row++)
            {
                n_idper = Convert.ToInt32(FgPer.GetData(n_Fila, 6).ToString());
                c_dato = FgPer.GetData(n_Fila, 1).ToString();

                if ((lstTarDet[n_row].n_idper == n_idper) && (lstTarDet[n_row].n_id == Convert.ToInt32(c_dato)))
                {
                    lstTarDet.RemoveAt(n_row);
                    break;
                }
            }

            FgPer.RemoveItem(FgPer.Row);
        }
        private void FgTar_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (booAgregando == true) { return; }
            if (n_QueHace == 3) { FgTar.AllowEditing = false; return; }
            

            //if (FgTar.Col == 5)
            //{ 
            //    int n_numper = 0;
            //    n_numper = Convert.ToInt32(FgTar.GetData(FgTar.Row, 5).ToString());
            //    FgPer.Rows.Count = 2;
            //    int n_row = 0;
            //    string c_dato = FgTar.GetData(FgTar.Row, 3).ToString();

            //    booAgregando = true;
            //    for (n_row = 0; n_row <= n_numper - 1; n_row++)
            //    {
            //        FgPer.Rows.Count = FgPer.Rows.Count + 1;
            //        FgPer.SetData(FgPer.Rows.Count - 1, 2, c_dato);
            //    }
            //    n_FilaTareaActual = FgTar.Row;
            //    booAgregando = false;
            //}
        }
        private void FgPer_EnterCell(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { FgPer.AllowEditing = false; return; }
            if (booAgregando == true) { return; }

            if (FgPer.Col == 2)
            {
                FgPer.AllowEditing = true;
                funFlex.FlexColumnaCombo(FgPer, dtPersonal, "c_apenom", 2);
            }
            if ((FgPer.Col == 3) || (FgPer.Col == 4) || (FgPer.Col == 5))
            {
                FgPer.AllowEditing = true;
            }
        }
        private void FgPer_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (booAgregando == true) { return; }

            if (funFunciones.NulosC(FgPer.GetData(FgPer.Row, 2)) == "")
            {
                return;
            }
            string c_dato = "";

            if (FgPer.Col == 2)
            {
                booAgregando = true;
                c_dato = FgPer.GetData(FgPer.Row, 2).ToString();
                c_dato = funDatos.DataTableBuscar(dtPersonal, "c_apenom", "n_id", c_dato, "C").ToString();
                FgPer.SetData(FgPer.Row, 6, c_dato);
                booAgregando = false;
                
            }

            AgregarPersonaaLista(FgPer.Row);
        }
        void AgregarPersonaaLista(int n_Fila)
        { 
            int n_row = 0;
            int n_idper = 0;
            int n_idtar = 0;
            double n_cantidad = 0;
            string c_dato = "";
            bool b_SeEncontro = false;

            if (funFunciones.NulosC(FgPer.GetData(n_Fila, 6)) == "")
            {
                return;
            }

            for (n_row= 0;n_row <= lstTarDet.Count-1; n_row++)
            {
                n_idper = Convert.ToInt32(FgPer.GetData(n_Fila,6).ToString());
                c_dato = FgPer.GetData(n_Fila, 1).ToString();

                if ((lstTarDet[n_row].n_idper == n_idper) && (lstTarDet[n_row].n_id == Convert.ToInt32(c_dato)))
                {
                    lstTarDet[n_row].n_id = Convert.ToInt32(funFunciones.NulosN(FgPer.GetData(n_Fila, 1)));
                    n_idtar = Convert.ToInt32(FgTar.GetData(FgTar.Row, 9).ToString());
                    lstTarDet[n_row].n_idtar =n_idtar;
                    lstTarDet[n_row].n_idper = n_idper;

                    n_cantidad=Convert.ToInt32(funFunciones.NulosN(FgPer.GetData(n_Fila, 5)));
                    lstTarDet[n_row].n_can = n_cantidad;
                    lstTarDet[n_row].c_obs = "";

                    c_dato = funFunciones.NulosC(FgPer.GetData(n_Fila, 3));
                    lstTarDet[n_row].c_horini = c_dato;

                    c_dato = funFunciones.NulosC(FgPer.GetData(n_Fila, 4));
                    lstTarDet[n_row].c_horter = c_dato;
                    lstTarDet[n_row].n_pretar = 0;
                    lstTarDet[n_row].n_imptot = 0;

                    b_SeEncontro = true;
                    break;
                }
            }

            if (b_SeEncontro == false)
            { 
                BE_PRO_PRODUCCIONTAREASDET entTarDet = new BE_PRO_PRODUCCIONTAREASDET();

                entTarDet.n_id = Convert.ToInt32(FgPer.GetData(FgPer.Rows.Count - 1, 1).ToString());

                n_idtar= Convert.ToInt32(FgTar.GetData(FgTar.Row,9).ToString());
                entTarDet.n_idtar = n_idtar;

                n_idper = Convert.ToInt32(FgPer.GetData(n_Fila,6).ToString());
                entTarDet.n_idper = n_idper;

                n_cantidad = Convert.ToInt32(funFunciones.NulosN(FgPer.GetData(n_Fila, 5)));
                entTarDet.n_can = n_cantidad;
                entTarDet.c_obs = "";

                c_dato = funFunciones.NulosC(FgPer.GetData(n_Fila, 3));
                entTarDet.c_horini = c_dato;

                c_dato  = funFunciones.NulosC(FgPer.GetData(n_Fila, 4));
                entTarDet.c_horter = c_dato;
                entTarDet.n_pretar = 0;
                entTarDet.n_imptot = 0;

                lstTarDet.Add(entTarDet);
            }
        }

        private void FgTar_RowColChange(object sender, EventArgs e)
        {
            //if (n_QueHace == 3) { return; }
            if (booAgregando == true) { return; }

            MostrarPersonas(FgTar.Row);
        }
        void MostrarPersonas(int n_Fila)
        {
            int n_row = 0;
            int n_idtar = Convert.ToInt32(FgTar.GetData(n_Fila, 9));
            string c_dato = "";
            int n_numeracion =0;
            FgPer.Rows.Count = 2;

            booAgregando = true;
            for (n_row = 0; n_row <= lstTarDet.Count - 1; n_row++)
            {
                if (n_idtar == lstTarDet[n_row].n_idtar)
                {
                    n_numeracion = n_numeracion + 1;
                    FgPer.Rows.Count = FgPer.Rows.Count +1;

                    c_dato = lstTarDet[n_row].n_id.ToString();
                    FgPer.SetData(FgPer.Rows.Count - 1, 1, c_dato);

                    c_dato = lstTarDet[n_row].n_idper.ToString();
                    c_dato = funDatos.DataTableBuscar(dtPersonal, "n_id", "c_apenom", c_dato, "N").ToString();
                    FgPer.SetData(FgPer.Rows.Count - 1, 2, c_dato);

                    c_dato = lstTarDet[n_row].c_horini;
                    FgPer.SetData(FgPer.Rows.Count - 1, 3, c_dato);

                    c_dato = lstTarDet[n_row].c_horter;
                    FgPer.SetData(FgPer.Rows.Count - 1, 4, c_dato);

                    c_dato = lstTarDet[n_row].n_can.ToString("0.00");
                    FgPer.SetData(FgPer.Rows.Count - 1, 5, c_dato);

                    c_dato = lstTarDet[n_row].n_idper.ToString();
                    FgPer.SetData(FgPer.Rows.Count - 1, 6, c_dato);
                }
            }
            booAgregando = false;
        }
        private void CmdAddPer_Click(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { return; }
            if (booAgregando == true) { return; }

            //if (FgPer.Rows.Count == 2) {return;}

            int n_numper = Convert.ToInt32(FgTar.GetData(FgTar.Row, 5));

            if ((FgPer.Rows.Count - 2) >= n_numper)
            {
                MessageBox.Show("¡ El numero de tranajadores requeridos no puede ser mayor al indicado en la lsita de tareas !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            
            if (funFunciones.NulosC(FgPer.GetData(FgPer.Rows.Count - 1, 1)) == "")
            {
                return;
            }

            int n_numer = 0;

            string c_dato = "";
            if (FgPer.Rows.Count == 2)
            {
                n_numer = 0;
            }
            else
            {
                n_numer = Convert.ToInt32(funFunciones.NulosN(FgPer.GetData(FgPer.Rows.Count - 1, 1)));
            }
            

            booAgregando = true;
            FgPer.Rows.Count = FgPer.Rows.Count + 1;
            FgPer.SetData(FgPer.Rows.Count - 1, 1, n_numer + 1);

            c_dato = FgTar.GetData(FgTar.Row, 3).ToString();
            FgPer.SetData(FgPer.Rows.Count - 1, 3, c_dato);
            booAgregando = false;
        }
        private void CboMeses_SelectedValueChanged(object sender, EventArgs e)
        {
            if (booAgregando == true) { return; }
           
            STU_SISTEMA.MESTRABAJO = Convert.ToInt32(CboMeses.SelectedValue);

            objProduccion.mysConec = mysConec;
            if (objProduccion.Consulta2(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO) == true)
            {
                dtLista = objProduccion.dtListar;
            }

            //objRegistro.mysConec = mysConec;
            //dtLista = objRegistro.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt32(CboMeses.SelectedValue));    // CARGAMOS LOS DATOS DEL FORMULARIO
            STU_SISTEMA.MESTRABAJO = Convert.ToInt32(CboMeses.SelectedValue);
            ListarItems();

            if (dtLista.Rows.Count == 0)
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
        private void FgTar_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            if (e.Col == 2)
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
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
            if (e.Col == 5)
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            if (e.Col == 7)
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            if (e.Col == 8)
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void FgPer_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
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

        private void FgTar_EnterCell(object sender, EventArgs e)
        {
            if (n_QueHace == 3) { FgTar.AllowEditing = false; return; }
            if (booAgregando == true) { return; }

            if ((FgTar.Col == 2) || (FgTar.Col == 3) || (FgTar.Col == 4) || (FgTar.Col == 5) || (FgTar.Col == 6) || (FgTar.Col == 7))
            {
                FgTar.AllowEditing = true;
            }
            else
            {
                FgTar.AllowEditing = false;
            }
        }
    }
}
