using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helper;
using Helper.Comunes;
using System.ComponentModel;
using System.Data;
using SIAC_DATOS.Contabilidad;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace SIAC_Objetos
{
    public class Funciones
    {
        Genericas funDatos = new Genericas();
        public MySqlConnection mysConec = new MySqlConnection();

        public double ObtenerTC(DataTable DtTC, string c_Fecha)
        {
            double n_tc;
            DataTable dtResul = new DataTable();
            dtResul = funDatos.DataTableFiltrar(DtTC, "c_fecha = '" + c_Fecha + "'");
            if (dtResul.Rows.Count != 0)
            {
                n_tc = Convert.ToDouble(dtResul.Rows[0]["n_preven"]);
            }
            else
            {
                n_tc = 0;
            }
            return n_tc;
        }
        public bool EstadoPeriodo(int n_IdEmpresa, int n_IdMes, int n_IdModulo)
        {
            bool b_estado = false;
            DataTable dtresult = new DataTable();
            //CN_con_cerrarmes objcermes = new CN_con_cerrarmes();
            CD_con_cerrarmes objcermes = new CD_con_cerrarmes();
            objcermes.mysConec = mysConec;
            objcermes.Listar(n_IdEmpresa);
            dtresult = objcermes.dtLista;
            dtresult = funDatos.DataTableFiltrar(dtresult, "((n_idmod = " + n_IdModulo.ToString() + ") AND (n_idmes = " + n_IdMes.ToString() + ") AND (n_idemp = " + n_IdEmpresa.ToString() + "))");
            if (dtresult.Rows.Count != 0)
            {
                if (Convert.ToInt32(dtresult.Rows[0]["n_estado"]) == 1)
                {
                    b_estado = true;
                }
            }
            return b_estado;
        }
        public MySqlConnection AbrirConeccion()
        {
            MySqlConnection mysConeccion = new MySqlConnection();
            Helper.DatosMySql hlpFuncion = new Helper.DatosMySql();
            Cls_Seguridad objSeg = new Cls_Seguridad();
            Helper.Genericas miFun = new Helper.Genericas();

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

            c_serip = objSeg.Desencriptar(c_serip);
            c_nombd = objSeg.Desencriptar(c_nombd);
            c_usu = objSeg.Desencriptar(c_usu);
            c_pas = objSeg.Desencriptar(c_pas);

            c_sysnom = objSeg.Desencriptar(c_sysnom);
            c_sysver = objSeg.Desencriptar(c_sysver);
            c_sysnomabr = objSeg.Desencriptar(c_sysnomabr);
            c_puerto = objSeg.Desencriptar(c_puerto);

            mysConeccion = null;

            mysConeccion = hlpFuncion.ObtenerConexion(c_serip, c_nombd, c_usu, c_pas, c_puerto);

            if (hlpFuncion.booOcurrioError == true)
            {
                //MessageBox.Show("No se pudo abrir la BD, por el siguiente motivo: " + hlpFuncion.StrErrorMensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            return mysConeccion;
        }
    }
}
