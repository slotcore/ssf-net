using Helper;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Sistema;
using SIAC_Negocio.Sistema;
using SIAC_Objetos.Sistema;
using SSF_NET.Formularios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using Newtonsoft.Json;

namespace SSF_NET
{
    public class Program
    {
        public static MySqlConnection mysConeccion = new MySqlConnection();
        public static OleDbConnection AccConeccion = new OleDbConnection();
        public static Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();
        public static SqlConnection sqlConeccion = new SqlConnection();
        
        //Función importada de la librería user32.dll para mostrar una ventana en diferentes estados
        [DllImport("user32.dll")]
        public static extern long ShowWindow(IntPtr hwnd, uint nCmdShow);

        //Función para pasar a primer plano una ventana y activarla
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hwnd);
        static bool b_MultiSession = false; 

        [STAThread]
        static void Main()
        {
            //Genericas fundt = new Genericas();
            //DataTable dt = new DataTable();
            //dt = fundt.DataTableAPI("http://localhost:30050/api/sys_empresa/listar");
            //enviar();
            //string c_cad = "";
            //Uri url = new Uri(string.Format("http://localhost:30050/api/sys_usuario/insertar"));
            //string request = "{\"dato1\":123}";
            //c_cad = Post(url, request);
            //Send();

            Helper.Genericas miFun = new Helper.Genericas();
            Cls_Seguridad objSeg = new Cls_Seguridad();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string c_nomarc = ConfigurationManager.AppSettings["PathIniFile"];
            string c_serip = miFun.IniLeerSeccion(c_nomarc, "INFORMACION", "DATO1").ToString();
            string c_nombd = miFun.IniLeerSeccion(c_nomarc, "INFORMACION", "DATO2").ToString();
            string c_usu = miFun.IniLeerSeccion(c_nomarc, "INFORMACION", "DATO3").ToString();
            string c_pas = miFun.IniLeerSeccion(c_nomarc, "INFORMACION", "DATO4").ToString();
            string c_rutarep = miFun.IniLeerSeccion(c_nomarc, "INFORMACION", "DATO5").ToString();
            
            string c_sysnom = miFun.IniLeerSeccion(c_nomarc, "INFORMACION", "DATO7").ToString();
            string c_sysver = miFun.IniLeerSeccion(c_nomarc, "INFORMACION", "DATO8").ToString();
            string c_sysnomabr = miFun.IniLeerSeccion(c_nomarc, "INFORMACION", "DATO9").ToString();
            string c_puerto = miFun.IniLeerSeccion(c_nomarc, "INFORMACION", "DATO10").ToString();
            string c_multisesion = miFun.IniLeerSeccion(c_nomarc, "INFORMACION", "DATO11").ToString();

            if (c_multisesion == "1") { b_MultiSession = true; }
            
            c_serip = objSeg.Desencriptar(c_serip);
            c_nombd = objSeg.Desencriptar(c_nombd);
            c_usu = objSeg.Desencriptar(c_usu);
            c_pas = objSeg.Desencriptar(c_pas);
            
            c_sysnom = objSeg.Desencriptar(c_sysnom);
            c_sysver = objSeg.Desencriptar(c_sysver);
            c_sysnomabr = objSeg.Desencriptar(c_sysnomabr);
            c_puerto = objSeg.Desencriptar(c_puerto);
            mysConeccion = Conectar(c_nombd, c_serip, c_usu, c_pas, c_puerto);

            STU_SISTEMA.RUTAREPORTES = c_rutarep;

            STU_SISTEMA.EMPRESAID = 0;
            STU_SISTEMA.EMPRESANOMBRE = "";
            STU_SISTEMA.ANOTRABAJO = 0;
            STU_SISTEMA.RUTAFOTOITEMS = "";
            STU_SISTEMA.RUTAPRODUCCIONCERTIFICADOS = "";
            STU_SISTEMA.RUTAFOTOEMPLEADOS = "";

            STU_SISTEMA.SYS_NOMBRE = c_sysnom;
            STU_SISTEMA.SYS_VESION = c_sysver;
            STU_SISTEMA.SYS_NOMBREABREV = c_sysnomabr;

            STU_SISTEMA.BD_IP = c_serip;
            STU_SISTEMA.BD_NOMBASEDATOS = c_nombd;
            STU_SISTEMA.BD_USUARIO = c_usu;
            STU_SISTEMA.BD_CONTRASEÑA = c_pas;
            STU_SISTEMA.BD_PUERTO = c_puerto;

            //IsExecutingApplication();

            if (mysConeccion != null)
            {
                if (b_MultiSession == true)
                {
                    if (mysConeccion.State == ConnectionState.Open)
                    {
                        FrmIngUsuario miForm = new FrmIngUsuario();
                        miForm.mysConeccion = mysConeccion;
                        miForm.ShowDialog();
                    }
                    //else
                    //{
                    //    MessageBox.Show("No se pudo abrir la BD ", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    //}
                }
                else
                {
                    if (mysConeccion.State == ConnectionState.Open)
                    {
                        FrmIngUsuario miForm = new FrmIngUsuario();
                        miForm.mysConeccion = mysConeccion;
                        miForm.ShowDialog();
                    }
                    //if (IsExecutingApplication() == false)
                    //{
                    //    if (mysConeccion.State == ConnectionState.Open)
                    //    {
                    //        FrmIngUsuario miForm = new FrmIngUsuario();
                    //        miForm.mysConeccion = mysConeccion;
                    //        miForm.ShowDialog();
                    //    }
                    //    //else
                    //    //{
                    //    //    MessageBox.Show("No se pudo abrir la BD ", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    //    //}
                    //}
                    //else
                    //{
                    //    MessageBox.Show("El sistema ya fue ejecutado", "Informacion Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    //    Application.Exit();
                    //}
                }
            }
        }
        private static bool IsExecutingApplication()
        {
            // Proceso actual
            Process currentProcess = Process.GetCurrentProcess();
            string c_cad = "";
            // Matriz de procesos
            Process[] processes = Process.GetProcesses();

            // Recorremos los procesos en ejecución
            foreach (Process p in processes)
            {
                if (p.Id != currentProcess.Id)
                {
                    c_cad = p.ProcessName + "0000000";
                    if (c_cad.Substring(0, 7) == currentProcess.ProcessName.Substring(0, 7))
                    {
                        return true;
                    }
                }
            }
            return false;
        } 
        private static bool FirstInstance
        {
            get
            {
                bool created;
                string name = Assembly.GetEntryAssembly().FullName;
                // created will be True if the current thread creates and owns the mutex.
                // Otherwise created will be False if a previous instance already exists.
                Mutex mutex = new Mutex(true, name, out created);
                return created;
            }
        }

