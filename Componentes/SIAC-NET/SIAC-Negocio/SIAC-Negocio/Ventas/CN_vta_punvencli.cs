using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades.Ventas;
using SIAC_DATOS.Ventas;
using MySql.Data.MySqlClient;

namespace SIAC_Negocio.Ventas
{
    public class CN_vta_punvencli
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable Listar()
        {
            DataTable dtResul = new DataTable();

            CD_vta_punvencli miFun = new CD_vta_punvencli();
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
        public DataTable Listar2(int n_TipoRegistro)
        {
            DataTable dtResul = new DataTable();

            CD_vta_punvencli miFun = new CD_vta_punvencli();
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar2(n_TipoRegistro);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public BE_VTA_PUNVENCLI TraerRegistro(int n_IdRegistro)
        {
            BE_VTA_PUNVENCLI entPunVenCli = new BE_VTA_PUNVENCLI();

            CD_vta_punvencli miFun = new CD_vta_punvencli();
            miFun.mysConec = mysConec;
            entPunVenCli = miFun.TraerRegistro(n_IdRegistro);

            return entPunVenCli;
        }
        public bool Insertar(BE_VTA_PUNVENCLI entPunVenCli)
        {
            BE_VTA_PUNVENCLI entNuevoPunVenCli = new BE_VTA_PUNVENCLI();
            CD_vta_punvencli miFun = new CD_vta_punvencli();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoPunVenCli.n_idemp = entPunVenCli.n_idemp;
            entNuevoPunVenCli.n_idcli = entPunVenCli.n_idcli;
            entNuevoPunVenCli.n_id = entPunVenCli.n_id;
            entNuevoPunVenCli.c_codcen = entPunVenCli.c_codcen;
            entNuevoPunVenCli.c_des = entPunVenCli.c_des;
            entNuevoPunVenCli.c_dir = entPunVenCli.c_dir;
            entNuevoPunVenCli.n_iddis = entPunVenCli.n_iddis;
            entNuevoPunVenCli.n_idpro = entPunVenCli.n_idpro;
            entNuevoPunVenCli.n_iddep = entPunVenCli.n_iddep;

            booOk = miFun.Insertar(entNuevoPunVenCli);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_VTA_PUNVENCLI entPunVenCli)
        {
            BE_VTA_PUNVENCLI entNuevoPunVenCli = new BE_VTA_PUNVENCLI();
            CD_vta_punvencli miFun = new CD_vta_punvencli();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoPunVenCli.n_idemp = entPunVenCli.n_idemp;
            entNuevoPunVenCli.n_idcli = entPunVenCli.n_idcli;
            entNuevoPunVenCli.n_id = entPunVenCli.n_id;
            entNuevoPunVenCli.c_codcen = entPunVenCli.c_codcen;
            entNuevoPunVenCli.c_des = entPunVenCli.c_des;
            entNuevoPunVenCli.c_dir = entPunVenCli.c_dir;
            entNuevoPunVenCli.n_iddis = entPunVenCli.n_iddis;
            entNuevoPunVenCli.n_idpro = entPunVenCli.n_idpro;
            entNuevoPunVenCli.n_iddep = entPunVenCli.n_iddep;

            booOk = miFun.Actualizar(entNuevoPunVenCli);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Eliminar(int n_Id)
        {
            CD_vta_punvencli miFun = new CD_vta_punvencli();
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
