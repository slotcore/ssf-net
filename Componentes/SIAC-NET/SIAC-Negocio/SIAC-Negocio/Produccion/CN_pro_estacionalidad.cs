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
    public class CN_pro_estacionalidad
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public BE_PRO_ESTACIONALIDAD entEstacion = new BE_PRO_ESTACIONALIDAD();
        public DataTable dtLista = new DataTable();

        public DataTable Listar(int n_IdEmpresa, int n_EsUnificado)
        {
            DataTable dtResul = new DataTable();

            CD_pro_estacionalidad miFun = new CD_pro_estacionalidad();
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
            CD_pro_estacionalidad miFun = new CD_pro_estacionalidad();

            miFun.mysConec = mysConec;

            booResult = miFun.TraerRegistro(n_IdRegistro);
            dtResult = miFun.dtTarea;

            if (dtResult.Rows.Count != 0)
            {
                entEstacion.n_idemp = Convert.ToInt32(dtResult.Rows[0]["n_idemp"]);
                entEstacion.n_id = Convert.ToInt32(dtResult.Rows[0]["n_id"]);
                entEstacion.n_idite = Convert.ToInt32(dtResult.Rows[0]["n_idite"]);
                entEstacion.n_idmp = Convert.ToInt32(dtResult.Rows[0]["n_idmp"]);
                entEstacion.c_des = dtResult.Rows[0]["c_des"].ToString();
                entEstacion.n_ene = Convert.ToInt32(dtResult.Rows[0]["n_ene"]);
                entEstacion.n_feb = Convert.ToInt32(dtResult.Rows[0]["n_feb"]);
                entEstacion.n_mar = Convert.ToInt32(dtResult.Rows[0]["n_mar"]);
                entEstacion.n_abr = Convert.ToInt32(dtResult.Rows[0]["n_abr"]);
                entEstacion.n_may = Convert.ToInt32(dtResult.Rows[0]["n_may"]);
                entEstacion.n_jun = Convert.ToInt32(dtResult.Rows[0]["n_jun"]);
                entEstacion.n_jul = Convert.ToInt32(dtResult.Rows[0]["n_jul"]);
                entEstacion.n_ago = Convert.ToInt32(dtResult.Rows[0]["n_ago"]);
                entEstacion.n_set = Convert.ToInt32(dtResult.Rows[0]["n_set"]);
                entEstacion.n_oct = Convert.ToInt32(dtResult.Rows[0]["n_oct"]);
                entEstacion.n_nov = Convert.ToInt32(dtResult.Rows[0]["n_nov"]);
                entEstacion.n_dic = Convert.ToInt32(dtResult.Rows[0]["n_dic"]);
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
            CD_pro_estacionalidad miFun = new CD_pro_estacionalidad();

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
        public bool Insertar(BE_PRO_ESTACIONALIDAD entEstacionalidad)
        {
            CD_pro_estacionalidad miFun = new CD_pro_estacionalidad();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(entEstacionalidad);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_PRO_ESTACIONALIDAD entEstacionalidad)
        {
            CD_pro_estacionalidad miFun = new CD_pro_estacionalidad();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(entEstacionalidad);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public void Consulta1()
        {
            bool b_Result = false;

            CD_pro_estacionalidad miFun = new CD_pro_estacionalidad();
            miFun.mysConec = mysConec;

            b_Result = miFun.Consulta1();
            if (b_Result == true)
            {
                dtLista = miFun.dtLista;
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return;
        }
    }
}
