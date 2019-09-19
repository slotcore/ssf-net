using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Ventas;

namespace SIAC_DATOS.Ventas
{
    public class CD_vta_boletaresumen
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        DatosMySql xMiFuncion = new DatosMySql();
        public DataTable dtLista = new DataTable();
        public DataTable dtListaDet = new DataTable();

        public void Listar(int n_idempresa, int n_anotrabajo, int n_mestrabajo)
        {
            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT16",n_idempresa.ToString()},
                                            {"n_ano", "System.INT16",n_anotrabajo.ToString()},
                                            {"n_mes", "System.INT16",n_mestrabajo.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("vta_boletaresumen_select", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtLista = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            BE_VTA_CHOFER Ent_Chofer = new BE_VTA_CHOFER();
            DatosMySql xMiFuncion = new DatosMySql();

            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT16", n_IdRegistro.ToString()}
                                      };
            dtLista = xMiFuncion.StoreDTLLenar("vta_boletaresumen_obtenerregistro", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtLista = null;
            }

            string[,] arrParametros2 = new string[1, 3] {
                                            {"n_idres", "System.INT16", n_IdRegistro.ToString()}
                                      };
            dtListaDet = xMiFuncion.StoreDTLLenar("vta_boletaresumendet_listar", arrParametros2, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtListaDet = null;
            }

            return;
        }
        public bool Insertar(BE_VTA_BOLETARESUMEN e_boleta, List<BE_VTA_BOLETARESUMENDET> l_boletadet)
        {
            bool b_Ok = false;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans = null;

            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            try
            {
                trans = mysConec.BeginTransaction();
                if (xMiFuncion.StoreEjecutar("vta_boletaresumen_insertar", e_boleta, mysConec, 1) == true)
                {
                    int n_idgen = Convert.ToInt32(xMiFuncion.intIdGenerado);
                    int n_row = 0;
                    for (n_row = 0; n_row <= l_boletadet.Count - 1; n_row++)
                    {
                        l_boletadet[n_row].n_idres = n_idgen;
                        if (xMiFuncion.StoreEjecutar("vta_boletaresumendet_insertar", l_boletadet[n_row], mysConec, 2) == false)
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return b_Ok;
                        }
                    }

                    b_Ok = true;
                }
                else
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return b_Ok;
                }

                trans.Commit();
                return b_Ok;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = true;
                StrErrorMensaje = exc.Message.ToString();
                IntErrorNumber = exc.HResult;
                trans.Rollback();
                return b_Ok;
            }
        }
        public bool Actualizar(BE_VTA_BOLETARESUMEN e_boleta, List<BE_VTA_BOLETARESUMENDET> l_boletadet)
        {
            bool b_Ok = false;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans = null;
            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            try
            {
                string[,] arrParametros2 = new string[1, 3] {
                                            {"n_idres", "System.INT16", e_boleta.n_id.ToString()}
                                      };
                if (xMiFuncion.StoreEjecutar("vta_boletaresumendet_delete", arrParametros2, mysConec, 2) == false)
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return b_Ok;
                }

                trans = mysConec.BeginTransaction();
                if (xMiFuncion.StoreEjecutar("vta_boletaresumen_actualizar", e_boleta, mysConec, null) == true)
                {
                    int n_row = 0;
                    for (n_row = 0; n_row <= l_boletadet.Count - 1; n_row++)
                    {
                        l_boletadet[n_row].n_idres = e_boleta.n_id;
                        if (xMiFuncion.StoreEjecutar("vta_boletaresumendet_insertar", l_boletadet[n_row], mysConec, 2) == false)
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return b_Ok;
                        }
                    }

                    b_Ok = true;
                }
                else
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return b_Ok;
                }

                trans.Commit();
                return b_Ok;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = true;
                StrErrorMensaje = exc.Message.ToString();
                IntErrorNumber = exc.HResult;
                trans.Rollback();
                return b_Ok;
            }
        }
        public bool Eliminar(int n_IdItem)
        {
            bool b_Ok = false;
            MySqlTransaction trans = null;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT16", n_IdItem.ToString()}
                                      };

            DatosMySql FunMysql = new DatosMySql();

            try
            {
                trans = mysConec.BeginTransaction();
                mysConec = FunMysql.ReAbrirConeccion(mysConec);

                b_Ok = xMiFuncion.StoreEjecutar("vta_boletaresumen_delete", arrParametros, mysConec);
                if (b_Ok == false)
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                    b_Ok = true;
                    trans.Rollback();
                    return b_Ok;
                }
                b_Ok = true;
                trans.Commit();
                return b_Ok;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = true;
                StrErrorMensaje = exc.Message.ToString();
                IntErrorNumber = exc.HResult;
                trans.Rollback();
                return b_Ok;
            }
        }
        public void Consulta1(int n_IdRegistro)
        {
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idres", "System.INT16", n_IdRegistro.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("vta_boletaresumendet_boletaresumen_rdi", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtLista = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
    }
}
