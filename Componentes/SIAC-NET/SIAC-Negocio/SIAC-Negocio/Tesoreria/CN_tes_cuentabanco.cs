using MySql.Data.MySqlClient;
using SIAC_DATOS.Tesoreria;
using SIAC_DATOS.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using SIAC_Entidades.Tesoreria;

namespace SIAC_Negocio.Tesoreria
{
    public class CN_tes_cuentabanco
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public BE_TES_CUENTABANCO entBancos = new BE_TES_CUENTABANCO();

        public DataTable Listar(int n_IdEmpresa)
        {
            DataTable dtResul = new DataTable();

            CD_tes_cuentabanco miFun = new CD_tes_cuentabanco();
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar(n_IdEmpresa);

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
            CD_tes_cuentabanco miFun = new CD_tes_cuentabanco();

            miFun.mysConec = mysConec;

            booResult = miFun.TraerRegistro(n_IdRegistro);
            dtResult = miFun.dtTarea;

            if (dtResult.Rows.Count != 0)
            {
                entBancos.n_id = Convert.ToInt16(dtResult.Rows[0]["n_id"]);
                entBancos.n_idemp = Convert.ToInt16(dtResult.Rows[0]["n_idemp"]);
                entBancos.n_idban = Convert.ToInt16(dtResult.Rows[0]["n_idban"]);
                entBancos.c_numcue = dtResult.Rows[0]["c_numcue"].ToString();
                entBancos.n_idmon = Convert.ToInt16(dtResult.Rows[0]["n_idmon"]);
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
            CD_tes_cuentabanco miFun = new CD_tes_cuentabanco();

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
        public bool Insertar(BE_TES_CUENTABANCO e_CuentaBanco)
        {
            CD_tes_cuentabanco miFun = new CD_tes_cuentabanco();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(e_CuentaBanco);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_TES_CUENTABANCO e_CuentaBanco)
        {
            CD_tes_cuentabanco miFun = new CD_tes_cuentabanco();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(e_CuentaBanco);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
    }
}
