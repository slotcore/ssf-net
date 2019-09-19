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

namespace SIAC_Negocio.Contabilidad
{
    public class CN_con_tc
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtLista = new DataTable();

        public BE_CON_TC e_Tc = new BE_CON_TC();

        public void Listartcano(int intMoneda, string c_AnoTrabajo)
        {
            CD_con_tc miFun = new CD_con_tc();
            miFun.mysConec = mysConec;

            miFun.Listartcano(intMoneda, c_AnoTrabajo);

            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            dtLista = miFun.dtLista;
            return;
        }
        public void TraerTC(string c_Fecha, int n_IdMoneda)
        {
            CD_con_tc miFun = new CD_con_tc();
            miFun.mysConec = mysConec;

            miFun.TraerTC(c_Fecha, n_IdMoneda);
            dtLista = miFun.dtLista;
            if (dtLista == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return;
        }
        public void Listar(int n_AñoTrabajo, int n_MesTrabajo)
        {
            CD_con_tc miFun = new CD_con_tc();
            miFun.mysConec = mysConec;

            miFun.Listar(n_AñoTrabajo, n_MesTrabajo);
            dtLista = miFun.dtLista;
            if (dtLista == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return;
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            CD_con_tc miFun = new CD_con_tc();
            miFun.mysConec = mysConec;

            miFun.TraerRegistro(n_IdRegistro);
            dtLista = miFun.dtLista;
            if (dtLista == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            else
            { 
                e_Tc.n_id = Convert.ToInt32(dtLista.Rows[0]["n_id"]);
                e_Tc.d_fecha = Convert.ToDateTime(dtLista.Rows[0]["d_fecha"]);
                e_Tc.n_idmon = Convert.ToInt32(dtLista.Rows[0]["n_idmon"]);
                e_Tc.n_impcom = Convert.ToDouble(dtLista.Rows[0]["n_impcom"]);
                e_Tc.n_impven = Convert.ToDouble(dtLista.Rows[0]["n_impven"]);
            }
            return;
        }
        public bool Eliminar(int n_Idregistro)
        {
            bool b_result = false;
            CD_con_tc miFun = new CD_con_tc();

            miFun.mysConec = mysConec;
            b_result = miFun.Eliminar(n_Idregistro);
            if (b_result == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return b_result;
        }
        public bool Insertar(BE_CON_TC e_TipoCambio)
        {
            bool b_result = false;
            CD_con_tc miFun = new CD_con_tc();

            miFun.mysConec = mysConec;
            b_result = miFun.Insertar(e_TipoCambio);
            if (b_result == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return b_result;
        }
        public bool Actualizar(BE_CON_TC e_TipoCambio)
        {
            bool b_result = false;
            CD_con_tc miFun = new CD_con_tc();

            miFun.mysConec = mysConec;
            b_result = miFun.Actualizar(e_TipoCambio);
            if (b_result == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return b_result;
        }
    }
}
