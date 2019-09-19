using MySql.Data.MySqlClient;
using SIAC_Objetos.Sistema;
using System.Windows.Forms;
using SIAC_Negocio.Produccion;
using System.Data;
using Helper;
using SIAC_Objetos;
using System;
using System.Drawing;

namespace SSF_NET_Produccion.Formularios
{
    public partial class FrmVerEstacionalidad : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public int n_IdItem;

        CN_pro_estacionalidad o_estacion = new CN_pro_estacionalidad();
        CN_pro_productosrecetas o_receta = new CN_pro_productosrecetas();
        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        DataTable dtLista = new DataTable();

        string[,] arrCabeceraFlex1 = new string[12, 5];

        public FrmVerEstacionalidad()
        {
            InitializeComponent();
        }

        private void FrmVerEstacionalidad_Load(object sender, System.EventArgs e)
        {
            CargarCombos();
            ConfigurarFormulario();
        }
        void ConfigurarFormulario()
        {
            DataTable dtRes = new DataTable();
            int n_val = 0;
            this.Height = 295;
            this.Width = 779;
            int n_ancho = 62;

            arrCabeceraFlex1[0, 0] = "Enero";
            arrCabeceraFlex1[0, 1] = n_ancho.ToString();
            arrCabeceraFlex1[0, 2] = "S";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_tipexides";

            arrCabeceraFlex1[1, 0] = "Febrero";
            arrCabeceraFlex1[1, 1] = n_ancho.ToString();
            arrCabeceraFlex1[1, 2] = "";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_itedes";

            arrCabeceraFlex1[2, 0] = "Marzo";
            arrCabeceraFlex1[2, 1] = n_ancho.ToString();
            arrCabeceraFlex1[2, 2] = "";
            arrCabeceraFlex1[2, 3] = "";
            arrCabeceraFlex1[2, 4] = "c_itedes";

            arrCabeceraFlex1[3, 0] = "Abril";
            arrCabeceraFlex1[3, 1] = n_ancho.ToString();
            arrCabeceraFlex1[3, 2] = "";
            arrCabeceraFlex1[3, 3] = "";
            arrCabeceraFlex1[3, 4] = "c_itedes";

            arrCabeceraFlex1[4, 0] = "Mayo";
            arrCabeceraFlex1[4, 1] = n_ancho.ToString();
            arrCabeceraFlex1[4, 2] = "";
            arrCabeceraFlex1[4, 3] = "";
            arrCabeceraFlex1[4, 4] = "c_itedes";

            arrCabeceraFlex1[5, 0] = "Junio";
            arrCabeceraFlex1[5, 1] = n_ancho.ToString();
            arrCabeceraFlex1[5, 2] = "";
            arrCabeceraFlex1[5, 3] = "";
            arrCabeceraFlex1[5, 4] = "c_itedes";

            arrCabeceraFlex1[6, 0] = "Julio";
            arrCabeceraFlex1[6, 1] = n_ancho.ToString();
            arrCabeceraFlex1[6, 2] = "";
            arrCabeceraFlex1[6, 3] = "";
            arrCabeceraFlex1[6, 4] = "c_itedes";

            arrCabeceraFlex1[7, 0] = "Agosto";
            arrCabeceraFlex1[7, 1] = n_ancho.ToString();
            arrCabeceraFlex1[7, 2] = "";
            arrCabeceraFlex1[7, 3] = "";
            arrCabeceraFlex1[7, 4] = "c_itedes";

            arrCabeceraFlex1[8, 0] = "Setiembre";
            arrCabeceraFlex1[8, 1] = n_ancho.ToString();
            arrCabeceraFlex1[8, 2] = "";
            arrCabeceraFlex1[8, 3] = "";
            arrCabeceraFlex1[8, 4] = "c_itedes";

            arrCabeceraFlex1[9, 0] = "Octubre";
            arrCabeceraFlex1[9, 1] = n_ancho.ToString();
            arrCabeceraFlex1[9, 2] = "";
            arrCabeceraFlex1[9, 3] = "";
            arrCabeceraFlex1[9, 4] = "c_itedes";

            arrCabeceraFlex1[10, 0] = "Noviembre";
            arrCabeceraFlex1[10, 1] = n_ancho.ToString();
            arrCabeceraFlex1[10, 2] = "";
            arrCabeceraFlex1[10, 3] = "";
            arrCabeceraFlex1[10, 4] = "c_itedes";

            arrCabeceraFlex1[11, 0] = "Diciembre";
            arrCabeceraFlex1[11, 1] = n_ancho.ToString();
            arrCabeceraFlex1[11, 2] = "";
            arrCabeceraFlex1[11, 3] = "";
            arrCabeceraFlex1[11, 4] = "c_itedes";

            funFlex.FlexMostrarDatos(FgItems, arrCabeceraFlex1, dtLista, 2, false);

            dtRes = funDatos.DataTableFiltrar(dtLista, "n_idite = " + n_IdItem.ToString() + "");
            if (dtRes.Rows.Count != 0)
            {
                TxtCodRec.Text = dtRes.Rows[0]["c_codpro"].ToString();
                TxtDesReceta.Text = dtRes.Rows[0]["c_despro"].ToString();
                TxtDesUniMed.Text = dtRes.Rows[0]["c_despre"].ToString();
            }
            FgItems.Rows.Count = 3;

            n_val = Convert.ToInt32(dtRes.Rows[0]["n_ene"]); n_pintacelda(2, 1, n_val);
            n_val = Convert.ToInt32(dtRes.Rows[0]["n_feb"]); n_pintacelda(2, 2, n_val);
            n_val = Convert.ToInt32(dtRes.Rows[0]["n_mar"]); n_pintacelda(2, 3, n_val);
            n_val = Convert.ToInt32(dtRes.Rows[0]["n_abr"]); n_pintacelda(2, 4, n_val);
            n_val = Convert.ToInt32(dtRes.Rows[0]["n_may"]); n_pintacelda(2, 5, n_val);
            n_val = Convert.ToInt32(dtRes.Rows[0]["n_jun"]); n_pintacelda(2, 6, n_val);
            n_val = Convert.ToInt32(dtRes.Rows[0]["n_jul"]); n_pintacelda(2, 7, n_val);
            n_val = Convert.ToInt32(dtRes.Rows[0]["n_ago"]); n_pintacelda(2, 8, n_val);
            n_val = Convert.ToInt32(dtRes.Rows[0]["n_set"]); n_pintacelda(2, 9, n_val);
            n_val = Convert.ToInt32(dtRes.Rows[0]["n_oct"]); n_pintacelda(2, 10, n_val);
            n_val = Convert.ToInt32(dtRes.Rows[0]["n_nov"]); n_pintacelda(2, 11, n_val);
            n_val = Convert.ToInt32(dtRes.Rows[0]["n_dic"]); n_pintacelda(2, 12, n_val);

            FgItems.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
        }
        void n_pintacelda(int n_fila, int n_columna, int n_valor)
        {
            Color o_color = new Color();

            if (n_valor == 5) { o_color = Color.Green; }
            if (n_valor == 4) { o_color = Color.Yellow; }
            if (n_valor == 6) { o_color = Color.Red; }
            funFlex.Flex_PintarCeldas(FgItems, n_fila, n_columna, n_fila, n_columna, o_color, o_color);
        }
        void CargarCombos()
        {
            DataTableCargar();
        }
        void DataTableCargar()
        {
            o_estacion.mysConec = mysConec;                                    // CARGAMOS LOS DATOS DEL FORMULARIO
            o_estacion.Consulta1();
            dtLista = o_estacion.dtLista;
        }

        private void CmdVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
