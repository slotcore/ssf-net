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
    public class CD_tes_operaciones
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public DataTable DtLista = new DataTable();
        public DataTable DtRegistro = new DataTable();
        public DataTable DtRegistroOri = new DataTable();
        public DataTable DtRegistroOriDet = new DataTable();
        public DataTable DtRegistroDes = new DataTable();
        public DataTable DtRegistroDesDet = new DataTable();
        public MySqlConnection mysConec = new MySqlConnection();

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();
        
        public void Listar(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrasbajo)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT32",n_IdEmpresa.ToString()},
                                            {"n_ano", "System.INT32",n_AnoTrabajo.ToString()},
                                            {"n_mes", "System.INT32",n_MesTrasbajo.ToString()},
                                      };

            DtLista = xMiFuncion.StoreDTLLenar("tes_operaciones_listar", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public bool TraerRegistro(int n_IdRegistro)
        {
            DataTable DtResultado = new DataTable();
            bool b_Result = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT32", n_IdRegistro.ToString()}
                                      };
            DtRegistro = xMiFuncion.StoreDTLLenar("tes_operaciones_obtenerregistro", arrParametros, mysConec);

            if (DtRegistro != null)
            {
                arrParametros[0, 0] = "n_idope";
                DtRegistroOri = xMiFuncion.StoreDTLLenar("tes_operacionesori_obtenerregistro", arrParametros, mysConec);
                if (DtRegistroOri != null)
                {
                    DtRegistroOriDet = xMiFuncion.StoreDTLLenar("tes_operacionesoridet_obtenerregistro", arrParametros, mysConec);
                    if (DtRegistroOriDet != null)
                    {
                        DtRegistroDes = xMiFuncion.StoreDTLLenar("tes_operacionesdes_obtenerregistro", arrParametros, mysConec);
                        if (DtRegistroDes != null)
                        {
                            DtRegistroDesDet = xMiFuncion.StoreDTLLenar("tes_operacionesdesdet_obtenerregistro", arrParametros, mysConec);
                            if (DtRegistroDesDet == null)
                            {
                                b_OcurrioError = xMiFuncion.booOcurrioError;
                                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                return b_Result;
                            }
                        }
                        else
                        {
                            b_OcurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            return b_Result;
                        }
                        
                    }
                    else
                    {
                        b_OcurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                        return b_Result;
                    }
                }
                else
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    return b_Result;
                }
            }
            else
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return b_Result;
            }
            b_Result = true;

            return b_Result;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool booResult = false;
            MySqlTransaction trans;
            // PARAMETROS PARA ELIMINAR LOS INSUMOS Y LAS TAREAS
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idope", "System.INT64", n_IdRegistro.ToString()}
                                      };

            trans = mysConec.BeginTransaction();

            try
            {
                if (xMiFuncion.StoreEjecutar("tes_operacionesoridet_delete", arrParametros, mysConec) == true)
                {
                    if (xMiFuncion.StoreEjecutar("tes_operacionesdesdet_delete", arrParametros, mysConec) == true)
                    {
                        if (xMiFuncion.StoreEjecutar("tes_operacionesdes_delete", arrParametros, mysConec) == true)
                        {
                            if (xMiFuncion.StoreEjecutar("tes_operacionesori_delete", arrParametros, mysConec) == true)
                            {
                                arrParametros[0, 0] = "n_id";
                                if (xMiFuncion.StoreEjecutar("tes_operaciones_delete", arrParametros, mysConec) == false)
                                {
                                    b_OcurrioError = xMiFuncion.booOcurrioError;
                                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                    trans.Rollback();
                                    return booResult;
                                }
                            }
                            else
                            {
                                b_OcurrioError = xMiFuncion.booOcurrioError;
                                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                trans.Rollback();
                                return booResult;
                            }
                        }
                        else
                        {
                            b_OcurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return booResult;
                        }
                    }
                    else
                    {
                        b_OcurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                        trans.Rollback();
                        return booResult;
                    }
                }
                else
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return booResult;
                }
                booResult = true;
                trans.Commit();
                return booResult;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                trans.Rollback();
                return booResult;
            }

        }
        public bool Insertar(BE_TES_OPERACIONES entOperaciones, List<BE_TES_OPERACIONESORI> lstOperacionesOri, List<BE_TES_OPERACIONESORIDET> lstOperacionesOriDet,
            List<BE_TES_OPERACIONESORI> lstOperacionesDes, List<BE_TES_OPERACIONESORIDET> lstOperacionesDesDet)
        {
            bool booOk = false;
            MySqlTransaction trans;
            DatosMySql xMiFuncion = new DatosMySql();
            int n_row = 0;
            trans = mysConec.BeginTransaction();

            try
            {
                if (xMiFuncion.StoreEjecutar("tes_operaciones_insertar", entOperaciones, mysConec, 1) == true)
                {
                    // ***********************************
                    // GRABAMOS EL ORIGEN DE LA OPERACION
                    for (n_row = 0; n_row <= lstOperacionesOri.Count - 1; n_row++)
                    {
                        lstOperacionesOri[n_row].n_idope = Convert.ToInt32(xMiFuncion.intIdGenerado);
                        if (xMiFuncion.StoreEjecutar("tes_operacionesori_insertar", lstOperacionesOri[n_row], mysConec, 1) == false)
                        {
                            b_OcurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return booOk;
                        }
                    }
                    for (n_row = 0; n_row <= lstOperacionesOri.Count - 1; n_row++)
                    {
                        lstOperacionesOri[n_row].n_idope = Convert.ToInt32(xMiFuncion.intIdGenerado);
                        if (xMiFuncion.StoreEjecutar("tes_operacionesoridet_insertar", lstOperacionesOriDet[n_row], mysConec, 1) == false)
                        {
                            b_OcurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return booOk;
                        }
                    }
                    // ***********************************
                    // GRABAMOS EL DESTINO DE LA OPERACION
                    for (n_row = 0; n_row <= lstOperacionesOri.Count - 1; n_row++)
                    {
                        lstOperacionesOri[n_row].n_idope = Convert.ToInt32(xMiFuncion.intIdGenerado);
                        if (xMiFuncion.StoreEjecutar("tes_operacionesdes_insertar", lstOperacionesDes[n_row], mysConec, 1) == false)
                        {
                            b_OcurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return booOk;
                        }
                    }
                    for (n_row = 0; n_row <= lstOperacionesOri.Count - 1; n_row++)
                    {
                        lstOperacionesOri[n_row].n_idope = Convert.ToInt32(xMiFuncion.intIdGenerado);
                        if (xMiFuncion.StoreEjecutar("tes_operacionesdesdet_insertar", lstOperacionesDesDet[n_row], mysConec, 1) == false)
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
        public bool Actualizar(BE_TES_OPERACIONES entOperaciones, List<BE_TES_OPERACIONESORI> lstOperacionesOri, List<BE_TES_OPERACIONESORIDET> lstOperacionesOriDet,
            List<BE_TES_OPERACIONESORI> lstOperacionesDes, List<BE_TES_OPERACIONESORIDET> lstOperacionesDesDet)
        {
            bool booOk = false;
            MySqlTransaction trans;
            DatosMySql xMiFuncion = new DatosMySql();

            trans = mysConec.BeginTransaction();

            try
            {
                if (xMiFuncion.StoreEjecutar("tes_operaciones_actualizar", entOperaciones, mysConec, null) == true)
                {
                    booOk = true;
                }
                else
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
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
