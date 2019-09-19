using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades.Ventas;
using SIAC_DATOS.Ventas;
using SIAC_DATOS.Logistica;
using MySql.Data.MySqlClient;
using Helper;
namespace SIAC_DATOS.Ventas
{
    public class CD_vta_pedidocen
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtLista = new DataTable();
        public DataTable dtDetalle = new DataTable();

        DatosMySql xMiFuncion = new DatosMySql();
        public void Listar(int n_IdEmpresa, int n_IdMes, int n_AnoTrabajo)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT16",n_IdEmpresa.ToString()},
                                            {"n_mestra", "System.INT16",n_IdMes.ToString()},
                                            {"n_anotra", "System.INT16",n_AnoTrabajo.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("vta_pedidocen_select", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return;
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            BE_VTA_GUIAS Ent_Guias = new BE_VTA_GUIAS();
            DatosMySql xMiFuncion = new DatosMySql();

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT16", n_IdRegistro.ToString()}
                                      };
            dtLista = null;
            dtLista = xMiFuncion.StoreDTLLenar("vta_pedidocen_obtenerregistro", arrParametros, mysConec);
            if (xMiFuncion.booOcurrioError == true)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return;
            }

            arrParametros[0, 0] = "n_idped";
            dtDetalle = xMiFuncion.StoreDTLLenar("vta_pedidocendet_select", arrParametros, mysConec);
            if (xMiFuncion.booOcurrioError == true)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return;
            }
            return;
        }
        public bool Insertar(BE_VTA_PEDIDOCEN entPedido, List<BE_VTA_PEDIDOCENDET> lstPedidoDet)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans;
            int n_row = 0;

            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            trans = mysConec.BeginTransaction();
            try
            {
                if (xMiFuncion.StoreEjecutar("vta_pedidocen_insertar", entPedido, mysConec, 1) == true)
                {
                    for (n_row = 0; n_row <= lstPedidoDet.Count - 1; n_row++)
                    {
                        lstPedidoDet[n_row].n_idped = Convert.ToInt32(xMiFuncion.intIdGenerado); 
                        if (xMiFuncion.StoreEjecutar("vta_pedidocendet_insertar", lstPedidoDet[n_row], mysConec, 1) == false)
                        {
                            b_OcurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            break;
                        }
                    }
                }
                else
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                }
                booOk = true;
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
        public bool Actualizar(BE_VTA_PEDIDOCEN entPedido, List<BE_VTA_PEDIDOCENDET> lstPedidoDet)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans;

            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            trans = mysConec.BeginTransaction();
            try
            {
                if (xMiFuncion.StoreEjecutar("vta_pedidocen_actualizar", entPedido, mysConec, null) == false)
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
                b_OcurrioError = true;
                c_ErrorMensaje = exc.Message.ToString();
                n_ErrorNumber = exc.HResult;
                trans.Rollback();
                return booOk;
            }
        }
        public bool Eliminar(int n_IdItem)
        {
            bool booResult = false;
            MySqlTransaction trans;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT16", n_IdItem.ToString()}
                                      };

            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            trans = mysConec.BeginTransaction();
            try
            {
                if (xMiFuncion.StoreEjecutar("vta_pedidocen_eliminar", arrParametros, mysConec) == true)
                {
                    arrParametros[0, 0] = "n_idped";
                    if (xMiFuncion.StoreEjecutar("vta_pedidocendet_delete", arrParametros, mysConec) == false)
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
                booResult = true;
                trans.Commit();
                return booResult;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                b_OcurrioError = true;
                c_ErrorMensaje = exc.Message.ToString();
                n_ErrorNumber = exc.HResult;
                trans.Rollback();
                return booResult;
            }
        }
        public bool ActualizarGuiaDespacho(int n_IdPedido, int n_IdGuia, int n_Estado)
        {
            bool b_result = false;
            MySqlTransaction trans;
            string[,] arrParametros = new string[3, 3] {
                                            {"n_id", "System.INT32", n_IdPedido.ToString()},
                                            {"n_idgui", "System.INT32",n_IdGuia.ToString()},
                                            {"n_estado", "System.INT32",n_Estado.ToString()}
                                      };

            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            trans = mysConec.BeginTransaction();
            try
            {
                b_result = xMiFuncion.StoreEjecutar("vta_pedidocen_actualisarguiadespacho", arrParametros, mysConec);

                if (xMiFuncion.IntErrorNumber != 0)
                {
                    trans.Rollback();
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                }
                else
                {
                    CD_vta_guias objGui = new CD_vta_guias();
                    objGui.mysConec = mysConec;

                    DataTable dtResul = new DataTable();
                    TraerRegistro(n_IdPedido);
                    dtResul = dtLista;

                    if (n_Estado == 1)                 // SI SE ESTA ELIMINADO EL DESPACHO, ELIMINAMOS LA GUIA
                    { 
                        //if (objGui.Eliminar(Convert.ToInt32(dtResul.Rows[0]["n_idguides"])) == false)
                        //{
                        //    trans.Rollback();
                        //    b_OcurrioError = objGui.booOcurrioError;
                        //    c_ErrorMensaje = objGui.StrErrorMensaje;
                        //    n_ErrorNumber = objGui.IntErrorNumber;
                        //}
                    }
                    b_result = true;
                }
                trans.Commit();
                return b_result;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                b_OcurrioError = true;
                c_ErrorMensaje = exc.Message.ToString();
                n_ErrorNumber = exc.HResult;
                trans.Rollback();
                return b_result;
            }
        }
        public void TraerDetallePedidos(int n_IdEmpresa, string c_CadenaIN)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"c_cadin", "System.STRING", c_CadenaIN.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("vta_pedidocendet_traerpedidos", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return;
        }
        public void TraerPendienteEnvio(int n_IdEmpresa, int n_IdMes, int n_AnoTrabajo)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT16",n_IdEmpresa.ToString()},
                                            {"n_idmes", "System.INT16",n_IdMes.ToString()},
                                            {"n_idano", "System.INT16",n_AnoTrabajo.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("vta_pedidocen_obtenersinguia", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return;
        }
        public void Consulta1(int n_IdEmpresa, string c_NumeroOrden)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT16",n_IdEmpresa.ToString()},
                                            {"c_numord", "System.STRING",c_NumeroOrden.ToString()},
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("vta_pedidocen_consulta1", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return;
        }
        public void Consulta2(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo)
        {
            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_anotra", "System.STRING", n_AnoTrabajo.ToString()},
                                            {"n_mestra", "System.STRING", n_MesTrabajo.ToString()},
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("vta_pedidocen_consulta3", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtLista = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return;
            }
            return;
        }
    }
}
