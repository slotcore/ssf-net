using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Planillas;
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
    public partial class FrmActivarEmpleado : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public string c_CodEmpresa;
        public bool b_Grabo = false;

        // OBJETOS LOCALES
        CN_sys_formulariovista objFormVis = new CN_sys_formulariovista();
        CN_sys_formulario objForm = new CN_sys_formulario();

        CN_TEMPUS_marcacion objCabecera = new CN_TEMPUS_marcacion();                            // CABECERA DEL REGISTRO

        // OBJETOS DE ACCESO A DATOS
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        Funciones funFunciones = new Funciones();
        Genericas funDatos = new Genericas();
        Cls_DBGrid funDbGrid = new Cls_DBGrid();

        string[,] arrCabeceraFlex1 = new string[4, 5];

        // DATATABLE LOCALES
        DataTable dtCabecera = new DataTable();                                        // CABECERA DEL REGISTRO

        public FrmActivarEmpleado()
        {
            InitializeComponent();
        }

        private void FrmActivarEmpleado_Load(object sender, EventArgs e)
        {
            ConfigurarFormulario();

            MostrarDatos();
        }
        void MostrarDatos()
        {
            int n_row = 0;
            CN_TEMPUS_marcacion objPersona = new CN_TEMPUS_marcacion();

            objPersona.mysConec = mysConec;
            dtCabecera = objPersona.ListarPersonal(c_CodEmpresa);
            FgEmpleados.Rows.Count = 2;
            for (n_row = 0; n_row <= dtCabecera.Rows.Count - 1; n_row++)
            {
                FgEmpleados.Rows.Count = FgEmpleados.Rows.Count + 1;
                FgEmpleados.SetData(FgEmpleados.Rows.Count - 1, 1, dtCabecera.Rows[n_row]["c_dni"].ToString());
                FgEmpleados.SetData(FgEmpleados.Rows.Count - 1, 2, dtCabecera.Rows[n_row]["c_apenom"].ToString());
                FgEmpleados.SetData(FgEmpleados.Rows.Count - 1, 3, true);
                FgEmpleados.SetData(FgEmpleados.Rows.Count - 1, 4, dtCabecera.Rows[n_row]["c_codigo"].ToString());
            }
        }
        void ConfigurarFormulario()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            //this.Height = 587;
            //this.Width = 1000;
            
            //Tab1.SelectedIndex = 0;
            //LblTitulo2.Text = "DETALLE DEL REGISTRO";
            OptSelTod.Checked = true;
            OptUnSelTod.Checked = false;

            arrCabeceraFlex1[0, 0] = "D.N.I.";
            arrCabeceraFlex1[0, 1] = "80";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_tipexides";

            arrCabeceraFlex1[1, 0] = "Empleado";
            arrCabeceraFlex1[1, 1] = "412";
            arrCabeceraFlex1[1, 2] = "C";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_itedes";

            arrCabeceraFlex1[2, 0] = "Activar";
            arrCabeceraFlex1[2, 1] = "50";
            arrCabeceraFlex1[2, 2] = "B";
            arrCabeceraFlex1[2, 3] = "";
            arrCabeceraFlex1[2, 4] = "";

            arrCabeceraFlex1[3, 0] = "Codigo";
            arrCabeceraFlex1[3, 1] = "0";
            arrCabeceraFlex1[3, 2] = "C";
            arrCabeceraFlex1[3, 3] = "";
            arrCabeceraFlex1[3, 4] = "";

            funFlex.FlexMostrarDatos(FgEmpleados, arrCabeceraFlex1, dtCabecera, 2, false);
            this.Text = "PLANILLAS - ACTIVACION DE EMPLEADOS";
            FgEmpleados.Rows.Count = 2;
        }

        private void FgEmpleados_Click(object sender, EventArgs e)
        {

        }

        private void CmdSalir_Click(object sender, EventArgs e)
        {
            b_Grabo = false;
            this.Hide();
        }

        private void OptUnSelTod_CheckedChanged(object sender, EventArgs e)
        {
            int n_fila;

            for (n_fila = 2; n_fila <= FgEmpleados.Rows.Count - 1; n_fila++)
            {
                FgEmpleados.SetData(n_fila, 3, false);
            }
        }

        private void OptSelTod_CheckedChanged(object sender, EventArgs e)
        {
            int n_fila;

            for (n_fila = 2; n_fila <= FgEmpleados.Rows.Count - 1; n_fila++)
            {
                FgEmpleados.SetData(n_fila, 3, true);
            }
        }

        private void CmdOk_Click(object sender, EventArgs e)
        {
            int n_fila = 0;
            List<BE_TEMPUS_MOSTRAR> lstEmpleados = new List<BE_TEMPUS_MOSTRAR>();
            CN_TEMPUS_mostrar objFun = new CN_TEMPUS_mostrar();

            objFun.mysConec = mysConec;
            objFun.Eliminar(c_CodEmpresa);

            for (n_fila = 2; n_fila <= FgEmpleados.Rows.Count-1; n_fila++)
            {
                BE_TEMPUS_MOSTRAR ent_empleado = new BE_TEMPUS_MOSTRAR();
                ent_empleado.c_idemp = c_CodEmpresa;
                ent_empleado.c_codemp = FgEmpleados.GetData(n_fila, 4).ToString();

                if (FgEmpleados.GetData(n_fila, 3).ToString() == "True")
                {
                    ent_empleado.n_mostrar = 1;
                }
                else
                {
                    ent_empleado.n_mostrar = 0;
                }
                lstEmpleados.Add(ent_empleado);
            }

            if (objFun.Insertar(lstEmpleados) == true) 
            {
                MessageBox.Show("El datos se actualizaron con exito", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show("No se pudieron actualizar los registros por el siguiente motivo :" + objFun.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            b_Grabo = true;
            this.Hide();
        }
    }
}
