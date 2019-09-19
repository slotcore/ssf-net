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
    public class CN_vta_emptra
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        public DataTable Listar(int n_IdEmp)
        {
            DataTable dtResul = new DataTable();

            CD_vta_emptra miFun = new CD_vta_emptra();
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
        public BE_VTA_EMPTRA TraerRegistro(int n_IdRegistro)
        {
            BE_VTA_EMPTRA entEmpTra = new BE_VTA_EMPTRA();

            CD_vta_emptra miFun = new CD_vta_emptra();
            miFun.mysConec = mysConec;
            entEmpTra = miFun.TraerRegistro(n_IdRegistro);

            return entEmpTra;
        }
        public bool Insertar(BE_VTA_EMPTRA entEmpTra)
        {
            BE_VTA_EMPTRA entNuevoEmpTra = new BE_VTA_EMPTRA();
            CD_vta_emptra miFun = new CD_vta_emptra();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoEmpTra.n_idemp = entEmpTra.n_idemp;
            entNuevoEmpTra.n_id = entEmpTra.n_id;
            entNuevoEmpTra.n_idpro = entEmpTra.n_idpro;

            booOk = miFun.Insertar(entNuevoEmpTra);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_VTA_EMPTRA entEmpTra)
        {
            BE_VTA_EMPTRA entNuevoEmpTra = new BE_VTA_EMPTRA();
            CD_vta_emptra miFun = new CD_vta_emptra();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoEmpTra.n_idemp = entEmpTra.n_idemp;
            entNuevoEmpTra.n_id = entEmpTra.n_id;
            entNuevoEmpTra.n_idpro = entEmpTra.n_idpro;

            booOk = miFun.Actualizar(entNuevoEmpTra);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Eliminar(int n_Id)
        {
            CD_vta_emptra miFun = new CD_vta_emptra();
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
