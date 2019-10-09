using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Helper;
using SIAC_Negocio.Sistema;
using System.Configuration;
//using SIAC_Negocio.Sistema;
//using SIAC_Entidades.Sistema;
//using SIAC_Negocio.Contabilidad;

namespace SSF_NET.Formularios
{
    public partial class FrmIngUsuario : Form
    {
        public MySqlConnection mysConeccion = new MySqlConnection();
        public Int16 n_IdEmpresa;
        public Int16 n_AnoTrabajo;

        Helper.Genericas miFun = new Helper.Genericas();
        
        int intOportunidades;
        public FrmIngUsuario()
        {
            InitializeComponent();
        }
        private void FrmIngUsuario_Load(object sender, EventArgs e)
        {
            this.Text = Program.STU_SISTEMA.SYS_NOMBREABREV + " -  ACCESO DE USUARIO";
            intOportunidades = 1;
        }
        private void CmdCancela_Click(object sender, EventArgs e)
        {

        }
        private void CmdAcepta_Click(object sender, EventArgs e)
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
            
        }
        private void FrmIngUsuario_Activated(object sender, EventArgs e)
        {
        //    TxtUsuario.Text = "ADMIN";
        //    TxtPass.Text = "40404040";
            TxtUsuario.Focus();
        }
        private void CmdAceptar_Click(object sender, EventArgs e)
        {
            DataTable dtResult = new DataTable();
            if (TxtUsuario.Text =="")
            {
                MessageBox.Show("¡ Nombre del usuario incorrectos, Ingrese otro !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtUsuario.Focus();
                return;
            }
            if (TxtPass.Text =="")
            {
                MessageBox.Show("¡ Password incorrecto, Ingrese otro !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                TxtPass.Focus();
                return;
            }

            
            ObternerBD(n_IdEmpresa);

            if (intOportunidades <= 3)
            {
                CN_sys_usuarios objUsuario = new CN_sys_usuarios();
                objUsuario.mysConec = Program.mysConeccion;

                dtResult = objUsuario.TraerUsuario(TxtUsuario.Text, TxtPass.Text, Convert.ToInt16(Program.STU_SISTEMA.EMPRESAID),  Convert.ToInt16(Program.STU_SISTEMA.SYS_UNIUSU));
                intOportunidades = intOportunidades + 1;

                if (dtResult.Rows.Count == 0)
                {
                    MessageBox.Show("¡ Usuario o Password incorrectos, Ingrese otro !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    Program.STU_SISTEMA.USUARIOALIAS = TxtUsuario.Text;
                    Program.STU_SISTEMA.USUARIOID = Convert.ToInt16(dtResult.Rows[0]["n_id"].ToString());
                    Program.STU_SISTEMA.USUARIOPERFIL = Convert.ToInt16(dtResult.Rows[0]["n_idperfil"].ToString());

                    this.Hide();
                    this.Close();

                    FrmSelEmpresa miForm = new FrmSelEmpresa();
                    miForm.n_VienedelMenu = 0;
                    miForm.mysConec = mysConeccion;
                    miForm.ShowDialog();

                    //this.Hide();
                    //this.Close();
                    //FrmMenu10 xFrmMenu = new FrmMenu10();
                    //Program.STU_SISTEMA.USUARIOALIAS = TxtUsuario.Text;
                    //Program.STU_SISTEMA.USUARIOID = Convert.ToInt16(dtResult.Rows[0]["n_id"].ToString());
                    //Program.STU_SISTEMA.USUARIOPERFIL = Convert.ToInt16(dtResult.Rows[0]["n_idperfil"].ToString());

                    //objTC.mysConec = mysConeccion;
                    //objTC.TraerTC(DateTime.Now.ToString("dd/MM/yyyy"), 151);
                    //dtResult = objTC.dtLista;
                    //if (dtResult.Rows.Count != 0)
                    //{
                    //    Program.STU_SISTEMA.TIPOCAMBIO = Convert.ToDouble(dtResult.Rows[0]["n_impven"]);
                    //    objTC = null;
                    //}
                    //else
                    //{
                    //    Program.STU_SISTEMA.TIPOCAMBIO = 0;
                    //}
                    //Program.GuardarIngreso(1);
                    //xFrmMenu.ShowDialog();
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
        void ObternerBD(int n_IdEmpresa)
        {
            
            DataTable dtResult = new DataTable();
            Cls_Seguridad objSeg = new Cls_Seguridad();
            string c_nomBD = "";
            string c_nomODBC = "";
            string c_usu = "";
            string c_pas = "";
            string c_nomEmpresa = "";
            string c_rucEmpresa = "";
            CN_sys_empresa objEmpresa = new CN_sys_empresa();
            string c_nomarc = ConfigurationManager.AppSettings["PathIniFile"];
            
            string c_nombd = miFun.IniLeerSeccion(c_nomarc, "INFORMACION", "DATO6").ToString();
            c_nombd = objSeg.Desencriptar(c_nombd);

            
            c_usu = miFun.IniLeerSeccion(c_nomarc, "INFORMACION", "DATO3").ToString();
            c_usu = objSeg.Desencriptar(c_usu);

            c_pas = miFun.IniLeerSeccion(c_nomarc, "INFORMACION", "DATO4").ToString();
            c_pas = objSeg.Desencriptar(c_pas);

            //BE_SYS_EMPRESA entEmpresa = new BE_SYS_EMPRESA();

            objEmpresa.mysConec = mysConeccion;
            dtResult = objEmpresa.TraerRegistroBDMain(n_IdEmpresa);

            if (dtResult.Rows.Count == 0)
            {
                MessageBox.Show("¡ La empresa seleccionada no existe, seleccione otra !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                this.Close();
                Close();
            }
            else 
            {
                c_nomBD = c_nombd;
                c_nomODBC = c_nombd;
                c_nomEmpresa = dtResult.Rows[0]["c_nomemp"].ToString();
                c_rucEmpresa = dtResult.Rows[0]["c_numdoc"].ToString();

                Program.STU_SISTEMA.BD_NOMSERVIDOR = c_nomODBC;   // LE PASAMOS EL NOMBRE DEL ODBC
                Program.STU_SISTEMA.BD_NOMBASEDATOS = c_nomBD;   // LE PASAMOS EL NOMBRE DE LA BASE DE DATOS
                Program.STU_SISTEMA.BD_USUARIO = c_usu;
                Program.STU_SISTEMA.BD_CONTRASEÑA = c_pas;
                
                Program.STU_SISTEMA.EMPRESAID = n_IdEmpresa;
                Program.STU_SISTEMA.ANOTRABAJO = n_AnoTrabajo;
                Program.STU_SISTEMA.EMPRESANOMBRE = c_nomEmpresa;
                Program.STU_SISTEMA.MESTRABAJO = DateTime.Now.Month;
                Program.STU_SISTEMA.EMPRESARUC = c_rucEmpresa;
            }
        }
        private void CmdSalir_Click(object sender, EventArgs e)
        {
            mysConeccion = null;
            this.Close();
            Close();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void TxtUsuario_TextChanged(object sender, EventArgs e)
        {

        }
        private void TxtPass_TextChanged(object sender, EventArgs e)
        {

        }
        private void TxtUsuario_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
        private void TxtPass_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                SendKeys.Send("{TAB}");
            }
        }
    }
}
