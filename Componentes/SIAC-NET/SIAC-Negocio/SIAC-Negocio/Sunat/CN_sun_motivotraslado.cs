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
    public class CN_sun_motivotraslado
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable Listar()
        {
            DataTable dtResul = new DataTable();

            CD_sun_motivotraslado miFun = new CD_sun_motivotraslado();
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
        public BE_SUN_MOTIVOTRASLADO TraerRegistro(int n_IdRegistro)
        {
            BE_SUN_MOTIVOTRASLADO entMotTra = new BE_SUN_MOTIVOTRASLADO();

            CD_sun_motivotraslado miFun = new CD_sun_motivotraslado();
            miFun.mysConec = mysConec;
            entMotTra = miFun.TraerRegistro(n_IdRegistro);

            return entMotTra;
        }
        public bool Insertar(BE_SUN_MOTIVOTRASLADO entMotivoTraslado)
        {
            BE_SUN_MOTIVOTRASLADO entNuevoMotivoTraslado = new BE_SUN_MOTIVOTRASLADO();
            CD_sun_motivotraslado miFun = new CD_sun_motivotraslado();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoMotivoTraslado.n_id = entMotivoTraslado.n_id;
            entNuevoMotivoTraslado.c_codsun = entMotivoTraslado.c_codsun;
            entNuevoMotivoTraslado.c_des = entMotivoTraslado.c_des;

            booOk = miFun.Insertar(entNuevoMotivoTraslado);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_SUN_MOTIVOTRASLADO entMotivoTraslado)
        {
            BE_SUN_MOTIVOTRASLADO entNuevoMotivoTraslado = new BE_SUN_MOTIVOTRASLADO();
            CD_sun_motivotraslado miFun = new CD_sun_motivotraslado();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoMotivoTraslado.n_id = entMotivoTraslado.n_id;
            entNuevoMotivoTraslado.c_codsun = entMotivoTraslado.c_codsun;
            entNuevoMotivoTraslado.c_des = entMotivoTraslado.c_des;

            booOk = miFun.Actualizar(entNuevoMotivoTraslado);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Eliminar(int n_Id)
        {
            CD_sun_motivotraslado miFun = new CD_sun_motivotraslado();
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
