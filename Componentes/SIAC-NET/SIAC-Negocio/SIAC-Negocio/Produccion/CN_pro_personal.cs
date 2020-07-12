using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades;
using SIAC_DATOS.Produccion;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Almacen;
using SIAC_Entidades.Produccion;

namespace SIAC_Negocio.Produccion
{
    public class CN_pro_personal
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();

        public DataTable dtPersonal = new DataTable();
        public int idEmpleado = 0;

        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();

        public bool Listar(int n_IdEmpresa)
        {
            bool b_Result = false;

            CD_pro_personal miFun = new CD_pro_personal();
            miFun.mysConec = mysConec;

            if (miFun.Listar(n_IdEmpresa) == true)
            {
                b_Result = true;
                dtPersonal = miFun.dtPersonal;
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return b_Result;
        }

        public bool ListarPorCargo(int n_IdEmpresa, int n_IdCargo)
        {
            bool b_Result = false;

            CD_pro_personal miFun = new CD_pro_personal();
            miFun.mysConec = mysConec;

            if (miFun.ListarPorCargo(n_IdEmpresa, n_IdCargo) == true)
            {
                b_Result = true;
                dtPersonal = miFun.dtPersonal;
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return b_Result;
        }

        public bool ObtenerEmpleado(int n_IdPersonal)
        {
            bool b_Result = false;

            CD_pro_personal miFun = new CD_pro_personal();
            miFun.mysConec = mysConec;

            if (miFun.ObtenerEmpleado(n_IdPersonal) == true)
            {
                b_Result = true;
                idEmpleado = Convert.ToInt32(miFun.dtPersonal.Rows[0]["n_idtra"]);
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return b_Result;
        }
    }
}
