using MySql.Data.MySqlClient;
using SIAC_DATOS.Almacen;
using SIAC_DATOS.Produccion;
using SIAC_DATOS.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using SIAC_Entidades.Produccion;

namespace SIAC_Negocio.Produccion
{
    public class CN_pro_tareas
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public BE_PRO_TAREA entTareas = new BE_PRO_TAREA();

        public DataTable Listar(int n_IdEmpresa, int n_EsUnificado)
        {
            DataTable dtResul = new DataTable();

            CD_pro_tareas miFun = new CD_pro_tareas();
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar(n_IdEmpresa, n_EsUnificado);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            DataTable dtResult = new DataTable();
            bool booResult;
            CD_pro_tareas miFun = new CD_pro_tareas();

            miFun.mysConec = mysConec;

            booResult = miFun.TraerRegistro(n_IdRegistro);
            dtResult = miFun.dtTarea;

            if (dtResult.Rows.Count != 0)
            {
                entTareas.n_idemp = Convert.ToInt32(dtResult.Rows[0]["n_idemp"]);
                entTareas.n_id = Convert.ToInt32(dtResult.Rows[0]["n_id"]);
                entTareas.c_cod = dtResult.Rows[0]["c_cod"].ToString();
                entTareas.c_des = dtResult.Rows[0]["c_des"].ToString();
                entTareas.n_idunimed = Convert.ToInt32(dtResult.Rows[0]["n_idunimed"]);
                entTareas.n_div = Convert.ToInt32(dtResult.Rows[0]["n_div"]);
                entTareas.c_abr = dtResult.Rows[0]["c_abr"].ToString();
                entTareas.c_obs = dtResult.Rows[0]["c_obs"].ToString();
                entTareas.n_pre = Convert.ToDouble(dtResult.Rows[0]["n_pre"]);
            }
            if (booResult == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool booResult = false;
            CD_pro_tareas miFun = new CD_pro_tareas();

            miFun.mysConec = mysConec;

            booResult = miFun.Eliminar(n_IdRegistro);
            if (booResult == false)
            {
                booOcurrioError = false;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return booResult;
        }
        public bool Insertar(BE_PRO_TAREA entTareas)
        {
            CD_pro_tareas miFun = new CD_pro_tareas();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(entTareas);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_PRO_TAREA entTareas)
        {
            CD_pro_tareas miFun = new CD_pro_tareas();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(entTareas);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
    }
}
