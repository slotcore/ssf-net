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
    public class CN_vta_chofer
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        public DataTable Listar(int n_IdEmp)
        {
            DataTable dtResul = new DataTable();

            CD_vta_chofer miFun = new CD_vta_chofer();
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar(n_IdEmp);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public BE_VTA_CHOFER TraerRegistro(int n_IdRegistro)
        {
            BE_VTA_CHOFER entChofer = new BE_VTA_CHOFER();

            CD_vta_chofer miFun = new CD_vta_chofer();
            miFun.mysConec = mysConec;
            entChofer = miFun.TraerRegistro(n_IdRegistro);

            return entChofer;
        }
        public bool Insertar(BE_VTA_CHOFER entChofer)
        {
            BE_VTA_CHOFER entNuevoChofer = new BE_VTA_CHOFER();
            CD_vta_chofer miFun = new CD_vta_chofer();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoChofer.n_idemp = entChofer.n_idemp;
            entNuevoChofer.n_idper = entChofer.n_idper;
            entNuevoChofer.n_id = entChofer.n_id;
            entNuevoChofer.n_idveh = entChofer.n_idveh;
            entNuevoChofer.c_numbre = entChofer.c_numbre;
            entNuevoChofer.c_cat = entChofer.c_cat;

            booOk = miFun.Insertar(entNuevoChofer);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_VTA_CHOFER entChofer)
        {
            BE_VTA_CHOFER entNuevoChofer = new BE_VTA_CHOFER();
            CD_vta_chofer miFun = new CD_vta_chofer();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoChofer.n_idemp = entChofer.n_idemp;
            entNuevoChofer.n_idper = entChofer.n_idper;
            entNuevoChofer.n_id = entChofer.n_id;
            entNuevoChofer.n_idveh = entChofer.n_idveh;
            entNuevoChofer.c_numbre = entChofer.c_numbre;
            entNuevoChofer.c_cat = entChofer.c_cat;

            booOk = miFun.Actualizar(entNuevoChofer);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Eliminar(int n_Id)
        {
            CD_vta_chofer miFun = new CD_vta_chofer();
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
