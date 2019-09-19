using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Planillas;

namespace SIAC_DATOS.Planilla
{
    public class CD_pla_personal
    {
        private DataTable _dtLista;
        private DataTable _dtRegistro;

        private bool _b_OcurrioError;
        private string _c_ErrorMensaje;
        private int _n_ErrorNumber;

        private MySqlConnection _mysConec;

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();
        public DataTable dtLista
        {
            get { return _dtLista; }
            set { _dtLista = value; }
        }
        public DataTable dtRegistro
        {
            get { return _dtRegistro; }
            set { _dtRegistro = value; }
        }
        public bool b_OcurrioError
        {
            get { return _b_OcurrioError; }
            set { _b_OcurrioError = value; }
        }
        public string c_ErrorMensaje
        {
            get { return _c_ErrorMensaje; }
            set { _c_ErrorMensaje = value; }
        }
        public int n_ErrorNumber
        {
            get { return _n_ErrorNumber; }
            set { _n_ErrorNumber = value; }
        }
        public MySqlConnection mysConec
        {
            get { return _mysConec; }
            set { _mysConec = value; }
        }

        public bool Listar(int n_IdEmpresa)
        {
            bool b_result = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT32",n_IdEmpresa.ToString()},
                                      };
            dtLista = xMiFuncion.StoreDTLLenar("pla_personal_listar", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                b_result = true;
            }
            return b_result;
        }
        public bool TraerRegistro(Int64 n_IdRegistro)
        {
            bool b_result = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdRegistro.ToString()}
                                      };
            dtRegistro = xMiFuncion.StoreDTLLenar("pla_personal_obtenerregistro", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber == 0)
            {
                b_result = true;
            }
            else
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return b_result;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool booResult = false;
            // PARAMETROS PARA ELIMINAR LOS INSUMOS Y LAS TAREAS
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdRegistro.ToString()}
                                      };

            if (xMiFuncion.StoreEjecutar("pla_personal_delete", arrParametros, mysConec) == true)
            {
                booResult = true;
            }
            else
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return booResult;
        }
        public bool Insertar(BE_PLA_PERSONAL entPersonalU)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            int n_row = 0;

            if (xMiFuncion.StoreEjecutar("pla_personal_insertar", entPersonalU, mysConec, 1) == true)
            {
                booOk = true;
            }
            else
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booOk;
        }
        public bool Actualizar(BE_PLA_PERSONAL entPersonalU)
        {
            bool booOk = false;
            int n_row = 0;
            DatosMySql xMiFuncion = new DatosMySql();

            if (xMiFuncion.StoreEjecutar("pla_personal_actualizar", entPersonalU, mysConec, null) == true)
            {
                booOk = true;
            }
            else
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booOk;
        }
    }
}