        //public static void ConectarSQLSERVER()
        //{
        //    //string c_cadCon;
        //    SqlConnection Coneccion = new SqlConnection();
        //    Helper.DatosSQL objSQLCon = new Helper.DatosSQL();

        //    Coneccion = objSQLCon.AbrirConeccion(@"AGRO-22\SQLEXPRESS", "TEMPUS", "TEMPUS", "TEMPUS");
        //    sqlConeccion = Coneccion;

        //     if (objSQLCon.booOcurrioError == true)
        //    {
        //        MessageBox.Show("No se pudo abrir la BD, por el siguiente motivo: " + objSQLCon.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        //        //mysConectar.cl = null;
        //    }
        //}
        public static MySqlConnection Conectar(string c_BaseDatos, string c_IP, string c_Usuario, string c_Password, string c_Puerto)
        {
            MySqlConnection mysConectar = new MySqlConnection();
            Helper.DatosMySql hlpFuncion = new Helper.DatosMySql();

            mysConectar = hlpFuncion.ObtenerConexion(c_IP, c_BaseDatos, c_Usuario, c_Password, c_Puerto);

            if (hlpFuncion.booOcurrioError == true)
            {
                MessageBox.Show("No se pudo abrir la BD, por el siguiente motivo: " + hlpFuncion.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            return mysConectar;
        }
        public static OleDbConnection ConectaAcces(int n_IdEmpresa)
        {
            string c_cadCon;
            OleDbConnection objCon = new OleDbConnection();
            Helper.DatosAcces objFunAcc = new Helper.DatosAcces();
            string c_RutaBD;
            string c_RutaGT;
            if (n_IdEmpresa == 1)
            {
                c_RutaBD = "J:\\seven\\data\\2017\\0001\\data.mdb";
            }
            else
            {
                c_RutaBD = "J:\\seven\\data\\2017\\0002\\data.mdb";            
            }
            c_RutaGT = "c:\\seven\\seven.mdw";

            c_cadCon = "Provider = Microsoft.Jet.OLEDB.4.0;" +
                "Data Source = '" + c_RutaBD + "';" +
                "Jet OLEDB:System Database = '" + c_RutaGT + "';" +
                "User Id = 'cav2005sialp'" + ";" +
                "Password = '010419762005'";

            objCon = objFunAcc.AbrirConeccion(c_cadCon);
            return objCon;
        }
        public static void GuardarIngreso(int n_TipMov)
        {
            BE_SYS_USUARIOSINGRESOS entIngresos = new BE_SYS_USUARIOSINGRESOS();
            CN_sys_usuariosingresos objIngresos = new CN_sys_usuariosingresos();

            entIngresos.n_id = 0;
            entIngresos.n_idemp = Program.STU_SISTEMA.EMPRESAID;
            entIngresos.n_idusu = Program.STU_SISTEMA.USUARIOID;
            entIngresos.d_fchreg = DateTime.Now;
            entIngresos.c_idpc = "";
            entIngresos.c_horreg = DateTime.Now.ToString("hh:mm:ss");
            entIngresos.n_tipmov = n_TipMov;
            if (n_TipMov == 1) { entIngresos.n_ord = 1; }
            if (n_TipMov == 2) { entIngresos.n_ord = 2; }
            
            objIngresos.mysConec = Program.mysConeccion;
            if (objIngresos.Insertar(entIngresos) == true)
            {
            }
        }


        //public static void mt_otro3()
        //{
        //    try
        //    {
        //        //Creating the Web Request.
        //        HttpWebRequest httpWebRequest = HttpWebRequest.Create("http://www.sunat.gob.pe/cl-ti-itmrconsruc/jcrS00Alias") as HttpWebRequest;
        //        //Specifing the Method 
        //        httpWebRequest.Method = "POST";
        //        //Data to Post to the Page, itis key value pairs; separated by "&"
        //        string data = "accion=consPorRuc&nroRuc=20100495989&search1:20100495989&codigo=FGRT&tipdoc=1";
        //        //Setting the content type, it is required, otherwise it will not work.
        //        httpWebRequest.ContentType = "application/x-www-form-urlencoded";
        //        //Getting the request stream and writing the post data
        //        using (StreamWriter sw = new StreamWriter(httpWebRequest.GetRequestStream()))
        //        {
        //            sw.Write(data);
        //        }
        //        //Getting the Respose and reading the result.
        //        HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse;
        //        using (StreamReader sr = new StreamReader(httpWebResponse.GetResponseStream()))
        //        {
        //            MessageBox.Show(sr.ReadToEnd());
        //        }
        //    }
        //    catch (WebException wex)
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        sb.AppendLine("ERROR:" + wex.Message + ". STATUS: " + wex.Status.ToString());

        //        if (wex.Status == WebExceptionStatus.ProtocolError)
        //        {
        //            var response = ((HttpWebResponse)wex.Response);
        //            sb.AppendLine(string.Format("Status Code : {0}", response.StatusCode));
        //            sb.AppendLine(string.Format("Status Description : {0}", response.StatusDescription));

        //            try
        //            {
        //                StreamReader reader = new StreamReader(response.GetResponseStream());
        //                sb.AppendLine(reader.ReadToEnd());
        //            }
        //            catch (WebException ex) { throw; }
        //        }

        //        throw new Exception(sb.ToString(), wex);
        //    }
        //    return;
        //}


        //public static bool BackupDatabase(string filename)
        //{
        //    string DatabaseUsername = "root";
        //    string DatabasePassword = "010419762005";
        //    string DatabaseName = "data06";
        //    bool _isExecuted = false;

        //    if (filename == "")
        //    {
        //        filename = string.Format("{0}/db_{1:MMddyyyyHHmmss}.sql", Environment.GetFolderPath(Environment.SpecialFolder.Desktop), DateTime.Now);
        //        filename = "C:\\SSF-NET\\data\\sssss.sql";
        //    }

        //    string _queryBackup = string.Format("mysqldump -u '{0}' -p '{1}' '{2}' > '{3}'", DatabaseUsername, DatabasePassword, DatabaseName, filename);
        //    using (MySqlConnection con = new MySqlConnection(mysConeccion.ConnectionString))
        //    {
        //        try
        //        {
        //            //con.Open();
        //            MySqlCommand cmd = new MySqlCommand(_queryBackup, mysConeccion);

        //            cmd.ExecuteNonQuery();
        //            _isExecuted = true;
        //        }
        //        catch (Exception ex)
        //        {
        //            string _x = ex.Message;
        //        }
        //    }

        //    return _isExecuted;
        //}

        //public static void backup2()
        //{
        //    string constring = "server=localhost;user=root;pwd=qwerty;database=test;";

        //    // Important Additional Connection Options
        //    constring += "charset=utf8;convertzerodatetime=true;";

        //    string file = "C:\\backup.sql";

        //    using (MySqlConnection conn = new MySqlConnection(constring))
        //    {
        //        using (MySqlCommand cmd = new MySqlCommand())
        //        {
        //            using (MySqlBackup mb = new MySqlBackup(cmd))
        //            {
        //                cmd.Connection = conn;
        //                conn.Open();
        //                mb.ExportToFile(file);
        //                conn.Close();
        //            }
        //        }
        //    }
        //}
    }
}
