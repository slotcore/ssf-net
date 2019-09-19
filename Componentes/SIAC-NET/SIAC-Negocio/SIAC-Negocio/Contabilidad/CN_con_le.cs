using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades;
using SIAC_DATOS.Contabilidad;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Contabilidad;
using SIAC_Objetos.Constantes;
using SIAC_Objetos.Sistema;
using Helper;

namespace SIAC_Negocio.Contabilidad
{
    public class CN_con_le
    {
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public string c_RutaSalida;
        public MySqlConnection mysConec = new MySqlConnection();
        
        public DataTable dtLista = new DataTable();
        Cls_IO funIO = new Cls_IO();
        CD_con_le miFun = new CD_con_le();
        string c_nomarc;

        public bool LE_81(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo)
        {
            bool b_result = false;
            miFun.mysConec = mysConec;
            miFun.LE_81(n_IdEmpresa, n_AnoTrabajo, n_MesTrabajo, 8);
            dtLista = miFun.dtLista;
            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_result;
            }
            if (dtLista.Rows.Count != 0)
            {
                c_nomarc = "LE" + STU_SISTEMA.EMPRESARUC + n_AnoTrabajo.ToString() + n_MesTrabajo.ToString("00")+"00080100"+"001111.txt";
                funIO.Fil_GenerarTxt(dtLista, c_nomarc, "C:\\SSF-NET", 0);
                c_RutaSalida = "C:\\SSF-NET\\" + c_nomarc;

                DataTable dtLis = new DataTable();
                c_nomarc = "LE" + STU_SISTEMA.EMPRESARUC + n_AnoTrabajo.ToString() + n_MesTrabajo.ToString("00") + "00080200" + "001011.txt";
                funIO.Fil_GenerarTxt(dtLis, c_nomarc, "C:\\SSF-NET", 0);
                c_RutaSalida = "C:\\SSF-NET\\" + c_nomarc;
            }
            b_result = true;
            return b_result;
        }
        public bool LE_82(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo)
        {
            bool b_result = false;
            miFun.mysConec = mysConec;
            miFun.LE_82(n_IdEmpresa, n_AnoTrabajo, n_MesTrabajo, 8);
            dtLista = miFun.dtLista;
            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            if (dtLista.Rows.Count != 0)
            {
                c_nomarc = "LE" + STU_SISTEMA.EMPRESARUC + n_AnoTrabajo.ToString() + n_MesTrabajo.ToString("00") + "00080200" + "001111.txt";
                funIO.Fil_GenerarTxt(dtLista, c_nomarc, "C:\\SSF-NET", 2);
                c_RutaSalida = "C:\\SSF-NET\\" + c_nomarc;
            }
            return b_result;
        }
        public bool LE_141(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo)
        {
            bool b_result = false;
            miFun.mysConec = mysConec;
            miFun.LE_141(n_IdEmpresa, n_AnoTrabajo, n_MesTrabajo, 14);
            dtLista = miFun.dtLista;
            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_result;
            }
            if (dtLista.Rows.Count != 0)
            {
                c_nomarc = "LE" + STU_SISTEMA.EMPRESARUC + n_AnoTrabajo.ToString() + n_MesTrabajo.ToString("00") + "00140100" + "001111.txt";
                funIO.Fil_GenerarTxt(dtLista, c_nomarc, "C:\\SSF-NET", 0);
                c_RutaSalida = "C:\\SSF-NET\\" + c_nomarc;
            }
            b_result = true;
            return b_result;
        }
    }
}
