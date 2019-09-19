using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades.Maestros;
using SIAC_DATOS.Maestros;
using MySql.Data.MySqlClient;


namespace SIAC_Negocio.Maestros
{
    public class CN_mae_subclase
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        public DataTable Listar(int n_IdEmpresa, int n_EsUnificado)
        {
            DataTable dtResul = new DataTable();

            CD_mae_subclase miFun = new CD_mae_subclase();
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar(n_IdEmpresa, n_EsUnificado);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public BE_MAE_SUBCLASE TraerRegistro(int n_IdRegistro)
        {
            BE_MAE_SUBCLASE entSubClase = new BE_MAE_SUBCLASE();

            CD_mae_subclase miFun = new CD_mae_subclase();
            miFun.mysConec = mysConec;
            entSubClase = miFun.TraerRegistro(n_IdRegistro);

            return entSubClase;
        }
        public bool Insertar(BE_MAE_SUBCLASE entSubClase)
        {
            BE_MAE_SUBCLASE entNuevoSubClase = new BE_MAE_SUBCLASE();
            CD_mae_subclase miFun = new CD_mae_subclase();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoSubClase.n_idemp = entSubClase.n_idemp;
            entNuevoSubClase.n_idtipexi = entSubClase.n_idtipexi;
            entNuevoSubClase.n_idfam = entSubClase.n_idfam;
            entNuevoSubClase.n_idcla = entSubClase.n_idcla;
            entNuevoSubClase.n_id = entSubClase.n_id;
            entNuevoSubClase.c_des = entSubClase.c_des;
            entNuevoSubClase.c_pre = entSubClase.c_pre;

            booOk = miFun.Insertar(entNuevoSubClase);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_MAE_SUBCLASE entSubClase)
        {
            BE_MAE_SUBCLASE entNuevoSubClase = new BE_MAE_SUBCLASE();
            CD_mae_subclase miFun = new CD_mae_subclase();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoSubClase.n_idemp = entSubClase.n_idemp;
            entNuevoSubClase.n_idtipexi = entSubClase.n_idtipexi;
            entNuevoSubClase.n_idfam = entSubClase.n_idfam;
            entNuevoSubClase.n_idcla = entSubClase.n_idcla;
            entNuevoSubClase.n_id = entSubClase.n_id;
            entNuevoSubClase.c_des = entSubClase.c_des;
            entNuevoSubClase.c_pre = entSubClase.c_pre;

            booOk = miFun.Actualizar(entNuevoSubClase);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Eliminar(int n_Id)
        {
            CD_mae_subclase miFun = new CD_mae_subclase();
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
