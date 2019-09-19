using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades.Maestros;
using SIAC_Entidades.Sunat;
using SIAC_DATOS.Maestros;
using SIAC_DATOS.Sunat;
using MySql.Data.MySqlClient;

namespace SIAC_Negocio.Maestros
{
    public class CN_mae_familia
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        public DataTable Listar(int n_IdEmpresa, int n_EsUnificado)
        {
            DataTable dtResul = new DataTable();

            CD_mae_familia miFun = new CD_mae_familia();
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
        public BE_MAE_FAMILIA TraerRegistro(int n_IdRegistro)
        {
            BE_MAE_FAMILIA entFam = new BE_MAE_FAMILIA();

            CD_mae_familia miFun = new CD_mae_familia();
            miFun.mysConec = mysConec;
            entFam = miFun.TraerRegistro(n_IdRegistro);

            return entFam;
        }
        public bool Insertar(BE_MAE_FAMILIA entFamilia)
        {
            BE_MAE_FAMILIA entNuevoFamilia = new BE_MAE_FAMILIA();
            CD_mae_familia miFun = new CD_mae_familia();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoFamilia.n_idemp = entFamilia.n_idemp;
            entNuevoFamilia.n_idtipexi = entFamilia.n_idtipexi;
            entNuevoFamilia.n_id = entFamilia.n_id;
            entNuevoFamilia.c_des = entFamilia.c_des;
            entNuevoFamilia.c_pre = entFamilia.c_pre;

            booOk = miFun.Insertar(entNuevoFamilia);
            
            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_MAE_FAMILIA entFamilia)
        {
            BE_MAE_FAMILIA entNuevoFamilia = new BE_MAE_FAMILIA();
            CD_mae_familia miFun = new CD_mae_familia();
            bool booOk = false;
            
            miFun.mysConec = mysConec;

            entNuevoFamilia.n_idemp = entFamilia.n_idemp;
            entNuevoFamilia.n_idtipexi = entFamilia.n_idtipexi;
            entNuevoFamilia.n_id = entFamilia.n_id;
            entNuevoFamilia.c_des = entFamilia.c_des;
            entNuevoFamilia.c_pre = entFamilia.c_pre;

            booOk = miFun.Actualizar(entNuevoFamilia);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Eliminar(int n_Id)
        {
            CD_mae_familia miFun = new CD_mae_familia();
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
