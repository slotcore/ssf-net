using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Gestion;

namespace SIAC_DATOS.Gestion
{
    public class CD_ges_planventas
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        
        DatosMySql xMiFuncion = new DatosMySql();
        
        private List<BE_GES_PLANVENTASDET> _lstDetalle;
        private BE_GES_PLANVENTAS _entCabecera;
        private List<BE_GES_PLANVENTASANOS> _lstDetalleAnos;
        private DataTable _dtCabecera;
        private DataTable _dtLista;
        private DataTable _dtDetalle;
        private DataTable _dtDetalleAnos;
        private DataTable _dtItems;
        private DataTable _dtDataAnos;
        private DataTable _dtVenHis;
        private DataTable _dtOrdenMes;
        
        public List<BE_GES_PLANVENTASDET> lstDetalle
        {
            get { return _lstDetalle; }
            set { _lstDetalle = value; }
        }
        public BE_GES_PLANVENTAS entCabecera
        {
            get { return _entCabecera; }
            set { _entCabecera = value; }
        }
        public List<BE_GES_PLANVENTASANOS> lstDetalleAnos
        {
            get { return _lstDetalleAnos; }
            set { _lstDetalleAnos = value; }
        }
        public DataTable dtCabecera
        {
            get { return _dtCabecera; }
            set { _dtCabecera = value; }
        }
        public DataTable dtDetalle
        {
            get { return _dtDetalle; }
            set { _dtDetalle = value; }
        }
        public DataTable dtDetalleAnos
        {
            get { return _dtDetalleAnos; }
            set { _dtDetalleAnos = value; }
        }
        public DataTable dtItems
        {
            get { return _dtItems; }
            set { _dtItems = value; }
        }
        public DataTable dtDataAnos
        {
            get { return _dtDataAnos; }
            set { _dtDataAnos = value; }
        }
        public DataTable dtVenHis
        {
            get { return _dtVenHis; }
            set { _dtVenHis = value; }
        }            
        public DataTable dtLista
        {
            get { return _dtLista; }
            set { _dtLista = value; }
        }
        public DataTable dtOrdenMes
        {
            get { return _dtOrdenMes; }
            set { _dtOrdenMes = value; } 
        }
        
