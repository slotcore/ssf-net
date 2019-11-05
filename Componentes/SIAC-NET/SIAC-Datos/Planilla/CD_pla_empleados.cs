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
    public class CD_pla_empleados
    {
        private DataTable _dtLista;
        private DataTable _dtRegistro;
        private DataTable _dtRegistroDet;

        private bool _b_OcurrioError;
        private string _c_ErrorMensaje;
        private int _n_ErrorNumber;

        private MySqlConnection mysConec;

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();
        
        public CD_pla_empleados(string c_IPServidor, string c_NombreBD, string c_Usuario, string c_Passord, string c_Puerto)
        {
            MySqlConnection mysConectar = new MySqlConnection();
            Helper.DatosMySql hlpFuncion = new Helper.DatosMySql();

            mysConec = hlpFuncion.ObtenerConexion(c_IPServidor, c_NombreBD, c_Usuario, c_Passord, c_Puerto);

            if (hlpFuncion.booOcurrioError == true)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
        }
        ~CD_pla_empleados()
        {
            mysConec.Close();
            mysConec = null; 
        }
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
        public DataTable dtRegistroDet
        {
            get { return _dtRegistroDet; }
            set { _dtRegistroDet = value; }
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
        //public MySqlConnection mysConec
        //{
        //    get { return _mysConec; }
        //    set { _mysConec = value; }
        //}

        public bool Listar(int n_IdEmpresa)
        {
            bool b_result = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT32",n_IdEmpresa.ToString()},
                                      };
            dtLista = xMiFuncion.StoreDTLLenar("pla_empleados_consulta2", arrParametros, mysConec);

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
        public bool ListarDetallado(int n_IdEmpresa)
        {
            bool b_result = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT32",n_IdEmpresa.ToString()},
                                      };
            dtLista = xMiFuncion.StoreDTLLenar("pla_empleados_jc_consulta", arrParametros, mysConec);

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
            dtRegistro = xMiFuncion.StoreDTLLenar("pla_empleados_obtenerregistro", arrParametros, mysConec);
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

            if (xMiFuncion.StoreEjecutar("pla_empleados_delete", arrParametros, mysConec) == true)
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
        public bool Insertar(BE_PLA_EMPLEADOS entPersonalU)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();

            if (xMiFuncion.StoreEjecutar("pla_empleados_insertar", entPersonalU, mysConec, 1) == true)
            {
                entPersonalU.n_id = Convert.ToInt32(xMiFuncion.intIdGenerado);
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
        public bool Actualizar(BE_PLA_EMPLEADOS entPersonalU)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();

            if (xMiFuncion.StoreEjecutar("pla_empleados_actualizar", entPersonalU, mysConec, null) == true)
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
        public bool Consulta1(int n_IdEmpresa, string c_CondicionIN)
        {
            bool b_result = false;
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT32",n_IdEmpresa.ToString()},
                                            {"c_con", "System.STRING",c_CondicionIN.ToString()},
                                      };
            dtLista = xMiFuncion.StoreDTLLenar("pla_empleados_consulta1", arrParametros, mysConec);

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
        public bool Desactivar(int n_IdRegistro, int n_Estado)
        {
            bool booResult = false;
            // PARAMETROS PARA ELIMINAR LOS INSUMOS Y LAS TAREAS
            string[,] arrParametros = new string[2, 3] {
                                            {"n_id", "System.INT32", n_IdRegistro.ToString()},
                                            {"n_estado", "System.INT32", n_Estado.ToString()}
                                      };

            if (xMiFuncion.StoreEjecutar("pla_empleados_desactivar", arrParametros, mysConec) == true)
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
        public bool Formato2(string c_FechaInicio, string c_FechaFinal, int n_Porcentaje)
        {
            bool b_result = false;
            string[,] arrParametros = new string[3, 3] {
                                            {"c_fchini", "System.STRING",c_FechaInicio.ToString()},
                                            {"c_fchfin", "System.STRING",c_FechaFinal.ToString()},
                                            {"n_capmax", "System.INT32",n_Porcentaje.ToString()}
                                      };
            	
            dtLista = xMiFuncion.StoreDTLLenar("pla_empleados_consulta7", arrParametros, mysConec);

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
        public bool ListaActivos()
        {
            bool b_result = false;
            string[,] arrParametros = new string[0, 0] {
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("pla_empleados_consulta9", arrParametros, mysConec);

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
        public bool DarBajaEmpleado(int n_Idregistro, string c_FechaBaja)
        {
            bool b_result = false;
            string[,] arrParametros = new string[2, 3] {
                                            {"n_id", "System.INT32",n_Idregistro.ToString()},
                                            {"c_fchpag", "System.STRING",c_FechaBaja.ToString()},
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("pla_empleados_darbaja", arrParametros, mysConec);

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
        public bool ListarAsistenciaDec(string c_FechaInicio, string c_FechaFInal)
        {
            bool b_result = false;
            string[,] arrParametros = new string[2, 3] {
                                            {"c_fchini", "System.STRING", c_FechaInicio.ToString()},
                                            {"c_fchfin", "System.STRING", c_FechaFInal.ToString()},
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("pla_marcacion_consulta1", arrParametros, mysConec);

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
        public bool ListarAsistenciaHor(string c_FechaInicio, string c_FechaFInal)
        {
            bool b_result = false;
            string[,] arrParametros = new string[2, 3] {
                                            {"c_fchini", "System.STRING", c_FechaInicio.ToString()},
                                            {"c_fchfin", "System.STRING", c_FechaFInal.ToString()},
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("pla_marcacion_consulta2", arrParametros, mysConec);

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
        public bool ListarMarcacion(string c_NumeroDocumento, string c_FechaInicio, string c_FechaFInal)
        {
            bool b_result = false;
            string[,] arrParametros = new string[3, 3] {
                                            {"c_numdoc", "System.STRING", c_NumeroDocumento.ToString()},
                                            {"c_fchini", "System.STRING", c_FechaInicio.ToString()},
                                            {"c_fchfin", "System.STRING", c_FechaFInal.ToString()},
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("pla_marcacion_consulta3", arrParametros, mysConec);

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
    }
}
