using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using SIAC_Negocio.Sistema;
using Helper;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Sistema;
using SIAC_Negocio.Contabilidad;

namespace SSF_NET.Formularios
{
    public partial class FrmSelEmpresa : Form
    {
        public MySqlConnection mysConec = new MySqlConnection();
        public int n_VienedelMenu;        
        Genericas funDatos = new Genericas();
        CN_con_tc objTC = new CN_con_tc();

        DataTable DtResultado = new DataTable();
        DataTable dtSetup = new DataTable();

        bool booSeEjecuto = false;
        bool booAcepto = false;
        public FrmSelEmpresa()
        {
            InitializeComponent();
        }
        private void FrmSelEmpresa_Load(object sender, EventArgs e)
        {
            booSeEjecuto = false;
            this.Text = Program.STU_SISTEMA.SYS_NOMBREABREV + " -  Seleccion de Empresa";
            TxtAñoTra.Minimum = 2006;
            TxtAñoTra.Maximum = DateTime.Now.Year;
            CargarEmpresa();
            TxtAñoTra.Value = DateTime.Now.Year;
            booAcepto = false;
        }
        void CargarEmpresa()
        {
            CN_sys_empresa MiFun = new CN_sys_empresa();
                 
            MiFun.mysConec = mysConec;
            DtResultado = MiFun.ListarEmpresasUsuario(Program.STU_SISTEMA.USUARIOID);
            funDatos.ComboBoxCargarDataTable(CboMeses, DtResultado, "n_id", "c_nomemp");
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(CboMeses.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha seleccionado el nombre de la empresa !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            if (Convert.ToInt16(TxtAñoTra.Value) == 0)
            {
                MessageBox.Show("¡ No ha selecfcionado el nombre de la empresa !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }

            Int16 n_IdEmpresa = Convert.ToInt16(CboMeses.SelectedValue);
            Int16 n_AnoTrabajo = Convert.ToInt16(TxtAñoTra.Value);
            CN_sys_setup objSet = new CN_sys_setup();
            DataTable dtResult = new DataTable();

            objSet.mysConec = mysConec;
            dtResult = objSet.TraerRegistro();
            
            this.Hide();
            this.Close();

            if (n_VienedelMenu == 0)
            {
                FrmIngUsuario MiForm = new FrmIngUsuario();

                Program.STU_SISTEMA.SYS_UNIBD = Convert.ToInt16(dtResult.Rows[0]["n_unimae"]);
                Program.STU_SISTEMA.SYS_UNIUSU = Convert.ToInt16(dtResult.Rows[0]["n_uniusu"]);

                dtResult = funDatos.DataTableFiltrar(DtResultado, "n_id =" + n_IdEmpresa.ToString() + "");
                if (dtResult.Rows.Count != 0)
                { 
                    Program.STU_SISTEMA.EMPRESAID = Convert.ToInt16(dtResult.Rows[0]["n_id"]);
                    Program.STU_SISTEMA.EMPRESANOMBRE = dtResult.Rows[0]["c_nomemp"].ToString();
                    Program.STU_SISTEMA.EMPRESARUC = dtResult.Rows[0]["c_numdoc"].ToString();
                    Program.STU_SISTEMA.ANOTRABAJO = n_AnoTrabajo;

                    Program.STU_SISTEMA.SYS_NOMALT = dtResult.Rows[0]["c_nomalt"].ToString();
                    Program.STU_SISTEMA.SYS_VERDATALT = Convert.ToInt16(dtResult.Rows[0]["n_verdatalt"]);
                }
                this.Hide();
                this.Close();
                FrmMenu10 xFrmMenu = new FrmMenu10();

                objTC.mysConec = mysConec;
                objTC.TraerTC(DateTime.Now.ToString("dd/MM/yyyy"), 151);
                dtResult = objTC.dtLista;
                if (dtResult.Rows.Count != 0)
                {
                    Program.STU_SISTEMA.TIPOCAMBIO = Convert.ToDouble(dtResult.Rows[0]["n_impven"]);
                    objTC = null;
                }
                else
                {
                    Program.STU_SISTEMA.TIPOCAMBIO = 0;
                }
                Program.GuardarIngreso(1);
                xFrmMenu.ShowDialog();
            }
            else
            {
                DataTable dt = new DataTable();

                dt = funDatos.DataTableFiltrar(DtResultado, "n_id = " + Convert.ToInt16(CboMeses.SelectedValue) + "");
                FrmMenu10 xFrmMenu = new FrmMenu10();
                Program.STU_SISTEMA.EMPRESAID = Convert.ToInt16(dt.Rows[0]["n_id"]);
                Program.STU_SISTEMA.EMPRESANOMBRE = dt.Rows[0]["c_nomemp"].ToString();
                Program.STU_SISTEMA.EMPRESARUC = dt.Rows[0]["c_numdoc"].ToString();
                Program.STU_SISTEMA.ANOTRABAJO = n_AnoTrabajo;

                Program.STU_SISTEMA.SYS_NOMALT = dt.Rows[0]["c_nomalt"].ToString();
                Program.STU_SISTEMA.SYS_VERDATALT = Convert.ToInt16(dt.Rows[0]["n_verdatalt"]);

                //Program.STU_SISTEMA.USUARIOALIAS = TxtUsuario.Text;
                //Program.STU_SISTEMA.USUARIOID = Convert.ToInt16(dtResult.Rows[0]["n_id"].ToString());
                //Program.GuardarIngreso(1);

                xFrmMenu.ShowDialog();
            }
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            if (Program.STU_SISTEMA.EMPRESAID == 0)
            {
                mysConec = null;
                this.Close();
                Close();
            }
            else
            {
                this.Close();
            }
        }
    }
}

