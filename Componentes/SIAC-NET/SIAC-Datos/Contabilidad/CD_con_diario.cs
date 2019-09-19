using Helper;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Contabilidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_DATOS.Contabilidad
{
    public class CD_con_diario
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtLista = new DataTable();
        public DataTable dtResumen = new DataTable();
        public bool b_DesdeOtraCapa = false;

        DatosMySql xMiFuncion = new DatosMySql();
        public void ObtenerUltimoAsiento(int n_AnoTrabajo, int n_MesTrabajo, int  n_IdLibro, int n_IdEmpresa)
        {
            string[,] arrParametros = new string[4, 3] {
                                            {"n_ano", "System.INT32", n_AnoTrabajo.ToString()},
                                            {"n_mes", "System.INT32", n_MesTrabajo.ToString()},
                                            {"n_lib", "System.INT32", n_IdLibro.ToString()},
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()}
                                      };

            xMiFuncion.ReAbrirConeccion(mysConec);
            dtLista = xMiFuncion.StoreDTLLenar("con_diario_obtenerultimonumero", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return;
        }
        public bool Eliminar(int n_IdLibro, int n_IdAnoTrabajo, int n_IdMesTrabajo, string c_AsientoContable, int n_IdEmpresa)
        {
            DatosMySql xMiFuncion = new DatosMySql();
            bool booOk = false;

            string[,] arrParametros = new string[5, 3] {
                                            {"n_lib", "System.INT16", n_IdLibro.ToString()},
                                            {"n_ano", "System.INT16", n_IdAnoTrabajo.ToString()},
                                            {"n_mes", "System.INT16", n_IdMesTrabajo.ToString()},
                                            {"c_numasi", "System.STRING", c_AsientoContable.ToString()},
                                            {"n_idemp", "System.STRING", n_IdEmpresa.ToString()}
                                      };

            if (xMiFuncion.StoreEjecutar("con_diario_delete", arrParametros, mysConec) == false)
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
        public bool EliminarLibroMes(int n_IdLibro, int n_IdAnoTrabajo, int n_IdMesTrabajo, int n_IdEmpresa)
        {
            DatosMySql xMiFuncion = new DatosMySql();
            bool booOk = false;

            string[,] arrParametros = new string[4, 3] {
                                            {"n_lib", "System.INT16", n_IdLibro.ToString()},
                                            {"n_ano", "System.INT16", n_IdAnoTrabajo.ToString()},
                                            {"n_mes", "System.INT16", n_IdMesTrabajo.ToString()},
                                            {"n_idemp", "System.STRING", n_IdEmpresa.ToString()}
                                      };

            if (xMiFuncion.StoreEjecutar("con_diario_deletelibromes", arrParametros, mysConec) == false)
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
        public bool Insertar(List<BE_CON_DIARIO> l_Diario)
        {
            bool booOk = false;
            int n_row = 0;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans = null;
            xMiFuncion.ReAbrirConeccion(mysConec);
            if (b_DesdeOtraCapa == false) { trans = mysConec.BeginTransaction(); }
            //trans = mysConec.BeginTransaction();

            try
            { 
                for (n_row = 0; n_row <= l_Diario.Count-1; n_row++)
                {
                    if (xMiFuncion.StoreEjecutar("con_diario_insertar", l_Diario[n_row], mysConec, 0) == false)
                    {
                        b_OcurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                        if (b_DesdeOtraCapa == false) { trans.Rollback(); }
                        return booOk;
                    }
                }

                if (b_DesdeOtraCapa == false) { trans.Commit(); }
                //trans.Commit();
                booOk = true;
                return booOk;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                b_OcurrioError = true;
                c_ErrorMensaje = exc.Message.ToString();
                n_ErrorNumber = exc.HResult;
                if (b_DesdeOtraCapa == false) { trans.Rollback(); }
                return booOk;
            }
        }
        public bool RegistroCompras(int n_AnoTrabajo, int n_MesTrabajo, int n_IdLibro, int n_IdEmpresa)
        {
            bool b_result = false;
            string[,] arrParametros = new string[4, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"n_ano", "System.INT32", n_AnoTrabajo.ToString()},
                                            {"n_mes", "System.INT32", n_MesTrabajo.ToString()},
                                            {"n_idlib", "System.INT32", n_IdLibro.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("log_compras_regcompras", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return b_result;
            }
            else
            {
                dtResumen = xMiFuncion.StoreDTLLenar("log_compras_regcompras_resumen", arrParametros, mysConec);
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
        public bool RegistroHonorarios(int n_AnoTrabajo, int n_MesTrabajo, int n_IdLibro, int n_IdEmpresa)
        {
            bool b_result = false;
            string[,] arrParametros = new string[4, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"n_ano", "System.INT32", n_AnoTrabajo.ToString()},
                                            {"n_mes", "System.INT32", n_MesTrabajo.ToString()},
                                            {"n_idlib", "System.INT32", n_IdLibro.ToString()},
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("log_compras_reghonorarios", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return b_result;
            }

            b_result = true;
            return b_result;
        }
        public bool RegistroVentas(int n_AnoTrabajo, int n_MesTrabajo, int n_IdLibro, int n_IdEmpresa)
        {
            bool b_result = false;
            string[,] arrParametros = new string[4, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"n_ano", "System.INT32", n_AnoTrabajo.ToString()},
                                            {"n_mes", "System.INT32", n_MesTrabajo.ToString()},
                                            {"n_idlib", "System.INT32", n_IdLibro.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("vta_ventas_regventas", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return b_result;
            }
            else
            {
                dtResumen = xMiFuncion.StoreDTLLenar("vta_ventas_regventas_resumen", arrParametros, mysConec);
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
        public void ConsultaAsiento(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo, int n_IdLibro, string c_NumeroAsiento)
        {
            string[,] arrParametros = new string[5, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"n_ano", "System.INT32", n_AnoTrabajo.ToString()},
                                            {"n_mes", "System.INT32", n_MesTrabajo.ToString()},
                                            {"n_lib", "System.INT32", n_IdLibro.ToString()},
                                            {"c_numasi", "System.STRING", c_NumeroAsiento.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("con_diario_consultaasiento", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public bool ConsultaDiario(int n_IdEmpresa, int n_AnoTrabajo, int n_IdLibro, int n_PeriodoInicio, int n_PeriodoFinal)
        {
            bool b_result = false;
            string[,] arrParametros = new string[5, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_ano", "System.INT16", n_AnoTrabajo.ToString()},
                                            {"n_idlib", "System.INT16", n_IdLibro.ToString()},
                                            {"n_perini", "System.INT16", n_PeriodoInicio.ToString()},
                                            {"n_perfin", "System.INT16", n_PeriodoFinal.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("con_diario_diario", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber == 0)
            {
                dtResumen = xMiFuncion.StoreDTLLenar("con_diario_diarioresumen", arrParametros, mysConec);
                if (xMiFuncion.IntErrorNumber != 0)
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                }
            }
            else
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            b_result = true;
            return b_result;
        }
        public bool BalanceComprobacion(int n_IdEmpresa, int n_AnoTrabajo, int n_PeriodoInicio, int n_PeriodoFinal, int n_IdLibro)
        {
            bool b_result = false;
            string[,] arrParametros = new string[5, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_anotra", "System.INT16", n_AnoTrabajo.ToString()},
                                            {"n_mesini", "System.INT16", n_PeriodoInicio.ToString()},
                                            {"n_mesfin", "System.INT16", n_PeriodoFinal.ToString()},
                                            {"n_idlib", "System.INT16", n_IdLibro.ToString()}
                                      };

            xMiFuncion.ReAbrirConeccion(mysConec);

            dtLista = xMiFuncion.StoreDTLLenar("con_diario_balancecomprobacion", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                dtLista = null;
                return b_result;
            }
            b_result = true;
            return b_result;
        }
        public bool BalanceDetalle(int n_IdEmpresa, int n_AnoTrabajo, int n_PeriodoInicio, int n_PeriodoFinal, int n_IdCuentaContable)
        {
            bool b_result = false;
            	
            string[,] arrParametros = new string[5, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_anotra", "System.INT16", n_AnoTrabajo.ToString()},
                                            {"n_mesini", "System.INT16", n_PeriodoInicio.ToString()},
                                            {"n_mesfin", "System.INT16", n_PeriodoFinal.ToString()},
                                            {"n_idcue", "System.INT16", n_IdCuentaContable.ToString()}
                                      };

            xMiFuncion.ReAbrirConeccion(mysConec);

            dtLista = xMiFuncion.StoreDTLLenar("con_diario_balance_detallecuenta", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                dtLista = null;
                return b_result;
            }
            b_result = true;
            return b_result;
        }
        public bool ConsultaMayor(int n_IdEmpresa, int n_AnoTrabajo, int n_PeriodoInicio, int n_PeriodoFinal, string c_CadenaIN)
        {
            bool b_result = false;

            string[,] arrParametros = new string[5, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_ano", "System.INT16", n_AnoTrabajo.ToString()},
                                            {"n_perini", "System.INT16", n_PeriodoInicio.ToString()},
                                            {"n_perfin", "System.INT16", n_PeriodoFinal.ToString()},
                                            {"c_cadin", "System.INT16", c_CadenaIN.ToString()}
                                      };

            xMiFuncion.ReAbrirConeccion(mysConec);

            dtLista = xMiFuncion.StoreDTLLenar("con_mayor_consulta", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                dtLista = null;
                return b_result;
            }
            b_result = true;
            return b_result;
        }
        public bool MostrarDescuadrados(int n_IdEmpresa, int n_IdAnoTrabajo, int n_IdMesTrabajo, int n_IdLibro)
        {
            DatosMySql xMiFuncion = new DatosMySql();
            bool booOk = false;

            string[,] arrParametros = new string[4, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_ano", "System.INT16", n_IdAnoTrabajo.ToString()},
                                            {"n_mes", "System.INT16", n_IdMesTrabajo.ToString()},
                                            {"n_lib", "System.STRING", n_IdLibro.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("con_diario_descuadrados", arrParametros, mysConec);
            
            if (xMiFuncion.booOcurrioError == true)
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
    }
}
