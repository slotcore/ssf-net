using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Tesoreria;
using SIAC_Entidades.Contabilidad;

namespace SIAC_DATOS.Tesoreria
{
    public class CD_tes_letras
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public DataTable dtLista1 = new DataTable();
        public DataTable dtLista2 = new DataTable();
        public DataTable dtLista3 = new DataTable();
        public DataTable dtLista4 = new DataTable();
        public MySqlConnection mysConec = new MySqlConnection();

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();
        public List<BE_CON_DIARIO> l_diario = new List<BE_CON_DIARIO>();

        public void Listar(int n_IdEmpresa, int n_AnoTrabajo, int n_mesTrabajo)
        {
            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"n_ano", "System.INT32", n_AnoTrabajo.ToString()},
                                            {"n_mes", "System.INT32", n_mesTrabajo.ToString()}
                                      };

            dtLista1 = xMiFuncion.StoreDTLLenar("tes_letras_listar", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtLista1 = null; dtLista2 = null; dtLista3 = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return;
            }

            return;
        }
        public bool TraerRegistro(int n_IdRegistro)
        {
            bool booResult = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_idreg", "System.INT32", n_IdRegistro.ToString()}
                                      };
            dtLista1 = xMiFuncion.StoreDTLLenar("tes_letras_obtenerregistro", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber == 0)
            {
                arrParametros[0, 0] = "n_idlet";
                dtLista2 = xMiFuncion.StoreDTLLenar("tes_letrasdet_listar", arrParametros, mysConec);
                if (xMiFuncion.IntErrorNumber == 0)
                {
                    dtLista3 = xMiFuncion.StoreDTLLenar("tes_letrasdoc_listar", arrParametros, mysConec);
                    if (xMiFuncion.IntErrorNumber == 0)
                    {
                        dtLista4 = xMiFuncion.StoreDTLLenar("tes_letrasdoc_select", arrParametros, mysConec);
                        if (xMiFuncion.IntErrorNumber != 0)
                        {
                            dtLista1 = null; dtLista2 = null; dtLista3 = null; dtLista4 = null;
                            b_OcurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            return booResult;
                        }
                    }
                    else
                    {
                        dtLista1 = null; dtLista2 = null; dtLista3 = null;
                        b_OcurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                        return booResult;
                    }
                }
                else
                {
                    dtLista1 = null; dtLista2 = null; dtLista3 = null;
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    return booResult;
                }
            }
            else
            {
                dtLista1 = null; dtLista2 = null; dtLista3 = null;
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
            bool b_Result = false;
            MySqlTransaction trans = null;

            trans = mysConec.BeginTransaction();
            xMiFuncion.ReAbrirConeccion(mysConec);
            try
            {
                // PARAMETROS PARA ELIMINAR LOS INSUMOS Y LAS TAREAS
                string[,] arrParametros = new string[1, 3] {
                                                {"n_idreg", "System.INT32", n_IdRegistro.ToString()}
                                          };

                b_Result = xMiFuncion.StoreEjecutar("tes_letras_delete", arrParametros, mysConec);
                if (b_Result == false)
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return b_Result;
                }
                b_Result = true;
                trans.Commit();
                return b_Result;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                trans.Rollback();
                return b_Result;
            }
        }
        public bool Insertar(BE_TES_LETRAS e_Letras, List<BE_TES_LETRASDET> l_LetrasDetalle, List<BE_TES_LETRASDOC> l_LetraDocumentos)
        {
            bool b_Result = false;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans = null;
            int n_row = 0;

            trans = mysConec.BeginTransaction();
            xMiFuncion.ReAbrirConeccion(mysConec);
            try
            {
                if (xMiFuncion.StoreEjecutar("tes_letras_insertar", e_Letras, mysConec, 4) == true)
                {
                    int n_idgenerado = Convert.ToInt32(xMiFuncion.intIdGenerado);

                    // GRABAMOS EL DETALLE DE LAS LETRAS
                    for (n_row = 0; n_row <= l_LetrasDetalle.Count - 1; n_row++)
                    {
                        BE_TES_LETRASDET e_letdet = new BE_TES_LETRASDET();
                        l_LetrasDetalle[n_row].n_idlet = n_idgenerado;
                        e_letdet = l_LetrasDetalle[n_row];
                        if (xMiFuncion.StoreEjecutar("tes_letrasdet_insertar", e_letdet, mysConec, 1) == false)
                        {
                            // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                            b_OcurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return b_Result;
                        }

                        int n_fil = 0;
                        for (n_fil = 0; n_fil <= l_diario.Count - 1; n_fil++)
                        {
                            if (l_diario[n_fil].c_orinumdoc == l_LetrasDetalle[n_row].c_numlet)
                            {
                                l_diario[n_fil].n_oriid = Convert.ToInt32(xMiFuncion.intIdGenerado);
                            }
                        }
                    }

                    // GRABAMOS LOS DOCUMENTOS RELACIONADOS A LAS LETRAS
                    for (n_row = 0; n_row <= l_LetraDocumentos.Count - 1; n_row++)
                    {
                        BE_TES_LETRASDOC e_letdoc = new BE_TES_LETRASDOC();

                        l_LetraDocumentos[n_row].n_idlet = n_idgenerado;
                        e_letdoc = l_LetraDocumentos[n_row];
                        if (xMiFuncion.StoreEjecutar("tes_letrasdoc_insertar", e_letdoc, mysConec, null) == false)
                        {
                            // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                            b_OcurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return b_Result;
                        }
                    }

                    // GRABAMOS EL DIARIO
                    for (n_row = 0; n_row <= l_diario.Count - 1; n_row++)
                    {
                        l_diario[n_row].n_oriid = Convert.ToInt32(n_idgenerado);
                        if (xMiFuncion.StoreEjecutar("con_diario_insertar", l_diario[n_row], mysConec, 0) == false)
                        {
                            b_OcurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return b_Result;
                        }
                    }
                }
                else
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return b_Result;
                }

                b_Result = true;
                trans.Commit();
                return b_Result;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                trans.Rollback();
                return b_Result;
            }
        }
        public bool Actualizar(BE_TES_LETRAS e_Letras, List<BE_TES_LETRASDET> l_LetrasDetalle, List<BE_TES_LETRASDOC> l_LetraDocumentos)
        {
            bool b_Result = false;
            int n_row = 0;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans = null;

            trans = mysConec.BeginTransaction();
            xMiFuncion.ReAbrirConeccion(mysConec);
            try
            {
                string[,] arrParametros = new string[1, 3] {
                                                {"n_idlet", "System.INT64", e_Letras.n_id.ToString()}
                                          };

                // ELIMINAMOS LOS LETRAS GENERADAS
                if (xMiFuncion.StoreEjecutar("tes_letrasdet_delete", arrParametros, mysConec) == false)
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return b_Result;
                }

                // ELIMINAMOS LOS DOCUMENTO CARGADOS
                if (xMiFuncion.StoreEjecutar("tes_letrasdoc_delete", arrParametros, mysConec) == false)
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return b_Result;
                }

                if (xMiFuncion.StoreEjecutar("tes_letras_actualizar", e_Letras, mysConec, null) == true)
                {
                    int n_id = Convert.ToInt32(e_Letras.n_id);
                    // GRABAMOS EL DETALLE DE LAS LETRAS
                    for (n_row = 0; n_row <= l_LetrasDetalle.Count - 1; n_row++)
                    {
                        BE_TES_LETRASDET e_letdet = new BE_TES_LETRASDET();
                        l_LetrasDetalle[n_row].n_idlet = n_id;
                        e_letdet = l_LetrasDetalle[n_row];
                        if (xMiFuncion.StoreEjecutar("tes_letrasdet_insertar", e_letdet, mysConec, 1) == false)
                        {
                            // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                            b_OcurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return b_Result;
                        }
                    }

                    // GRABAMOS LOS DOCUMENTOS RELACIONADOS A LAS LETRAS
                    for (n_row = 0; n_row <= l_LetraDocumentos.Count - 1; n_row++)
                    {
                        BE_TES_LETRASDOC e_letdoc = new BE_TES_LETRASDOC();

                        l_LetraDocumentos[n_row].n_idlet = n_id;
                        e_letdoc = l_LetraDocumentos[n_row];
                        if (xMiFuncion.StoreEjecutar("tes_letrasdoc_insertar", e_letdoc, mysConec, null) == false)
                        {
                            // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                            b_OcurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return b_Result;
                        }
                    }
                    b_Result = true;
                }
                else
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                }

                b_Result = true;
                trans.Commit();
                return b_Result;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                trans.Rollback();
                return b_Result;
            }
        }
    }
}
