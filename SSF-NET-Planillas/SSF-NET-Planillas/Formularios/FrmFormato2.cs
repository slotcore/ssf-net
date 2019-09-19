using MySql.Data.MySqlClient;
using SIAC_Objetos.Sistema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using SIAC_Negocio.Planilla;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSF_NET_Planillas.Formularios
{
    public partial class FrmFormato2 : Form
    {
        // VARIABLES PUBLICAS
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        string strNumerovalidos = "1234567890." + (char)8;                                        // + (char)8;

        public FrmFormato2()
        {
            InitializeComponent();
        }

        private void CmdSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CmdImp_Click(object sender, EventArgs e)
        {
            if (TxtFchIni.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha de inicio !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchIni.Focus();
                return;
            }
            if (TxtFchFin.Text == "")
            {
                MessageBox.Show("¡ No ha especificado la fecha final !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtFchFin.Focus();
                return;
            }
            CN_pla_empleados o_emp = new CN_pla_empleados();
            o_emp.mysConec = mysConec;
            o_emp.STU_SISTEMA = STU_SISTEMA;
            o_emp.ReporteFormat2(TxtFchIni.Text, TxtFchFin.Text, Convert.ToInt32(TxtPor.Text));
            this.Close();
        }

        private void FrmFormato2_Load(object sender, EventArgs e)
        {
            this.Text = "PLANILLAS - EFICIENCIA DE ASISTENCIA";
            TxtPor.Text = "150";
        }

        private void TxtPor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void TxtFchIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void TxtFchFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (!strNumerovalidos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
    }
}