        public void Listar(int n_IdEmpresa, int n_AnoTrabajo)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT16",n_IdEmpresa.ToString()},
                                            {"n_idano", "System.INT16",n_AnoTrabajo.ToString()}
                                            //{"n_idmes", "System.INT16",n_MesTrabajo.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("ges_planventas_select", arrParametros, mysConec);
            
            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idreg", "System.INT16", n_IdRegistro.ToString()}
                                      };
            string[,] arrParametros2 = new string[1, 3] {
                                            {"n_idplaven", "System.INT16", n_IdRegistro.ToString()}
                                      };
            try
            {
                dtCabecera = xMiFuncion.StoreDTLLenar("ges_planventas_obtenerregistro", arrParametros, mysConec);
                dtDetalle = xMiFuncion.StoreDTLLenar("ges_planventasdet_obtenerregistro", arrParametros2, mysConec);
                dtDetalleAnos = xMiFuncion.StoreDTLLenar("ges_planventasanos_obtenerregistro", arrParametros, mysConec);
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                return;
            }
        }
        public bool Insertar()
        {
            int n_fila = 0;
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans;

            trans = mysConec.BeginTransaction();

            try
            {
                if (xMiFuncion.StoreEjecutar("ges_planventas_insertar", entCabecera, mysConec, 1) == true)
                {
                    for (n_fila = 0; n_fila <= lstDetalle.Count-1; n_fila++)
                    {
                        // AGREGAMOS LOS ITEMS
                        lstDetalle[n_fila].n_idplan = Convert.ToInt32(xMiFuncion.intIdGenerado);
                        if (xMiFuncion.StoreEjecutar("ges_planventasdet_insertar", lstDetalle[n_fila], mysConec, null) == true)
                        {
                            booOk = true;
                        }
                        else
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                        }
                    }

                    // AGREGAMOS LOS AÑOS DEL HISTORICO
                    for (n_fila = 0; n_fila <= lstDetalleAnos.Count - 1; n_fila++)
                    {
                        lstDetalleAnos[n_fila].n_idplan = Convert.ToInt32(xMiFuncion.intIdGenerado); 
                        if (xMiFuncion.StoreEjecutar("ges_planventasanos_insertar", lstDetalleAnos[n_fila], mysConec, null) == true)
                        {
                            booOk = true;
                        }
                        else
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                        }
                    }                   
                }
                else
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                }
                trans.Commit();
                return booOk;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                trans.Rollback();
                return booOk;
            }
        }
        public bool Actualizar()
        {
            int n_fila = 0;
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans;

            trans = mysConec.BeginTransaction();
            string[,] arrParametros2 = new string[1, 3] {
                                            {"n_idplan", "System.INT16", entCabecera.n_id.ToString()}
                                       };
            try
            {
                // ELIMINAMOS EL DETALLE
                if (xMiFuncion.StoreEjecutar("ges_planventasdet_delete", arrParametros2, mysConec)== false)
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return booOk;
                }

                // ELIMINAMOS LOS AÑOS CARGADOS
                if (xMiFuncion.StoreEjecutar("ges_planventasanos_delete", arrParametros2, mysConec) == false)
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return booOk;
                }

                if (xMiFuncion.StoreEjecutar("ges_planventas_actualizar", entCabecera, mysConec, null) == true)
                {
                    for (n_fila = 0; n_fila <= lstDetalle.Count - 1; n_fila++)
                    {
                        // AGREGAMOS LOS ITEMS
                        lstDetalle[n_fila].n_idplan = entCabecera.n_id;
                        if (xMiFuncion.StoreEjecutar("ges_planventasdet_insertar", lstDetalle[n_fila], mysConec, null) == true)
                        {
                            booOk = true;
                        }
                        else
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return booOk;
                        }
                    }

                    // AGREGAMOS LOS AÑOS DEL HISTORICO
                    for (n_fila = 0; n_fila <= lstDetalleAnos.Count - 1; n_fila++)
                    {
                        lstDetalleAnos[n_fila].n_idplan = entCabecera.n_id;
                        if (xMiFuncion.StoreEjecutar("ges_planventasanos_insertar", lstDetalleAnos[n_fila], mysConec, null) == true)
                        {
                            booOk = true;
                        }
                        else
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return booOk;
                        }
                    }                   
                }
                else
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return booOk;
                }
                trans.Commit();
                return booOk;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
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
            string[,] arrParametros2 = new string[1, 3] {
                                            {"n_idplan", "System.INT16", n_IdItem.ToString()}
                                      };
            trans = mysConec.BeginTransaction();

            try
            {
                booResult = xMiFuncion.StoreEjecutar("ges_planventasdet_delete", arrParametros2, mysConec);
                if (booResult == true)
                {
                    booResult = xMiFuncion.StoreEjecutar("ges_planventasanos_delete", arrParametros2, mysConec);
                    if (booResult == true)
                    {
                        booResult = xMiFuncion.StoreEjecutar("ges_planventas_delete", arrParametros, mysConec);
                        if (booResult == false)
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
                }
                else
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;   
                }
                trans.Commit();
                return booResult;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                trans.Rollback();
                return booResult;
            }
        }
        public void VentasAnuales(int n_IdEmpresa, int n_AnoTrabajo)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_anotra", "System.INT16", n_AnoTrabajo.ToString()},
                                            {"c_cadin", "System.STRING", ""}           	
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("vta_ventas_anuales", arrParametros, mysConec);
            
            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

        }
        public void VentasItemxPorAnos(int n_IdEmpresa, int n_IdItem)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_idite", "System.INT16", n_IdItem.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("vta_ventas_itemsporanos", arrParametros, mysConec);
            
            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
        }
        public void Consulta1(int n_IdEmpresa, string c_CadenaIn)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"c_cadin", "System.STRING", c_CadenaIn.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("ges_planventas_listarproductos", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
        }
        //call ges_planventas_listarproductos(2, '2331,2380,1735,1572')
        public void TraerDataAnos(int n_IdEmpresa)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("ges_planventas_ventasanosdata", arrParametros, mysConec);
            dtDataAnos = DtResultado;
            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
        }
        public void TraerVentaHistorica(int n_IdEmpresa, int n_NumMesesConsultar, string c_CadenaIN)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT16",n_IdEmpresa.ToString()},
                                            {"n_nummeses", "System.INT16",n_NumMesesConsultar.ToString()},
                                            {"c_cadin", "System.STRING",c_CadenaIN.ToString()}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("ges_planventas_historicompranos_resumen", arrParametros, mysConec);
            dtVenHis = DtResultado;
            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
        }
        public void TraerVentaHistoricaDetalle(int n_IdEmpresa, string c_CadenaIN)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT16",n_IdEmpresa.ToString()},
                                            {"c_anos", "System.STRING",c_CadenaIN.ToString()}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("ges_planventas_historicoporanos", arrParametros, mysConec);
            dtDetalleAnos = DtResultado;
            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
        }
        public void OrdenMeses(int n_IdRegistro)
        { 
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idreg", "System.INT16", n_IdRegistro.ToString()}
                                      };
            try
            {
                dtOrdenMes = xMiFuncion.StoreDTLLenar("ges_planventasdet_obtenerordenmes", arrParametros, mysConec);
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                return;
            }
        }
        public void Consulta1(int n_IdEmpresa, int n_Estado)
        {
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_estado", "System.INT16", n_Estado.ToString()}
                                            
                                      };
            try
            {
                dtLista = xMiFuncion.StoreDTLLenar("ges_planventas_consulta1", arrParametros, mysConec);
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                return;
            }
        }
        public void Consulta2(int n_IdPlanVenta)
        {
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idplaven", "System.INT16", n_IdPlanVenta.ToString()}
                                      };
            try
            {
                dtLista = xMiFuncion.StoreDTLLenar("ges_planventasdet_obtenerregistro", arrParametros, mysConec);
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                return;
            }
        }
        public void CambiarEstadoPlanVentas(int n_IdPlanVenta, int n_Estado)
        {
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idplaven", "System.INT16", n_IdPlanVenta.ToString()},
                                            {"n_estado", "System.INT16", n_Estado.ToString()}
                                      };

            MySqlTransaction trans;

            trans = mysConec.BeginTransaction();
            try
            {
                if (xMiFuncion.StoreEjecutar("ges_planventas_cambiarestado", arrParametros, mysConec, null) == false)
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return;
                }
                trans.Commit();
                return;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                trans.Rollback();
                return;
            }
        }
        public void ListarActivos(int n_IdEmpresa)
        {
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()}
                                      };
            try
            {
                dtLista = xMiFuncion.StoreDTLLenar("ges_planventas_listaractivos", arrParametros, mysConec);
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                return;
            }
        }
        public void PlanVentasUnificado()
        {
            string[,] arrParametros = new string[0, 3] {
                                      };
            try
            {
                dtLista = xMiFuncion.StoreDTLLenar("ges_planventas_unificado", arrParametros, mysConec);
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                return;
            }
        }
    }
}
