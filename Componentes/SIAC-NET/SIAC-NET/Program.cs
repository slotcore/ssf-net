using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIAC_Objetos.Sistema;
using SIAC_NET.Formularios;
using MySql.Data.MySqlClient;
using Helper;
using System.Data;

namespace SIAC_NET
{
    public class Program
    {
        public static MySqlConnection mysConeccion = new MySqlConnection();

        public static Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        [STAThread]
                
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mysConeccion = Conectar();

            if (mysConeccion.State == ConnectionState.Open) 
            {
                FrmIngUsuario MiForm  = new FrmIngUsuario();
                MiForm.mysConeccion = mysConeccion;
                MiForm.ShowDialog();
                //MessageBox.Show(" Se conecto con exito", striTituloSistema, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show("No se pudo abrir la BD ", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //mysConectar = Nothing;
            }
            
            //Application.Run(new FrmIngUsuario());
            //Application.Run(new MDIMenu());
        }

        static MySqlConnection Conectar()
        {
            MySqlConnection mysConectar = new MySqlConnection();
            Helper.DatosMySql hlpFuncion = new Helper.DatosMySql();

            mysConectar = hlpFuncion.ObtenerConexion("localhost", "data01", "root", "010419762005");
            return mysConectar;
        }
    }
}
