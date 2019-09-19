using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades.Sunat;
using SIAC_DATOS.Sunat;
using MySql.Data.MySqlClient;

namespace SIAC_Negocio.Sunat
{
    public class CN_sun_tipexi
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable Listar()
        {
            DataTable dtResul = new DataTable();

            CD_sun_tipexi miFun = new CD_sun_tipexi();
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
        public BE_SUN_TIPEXI TraerRegistro(int n_IdRegistro)
        {
            BE_SUN_TIPEXI entTipExi = new BE_SUN_TIPEXI();

            CD_sun_tipexi miFun = new CD_sun_tipexi();
            miFun.mysConec = mysConec;
            entTipExi = miFun.TraerRegistro(n_IdRegistro);

            return entTipExi;
        }
        public bool Insertar(BE_SUN_TIPEXI entTipExi)
        {
            BE_SUN_TIPEXI entNuevoTipExi = new BE_SUN_TIPEXI();
            CD_sun_tipexi miFun = new CD_sun_tipexi();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoTipExi.n_id = entTipExi.n_id;
            entNuevoTipExi.c_codsun = entTipExi.c_codsun;
            entNuevoTipExi.c_des = entTipExi.c_des;
            entNuevoTipExi.c_pre = entTipExi.c_pre;

            booOk = miFun.Insertar(entTipExi);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_SUN_TIPEXI entTipExi)
        {
            BE_SUN_TIPEXI entNuevoTipExi = new BE_SUN_TIPEXI();
            CD_sun_tipexi miFun = new CD_sun_tipexi();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoTipExi.n_id = entTipExi.n_id;
            entNuevoTipExi.c_codsun = entTipExi.c_codsun;
            entNuevoTipExi.c_des = entTipExi.c_des;
            entNuevoTipExi.c_pre = entTipExi.c_pre;

            booOk = miFun.Actualizar(entTipExi);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Eliminar(int n_Id)
        {
            CD_sun_tipexi miFun = new CD_sun_tipexi();
            bool booOk = false;

            miFun.mysConec = mysConec;

            booOk = miFun.Eliminar(n_Id);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
    }
}
