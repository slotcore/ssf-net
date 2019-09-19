using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Contabilidad;
using SIAC_Entidades.Ventas;
using SIAC_DATOS.Ventas;
namespace SIAC_DATOS.Contabilidad
{
    public class CD_con_regretencion
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtLista = new DataTable();
        public DataTable dtDetalle = new DataTable();

        DatosMySql xMiFuncion = new DatosMySql();

        public List<BE_CON_DIARIO> l_Diario = new List<BE_CON_DIARIO>();

        public void Listar(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo)
        {
            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"n_ano", "System.INT32", n_AnoTrabajo.ToString()},
                                            {"n_mes", "System.INT32", n_MesTrabajo.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("con_regretencion_listar", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return;
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT32", n_IdRegistro.ToString()}
                                      };
            dtLista = xMiFuncion.StoreDTLLenar("con_regretencion_obtenerregistro", arrParametros, mysConec);
            if (xMiFuncion.booOcurrioError == true)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                string[,] arrParametros2 = new string[1, 3] {
                                            {"n_idreg", "System.INT32", n_IdRegistro.ToString()},
                                      };

                dtDetalle = xMiFuncion.StoreDTLLenar("con_regretenciondet_listar", arrParametros2, mysConec);
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

            if (xMiFuncion.StoreEjecutar("con_regretencion_delete", arrParametros, mysConec) == false)
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
        public bool Insertar(BE_CON_REGRETENCION e_Retencion, List<BE_CON_REGRETENCIONDET> l_RetencionDetalle)
        {
            bool booOk = false;
            int n_row = 0;
            int n_idgenerado=0;
            DatosMySql xMiFuncion = new DatosMySql();

            if (xMiFuncion.StoreEjecutar("con_regretencion_insertar", e_Retencion, mysConec, 4) == false)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                n_idgenerado = Convert.ToInt32(xMiFuncion.intIdGenerado);
                e_Retencion.n_id = n_idgenerado;
                for (n_row = 0; n_row <= l_RetencionDetalle.Count - 1; n_row++)
                {
                    l_RetencionDetalle[n_row].n_idret = n_idgenerado; 
                    if (xMiFuncion.StoreEjecutar("con_regretenciondet_insertar", l_RetencionDetalle[n_row], mysConec, null) == false)
                    {
                        b_OcurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                        return booOk;
                    }
                }

                for (n_row = 0; n_row <= l_RetencionDetalle.Count - 1; n_row++)
                {
                    BE_VTA_VENTAS e_ventas = new BE_VTA_VENTAS();
                    CD_vta_ventas o_venta = new CD_vta_ventas();
                    double n_salfac = 0;

                    o_venta.mysConec = mysConec;

                    // TRAEMOS EL DOCUMENTO QUE SE NODIFICA
                    int n_iddocventa = l_RetencionDetalle[n_row].n_iddoc;                                   // OBTENEMOS EL ID DEL DOCUMENTO QUE SE MODIFICA
                    e_ventas = o_venta.TraerRegistro(n_iddocventa);
                    n_salfac = e_ventas.n_impsal - l_RetencionDetalle[n_row].n_impret;

                    string[,] arrParam1 = new string[2, 3] {
                                                {"n_id", "System.INT32", n_iddocventa.ToString()},
                                                {"n_saldo", "System.DOUBLE", n_salfac.ToString()}
                                          };

                    if (xMiFuncion.StoreEjecutar("vta_ventas_actualizarsaldo", arrParam1, mysConec) == false)
                    {
                        b_OcurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                        //trans.Rollback();
                        return booOk;
                    }
                }

                // GRABAMOS EL DIARIO
                for (n_row = 0; n_row <= l_Diario.Count - 1; n_row++)
                {
                    l_Diario[n_row].n_oriid = Convert.ToInt32(n_idgenerado);
                    if (xMiFuncion.StoreEjecutar("con_diario_insertar", l_Diario[n_row], mysConec, 0) == false)
                    {
                        b_OcurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                        //trans.Rollback();
                        return booOk;
                    }
                }
                               
                booOk = true;
            }
            return booOk;
        }
        public bool Actualizar(BE_CON_REGRETENCION e_Retencion, List<BE_CON_REGRETENCIONDET> l_RetencionDetalle)
        {
            bool booOk = false;
            int n_row = 0;
            int n_idregistro = 0;
            DatosMySql xMiFuncion = new DatosMySql();

            MySqlTransaction trans = null;

            try
            {
                trans = mysConec.BeginTransaction();

                string[,] arrParametros = new string[5, 3] {
                                                {"n_lib", "System.INT32",e_Retencion.n_idlib.ToString()},
                                                {"n_ano", "System.INT32",e_Retencion.n_ano.ToString()},
                                                {"n_mes", "System.INT32",e_Retencion.n_mes.ToString()},
                                                {"c_numasi", "System.STRING",e_Retencion.c_numreg.ToString()},
                                                {"n_idemp", "System.INT32",e_Retencion.n_idemp.ToString()}
                                          };
                if (xMiFuncion.StoreEjecutar("con_diario_delete", arrParametros, mysConec) == false)
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return booOk;
                }

                if (xMiFuncion.StoreEjecutar("con_regretenciondet_delete", e_Retencion, mysConec, null) == false)
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return booOk;
                }

                if (xMiFuncion.StoreEjecutar("con_regretencion_actualizar", e_Retencion, mysConec, null) == false)
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return booOk;
                }
                else
                {
                    n_idregistro = e_Retencion.n_id;
                    for (n_row = 0; n_row <= l_RetencionDetalle.Count - 1; n_row++)
                    {
                        l_RetencionDetalle[n_row].n_idret = n_idregistro;
                        if (xMiFuncion.StoreEjecutar("con_regretenciondet_insertar", l_RetencionDetalle[n_row], mysConec, null) == false)
                        {
                            b_OcurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return booOk;
                        }
                    }

                    // GRABAMOS EL DIARIO
                    for (n_row = 0; n_row <= l_Diario.Count - 1; n_row++)
                    {
                        l_Diario[n_row].n_oriid = Convert.ToInt32(n_idregistro);
                        if (xMiFuncion.StoreEjecutar("con_diario_insertar", l_Diario[n_row], mysConec, 0) == false)
                        {
                            b_OcurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return booOk;
                        }
                    }
                    booOk = true;
                }
                trans.Commit();
                return booOk;
            }
             catch (Exception exc)
             {
                 // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                 b_OcurrioError = true;
                 c_ErrorMensaje = exc.Message.ToString();
                 n_ErrorNumber = exc.HResult;
                 trans.Rollback();
                 return booOk;
             }
        }
        public bool AsientoCab(int n_IdEmpresa, int n_Idretencion)
        {
            bool b_result = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT64",n_Idretencion.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("con_regretencion_asientocab", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return b_result;
            }
            else
            {
                dtDetalle = xMiFuncion.StoreDTLLenar("con_regretencion_asientodet", arrParametros, mysConec);
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
            if (xMiFuncion.StoreEjecutar("con_regretencion_insertarnumasi", arrParametros, mysConec) == false)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return b_Result;
        }
    }
}
