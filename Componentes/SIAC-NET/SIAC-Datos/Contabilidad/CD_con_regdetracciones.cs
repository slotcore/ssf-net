using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Contabilidad;
using SIAC_Entidades.Tesoreria;
using SIAC_DATOS.Tesoreria;

namespace SIAC_DATOS.Contabilidad
{
    public class CD_con_regdetracciones
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtLista = new DataTable();
        public DataTable dtDetalle = new DataTable();
        public int n_IdTesoreria = 0;                                    // DEVUELVE EL ID DE TESORERIA GENERADO AL MOMENTO DE PAGAR LA DETRACCION  

        public BE_TES_TESORERIA e_Tesoreria = new BE_TES_TESORERIA();
        public List<BE_TES_TESORERIAORI> l_TesOri = new List<BE_TES_TESORERIAORI>();
        public List<BE_TES_TESORERIADES> l_TesDes = new List<BE_TES_TESORERIADES>();
        public List<BE_TES_TESORERIADESDET> l_TesDesDet = new List<BE_TES_TESORERIADESDET>();
        public List<BE_TES_TESORERIAORIDET> l_TesOriDet = new List<BE_TES_TESORERIAORIDET>();

        DatosMySql xMiFuncion = new DatosMySql();
        public void Listar(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo, int n_TipoMovimiento)
        {
            string[,] arrParametros = new string[4, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"n_ano", "System.INT32", n_AnoTrabajo.ToString()},
                                            {"n_mes", "System.INT32", n_MesTrabajo.ToString()},
                                            {"n_tipo", "System.INT32", n_TipoMovimiento.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("con_regdetracciones_listar", arrParametros, mysConec);

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
            dtLista = xMiFuncion.StoreDTLLenar("con_regdetracciones_obtenerregistro", arrParametros, mysConec);
            if (xMiFuncion.booOcurrioError == true)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public bool Eliminar(int n_IdDetraccion, int n_IdTesoreria)
        {
            DatosMySql xMiFuncion = new DatosMySql();
            CD_tes_tesoreria funTes = new CD_tes_tesoreria();
            MySqlTransaction trans;
            bool booOk = false;

            xMiFuncion.ReAbrirConeccion(mysConec);

            trans = mysConec.BeginTransaction();
            try
            {
                string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT16", n_IdDetraccion.ToString()}
                                      };

                if (xMiFuncion.StoreEjecutar("con_regdetracciones_delete", arrParametros, mysConec) == false)
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                }
                else
                {
                    CD_con_regdetracciones o_det = new CD_con_regdetracciones();
                    DataTable dt = new DataTable();
                    o_det.mysConec = mysConec;
                    o_det.TraerRegistro(n_IdDetraccion);
                    dt = o_det.dtLista;

                    if (dt.Rows.Count != 0)
                    { 
                        string[,] arrParametros2 = new string[3, 3] {
                                                        {"n_idreg", "System.INT64", n_IdDetraccion.ToString()},
                                                        {"n_importe", "System.DOUBLE", Convert.ToDouble(dt.Rows[0]["n_imp"]).ToString()},
                                                        {"n_tipo", "System.INT16", "1"}        
                                                  };
                        // ACTUALIZAMOS EL SALDO DEL DOCUMENTO DE COMPRA
                        if (xMiFuncion.StoreEjecutar("log_compras_actualizarsaldo", arrParametros2, mysConec) == false)
                        {
                            b_OcurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return booOk;
                        }
                    }

                    if (n_IdTesoreria != 0)
                    { 
                        funTes.mysConec = mysConec;
                        funTes.b_DesdeOtraCapa = true;
                        if (funTes.Eliminar(n_IdTesoreria) == false)
                        {
                            b_OcurrioError = funTes.b_OcurrioError;
                            c_ErrorMensaje = funTes.c_ErrorMensaje;
                            n_ErrorNumber = funTes.n_ErrorNumber;
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
        public bool Insertar(BE_CON_REGDETRACCIONES e_Detracciones)
        {
            bool booOk = false;
            CD_tes_tesoreria funTes = new CD_tes_tesoreria();
            DatosMySql xMiFuncion = new DatosMySql();
            int n_idtes = 0;
            int n_idet = 0;
            MySqlTransaction trans;

            xMiFuncion.ReAbrirConeccion(mysConec);
            trans = mysConec.BeginTransaction();
            try
            {
                if (xMiFuncion.StoreEjecutar("con_regdetracciones_insertar", e_Detracciones, mysConec, 0) == false)
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return booOk;
                }
                else
                {
                    if (e_Detracciones.n_aplipag == 1)
                    {
                        n_idet = Convert.ToInt32(xMiFuncion.intIdGenerado);
                        // INSERTAMOS EL REGISTRO DE TESORERIA
                        funTes.b_DesdeOtraCapa = true;
                        funTes.mysConec = mysConec;
                        booOk = funTes.Insertar(e_Tesoreria, l_TesOri, l_TesOriDet, l_TesDes, l_TesDesDet);
                        if (booOk == false)
                        {
                            b_OcurrioError = funTes.b_OcurrioError;
                            c_ErrorMensaje = funTes.c_ErrorMensaje;
                            n_ErrorNumber = funTes.n_ErrorNumber;
                            trans.Rollback();
                            return booOk;
                        }
                        else
                        {
                            n_idtes = Convert.ToInt32(funTes.n_IdGenerado);
                            n_IdTesoreria = n_idtes;
                            string[,] arrParametros = new string[2, 3] {
                                                    {"n_iddet", "System.INT64",n_idet.ToString()},
                                                    {"n_idtes", "System.INT64",n_idtes.ToString()}
                                              };
                            // ACTUALIZAMOS EL ID DE TESORERIA EN LA TABLA DETRACCION
                            if (xMiFuncion.StoreEjecutar("con_regdetracciones_actualizar_id_tesoreria", arrParametros, mysConec) == false)
                            {
                                b_OcurrioError = xMiFuncion.booOcurrioError;
                                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                trans.Rollback();
                                return booOk;
                            }

                            // ACTUALIZAMOS EL SALDO DE LOS DOCUMETOS
                            if (e_Detracciones.n_tipmov == 1)
                            {
                                // ACTUALIZAMOS EL SALDO DE LA COMPRA
                                string[,] arrParametros2 = new string[3, 3] {
                                                        {"n_idreg", "System.INT64", e_Detracciones.n_iddoccom.ToString()},
                                                        {"n_importe", "System.DOUBLE", e_Detracciones.n_imp.ToString("0.00")},
                                                        {"n_tipo", "System.INT16", "2"}        
                                                  };
                                // ACTUALIZAMOS EL SALDO DEL DOCUMENTO DE COMPRA
                                if (xMiFuncion.StoreEjecutar("log_compras_actualizarsaldo2", arrParametros2, mysConec) == false)
                                {
                                    b_OcurrioError = xMiFuncion.booOcurrioError;
                                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                    trans.Rollback();
                                    return booOk;
                                }
                            }
                            else
                            {
                                // ACTUALIZAMOS EL SALDO DE LA VENTA
                                string[,] arrParametros3 = new string[3, 3] {
                                                        {"n_id", "System.INT64", e_Detracciones.n_iddoccom.ToString()},
                                                        {"n_saldo", "System.DOUBLE", e_Detracciones.n_imp.ToString("0.00")},
                                                        {"n_tipo", "System.INT16", "2"}        
                                                  };
                                // ACTUALIZAMOS EL SALDO DEL DOCUMENTO DE VENTA
                                if (xMiFuncion.StoreEjecutar("vta_ventas_actualizarsaldo2", arrParametros3, mysConec) == false)
                                {
                                    b_OcurrioError = xMiFuncion.booOcurrioError;
                                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                    trans.Rollback();
                                    return booOk;
                                }
                            }
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
        public bool Actualizar(BE_CON_REGDETRACCIONES e_Detracciones)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans;
            int n_idtes = 0;
            int n_idet = 0;
            CD_tes_tesoreria funTes = new CD_tes_tesoreria();

            xMiFuncion.ReAbrirConeccion(mysConec);
            trans = mysConec.BeginTransaction();
            try
            {
                CD_con_regdetracciones o_det = new CD_con_regdetracciones();
                DataTable dt = new DataTable();
                o_det.mysConec = mysConec;
                o_det.TraerRegistro(e_Detracciones.n_id);
                dt = o_det.dtLista;

                if (e_Detracciones.n_tipmov == 1)
                {
                    string[,] arrParametros3 = new string[3, 3] {
                                                        {"n_idreg", "System.INT64", e_Detracciones.n_iddoccom.ToString()},
                                                        {"n_importe", "System.DOUBLE", e_Detracciones.n_imp.ToString("0.00")},
                                                        {"n_tipo", "System.INT16", "1"}
                                                };
                    // ACTUALIZAMOS EL SALDO DEL DOCUMENTOD E COMPRA
                    if (xMiFuncion.StoreEjecutar("log_compras_actualizarsaldo2", arrParametros3, mysConec) == false)
                    {
                        b_OcurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                        trans.Rollback();
                        return booOk;
                    }
                }
                else
                {
                    string[,] arrParametros4 = new string[3, 3] {
                                                        {"n_id", "System.INT64", e_Detracciones.n_iddoccom.ToString()},
                                                        {"n_saldo", "System.DOUBLE", e_Detracciones.n_imp.ToString("0.00")},
                                                        {"n_tipo", "System.INT16", "1"}        
                                                  };
                    // ACTUALIZAMOS EL SALDO DEL DOCUMENTO DE VENTA
                    if (xMiFuncion.StoreEjecutar("vta_ventas_actualizarsaldo2", arrParametros4, mysConec) == false)
                    {
                        b_OcurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                        trans.Rollback();
                        return booOk;
                    }
                }
                if (xMiFuncion.StoreEjecutar("con_regdetracciones_actualizar", e_Detracciones, mysConec, null) == false)
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return booOk;
                }
                else
                {
                    if (e_Detracciones.n_aplipag == 1)
                    {
                        n_idet = e_Detracciones.n_id;
                        // INSERTAMOS EL REGISTRO DE TESORERIA
                        funTes.b_DesdeOtraCapa = true;
                        funTes.mysConec = mysConec;
                        if (e_Detracciones.n_idtes == 0)
                        {
                            booOk = funTes.Insertar(e_Tesoreria, l_TesOri, l_TesOriDet, l_TesDes, l_TesDesDet);
                        }
                        else
                        {
                            booOk = funTes.Actualizar(e_Tesoreria, l_TesOri, l_TesOriDet, l_TesDes, l_TesDesDet);
                        }
                        if (booOk == false)
                        {
                            b_OcurrioError = funTes.b_OcurrioError;
                            c_ErrorMensaje = funTes.c_ErrorMensaje;
                            n_ErrorNumber = funTes.n_ErrorNumber;
                            trans.Rollback();
                            return booOk;
                        }
                        else
                        {
                            n_idtes = funTes.n_IdGenerado; //e_Detracciones.n_idtes;
                            n_IdTesoreria = n_idtes;
                            string[,] arrParametros = new string[2, 3] {
                                                    {"n_iddet", "System.INT64",n_idet.ToString()},
                                                    {"n_idtes", "System.INT64",n_idtes.ToString()}
                                              };
                            // ACTUALIZAMOS EL ID DE TESORERIA EN LA TABLA DETRACCION
                            if (xMiFuncion.StoreEjecutar("con_regdetracciones_actualizar_id_tesoreria", arrParametros, mysConec) == false)
                            {
                                b_OcurrioError = xMiFuncion.booOcurrioError;
                                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                trans.Rollback();
                                return booOk;
                            }
                              
                            // ACTUALIZAMOS EL SALDO DE LOS DOCUMETOS
                            if (e_Detracciones.n_tipmov == 1)
                            {
                                // ACTUALIZAMOS EL SALDO DE LA COMPRA
                                string[,] arrParametros2 = new string[3, 3] {
                                                        {"n_idreg", "System.INT64", e_Detracciones.n_iddoccom.ToString()},
                                                        {"n_importe", "System.DOUBLE", e_Detracciones.n_imp.ToString("0.00")},
                                                        {"n_tipo", "System.INT16", "2"}        
                                                  };
                                // ACTUALIZAMOS EL SALDO DEL DOCUMENTO DE COMPRA
                                if (xMiFuncion.StoreEjecutar("log_compras_actualizarsaldo2", arrParametros2, mysConec) == false)
                                {
                                    b_OcurrioError = xMiFuncion.booOcurrioError;
                                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                    trans.Rollback();
                                    return booOk;
                                }
                            }
                            else
                            {
                                // ACTUALIZAMOS EL SALDO DE LA VENTA
                                string[,] arrParametros4 = new string[3, 3] {
                                                        {"n_id", "System.INT64", e_Detracciones.n_iddoccom.ToString()},
                                                        {"n_saldo", "System.DOUBLE", e_Detracciones.n_imp.ToString("0.00")},
                                                        {"n_tipo", "System.INT16", "2"}        
                                                  };
                                // ACTUALIZAMOS EL SALDO DEL DOCUMENTO DE VENTA
                                if (xMiFuncion.StoreEjecutar("vta_ventas_actualizarsaldo2", arrParametros4, mysConec) == false)
                                {
                                    b_OcurrioError = xMiFuncion.booOcurrioError;
                                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                    trans.Rollback();
                                    return booOk;
                                }
                            }
                        }
                    }
                    else
                    { 
                        // SI NO ESTA PAGANDO LA DETRACCION ELIMINAMOS EL ABONO EN TESORERIA SI ES QUE LO TUVIERA
                        if (e_Detracciones.n_idtes != 0)
                        {
                            string[,] arrParametros2 = new string[1, 3] {
                                            {"n_id", "System.INT64", e_Detracciones.n_idtes.ToString()}
                                      };
                            
                            if (xMiFuncion.StoreEjecutar("tes_tesoreria_delete", arrParametros2, mysConec) == false)
                            {
                                b_OcurrioError = xMiFuncion.booOcurrioError;
                                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                trans.Rollback();
                                return booOk;
                            }

                            string[,] arrParametros4 = new string[2, 3] {
                                                    {"n_iddet", "System.INT64", e_Detracciones.n_id.ToString()},
                                                    {"n_idtes", "System.INT64", "0"}
                                              };
                            // ACTUALIZAMOS EL ID DE TESORERIA EN LA TABLA DETRACCION
                            if (xMiFuncion.StoreEjecutar("con_regdetracciones_actualizar_id_tesoreria", arrParametros4, mysConec) == false)
                            {
                                b_OcurrioError = xMiFuncion.booOcurrioError;
                                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                trans.Rollback();
                                return booOk;
                            }

                            if (e_Detracciones.n_tipmov == 1)
                            {
                                string[,] arrParametros5 = new string[3, 3] {
                                                        {"n_idreg", "System.INT64", e_Detracciones.n_iddoccom.ToString()},
                                                        {"n_importe", "System.DOUBLE", e_Detracciones.n_imp.ToString("0.00")},
                                                        {"n_tipo", "System.INT16", "1"}                                                // ESTE DATO INDICA SI SE SUMA O RESTA EL SALDO (1 = SUMA ; 2 = RESTA) 
                                                  };
                                // ACTUALIZAMOS EL SALDO DEL DOCUMENTOD E COMPRA
                                if (xMiFuncion.StoreEjecutar("log_compras_actualizarsaldo2", arrParametros5, mysConec) == false)
                                {
                                    b_OcurrioError = xMiFuncion.booOcurrioError;
                                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                    trans.Rollback();
                                    return booOk;
                                }
                            }
                            else
                            {
                                // ACTUALIZAMOS EL SALDO DE LA VENTA
                                string[,] arrParametros5 = new string[3, 3] {
                                                        {"n_id", "System.INT64", e_Detracciones.n_iddoccom.ToString()},
                                                        {"n_saldo", "System.DOUBLE", e_Detracciones.n_imp.ToString("0.00")},
                                                        {"n_tipo", "System.INT16", "1"}        
                                                  };
                                // ACTUALIZAMOS EL SALDO DEL DOCUMENTO DE VENTA
                                if (xMiFuncion.StoreEjecutar("vta_ventas_actualizarsaldo2", arrParametros5, mysConec) == false)
                                {
                                    b_OcurrioError = xMiFuncion.booOcurrioError;
                                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                    trans.Rollback();
                                    return booOk;
                                }
                            }
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
        public bool AsientoCab(int n_IdEmpresa, int n_IdDetraccion)
        {
            bool b_result = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT64",n_IdDetraccion.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("con_regdetracciones_asientocab", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return b_result;
            }
            else
            {
                dtDetalle = xMiFuncion.StoreDTLLenar("con_regdetracciones_asientodet", arrParametros, mysConec);
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
            if (xMiFuncion.StoreEjecutar("con_regdetracciones_insertarnumasi", arrParametros, mysConec) == false)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return b_Result;
        }
    }
}
