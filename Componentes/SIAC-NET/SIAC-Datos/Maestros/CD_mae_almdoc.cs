using System;
using Helper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_DATOS.Maestros
{
    public class CD_mae_almdoc
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        private DataTable _dtResult;

        // ***********************************************************************
        // n_tipmov = 1  FILTRARA SOLO LOS DOCUMENTOS QUE SEAN DE INGRESO 
        // n_tipmov = 2  FILTRARA SOLO LOS DOCUMENTOS QUE SEA DE SALIDA
        // ***********************************************************************
        public DataTable ListarEmpAlm(int n_idemp, int n_tipmov)
        {
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "SYSTEM.INT16", n_idemp.ToString()},
                                            {"n_tipmov", "SYSTEM.INT16", n_tipmov.ToString()}
                                      };

            _dtResult = xMiFuncion.StoreDTLLenar("mae_almdoc_listar", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {
                _dtResult = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
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
