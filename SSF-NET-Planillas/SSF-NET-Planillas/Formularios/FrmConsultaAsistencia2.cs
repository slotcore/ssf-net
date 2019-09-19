using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Gestion;
using SIAC_Negocio.Maestros;
using SIAC_Negocio.Planilla;
using SIAC_Negocio.Sistema;
using SIAC_Objetos.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SSF_NET_Planillas.Formularios
{
    public partial class FrmConsultaAsistencia2 : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        // OBJETOS LOCALES
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();
        CN_mae_meses objMeses = new CN_mae_meses();

        CN_TEMPUS_marcacion objCabecera = new CN_TEMPUS_marcacion();                            // CABECERA DEL REGISTRO

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        // ENTIDADES LOCALES
        BE_GES_PLANVENTAS BE_CABECERA = new BE_GES_PLANVENTAS();
        List<BE_GES_PLANVENTASDET> LS_DETALLE = new List<BE_GES_PLANVENTASDET>();

        // DATATABLE LOCALES
        DataTable dtCabecera = new DataTable();                                        // CABECERA DEL REGISTRO
        DataTable dtDetalle = new DataTable();                                         // DETALLE DEL REGISTRO
        DataTable dtForm = new DataTable();
        DataTable dtMeses = new DataTable();
        DataTable dtAnosTra = new DataTable();
        DataTable dtEmpresas = new DataTable();
        
        // VARIABLES LOCALES
        int n_NumFilasDocumento = 30;                                                   // LE INDICAMOS AL FORMULARIO EL NUMERO MAXIMO DE FILAS PARA EL DETALLE
        string[,] arrCabeceraDg1 = new string[7, 4];                                    // ARRAY PARA MOSTRAR LAS COLUMNAS DEL DATAGRID PRINCIPAL
        string[,] arrCabeceraFlex1 = new string[3, 5];
        bool booSeEjecuto = false;
        int n_idformulario = 26;  
        public FrmConsultaAsistencia2()
        {
            InitializeComponent();
        }
        private void FrmConsultaAsistencia2_Load(object sender, EventArgs e)
        {
            CargarCombos();
            ConfigurarFormulario();
        }
        void CargarCombos()
        {
            DataTableCargar();
            funDatos.ComboBoxCargarDataTable(CboMeses, dtMeses, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboAño, dtAnosTra, "IDANOTRA", "ANOTRA");
            funDatos.ComboBoxCargarDataTable(CboEmpresa, dtEmpresas, "EMPRESA", "NOMBRE");
        }
        void Sizer_Dimensionar(C1.Win.C1Sizer.C1Sizer objSizer, int n_Ancho, int n_Alto)
        {
            objSizer.Width = n_Ancho;
            objSizer.Height = n_Alto - 3;
            
            panel1.Width = objSizer.Grid.Columns[1].Size - 10;
            panel1.Height = objSizer.Grid.Rows[1].Size - 20;

            FgItems.Width = panel1.Width;
            FgItems.Height = panel1.Height - 56;

        }
        void Sizer_Posicionar(C1.Win.C1Sizer.C1Sizer objSizer, int n_PosX, int n_PosY)
        {
            objSizer.Left = n_PosX;
            objSizer.Top = n_PosY;
        }
        void ConfigurarFormulario()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Height = 587;
            this.Width = 1000;
            Sizer_Dimensionar(c1Sizer1, this.Width - 16, this.Height - 80);
            Sizer_Posicionar(c1Sizer1, 1, 41);
            ChkOpcion.Checked = false;

            arrCabeceraFlex1[0, 0] = "D.N.I.";
            arrCabeceraFlex1[0, 1] = "80";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_tipexides";

            arrCabeceraFlex1[1, 0] = "Empleado";
            arrCabeceraFlex1[1, 1] = "200";
            arrCabeceraFlex1[1, 2] = "C";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_itedes";

            arrCabeceraFlex1[2, 0] = "Area";
            arrCabeceraFlex1[2, 1] = "100";
            arrCabeceraFlex1[2, 2] = "C";
            arrCabeceraFlex1[2, 3] = "";
            arrCabeceraFlex1[2, 4] = "c_itepredes";

            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtDetalle, 2, false);
            FgItems.Rows.Count = FgItems.Rows.Count + n_NumFilasDocumento;                  // INDICAMOS EL NUMERO DE FILAS PARA EL DETALLE
            this.Text = dtForm.Rows[0]["c_titfor"].ToString() + " - Materia Prima";
            CboMeses.SelectedValue = STU_SISTEMA.MESTRABAJO;
            FgItems.Rows.Count = 2;

            ToolTip miTool = new ToolTip();
            miTool.AutoPopDelay = 4000;
            miTool.InitialDelay = 1000;
            miTool.ReshowDelay = 500;
            miTool.ShowAlways = true;
            miTool.SetToolTip(CmdBuscar, "Buscar");
            miTool.SetToolTip(CmdBuscar, "CmdActEmp");
        }
        void DataTableCargar()
        {
            objMeses.mysConec = mysConec;
            dtMeses = objMeses.Listar();
                        
            objCabecera.mysConec = mysConec;
            dtAnosTra = objCabecera.AnosTrabajo();
            dtEmpresas = objCabecera.Empresas();

            objForm.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            dtForm = objForm.TraerRegistro(n_idformulario);

            objFormVis.mysConec = mysConec;                                 // CARGAMOS EL ARRAY CON LOS DATOS PARA LA VISTA DE DgLista
            objFormVis.ObtenerCabeceraLista(n_idformulario, ref arrCabeceraDg1);
        }
        private void FrmConsultaAsistencia2_Activated(object sender, EventArgs e)
        {
            if (booSeEjecuto == false)
            {
                OptImp1.Checked = true;
                booSeEjecuto = true;
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
        void Cancelar()
        {
            ActivarTool();
        }

        private void CmdBuscar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(CboMeses.SelectedValue) == 0)
            {
                MessageBox.Show("No se ha especificado el mes a consultar", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboMeses.Focus();
                return;
            }

            if (Convert.ToInt16(CboAño.SelectedValue) == 0)
            {
                MessageBox.Show("No se ha especificado el año de trabajo", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboAño.Focus();
                return;
            }

            if (Convert.ToString(CboEmpresa.SelectedValue) == "")
            {
                MessageBox.Show("No se ha especificado la empresa que desea consultar", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboEmpresa.Focus();
                return;
            }

            DataTable dtResultIng = new DataTable();
            DataTable dtResultSal = new DataTable();
            int n_mes = Convert.ToInt16(CboMeses.SelectedValue);
            int n_ano = Convert.ToInt16(CboAño.SelectedValue);
            string c_codempresa = Convert.ToString(CboEmpresa.SelectedValue);

            objCabecera.mysConec = mysConec;
            dtResultIng = objCabecera.ListarIngresos(n_ano, n_mes, c_codempresa);
            dtResultSal = objCabecera.ListarSalidas(n_ano, n_mes, c_codempresa);
            FgItems.Rows.Count = 2;
            if (dtResultIng.Rows.Count != 0)
            {
                MostrarDatos(dtResultIng, dtResultSal);
            }
            else
            {
                MessageBox.Show("! No se ha encontrado marcaciones con los criterios especificados ¡.. Posiblemente no hay empleados activos, pique en el boton Activar Empleados para activarlos", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            dtResultIng = null;
            dtResultSal = null;
        }
        void MostrarDatos(DataTable dtIngresos, DataTable dtSalidas)
        {
            int n_fil = 0;
            int n_col = 0;
            int n_reg = 0;
            string c_nomcol = "";
            int n_columna = 0;
            C1.Win.C1FlexGrid.CellRange rng;

            n_fil = 2;
            FgItems.Cols.Count = 3;
            FgItems.Rows.Count = 2;
            FgItems.Cols.Count = 66;
            FgItems.Rows.Count = 2;
            n_col = 6;
            for (n_reg = 0; n_reg <= dtIngresos.Rows.Count - 1; n_reg++)
            {
                FgItems.Rows.Count = FgItems.Rows.Count + 1;
                FgItems.SetData(n_fil, 1, dtIngresos.Rows[n_reg]["TARJETA_TMP"].ToString());
                FgItems.SetData(n_fil, 2, dtIngresos.Rows[n_reg]["APENOM"].ToString());
                FgItems.SetData(n_fil, 3, dtIngresos.Rows[n_reg]["AREA"].ToString());

                n_columna = 4;
                for (n_col = 6; n_col <= dtIngresos.Columns.Count - 1; n_col++)
                {
                    c_nomcol = dtIngresos.Columns[n_col].ColumnName;
                    if (n_reg == 0)
                    {
                        FgItems.Cols[n_columna].Width = 45;
                        FgItems.Cols[n_columna + 1].Width = 45;
                        FgItems.Cols[n_columna].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
                        FgItems.Cols[n_columna + 1].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;

                        FgItems.Rows[0].AllowMerging = true;
                        rng = FgItems.GetCellRange(0, n_columna, 0, n_columna + 1);

                        if (c_nomcol.Length == 5)
                        {
                            rng.Data = "DIA " + c_nomcol.Substring(4, 1);
                        }
                        else
                        {
                            rng.Data = "DIA " + c_nomcol.Substring(4, 2);
                        }

                        FgItems.SetData(1, n_columna, "Ingreso");
                        FgItems.SetData(1, n_columna + 1, "Salida");
                    }
                    FgItems.SetData(n_fil, n_columna, dtIngresos.Rows[n_reg][c_nomcol].ToString());
                    n_columna = n_columna + 1;

                    if (ChkOpcion.Checked == true)                // AQUI IMPRIME EL FORMTATO PREPARADO MISTRANDO LAS HORAS FORMALES DE TRABAJO
                    {
                        string c_newhor;
                        string c_ing = "";
                        string c_sal = "";

                        if (dtIngresos.Rows[n_reg][c_nomcol].ToString() != "")
                        {
                            c_ing = dtIngresos.Rows[n_reg][c_nomcol].ToString();
                        }
                        if (dtSalidas.Rows[n_reg][c_nomcol].ToString() != "")
                        {
                            c_sal = dtSalidas.Rows[n_reg][c_nomcol].ToString();
                        }
                        c_newhor = Hora_HallarSalidaFormal(c_sal, c_ing, 9);
                        FgItems.SetData(n_fil, n_columna, c_newhor);
                    }
                    else
                    {
                        FgItems.SetData(n_fil, n_columna, dtSalidas.Rows[n_reg][c_nomcol].ToString());  // AQUI IMPRIME LAS HORAS REALMENTE TRABAJADAS
                    }
                    n_columna = n_columna + 1;
                }
                n_fil = n_fil + 1;
            }
        }
        string Hora_HallarSalidaFormal(string c_HoraSalida, string c_Hora_Ingreso, int n_NumeroHoras)
        {
            int n_totalhoras = 0;
            int n_diferenciaHoras = 0;
            string n_horanueva = "";

            if ((c_HoraSalida == "" ) || (c_Hora_Ingreso == ""))
            {
                n_horanueva = "";
            }
            else
            {
                DateTime h_HoraSalida = Convert.ToDateTime(c_HoraSalida);
                DateTime h_Hora_Ingreso = Convert.ToDateTime(c_Hora_Ingreso);

                n_totalhoras = (h_HoraSalida - h_Hora_Ingreso).Hours;
                if (n_totalhoras != 0)
                {
                    if (n_totalhoras > n_NumeroHoras)
                    {
                        n_diferenciaHoras = (n_totalhoras - n_NumeroHoras);
                        string c_hora;
                        string c_minuto;
                        c_hora = (h_HoraSalida.Hour - n_diferenciaHoras).ToString("00");
                        c_minuto = h_HoraSalida.Minute.ToString("00");

                        n_horanueva = c_hora + ":" + c_minuto;
                    }
                    else
                    {
                        n_horanueva = h_HoraSalida.ToString("HH:mm");
                    }
                }
            }
            return n_horanueva;
        }
        private void c1Sizer1_Click(object sender, EventArgs e)
        {

        }
        private void FrmConsultaAsistencia2_Resize(object sender, EventArgs e)
        {
            Sizer_Dimensionar(c1Sizer1, this.Width - 16, this.Height - 75);
            Sizer_Posicionar(c1Sizer1, 1, 42);
        }

        private void ToolSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ToolCancelar_Click(object sender, EventArgs e)
        {
            if (FgItems.Rows.Count == 2)
            {
                MessageBox.Show("No hay datos para exportar", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CmdBuscar.Focus();
                return;
            }
            FgItems.SaveGrid("c:\\SSF-NET\\asistencia-" + CboMeses.Text+".xls",C1.Win.C1FlexGrid.FileFormatEnum.Excel,C1.Win.C1FlexGrid.FileFlags.AsDisplayed);
        }

        private void ToolImprimir_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(CboMeses.SelectedValue) == 0)
            {
                MessageBox.Show("No se ha especificado el mes a consultar", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboMeses.Focus();
                return;
            }

            if (Convert.ToInt16(CboAño.SelectedValue) == 0)
            {
                MessageBox.Show("No se ha especificado el año de trabajo", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboAño.Focus();
                return;
            }

            if (Convert.ToString(CboEmpresa.SelectedValue) == "")
            {
                MessageBox.Show("No se ha especificado la empresa que desea consultar", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboEmpresa.Focus();
                return;
            }

            int n_opcion = 0;
            objCabecera.STU_SISTEMA = STU_SISTEMA;
            if (OptImp1.Checked == true) { n_opcion = 1; }
            if (OptImp2.Checked == true) { n_opcion = 2; }
            if (OptImp3.Checked == true) { n_opcion = 3; }

            objCabecera.RptAsistencia(n_opcion, Convert.ToInt16(CboAño.SelectedValue), Convert.ToInt16(CboMeses.SelectedValue), Convert.ToString(CboEmpresa.SelectedValue));

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmActivarEmpleado xFrm = new FrmActivarEmpleado();
            xFrm.c_CodEmpresa = CboEmpresa.SelectedValue.ToString();
            xFrm.mysConec = mysConec;
            xFrm.ShowDialog();
            if (xFrm.b_Grabo == true)
            {
                CmdBuscar_Click(sender, e);
            }
            xFrm.Close();
        }
    }
}
