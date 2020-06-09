using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Produccion;

namespace SIAC_DATOS.Produccion
{
    public class CD_pro_solicitudamateriales
    {
        private DataTable _dtLista;
        private DataTable _dtRegistro;
        private DataTable _dtRegistroDet;
        private DataTable _dtlstInsumos;
        
        public DataTable dtListaMatEnt;

        private bool _b_OcurrioError;
        private string _c_ErrorMensaje;
        private int _n_ErrorNumber;
        
        private MySqlConnection _mysConec;

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();
        public DataTable dtLista
        {
            get { return _dtLista; }
            set { _dtLista = value; }
        }
        public DataTable dtlstInsumos
        {
            get { return _dtlstInsumos; }
            set { _dtlstInsumos = value; }
        }
        public DataTable dtRegistro
        {
            get { return _dtRegistro; }
            set { _dtRegistro = value; }
        }
        public DataTable dtRegistroDet
        {
            get { return _dtRegistroDet; }
            set { _dtRegistroDet = value; }
        }
        public bool b_OcurrioError
        {
            get { return _b_OcurrioError; }
            set { _b_OcurrioError = value; }
        }
        public string c_ErrorMensaje
        {
            get { return _c_ErrorMensaje; }
            set { _c_ErrorMensaje = value; }
        }
        public int n_ErrorNumber
        {
            get { return _n_ErrorNumber; }
            set { _n_ErrorNumber = value; }
        }
        public MySqlConnection mysConec
        {
            get { return _mysConec; }
            set { _mysConec = value; }
        }
        public bool Listar(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo)
        {
            bool b_result = false;
            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"n_anotra", "System.INT32", n_AnoTrabajo.ToString()},
                                            {"n_mestra", "System.INT32", n_MesTrabajo.ToString()},
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("pro_solicitudmateriales_select", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                b_result = true;
            }
            return b_result;
        }
        public bool ListarSolicituddePrograma(int n_idPrograma, int n_idordenproduccion, int n_iditem, string c_fechaentrega)
        {
            bool b_result = false;
            string[,] arrParametros = new string[4, 3] {
                                            {"n_idpro", "System.INT32", n_idPrograma.ToString()},
                                            {"n_idordpro", "System.INT32", n_idordenproduccion.ToString()},
                                            {"n_idite", "System.INT32", n_iditem.ToString()},
                                            {"c_fchent", "System.STRING", c_fechaentrega.ToString()},
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("pro_solicitudmateriales_consulta1", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                b_result = true;
            }
            return b_result;
        }
        public bool TraerRegistro(Int64 n_IdRegistro)
        {
            bool b_result = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdRegistro.ToString()}
                                      };
            dtRegistro = xMiFuncion.StoreDTLLenar("pro_solicitudmateriales_obtenerregistro", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber == 0)
            {
                string[,] arrParametros2 = new string[1, 3] {
                                            {"n_idsol", "System.INT64", n_IdRegistro.ToString()}
                                      };
                dtRegistroDet = xMiFuncion.StoreDTLLenar("pro_solicitudmaterialesdet_select", arrParametros2, mysConec);
                if (xMiFuncion.IntErrorNumber != 0)
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                }
                else
                {
                    b_result = true;
                }
            }
            else
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return b_result;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool booResult = false;
            // PARAMETROS PARA ELIMINAR LOS INSUMOS Y LAS TAREAS
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idsol", "System.INT64", n_IdRegistro.ToString()}
                                      };

            if (xMiFuncion.StoreEjecutar("pro_solicitudmaterialesdet_delete", arrParametros, mysConec) == true)
            {
                arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdRegistro.ToString()}
                                      };
                if (xMiFuncion.StoreEjecutar("pro_solicitudmateriales_delete", arrParametros, mysConec) == true)
                {
                    booResult = true;
                }
                else
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
            return booResult;
        }
        public bool Insertar(BE_PRO_SOLICITUDMATERIALES entSolicitudU, List<BE_PRO_SOLICITUDMATERIALESDET> LstSolicitudDetU)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            int n_row = 0;
                                          
            if (xMiFuncion.StoreEjecutar("pro_solicitudmateriales_insertar", entSolicitudU, mysConec, 1) == true)
            {
                entSolicitudU.n_id = Convert.ToInt32(xMiFuncion.intIdGenerado);
                for (n_row = 0; n_row <= LstSolicitudDetU.Count - 1; n_row++)
                {
                    LstSolicitudDetU[n_row].n_idsol = entSolicitudU.n_id;
                    if (xMiFuncion.StoreEjecutar("pro_solicitudmaterialesdet_insertar", LstSolicitudDetU[n_row], mysConec, null) == false)
                    {
                        b_OcurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                        break;
                    }
                    booOk = true;
                }
            }
            else
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booOk;
        }
        public bool Actualizar(BE_PRO_SOLICITUDMATERIALES entSolicitudU, List<BE_PRO_SOLICITUDMATERIALESDET> LstSolicitudDetU)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            int n_row = 0;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idsol", "System.INT64", entSolicitudU.n_id.ToString()}
                                      };
            // BORRAMOS EL DETALLE DE LA SOLICITUD DE MATERIALES

            if (xMiFuncion.StoreEjecutar("pro_solicitudmaterialesdet_delete", arrParametros, mysConec) == true)
            { 
                // ACTUALIZAMOS LA CABECERA DE LA SOLICITUD DE MATERIALES
                if (xMiFuncion.StoreEjecutar("pro_solicitudamateriales_actualizar", entSolicitudU, mysConec, null) == true)
                {
                    // INSERTAMOS EL DETALLE DE LA SOLICITU DE MATERIALES
                    for (n_row = 0; n_row <= LstSolicitudDetU.Count - 1; n_row++)
                    {
                        LstSolicitudDetU[n_row].n_idsol = entSolicitudU.n_id;

                        if (xMiFuncion.StoreEjecutar("pro_solicitudmaterialesdet_insertar", LstSolicitudDetU[n_row], mysConec, null) == false)
                        {
                            b_OcurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            break;
                        }
                        booOk = true;
                    }
                }
                else
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
            return booOk;
        }
        public bool Consulta2(int n_IdOrdPro, int n_idite, int n_idrec, string c_FechaEntrega)
        {
            bool b_result = false;

            string[,] arrParametros = new string[4, 3] {
                                            {"n_idordpro", "System.INT64", n_IdOrdPro.ToString()},
                                            {"n_idite", "System.INT64", n_idite.ToString()},
                                            {"n_idrec", "System.INT64", n_idrec.ToString()},
                                            {"c_fchent", "System.STRING", c_FechaEntrega.ToString().Substring(0,10)}
                                      };
            dtLista = xMiFuncion.StoreDTLLenar("pro_solicitudmateriales_consulta2", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber == 0)
            {
                string[,] arrParametros2 = new string[4, 3] {
                                            {"n_idordpro", "System.INT64", n_IdOrdPro.ToString()},
                                            {"n_idite", "System.INT64", n_idite.ToString()},
                                            {"n_idrec", "System.INT64", n_idrec.ToString()},
                                            {"c_fchent", "System.STRING", c_FechaEntrega.ToString().Substring(0,10)}
                                     };

                dtlstInsumos = xMiFuncion.StoreDTLLenar("pro_solicitudmaterialesdet_consulta1", arrParametros2, mysConec);

                if (xMiFuncion.IntErrorNumber != 0)
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                }
                else
                {
                    b_result = true;
                }
            }
            else
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                
            }
            return b_result;
        }
        public bool Consulta3(int n_IdEmpresa)
        {
            bool b_result = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("pro_solicitudmateriales_consulta3", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                b_result = true;
            }
            return b_result;
        }
        public bool Consulta4(int n_idProduccion)
        { 
            bool b_result = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idpro", "System.INT32", n_idProduccion.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("pro_solicitudmateriales_consulta4", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                b_result = true;
            }
            return b_result;
        }
        public bool Consulta5(int n_idProduccion, int n_idProducto, int n_IdReceta, double n_CantidadProducto)
        { 
            bool b_result = false;
            string[,] arrParametros = new string[4, 3] {
                                            {"n_idproduccion", "System.INT32", n_idProduccion.ToString()},
                                            {"n_idproducto", "System.INT32", n_idProducto.ToString()},
                                            {"n_idrec", "System.INT32", n_IdReceta.ToString()},
                                            {"n_can", "System.DOUBLE", n_CantidadProducto.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("pro_solicitudmateriales_consulta5", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                b_result = true;
            }
            return b_result;
        }
        public bool Consulta6(int n_IdEmpresa)
        {
            bool b_result = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("pro_solicitudmateriales_consulta6", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                b_result = true;
            }
            return b_result;
        }
        public bool ConsultaDevoluciones(int n_IdEmpresa)
        {
            bool b_result = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("pro_solicitudmateriales_consulta_devoluciones", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                b_result = true;
            }
            return b_result;
        }
        public bool ConsultaAdicionales(int n_IdEmpresa)
        {
            bool b_result = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("pro_solicitudmateriales_consulta_adicionales", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                b_result = true;
            }
            return b_result;
        }
        public bool BuscarProduccionEnSolicitud(int n_idProduccion, int n_IdEmpresa)
        {
            bool b_result = false;
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idpro", "System.INT32", n_idProduccion.ToString()},
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("pro_solicitudmateriales_consulta7", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                b_result = true;
            }
            return b_result;
        }
        public bool ListarResumenSolMat(int n_IdProduccion, int n_IdEmpresa)
        {
            bool b_result = false;
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idpro", "System.INT32", n_IdProduccion.ToString()},
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()}
                                      };

            dtListaMatEnt = xMiFuncion.StoreDTLLenar("pro_solicitudmaterialesdet_listar", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                b_result = true;
            }
            return b_result;
        }
    }
}
