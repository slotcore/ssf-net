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
    public class CN_sun_unimed
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        public DataTable Listar()
        {
            DataTable dtResul = new DataTable();

            CD_sun_unimed miFun = new CD_sun_unimed();
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
        public BE_SUN_UNIMED TraerRegistro(int n_IdRegistro)
        {
            BE_SUN_UNIMED entAlmacen = new BE_SUN_UNIMED();

            CD_sun_unimed miFun = new CD_sun_unimed();
            miFun.mysConec = mysConec;
            entAlmacen = miFun.TraerRegistro(n_IdRegistro);

            return entAlmacen;
        }
        public bool Insertar(BE_SUN_UNIMED entUniMed)
        {
            BE_SUN_UNIMED entNuevoUniMed = new BE_SUN_UNIMED();
            CD_sun_unimed miFun = new CD_sun_unimed();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoUniMed.n_id = entUniMed.n_id;
            entNuevoUniMed.c_codsun = entUniMed.c_codsun;
            entNuevoUniMed.c_des = entUniMed.c_des;
            entNuevoUniMed.c_abr = entUniMed.c_abr;

            booOk = miFun.Insertar(entUniMed);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_SUN_UNIMED entUniMed)
        {
            BE_SUN_UNIMED entNuevoUniMed = new BE_SUN_UNIMED();
            CD_sun_unimed miFun = new CD_sun_unimed();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoUniMed.n_id = entUniMed.n_id;
            entNuevoUniMed.c_codsun = entUniMed.c_codsun;
            entNuevoUniMed.c_des = entUniMed.c_des;
            entNuevoUniMed.c_abr = entUniMed.c_abr;

            booOk = miFun.Actualizar(entUniMed);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Eliminar(int n_Id)
        {
            CD_sun_unimed miFun = new CD_sun_unimed();
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
