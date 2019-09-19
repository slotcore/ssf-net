using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Contabilidad;
using SIAC_DATOS.Contabilidad;

namespace SIAC_Negocio.Contabilidad
{
    public class CN_con_impuestos
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtLista = new DataTable();
        public BE_CON_IMPUESTOS e_Impuestos = new BE_CON_IMPUESTOS();
        public void Listar(int n_IdEmpresa)
        {
            CD_con_impuestos miFun = new CD_con_impuestos();
            miFun.mysConec = mysConec;

            miFun.Listar(n_IdEmpresa);
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
            CD_con_impuestos miFun = new CD_con_impuestos();
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
                e_Impuestos.n_id = Convert.ToInt32(dtLista.Rows[0]["n_id"]);
                e_Impuestos.c_des = dtLista.Rows[0]["c_des"].ToString();
                e_Impuestos.n_tasa = Convert.ToInt32(dtLista.Rows[0]["n_tasa"]);
                e_Impuestos.n_idcue = Convert.ToInt32(dtLista.Rows[0]["n_idcue"]);
                e_Impuestos.n_idcuevta = Convert.ToInt32(dtLista.Rows[0]["n_idcuevta"]);
                e_Impuestos.c_abr = dtLista.Rows[0]["c_abr"].ToString();
            }
            return;
        }
        public bool Eliminar(int n_Idregistro)
        {
            bool b_result = false;
            CD_con_impuestos miFun = new CD_con_impuestos();

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
        public bool Insertar(BE_CON_IMPUESTOS e_Impuestos)
        {
            bool b_result = false;
            CD_con_impuestos miFun = new CD_con_impuestos();

            miFun.mysConec = mysConec;
            b_result = miFun.Insertar(e_Impuestos);
            if (b_result == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return b_result;
        }
        public bool Actualizar(BE_CON_IMPUESTOS e_Impuestos)
        {
            bool b_result = false;
            CD_con_impuestos miFun = new CD_con_impuestos();

            miFun.mysConec = mysConec;
            b_result = miFun.Actualizar(e_Impuestos);
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
