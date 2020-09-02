using Helper;
using Helper.Comunes;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Ventas;
using SIAC_Entidades.Maestros;
using SIAC_Entidades.Sistema;
using SIAC_Entidades.Estacionamiento;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Ventas;
using SIAC_Negocio.Almacen;
using SIAC_Negocio.Maestros;
using SIAC_Objetos.Sistema;
using SIAC_Negocio.Contabilidad;
using SIAC_Negocio.Sunat;
using SIAC_Negocio.Estacionamiento;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SIAC_NET_Estacionamientos.Formularios
{
    public partial class FrmConfigurarPunto : Form
    {
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
                
        Genericas funGen = new Genericas();

        DataTable dtLocal = new DataTable();
        DataTable dtCajero = new DataTable();

        bool b_agregando = false;
        string C_IDCAJERO;
        string C_IDLOCAL;

        public FrmConfigurarPunto()
        {
            InitializeComponent();
        }

        private void CmdCan_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void CargarDatos()
        {
            CN_est_conecta o_con = new CN_est_conecta(STU_SISTEMA);
            CN_sys_empresalocal o_emploc = new CN_sys_empresalocal();
            o_emploc.mysConec = o_con.mysConec;
            dtLocal = o_emploc.Listar(STU_SISTEMA.EMPRESAID, 0);
            o_emploc = null;

            CN_est_cajeros o_cajero = new CN_est_cajeros(STU_SISTEMA);
            o_cajero.STU_SISTEMA = STU_SISTEMA;
            o_cajero.Listar(STU_SISTEMA.EMPRESAID);
            dtCajero = o_cajero.dtListar;
            o_cajero = null;
        }

        private void FrmConfigurarPunto_Load(object sender, EventArgs e)
        {
            CargarDatos();
            ConfigurarForrmulario();
        }
            
        void ConfigurarForrmulario()
        {
            this.Text = "ESTACIONAMIENTO - SETUP";
            string c_nomarc = @"C:\SSF-NET\estacionamiento.ini";
            //string N_IDEMPRESA = funGen.IniLeerSeccion(c_nomarc, "INFORMACION", "EMPRESA").ToString();
            C_IDLOCAL = funGen.IniLeerSeccion(c_nomarc, "INFORMACION", "LOCAL").ToString();
            C_IDCAJERO = funGen.IniLeerSeccion(c_nomarc, "INFORMACION", "CAJERO").ToString();
            b_agregando = true;
            funGen.ComboBoxCargarDataTable(CboLocal, dtLocal, "n_id", "c_des");
            CboLocal.SelectedValue = Convert.ToInt32(C_IDLOCAL);

            DataTable dtResul = new DataTable();
            dtResul = funGen.DataTableFiltrar(dtCajero, "n_idloc = " + Convert.ToInt32(CboLocal.SelectedValue).ToString() + "");
            funGen.ComboBoxCargarDataTable(CboCajero, dtResul, "n_id", "c_cajapenom");
            CboCajero.SelectedValue = Convert.ToInt32(C_IDCAJERO);
            b_agregando = false;
        }

        private void CmdAce_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(CboCajero.SelectedValue) == 0)
            {
                MessageBox.Show("¡ No ha especificado el nombre del cajero !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                CboCajero.Focus();
                return;
            }
            
            string c_nomarc = @"C:\SSF-NET\estacionamiento.ini";
            funGen.IniEscribirSeccion(c_nomarc, "INFORMACION", "LOCAL", Convert.ToInt32(CboLocal.SelectedValue).ToString());
            funGen.IniEscribirSeccion(c_nomarc, "INFORMACION", "CAJERO", Convert.ToInt32(CboCajero.SelectedValue).ToString());

            CmdCan_Click(sender, e);
        }

        private void CboLocal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (b_agregando == true)
            {
                return;
            }
            DataTable dtResul = new DataTable();
            dtResul = funGen.DataTableFiltrar(dtCajero, "n_idloc = " + Convert.ToInt32(CboLocal.SelectedValue).ToString() + "");
            funGen.ComboBoxCargarDataTable(CboCajero, dtResul, "n_id", "c_cajapenom");
            //CboCajero.SelectedValue = Convert.ToInt32(C_IDCAJERO);
        }
    }
}
