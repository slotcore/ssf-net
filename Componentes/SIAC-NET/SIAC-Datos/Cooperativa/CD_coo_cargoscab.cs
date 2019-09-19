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
    public class CD_coo_cargoscab
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        public DataTable dtCargosCab = new DataTable();    
        public void Consulta1(int n_idCargo, int n_Inicio)
        {
            DataTable DtResultado = new DataTable();
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idcar", "System.INT16", n_idCargo.ToString()},
                                            {"n_ini", "System.INT16", n_Inicio.ToString()}
                                      };

            dtCargosCab = xMiFuncion.StoreDTLLenar("coo_cargoscab_consulta1", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public bool ActualizarNumeracion(int n_IdEmpresa, int n_IdCargo, int n_IdSocio, int n_IdSocioPuesto, string c_NumeroDocumento)
        {
            bool b_Result = false;
            DataTable DtResultado = new DataTable();
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[5, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_idcar", "System.INT16", n_IdCargo.ToString()},
                                            {"n_idsoc", "System.INT16", n_IdSocio.ToString()},
                                            {"n_idsocpue", "System.INT16", n_IdSocioPuesto.ToString()},
                                            {"c_numdoc", "System.STRING", c_NumeroDocumento.ToString()}
                                      };

            if (xMiFuncion.StoreEjecutar("coo_cargoscab_actualizarnumeracion", arrParametros, mysConec) == false)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                b_Result = true;
            }
            
            return b_Result;
        }
        public void ActualizarCargo(int n_IdCargo, int n_IdSocio, int n_IdPuesto, int n_IdConcepto, double n_IdVenta, double n_impabo)
        {
            DataTable DtResultado = new DataTable();
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[6, 3] {
                                            {"n_idsoc", "System.INT16", n_IdSocio.ToString()},
                                            {"n_idcar", "System.INT16", n_IdCargo.ToString()},
                                            {"n_idpue", "System.INT16", n_IdPuesto.ToString()},
                                            {"n_idcon", "System.INT16", n_IdConcepto.ToString()},
                                            {"n_idvta", "System.DOUBLE", n_IdVenta.ToString()},
                                            {"n_impabo", "System.DOUBLE", n_impabo.ToString()}
                                      };
            //coo_cargoscab_actualizarcargo
            if (xMiFuncion.StoreEjecutar("coo_cargoscab_actualizarcargo", arrParametros, mysConec) == false)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public bool InsertarCargo(BE_COO_CARGOSCAB entCargos, List<BE_COO_CARGOSDET> lstDetalle)
        {
            bool booOk = false;
            int n_row = 0;
            DatosMySql xMiFuncion = new DatosMySql();

            if (xMiFuncion.StoreEjecutar("coo_cargoscab_insertar", entCargos, mysConec, 16) == true)
            {
                for (n_row = 0; n_row <= lstDetalle.Count - 1; n_row++)
                {
                    BE_COO_CARGOSDET entCargosDet = new BE_COO_CARGOSDET();
                    entCargosDet = lstDetalle[n_row];
                    //entCargosDet.n_idcar = entCargos.n_id;
                    entCargosDet.n_idcor = Convert.ToInt32(xMiFuncion.intIdGenerado);
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
        public void ExtornarPago(int n_IdDocumentoPago, int n_IdEmpresa, int n_IdSocio)
        {
            DataTable DtResultado = new DataTable();
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[3, 3] {
                                            {"n_iddocpag", "System.INT16", n_IdDocumentoPago.ToString()},
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_idsoc", "System.INT16", n_IdSocio.ToString()},
                                      };

            if (xMiFuncion.StoreEjecutar("coo_cargoscab_extornarpago", arrParametros, mysConec) == false)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
    }
}
