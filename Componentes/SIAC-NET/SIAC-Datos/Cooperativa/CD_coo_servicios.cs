using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Cooperativa;

namespace SIAC_DATOS.Cooperativa
{
    public class CD_coo_servicios
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        public DataTable dtListar = new DataTable();
        public DataTable dtServicios = new DataTable();
        public DataTable dtServiciosDet = new DataTable();
        public DataTable Listar(int n_IdEmpresa)
        {
            DataTable DtResultado = new DataTable();
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()}
                                      };

            dtListar = xMiFuncion.StoreDTLLenar("coo_servicios_listar", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public DataTable Consulta2(int n_IdEmpresa, int n_IdTipoServicio)
        {
            DataTable DtResultado = new DataTable();
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_tipser", "System.INT16", n_IdTipoServicio.ToString()}
                                      };

            dtListar = xMiFuncion.StoreDTLLenar("coo_servicios_consulta2", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public void TraerRegistro(int n_idSocio)
        {
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT32", n_idSocio.ToString()}
                                      };
            string[,] arrParametros2 = new string[1, 3] {
                                            {"n_idser", "System.INT32", n_idSocio.ToString()}
                                      };
            dtServicios = xMiFuncion.StoreDTLLenar("coo_servicios_obtenerregistro", arrParametros, mysConec);
            if (xMiFuncion.booOcurrioError == false)
            {
                dtServiciosDet = xMiFuncion.StoreDTLLenar("coo_serviciosdet_listar", arrParametros2, mysConec);
                if (xMiFuncion.booOcurrioError == true)
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                }
            }
            else
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public bool Insertar(BE_COO_SERVICIOS entServicios, List<BE_COO_SERVICIOSDET> lstServiciosDet)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            int n_row = 0;

            if (xMiFuncion.StoreEjecutar("coo_servicios_insertar", entServicios, mysConec, 0) == true)
            {
                for (n_row =0; n_row <= lstServiciosDet.Count-1; n_row++)
                {
                    lstServiciosDet[n_row].n_idser = Convert.ToInt32(xMiFuncion.intIdGenerado);
                    if (xMiFuncion.StoreEjecutar("coo_serviciosdet_insertar", lstServiciosDet[n_row], mysConec, null) == false)
                    {
                        booOcurrioError = xMiFuncion.booOcurrioError;
                        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        IntErrorNumber = xMiFuncion.IntErrorNumber;
                    }
                }
                booOk = true;
            }
            else
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booOk;
        }
        public bool Actualizar(BE_COO_SERVICIOS entServicios, List<BE_COO_SERVICIOSDET> lstServiciosDet)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            int n_row = 0;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idser", "System.INT16", entServicios.n_id.ToString()}
                                      };

            booOk = xMiFuncion.StoreEjecutar("coo_serviciosdet_delete", arrParametros, mysConec);
            if (booOk == true)
            {
                if (xMiFuncion.StoreEjecutar("coo_servicios_actualizar", entServicios, mysConec, null) == true)
                {
                    for (n_row = 0; n_row <= lstServiciosDet.Count - 1; n_row++)
                    {
                        if (xMiFuncion.StoreEjecutar("coo_serviciosdet_insertar", lstServiciosDet[n_row], mysConec, null) == false)
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                        }
                    }
                    booOk = true;
                }
                else
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                }
            }
            else
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return booOk;
        }
        public bool Eliminar(int n_IdItem)
        {
            DatosMySql xMiFuncion = new DatosMySql();
            bool booResult = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT16", n_IdItem.ToString()}
                                      };
            string[,] arrParametros2 = new string[1, 3] {
                                            {"n_idser", "System.INT16", n_IdItem.ToString()}
                                      };
            booResult = xMiFuncion.StoreEjecutar("coo_servicios_delete", arrParametros, mysConec);

            if (booResult == true)
            {
                booResult = xMiFuncion.StoreEjecutar("coo_serviciosdet_delete", arrParametros2, mysConec);
                if (booResult == false)
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                }
            }
            else
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booResult;
        }
    }
}
