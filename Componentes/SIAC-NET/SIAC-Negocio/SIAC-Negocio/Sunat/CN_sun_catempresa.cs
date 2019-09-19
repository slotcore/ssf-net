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
    public class CN_sun_catempresa
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable Listar()
        {
            DataTable dtResul = new DataTable();

            CD_sun_catempresa miFun = new CD_sun_catempresa();
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
        //public BE_SUN_TIPEXI TraerRegistro(int n_IdRegistro)
        //{
        //    BE_SUN_TIPEXI entTipExi = new BE_SUN_TIPEXI();

        //    CD_sun_tipexi miFun = new CD_sun_tipexi();
        //    miFun.mysConec = mysConec;
        //    entTipExi = miFun.TraerRegistro(n_IdRegistro);

        //    return entTipExi;
        //}
        public bool Insertar(BE_SUN_CATEMPRESA entCatEmpresa)
        {
            BE_SUN_CATEMPRESA entNuevoCatEmpresa = new BE_SUN_CATEMPRESA();
            CD_sun_catempresa miFun = new CD_sun_catempresa();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoCatEmpresa.n_id = entCatEmpresa.n_id;
            entNuevoCatEmpresa.c_codsun = entCatEmpresa.c_codsun;
            entNuevoCatEmpresa.c_des = entCatEmpresa.c_des;

            booOk = miFun.Insertar(entNuevoCatEmpresa);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_SUN_CATEMPRESA entCatEmpresa)
        {
            BE_SUN_CATEMPRESA entNuevoCatEmpresa = new BE_SUN_CATEMPRESA();
            CD_sun_catempresa miFun = new CD_sun_catempresa();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoCatEmpresa.n_id = entCatEmpresa.n_id;
            entNuevoCatEmpresa.c_codsun = entCatEmpresa.c_codsun;
            entNuevoCatEmpresa.c_des = entCatEmpresa.c_des;

            booOk = miFun.Actualizar(entNuevoCatEmpresa);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Eliminar(int n_Id)
        {
            CD_sun_catempresa miFun = new CD_sun_catempresa();
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
