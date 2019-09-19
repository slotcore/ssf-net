using MySql.Data.MySqlClient;
using SIAC_DATOS.Produccion;
using System;
using System.Collections.Generic;
using System.Data;
using SIAC_Entidades.Produccion;

namespace SIAC_Negocio.Produccion
{
    public class CN_pro_tipoproducto
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;

        public DataTable dtLista = new DataTable();
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public BE_PRO_TIPOPRODUCTO entTipProducto = new BE_PRO_TIPOPRODUCTO();
        public bool Listar(int n_idempresa)
        {
            bool b_result = false;
            DataTable dtResul = new DataTable();

            CD_pro_tipoproducto miFun = new CD_pro_tipoproducto();
            miFun.mysConec = mysConec;

            b_result = miFun.Listar(n_idempresa);

            if (b_result == true)
            {
                dtLista = miFun.dtLista;
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return b_result;
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            DataTable dtResult = new DataTable();
            bool booResult;
            CD_pro_tipoproducto miFun = new CD_pro_tipoproducto();

            miFun.mysConec = mysConec;

            booResult = miFun.TraerRegistro(n_IdRegistro);
            dtResult = miFun.dtRegistro;

            if (dtResult.Rows.Count != 0)
            {
                entTipProducto.n_idemp = Convert.ToInt16(dtResult.Rows[0]["n_idemp"]);
                entTipProducto.n_id = Convert.ToInt16(dtResult.Rows[0]["n_id"]);
                entTipProducto.c_des = dtResult.Rows[0]["c_des"].ToString();
              
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
            CD_pro_tipoproducto miFun = new CD_pro_tipoproducto();

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
        public bool Insertar(BE_PRO_TIPOPRODUCTO entTipoProducto)
        {
            CD_pro_tipoproducto miFun = new CD_pro_tipoproducto();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(entTipoProducto);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_PRO_TIPOPRODUCTO entTipoProducto)
        {
            CD_pro_tipoproducto miFun = new CD_pro_tipoproducto();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(entTipoProducto);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
    }
}
