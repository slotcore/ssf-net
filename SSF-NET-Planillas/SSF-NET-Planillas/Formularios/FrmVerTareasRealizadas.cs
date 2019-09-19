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
using SIAC_Negocio.Produccion;
using SIAC_Negocio.Almacen;
using SIAC_Negocio.Sunat;
using SIAC_Negocio.Logistica;
using SIAC_Negocio.Ventas;
using SIAC_Entidades.Produccion;
using SIAC_Entidades.Almacen;
using SIAC_Entidades.Logistica;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using Helper;
using SIAC_Objetos.Sistema;
using System.Data.OleDb;

namespace SSF_NET_Planillas.Formularios
{
    public partial class FrmVerTareasRealizadas : Form
    {
        string[,] arrCabeceraFlexLisPro = new string[7, 5];

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();
        Convertir funCon = new Convertir();

        public DataTable dtTareas = new DataTable();
        public string c_apenomtra;
        public string c_numdoc;
        public string c_fchtra;
        public FrmVerTareasRealizadas()
        {
            InitializeComponent();
        }

        private void FrmVerTareasRealizadas_Load(object sender, EventArgs e)
        {
            ConfigurarFormulario();
        }
        void ConfigurarFormulario()
        {
            this.Text = "MODULO DE PLANILLAS - Tareas realizadas por el colaborador";

            // FLEX GRID DE LOS INSUMOS
            arrCabeceraFlexLisPro[0, 0] = "Tarea";
            arrCabeceraFlexLisPro[0, 1] = "250";
            arrCabeceraFlexLisPro[0, 2] = "C";
            arrCabeceraFlexLisPro[0, 3] = "";
            arrCabeceraFlexLisPro[0, 4] = "c_despro";

            arrCabeceraFlexLisPro[1, 0] = "Cantidad";
            arrCabeceraFlexLisPro[1, 1] = "70";
            arrCabeceraFlexLisPro[1, 2] = "N";
            arrCabeceraFlexLisPro[1, 3] = "";
            arrCabeceraFlexLisPro[1, 4] = "c_codrec";

            arrCabeceraFlexLisPro[2, 0] = "Hora Inicio";
            arrCabeceraFlexLisPro[2, 1] = "60";
            arrCabeceraFlexLisPro[2, 2] = "H";
            arrCabeceraFlexLisPro[2, 3] = "";
            arrCabeceraFlexLisPro[2, 4] = "c_unimed";

            arrCabeceraFlexLisPro[3, 0] = "Hora Termimo";
            arrCabeceraFlexLisPro[3, 1] = "60";
            arrCabeceraFlexLisPro[3, 2] = "H";
            arrCabeceraFlexLisPro[3, 3] = "";
            arrCabeceraFlexLisPro[3, 4] = "n_can";

            arrCabeceraFlexLisPro[4, 0] = "Tiempo Labor";
            arrCabeceraFlexLisPro[4, 1] = "60";
            arrCabeceraFlexLisPro[4, 2] = "H";
            arrCabeceraFlexLisPro[4, 3] = "";
            arrCabeceraFlexLisPro[4, 4] = "d_fchent";

            arrCabeceraFlexLisPro[5, 0] = "Precio Kilo";
            arrCabeceraFlexLisPro[5, 1] = "70";
            arrCabeceraFlexLisPro[5, 2] = "N";
            arrCabeceraFlexLisPro[5, 3] = "";
            arrCabeceraFlexLisPro[5, 4] = "n_numarm";

            arrCabeceraFlexLisPro[6, 0] = "Total Pagar";
            arrCabeceraFlexLisPro[6, 1] = "70";
            arrCabeceraFlexLisPro[6, 2] = "N";
            arrCabeceraFlexLisPro[6, 3] = "";
            arrCabeceraFlexLisPro[6, 4] = "n_idpro";

            funFlex.FlexMostrarDatos(FgLisPer, arrCabeceraFlexLisPro, FgLisPer, 2, false);
        }

        private void FrmVerTareasRealizadas_Activated(object sender, EventArgs e)
        {
            MostrarDatos();
        }
        void MostrarDatos()
        {
            int n_row = 0;
            string c_dato = "";
            double n_valor = 0;

            double n_horini = 0;
            double n_horfin = 0;

            LblApeNom.Text = c_apenomtra;
            LblNumDoc.Text = c_numdoc;
            LblFchTra.Text = c_fchtra;

            FgLisPer.Rows.Count = 2;
            if (dtTareas.Rows.Count != 0)
            {
                for (n_row = 0; n_row <= dtTareas.Rows.Count - 1; n_row++)
                {
                    FgLisPer.Rows.Count = FgLisPer.Rows.Count + 1;

                    c_dato = dtTareas.Rows[n_row]["c_des"].ToString();
                    FgLisPer.SetData(FgLisPer.Rows.Count - 1, 1, c_dato);

                    n_valor = Convert.ToDouble(funFunciones.NulosN(dtTareas.Rows[n_row]["n_can"]));
                    FgLisPer.SetData(FgLisPer.Rows.Count - 1, 2, n_valor.ToString("0.00"));

                    c_dato = dtTareas.Rows[n_row]["c_horini"].ToString();
                    FgLisPer.SetData(FgLisPer.Rows.Count - 1, 3, c_dato);

                    c_dato = dtTareas.Rows[n_row]["c_horter"].ToString();
                    FgLisPer.SetData(FgLisPer.Rows.Count - 1, 4, c_dato);

                    n_horini = funCon.HoraEnDecimal(dtTareas.Rows[n_row]["c_horini"].ToString() + ":00");
                    n_horfin = funCon.HoraEnDecimal(dtTareas.Rows[n_row]["c_horter"].ToString() + ":00");
                    n_valor = n_horfin - n_horini;
                    c_dato = funCon.DecimalEnHoras(n_valor);
                    FgLisPer.SetData(FgLisPer.Rows.Count - 1, 5, c_dato);

                    n_valor = Convert.ToDouble(funFunciones.NulosN(dtTareas.Rows[n_row]["n_pretar"]));
                    FgLisPer.SetData(FgLisPer.Rows.Count - 1, 6, n_valor);

                    n_valor = Convert.ToDouble(funFunciones.NulosN(dtTareas.Rows[n_row]["n_imptot"]));
                    FgLisPer.SetData(FgLisPer.Rows.Count - 1, 7, n_valor);
                }
            }


            //LblTot1.Text = funFlex.FlexSumarCol(FgLisPer, 6, 2, FgLisPer.Rows.Count - 1).ToString("0.00");
            LblTot2.Text = funFlex.FlexSumarCol(FgLisPer, 7, 2, FgLisPer.Rows.Count - 1).ToString("0.00");
        }
        private void CmdVolver_Click(object sender, EventArgs e)
        {
            funFlex = null;
            funFunciones = null;
            funDatos = null;
            funDbGrid = null;
            funCon = null;

            dtTareas = null;
            this.Close();
        }

    }
}
