using MySql.Data.MySqlClient;
using SIAC_DATOS.Tesoreria;
using SIAC_DATOS.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using SIAC_Entidades.Tesoreria;

namespace SIAC_Negocio.Tesoreria
{
    public class CN_tes_bancos
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public BE_TES_BANCOS entBancos = new BE_TES_BANCOS();

        public DataTable Listar()
        {
            DataTable dtResul = new DataTable();

            CD_tes_bancos miFun = new CD_tes_bancos();
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar();

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
            CD_tes_bancos miFun = new CD_tes_bancos();

            miFun.mysConec = mysConec;

            booResult = miFun.TraerRegistro(n_IdRegistro);
            dtResult = miFun.dtTarea;

            if (dtResult.Rows.Count != 0)
            {
                entBancos.n_id = Convert.ToInt16(dtResult.Rows[0]["n_id"]);
                entBancos.c_des = dtResult.Rows[0]["c_des"].ToString();
                entBancos.c_codsun = dtResult.Rows[0]["c_codsun"].ToString();
                entBancos.c_abr = dtResult.Rows[0]["c_abr"].ToString();
                entBancos.c_numruc = dtResult.Rows[0]["c_numruc"].ToString();
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
            CD_tes_bancos miFun = new CD_tes_bancos();

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
        public bool Insertar(BE_TES_BANCOS entBancos)
        {
            CD_tes_bancos miFun = new CD_tes_bancos();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(entBancos);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_TES_BANCOS entBancos)
        {
            CD_tes_bancos miFun = new CD_tes_bancos();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(entBancos);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
    }
}
