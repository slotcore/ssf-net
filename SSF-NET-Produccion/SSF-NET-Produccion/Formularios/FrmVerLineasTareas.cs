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
using SIAC_Entidades.Produccion;
using SIAC_Entidades.Almacen;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using Helper;
using SIAC_Objetos.Sistema;

namespace SSF_NET_Produccion.Formularios
{
    public partial class FrmVerLineasTareas : Form
    {
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        public DataTable dtItems = new DataTable();
        public DataTable dtTipExi = new DataTable();
        public DataTable dtUniMed = new DataTable();
        public DataTable dtTipPro = new DataTable();
        public DataTable dtTareas = new DataTable();
        public DataTable dtEquipos = new DataTable();

        public BE_PRO_PRODUCTOSRECETASLINEAS entLinea = new BE_PRO_PRODUCTOSRECETASLINEAS();
        public List<BE_PRO_PRODUCTOSRECETASLINEASTAREAS> lstLineaTar = new List<BE_PRO_PRODUCTOSRECETASLINEASTAREAS>();
        
        public bool b_CalcularLinea;
        public double n_NewcanProPri;
        public bool b_Modificar;
        public bool b_Aceptar;                                           // ESTA VARIABLE SE ULTILIZA PARA INDICAR QUE SE PRESIONO EN EL BOTONA ACEPTAR

        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Convertir funCon = new Convertir();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        string[,] arrCabeceraFlexTar = new string[19, 5];
        bool booAgregando;
        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;
        string strCaracteres = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890-()º.,/$' !!·%/()=?¿*^" + (char)8;
        public FrmVerLineasTareas()
        {
            InitializeComponent();
        }
        private void FrmVerLineasTareas_Load(object sender, EventArgs e)
        {
            ConfigurarFormulario();
            CargarCombos();
            MostrarLinea();
        }
        void CargarCombos()
        {
            funDatos.ComboBoxCargarDataTable(CboUniMed, dtUniMed, "n_id", "c_des");
            funDatos.ComboBoxCargarDataTable(CboItemPri, dtItems, "n_id", "c_despro");
        }
        void ConfigurarFormulario()
        {
            //if (b_CalcularLinea == true)
            //{
            //    CmdAcep.Visible = true;
            //    CmdCan.Visible = true;
            //    CmdVolver.Visible = false;

            //    CmdAddIns.Enabled = true;
            //    CmdDelIns.Enabled = true;

            //    TxtTiempo2.Enabled = true;
                
            //}
            //else
            //{
            //    CmdAddIns.Enabled = false;
            //    CmdDelIns.Enabled = false;

            //    CmdAcep.Visible = false;
            //    CmdCan.Visible = false;
            //    CmdVolver.Visible = true;
            //}
            this.Text = "LINEA DE LA RECETA";

            // FLEX GRID DE LOS INSUMOS
            arrCabeceraFlexTar[0, 0] = "Tarea";
            arrCabeceraFlexTar[0, 1] = "200";
            arrCabeceraFlexTar[0, 2] = "C";
            arrCabeceraFlexTar[0, 3] = "";
            arrCabeceraFlexTar[0, 4] = "";

            arrCabeceraFlexTar[1, 0] = "% de Incre mento";
            arrCabeceraFlexTar[1, 1] = "40";
            arrCabeceraFlexTar[1, 2] = "N";
            arrCabeceraFlexTar[1, 3] = "";
            arrCabeceraFlexTar[1, 4] = "";

            arrCabeceraFlexTar[2, 0] = "Cantidad KLG.";
            arrCabeceraFlexTar[2, 1] = "60";
            arrCabeceraFlexTar[2, 2] = "N";
            arrCabeceraFlexTar[2, 3] = "";
            arrCabeceraFlexTar[2, 4] = "";

            arrCabeceraFlexTar[3, 0] = "Distorcion Maquina";
            arrCabeceraFlexTar[3, 1] = "60";
            arrCabeceraFlexTar[3, 2] = "N";
            arrCabeceraFlexTar[3, 3] = "";
            arrCabeceraFlexTar[3, 4] = "";

            arrCabeceraFlexTar[4, 0] = "Nº  Per sonas";
            arrCabeceraFlexTar[4, 1] = "40";
            arrCabeceraFlexTar[4, 2] = "N";
            arrCabeceraFlexTar[4, 3] = "0.00";
            arrCabeceraFlexTar[4, 4] = "";

            arrCabeceraFlexTar[5, 0] = "Equipo";
            arrCabeceraFlexTar[5, 1] = "100";
            arrCabeceraFlexTar[5, 2] = "C";
            arrCabeceraFlexTar[5, 3] = "0";
            arrCabeceraFlexTar[5, 4] = "";

            arrCabeceraFlexTar[6, 0] = "Nº Equi pos";
            arrCabeceraFlexTar[6, 1] = "40";
            arrCabeceraFlexTar[6, 2] = "N";
            arrCabeceraFlexTar[6, 3] = "0";
            arrCabeceraFlexTar[6, 4] = "";

            arrCabeceraFlexTar[7, 0] = "Capacidad KLG x Persona x Hora";
            arrCabeceraFlexTar[7, 1] = "63";
            arrCabeceraFlexTar[7, 2] = "N";
            arrCabeceraFlexTar[7, 3] = "";
            arrCabeceraFlexTar[7, 4] = "";

            arrCabeceraFlexTar[8, 0] = "Capacidad de la Linea x Hora";
            arrCabeceraFlexTar[8, 1] = "63";
            arrCabeceraFlexTar[8, 2] = "N";
            arrCabeceraFlexTar[8, 3] = "0";
            arrCabeceraFlexTar[8, 4] = "";

            arrCabeceraFlexTar[9, 0] = "Capacidad de la Linea x Lapso Tiempo ";
            arrCabeceraFlexTar[9, 1] = "63";
            arrCabeceraFlexTar[9, 2] = "N";
            arrCabeceraFlexTar[9, 3] = "0";
            arrCabeceraFlexTar[9, 4] = "";

            arrCabeceraFlexTar[10, 0] = "Nº Operarios Calculados";
            arrCabeceraFlexTar[10, 1] = "60";
            arrCabeceraFlexTar[10, 2] = "N";
            arrCabeceraFlexTar[10, 3] = "0";
            arrCabeceraFlexTar[10, 4] = "";

            arrCabeceraFlexTar[11, 0] = "Total Producir x Lapso de Tiempo";
            arrCabeceraFlexTar[11, 1] = "63";
            arrCabeceraFlexTar[11, 2] = "N";
            arrCabeceraFlexTar[11, 3] = "0";
            arrCabeceraFlexTar[11, 4] = "";

            arrCabeceraFlexTar[12, 0] = "Eficiencia Unitaria";
            arrCabeceraFlexTar[12, 1] = "60";
            arrCabeceraFlexTar[12, 2] = "N";
            arrCabeceraFlexTar[12, 3] = "0";
            arrCabeceraFlexTar[12, 4] = "";

            arrCabeceraFlexTar[13, 0] = "Eficiencia Total";
            arrCabeceraFlexTar[13, 1] = "60";
            arrCabeceraFlexTar[13, 2] = "N";
            arrCabeceraFlexTar[13, 3] = "0";
            arrCabeceraFlexTar[13, 4] = "";

            arrCabeceraFlexTar[14, 0] = "Kg H x Persona";
            arrCabeceraFlexTar[14, 1] = "60";
            arrCabeceraFlexTar[14, 2] = "D";
            arrCabeceraFlexTar[14, 3] = "0.0000";
            arrCabeceraFlexTar[14, 4] = "";

            arrCabeceraFlexTar[15, 0] = "Costo tarea";
            arrCabeceraFlexTar[15, 1] = "60";
            arrCabeceraFlexTar[15, 2] = "D";
            arrCabeceraFlexTar[15, 3] = "0.0000";
            arrCabeceraFlexTar[15, 4] = "";

            arrCabeceraFlexTar[16, 0] = "Id Equipo";
            arrCabeceraFlexTar[16, 1] = "0";
            arrCabeceraFlexTar[16, 2] = "N";
            arrCabeceraFlexTar[16, 3] = "0";
            arrCabeceraFlexTar[16, 4] = "";

            arrCabeceraFlexTar[17, 0] = "Id Tarea";
            arrCabeceraFlexTar[17, 1] = "0";
            arrCabeceraFlexTar[17, 2] = "N";
            arrCabeceraFlexTar[17, 3] = "0";
            arrCabeceraFlexTar[17, 4] = "";

            arrCabeceraFlexTar[18, 0] = "Nº Ord.";
            arrCabeceraFlexTar[18, 1] = "30";
            arrCabeceraFlexTar[18, 2] = "N";
            arrCabeceraFlexTar[18, 3] = "0";
            arrCabeceraFlexTar[18, 4] = "";

            booAgregando = true;
            funFlex.FlexMostrarDatos(FgTar, arrCabeceraFlexTar, dtItems, 3, false);
            this.Width = 1180;                                                            // ESTABLECEMOS EL ANCHO DE LA VENTANA
            Sizer_Posicionar(Eo1, 1, 1);
            Sizer_Dimensionar(Eo1, this.Height - 40, this.Width - 18);
            LblEstatus.Visible = false;
            LblEstatus.Text = "";

            if (b_Modificar == true)
            {
                CmdAddIns.Enabled = true;
                CmdDelIns.Enabled = true;

                CmdVolver.Visible = false;

                CmdAcep.Visible = true;
                CmdCan.Visible = true;

                CmdCalLin.Enabled = true;

                this.Text = "TAREAS DE LA LINEA - Edicion de Linea";
            }
            else
            {
                this.Text = "TAREAS DE LA LINEA";
            }
        }
        void MostrarLinea()
        {
            TxtCodTar.Text = entLinea.c_codlin;
            TxtDesTar.Text = entLinea.c_deslin;
            CboUniMed.SelectedValue = entLinea.n_idunimed;
            CboItemPri.SelectedValue = entLinea.n_idite;
            TxtCanIttePri.Text = entLinea.n_can.ToString("0.00");
            TxtPorEfi.Text = entLinea.n_efi.ToString("0.00");
            TxtNumOper.Text = entLinea.n_numope.ToString("0.00");
            TxtTiempo.Text = funCon.DecimalEnHoras(entLinea.n_tiepro);
            TxtCanIttePri.Text = entLinea.n_can.ToString("0.00");
            //TxtTiempo2.Text = funCon.DecimalEnHoras(entLinea.n_tiepro);
            TxtPreHorTra.Text = entLinea.n_prehorjor.ToString("0.00");
            TxtObs.Text = entLinea.c_obs;
            //CalcularLinea();
            VerLinea();
        }
        void VerLinea()
        {
            int n_row = 0;
            string c_dato = "";
            int n_fila = 0;

            booAgregando = true;
            FgTar.Rows.Count = 3;
            n_fila = 3;

            if (lstLineaTar.Count == 0)
            {
                MessageBox.Show("¡ La linea no tiene tareas asignadas !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            double n_Valor = 0;

            for (n_row = 0; n_row <= lstLineaTar.Count - 1; n_row++)
            {
                FgTar.Rows.Count = FgTar.Rows.Count + 1;

                c_dato = lstLineaTar[n_row].n_idtar.ToString();
                //17
                FgTar.SetData(n_fila, 18, c_dato);  
                c_dato = funDatos.DataTableBuscar(dtTareas, "n_id", "c_des", c_dato, "N").ToString();
                FgTar.SetData(n_fila, 1, c_dato);                                                        // DESCRIPCION DE LA TAREA

                n_Valor = lstLineaTar[n_row].n_porefi;
                FgTar.SetData(n_fila, 2, n_Valor.ToString("0.00"));                                      // % DE INCREMENTO (campo n_porefi)

                n_Valor = lstLineaTar[n_row].n_cankilpro;
                FgTar.SetData(n_fila, 3, n_Valor.ToString("0.00"));                                      // CANTIDAD DE KILOS

                n_Valor = 0;
                FgTar.SetData(n_fila, 4, n_Valor.ToString("0.00"));                                      // DESVIACION

                n_Valor = lstLineaTar[n_row].n_numpertar;
                FgTar.SetData(n_fila, 5, n_Valor.ToString("0"));                                         // NUMERO DE PERSONAS PARA LA TAREA

                c_dato = lstLineaTar[n_row].n_idequipo.ToString();
                //16
                FgTar.SetData(n_fila, 17, c_dato);                                                        // ID DEL EQUIPO
                if (c_dato == "0")
                {
                    c_dato = "";
                }
                else
                {
                    c_dato = funDatos.DataTableBuscar(dtEquipos, "n_id", "c_des", c_dato, "N").ToString();
                }
                
                FgTar.SetData(n_fila, 6, c_dato);                                                        // DESCRIPCION DEL EQUIPO

                n_Valor = lstLineaTar[n_row].n_canequi;
                FgTar.SetData(n_fila, 7, n_Valor);                                                       // CANTIDAD DE EQUIPOS QUE INTERVIENEN EN LA LINEA

                n_Valor = lstLineaTar[n_row].n_capkilporper;
                //7
                FgTar.SetData(n_fila, 8, n_Valor.ToString("0.00"));                                      // CANTIDAD DE KILOS QUE PRODUCE UNA PERSONA EN LA TAREA

                n_Valor = lstLineaTar[n_row].n_capkilporhorlin;
                //8
                FgTar.SetData(n_fila, 9, n_Valor.ToString("0.00"));                                      // CAPACIDAD KILOS DE LA TAREA EN LA LINEA  (SE MULTIUPLICA n_numpertar * n_capkilporper)

                n_Valor = lstLineaTar[n_row].n_capkilporlintietra;
                //9
                FgTar.SetData(n_fila, 10, n_Valor.ToString("0.00"));                                      // CAPACIDAD DE PRODUCCION DE LA LINEA POR EL TIEMPO DE TRABAJO

                n_Valor = lstLineaTar[n_row].n_numpercal;
                //10
                FgTar.SetData(n_fila, 11, n_Valor.ToString("0.00"));                                      // NUMERO DE PERSONAS CALCULADAS PARA LA TAREA

                n_Valor = lstLineaTar[n_row].n_totprotietra;
                //11
                FgTar.SetData(n_fila, 12, n_Valor);                                                       // TOTAL A PRODUCIR TOTAL PERSONAS POR EL TIEMPO DETRABAJO
                
                n_Valor = lstLineaTar[n_row].n_porefiuni;
                //12
                FgTar.SetData(n_fila, 13, n_Valor.ToString("0.00"));                                      // PORCENTAJE DE EFICIENCIA UNITARIA

                n_Valor = lstLineaTar[n_row].n_porefitot;
                //13
                FgTar.SetData(n_fila, 14, n_Valor.ToString("0.00"));                                      // PORCENTAJE DE EFICIENCIA TOTAL

                n_Valor = lstLineaTar[n_row].n_kghper;
                //14
                FgTar.SetData(n_fila, 15, n_Valor.ToString("0.000000"));                                      

                n_Valor = lstLineaTar[n_row].n_costar;
                //15
                FgTar.SetData(n_fila, 16, n_Valor.ToString("0.000000"));                                  // COSTO DE LA TAREA

                n_Valor = lstLineaTar[n_row].n_ord;
                //19
                FgTar.SetData(n_fila, 19, n_Valor.ToString("0"));                                         // ORDEN DE LA TAREA

                n_fila = n_fila + 1;
            }
            SumarColumnas();
            booAgregando = false;
        }
        void SumarColumnas()
        {
            //10
            TxtDato1.Text = funFlex.FlexSumarCol(FgTar, 11, 3, FgTar.Rows.Count - 1).ToString("0.00");                    // TOTAL DE PERSONAS CALCULADAS
            
            double n_dato = 0;
            //13
            n_dato = Convert.ToDouble(funFlex.FlexSumarCol(FgTar, 14, 3, FgTar.Rows.Count - 1).ToString());               // EFICIENCICIA TOTAL
            TxtDato3.Text = (n_dato / Convert.ToDouble(TxtDato1.Text)).ToString("0.00");

            n_dato = Convert.ToDouble(TxtCanIttePri.Text);
            n_dato = n_dato / Convert.ToDouble(TxtDato1.Text);
            TxtDato4.Text = n_dato.ToString("0.00");

            //15
            TxtDato5.Text = funFlex.FlexSumarCol(FgTar, 16, 3, FgTar.Rows.Count - 1).ToString("0.000000");

            TxtPorEfi.Text = TxtDato3.Text;
            TxtNumOper.Text = TxtDato1.Text;
        }
        private void FrmVerLineasTareas_Resize(object sender, EventArgs e)
        {
            Sizer_Dimensionar(Eo1, this.Height - 40, this.Width - 18);
        }
        void Sizer_Dimensionar(C1.Win.C1Sizer.C1Sizer Eo, int intAlto, int intAncho)
        {
            Eo.Height = intAlto;
            Eo.Width = intAncho;
        }
        void Sizer_Posicionar(C1.Win.C1Sizer.C1Sizer Eo, int intPosX, int intPosY)
        {
            Eo.Left = intPosX;
            Eo.Top = intPosY;
        }
        private void CmdVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void CmdCalLin_Click(object sender, EventArgs e)
        {
            int n_fila = 0;
            bool b_Salir = false;
            if (Convert.ToDouble(funFunciones.NulosN(TxtCanIttePri.Text)) == 0)
            {
                MessageBox.Show("¡ No ha especificado la cantidad a procesar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtCanIttePri.Focus();
                return;
            }

            if (Convert.ToDouble(funFunciones.NulosN(TxtPreHorTra.Text)) == 0)
            {
                MessageBox.Show("¡ No ha especificado le precio del producto para la linea !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtPreHorTra.Focus();
                return;
            }

            for (n_fila = 3; n_fila <= FgTar.Rows.Count - 1; n_fila++)
            {
                if (funFunciones.NulosC(FgTar.GetData(n_fila, 1)) == "")
                {
                    MessageBox.Show("¡ No ha especificado el nombre de la tarea en la fila Nº " + n_fila.ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    FgTar.Focus();
                    b_Salir = true;
                    break;
                }
                if (funFunciones.NulosC(FgTar.GetData(n_fila, 2)) == "")
                {
                    MessageBox.Show("¡ No ha especificado el porcentaje de incremento en la fila Nº " + n_fila.ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    FgTar.Focus();
                    b_Salir = true;
                    break;
                }
                //if (funFunciones.NulosC(FgTar.GetData(n_fila, 3)) == "")
                //{
                //    MessageBox.Show("¡ No ha especificado la cantidad de kilos a procesar en la fila Nº " + n_fila.ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //    FgTar.Focus();
                //    b_Salir = true;
                //    break;
                //}
                if (funFunciones.NulosC(FgTar.GetData(n_fila, 5)) == "")
                {
                    MessageBox.Show("¡ No ha especificado el numero de personas en la fila Nº " + n_fila.ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    FgTar.Focus();
                    b_Salir = true;
                    break;
                }
                if (funFunciones.NulosC(FgTar.GetData(n_fila, 6)) != "")
                {
                    int n_valor = Convert.ToInt32(funFunciones.NulosN(FgTar.GetData(n_fila, 7)));
                    if (n_valor == 0)
                    { 
                        MessageBox.Show("¡ No ha especificado la cantidad de equipos a utilizar en la fila Nº " + n_fila.ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        FgTar.Focus();
                        return;
                    }
                }
                //7
                if (funFunciones.NulosC(FgTar.GetData(n_fila, 8)) == "")
                {
                    MessageBox.Show("¡ No ha especificado la capacidad en kilogramos por hora de la persona en la fila Nº " + n_fila.ToString() + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    FgTar.Focus();
                    b_Salir = true;
                    break;
                }
            }
            if (b_Salir == true)
            {
                return;
            }
            CalcularLinea();
        }
        void CalcularLinea()
        {
            int n_row = 0;
            //string c_dato = "";
            int n_fila = 0;
            double n_por;
            double n_numper;
     

            n_fila = 3;
            LblEstatus.Text = "";
            LblEstatus.Visible = false;
            if (lstLineaTar.Count == 0)
            {
                MessageBox.Show("¡ La linea no tiene tareas asignadas !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                this.Close();
                return;
            }

            double n_Valor = 0;
            double n_Valor2 = 0;
            double n_tiempo = funCon.HoraEnDecimal(TxtTiempo.Text);

            for (n_row = 3; n_row <= FgTar.Rows.Count - 1; n_row++)
            {

                n_Valor = Convert.ToDouble(FgTar.GetData(n_fila, 2).ToString());
                //FgTar.SetData(n_fila, 2, n_Valor.ToString("0.00"));                                    // % DE INCREMENTO (campo n_porefi)

                n_Valor = Convert.ToDouble(TxtCanIttePri.Text);
                n_por = ((Convert.ToDouble(FgTar.GetData(n_fila, 2).ToString()) / 100) + 1);
                n_Valor = n_Valor * n_por;
                FgTar.SetData(n_fila, 3, n_Valor.ToString("0.00"));                                      // CANTIDAD DE KILOS A PROCESAR

                //16
                n_Valor = Convert.ToInt32(funFunciones.NulosN(FgTar.GetData(n_fila, 17)));
                if (n_Valor != 0)                                                                        // SI SE HA SELECCIONADO UNA MAQUINA (COLUMNA ID MAQUINA != 0)
                {
                    //9
                    n_Valor = Convert.ToDouble(funFunciones.NulosN(FgTar.GetData(n_fila, 10)));
                    n_Valor2 = Convert.ToDouble(FgTar.GetData(n_fila, 3).ToString());
                    n_Valor = (n_Valor - n_Valor2);

                    FgTar.SetData(n_fila, 4, n_Valor.ToString("0.00"));
                    if (n_Valor < 0)
                    {
                        LblEstatus.Visible = true;
                        LblEstatus.Text = "LINEA NO OPTIMA";
                        LblEstatus.ForeColor = Color.Red;
                    }
                    if (n_Valor == 0)
                    {
                        LblEstatus.Visible = true;
                        LblEstatus.Text = "LINEA OPTIMA";
                        LblEstatus.ForeColor = Color.Blue;
                    }
                    if (n_Valor > 0)
                    {
                        LblEstatus.Visible = true;
                        LblEstatus.Text = "LINEA ACEPTABLE";
                        LblEstatus.ForeColor = Color.Green;
                    }
                }
                else
                {
                    FgTar.SetData(n_fila, 4, "");
                }
                if (funFunciones.NulosC(FgTar.GetData(n_fila, 6)) == "")                                          // PREGUNTAMOS SI LA MAQUINA ESTA VACIA
                {
                    n_Valor = Convert.ToDouble(FgTar.GetData(n_fila, 5).ToString());                    // SI LA CELDA DE EQUIPO ESTA EN BLANCO ESCOJEMOS EL NUMERO DE PERSONAÑ
                }
                else
                {
                    n_Valor = Convert.ToDouble(FgTar.GetData(n_fila, 7).ToString());                    // SI LA CELDA DE EQUIPO SE HA SELEECIONADO UN EQUIPO ESCOJEMOS EL NUMERO DE EQUIPOS
                }
                //7
                n_Valor2 = Convert.ToDouble(FgTar.GetData(n_fila, 8).ToString());
                n_Valor = n_Valor * n_Valor2;
                //8
                FgTar.SetData(n_fila, 9, n_Valor.ToString("0.00"));                                      // CAPACIDAD DE PRODUCCION DE LA LINEA POR HORA
                //8
                n_Valor = Convert.ToDouble(FgTar.GetData(n_fila, 9).ToString());
                n_Valor = n_Valor * n_tiempo;
                //9
                FgTar.SetData(n_fila, 10, n_Valor.ToString("0.00"));                                                       // TOTAL A PRODUCIR =  TOTAL PERSONAS POR EL TIEMPO DETRABAJO
                //16
                n_Valor = Convert.ToInt32(funFunciones.NulosN(FgTar.GetData(n_fila, 17)));
                if (n_Valor == 0)                                                                        // SI SE HA SELECCIONADO UNA MAQUINA (COLUMNA ID MAQUINA != 0)
                {
                    // CALCULAMOS EL NUMERO DE OPERARIOS
                    n_Valor = Convert.ToDouble(FgTar.GetData(n_row, 3).ToString());                      // CANTIDAD DE KILOS A PROCESAR
                    //9
                    n_Valor2 = Convert.ToDouble(FgTar.GetData(n_row, 10).ToString());
                    n_numper = n_Valor / n_Valor2;                                                       // NUMERO DE OPERARIOS CALCULADO 
                    n_Valor2 = Convert.ToInt32(n_numper);                                                // NUMERO DE OPERARIOS CALCULADOS SIN EL DECIMAL   
                    n_Valor = (n_numper - n_Valor2);                                                          // RESTAMOS LOS 2 VALORES POARA OBTENER EL DECIMAL

                    if (n_Valor <= 0.25)                                                                     //    
                    {
                        n_numper = n_Valor2;
                    }
                    else
                    {
                        if (n_Valor <= 0.99)
                        {
                            n_numper = 1;
                        }
                        else 
                        { 
                            n_numper = Math.Round(n_numper, 0);
                        }
                        
                    }
                    //10
                    FgTar.SetData(n_fila, 11, n_numper.ToString(""));                                      // MOSTRAMOS EL NUMERO DE OPERARIOS REQUERIDOS
                }
                else
                {
                    // MOSTRAMOS ELNUMERO DE OPERARIOS ASIGNADO, NO SE PUEDE CALCULAR OPERARIOS CUANDO HAY UNA MAQUINA SELECCIONADA
                    n_Valor = Convert.ToInt32(FgTar.GetData(n_fila, 5).ToString());
                    //10
                    FgTar.SetData(n_fila, 11, n_Valor.ToString("00"));
                }
                //16
                n_Valor = Convert.ToInt32(funFunciones.NulosN(FgTar.GetData(n_fila, 17)));
                if (n_Valor == 0)                                                                        // SI NO HA SELECCIONADO UNA MAQUINA (COLUMNA ID MAQUINA = 0)
                {
                    //9
                    n_Valor = Convert.ToDouble(FgTar.GetData(n_fila, 10).ToString());
                    //10
                    n_Valor2 = Convert.ToDouble(FgTar.GetData(n_fila, 11).ToString());
                    n_Valor = n_Valor * n_Valor2;
                    //11
                    FgTar.SetData(n_fila, 12, n_Valor.ToString("0.00"));                                      // MOSTRAMOS EL TOTAL A PRODUCIR POR TODA LA JORNADA Y LOS TRABAJADORES CALCULADOS
                }
                else
                {
                    //9
                    n_Valor = Convert.ToDouble(FgTar.GetData(n_fila, 10).ToString());
                    //11
                    FgTar.SetData(n_fila, 12, n_Valor.ToString("0.00"));                                      // MOSTRAMOS EL TOTAL A PRODUCIR POR TODA LA JORNADA Y LOS TRABAJADORES CALCULADOS
                }

                n_Valor = Convert.ToDouble(FgTar.GetData(n_fila, 3).ToString());
                //11
                n_Valor2 = Convert.ToDouble(FgTar.GetData(n_fila, 12).ToString());
                n_Valor = n_Valor / n_Valor2;
                //12
                FgTar.SetData(n_fila, 13, n_Valor.ToString("0.00"));                                      // PORCENTAJE DE EFICIENCIA UNITARIA
                //10
                n_Valor = Convert.ToDouble(FgTar.GetData(n_fila, 11).ToString());
                //12
                n_Valor2 = Convert.ToDouble(funFunciones.NulosN(FgTar.GetData(n_fila, 13)));
                n_Valor = n_Valor * n_Valor2;
                //13
                FgTar.SetData(n_fila, 14, n_Valor.ToString("0.00"));                                      // PORCENTAJE DE EFICIENCIA TOTAL


                n_Valor = 0;
                //14
                FgTar.SetData(n_fila, 15, n_Valor.ToString("0.00"));

                n_Valor = Convert.ToDouble(TxtPreHorTra.Text);
                //8
                n_Valor2 = Convert.ToDouble(FgTar.GetData(n_fila, 9).ToString());
                n_Valor = n_Valor / n_Valor2;
                //15
                FgTar.SetData(n_fila, 16, n_Valor.ToString("0.000000"));                                  // COSTO DE LA TAREA

                n_fila = n_fila + 1;
            }
            SumarColumnas();
        }
        private void FrmVerLineasTareas_Activated(object sender, EventArgs e)
        {
            //if (b_Modificar == true)
            //{
            //    CmdCalLin.Enabled = true;
            //    TxtCanIttePri.Text = n_NewcanProPri.ToString("0.00");
            //    CmdCalLin_Click(sender, e);
            //}
            //else
            //{
            //    CmdCalLin.Enabled = false;
            //}
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void CmdAcep_Click(object sender, EventArgs e)
        {
            if (FgTar.Rows.Count == 0)
            {
                MessageBox.Show("¡ No hay tareas para guardar, ingrese al menos 1 tarea !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            int n_fila=0;
            int n_row = 0;
            int n_idtar = 0;
            bool n_encontrado = false;

            // ACTUALIZAMOS LISTA  CABECERA DE LA LINEA
            entLinea.n_idite = entLinea.n_idite;
            entLinea.n_can = Convert.ToDouble(TxtCanIttePri.Text);
            entLinea.n_numope = Convert.ToDouble(TxtNumOper.Text);
            entLinea.n_efi = Convert.ToDouble(TxtPorEfi.Text);
            entLinea.n_tiepro = funCon.HoraEnDecimal(TxtTiempo.Text);
            entLinea.n_prehorjor = Convert.ToDouble(TxtPreHorTra.Text);

            lstLineaTar.Clear();
            
            // ACTUALIZAMOS EL DETALLE DE LA LINEA
            for (n_fila = 3; n_fila <= FgTar.Rows.Count - 1; n_fila++)
            {
                n_encontrado = false;
                //17
                n_idtar = Convert.ToInt32(FgTar.GetData(n_fila, 18).ToString());

                for (n_row = 0; n_row <= lstLineaTar.Count - 1; n_row++)
                { 
                    if (lstLineaTar[n_row].n_idtar == n_idtar)
                    { 
                        //lstLineaTar[n_row].n_idpro = 0;
                        //lstLineaTar[n_row].n_idrec = 0;
                        //lstLineaTar[n_row].n_idlin = 0;
                        //17
                        lstLineaTar[n_row].n_idtar = Convert.ToInt32(FgTar.GetData(n_fila, 18).ToString());
                        lstLineaTar[n_row].n_porefi = Convert.ToDouble(FgTar.GetData(n_fila, 2).ToString());
                        lstLineaTar[n_row].n_cankilpro = Convert.ToDouble(FgTar.GetData(n_fila, 3).ToString());
                        lstLineaTar[n_row].n_numpertar = Convert.ToInt32(FgTar.GetData(n_fila, 5).ToString());
                        //16
                        if (funFunciones.NulosC(FgTar.GetData(n_fila, 17)) == "")
                        {
                            lstLineaTar[n_row].n_idequipo = 0;
                            lstLineaTar[n_row].n_canequi = 0;
                        }
                        else
                        {
                            //16
                            lstLineaTar[n_row].n_idequipo = Convert.ToInt32(FgTar.GetData(n_fila, 17).ToString());
                            lstLineaTar[n_row].n_canequi = Convert.ToInt32(FgTar.GetData(n_fila, 7).ToString()); ;
                        }
                        
                        lstLineaTar[n_row].n_numpertarequ = Convert.ToInt32(FgTar.GetData(n_fila, 5).ToString());
                        //7
                        lstLineaTar[n_row].n_capkilporper = Convert.ToDouble(FgTar.GetData(n_fila, 8).ToString());
                        //8
                        lstLineaTar[n_row].n_capkilporhorlin = Convert.ToDouble(FgTar.GetData(n_fila, 9).ToString());
                        //9
                        lstLineaTar[n_row].n_capkilporlintietra = Convert.ToDouble(FgTar.GetData(n_fila, 10).ToString());
                        //10
                        double n_valor = Convert.ToDouble(FgTar.GetData(n_fila, 11));
                        lstLineaTar[n_row].n_numpercal =Convert.ToInt32(n_valor);
                        //11
                        lstLineaTar[n_row].n_totprotietra = Convert.ToDouble(FgTar.GetData(n_fila, 12).ToString());
                        //12
                        lstLineaTar[n_row].n_porefiuni = Convert.ToDouble(funFunciones.NulosN(FgTar.GetData(n_fila, 13)));
                        //13
                        lstLineaTar[n_row].n_porefitot = Convert.ToDouble(FgTar.GetData(n_fila, 14).ToString());
                        //15
                        lstLineaTar[n_row].n_kghper = Convert.ToDouble(FgTar.GetData(n_fila, 15).ToString());
                        //16
                        lstLineaTar[n_row].n_costar = Convert.ToDouble(FgTar.GetData(n_fila, 16).ToString());

                        lstLineaTar[n_row].n_ord = Convert.ToInt32(FgTar.GetData(n_fila, 19).ToString());
                        n_encontrado = true;
                        break;
                    }
                }
                if (n_encontrado == false) 
                { 
                    BE_PRO_PRODUCTOSRECETASLINEASTAREAS entTar = new BE_PRO_PRODUCTOSRECETASLINEASTAREAS();
                    entTar.n_idpro = entLinea.n_idpro;
                    entTar.n_idrec = entLinea.n_idrec;
                    entTar.n_idlin = entLinea.n_id;
                    //17
                    entTar.n_idtar = Convert.ToInt32(FgTar.GetData(n_fila, 18).ToString());
                    if (string.IsNullOrEmpty(funFunciones.NulosC(FgTar.GetData(n_fila, 2))))
                    {
                        entTar.n_porefi = 0;
                    }
                    else
                    {
                        entTar.n_porefi = Convert.ToDouble(FgTar.GetData(n_fila, 2));
                    }

                    if (string.IsNullOrEmpty(funFunciones.NulosC(FgTar.GetData(n_fila, 3))))
                    {
                        entTar.n_cankilpro = 0;
                    }
                    else
                    {
                        entTar.n_cankilpro = Convert.ToDouble(FgTar.GetData(n_fila, 3));
                    }

                    if (string.IsNullOrEmpty(funFunciones.NulosC(FgTar.GetData(n_fila, 5))))
                    {
                        entTar.n_numpertar = 0;
                    }
                    else
                    {
                        entTar.n_numpertar = Convert.ToInt32(FgTar.GetData(n_fila, 5));
                    }

                    //16
                    if (funFunciones.NulosC(FgTar.GetData(n_fila, 17)) == "")
                    {
                        entTar.n_idequipo = 0;
                    }
                    else 
                    { 
                        //16
                        entTar.n_idequipo = Convert.ToInt32(FgTar.GetData(n_fila, 17).ToString());
                    }
                    if (string.IsNullOrEmpty(funFunciones.NulosC(FgTar.GetData(n_fila, 5))))
                    {
                        MessageBox.Show("¡ Debe de ingresar el numero de personas por tarea !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    entTar.n_numpertarequ = Convert.ToInt32(FgTar.GetData(n_fila, 5).ToString());
                    //7
                    if (string.IsNullOrEmpty(funFunciones.NulosC(FgTar.GetData(n_fila, 8))))
                    {
                        MessageBox.Show("¡ Debe de ingresar la capacidad kg persona x hora !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    entTar.n_capkilporper = Convert.ToDouble(FgTar.GetData(n_fila, 8).ToString());
                    //8
                    if (string.IsNullOrEmpty(funFunciones.NulosC(FgTar.GetData(n_fila, 9))))
                    {
                        MessageBox.Show("¡ Debe de ingresar la capacidad de la linea por hora !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    entTar.n_capkilporhorlin = Convert.ToDouble(FgTar.GetData(n_fila, 9).ToString());
                    //9
                    if (string.IsNullOrEmpty(funFunciones.NulosC(FgTar.GetData(n_fila, 10))))
                    {
                        MessageBox.Show("¡ Debe de ingresar la capacidad de la linea por lapso de tiempo !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    entTar.n_capkilporlintietra = Convert.ToDouble(FgTar.GetData(n_fila, 10).ToString());

                    int n_valor = 1;// Convert.ToInt32(FgTar.GetData(n_fila, 10));
                    entTar.n_numpercal = n_valor;
                    //11
                    if (string.IsNullOrEmpty(funFunciones.NulosC(FgTar.GetData(n_fila, 12))))
                    {
                        MessageBox.Show("¡ Debe de ingresar la cantidad total a producir por lapso de tiempo !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    entTar.n_totprotietra = Convert.ToDouble(FgTar.GetData(n_fila, 12).ToString());
                    //12
                    entTar.n_porefiuni = Convert.ToDouble(funFunciones.NulosN(FgTar.GetData(n_fila, 13)));
                    //13
                    if (string.IsNullOrEmpty(funFunciones.NulosC(FgTar.GetData(n_fila, 14))))
                    {
                        MessageBox.Show("¡ Debe de ingresar la eficiencia total !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    entTar.n_porefitot = Convert.ToDouble(FgTar.GetData(n_fila, 14).ToString());
                    //15
                    if (string.IsNullOrEmpty(funFunciones.NulosC(FgTar.GetData(n_fila, 15))))
                    {
                        MessageBox.Show("¡ Debe de ingresar KgHxPersona !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    entTar.n_kghper = Convert.ToDouble(FgTar.GetData(n_fila, 15).ToString());
                    //16
                    if (string.IsNullOrEmpty(funFunciones.NulosC(FgTar.GetData(n_fila, 16))))
                    {
                        MessageBox.Show("¡ Debe de ingresar costo de la tarea !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    entTar.n_costar = Convert.ToDouble(FgTar.GetData(n_fila, 16).ToString());

                    entTar.n_ord = Convert.ToInt32(funFunciones.NulosN(FgTar.GetData(n_fila, 19)));
                    lstLineaTar.Add(entTar);
                }
            }
            b_Aceptar = true;
            Cerrar();
        }
        void Cerrar()
        {
            funFlex = null;
            funFunciones = null;
            funCon = null;
            funDbGrid = null;
            funDatos = null;
            
            dtItems = null;
            dtTipExi = null;
            dtUniMed = null;
            dtTipPro = null;
            dtTareas = null;
            dtEquipos = null;

            this.Hide();
        }
        private void CmdCan_Click(object sender, EventArgs e)
        {
            b_Aceptar = false;
            Cerrar();
        }
        private void CmdAddIns_Click(object sender, EventArgs e)
        {
            if (FgTar.Rows.Count >= 2)
            {
                if (funFunciones.NulosC( FgTar.GetData(FgTar.Rows.Count - 1, 1)) == "")
                {
                    MessageBox.Show("¡ No ha especificado el nombre de la tarea anterior !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }

                if (funFunciones.NulosC( FgTar.GetData(FgTar.Rows.Count - 1, 2)) == "")
                {
                    MessageBox.Show("¡ No ha especificado el porcentaje de incremento de la tarea anterior !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }
                if (funFunciones.NulosC(FgTar.GetData(FgTar.Rows.Count - 1, 5)) == "")
                {
                    MessageBox.Show("¡ No ha especificado el numero de personas para la tarea anterior!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }
                //7
                if (funFunciones.NulosC(FgTar.GetData(FgTar.Rows.Count - 1, 8)) == "")
                {
                    MessageBox.Show("¡ No ha especificado la cantidad de kilos por hora de la tarea anterior!", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    return;
                }
            }

            booAgregando = true;
            int n_valor = 0;
            double n_can = 0;
            FgTar.Rows.Count = FgTar.Rows.Count + 1;
            FgTar.Select(1, 1);

            BE_PRO_PRODUCTOSRECETASLINEASTAREAS entTarea = new BE_PRO_PRODUCTOSRECETASLINEASTAREAS();

            entTarea.n_idpro = entLinea.n_idpro;
            //entTarea.n_idordpro= entLinea.n_idpro;
            //entTarea.n= entLinea.n_idite;
            entTarea.n_idrec = entLinea.n_idrec;
            entTarea.n_idlin = entLinea.n_id;
            //17
            n_valor = Convert.ToInt32(funFunciones.NulosN(FgTar.GetData(FgTar.Rows.Count-1, 18)));  // ID DE LA TAREA
            entTarea.n_idtar = n_valor;

            n_valor = Convert.ToInt32(funFunciones.NulosN(FgTar.GetData(FgTar.Rows.Count - 1, 2)));        // PORCENTAJE DE EFICIENCIA
            entTarea.n_porefi = n_valor;

            n_valor = Convert.ToInt32(funFunciones.NulosN(FgTar.GetData(FgTar.Rows.Count - 1, 3)));        // CANTIDAD DE KILOS A PRODUCIR 
            entTarea.n_cankilpro = n_valor;

            n_valor = Convert.ToInt32(funFunciones.NulosN(FgTar.GetData(FgTar.Rows.Count - 1, 5)));         // NUMERO DE PERSONAS PARA LA TAREAS
            entTarea.n_numpertar= n_valor;
            //16
            n_valor = Convert.ToInt32(funFunciones.NulosN(FgTar.GetData(FgTar.Rows.Count - 1, 17)));        // ID DEL EQUIPO
            entTarea.n_idequipo= n_valor;
            
            //n_valor = Convert.ToInt32(FgTar.GetData(FgTar.Row, 5).ToString());
            //entTarea.n_numpertarequ= n_valor;
            //7
            n_can = Convert.ToInt32(funFunciones.NulosN(FgTar.GetData(FgTar.Rows.Count - 1, 8)));            // CANTIDAD DE KILOS POR PERSONA
            entTarea.n_capkilporper = n_can;
            //8
            n_can = Convert.ToInt32(funFunciones.NulosN(FgTar.GetData(FgTar.Rows.Count - 1, 9)));            // CAPACIDAD DE KILOS DE LA LINEA POR HORA
            entTarea.n_capkilporhorlin= n_valor;

            //9
            n_can = Convert.ToInt32(funFunciones.NulosN(FgTar.GetData(FgTar.Rows.Count - 1, 10)));            // CANT DE KILOS POR LINEA DE TIEMPO
            entTarea.n_capkilporlintietra= n_valor;
            //10
            n_valor = Convert.ToInt32(funFunciones.NulosN(FgTar.GetData(FgTar.Rows.Count - 1, 11)));         // NUMERO DE PERSONAS CLACULADAS
            entTarea.n_numpercal = n_valor;
            //11
            n_valor = Convert.ToInt32(funFunciones.NulosN(FgTar.GetData(FgTar.Rows.Count - 1, 12)));         // TOTAL A PRODUCIR EN EL LAPSO DE TIEMPO
            entTarea.n_totprotietra= n_valor;
            //12
            n_valor = Convert.ToInt32(funFunciones.NulosN(FgTar.GetData(FgTar.Rows.Count - 1, 13)));         // PORCENTAJE EFICIENCIA UNITARIA
            entTarea.n_porefiuni= n_valor;
            //13
            n_valor = Convert.ToInt32(funFunciones.NulosN(FgTar.GetData(FgTar.Rows.Count - 1, 14)));         // PORCENTAJE DE EFICIENCIA TOTAL
            entTarea.n_porefitot= n_valor;
            //15
            n_valor = Convert.ToInt32(funFunciones.NulosN(FgTar.GetData(FgTar.Rows.Count - 1, 16)));         // COSTO POR TAREA
            entTarea.n_costar= n_valor;

            lstLineaTar.Add(entTarea);
            booAgregando = false;
        }
        private void CmdDelIns_Click(object sender, EventArgs e)
        {
            if (FgTar.Rows.Count == 2)
            {
                MessageBox.Show("¡ La linea no tiene tareas para eliminar !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            
            int n_valor = Convert.ToInt32(funFunciones.NulosN(FgTar.GetData(FgTar.Row, 18)));
            EliminarTarea(n_valor);
            FgTar.RemoveItem(FgTar.Row);
        }
        void EliminarTarea(int n_idTarea)
        { 
            int n_row = 0;

            for (n_row = 0; n_row <= lstLineaTar.Count - 1; n_row++)
            {
                if (lstLineaTar[n_row].n_idtar == n_idTarea)
                {
                    lstLineaTar.RemoveAt(n_row);
                    break;
                }
            }
            //n_valor = Convert.ToInt32(funFunciones.NulosN(FgTar.GetData(FgTar.Rows.Count-1, 18)));  // ID DE LA TAREA
            //lstLineaTar
        }
        private void FgTar_CellChanged(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (b_Modificar == false)
            {
                FgTar.AllowEditing = false; return;
            }

            if (booAgregando == true) { return; }

            double n_valor = 0;
            int n_can = 0;
            int n_index = 0;
            int n_idlin = 0;
            int n_ord = 0;

            if (lstLineaTar.Count == 1)
            {
                //if (Convert.ToInt32(funFunciones.NulosN(FgTar.GetData(FgTar.Row, 17))) != 0) 
                //{ 
                //    n_index= Convert.ToInt32(funFunciones.NulosN(FgTar.GetData(FgTar.Row, 17)));
                //}
                //else
                //{
                    n_index=0;
                //}
                
            }
            else
            {
                int n_fila = 0;
                //17
                n_idlin = Convert.ToInt32(funFunciones.NulosN(FgTar.GetData(FgTar.Row, 18)));
                for (n_fila = 0; n_fila <= lstLineaTar.Count - 1; n_fila++)
                {
                    if (Convert.ToInt32(funFunciones.NulosN(lstLineaTar[n_fila].n_idtar)) == n_idlin)
                    {
                        n_index = n_fila;
                        break;
                    }
                }
                    //n_index = Convert.ToInt32(FgTar.GetData(FgTar.Row, 17));
            }
            
            if (FgTar.Col == 1)                     // TAREA
            {
                string c_dato = FgTar.GetData(FgTar.Row, 1).ToString();
                c_dato = funDatos.DataTableBuscar(dtTareas, "c_des", "n_id", c_dato, "C").ToString();

                lstLineaTar[n_index].n_idtar = Convert.ToInt32(c_dato);                                                             // ID DE LA TAREA
                //17
                FgTar.SetData(FgTar.Row, 18, c_dato);
            }

            if (FgTar.Col == 2)                     
            {
                if (funFunciones.EsNumerico(FgTar.GetData(FgTar.Row, 2).ToString()) == false)
                {
                    MessageBox.Show("¡ El valor ingresado no es un valor numerico !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booAgregando = true;
                    FgTar.SetData(FgTar.Row, 2, "");
                    booAgregando = false;
                    return;
                }
                n_valor = Convert.ToDouble( FgTar.GetData(FgTar.Row, 2).ToString());
                lstLineaTar[n_index].n_porefi = n_valor;                                                                            // PORCENTAJE DE EFICIENCIA
                FgTar.SetData(FgTar.Row, 2, n_valor.ToString("0.00"));
            }

            if (FgTar.Col == 3)                     
            {
                n_valor = Convert.ToDouble(FgTar.GetData(FgTar.Row, 3).ToString());
                lstLineaTar[n_index].n_cankilpro = n_valor;                                                                            // CANTIDAD DE KILOS A PRODUCIR 
                FgTar.SetData(FgTar.Row, 3, n_valor.ToString("0.00"));
            }

            if (FgTar.Col == 5)
            {
                if (funFunciones.EsEntero(FgTar.GetData(FgTar.Row, 5).ToString()) == false)
                {
                    MessageBox.Show("¡ El valor ingresado no es un valor numerico !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booAgregando = true;
                    FgTar.SetData(FgTar.Row, 5, "");
                    booAgregando = false;
                    return;
                }
                n_can = Convert.ToInt32(FgTar.GetData(FgTar.Row, 5).ToString());
                lstLineaTar[n_index].n_numpertar = n_can;                                                                                     // NUMERO DE PERSONAS PARA LA TAREAS
                FgTar.SetData(FgTar.Row, 5, n_can.ToString());
            }
                                   
            if (FgTar.Col == 6)                     // EQUIPO
            {
                
                string c_dato = funFunciones.NulosC(FgTar.GetData(FgTar.Row, 6));
                if (c_dato != "")
                {
                    c_dato = funDatos.DataTableBuscar(dtEquipos, "c_des", "n_id", c_dato, "C").ToString();
                    lstLineaTar[n_index].n_idequipo = Convert.ToInt32(c_dato);  
                    //16
                    FgTar.SetData(FgTar.Row, 17, c_dato);
                }
            }
            //7
            if (FgTar.Col == 8)
            {
                //7
                if (funFunciones.EsNumerico(FgTar.GetData(FgTar.Row, 8).ToString()) == false)
                {
                    MessageBox.Show("¡ El valor ingresado no es un valor numerico !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    booAgregando = true;
                    //7
                    FgTar.SetData(FgTar.Row, 8, "");
                    booAgregando = false;
                    return;
                }
                //7
                n_valor = Convert.ToDouble(FgTar.GetData(FgTar.Row, 8).ToString());
                lstLineaTar[n_index].n_capkilporper = n_valor;                                                                        // CANTIDAD DE KILOS POR PERSONA               
                //7
                FgTar.SetData(FgTar.Row, 8, n_valor.ToString("0.00"));
            }
            //8
            if (FgTar.Col == 9)
            {
                //8
                n_valor = Convert.ToDouble(funFunciones.NulosN( FgTar.GetData(FgTar.Row, 9)));
                lstLineaTar[n_index].n_capkilporhorlin = n_valor;                                                                   // CAPACIDAD DE KILOS DE LA LINEA POR HORA
                //8
                FgTar.SetData(FgTar.Row, 9, n_valor.ToString("0.00"));
            }
            //9
            if (FgTar.Col == 10)
            {
                //9
                n_valor = Convert.ToDouble(funFunciones.NulosN( FgTar.GetData(FgTar.Row, 10)));
                lstLineaTar[n_index].n_capkilporlintietra = n_valor;                                                                   // CANT DE KILOS POR LINEA DE TIEMPO
                //9
                FgTar.SetData(FgTar.Row, 10, n_valor.ToString("0.00"));
            }
            //10
            if (FgTar.Col == 11)
            {
                //10
                n_valor = Convert.ToDouble(FgTar.GetData(FgTar.Row, 11));
                lstLineaTar[n_index].n_numpercal = Convert.ToInt32(n_valor);                                                     // NUMERO DE PERSONAS CLACULADAS
                //10
                FgTar.SetData(FgTar.Row, 11, n_valor.ToString("0.00"));
            }
            //11
            if (FgTar.Col == 12)
            {
                //11
                n_valor = Convert.ToDouble(funFunciones.NulosN(FgTar.GetData(FgTar.Row, 12)));
                lstLineaTar[n_index].n_totprotietra = n_valor;                                                     // TOTAL A PRODUCIR EN EL LAPSO DE TIEMPO              
                //11
                FgTar.SetData(FgTar.Row, 12, n_valor.ToString("0.00"));
            }
            //13
            if (FgTar.Col == 13)
            {
                //12
                n_valor = Convert.ToDouble(funFunciones.NulosN(FgTar.GetData(FgTar.Row, 13)));
                lstLineaTar[n_index].n_porefiuni = n_valor;                                                    // PORCENTAJE EFICIENCIA UNITARIA
                //12
                FgTar.SetData(FgTar.Row, 13, n_valor.ToString("0.00"));
            }
            //13
            if (FgTar.Col == 14)
            {
                //13
                n_valor = Convert.ToDouble(funFunciones.NulosN(FgTar.GetData(FgTar.Row, 14)));
                lstLineaTar[n_index].n_porefitot = n_valor;   
                //13
                FgTar.SetData(FgTar.Row, 14, n_valor.ToString("0.00"));
            }
            //15
            if (FgTar.Col == 15)
            {
                //15
                n_valor = Convert.ToDouble(funFunciones.NulosN(FgTar.GetData(FgTar.Row, 15)));
                lstLineaTar[n_index].n_kghper = n_valor;                                                    // COSTO POR TAREA         
                //15
                FgTar.SetData(FgTar.Row, 15, n_valor.ToString("0.0000"));
            }
            //16
            if (FgTar.Col == 16)
            {
                //15
                n_valor = Convert.ToDouble(funFunciones.NulosN(FgTar.GetData(FgTar.Row, 16)));
                lstLineaTar[n_index].n_costar = n_valor;                                                    // COSTO POR TAREA         
                //15
                FgTar.SetData(FgTar.Row, 16, n_valor.ToString("0.0000"));
            }
            if (FgTar.Col == 19)
            {
                //19
                n_ord = Convert.ToInt32(FgTar.GetData(FgTar.Row, 19).ToString());
                lstLineaTar[n_index].n_ord = n_ord;                                                         // Nº ORDEN         
                FgTar.SetData(FgTar.Row, 19, n_ord.ToString("0"));
            }
        }
        private void FgTar_EnterCell(object sender, EventArgs e)
        {
            if (b_Modificar == false)
            {
                FgTar.AllowEditing = false; return;
            }
            if (FgTar.Rows.Count == 2) { return; }

            if (booAgregando == true) { return; }

            DataTable dtResul = new DataTable();

            if (FgTar.Col == 1)                                                 // TAREA
            {
                funFlex.FlexColumnaCombo(FgTar, dtTareas, "c_des", 1);          // ITEMS
                FgTar.AllowEditing = true;
            }

            if (FgTar.Col == 2)                                                 // % DE INCREMENTO
            {
                FgTar.AllowEditing = true;
            }
            if (FgTar.Col == 3)                                                 // CANTIDAD DE KILOGRAMOS
            {
                FgTar.AllowEditing = false;
            }
            if (FgTar.Col == 4)                                                 // DISTORCION DE LA MAQUINA
            {
                FgTar.AllowEditing = false;
            }
            if (FgTar.Col == 5)                                                 // NUMERO DE PERSONAS
            {
                FgTar.AllowEditing = true;
            }
            
            if (FgTar.Col == 6)                                                 
            {
                funFlex.FlexColumnaCombo(FgTar, dtEquipos, "c_des", 6);      // MAQUINAS Y EQUIPOS
                FgTar.AllowEditing = true;
            }

            // LINEA NUEVA
            if (FgTar.Col == 7)
            {
                if (funFunciones.NulosC(FgTar.GetData(FgTar.Row, 6)) != "")
                {
                    FgTar.AllowEditing = true;
                }
                else
                {
                    FgTar.AllowEditing = false;
                }
            }
            //7
            if (FgTar.Col == 8)                                                 // NUMERO DE PERSONAS
            {
                FgTar.AllowEditing = true;
            }
            //9
            if (FgTar.Col == 9)                                                 // NUMERO DE PERSONAS
            {
                FgTar.AllowEditing = true;
            }
            //10
            if (FgTar.Col == 10)                                                 // NUMERO DE PERSONAS
            {
                FgTar.AllowEditing = true;
            }
            //11
            if (FgTar.Col == 11)                                                 // NUMERO DE PERSONAS
            {
                FgTar.AllowEditing = false;
            }
            //12
            if (FgTar.Col == 12)                                                 // NUMERO DE PERSONAS
            {
                FgTar.AllowEditing = true;
            }
            //13
            if (FgTar.Col == 13)                                                 // NUMERO DE PERSONAS
            {
                FgTar.AllowEditing = false;
            }
            //14
            if (FgTar.Col == 14)                                                 // NUMERO DE PERSONAS
            {
                FgTar.AllowEditing = true;
            }
            //15
            if (FgTar.Col == 15)                                                 // KGH POR PERSONA
            {
                FgTar.AllowEditing = true;
            }
            //16
            if (FgTar.Col == 16)                                                 // NUMERO DE PERSONAS
            {
                FgTar.AllowEditing = true;
            }
            //12
            if ((FgTar.Col > 16) && (FgTar.Col < 18))
            {
                FgTar.AllowEditing = false;
            }
            if (FgTar.Col == 19)                                                 // NUMERO DE ORDEN
            {
                FgTar.AllowEditing = true;
            }
        }
        private void FgTar_KeyPressEdit(object sender, C1.Win.C1FlexGrid.KeyPressEditEventArgs e)
        {
            //    7
            if ((e.Col == 2) || (e.Col == 3) || (e.Col == 4) || (e.Col == 5) || (e.Col == 6) || (e.Col == 8))
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
