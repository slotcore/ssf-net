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
    public class CN_sun_tipope
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable Listar()
        {
            DataTable dtResul = new DataTable();

            CD_sun_tipope miFun = new CD_sun_tipope();
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

        public DataTable ListarParaMovimientos(Int16 n_TipoMovimiento)
        {
            DataTable dtResul = new DataTable();

            CD_sun_tipope miFun = new CD_sun_tipope();
            miFun.mysConec = mysConec;

            dtResul = miFun.ListarParaMovimientos(n_TipoMovimiento);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        
        public BE_SUN_TIPOPE TraerRegistro(int n_IdRegistro)
        {
            BE_SUN_TIPOPE entTipOpe = new BE_SUN_TIPOPE();

            CD_sun_tipope miFun = new CD_sun_tipope();
            miFun.mysConec = mysConec;
            entTipOpe = miFun.TraerRegistro(n_IdRegistro);

            return entTipOpe;
        }
        public bool Insertar(BE_SUN_TIPOPE entTipExi)
        {
            BE_SUN_TIPOPE entNuevoTipExi = new BE_SUN_TIPOPE();
            CD_sun_tipope miFun = new CD_sun_tipope();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoTipExi.n_id = entTipExi.n_id;
            entNuevoTipExi.c_codsun = entTipExi.c_codsun;
            entNuevoTipExi.c_des = entTipExi.c_des;

            booOk = miFun.Insertar(entTipExi);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_SUN_TIPOPE entTipExi)
        {
            BE_SUN_TIPOPE entNuevoTipExi = new BE_SUN_TIPOPE();
            CD_sun_tipope miFun = new CD_sun_tipope();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoTipExi.n_id = entTipExi.n_id;
            entNuevoTipExi.c_codsun = entTipExi.c_codsun;
            entNuevoTipExi.c_des = entTipExi.c_des;

            booOk = miFun.Actualizar(entTipExi);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Eliminar(int n_Id)
        {
            CD_sun_tipope miFun = new CD_sun_tipope();
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
