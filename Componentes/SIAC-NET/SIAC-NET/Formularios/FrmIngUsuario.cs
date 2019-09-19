using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using MySql.Data.MySqlClient;
using Helper;
using SIAC_Negocio.Sistema;

namespace SIAC_NET.Formularios
{
    public partial class FrmIngUsuario : DevComponents.DotNetBar.Metro.MetroForm
    {
        public MySqlConnection mysConeccion = new MySqlConnection();
        int intOportunidades;
        public FrmIngUsuario()
        {
            InitializeComponent();
        }

        private void FrmIngUsuario_Load(object sender, EventArgs e)
        {
            intOportunidades = 1;
        }
        private void CmdCancela_Click(object sender, EventArgs e)
        {
            mysConeccion = null;
            this.Close();
            Close();
        }
        private void CmdAcepta_Click(object sender, EventArgs e)
        {
            if (intOportunidades <= 3)
            {
                DataTable dtResult = new DataTable();
                CN_sys_usuarios objUsuario = new CN_sys_usuarios();
                objUsuario.mysConec = mysConeccion;
                dtResult = objUsuario.TraerUsuario(TxtUsuario.Text, TxtPass.Text);
                intOportunidades = intOportunidades + 1;
                
                if (dtResult.Rows.Count == 0)
                {
                    MessageBox.Show("¡ Usuario o Password incorrectos, Ingrese otro !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);                  
                }
                    else
                {
                    this.Hide();
                    this.Close();
                    FrmMenu10 xFrmMenu = new FrmMenu10();
                    //xFrmMenu.mysConeccion = mysConeccion;
                    xFrmMenu.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("¡ Ha excedido el numero de oportunidades para ingresar al sistema !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                mysConeccion = null;
                this.Close();
                Close();
            }
        }
        private void TxtUsuario_TextChanged(object sender, EventArgs e)
        {

        }
        private void TxtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void FrmIngUsuario_Activated(object sender, EventArgs e)
        {
            TxtUsuario.Focus();
        }
    }
}