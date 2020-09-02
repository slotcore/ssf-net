using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Logistica;
using SIAC_Entidades.Maestros;
using SIAC_Entidades.Almacen;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Logistica;
using SIAC_Negocio.Almacen;
using SIAC_Negocio.Contabilidad;
using SIAC_Negocio.Maestros;
using SIAC_Objetos.Sistema;
using SIAC_Negocio.Sunat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace SSF_NET_Contabilidad.Formularios
{
    public partial class FrmLibroMayor : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public int n_Libro;

        DataTable dtCuenta = new DataTable();
        DataTable dtLista = new DataTable();
        DataTable dtResumen = new DataTable();
        DataTable dtMon = new DataTable();
        DataTable dtPerIni = new DataTable();
        DataTable dtPerFin = new DataTable();

        CN_sun_tipmon objMon = new CN_sun_tipmon();
        CN_mae_meses objMes = new CN_mae_meses();
        CN_sun_libro o_Libro = new CN_sun_libro();
        CN_con_pc o_Cuenta = new CN_con_pc();
        CN_con_diario o_Diario = new CN_con_diario();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        Cls_Controles funControl = new Cls_Controles();

        string[,] arrCabeceraFlex1 = new string[3, 5];
        string[,] arrCabecera1 = new string[18, 5];
        string[,] arrCabResumen1 = new string[4, 5];
        public FrmLibroMayor()
        {
            InitializeComponent();
        }

        private void FrmLibroMayor_Load(object sender, EventArgs e)
        {
            ConfigurarFormulario();
            CargarDT();
        }
        void CargarDT()
        {
            objMon.mysConec = mysConec;
            dtMon = objMon.Listar();

            o_Cuenta.mysConec = mysConec;
            o_Cuenta.Listar(STU_SISTEMA.EMPRESAID);
            dtCuenta = o_Cuenta.dtLista;

            objMes.mysConec = mysConec;
            dtPerIni = objMes.Listar();
            dtPerIni = funDatos.DataTableFiltrar(dtPerIni, "n_id NOT IN(0,13)");

            dtPerFin = objMes.Listar();
            dtPerFin = funDatos.DataTableFiltrar(dtPerFin, "n_id NOT IN(0,13)");

            funDatos.ComboBoxCargarDataTable(CboPerIni, dtPerIni, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboPerFin, dtPerFin, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboMon, dtMon, "n_id", "c_des");

            CboMon.SelectedValue = 115;       // LA MONEDA POR EFECTO ES SOLES
        }
        void ConfigurarFormulario()
        {
            this.Height = 600;
            this.Width = 1010;

            Sz1.Left = 0;
            Sz1.Top = 41;
            Sz1.Height = this.Height - 83;
            Sz1.Width = this.Width - 18;

            LblNumReg.Text = "";
            Tab02.SelectedIndex = 0;

            Cabecera1();
            this.Text = "CONTABILIDAD - LIBRO MAYOR";
            funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 3, false);
            funFlex.FlexMostrarDatos(FgRes, arrCabResumen1, dtResumen, 3, false);
            funFlex.FlexMostrarDatos(FgCuenta, arrCabeceraFlex1, dtResumen, 1, false);
            FgCuenta.AllowEditing = true;
            FgCuenta.Cols[1].ComboList = "...";
            //Configurarcabecera1();
        }
        void Cabecera1()
        {
            // FLEX GRID DE LOS TAREAS
            arrCabecera1[0, 0] = "Nº Registro";
            arrCabecera1[0, 1] = "60";
            arrCabecera1[0, 2] = "C";
            arrCabecera1[0, 3] = "";
            arrCabecera1[0, 4] = "c_numasi";

            arrCabecera1[1, 0] = "Glosa";
            arrCabecera1[1, 1] = "200";
            arrCabecera1[1, 2] = "C";
            arrCabecera1[1, 3] = "";
            arrCabecera1[1, 4] = "c_origlo";

            arrCabecera1[2, 0] = "Libro";
            arrCabecera1[2, 1] = "80";
            arrCabecera1[2, 2] = "C";
            arrCabecera1[2, 3] = "";
            arrCabecera1[2, 4] = "c_des";

            arrCabecera1[3, 0] = "Fch. Operacion";
            arrCabecera1[3, 1] = "70";
            arrCabecera1[3, 2] = "F";
            arrCabecera1[3, 3] = "dd/MM/yyyy";
            arrCabecera1[3, 4] = "d_orifchdoc";

            arrCabecera1[4, 0] = "Nº Reg. Doc.";
            arrCabecera1[4, 1] = "60";
            arrCabecera1[4, 2] = "C";
            arrCabecera1[4, 3] = "";
            arrCabecera1[4, 4] = "c_numasi";

            arrCabecera1[5, 0] = "T.D.";
            arrCabecera1[5, 1] = "40";
            arrCabecera1[5, 2] = "C";
            arrCabecera1[5, 3] = "";
            arrCabecera1[5, 4] = "c_docabredoc";

            arrCabecera1[6, 0] = "Fch. Doc.";
            arrCabecera1[6, 1] = "70";
            arrCabecera1[6, 2] = "F";
            arrCabecera1[6, 3] = "dd/MM/yyyy";
            arrCabecera1[6, 4] = "d_orifchdoc";

            arrCabecera1[7, 0] = "M.";
            arrCabecera1[7, 1] = "40";
            arrCabecera1[7, 2] = "C";
            arrCabecera1[7, 3] = "";
            arrCabecera1[7, 4] = "c_docmon";

            arrCabecera1[8, 0] = "Nº Documento";
            arrCabecera1[8, 1] = "110";
            arrCabecera1[8, 2] = "C";
            arrCabecera1[8, 3] = "";
            arrCabecera1[8, 4] = "c_docnumdoc";

            arrCabecera1[9, 0] = "Glosa";
            arrCabecera1[9, 1] = "200";
            arrCabecera1[9, 2] = "C";
            arrCabecera1[9, 3] = "";
            arrCabecera1[9, 4] = "c_origlo";

            arrCabecera1[10, 0] = "Nº R.U.C. / D.N.I.";
            arrCabecera1[10, 1] = "90";
            arrCabecera1[10, 2] = "C";
            arrCabecera1[10, 3] = "";
            arrCabecera1[10, 4] = "c_docruc";

            arrCabecera1[11, 0] = "Proveedor / Cliente";
            arrCabecera1[11, 1] = "200";
            arrCabecera1[11, 2] = "C";
            arrCabecera1[11, 3] = "";
            arrCabecera1[11, 4] = "c_doccli";

            arrCabecera1[12, 0] = "Nº Cuenta";
            arrCabecera1[12, 1] = "60";
            arrCabecera1[12, 2] = "C";
            arrCabecera1[12, 3] = "";
            arrCabecera1[12, 4] = "c_datctacon";

            arrCabecera1[13, 0] = "Descripcion";
            arrCabecera1[13, 1] = "300";
            arrCabecera1[13, 2] = "C";
            arrCabecera1[13, 3] = "";
            arrCabecera1[13, 4] = "c_datdescon";
            
            arrCabecera1[14, 0] = "T.C.";
            arrCabecera1[14, 1] = "50";
            arrCabecera1[14, 2] = "D";
            arrCabecera1[14, 3] = "0.000";
            arrCabecera1[14, 4] = "n_dattc";

            arrCabecera1[15, 0] = "Debe";
            arrCabecera1[15, 1] = "80";
            arrCabecera1[15, 2] = "D";
            arrCabecera1[15, 3] = "0.00";
            arrCabecera1[15, 4] = "n_datimpdeb";

            arrCabecera1[16, 0] = "Haber";
            arrCabecera1[16, 1] = "80";
            arrCabecera1[16, 2] = "D";
            arrCabecera1[16, 3] = "0.00";
            arrCabecera1[16, 4] = "n_datimphab";

            arrCabecera1[17, 0] = "Saldo";
            arrCabecera1[17, 1] = "80";
            arrCabecera1[17, 2] = "D";
            arrCabecera1[17, 3] = "0.00";
            arrCabecera1[17, 4] = "n_saldo";




            //arrCabecera1[0, 0] = "Nº Reg. Doc.";
            //arrCabecera1[0, 1] = "80";
            //arrCabecera1[0, 2] = "C";
            //arrCabecera1[0, 3] = "";
            //arrCabecera1[0, 4] = "c_numasi2";

            //arrCabecera1[1, 0] = "T.D.";
            //arrCabecera1[1, 1] = "30";
            //arrCabecera1[1, 2] = "C";
            //arrCabecera1[1, 3] = "";
            //arrCabecera1[1, 4] = "c_abr";

            //arrCabecera1[2, 0] = "Fch. Doc.";
            //arrCabecera1[2, 1] = "70";
            //arrCabecera1[2, 2] = "F";
            //arrCabecera1[2, 3] = "";
            //arrCabecera1[2, 4] = "d_orifchdoc";

            //arrCabecera1[3, 0] = "Nº Documento";
            //arrCabecera1[3, 1] = "110";
            //arrCabecera1[3, 2] = "C";
            //arrCabecera1[3, 3] = "";
            //arrCabecera1[3, 4] = "c_orinumdoc";

            //arrCabecera1[4, 0] = "Glosa";
            //arrCabecera1[4, 1] = "100";
            //arrCabecera1[4, 2] = "C";
            //arrCabecera1[4, 3] = "";
            //arrCabecera1[4, 4] = "c_origlo";

            //arrCabecera1[5, 0] = "Nº RUC / DNI";
            //arrCabecera1[5, 1] = "80";
            //arrCabecera1[5, 2] = "C";
            //arrCabecera1[5, 3] = "";
            //arrCabecera1[5, 4] = "c_orinumruc";

            //arrCabecera1[6, 0] = "Proveedor / Cliente / Otros";
            //arrCabecera1[6, 1] = "150";
            //arrCabecera1[6, 2] = "C";
            //arrCabecera1[6, 3] = "";
            //arrCabecera1[6, 4] = "c_orinomcli";

            //arrCabecera1[7, 0] = "Nº Cuenta";
            //arrCabecera1[7, 1] = "80";
            //arrCabecera1[7, 2] = "C";
            //arrCabecera1[7, 3] = "";
            //arrCabecera1[7, 4] = "c_cuecon";

            //arrCabecera1[8, 0] = "Nombre de la Cuenta";
            //arrCabecera1[8, 1] = "200";
            //arrCabecera1[8, 2] = "C";
            //arrCabecera1[8, 3] = "";
            //arrCabecera1[8, 4] = "c_des";

            //arrCabecera1[9, 0] = "M.";
            //arrCabecera1[9, 1] = "30";
            //arrCabecera1[9, 2] = "C";
            //arrCabecera1[9, 3] = "";
            //arrCabecera1[9, 4] = "c_simbolo";

            //arrCabecera1[10, 0] = "T.C.";
            //arrCabecera1[10, 1] = "40";
            //arrCabecera1[10, 2] = "D";
            //arrCabecera1[10, 3] = "0.000";
            //arrCabecera1[10, 4] = "n_tc";

            //arrCabecera1[11, 0] = "Debe";
            //arrCabecera1[11, 1] = "80";
            //arrCabecera1[11, 2] = "D";
            //arrCabecera1[11, 3] = "0.00";
            //arrCabecera1[11, 4] = "n_impdebsol";

            //arrCabecera1[12, 0] = "Haber";
            //arrCabecera1[12, 1] = "80";
            //arrCabecera1[12, 2] = "D";
            //arrCabecera1[12, 3] = "0.00";
            //arrCabecera1[12, 4] = "n_imphabsol";

            //arrCabecera1[13, 0] = "n_idlib";
            //arrCabecera1[13, 1] = "0";
            //arrCabecera1[13, 2] = "N";
            //arrCabecera1[13, 3] = "";
            //arrCabecera1[13, 4] = "n_lin";

            //arrCabecera1[14, 0] = "n_orden";
            //arrCabecera1[14, 1] = "0";
            //arrCabecera1[14, 2] = "N";
            //arrCabecera1[14, 3] = "";
            //arrCabecera1[14, 4] = "n_orden";




            arrCabResumen1[0, 0] = "Nº Cuenta";
            arrCabResumen1[0, 1] = "80";
            arrCabResumen1[0, 2] = "C";
            arrCabResumen1[0, 3] = "";
            arrCabResumen1[0, 4] = "c_cuecon";

            arrCabResumen1[1, 0] = "Nombre Cuenta Contable";
            arrCabResumen1[1, 1] = "300";
            arrCabResumen1[1, 2] = "C";
            arrCabResumen1[1, 3] = "";
            arrCabResumen1[1, 4] = "c_des";

            arrCabResumen1[2, 0] = "Deb";
            arrCabResumen1[2, 1] = "80";
            arrCabResumen1[2, 2] = "D";
            arrCabResumen1[2, 3] = "";
            arrCabResumen1[2, 4] = "n_impdebsol";

            arrCabResumen1[3, 0] = "Haber";
            arrCabResumen1[3, 1] = "80";
            arrCabResumen1[3, 2] = "D";
            arrCabResumen1[3, 3] = "";
            arrCabResumen1[3, 4] = "n_imphabsol";



            arrCabeceraFlex1[0, 0] = "Nº Cuenta";
            arrCabeceraFlex1[0, 1] = "80";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_cuecon";

            arrCabeceraFlex1[1, 0] = "Nombre Cuenta Contable";
            arrCabeceraFlex1[1, 1] = "300";
            arrCabeceraFlex1[1, 2] = "C";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_des";

            arrCabeceraFlex1[2, 0] = "n_id";
            arrCabeceraFlex1[2, 1] = "0";
            arrCabeceraFlex1[2, 2] = "N";
            arrCabeceraFlex1[2, 3] = "";
            arrCabeceraFlex1[2, 4] = "n_id";
            
        }

        private void FrmLibroMayor_Resize(object sender, EventArgs e)
        {
            Sz1.Left = 0;
            Sz1.Top = 41;
            Sz1.Height = this.Height - 83;
            Sz1.Width = this.Width - 18;
        }

        private void ToolSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ToolBuscar_Click(object sender, EventArgs e)
        {
            EjecutarConsulta();
        }
        void EjecutarConsulta()
        {
            if (Convert.ToInt32(CboPerIni.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el periodo de inicio a consultar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboPerIni.Focus();
                return;
            }
            if (Convert.ToInt32(CboPerFin.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el periodo final a consultar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboPerFin.Focus();
                return;
            }
            if (Convert.ToInt32(CboPerIni.SelectedValue) > Convert.ToInt32(CboPerFin.SelectedValue))
            {
                MessageBox.Show("¡ El periodo de inicio no puede ser mayor al periodo final !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboPerFin.Focus();
                return;
            }
            string c_CadINCli = funFlex.Flex_CadenaIN(FgCuenta, 1, 1);

            if (c_CadINCli == "") 
            {
                MessageBox.Show("¡ No ha especificado las cuentas que desea mayorizar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                FgCuenta.Focus();
                return;
            }

            Tab02.SelectedIndex = 0;
            CN_con_diario funCom = new CN_con_diario();
            funCom.mysConec = mysConec;
            funCom.STU_SISTEMA = STU_SISTEMA;

            MostrarDetalle(c_CadINCli);
            //bool b_Result = false;
            //b_Result = funCom.ConsultaDiario(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt32(CboLibro.SelectedValue), Convert.ToInt32(CboPerIni.SelectedValue), Convert.ToInt32(CboPerFin.SelectedValue));

            //if (b_Result == true)
            //{
            //    dtLista = funCom.dtLista;
            //    dtResumen = funCom.dtResumen;
            //    if (dtLista.Rows.Count != 0)
            //    {
            //        funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 3, true);
            //        funFlex.FlexMostrarDatos(FgRes, arrCabResumen1, dtResumen, 3, true);
            //        HalarTotalresumen();
            //    }
            //    else
            //    {
            //        MessageBox.Show("¡ No hay registros en el periodo indicado !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //    }
            //}
        }
        void MostrarDetalle(string c_CadenaIN)
        {
            DataTable dtResult = new DataTable();
            int n_row = 0;
            double n_salini = 0;
            double n_imphab = 0;
            double n_impdeb = 0;

            o_Diario.mysConec = mysConec;
            o_Diario.ConsultaMayor(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt32(CboPerIni.SelectedValue), Convert.ToInt32(CboPerFin.SelectedValue), c_CadenaIN);

            if (o_Diario.b_OcurrioError == true)
            {
                return;
            }
            dtResult = o_Diario.dtLista;

            for (n_row = 0; n_row <= dtResult.Rows.Count - 1; n_row++)
            {
                if (n_row == 0)
                {
                    if (Convert.ToInt32(dtResult.Rows[n_row]["n_orden"]) == 1)
                    {
                        n_salini = Convert.ToDouble(dtResult.Rows[n_row]["n_saldo"]);
                    }
                    //if (Convert.ToDouble(dtResult.Rows[n_row]["n_datimpdeb"]) != 0) { n_valor = Convert.ToDouble(dtResult.Rows[n_row]["n_datimpdeb"]); }
                    //if (Convert.ToDouble(dtResult.Rows[n_row]["n_datimphab"]) != 0) { n_valor = Convert.ToDouble(dtResult.Rows[n_row]["n_datimphab"]); }
                    //dtResult.Rows[n_row]["n_saldo"] = n_valor;
                }
                else
                {
                    if (Convert.ToDouble(dtResult.Rows[n_row]["n_orden"]) == 1)
                    {
                        n_salini = Convert.ToDouble(dtResult.Rows[n_row]["n_saldo"]);
                    }

                    if (Convert.ToDouble(dtResult.Rows[n_row]["n_orden"]) == 2)
                    { 
                        n_impdeb = Convert.ToDouble(dtResult.Rows[n_row]["n_datimpdeb"]);
                        n_imphab = Convert.ToDouble(dtResult.Rows[n_row]["n_datimphab"]);
                        n_salini = ((n_salini + n_impdeb) - n_imphab);
                        dtResult.Rows[n_row]["n_saldo"] = n_salini.ToString("0.00");
                    }

                    //if (Convert.ToDouble(dtResult.Rows[n_row]["n_datimpdeb"]) != 0)
                    //{
                    //    //n_valor = Convert.ToDouble(dtResult.Rows[n_row]["datimpdeb"]) ;
                    //    dtResult.Rows[n_row]["n_saldo"] = (n_valor + Convert.ToDouble(dtResult.Rows[n_row]["n_datimpdeb"]));
                    //}
                    //if (Convert.ToDouble(dtResult.Rows[n_row]["n_datimphab"]) != 0)
                    //{
                    //    //n_valor = Convert.ToDouble(dtResult.Rows[n_row]["datimphab"]) ;
                    //    dtResult.Rows[n_row]["n_saldo"] = (n_valor - Convert.ToDouble(dtResult.Rows[n_row]["n_datimphab"]));
                    //}
                    //n_valor = Convert.ToDouble(dtResult.Rows[n_row]["n_saldo"]);
                }
            }

            funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtResult, 3, true);
            ConfigurarCabecera1();
        }
        void HalarTotalresumen()
        {
            double n_val1 = funFlex.FlexSumarCol(FgRes, 3, 3, FgRes.Rows.Count - 1);
            double n_val2 = funFlex.FlexSumarCol(FgRes, 3, 4, FgRes.Rows.Count - 1);

            FgRes.Rows.Count = FgRes.Rows.Count + 1;
            FgRes.SetData(FgRes.Rows.Count - 1, 2, "TOTAL ==>");
            FgRes.SetData(FgRes.Rows.Count - 1, 3, n_val1.ToString("0.00"));
            FgRes.SetData(FgRes.Rows.Count - 1, 4, n_val2.ToString("0.00"));
        }
        void ConfigurarCabecera1()
        {
            // CABECERA DETALLE
            funFlex.Flex_FixUniColumnas(FgDatos, 0, 1, 4, "CDATOS DE LA OPERACION", 1);
            funFlex.Flex_FixUniColumnas(FgDatos, 0, 5, 12, "DATOS DEL DOCUMENTO", 1);
            funFlex.Flex_FixUniColumnas(FgDatos, 0, 15, 18, "DATOS DE LA OPERACION", 1);
        }
        void Configurarcabecera2()
        {
        }
        private void ToolExportar_Click(object sender, EventArgs e)
        {
            funFlex.ExportToExcel_NumFilaCabecera = 3;
            string c_nomarch = STU_SISTEMA.EMPRESARUC + "-" + STU_SISTEMA.ANOTRABAJO.ToString() + "-LIBRO_MAYOR-" + Convert.ToInt32(CboPerIni.SelectedValue).ToString("00") + "-AL-" + Convert.ToInt32(CboPerIni.SelectedValue).ToString("00") + ".xls";
            funFlex.ExportToExcel(FgDatos, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "LIBRO MAYOR", "DE " + CboPerIni.Text.ToUpper() + " A " + CboPerFin.Text.ToUpper(), c_nomarch);
        }

        private void TooGenLE_Click(object sender, EventArgs e)
        {
            bool b_result = false;
            CN_con_le objLE = new CN_con_le();
            objLE.mysConec = mysConec;
            objLE.STU_SISTEMA = STU_SISTEMA;
            if (n_Libro == 8)
            {
                b_result = objLE.LE_81(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt32(CboPerIni.SelectedValue));
            }
            if (n_Libro == 14)
            {
                b_result = objLE.LE_141(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt32(CboPerIni.SelectedValue));
            }

            if (b_result == true)
            {
                MessageBox.Show("¡ El libro electronico se genero con exito en la siguiente ruta :" + objLE.c_RutaSalida + "!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void CmdAddPro_Click(object sender, EventArgs e)
        {
            if (funFunciones.NulosC(FgCuenta.GetData(FgCuenta.Rows.Count - 1, 1)) == "")
            {
                MessageBox.Show("¡ No ha especificado un proveedor en la ultima fila !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                FgCuenta.Focus();
                return;
            }
            FgCuenta.Rows.Count = FgCuenta.Rows.Count + 1;
        }

        private void CmdDelPro_Click(object sender, EventArgs e)
        {
            if (FgCuenta.Rows.Count == 1) { return; }
            FgCuenta.RemoveItem(FgCuenta.Row);
        }

        private void FgCuenta_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (FgCuenta.Col == 1)
            {
                DataTable dtResult = new DataTable();
                string c_dato = "";
                o_Cuenta.mysConec = mysConec;
                dtResult = o_Cuenta.BuscarCuenta(dtCuenta);
                if (dtResult != null)
                {
                    if (dtResult.Rows.Count != 0)
                    {
                        c_dato = dtResult.Rows[0]["c_cuecon"].ToString();
                        FgCuenta.SetData(FgCuenta.Row, 1, c_dato);

                        c_dato = dtResult.Rows[0]["c_des"].ToString();
                        FgCuenta.SetData(FgCuenta.Row, 2, c_dato);

                        c_dato = dtResult.Rows[0]["n_id"].ToString();
                        FgCuenta.SetData(FgCuenta.Row, 3, c_dato);

                        FgCuenta.Rows.Count = FgCuenta.Rows.Count + 1;
                    }
                }
            }
        }
    }
}
