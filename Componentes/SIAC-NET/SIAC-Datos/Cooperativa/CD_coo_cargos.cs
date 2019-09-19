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
    public class CD_coo_cargos
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        public DataTable dtCargos = new DataTable();
        public DataTable dtCargosCab = new DataTable();
        public DataTable dtCargosDet = new DataTable();
        public DataTable dtLista = new DataTable();
        public DataTable Listar(int n_IdEmpresa, int n_AnoTrabajo, int n_mesTrabajo)
        {
            DataTable DtResultado = new DataTable();
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_anotra", "System.INT16", n_AnoTrabajo.ToString()},
                                            {"n_mestra", "System.INT16", n_mesTrabajo.ToString()}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("coo_cargos_select", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public void GenerarCargos(int n_IdEmpresa, int n_anotra, int n_mestra, string c_glosa, string c_fchini, string c_fchfin, int n_idtipdoc)
        {
            //coo_cargos_generarcargomensualcab
            //coo_cargos_generarcargomensual
            DataTable DtResultado = new DataTable();
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[7, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_anotra", "System.INT16", n_anotra.ToString()},
                                            {"n_mestra", "System.INT16", n_mestra.ToString()},
                                            {"c_glosa", "System.STRING", c_glosa.ToString()},
                                            {"c_fchini", "System.STRING", c_fchini.ToString()},
                                            {"c_fchfin", "System.STRING", c_fchfin.ToString()},
                                            {"n_idtipdoc", "System.INT16", n_idtipdoc.ToString()}
                                      };

            dtCargosCab = xMiFuncion.StoreDTLLenar("coo_cargos_generarcargomensualcab", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            dtCargosDet = xMiFuncion.StoreDTLLenar("coo_cargos_generarcargomensual", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return;
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            BE_COO_PUESTOS entPuestos = new BE_COO_PUESTOS();
            DatosMySql xMiFuncion = new DatosMySql();

            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT32", n_IdRegistro.ToString()}
                                      };
            dtCargos = xMiFuncion.StoreDTLLenar("coo_cargos_obtenerregistro", arrParametros, mysConec);
            if (xMiFuncion.booOcurrioError == false)
            {
                string[,] arrParametros2 = new string[1, 3] {
                                                {"n_idcar", "System.INT32", n_IdRegistro.ToString()}
                                          };
                dtCargosCab = xMiFuncion.StoreDTLLenar("coo_cargoscab_listar", arrParametros2, mysConec);
                if (xMiFuncion.IntErrorNumber != 0)
                {
                    dtCargosCab = null;
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                    return;
                }

                dtCargosDet = xMiFuncion.StoreDTLLenar("coo_cargosdet_listar", arrParametros2, mysConec);
                if (xMiFuncion.IntErrorNumber != 0)
                {
                    dtCargosDet = null;
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                    return;
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
        public bool Insertar(BE_COO_CARGOS entCargos, List<BE_COO_CARGOSCAB> LstCargosCab, List<BE_COO_CARGOSDET> LstCargosDet)
        {
            bool booOk = false;
            int n_row = 0;
            DatosMySql xMiFuncion = new DatosMySql();

            if (xMiFuncion.StoreEjecutar("coo_cargos_insertar", entCargos, mysConec, 1) == true)
            {
                entCargos.n_id = Convert.ToInt32(xMiFuncion.intIdGenerado);
                for (n_row = 0; n_row <= LstCargosCab.Count - 1; n_row++)
                {
                    BE_COO_CARGOSCAB entCargosCab = new BE_COO_CARGOSCAB();

                    entCargosCab = LstCargosCab[n_row];
                    entCargosCab.n_idcar = entCargos.n_id;
                    if (xMiFuncion.StoreEjecutar("coo_cargoscab_insertar", entCargosCab, mysConec, null) == false)
                    {
                        booOcurrioError = xMiFuncion.booOcurrioError;
                        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        IntErrorNumber = xMiFuncion.IntErrorNumber;
                        return booOk;
                    }
                }

                for (n_row = 0; n_row <= LstCargosDet.Count - 1; n_row++)
                {
                    BE_COO_CARGOSDET entCargosDet = new BE_COO_CARGOSDET();
                    entCargosDet = LstCargosDet[n_row];
                    entCargosDet.n_idcar = entCargos.n_id;
                    if (xMiFuncion.StoreEjecutar("coo_cargosdet_insertar", entCargosDet, mysConec, null) == false)
                    {
                        booOcurrioError = xMiFuncion.booOcurrioError;
                        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        IntErrorNumber = xMiFuncion.IntErrorNumber;
                        return booOk;
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
        public bool Actualizar(BE_COO_CARGOS entCargos, List<BE_COO_CARGOSCAB> LstCargosCab, List<BE_COO_CARGOSDET> LstCargosDet)
        {
            bool booOk = false;
            int n_row = 0;
            DatosMySql xMiFuncion = new DatosMySql();

            string[,] arrParametros = new string[1, 3] {
                                            {"n_idcar", "System.INT16", entCargos.n_id.ToString()}
                                      };

            booOk = xMiFuncion.StoreEjecutar("coo_cargosdet_delete", arrParametros, mysConec);
            if (booOk == true)
            {
                booOk = xMiFuncion.StoreEjecutar("coo_cargoscab_delete", arrParametros, mysConec);
                if (booOk == false)
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


            if (xMiFuncion.StoreEjecutar("coo_cargos_actualizar", entCargos, mysConec, null) == true)
            {
                for (n_row = 0; n_row <= LstCargosCab.Count; n_row++)
                {
                    BE_COO_CARGOSCAB entCargosCab = new BE_COO_CARGOSCAB();
                    entCargosCab = LstCargosCab[n_row];
                    if (xMiFuncion.StoreEjecutar("coo_cargoscab_insertar", entCargosCab, mysConec, 1) == false)
                    {
                        booOcurrioError = xMiFuncion.booOcurrioError;
                        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        IntErrorNumber = xMiFuncion.IntErrorNumber;
                        return booOk;
                    }
                }

                for (n_row = 0; n_row <= LstCargosDet.Count; n_row++)
                {
                    BE_COO_CARGOSDET entCargosDet = new BE_COO_CARGOSDET();
                    entCargosDet = LstCargosDet[n_row];
                    if (xMiFuncion.StoreEjecutar("coo_cargosdet_insertar", entCargosDet, mysConec, 1) == false)
                    {
                        booOcurrioError = xMiFuncion.booOcurrioError;
                        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        IntErrorNumber = xMiFuncion.IntErrorNumber;
                        return booOk;
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
        public bool Eliminar(int n_IdCargo)
        {
            DatosMySql xMiFuncion = new DatosMySql();
            bool booOk = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_idcar", "System.INT16", n_IdCargo.ToString()}
                                      };

            booOk = xMiFuncion.StoreEjecutar("coo_cargosdet_delete", arrParametros, mysConec);
            if (booOk == true)
            {
                booOk = xMiFuncion.StoreEjecutar("coo_cargoscab_delete", arrParametros, mysConec);
                if (booOk == false)
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

            string[,] arrParametros2 = new string[1, 3] {
                                            {"n_id", "System.INT16", n_IdCargo.ToString()}
                                      };

            booOk = xMiFuncion.StoreEjecutar("coo_cargos_delete", arrParametros2, mysConec);

            if (booOk == true)
            {
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
        public void Consulta1(int n_IdPuesto)
        {

            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idsocpue", "System.INT16", n_IdPuesto.ToString()},
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("coo_cargos_consulta1", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {

                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public void Consulta3(int n_IdPuesto)
        {

            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idsoc", "System.INT16", n_IdPuesto.ToString()},
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("coo_cargos_consulta2", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {

                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public void Consulta2(string c_CadenaIN)
        {
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[1, 3] {
                                            {"c_doc", "System.STRING", c_CadenaIN.ToString()},
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("coo_cargosdet_traerdetalle", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {

                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public void ObtenerMesValido(int n_IdEmpresa, int n_AnoTrabajo, int n_IdTipoDocumento)
        {
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.STRING", n_IdEmpresa.ToString()},
                                            {"n_anotra", "System.STRING", n_AnoTrabajo.ToString()},
                                            {"n_idtipdoc", "System.STRING", n_IdTipoDocumento.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("coo_cargos_obtenerultimomes", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return;
        }
        //public bool EliminarConcepto(int n_Idcargo, int n_IdPuesto, int n_IdConcepto, int IdSocio)
        //{
        //    DatosMySql xMiFuncion = new DatosMySql();
        //    bool booOk = false;
        //    MySqlTransaction trans;

        //    trans = mysConec.BeginTransaction();
        //    try
        //    {
        //        string[,] arrParametros = new string[4, 3] {
        //                                    {"n_idcar", "System.INT16", n_Idcargo.ToString()},
        //                                    {"n_idpue", "System.INT16", n_IdPuesto.ToString()},
        //                                    {"n_idcon", "System.INT16", n_IdConcepto.ToString()},
        //                                    {"n_idsoc", "System.INT16", IdSocio.ToString()}
        //                              };

        //        booOk = xMiFuncion.StoreEjecutar("coo_cargosdet_deleteconcepto", arrParametros, mysConec);
        //        if (booOk == false)
        //        {
        //            booOcurrioError = xMiFuncion.booOcurrioError;
        //            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
        //            IntErrorNumber = xMiFuncion.IntErrorNumber;
        //            return booOk;
        //        }
        //        booOk = true;
        //        trans.Commit();
        //        return booOk;
        //    }
        //    catch (Exception exc)
        //    {
        //        // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
        //        booOcurrioError = true;
        //        StrErrorMensaje = exc.Message.ToString();
        //        IntErrorNumber = exc.HResult;
        //        trans.Rollback();
        //        return booOk;
        //    }
        //}
        public bool EliminarConcepto(int n_IdCargo, int n_IdCargoDet)
        {
            DatosMySql xMiFuncion = new DatosMySql();
            bool booOk = false;
            MySqlTransaction trans;

            trans = mysConec.BeginTransaction();
            try
            {
                string[,] arrParametros = new string[2, 3] {
                                            {"n_idcar", "System.INT16", n_IdCargo.ToString()},
                                            {"n_idcordet", "System.INT16", n_IdCargoDet.ToString()},
                                      };

                booOk = xMiFuncion.StoreEjecutar("coo_cargosdet_deleterecibo", arrParametros, mysConec);
                if (booOk == true)
                {
                    booOk = xMiFuncion.StoreEjecutar("coo_cargost_deleterecibo", arrParametros, mysConec);
                    if (booOk == true)
                    {
                        booOcurrioError = xMiFuncion.booOcurrioError;
                        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        IntErrorNumber = xMiFuncion.IntErrorNumber;
                        return booOk;
                    }
                }
                else
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                    return booOk;
                }
                booOk = true;
                trans.Commit();
                return booOk;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = true;
                StrErrorMensaje = exc.Message.ToString();
                IntErrorNumber = exc.HResult;
                trans.Rollback();
                return booOk;
            }
        }
    }
}
