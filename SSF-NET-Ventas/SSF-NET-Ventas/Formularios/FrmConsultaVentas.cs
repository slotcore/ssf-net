﻿using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Logistica;
using SIAC_Entidades.Maestros;
using SIAC_Entidades.Almacen;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Ventas;
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

namespace SSF_NET_Ventas.Formularios
{
    public partial class FrmConsultaVentas : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public int n_Libro;

        DataTable dtTipExi = new DataTable();
        DataTable dtProv = new DataTable();
        DataTable dtItem = new DataTable();
        DataTable dtLista = new DataTable();
        DataTable dttipexi = new DataTable();

        CN_mae_clipro objPro = new CN_mae_clipro();
        CN_alm_inventario objIte = new CN_alm_inventario();
        CN_sun_tipexi o_tipexi = new CN_sun_tipexi();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        Cls_Controles funControl = new Cls_Controles();
        //CN_mae_clipro objPro = new CN_mae_clipro();
        CN_alm_inventario objItems = new CN_alm_inventario();

        string[,] arrCabeceraFlex1 = new string[2, 5];
        string[,] arrCabeceraFlex2 = new string[2, 5];
        string[,] arrCabecera1 = new string[13, 5];
        string[,] arrCabecera2 = new string[18, 5];
        string[,] arrCabecera3 = new string[11, 5];

        public FrmConsultaVentas()
        {
            InitializeComponent();
        }

