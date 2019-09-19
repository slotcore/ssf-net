using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Negocio.Ventas;
using SIAC_Negocio.Almacen;
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
using SIAC_Objetos.Sistema;

namespace SSF_NET_Ventas.Formularios
{
    public partial class FrmVistaGuias : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public int n_Libro;

        DataTable dtCli = new DataTable();
        DataTable dtItem = new DataTable();

        DataTable dtLista = new DataTable();
        DataTable dtResumen = new DataTable();

        CN_mae_clipro objCli = new CN_mae_clipro();
        CN_alm_inventario objIte = new CN_alm_inventario();


        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        Cls_Controles funControl = new Cls_Controles();

        string[,] arrCabeceraFlex1 = new string[2, 5];
        string[,] arrCabeceraFlex2 = new string[2, 5];
        string[,] arrCabecera1 = new string[36, 5];
        string[,] arrCabecera2 = new string[12, 5];
        string[,] arrCabecera3 = new string[24, 5];

        string[,] arrFleIte = new string[2, 5];
        string[,] arrFleCli = new string[2, 5];

        public FrmVistaGuias()
        {
            InitializeComponent();
        }

        private void FrmVistaGuias_Load(object sender, EventArgs e)
        {
            ConfigurarFormulario();
            CargarDT();
        }
        void CargarDT()
        {
            objIte.mysConec = mysConec;
            dtItem = objIte.Listar(STU_SISTEMA.EMPRESAID, 1, 1);

            objCli.mysConec = mysConec;
            dtCli = objCli.ListarCliente(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);
        }
        void ConfigurarFormulario()
        {
            this.Height = 600;
            this.Width = 1024;
            
            Sz1.Left = 0;
            Sz1.Top = 41;
            Sz1.Height = this.Height - 83;
            Sz1.Width = this.Width - 18;
            ConfigurarFlex();
            LblNumReg.Text = "";

            OptOpcion2.Checked = true;
            OptIte1.Checked = true;

            FgCliente.Rows.Count = 2;
            FgItems.Rows.Count = 2;

            FgCliente.Cols[1].ComboList = "...";
            FgItems.Cols[1].ComboList = "...";

            this.Text = "VENTAS - CONSULTA DESPACHOS POR GUIAS";

            if (OptOpcion1.Checked == true)
            {
                Cabecera1();
                funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 3, false);
                Configurarcabecera1();
            }
            if (OptOpcion2.Checked == true)
            {
                Cabecera2();
                funFlex.FlexMostrarDatos(FgDatos, arrCabecera2, dtLista, 3, false);
                Configurarcabecera2();
            }
            if (OptOpcion3.Checked == true)
            {
                Cabecera3();
                funFlex.FlexMostrarDatos(FgDatos, arrCabecera3, dtLista, 3, false);
                Configurarcabecera3();
            }
        }

        void ConfigurarFlex()
        { 
            arrFleIte[0, 0] = "Producto / Mercaderia";
            arrFleIte[0, 1] = "300";
            arrFleIte[0, 2] = "C";
            arrFleIte[0, 3] = "";
            arrFleIte[0, 4] = "c_depro";

            arrFleIte[1, 0] = "Id";
            arrFleIte[1, 1] = "0";
            arrFleIte[1, 2] = "N";
            arrFleIte[1, 3] = "";
            arrFleIte[1, 4] = "n_id";

            funFlex.FlexMostrarDatos(FgItems, arrFleIte, dtLista, 1, false);


            arrFleCli[0, 0] = "Cliente";
            arrFleCli[0, 1] = "300";
            arrFleCli[0, 2] = "C";
            arrFleCli[0, 3] = "";
            arrFleCli[0, 4] = "c_nombre";

            arrFleCli[1, 0] = "Id";
            arrFleCli[1, 1] = "0";
            arrFleCli[1, 2] = "N";
            arrFleCli[1, 3] = "";
            arrFleCli[1, 4] = "n_id";

            funFlex.FlexMostrarDatos(FgCliente, arrFleCli, dtLista, 1, false);
        }
        void Cabecera2()
        {
            arrCabecera2[0, 0] = "Fch. Documento";
            arrCabecera2[0, 1] = "70";
            arrCabecera2[0, 2] = "F";
            arrCabecera2[0, 3] = "";
            arrCabecera2[0, 4] = "d_fchdoc";

            arrCabecera2[1, 0] = "Nº Documento";
            arrCabecera2[1, 1] = "110";
            arrCabecera2[1, 2] = "C";
            arrCabecera2[1, 3] = "";
            arrCabecera2[1, 4] = "c_numdoc";

            arrCabecera2[2, 0] = "Nº R.U.C.";
            arrCabecera2[2, 1] = "80";
            arrCabecera2[2, 2] = "C";
            arrCabecera2[2, 3] = "";
            arrCabecera2[2, 4] = "c_clinumdoc";

            arrCabecera2[3, 0] = "Cliente";
            arrCabecera2[3, 1] = "300";
            arrCabecera2[3, 2] = "C";
            arrCabecera2[3, 3] = "";
            arrCabecera2[3, 4] = "c_nombre";

            arrCabecera2[4, 0] = "Cod CEN. Punto Venta";
            arrCabecera2[4, 1] = "80";
            arrCabecera2[4, 2] = "C";
            arrCabecera2[4, 3] = "";
            arrCabecera2[4, 4] = "c_codcen";

            arrCabecera2[5, 0] = "Punto Venta";
            arrCabecera2[5, 1] = "250";
            arrCabecera2[5, 2] = "C";
            arrCabecera2[5, 3] = "";
            arrCabecera2[5, 4] = "c_clidespunven";

            arrCabecera2[6, 0] = "Cod. Producto";
            arrCabecera2[6, 1] = "100";
            arrCabecera2[6, 2] = "C";
            arrCabecera2[6, 3] = "";
            arrCabecera2[6, 4] = "c_codpro";

            arrCabecera2[7, 0] = "Producto / Mercaderia";
            arrCabecera2[7, 1] = "300";
            arrCabecera2[7, 2] = "C";
            arrCabecera2[7, 3] = "";
            arrCabecera2[7, 4] = "c_despro";

            arrCabecera2[8, 0] = "Uni. Medida";
            arrCabecera2[8, 1] = "40";
            arrCabecera2[8, 2] = "D";
            arrCabecera2[8, 3] = "0.000";
            arrCabecera2[8, 4] = "c_iteunimed";

            arrCabecera2[9, 0] = "Cantidad Entregada";
            arrCabecera2[9, 1] = "80";
            arrCabecera2[9, 2] = "D";
            arrCabecera2[9, 3] = "";
            arrCabecera2[9, 4] = "n_canpro";

            arrCabecera2[10, 0] = "Nº Lote";
            arrCabecera2[10, 1] = "80";
            arrCabecera2[10, 2] = "C";
            arrCabecera2[10, 3] = "";
            arrCabecera2[10, 4] = "c_numlot"; 

            arrCabecera2[11, 0] = "Nº Orden Compra";
            arrCabecera2[11, 1] = "100";
            arrCabecera2[11, 2] = "C";
            arrCabecera2[11, 3] = "";
            arrCabecera2[11, 4] = "c_numordcom"; 

            
        }
        void Cabecera1()
        {
            arrCabecera1[0, 0] = "Nº Registro";
            arrCabecera1[0, 1] = "60";
            arrCabecera1[0, 2] = "C";
            arrCabecera1[0, 3] = "";
            arrCabecera1[0, 4] = "c_numreg";

            arrCabecera1[1, 0] = "Fch. Documento";
            arrCabecera1[1, 1] = "70";
            arrCabecera1[1, 2] = "F";
            arrCabecera1[1, 3] = "";
            arrCabecera1[1, 4] = "d_fchdoc";

            arrCabecera1[2, 0] = "Fch. Vencimiento";
            arrCabecera1[2, 1] = "70";
            arrCabecera1[2, 2] = "F";
            arrCabecera1[2, 3] = "";
            arrCabecera1[2, 4] = "d_fchven";

            arrCabecera1[3, 0] = "Nº Serie";
            arrCabecera1[3, 1] = "50";
            arrCabecera1[3, 2] = "C";
            arrCabecera1[3, 3] = "";
            arrCabecera1[3, 4] = "c_numser";

            arrCabecera1[4, 0] = "Nº Documento";
            arrCabecera1[4, 1] = "80";
            arrCabecera1[4, 2] = "C";
            arrCabecera1[4, 3] = "";
            arrCabecera1[4, 4] = "c_numdoc";

            arrCabecera1[5, 0] = "Tip. Doc. Idem.";
            arrCabecera1[5, 1] = "50";
            arrCabecera1[5, 2] = "C";
            arrCabecera1[5, 3] = "";
            arrCabecera1[5, 4] = "c_tipdoc";

            arrCabecera1[6, 0] = "Nº Doc. Ident.";
            arrCabecera1[6, 1] = "90";
            arrCabecera1[6, 2] = "C";
            arrCabecera1[6, 3] = "";
            arrCabecera1[6, 4] = "c_numdoc1";

            arrCabecera1[7, 0] = "Proveedor";
            arrCabecera1[7, 1] = "200";
            arrCabecera1[7, 2] = "C";
            arrCabecera1[7, 3] = "";
            arrCabecera1[7, 4] = "c_nombre";

            arrCabecera1[8, 0] = "Glosa";
            arrCabecera1[8, 1] = "200";
            arrCabecera1[8, 2] = "C";
            arrCabecera1[8, 3] = "";
            arrCabecera1[8, 4] = "c_glosa";
        }
        void Cabecera3()
        {
            arrCabecera3[0, 0] = "Nº Registro";
            arrCabecera3[0, 1] = "60";
            arrCabecera3[0, 2] = "C";
            arrCabecera3[0, 3] = "";
            arrCabecera3[0, 4] = "c_numreg";

            arrCabecera3[1, 0] = "Fch. Documento";
            arrCabecera3[1, 1] = "70";
            arrCabecera3[1, 2] = "F";
            arrCabecera3[1, 3] = "";
            arrCabecera3[1, 4] = "d_fchdoc";

            arrCabecera3[2, 0] = "Fch. Vencimiento";
            arrCabecera3[2, 1] = "70";
            arrCabecera3[2, 2] = "F";
            arrCabecera3[2, 3] = "";
            arrCabecera3[2, 4] = "d_fchven";

            arrCabecera3[3, 0] = "TCP";
            arrCabecera3[3, 1] = "40";
            arrCabecera3[3, 2] = "C";
            arrCabecera3[3, 3] = "";
            arrCabecera3[3, 4] = "c_codsun";

            arrCabecera3[4, 0] = "Nº Serie";
            arrCabecera3[4, 1] = "50";
            arrCabecera3[4, 2] = "C";
            arrCabecera3[4, 3] = "";
            arrCabecera3[4, 4] = "c_numser";

            arrCabecera3[5, 0] = "Nº CP";
            arrCabecera3[5, 1] = "70";
            arrCabecera3[5, 2] = "C";
            arrCabecera3[5, 3] = "";
            arrCabecera3[5, 4] = "c_numdoc";

            arrCabecera3[6, 0] = "Tipo Doc. Idem";
            arrCabecera3[6, 1] = "50";
            arrCabecera3[6, 2] = "C";
            arrCabecera3[6, 3] = "";
            arrCabecera3[6, 4] = "c_codsun1";

            arrCabecera3[7, 0] = "Nº de Doc. Iden.";
            arrCabecera3[7, 1] = "90";
            arrCabecera3[7, 2] = "C";
            arrCabecera3[7, 3] = "";
            arrCabecera3[7, 4] = "c_numdoc1";

            arrCabecera3[8, 0] = "Apellidos y Nombres / Razon Social";
            arrCabecera3[8, 1] = "200";
            arrCabecera3[8, 2] = "C";
            arrCabecera3[8, 3] = "";
            arrCabecera3[8, 4] = "c_nombre";

            arrCabecera3[9, 0] = "T.C.";
            arrCabecera3[9, 1] = "40";
            arrCabecera3[9, 2] = "D";
            arrCabecera3[9, 3] = "0.000";
            arrCabecera3[9, 4] = "n_tc";

            arrCabecera3[10, 0] = "M";
            arrCabecera3[10, 1] = "40";
            arrCabecera3[10, 2] = "C";
            arrCabecera3[10, 3] = "";
            arrCabecera3[10, 4] = "c_codsun2";

            arrCabecera3[11, 0] = "Valor de la Export.";
            arrCabecera3[11, 1] = "80";
            arrCabecera3[11, 2] = "D";
            arrCabecera3[11, 3] = "";
            arrCabecera3[11, 4] = "n_impbru";

            arrCabecera3[12, 0] = "Base Imp. Oper. Grav";
            arrCabecera3[12, 1] = "80";
            arrCabecera3[12, 2] = "D";
            arrCabecera3[12, 3] = "";
            arrCabecera3[12, 4] = "n_impbru2";

            arrCabecera3[13, 0] = "Operaciones Exoneradas";
            arrCabecera3[13, 1] = "70";
            arrCabecera3[13, 2] = "D";
            arrCabecera3[13, 3] = "";
            arrCabecera3[13, 4] = "n_impbru3";

            arrCabecera3[14, 0] = "Operaciones Inafectas";
            arrCabecera3[14, 1] = "80";
            arrCabecera3[14, 2] = "D";
            arrCabecera3[14, 3] = "";
            arrCabecera3[14, 4] = "n_impinaf";

            arrCabecera3[15, 0] = "I.S.C.";
            arrCabecera3[15, 1] = "80";
            arrCabecera3[15, 2] = "D";
            arrCabecera3[15, 3] = "";
            arrCabecera3[15, 4] = "n_impisc";

            arrCabecera3[16, 0] = "Tasa IGV(%)";
            arrCabecera3[16, 1] = "80";
            arrCabecera3[16, 2] = "D";
            arrCabecera3[16, 3] = "";
            arrCabecera3[16, 4] = "n_tasaigv";

            arrCabecera3[17, 0] = "I.G.V. e I.M.P.";
            arrCabecera3[17, 1] = "80";
            arrCabecera3[17, 2] = "D";
            arrCabecera3[17, 3] = "";
            arrCabecera3[17, 4] = "n_impigv";

            arrCabecera3[18, 0] = "Otros Trib. o Cargos";
            arrCabecera3[18, 1] = "80";
            arrCabecera3[18, 2] = "D";
            arrCabecera3[18, 3] = "";
            arrCabecera3[18, 4] = "n_impotr";

            arrCabecera3[19, 0] = "Importe Total";
            arrCabecera3[19, 1] = "80";
            arrCabecera3[19, 2] = "D";
            arrCabecera3[19, 3] = "";
            arrCabecera3[19, 4] = "n_imptotven";

            arrCabecera3[20, 0] = "Fch. Doc.";
            arrCabecera3[20, 1] = "80";
            arrCabecera3[20, 2] = "F";
            arrCabecera3[20, 3] = "";
            arrCabecera3[20, 4] = "d_fchdoc1";

            arrCabecera3[21, 0] = "TCP";
            arrCabecera3[21, 1] = "50";
            arrCabecera3[21, 2] = "C";
            arrCabecera3[21, 3] = "";
            arrCabecera3[21, 4] = "c_codsun3";

            arrCabecera3[22, 0] = "Nº Serie";
            arrCabecera3[22, 1] = "70";
            arrCabecera3[22, 2] = "C";
            arrCabecera3[22, 3] = "";
            arrCabecera3[22, 4] = "c_numser1";

            arrCabecera3[23, 0] = "Nº CP";
            arrCabecera3[23, 1] = "80";
            arrCabecera3[23, 2] = "C";
            arrCabecera3[23, 3] = "";
            arrCabecera3[23, 4] = "c_numdoc2";
        }

        private void FrmVistaGuias_Resize(object sender, EventArgs e)
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
            if (OptOpcion3.Checked != true)
            { 
                if (TxtFchIni.Text == "")
                {
                    MessageBox.Show("¡ Debe de indicar la fecha de inicio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    TxtFchIni.Focus();
                    return;
                }

                if (TxtFchFin.Text == "")
                {
                    MessageBox.Show("¡ Debe de indicar la fecha final !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    TxtFchFin.Focus();
                    return;
                }

                DateTime d_fchini = Convert.ToDateTime(TxtFchIni.Text);
                DateTime d_fchfin = Convert.ToDateTime(TxtFchFin.Text);

                if (d_fchini > d_fchfin)
                {
                    MessageBox.Show("¡ La fecha de inicio no puede ser mayor a la fecha de fin !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    TxtFchIni.Focus();
                    return;
                }
            }

            string c_cadin = "";
            int n_row = 1;
            string c_cad = "";

            for (n_row = 1; n_row <= FgItems.Rows.Count - 1; n_row++)
            { 
                c_cad = funFunciones.NulosC( FgItems.GetData(n_row,2));

                if (c_cad != "")
                { 
                    if (n_row == 1)
                    {
                        if (c_cad != "")
                        {
                            c_cadin = c_cadin + c_cad;
                        }
                    }
                    else
                    {
                        if (c_cad != "")
                        { 
                            c_cadin = c_cadin + ", " + c_cad;
                        }
                    }
                }
            }


            CN_vta_guias objVen = new CN_vta_guias();
            objVen.mysConec = mysConec;
            bool b_Result = false;

            b_Result = objVen.Consulta10(STU_SISTEMA.EMPRESAID, 2, TxtFchIni.Text, TxtFchFin.Text, c_cadin);

            if (b_Result == true)
            {
                dtLista = objVen.dtLista;
                if (dtLista.Rows.Count != 0)
                {
                    Ordenar();
                }
                else
                {
                    MessageBox.Show("¡ No hay registros en el periodo indicado !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
        }
        void Ordenar()
        {
            //string c_orden = "";
            if (dtLista == null) { return; }
            if (dtLista.Rows.Count == 0) { return; }

            //if (OptSor1.Checked == true) { dtLista = funDatos.DataTableOrdenar(dtLista, "d_fchdoc"); c_orden = "fecha de documento"; }
            //if (OptSor2.Checked == true) { dtLista = funDatos.DataTableOrdenar(dtLista, "c_numdocord"); c_orden = "numero de documento"; }
            //if (OptSor3.Checked == true) { dtLista = funDatos.DataTableOrdenar(dtLista, "c_numreg"); c_orden = "numero de registro"; }
            //if (OptSor4.Checked == true) { dtLista = funDatos.DataTableOrdenar(dtLista, "c_fchdocnumdocord"); c_orden = "fecha de documento y numero de registro"; }

            if (OptOpcion1.Checked == true)
            {
                funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 3, true);
                Configurarcabecera1();
            }
            if (OptOpcion2.Checked == true) 
            { 
                funFlex.FlexMostrarDatos(FgDatos, arrCabecera2, dtLista, 3, true); 
            }
            if (OptOpcion3.Checked == true)
            {
                funFlex.FlexMostrarDatos(FgDatos, arrCabecera3, dtLista, 3, true);
                Configurarcabecera3();
            }
            LblNumReg.Text = dtLista.Rows.Count.ToString();
            //SumarColumnas();
            //MessageBox.Show("¡ Se ordeno por " + c_orden + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }
        void Configurarcabecera1()
        {
            // CABECERA DETALLE
            funFlex.Flex_UniColumnas(FgDatos, 0, 2, 5, "COMPROBANTE DE PAGO", 1);
            funFlex.Flex_UniColumnas(FgDatos, 0, 6, 8, "INFORMACION DEL PROVEEDOR", 1);
            funFlex.Flex_UniColumnas(FgDatos, 0, 9, 11, "COMPROBANTE DE PAGO", 1);
            funFlex.Flex_UniColumnas(FgDatos, 0, 12, 14, "BASE IMPONIBLE", 1);

            funFlex.Flex_UniColumnas(FgDatos, 0, 18, 21, "IMPUESTO GENERAL A LA VENTAS", 1);
            funFlex.Flex_UniColumnas(FgDatos, 0, 25, 28, "CONSTANCIA DE DEPOSITO SPOT", 1);
            funFlex.Flex_UniColumnas(FgDatos, 0, 29, 36, "REFERENCIA DEL COMPROBANTE DE PAGO O DOCUMENTO ORIGINAL", 1);
        }
        void Configurarcabecera2()
        {
        }
        void Configurarcabecera3()
        {
            funFlex.Flex_UniColumnas(FgDatos, 0, 2, 6, "COMPROBANTE DE PAGO", 1);
            funFlex.Flex_UniColumnas(FgDatos, 0, 7, 9, "INFORMACION DEL CLIENTE", 1);
            funFlex.Flex_UniColumnas(FgDatos, 0, 21, 24, "REFERENCIA DEL COMPROBANTE DE PAGO", 1);
        }

        private void ToolExportar_Click(object sender, EventArgs e)
        {
            funFlex.ExportToExcel_NumFilaCabecera = 3;
            if (OptOpcion1.Checked == true)
            {
                string c_nomarch = STU_SISTEMA.EMPRESARUC + "-" + STU_SISTEMA.ANOTRABAJO.ToString() + "-" + STU_SISTEMA.MESTRABAJO.ToString() + "-DESPACHOS POR GUIA-1" + ".xls";
                funFlex.ExportToExcel(FgDatos, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "RESUMEN DESPACHOS X GUIA", "DEL " + TxtFchIni.Text + " AL "+ TxtFchFin.Text, c_nomarch);
            }
            if (OptOpcion2.Checked == true)
            {
                string c_nomarch = STU_SISTEMA.EMPRESARUC + "-" + STU_SISTEMA.ANOTRABAJO.ToString() + "-" + STU_SISTEMA.MESTRABAJO.ToString() + "-DESPACHOS POR GUIA-2" + ".xls";
                funFlex.ExportToExcel(FgDatos, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "DETALLE DESPACHOS X GUIA", "DEL " + TxtFchIni.Text + " AL " + TxtFchFin.Text, c_nomarch);
            }
            if (OptOpcion3.Checked == true)
            {
                string c_nomarch = STU_SISTEMA.EMPRESARUC + "-" + STU_SISTEMA.ANOTRABAJO.ToString() + "-" + STU_SISTEMA.MESTRABAJO.ToString() + "-DESPACHOS POR GUIA-3" + ".xls";
                funFlex.ExportToExcel(FgDatos, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "COMPARATIVO ACUMULADO POR MESES", "HASTA " +DateTime.Now.ToString("MMMM"), c_nomarch);
            }
        }
        void SumarColumnas()
        {
            double n_tot1 = 0;
            double n_tot2 = 0;
            double n_tot3 = 0;
            double n_tot4 = 0;
            double n_tot5 = 0;
            double n_tot6 = 0;
            double n_tot7 = 0;
            double n_tot8 = 0;
            double n_tot9 = 0;
            double n_tot10 = 0;
            double n_tot11 = 0;
            double n_tot12 = 0;
            int n_row = 0;

            if (n_Libro == 8)
            {
                for (n_row = 3; n_row <= FgDatos.Rows.Count - 1; n_row++)
                {
                    if (FgDatos.GetData(n_row, 12).ToString() == "07")
                    {
                        n_tot1 = n_tot1 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 12)));
                        n_tot2 = n_tot2 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 13)));
                        n_tot3 = n_tot3 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 14)));
                        n_tot4 = n_tot4 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 15)));
                        n_tot5 = n_tot5 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 16)));
                        n_tot6 = n_tot6 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 17)));
                        n_tot7 = n_tot7 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 18)));
                        n_tot8 = n_tot8 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 19)));
                        n_tot9 = n_tot9 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 20)));
                        n_tot10 = n_tot10 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 21)));
                        n_tot11 = n_tot11 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 22)));
                        n_tot12 = n_tot12 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 23)));
                    }
                    else
                    {
                        n_tot1 = n_tot1 + Convert.ToDouble(FgDatos.GetData(n_row, 12));
                        n_tot2 = n_tot2 + Convert.ToDouble(FgDatos.GetData(n_row, 13));
                        n_tot3 = n_tot3 + Convert.ToDouble(FgDatos.GetData(n_row, 14));
                        n_tot4 = n_tot4 + Convert.ToDouble(FgDatos.GetData(n_row, 15));
                        n_tot5 = n_tot5 + Convert.ToDouble(FgDatos.GetData(n_row, 16));
                        n_tot6 = n_tot6 + Convert.ToDouble(FgDatos.GetData(n_row, 17));
                        n_tot7 = n_tot7 + Convert.ToDouble(FgDatos.GetData(n_row, 18));
                        n_tot8 = n_tot8 + Convert.ToDouble(FgDatos.GetData(n_row, 19));
                        n_tot9 = n_tot9 + Convert.ToDouble(FgDatos.GetData(n_row, 20));
                        n_tot10 = n_tot10 + Convert.ToDouble(FgDatos.GetData(n_row, 21));
                        n_tot11 = n_tot11 + Convert.ToDouble(FgDatos.GetData(n_row, 22));
                        n_tot12 = n_tot12 + Convert.ToDouble(FgDatos.GetData(n_row, 23));
                    }
                }

                FgDatos.Rows.Count = FgDatos.Rows.Count + 1;
                FgDatos.SetData(FgDatos.Rows.Count - 1, 9, "TOTAL ==>");

                FgDatos.SetData(FgDatos.Rows.Count - 1, 12, n_tot1.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 13, n_tot2.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 14, n_tot3.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 15, n_tot4.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 16, n_tot5.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 17, n_tot6.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 18, n_tot7.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 19, n_tot8.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 20, n_tot9.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 21, n_tot10.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 22, n_tot11.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 23, n_tot12.ToString("0.00"));
            }

            if (n_Libro == 32)
            {
                n_tot1 = funFlex.FlexSumarCol(FgDatos, 12, 3, FgDatos.Rows.Count - 1);
                n_tot2 = funFlex.FlexSumarCol(FgDatos, 13, 3, FgDatos.Rows.Count - 1);
                n_tot3 = funFlex.FlexSumarCol(FgDatos, 14, 3, FgDatos.Rows.Count - 1);
                n_tot4 = funFlex.FlexSumarCol(FgDatos, 15, 3, FgDatos.Rows.Count - 1);

                FgDatos.Rows.Count = FgDatos.Rows.Count + 1;
                FgDatos.SetData(FgDatos.Rows.Count - 1, 9, "TOTAL ==>");
                FgDatos.SetData(FgDatos.Rows.Count - 1, 12, n_tot1.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 13, n_tot2.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 14, n_tot3.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 15, n_tot4.ToString("0.00"));
            }

            if (n_Libro == 14)
            {
                for (n_row = 3; n_row <= FgDatos.Rows.Count - 1; n_row++)
                {
                    if (FgDatos.GetData(n_row, 4).ToString() == "07")
                    {
                        n_tot1 = n_tot1 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 12)));
                        n_tot2 = n_tot2 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 13)));
                        n_tot3 = n_tot3 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 14)));
                        n_tot4 = n_tot4 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 15)));
                        n_tot5 = n_tot5 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 16)));
                        n_tot6 = n_tot6 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 18)));
                        n_tot7 = n_tot7 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 19)));
                        n_tot8 = n_tot8 + funFunciones.PostivoNegativo(Convert.ToDouble(FgDatos.GetData(n_row, 20)));
                    }
                    else
                    {
                        n_tot1 = n_tot1 + Convert.ToDouble(FgDatos.GetData(n_row, 12));
                        n_tot2 = n_tot2 + Convert.ToDouble(FgDatos.GetData(n_row, 13));
                        n_tot3 = n_tot3 + Convert.ToDouble(FgDatos.GetData(n_row, 14));
                        n_tot4 = n_tot4 + Convert.ToDouble(FgDatos.GetData(n_row, 15));
                        n_tot5 = n_tot5 + Convert.ToDouble(FgDatos.GetData(n_row, 16));
                        n_tot6 = n_tot6 + Convert.ToDouble(FgDatos.GetData(n_row, 18));
                        n_tot7 = n_tot7 + Convert.ToDouble(FgDatos.GetData(n_row, 19));
                        n_tot8 = n_tot8 + Convert.ToDouble(FgDatos.GetData(n_row, 20));
                    }
                }

                FgDatos.Rows.Count = FgDatos.Rows.Count + 1;
                FgDatos.SetData(FgDatos.Rows.Count - 1, 9, "TOTAL ==>");
                FgDatos.SetData(FgDatos.Rows.Count - 1, 12, n_tot1.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 13, n_tot2.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 14, n_tot3.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 15, n_tot4.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 16, n_tot5.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 18, n_tot6.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 19, n_tot7.ToString("0.00"));
                FgDatos.SetData(FgDatos.Rows.Count - 1, 20, n_tot8.ToString("0.00"));
            }
        }

        private void OptAll_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void OptOpcion1_CheckedChanged(object sender, EventArgs e)
        {
            ActivarFechas(true);
        }
        void ActivarFechas(bool b_estado)
        {
            TxtFchFin.Enabled = b_estado;
            TxtFchIni.Enabled = b_estado;
        }

        private void OptOpcion2_CheckedChanged(object sender, EventArgs e)
        {
            ActivarFechas(true);
        }

        private void OptOpcion3_CheckedChanged(object sender, EventArgs e)
        {
            ActivarFechas(false);
        }

        private void FgItems_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (FgItems.Col == 1)
            {
                //int n_idtipexi = 0;
                DataTable dtResul = new DataTable();
                string c_dato = "";

                dtResul = funDatos.DataTableFiltrar(dtItem, "n_idtipexi IN (1, 2)");

                dtResul = objIte.BuscarItem("", "n_id", dtResul, 2);
                if (dtResul != null)
                {
                    if (dtResul.Rows.Count != 0)
                    {
                        c_dato = dtResul.Rows[0]["c_despro"].ToString();        // MOSTRAMOS LA DESCRIPCION DEL ITEM
                        FgItems.SetData(FgItems.Row, 1, c_dato);

                        c_dato = dtResul.Rows[0]["n_id"].ToString();            // MOSTRAMOS EL ID DEL ITEM
                        FgItems.SetData(FgItems.Row, 2, c_dato);

                        FgItems.Rows.Count = FgItems.Rows.Count + 1;
                    }
                }
            }
        }

        private void FgItems_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete")
            {
                if (funFunciones.NulosC(FgItems.GetData(FgItems.Row, 2)) != "")
                {
                    FgItems.RemoveItem(FgItems.Row);
                }
            }
        }

        private void FgCliente_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete")
            {
                if (funFunciones.NulosC(FgCliente.GetData(FgCliente.Row, 2)) != "")
                {
                    FgCliente.RemoveItem(FgCliente.Row);
                }
            }
        }

        private void FgCliente_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (FgItems.Col == 1)
            {
                DataTable dtResul = new DataTable();
                string c_dato = "";

                dtResul = objCli.BuscarCliPro(dtCli, 1 ,"n_id", "");
                if (dtResul != null)
                {
                    if (dtResul.Rows.Count != 0)
                    {
                        c_dato = dtResul.Rows[0]["c_nombre"].ToString();        // MOSTRAMOS LA DESCRIPCION DEL ITEM
                        FgCliente.SetData(FgCliente.Row, 1, c_dato);

                        c_dato = dtResul.Rows[0]["n_id"].ToString();            // MOSTRAMOS EL ID DEL ITEM
                        FgCliente.SetData(FgCliente.Row, 2, c_dato);

                        FgCliente.Rows.Count = FgCliente.Rows.Count + 1;
                    }
                }
            }
        }
    }
}
