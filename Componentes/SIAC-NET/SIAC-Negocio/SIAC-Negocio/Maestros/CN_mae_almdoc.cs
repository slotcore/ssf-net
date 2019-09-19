using MySql.Data.MySqlClient;
using SIAC_DATOS.Maestros;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Negocio.Maestros
{
    public class CN_mae_almdoc
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        
        private DataTable _dtResult;
        public DataTable ListarEmpAlm(int n_idemp, int n_tipmov)
        {
            CD_mae_almdoc miFun = new CD_mae_almdoc();
            miFun.mysConec = mysConec;

            _dtResult = miFun.ListarEmpAlm(n_idemp, n_tipmov);

            if (_dtResult == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return _dtResult;
        }
        public DataTable dtResult
        {
            get { return _dtResult; }
            set { _dtResult = value; }
        }
    }
}
