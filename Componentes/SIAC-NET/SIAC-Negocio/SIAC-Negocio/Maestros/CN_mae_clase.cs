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
    public class CN_mae_clase
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        public DataTable Listar(int n_IdEmpresa, int n_EsUnificado)
        {
            DataTable dtResul = new DataTable();

            CD_mae_clase miFun = new CD_mae_clase();
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
        public BE_MAE_CLASE TraerRegistro(int n_IdRegistro)
        {
            BE_MAE_CLASE entAlmacen = new BE_MAE_CLASE();

            CD_mae_clase miFun = new CD_mae_clase();
            miFun.mysConec = mysConec;
            entAlmacen = miFun.TraerRegistro(n_IdRegistro);

            return entAlmacen;
        }
        public bool Insertar(BE_MAE_CLASE entClase)
        {
            BE_MAE_CLASE entNuevoClase = new BE_MAE_CLASE();
            CD_mae_clase miFun = new CD_mae_clase();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoClase.n_idemp = entClase.n_idemp;
            entNuevoClase.n_idtipexi = entClase.n_idtipexi;
            entNuevoClase.n_idfam = entClase.n_idfam;
            entNuevoClase.n_id = entClase.n_id;
            entNuevoClase.c_des = entClase.c_des;
            entNuevoClase.c_pre = entClase.c_pre;

            booOk = miFun.Insertar(entNuevoClase);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_MAE_CLASE entClase)
        {
            BE_MAE_CLASE entNuevoClase = new BE_MAE_CLASE();
            CD_mae_clase miFun = new CD_mae_clase();
            bool booOk = false;
            
            miFun.mysConec = mysConec;

            entNuevoClase.n_idemp = entClase.n_idemp;
            entNuevoClase.n_idtipexi = entClase.n_idtipexi;
            entNuevoClase.n_idfam = entClase.n_idfam;
            entNuevoClase.n_id = entClase.n_id;
            entNuevoClase.c_des = entClase.c_des;
            entNuevoClase.c_pre = entClase.c_pre;

            booOk = miFun.Actualizar(entNuevoClase);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Eliminar(int n_Id)
        {
            CD_mae_clase miFun = new CD_mae_clase();
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
