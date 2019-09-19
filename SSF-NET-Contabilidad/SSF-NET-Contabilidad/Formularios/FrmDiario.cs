using Helper;
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
using SIAC_Negocio.Tesoreria;

namespace SSF_NET_Contabilidad.Formularios
{
    public partial class FrmDiario : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public int n_Libro;

        DataTable dtTipExi = new DataTable();
        DataTable dtMes = new DataTable();
        DataTable dtLibro = new DataTable();
        DataTable dtLista = new DataTable();

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        Cls_Controles funControl = new Cls_Controles();

        CN_sun_libro o_libro = new CN_sun_libro();
        CN_mae_meses o_meses = new CN_mae_meses();

        CN_alm_inventario objItems = new CN_alm_inventario();

        string[,] arrCabecera1 = new string[15, 5];
        string[,] arrCabecera2 = new string[4, 5];
        public FrmDiario()
        {
            InitializeComponent();
        }

        private void OptTodDoc_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void FrmDiario_Load(object sender, EventArgs e)
        {
            ConfigurarFormulario();
            CargarDT();
        }
        void CargarDT()
        {
            o_libro.mysConec = mysConec;
            o_libro.Listar();
            dtLibro = o_libro.dtLista;

            o_meses.mysConec = mysConec;
            dtMes = o_meses.Listar();
        }
        void ConfigurarFormulario()
        {
            this.Height = 600;
            this.Width = 1094;

            Sz1.Left = 0;
            Sz1.Top = 41;
            Sz1.Height = this.Height - 83;
            Sz1.Width = this.Width - 18;

            this.Text = "CONTABILIDAD - LIBRO DIARIO";

            OptSelLib.Checked = true;
            OptSol.Checked = true;
            
            FgDatos.Cols.Count = 15;
            Cabecera1();
            funFlex.FlexMostrarDatos(FgDatos, arrCabecera1, dtLista, 2, false);
            SetearCabecera1();
        }
        void SetearCabecera1()
        {
            funFlex.Flex_UniColumnas(FgDatos, 0, 1, 7, "DATOS DEL DOCUMENTO", 1);
            funFlex.Flex_UniColumnas(FgDatos, 0, 8, 13, "DATOS DE LA OPERACION", 1);
            funFlex.Flex_UniColumnas(FgDatos, 0, 14, 15, "REFERENCIA", 1);
        }
        void Cabecera1()
        {
            arrCabecera1[0, 0] = "Nº Reg. Doc.";
            arrCabecera1[0, 1] = "80";
            arrCabecera1[0, 2] = "C";
            arrCabecera1[0, 3] = "";
            arrCabecera1[0, 4] = "c_numasi2";

            arrCabecera1[1, 0] = "T.D.";
            arrCabecera1[1, 1] = "30";
            arrCabecera1[1, 2] = "C";
            arrCabecera1[1, 3] = "";
            arrCabecera1[1, 4] = "c_abr";

            arrCabecera1[2, 0] = "Fch. Doc.";
            arrCabecera1[2, 1] = "70";
            arrCabecera1[2, 2] = "F";
            arrCabecera1[2, 3] = "";
            arrCabecera1[2, 4] = "d_orifchdoc";

            arrCabecera1[3, 0] = "Nº Documento";
            arrCabecera1[3, 1] = "110";
            arrCabecera1[3, 2] = "C";
            arrCabecera1[3, 3] = "";
            arrCabecera1[3, 4] = "c_orinumdoc";

            arrCabecera1[4, 0] = "Glosa";
            arrCabecera1[4, 1] = "100";
            arrCabecera1[4, 2] = "C";
            arrCabecera1[4, 3] = "";
            arrCabecera1[4, 4] = "c_origlo";

            arrCabecera1[5, 0] = "Nº RUC / DNI";
            arrCabecera1[5, 1] = "80";
            arrCabecera1[5, 2] = "C";
            arrCabecera1[5, 3] = "";
            arrCabecera1[5, 4] = "c_orinumruc";

            arrCabecera1[6, 0] = "Proveedor / Cliente / Otros";
            arrCabecera1[6, 1] = "150";
            arrCabecera1[6, 2] = "C";
            arrCabecera1[6, 3] = "";
            arrCabecera1[6, 4] = "c_orinomcli";

            arrCabecera1[7, 0] = "Nº Cuenta";
            arrCabecera1[7, 1] = "80";
            arrCabecera1[7, 2] = "C";
            arrCabecera1[7, 3] = "";
            arrCabecera1[7, 4] = "c_cuecon";

            arrCabecera1[8, 0] = "Nombre de la Cuenta";
            arrCabecera1[8, 1] = "200";
            arrCabecera1[8, 2] = "C";
            arrCabecera1[8, 3] = "";
            arrCabecera1[8, 4] = "c_des";

            arrCabecera1[9, 0] = "M.";
            arrCabecera1[9, 1] = "30";
            arrCabecera1[9, 2] = "C";
            arrCabecera1[9, 3] = "";
            arrCabecera1[9, 4] = "c_simbolo";

            arrCabecera1[10, 0] = "T.C.";
            arrCabecera1[10, 1] = "40";
            arrCabecera1[10, 2] = "D";
            arrCabecera1[10, 3] = "0.000";
            arrCabecera1[10, 4] = "n_tc";

            arrCabecera1[11, 0] = "Debe";
            arrCabecera1[11, 1] = "80";
            arrCabecera1[11, 2] = "D";
            arrCabecera1[11, 3] = "0.00";
            arrCabecera1[11, 4] = "n_impdelsol";

            arrCabecera1[12, 0] = "Haber";
            arrCabecera1[12, 1] = "80";
            arrCabecera1[12, 2] = "D";
            arrCabecera1[12, 3] = "0.00";
            arrCabecera1[12, 4] = "n_imphalsol";

            arrCabecera1[13, 0] = "n_idlib";
            arrCabecera1[13, 1] = "0";
            arrCabecera1[13, 2] = "N";
            arrCabecera1[13, 3] = "";
            arrCabecera1[13, 4] = "n_lin";

            arrCabecera1[14, 0] = "n_orden";
            arrCabecera1[14, 1] = "0";
            arrCabecera1[14, 2] = "N";
            arrCabecera1[14, 3] = "";
            arrCabecera1[14, 4] = "n_orden";
        }
        void Cabecera2()
        {
            arrCabecera2[0, 0] = "Nº Registro";
            arrCabecera2[0, 1] = "70";
            arrCabecera2[0, 2] = "C";
            arrCabecera2[0, 3] = "";
            arrCabecera2[0, 4] = "c_numreg";

            arrCabecera2[1, 0] = "Origen";
            arrCabecera2[1, 1] = "250";
            arrCabecera2[1, 2] = "C";
            arrCabecera2[1, 3] = "";
            arrCabecera2[1, 4] = "c_deslib";

            arrCabecera2[2, 0] = "T.D.";
            arrCabecera2[2, 1] = "40";
            arrCabecera2[2, 2] = "C";
            arrCabecera2[2, 3] = "";
            arrCabecera2[2, 4] = "c_abr";

            arrCabecera2[3, 0] = "Nº Documento";
            arrCabecera2[3, 1] = "100";
            arrCabecera2[3, 2] = "C";
            arrCabecera2[3, 3] = "";
            arrCabecera2[3, 4] = "c_numdoc";

            arrCabecera2[4, 0] = "Fch. Emision";
            arrCabecera2[4, 1] = "70";
            arrCabecera2[4, 2] = "F";
            arrCabecera2[4, 3] = "dd/MM/yyyy";
            arrCabecera2[4, 4] = "d_fchdoc";

            arrCabecera2[5, 0] = "Fch. Vencimiento";
            arrCabecera2[5, 1] = "70";
            arrCabecera2[5, 2] = "F";
            arrCabecera2[5, 3] = "dd/MM/yyyy";
            arrCabecera2[5, 4] = "d_fchven";

            arrCabecera2[6, 0] = "Moneda";
            arrCabecera2[6, 1] = "40";
            arrCabecera2[6, 2] = "C";
            arrCabecera2[6, 3] = "";
            arrCabecera2[6, 4] = "c_mondes";

            arrCabecera2[7, 0] = "Importe";
            arrCabecera2[7, 1] = "80";
            arrCabecera2[7, 2] = "D";
            arrCabecera2[7, 3] = "0.00";
            arrCabecera2[7, 4] = "n_imptotcom";

            arrCabecera2[8, 0] = "T.C.";
            arrCabecera2[8, 1] = "50";
            arrCabecera2[8, 2] = "D";
            arrCabecera2[8, 3] = "0.000";
            arrCabecera2[8, 4] = "n_tc";

            arrCabecera2[9, 0] = "Debe";
            arrCabecera2[9, 1] = "80";
            arrCabecera2[9, 2] = "D";
            arrCabecera2[9, 3] = "0.00";
            arrCabecera2[9, 4] = "n_debe";

            arrCabecera2[10, 0] = "Haber";
            arrCabecera2[10, 1] = "80";
            arrCabecera2[10, 2] = "D";
            arrCabecera2[10, 3] = "0.00";
            arrCabecera2[10, 4] = "n_haber";

            arrCabecera2[11, 0] = "Saldo";
            arrCabecera2[11, 1] = "80";
            arrCabecera2[11, 2] = "D";
            arrCabecera2[11, 3] = "0.00";
            arrCabecera2[11, 4] = "n_saldo";
        }

        private void ToolBuscar_Click(object sender, EventArgs e)
        {

        }
    }
}
