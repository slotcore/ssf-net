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
    public class CD_con_proviciones
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtLista = new DataTable();
        public DataTable dtDetalle = new DataTable();

        DatosMySql xMiFuncion = new DatosMySql();
        public void Listar(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo)
        {
            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"n_ano", "System.INT32", n_AnoTrabajo.ToString()},
                                            {"n_mes", "System.INT32", n_MesTrabajo.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("con_proviciones_listar", arrParametros, mysConec);

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
            dtLista = xMiFuncion.StoreDTLLenar("con_proviciones_obtenerregistro", arrParametros, mysConec);
            if (xMiFuncion.booOcurrioError == true)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            arrParametros[0, 0] = "n_idpro";
            dtDetalle = xMiFuncion.StoreDTLLenar("con_provicionesdet_select", arrParametros, mysConec);
            if (xMiFuncion.booOcurrioError == true)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return;
        }
        public bool Eliminar(BE_CON_PROVICIONES e_Proviciones)
        {
            DatosMySql xMiFuncion = new DatosMySql();
            bool booOk = false;
            MySqlTransaction trans;

            trans = mysConec.BeginTransaction();
            try
            {
                string[,] arrParametros = new string[1, 3] {
                                                {"n_id", "System.INT16", e_Proviciones.n_id.ToString()}
                                          };

                if (xMiFuncion.StoreEjecutar("con_proviciones_delete", arrParametros, mysConec) == false)
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                }
                else
                {
                    arrParametros[0, 0] = "n_idpro";
                    if (xMiFuncion.StoreEjecutar("con_provicionesdet_delete", arrParametros, mysConec) == false)
                    {
                        b_OcurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    }

                    // ELIMINAMOS EL ASIENDO DIARIO
                    CD_con_diario funDiario = new CD_con_diario();
                    xMiFuncion.ReAbrirConeccion(mysConec);
                    funDiario.mysConec = mysConec;

                    if (funDiario.Eliminar(e_Proviciones.n_idlib, e_Proviciones.n_ano, e_Proviciones.n_mes, e_Proviciones.c_numreg, e_Proviciones.n_idemp) == false)
                    {
                        b_OcurrioError = funDiario.b_OcurrioError;
                        c_ErrorMensaje = funDiario.c_ErrorMensaje;
                        n_ErrorNumber = funDiario.n_ErrorNumber;
                        trans.Rollback();
                        return booOk;
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
        public bool Insertar(BE_CON_PROVICIONES e_Proviciones, List<BE_CON_PROVICIONESDET> l_ProvicionesDet, List<BE_CON_DIARIO> l_Diario)
        {
            bool booOk = false;
            int n_row = 0;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans;
            xMiFuncion.ReAbrirConeccion(mysConec);
    
            trans = mysConec.BeginTransaction();
            try
            {
                if (xMiFuncion.StoreEjecutar("con_proviciones_insertar", e_Proviciones, mysConec, 0) == false)
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return booOk;
                }
                else
                {
                    for (n_row = 0; n_row <= l_ProvicionesDet.Count - 1; n_row++)
                    {
                        e_Proviciones.n_id = Convert.ToInt32(xMiFuncion.intIdGenerado);
                        l_ProvicionesDet[n_row].n_idpro = Convert.ToInt32(xMiFuncion.intIdGenerado);
                        if (xMiFuncion.StoreEjecutar("con_provicionesdet_insertar", l_ProvicionesDet[n_row], mysConec, null) == false)
                        {
                            b_OcurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return booOk;
                        }
                    }
                    
                    // INSERTAMOS EL ASIENTO DEL DIARIO
                    CD_con_diario funDiario = new CD_con_diario();
                    xMiFuncion.ReAbrirConeccion(mysConec);
                    funDiario.mysConec = mysConec;
                    funDiario.b_DesdeOtraCapa = true;
  
                    for (n_row = 0; n_row <= l_Diario.Count - 1; n_row++)
                    {
                        l_Diario[n_row].n_oriid = e_Proviciones.n_id;
                    }

                    if (funDiario.Insertar(l_Diario) == false)
                    {
                        b_OcurrioError = funDiario.b_OcurrioError;
                        c_ErrorMensaje = funDiario.c_ErrorMensaje;
                        n_ErrorNumber = funDiario.n_ErrorNumber;
                        trans.Rollback();
                        return booOk;
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
        public bool Actualizar(BE_CON_PROVICIONES e_Proviciones, List<BE_CON_PROVICIONESDET> l_ProvicionesDet, List<BE_CON_DIARIO> l_Diario)
        {
            bool booOk = false;
            int n_row = 0;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans;
            xMiFuncion.ReAbrirConeccion(mysConec);

            trans = mysConec.BeginTransaction();
            try
            {
                string[,] arrParametros = new string[1, 3] {
                                            {"n_idpro", "System.INT32", e_Proviciones.n_id.ToString()}
                                      };

                if (xMiFuncion.StoreEjecutar("con_provicionesdet_delete", arrParametros, mysConec) == false)
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return booOk;
                }

                if (xMiFuncion.StoreEjecutar("con_proviciones_actualizar", e_Proviciones, mysConec, null) == false)
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return booOk;
                }
                else
                {
                    for (n_row = 0; n_row <= l_ProvicionesDet.Count - 1; n_row++)
                    {
                        l_ProvicionesDet[n_row].n_idpro = e_Proviciones.n_id;
                        if (xMiFuncion.StoreEjecutar("con_provicionesdet_insertar", l_ProvicionesDet[n_row], mysConec, null) == false)
                        {
                            b_OcurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return booOk;
                        }
                    }

                    // ELIMINAMOS EL ASIENDO DIARIO
                    CD_con_diario funDiario = new CD_con_diario();
                    xMiFuncion.ReAbrirConeccion(mysConec);
                    funDiario.mysConec = mysConec;

                    if (funDiario.Eliminar(l_Diario[0].n_lib, e_Proviciones.n_ano, e_Proviciones.n_mes, l_Diario[0].c_numasi, e_Proviciones.n_idemp)==false)
                    {
                        b_OcurrioError = funDiario.b_OcurrioError;
                        c_ErrorMensaje = funDiario.c_ErrorMensaje;
                        n_ErrorNumber = funDiario.n_ErrorNumber;
                        trans.Rollback();
                        return booOk;
                    }

                    // INSERTAMOS EL ASIENTO DEL DIARIO
                    funDiario.b_DesdeOtraCapa = true;
                    for (n_row = 0; n_row <= l_Diario.Count - 1; n_row++)
                    {
                        l_Diario[n_row].n_oriid = e_Proviciones.n_id;
                    }

                    if (funDiario.Insertar(l_Diario) == false)
                    {
                        b_OcurrioError = funDiario.b_OcurrioError;
                        c_ErrorMensaje = funDiario.c_ErrorMensaje;
                        n_ErrorNumber = funDiario.n_ErrorNumber;
                        trans.Rollback();
                        return booOk;
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
    }
}
