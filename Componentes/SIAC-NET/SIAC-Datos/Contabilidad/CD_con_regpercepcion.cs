using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Contabilidad;

namespace SIAC_DATOS.Contabilidad
{
    public class CD_con_regpercepcion
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtLista = new DataTable();
        public DataTable dtDetalle = new DataTable();

        DatosMySql xMiFuncion = new DatosMySql();
        public void Listar(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo, int n_TipoRegistro)
        {
            string[,] arrParametros = new string[4, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"n_ano", "System.INT32", n_AnoTrabajo.ToString()},
                                            {"n_mes", "System.INT32", n_MesTrabajo.ToString()},
                                            {"n_tipreg", "System.INT32", n_TipoRegistro.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("con_regpercepcion_listar", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return;
        }
        public void TraerRegistro(int n_IdRegistro, int n_TipoRegistro)
        {
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT32", n_IdRegistro.ToString()}
                                      };
            dtLista = xMiFuncion.StoreDTLLenar("con_regpercepcion_obtenerregistro", arrParametros, mysConec);
            if (xMiFuncion.booOcurrioError == true)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                string[,] arrParametros2 = new string[2, 3] {
                                            {"n_idper", "System.INT32", n_IdRegistro.ToString()},
                                            {"n_tipreg", "System.INT32", n_TipoRegistro.ToString()},
                                      };

                dtDetalle = xMiFuncion.StoreDTLLenar("con_regpercepciondet_listar", arrParametros2, mysConec);
                if (xMiFuncion.IntErrorNumber != 0)
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                }
            }
            return;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            DatosMySql xMiFuncion = new DatosMySql();
            bool booOk = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT16", n_IdRegistro.ToString()}
                                      };

            if (xMiFuncion.StoreEjecutar("con_regpercepcion_delete", arrParametros, mysConec) == false)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                booOk = true;
            }

            return booOk;
        }
        public bool Insertar(BE_CON_REGPERCEPCION e_Perepcion, List<BE_CON_REGPERCEPCIONDET> l_PercecionDetalle)
        {
            bool booOk = false;
            int n_row = 0;
            int n_idgenerado = 0;
            DatosMySql xMiFuncion = new DatosMySql();

            if (xMiFuncion.StoreEjecutar("con_regpercepcion_insertar", e_Perepcion, mysConec, 4) == false)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                n_idgenerado = Convert.ToInt32(xMiFuncion.intIdGenerado);
                e_Perepcion.n_id = n_idgenerado;
                for (n_row = 0; n_row <= l_PercecionDetalle.Count - 1; n_row++)
                {
                    l_PercecionDetalle[n_row].n_idper = n_idgenerado;
                    if (xMiFuncion.StoreEjecutar("con_regpercepciondet_insertar", l_PercecionDetalle[n_row], mysConec, null) == false)
                    {
                        b_OcurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                        return booOk;
                    }
                }
                booOk = true;
            }
            return booOk;
        }
        public bool Actualizar(BE_CON_REGPERCEPCION e_Perepcion, List<BE_CON_REGPERCEPCIONDET> l_PercecionDetalle)
        {
            bool booOk = false;
            int n_row = 0;
            int n_idregistro = 0;
            DatosMySql xMiFuncion = new DatosMySql();

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT64", e_Perepcion.n_id.ToString()}
                                      };

            if (xMiFuncion.StoreEjecutar("con_regpercepciondet_delete", arrParametros, mysConec) == false)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return booOk;
            }

            if (xMiFuncion.StoreEjecutar("con_regpercepcion_actualizar", e_Perepcion, mysConec, null) == false)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return booOk;
            }
            else
            {
                n_idregistro = e_Perepcion.n_id;
                for (n_row = 0; n_row <= l_PercecionDetalle.Count - 1; n_row++)
                {
                    l_PercecionDetalle[n_row].n_idper = n_idregistro;
                    if (xMiFuncion.StoreEjecutar("con_regpercepciondet_insertar", l_PercecionDetalle[n_row], mysConec, null) == false)
                    {
                        b_OcurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                        return booOk;
                    }
                }
                booOk = true;
            }
            return booOk;
        }
        public bool AsientoCab(int n_IdEmpresa, int n_IdPercepcion, int n_TipoRegistro)
        {
            bool b_result = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdPercepcion.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("con_regpercepcion_asientocab", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return b_result;
            }
            else
            {
                string[,] arrParametros2 = new string[2, 3] {
                                            {"n_id", "System.INT64", n_IdPercepcion.ToString()},
                                            {"n_tipreg", "System.INT64", n_TipoRegistro.ToString()}
                                      };
                dtDetalle = xMiFuncion.StoreDTLLenar("con_regpercepcion_asientodet", arrParametros2, mysConec);
                if (xMiFuncion.IntErrorNumber != 0)
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    return b_result;
                }
            }
            b_result = true;

            return b_result;
        }
        public bool AgregarNumAsi(int n_IdRegistro, string c_NumeroRegistro)
        {
            bool b_Result = false;

            string[,] arrParametros = new string[2, 3] {
                                            {"n_id", "System.INT64", n_IdRegistro.ToString()},
                                            {"c_numasi", "System.STRING", c_NumeroRegistro.ToString()}
                                      };
            // BORRAMOS EL PADRE
            if (xMiFuncion.StoreEjecutar("con_regpercepcion_insertarnumasi", arrParametros, mysConec) == false)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return b_Result;
        }
    }
}
