using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Tesoreria;

namespace SIAC_DATOS.Tesoreria
{
    public class CD_tes_canje
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;

        public DataTable dtOrigen = new DataTable();
        public DataTable dtDocCompra = new DataTable();
        public DataTable dtDocVenta = new DataTable();
        public MySqlConnection mysConec = new MySqlConnection();

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();
        public void Listar(int n_IdEmpresa, int n_AñoTrabajo, int n_MesTrabajo)
        {
            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"n_ano", "System.INT32", n_AñoTrabajo.ToString()},
                                            {"n_mes", "System.INT32", n_MesTrabajo.ToString()}
                                      };

            dtOrigen = xMiFuncion.StoreDTLLenar("tes_canje_listar", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
        }
        public bool TraerRegistro(int n_IdRegistro)
        {
            bool booResult = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT32", n_IdRegistro.ToString()}
                                      };
            dtOrigen = xMiFuncion.StoreDTLLenar("tes_canje_obtenerregistro", arrParametros, mysConec);

            if (xMiFuncion.booOcurrioError == true)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return booResult;
            }

            // TRAEMOS LOS DOCUMENTOS DEL PROVEEDOR
            string[,] arrParametros2 = new string[2, 3] {
                                            {"n_idcan", "System.INT32", n_IdRegistro.ToString()},
                                            {"n_tipo", "System.INT32", "2"}
                                      };

            dtDocCompra = xMiFuncion.StoreDTLLenar("tes_canjedet_listardocumentos", arrParametros2, mysConec);

            if (xMiFuncion.booOcurrioError == true)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return booResult;
            }

            // TRAEMOS LOS DOCUMENTOS DEL CLIENTE
            arrParametros2[1, 2] = "1";
            dtDocVenta = xMiFuncion.StoreDTLLenar("tes_canjedet_listardocumentos", arrParametros2, mysConec);

            if (xMiFuncion.booOcurrioError == true)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return booResult;
            }

            booResult = true;
            return booResult;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool booOk = false;
            MySqlTransaction trans = null;
            trans = mysConec.BeginTransaction();

            try
            {
                string[,] arrParametros2 = new string[1, 3] {
                                                    {"n_idcan", "System.INT32", n_IdRegistro.ToString()}
                                              };

                if (xMiFuncion.StoreEjecutar("tes_canjedet_delete", arrParametros2, mysConec) == true)
                {
                    string[,] arrParametros = new string[1, 3] {
                                                    {"n_id", "System.INT32", n_IdRegistro.ToString()}
                                              };

                    booOk = xMiFuncion.StoreEjecutar("tes_canje_delete", arrParametros, mysConec);
                    if (booOk == false)
                    {
                        b_OcurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                        trans.Rollback();
                        return booOk;
                    }
                }
                else
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return booOk;
                }

                booOk = true;
                trans.Commit();
                return booOk;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                trans.Rollback();
                return booOk;
            }
        }
        public bool Insertar(BE_TES_CANJE e_Origen, List<BE_TES_CANJEDET> l_CanjeDocumentos)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans = null;
            trans = mysConec.BeginTransaction();
            
            try
            {
                if (xMiFuncion.StoreEjecutar("tes_canje_insertar", e_Origen, mysConec, 1) == true)
                {
                    int n_row = 0;
                    int n_idgenerado = Convert.ToInt32(xMiFuncion.intIdGenerado);

                    for (n_row = 0; n_row <= l_CanjeDocumentos.Count - 1; n_row++)
                    {
                        l_CanjeDocumentos[n_row].n_idcan = n_idgenerado;
                        if (xMiFuncion.StoreEjecutar("tes_canjedet_insertar", l_CanjeDocumentos[n_row], mysConec, null) == true)
                        {
                            booOk = true;
                        }
                        else
                        {
                            b_OcurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return booOk;
                        }
                    }
                }
                else
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return booOk;
                }
                booOk = true;
                trans.Commit();
                return booOk;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                trans.Rollback();
                return booOk;
            }
        }
        public bool Actualizar(BE_TES_CANJE e_Origen, List<BE_TES_CANJEDET> l_CanjeDocumentos)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans = null;
            trans = mysConec.BeginTransaction();
            
            try
            {
                string[,] arrParametros2 = new string[1, 3] {
                                                    {"n_idcan", "System.INT32", e_Origen.n_id.ToString()}
                                              };

                if (xMiFuncion.StoreEjecutar("tes_canjedet_delete", arrParametros2, mysConec) == true)
                { 
                    if (xMiFuncion.StoreEjecutar("tes_canje_actualizar", e_Origen, mysConec, null) == true)
                    {
                        int n_row = 0;
                        int n_idgenerado = Convert.ToInt32(xMiFuncion.intIdGenerado);

                        for (n_row = 0; n_row <= l_CanjeDocumentos.Count - 1; n_row++)
                        {
                            l_CanjeDocumentos[n_row].n_idcan = n_idgenerado;
                            if (xMiFuncion.StoreEjecutar("tes_canjedet_insertar", l_CanjeDocumentos[n_row], mysConec, null) == false)
                            {
                                b_OcurrioError = xMiFuncion.booOcurrioError;
                                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                trans.Rollback();
                                return booOk;
                            }
                        }
                    }
                    else
                    {
                        b_OcurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                        trans.Rollback();
                        return booOk;
                    }
                }
                booOk = true;
                trans.Commit();
                return booOk;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                trans.Rollback();
                return booOk;
            }
        }
    }
}