        private void FrmConsultaVentas_Load(object sender, EventArgs e)
        {
            ConfigurarFormulario();
            CargarDT();
        }
        void CargarDT()
        {
            objIte.mysConec = mysConec;
            dtItem = objIte.Listar(STU_SISTEMA.EMPRESAID, 1, 1);

            objPro.mysConec = mysConec;
            dtProv = objPro.ListarCliente(STU_SISTEMA.EMPRESAID, STU_SISTEMA.SYS_UNIBD);

            o_tipexi.mysConec = mysConec;
            dttipexi = o_tipexi.Listar();

            funDatos.ComboBoxCargarDataTable(CboTipPro, dttipexi, "n_id", "c_des");
        }
        void ConfigurarFormulario()
        {
            this.Height = 600;
            this.Width = 1010;

            Sz1.Left = 0;
            Sz1.Top = 41;
            Sz1.Height = this.Height - 83;
            Sz1.Width = this.Width - 18;

            if (n_Libro == 14) { this.Text = "VENTAS - CONSULTA DE VENTAS "; }
            
            OptRes.Checked = true;
            OptFchEmi.Checked = true;
            OptTod.Checked = true;
            OptTodDoc.Checked = true;

            //funControl.dtpBlanquea(TxtFchIni);
            //funControl.dtpBlanquea(TxtFchFin);
            TxtFchIni.Text = "01/01/"+STU_SISTEMA.ANOTRABAJO.ToString();
            TxtFchFin.Text = DateTime.Now.ToString("dd/MM/yyyy");

            arrCabeceraFlex1[0, 0] = "Cliente";
            arrCabeceraFlex1[0, 1] = "420";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_tipexides";

            arrCabeceraFlex1[1, 0] = "Id";
            arrCabeceraFlex1[1, 1] = "0";
            arrCabeceraFlex1[1, 2] = "N";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_itedes";

            funFlex.FlexMostrarDatos(FgPro, arrCabeceraFlex1, dtLista, 1, false);

            arrCabeceraFlex2[0, 0] = "Item(s)";
            arrCabeceraFlex2[0, 1] = "420";
            arrCabeceraFlex2[0, 2] = "C";
            arrCabeceraFlex2[0, 3] = "";
            arrCabeceraFlex2[0, 4] = "c_tipexides";

            arrCabeceraFlex2[1, 0] = "Id";
            arrCabeceraFlex2[1, 1] = "0";
            arrCabeceraFlex2[1, 2] = "N";
            arrCabeceraFlex2[1, 3] = "";
            arrCabeceraFlex2[1, 4] = "c_itedes";

            funFlex.FlexMostrarDatos(FgItem, arrCabeceraFlex2, dtLista, 1, false);

            FgDatos.Cols.Count = 13;
            Cabecera1();
            funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 2, false);
            SetearCabecera1();
        }
        void Cabecera1()
        {
            arrCabecera1[0, 0] = "Nº R.U.C.";
            arrCabecera1[0, 1] = "100";
            arrCabecera1[0, 2] = "C";
            arrCabecera1[0, 3] = "";
            arrCabecera1[0, 4] = "c_numdoc";

            arrCabecera1[1, 0] = "Proveedor";
            arrCabecera1[1, 1] = "300";
            arrCabecera1[1, 2] = "C";
            arrCabecera1[1, 3] = "";
            arrCabecera1[1, 4] = "c_nombre";

            arrCabecera1[2, 0] = "Nº Docts.";
            arrCabecera1[2, 1] = "40";
            arrCabecera1[2, 2] = "N";
            arrCabecera1[2, 3] = "";
            arrCabecera1[2, 4] = "n_candoc";

            arrCabecera1[3, 0] = "Imp. Soles";
            arrCabecera1[3, 1] = "70";
            arrCabecera1[3, 2] = "D";
            arrCabecera1[3, 3] = "";
            arrCabecera1[3, 4] = "n_impsol";

            arrCabecera1[4, 0] = "Saldo Soles";
            arrCabecera1[4, 1] = "70";
            arrCabecera1[4, 2] = "D";
            arrCabecera1[4, 3] = "";
            arrCabecera1[4, 4] = "n_impsalsol";

            arrCabecera1[5, 0] = "Imp. Dolares";
            arrCabecera1[5, 1] = "70";
            arrCabecera1[5, 2] = "D";
            arrCabecera1[5, 3] = "";
            arrCabecera1[5, 4] = "n_impdol";

            arrCabecera1[6, 0] = "Saldo Dolares";
            arrCabecera1[6, 1] = "70";
            arrCabecera1[6, 2] = "D";
            arrCabecera1[6, 3] = "";
            arrCabecera1[6, 4] = "n_impsaldol";

            arrCabecera1[7, 0] = "Imp. Exp. Soles";
            arrCabecera1[7, 1] = "70";
            arrCabecera1[7, 2] = "D";
            arrCabecera1[7, 3] = "";
            arrCabecera1[7, 4] = "n_impexpsol";

            arrCabecera1[8, 0] = "Abo. Exp. Soles";
            arrCabecera1[8, 1] = "70";
            arrCabecera1[8, 2] = "D";
            arrCabecera1[8, 3] = "";
            arrCabecera1[8, 4] = "n_impaboexpsol";

            arrCabecera1[9, 0] = "Sal Exp. Soles";
            arrCabecera1[9, 1] = "70";
            arrCabecera1[9, 2] = "D";
            arrCabecera1[9, 3] = "";
            arrCabecera1[9, 4] = "n_impsalexpsol";

            arrCabecera1[10, 0] = "Imp. Exp. Dolares";
            arrCabecera1[10, 1] = "70";
            arrCabecera1[10, 2] = "D";
            arrCabecera1[10, 3] = "";
            arrCabecera1[10, 4] = "n_impexpdol";

            arrCabecera1[11, 0] = "Abo. Exp. Dolares";
            arrCabecera1[11, 1] = "70";
            arrCabecera1[11, 2] = "D";
            arrCabecera1[11, 3] = "";
            arrCabecera1[11, 4] = "n_impaboexpdol";

            arrCabecera1[12, 0] = "Sal Exp. Dolares";
            arrCabecera1[12, 1] = "70";
            arrCabecera1[12, 2] = "D";
            arrCabecera1[12, 3] = "";
            arrCabecera1[12, 4] = "n_impsalexpdol";
        }

        void Cabecera2()
        {
            arrCabecera2[0, 0] = "Nº Registro";
            arrCabecera2[0, 1] = "80";
            arrCabecera2[0, 2] = "C";
            arrCabecera2[0, 3] = "";
            arrCabecera2[0, 4] = "c_numreg";

            arrCabecera2[1, 0] = "Cliente";
            arrCabecera2[1, 1] = "300";
            arrCabecera2[1, 2] = "C";
            arrCabecera2[1, 3] = "";
            arrCabecera2[1, 4] = "c_cliente";

            arrCabecera2[2, 0] = "T.D.";
            arrCabecera2[2, 1] = "40";
            arrCabecera2[2, 2] = "C";
            arrCabecera2[2, 3] = "";
            arrCabecera2[2, 4] = "c_destipdoc";

            arrCabecera2[3, 0] = "Nº Documento";
            arrCabecera2[3, 1] = "100";
            arrCabecera2[3, 2] = "C";
            arrCabecera2[3, 3] = "";
            arrCabecera2[3, 4] = "c_numdoc";

            arrCabecera2[4, 0] = "Fecha Documento";
            arrCabecera2[4, 1] = "70";
            arrCabecera2[4, 2] = "F";
            arrCabecera2[4, 3] = "dd/MM/yyyy";
            arrCabecera2[4, 4] = "d_fchdoc";

            arrCabecera2[5, 0] = "Fecha Vencimiento";
            arrCabecera2[5, 1] = "70";
            arrCabecera2[5, 2] = "F";
            arrCabecera2[5, 3] = "dd/MM/yyyy";
            arrCabecera2[5, 4] = "d_fchven";

            arrCabecera2[6, 0] = "Condicion de Pago";
            arrCabecera2[6, 1] = "80";
            arrCabecera2[6, 2] = "C";
            arrCabecera2[6, 3] = "";
            arrCabecera2[6, 4] = "c_desconpag";

            arrCabecera2[7, 0] = "Dias atrazo";
            arrCabecera2[7, 1] = "60";
            arrCabecera2[7, 2] = "N";
            arrCabecera2[7, 3] = "";
            arrCabecera2[7, 4] = "n_diaatrazo";

            arrCabecera2[8, 0] = "Glosa";
            arrCabecera2[8, 1] = "100";
            arrCabecera2[8, 2] = "D";
            arrCabecera2[8, 3] = "";
            arrCabecera2[8, 4] = "c_glosa";

            arrCabecera2[9, 0] = "Moneda";
            arrCabecera2[9, 1] = "60";
            arrCabecera2[9, 2] = "C";
            arrCabecera2[9, 3] = "";
            arrCabecera2[9, 4] = "c_desmon";

            arrCabecera2[10, 0] = "Tipo Cambio";
            arrCabecera2[10, 1] = "60";
            arrCabecera2[10, 2] = "D";
            arrCabecera2[10, 3] = "0.000";
            arrCabecera2[10, 4] = "n_tc";

            arrCabecera2[11, 0] = "Importe";
            arrCabecera2[11, 1] = "80";
            arrCabecera2[11, 2] = "D";
            arrCabecera2[11, 3] = "0.00";
            arrCabecera2[11, 4] = "n_impsol";

            arrCabecera2[12, 0] = "Saldo";
            arrCabecera2[12, 1] = "80";
            arrCabecera2[12, 2] = "D";
            arrCabecera2[12, 3] = "0.00";
            arrCabecera2[12, 4] = "n_impsalsol";

            arrCabecera2[13, 0] = "Importe";
            arrCabecera2[13, 1] = "80";
            arrCabecera2[13, 2] = "D";
            arrCabecera2[13, 3] = "0.00";
            arrCabecera2[13, 4] = "n_impdol";

            arrCabecera2[14, 0] = "Saldo";
            arrCabecera2[14, 1] = "80";
            arrCabecera2[14, 2] = "D";
            arrCabecera2[14, 3] = "0.00";
            arrCabecera2[14, 4] = "n_impsaldol";

            arrCabecera2[15, 0] = "Total";
            arrCabecera2[15, 1] = "80";
            arrCabecera2[15, 2] = "D";
            arrCabecera2[15, 3] = "0.00";
            arrCabecera2[15, 4] = "n_imptotsol";

            arrCabecera2[16, 0] = "Abono";
            arrCabecera2[16, 1] = "80";
            arrCabecera2[16, 2] = "D";
            arrCabecera2[16, 3] = "0.00";
            arrCabecera2[16, 4] = "n_impabototsol";

            arrCabecera2[17, 0] = "Saldo";
            arrCabecera2[17, 1] = "80";
            arrCabecera2[17, 2] = "D";
            arrCabecera2[17, 3] = "0.00";
            arrCabecera2[17, 4] = "n_impsaltotol";
        }
        void Cabecera3()
        {
            arrCabecera3[0, 0] = "Cliente";
            arrCabecera3[0, 1] = "300";
            arrCabecera3[0, 2] = "C";
            arrCabecera3[0, 3] = "";
            arrCabecera3[0, 4] = "c_nombre";

            arrCabecera3[1, 0] = "T.D.";
            arrCabecera3[1, 1] = "50";
            arrCabecera3[1, 2] = "C";
            arrCabecera3[1, 3] = "";
            arrCabecera3[1, 4] = "c_codpro";

            arrCabecera3[2, 0] = "Nº Documento";
            arrCabecera3[2, 1] = "110";
            arrCabecera3[2, 2] = "C";
            arrCabecera3[2, 3] = "";
            arrCabecera3[2, 4] = "c_numdoc";

            arrCabecera3[3, 0] = "Fch. Emision";
            arrCabecera3[3, 1] = "70";
            arrCabecera3[3, 2] = "F";
            arrCabecera3[3, 3] = "dd/MM/yyyy";
            arrCabecera3[3, 4] = "d_fchdoc";

            arrCabecera3[4, 0] = "Fch. Vencimiento";
            arrCabecera3[4, 1] = "70";
            arrCabecera3[4, 2] = "F";
            arrCabecera3[4, 3] = "dd/MM/yyyy";
            arrCabecera3[4, 4] = "d_fchven";

            arrCabecera3[5, 0] = "Cod. Item";
            arrCabecera3[5, 1] = "80";
            arrCabecera3[5, 2] = "C";
            arrCabecera3[5, 3] = "";
            arrCabecera3[5, 4] = "c_codpro";

            arrCabecera3[6, 0] = "Descripcion";
            arrCabecera3[6, 1] = "300";
            arrCabecera3[6, 2] = "C";
            arrCabecera3[6, 3] = "";
            arrCabecera3[6, 4] = "c_despro";

            arrCabecera3[7, 0] = "Uni. Med.";
            arrCabecera3[7, 1] = "50";
            arrCabecera3[7, 2] = "C";
            arrCabecera3[7, 3] = "";
            arrCabecera3[7, 4] = "c_desunimed";

            arrCabecera3[8, 0] = "Cantidad";
            arrCabecera3[8, 1] = "70";
            arrCabecera3[8, 2] = "D";
            arrCabecera3[8, 3] = "0.00";
            arrCabecera3[8, 4] = "n_canpro";

            arrCabecera3[9, 0] = "Precio Unitario";
            arrCabecera3[9, 1] = "80";
            arrCabecera3[9, 2] = "D";
            arrCabecera3[9, 3] = "0.00";
            arrCabecera3[9, 4] = "n_preunibru";

            arrCabecera3[10, 0] = "Imp Total";
            arrCabecera3[10, 1] = "80";
            arrCabecera3[10, 2] = "D";
            arrCabecera3[10, 3] = "0.00";
            arrCabecera3[10, 4] = "n_imptot";
            
            //arrCabecera3[0, 0] = "Cliente";
            //arrCabecera3[0, 1] = "300";
            //arrCabecera3[0, 2] = "C";
            //arrCabecera3[0, 3] = "";
            //arrCabecera3[0, 4] = "c_nombre";

            //arrCabecera3[1, 0] = "Codigo";
            //arrCabecera3[1, 1] = "120";
            //arrCabecera3[1, 2] = "C";
            //arrCabecera3[1, 3] = "";
            //arrCabecera3[1, 4] = "c_codpro";

            //arrCabecera3[2, 0] = "Producto";
            //arrCabecera3[2, 1] = "350";
            //arrCabecera3[2, 2] = "C";
            //arrCabecera3[2, 3] = "";
            //arrCabecera3[2, 4] = "c_despro";

            //arrCabecera3[3, 0] = "Uni. Med.";
            //arrCabecera3[3, 1] = "40";
            //arrCabecera3[3, 2] = "C";
            //arrCabecera3[3, 3] = "";
            //arrCabecera3[3, 4] = "c_desunimed";

            //arrCabecera3[4, 0] = "Cantidad";
            //arrCabecera3[4, 1] = "80";
            //arrCabecera3[4, 2] = "D";
            //arrCabecera3[4, 3] = "0.00";
            //arrCabecera3[4, 4] = "n_totcan";

            //arrCabecera3[5, 0] = "Importe Soles";
            //arrCabecera3[5, 1] = "80";
            //arrCabecera3[5, 2] = "D";
            //arrCabecera3[5, 3] = "0.00";
            //arrCabecera3[5, 4] = "n_impsol";

            //arrCabecera3[6, 0] = "Importe Dolares";
            //arrCabecera3[6, 1] = "80";
            //arrCabecera3[6, 2] = "D";
            //arrCabecera3[6, 3] = "0.00";
            //arrCabecera3[6, 4] = "n_impdol";

            //arrCabecera3[7, 0] = "Importe Exp. Soles";
            //arrCabecera3[7, 1] = "80";
            //arrCabecera3[7, 2] = "D";
            //arrCabecera3[7, 3] = "0.00";
            //arrCabecera3[7, 4] = "n_impexpsol";
        }
        private void FrmConsultaVentas_Resize(object sender, EventArgs e)
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
            int n_idmon = 0;
            int n_tipfch = 0;
            int n_tipsal = 0;

            if (funFunciones.NulosC(TxtFchIni.Text) == "")
            {
                MessageBox.Show("¡ No ha indicado la fecha de inicio para la consulta !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchIni.Focus();
                return;
            }
            if (funFunciones.NulosC(TxtFchFin.Text) == "")
            {
                MessageBox.Show("¡ No ha indicado la fecha final para la consulta !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchFin.Focus();
                return;
            }

            DateTime d_fchini = Convert.ToDateTime(TxtFchIni.Text);
            DateTime d_fchfin = Convert.ToDateTime(TxtFchFin.Text);

            if (d_fchini > d_fchfin)
            {
                MessageBox.Show("¡ La fecha de inicio no puede ser mayor a la fecha final de la consulta !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchIni.Focus();
                return;
            }

            
            if (OptTod.Checked == true) { n_idmon = 0; }
            if (OptSol.Checked == true) { n_idmon = 115; }
            if (OptDol.Checked == true) { n_idmon = 151; }

            if (OptFchEmi.Checked == true) { n_tipfch = 1; }
            if (OptFchVen.Checked == true) { n_tipfch = 2; }
            if (OptFchReg.Checked == true) { n_tipfch = 3; }

            if (OptTodDoc.Checked == true) { n_tipsal = 1; }
            if (OptPen.Checked == true) { n_tipsal = 2; }
            if (OptPag.Checked == true) { n_tipsal = 3; }

            int n_tipoconsulta = 0;
            CN_vta_ventas funCom = new CN_vta_ventas();
            funCom.mysConec = mysConec;
            funCom.STU_SISTEMA = STU_SISTEMA;
            if (OptRes.Checked == true) { n_tipoconsulta = 1; }
            if (OptDet.Checked == true) { n_tipoconsulta = 2; }
            if (OptResDet.Checked == true) { n_tipoconsulta = 3; }

            string c_CadINCli = funFlex.Flex_CadenaIN(FgPro,2,1);
            string c_CadINIte = funFlex.Flex_CadenaIN(FgItem, 2, 1); ;

            funCom.Consulta2(STU_SISTEMA.EMPRESAID, TxtFchIni.Text, TxtFchFin.Text, n_idmon, n_tipfch, n_tipsal, n_Libro, n_tipoconsulta, c_CadINCli, c_CadINIte);
            dtLista = funCom.dtLista1;

            if (OptRes.Checked == true)
            {
                FgDatos.Cols.Count = 13;
                Cabecera1();
                funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 2, true);
                SetearCabecera1();
                MostrarTotales();
            }
            if (OptDet.Checked == true)
            {
                FgDatos.Cols.Count = 18;
                Cabecera2();
                funFlex.FlexMostrarDatos(FgDatos, arrCabecera2, dtLista, 2, true);
                SetearCabecera2();
            }
            if (OptResDet.Checked == true)
            {
                FgDatos.Cols.Count = 7;
                Cabecera3();
                funFlex.FlexMostrarDatos(FgDatos, arrCabecera3, dtLista, 2, true);
                SetearCabecera3();
            } 
        }
        void MostrarTotales()
        {
            double n_valor = 0;

            FgDatos.Rows.Count = FgDatos.Rows.Count + 1;
            FgDatos.SetData(FgDatos.Rows.Count - 1, 2, "TOTALES ==>");

            n_valor = funFlex.FlexSumarCol(FgDatos, 3, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 3, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 4, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 4, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 5, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 5, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 6, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 6, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 7, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 7, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 8, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 8, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 9, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 9, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 10, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 10, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 11, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 11, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 12, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 12, n_valor.ToString("0.00"));

            n_valor = funFlex.FlexSumarCol(FgDatos, 13, 2, FgDatos.Rows.Count - 2);
            FgDatos.SetData(FgDatos.Rows.Count - 1, 13, n_valor.ToString("0.00"));
            
            funFlex.Flex_PintarCeldas(FgDatos, FgDatos.Rows.Count - 1, 1, FgDatos.Rows.Count - 1, FgDatos.Cols.Count - 1, Color.Black, Color.PaleGreen);
            funFlex.Flex_PintarCeldas(FgDatos, 1, FgDatos.Cols.Count - 1, FgDatos.Rows.Count - 1, FgDatos.Cols.Count - 1, Color.Black, Color.Pink);
        }
        void SetearCabecera1()
        {
            funFlex.Flex_FixUniColumnas(FgDatos, 0, 4, 5, "MONEDA NACIONAL", 1);
            funFlex.Flex_FixUniColumnas(FgDatos, 0, 6, 7, "MONEDA EXTRANJERA", 1);
            funFlex.Flex_FixUniColumnas(FgDatos, 0, 8, 10, "EXPRESADO EN MN", 1);
            funFlex.Flex_FixUniColumnas(FgDatos, 0, 11, 13, "EXPRESADO EN ME", 1);
        }
        void SetearCabecera2()
        {

            funFlex.Flex_FixUniColumnas(FgDatos, 0, 12, 13, "MONEDA NACIONAL", 1);
            funFlex.Flex_FixUniColumnas(FgDatos, 0, 14, 15, "MONEDA EXTRANJERA", 1);
            funFlex.Flex_FixUniColumnas(FgDatos, 0, 16, 18, "EXPRESADO EN MN", 1);
        }
        void SetearCabecera3()
        {
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
                funControl.dtpBlanquea(TxtFchIni);
            }
        }
        private void TxtFchFin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Delete")
            {
                funControl.dtpBlanquea(TxtFchFin);
            }
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
        private void ToolExportar_Click(object sender, EventArgs e)
        {
            string c_nomarch = STU_SISTEMA.EMPRESARUC + "-" + STU_SISTEMA.ANOTRABAJO.ToString() + "-" + STU_SISTEMA.MESTRABAJO.ToString() + "-08-" + ".xls";
            funFlex.ExportToExcel(FgDatos, STU_SISTEMA.EMPRESANOMBRE, STU_SISTEMA.EMPRESARUC, "CONSULTA DE COMPRAS", "RESUMIDO X PROVEEDORES", c_nomarch);
        }
        private void OptRes_CheckedChanged(object sender, EventArgs e)
        {
            if (OptRes.Checked == true)
            {
                CboTipPro.Enabled = false;
                CboTipPro.SelectedValue = 0;
                FgPro.Enabled = false;
                FgItem.Enabled = false;

                groupBox5.Enabled = false;

                CmdAddPro.Enabled = false;
                CmdDelPro.Enabled = false;
                CmdDelIte.Enabled = false;
                CmdAddIte.Enabled = false;

                FgItem.Rows.Count = 1;
                FgPro.Rows.Count = 1;

                FgDatos.Cols.Count = 13;
                Cabecera1();
                funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 2, false);
                SetearCabecera1();
            }
        }
        private void OptDet_CheckedChanged(object sender, EventArgs e)
        {
            if (OptDet.Checked == true)
            {
                CboTipPro.Enabled = false;
                CboTipPro.SelectedValue = 0;
                FgPro.Enabled = true;
                FgItem.Enabled = false;
                CmdAddPro.Enabled = true;
                CmdDelPro.Enabled = true;
                CmdDelIte.Enabled = false;
                CmdAddIte.Enabled = false;
                FgItem.Rows.Count = 1;
                FgPro.Rows.Count = 1;

                FgItem.Rows.Count = 1;
                FgPro.Rows.Count = 2;
                
                groupBox5.Enabled = true;

                FgPro.Cols[1].ComboList = "...";

                FgPro.AllowEditing = true;
                FgItem.AllowEditing = true;

                FgDatos.Cols.Count = 18;
                Cabecera2();
                funFlex.FlexMostrarDatos(FgDatos, arrCabecera2, dtLista, 2, false);
                SetearCabecera2();
            }
        }
        private void OptResDet_CheckedChanged(object sender, EventArgs e)
        {
            if (OptResDet.Checked == true)
            {
                FgDatos.Cols.Count = 18;

                FgPro.Enabled = true;
                FgItem.Enabled = true;

                FgItem.Rows.Count = 1;
                FgPro.Rows.Count = 1;

                FgItem.Rows.Count = 2;
                FgPro.Rows.Count = 2;
                
                groupBox5.Enabled = false;

                CmdAddPro.Enabled = true;
                CmdDelPro.Enabled = true;
                CmdDelIte.Enabled = true;
                CmdAddIte.Enabled = true;

                FgPro.Cols[1].ComboList = "...";
                FgItem.Cols[1].ComboList = "...";

                FgPro.AllowEditing = true;
                FgItem.AllowEditing = true;

                CboTipPro.Enabled = true;
                CboTipPro.SelectedValue = 0;

                Cabecera3();
                funFlex.FlexMostrarDatos(FgDatos, arrCabecera3, dtLista, 2, false);
                SetearCabecera3();
            }
        }
        private void CmdAddPro_Click(object sender, EventArgs e)
        {
            if (funFunciones.NulosC(FgPro.GetData(FgPro.Rows.Count - 1, 1)) == "")
            {
                MessageBox.Show("¡ No ha especificado un proveedor en la ultima fila !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                FgPro.Focus();
                return;
            }
            FgPro.Rows.Count = FgPro.Rows.Count + 1;
        }
        private void CmdAddIte_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(CboTipPro.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el tipo de existencia !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                c1DockingTab1.SelectedIndex = 0;
                CboTipPro.Focus();
                return;
            }
            if (funFunciones.NulosC(FgItem.GetData(FgItem.Rows.Count - 1, 1)) == "")
            {
                MessageBox.Show("¡ No ha especificado un item en la ultima fila !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                FgItem.Focus();
                return;
            }
            FgItem.Rows.Count = FgItem.Rows.Count + 1;
        }
        private void CmdDelIte_Click(object sender, EventArgs e)
        {
            if (FgItem.Rows.Count == 1) { return; }
            FgItem.RemoveItem(FgItem.Row);
        }
        private void CmdDelPro_Click(object sender, EventArgs e)
        {
            if (FgPro.Rows.Count == 1) { return; }
            FgPro.RemoveItem(FgPro.Row);
        }

        private void FgPro_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (FgPro.Col == 1)
            {
                DataTable dtResult = new DataTable();
                string c_dato = "";
                objPro.mysConec = mysConec;
                dtResult = objPro.BuscarCliPro(dtProv, 1, "n_id", "");
                if (dtResult != null)
                {
                    if (dtResult.Rows.Count != 0)
                    {
                        c_dato = dtResult.Rows[0]["c_nombre"].ToString();
                        FgPro.SetData(FgPro.Row, 1, c_dato);

                        c_dato = dtResult.Rows[0]["n_id"].ToString();
                        FgPro.SetData(FgPro.Row, 2, c_dato);

                        FgPro.Rows.Count = FgPro.Rows.Count + 1;
                    }
                }
            }
        }

        private void FgItem_CellButtonClick(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (FgItem.Col == 1)
            {
                if (Convert.ToInt32(CboTipPro.SelectedValue) == 0)
                {
                    MessageBox.Show("¡ No ha especificado el tipo de existencia !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    c1DockingTab1.SelectedIndex = 0;
                    CboTipPro.Focus();
                    return;
                }

                DataTable dtResul = new DataTable();
                string c_dato = "";
                int n_idtippro = Convert.ToInt32(CboTipPro.SelectedValue);
                if (n_idtippro == 0)
                {
                    dtResul = objItems.BuscarItem("", "n_id", dtItem, n_idtippro);
                }
                else
                {
                    dtResul = objItems.BuscarItem("", "n_id", dtItem, n_idtippro);
                }

                if (dtResul != null)
                {
                    if (dtResul.Rows.Count != 0)
                    {
                        c_dato = dtResul.Rows[0]["c_despro"].ToString();        // MOSTRAMOS LA DESCRIPCION DEL ITEM
                        FgItem.SetData(FgItem.Row, 1, c_dato);

                        c_dato = dtResul.Rows[0]["n_id"].ToString();            // MOSTRAMOS EL ID DEL ITEM
                        FgItem.SetData(FgItem.Row, 2, c_dato);

                        FgItem.Rows.Count = FgItem.Rows.Count + 1;
                    }
                }
            }
        }
    }

}
