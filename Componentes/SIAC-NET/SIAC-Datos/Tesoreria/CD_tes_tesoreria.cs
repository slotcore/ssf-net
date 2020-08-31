using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Tesoreria;
using SIAC_DATOS.Contabilidad;

namespace SIAC_DATOS.Tesoreria
{
    public class CD_tes_tesoreria
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public int n_IdGenerado = 0;
        public bool b_DesdeOtraCapa = false;
        public DataTable DtLista = new DataTable();
        public DataTable DtLista1 = new DataTable();
        public DataTable DtLista2 = new DataTable();

        public DataTable DtRegistro = new DataTable();
        public DataTable DtRegistroOri = new DataTable();
        public DataTable DtRegistroOriDet = new DataTable();
        public DataTable DtRegistroDes = new DataTable();
        public DataTable DtRegistroDesDet = new DataTable();
        public MySqlConnection mysConec = new MySqlConnection();

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();
        
        public void Listar(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrasbajo, int n_TipoRegistro)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[4, 3] {
                                            {"n_idemp", "System.INT32",n_IdEmpresa.ToString()},
                                            {"n_ano", "System.INT32",n_AnoTrabajo.ToString()},
                                            {"n_mes", "System.INT32",n_MesTrasbajo.ToString()},
                                            {"n_tipreg", "System.INT32",n_TipoRegistro.ToString()}
                                      };

            DtLista = xMiFuncion.StoreDTLLenar("tes_tesoreria_listar", arrParametros, mysConec);

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
            DtRegistro = xMiFuncion.StoreDTLLenar("tes_tesoreria_obtenerregistro", arrParametros, mysConec);

            if (DtRegistro != null)
            {
                //arrParametros[0, 0] = "n_idope";
                DtRegistroOri = xMiFuncion.StoreDTLLenar("tes_tesoreriaori_obtenerregistro", arrParametros, mysConec);
                if (DtRegistroOri != null)
                {
                    DtRegistroOriDet = xMiFuncion.StoreDTLLenar("tes_tesoreriaoridet_obtenerregistro", arrParametros, mysConec);
                    if (DtRegistroOriDet != null)
                    {
                        DtRegistroDes = xMiFuncion.StoreDTLLenar("tes_tesoreriades_obtenerregistro", arrParametros, mysConec);
                        if (DtRegistroDes != null)
                        {
                            DtRegistroDesDet = xMiFuncion.StoreDTLLenar("tes_tesoreriadesdet_obtenerregistro", arrParametros, mysConec);
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
            MySqlTransaction trans = null;
            int n_row = 0;
            // PARAMETROS PARA ELIMINAR LOS INSUMOS Y LAS TAREAS
            if (b_DesdeOtraCapa == false) 
            {
                xMiFuncion.ReAbrirConeccion(mysConec);
                trans = mysConec.BeginTransaction(); 
            }

            try
            {
                // BORRAMOS ACTUALIZAMOS EL DETALLE DE LA TABLA DETALLE  tes_tesoreriadesdet
                //DtRegistroDesDet
                string[,] arrParametros = new string[5, 3] {
                                            {"n_idtes", "System.INT32",""},
                                            {"n_idmod", "System.INT32",""},
                                            {"n_idlib", "System.INT32",""},
                                            {"n_iddoc", "System.INT32",""},
                                            {"n_acuenta", "System.DOUBLE",""}
                                      };

                for (n_row = 0; n_row <= DtRegistroDesDet.Rows.Count - 1; n_row++)
                {
                    arrParametros[0, 2] = DtRegistroDesDet.Rows[n_row]["n_idtes"].ToString();
                    arrParametros[1, 2] = DtRegistroDesDet.Rows[n_row]["n_idmod"].ToString();
                    arrParametros[2, 2] = DtRegistroDesDet.Rows[n_row]["n_idlib"].ToString();
                    arrParametros[3, 2] = DtRegistroDesDet.Rows[n_row]["n_iddoc"].ToString();
                    arrParametros[4, 2] = DtRegistroDesDet.Rows[n_row]["n_acuenta"].ToString();

                    if (xMiFuncion.StoreEjecutar("tes_tesoreriadesdet_actualizarsaldo", arrParametros, mysConec) == false)
                    {
                        b_OcurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                        if (b_DesdeOtraCapa == false) { trans.Rollback(); }
                        return booResult;
                    }
                }

                string[,] arrParametros2 = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdRegistro.ToString()}
                                      };
                if (xMiFuncion.StoreEjecutar("tes_tesoreria_delete", arrParametros2, mysConec) == false)
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    if (b_DesdeOtraCapa == false) { trans.Rollback(); }
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
                if (b_DesdeOtraCapa == false) { trans.Rollback(); }
                return booResult;
            }
        }
        public bool Insertar(BE_TES_TESORERIA entOperaciones, List<BE_TES_TESORERIAORI> lstOperacionesOri, List<BE_TES_TESORERIAORIDET> lstOperacionesOriDet,
            List<BE_TES_TESORERIADES> lstOperacionesDes, List<BE_TES_TESORERIADESDET> lstOperacionesDesDet)
        {
            bool booOk = false;
            MySqlTransaction trans = null;
            DatosMySql xMiFuncion = new DatosMySql();
            int n_row = 0;
            //int n_idgenerado = 0;
            if (b_DesdeOtraCapa == false) { trans = mysConec.BeginTransaction(); }

            try
            {
                if (xMiFuncion.StoreEjecutar("tes_tesoreria_insertar", entOperaciones, mysConec, 4) == true)
                {
                    // ***********************************
                    // GRABAMOS EL ORIGEN DE LA OPERACION
                    n_IdGenerado = Convert.ToInt32(xMiFuncion.intIdGenerado);
                    
                    for (n_row = 0; n_row <= lstOperacionesOri.Count - 1; n_row++)
                    {
                        lstOperacionesOri[n_row].n_idtes = n_IdGenerado;
                        if (xMiFuncion.StoreEjecutar("tes_tesoreriaori_insertar", lstOperacionesOri[n_row], mysConec, null) == false)
                        {
                            b_OcurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            if (b_DesdeOtraCapa == false) { trans.Rollback(); }
                            return booOk;
                        }
                    }
                    for (n_row = 0; n_row <= lstOperacionesOriDet.Count - 1; n_row++)
                    {
                        lstOperacionesOriDet[n_row].n_idtes = n_IdGenerado;
                        if (xMiFuncion.StoreEjecutar("tes_tesoreriaoridet_insertar", lstOperacionesOriDet[n_row], mysConec, null) == false)
                        {
                            b_OcurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            if (b_DesdeOtraCapa == false) { trans.Rollback(); }
                            return booOk;
                        }
                    }
                    // ***********************************
                    // GRABAMOS EL DESTINO DE LA OPERACION
                    for (n_row = 0; n_row <= lstOperacionesDes.Count - 1; n_row++)
                    {
                        lstOperacionesDes[n_row].n_idtes = n_IdGenerado;
                        if (xMiFuncion.StoreEjecutar("tes_tesoreriades_insertar", lstOperacionesDes[n_row], mysConec, null) == false)
                        {
                            b_OcurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            if (b_DesdeOtraCapa == false) { trans.Rollback(); }
                            return booOk;
                        }
                    }
                    for (n_row = 0; n_row <= lstOperacionesDesDet.Count - 1; n_row++)
                    {
                        lstOperacionesDesDet[n_row].n_idtes = n_IdGenerado;
                        if (xMiFuncion.StoreEjecutar("tes_tesoreriadesdet_insertar", lstOperacionesDesDet[n_row], mysConec, null) == false)
                        {
                            b_OcurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            if (b_DesdeOtraCapa == false) { trans.Rollback(); }
                            return booOk;
                        }
                    }
                }
                else
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    if (b_DesdeOtraCapa == false) { trans.Rollback(); }
                    return booOk;
                }

                booOk = true;
                if (b_DesdeOtraCapa == false) { trans.Commit(); }
                return booOk;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                if (b_DesdeOtraCapa == false) { trans.Rollback(); }
                return booOk;
            }
        }
        public bool Actualizar(BE_TES_TESORERIA entOperaciones, List<BE_TES_TESORERIAORI> lstOperacionesOri, List<BE_TES_TESORERIAORIDET> lstOperacionesOriDet,
            List<BE_TES_TESORERIADES> lstOperacionesDes, List<BE_TES_TESORERIADESDET> lstOperacionesDesDet)
        {
            bool booOk = false;
            MySqlTransaction trans = null;
            DatosMySql xMiFuncion = new DatosMySql();
            int n_row = 0;
            if (b_DesdeOtraCapa == false) { trans = mysConec.BeginTransaction(); }

            try
            {
                // BORRAMOS ACTUALIZAMOS EL DETALLE DE LA TABLA DETALLE  tes_tesoreriadesdet
                //DtRegistroDesDet
                string[,] arrParametros = new string[5, 3] {
                                            {"n_idtes", "System.INT32",entOperaciones.n_id.ToString()},
                                            {"n_idmod", "System.INT32",entOperaciones.n_id.ToString()},
                                            {"n_idlib", "System.INT32",entOperaciones.n_id.ToString()},
                                            {"n_iddoc", "System.INT32",entOperaciones.n_id.ToString()},
                                            {"n_acuenta", "System.DOUBLE",entOperaciones.n_id.ToString()}
                                      };

                for (n_row = 0; n_row <= DtRegistroDesDet.Rows.Count - 1; n_row++)
                {
                    arrParametros[0, 2] = DtRegistroDesDet.Rows[n_row]["n_idtes"].ToString();
                    arrParametros[1, 2] = DtRegistroDesDet.Rows[n_row]["n_idmod"].ToString();
                    arrParametros[2, 2] = DtRegistroDesDet.Rows[n_row]["n_idlib"].ToString();
                    arrParametros[3, 2] = DtRegistroDesDet.Rows[n_row]["n_iddoc"].ToString();
                    arrParametros[4, 2] = DtRegistroDesDet.Rows[n_row]["n_acuenta"].ToString();

                    if (xMiFuncion.StoreEjecutar("tes_tesoreriadesdet_actualizarsaldo", arrParametros, mysConec)==false)
                    {
                        b_OcurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                        if (b_DesdeOtraCapa == false) { trans.Rollback(); }
                        return booOk;
                    }
                }
                //booOk = true;

                string[,] arrParametros2 = new string[1, 3] {
                                            {"n_id", "System.INT32",entOperaciones.n_id.ToString()}
                                         };
                    if (xMiFuncion.StoreEjecutar("tes_tesoreria_borrardetalle", arrParametros2, mysConec) == true)
                    {
                        if (xMiFuncion.StoreEjecutar("tes_tesoreria_actualizar", entOperaciones, mysConec, null) == true)
                        {
                            // ***********************************
                            // GRABAMOS EL ORIGEN DE LA OPERACION
                            n_IdGenerado = entOperaciones.n_id;

                            for (n_row = 0; n_row <= lstOperacionesOri.Count - 1; n_row++)
                            {
                                lstOperacionesOri[n_row].n_idtes = n_IdGenerado;
                                if (xMiFuncion.StoreEjecutar("tes_tesoreriaori_insertar", lstOperacionesOri[n_row], mysConec, null) == false)
                                {
                                    b_OcurrioError = xMiFuncion.booOcurrioError;
                                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                    if (b_DesdeOtraCapa == false) { trans.Rollback(); }
                                    return booOk;
                                }
                            }
                            for (n_row = 0; n_row <= lstOperacionesOriDet.Count - 1; n_row++)
                            {
                                lstOperacionesOriDet[n_row].n_idtes = n_IdGenerado;
                                if (xMiFuncion.StoreEjecutar("tes_tesoreriaoridet_insertar", lstOperacionesOriDet[n_row], mysConec, null) == false)
                                {
                                    b_OcurrioError = xMiFuncion.booOcurrioError;
                                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                    if (b_DesdeOtraCapa == false) { trans.Rollback(); }
                                    return booOk;
                                }
                            }
                            // ***********************************
                            // GRABAMOS EL DESTINO DE LA OPERACION
                            for (n_row = 0; n_row <= lstOperacionesDes.Count - 1; n_row++)
                            {
                                lstOperacionesDes[n_row].n_idtes = n_IdGenerado;
                                if (xMiFuncion.StoreEjecutar("tes_tesoreriades_insertar", lstOperacionesDes[n_row], mysConec, null) == false)
                                {
                                    b_OcurrioError = xMiFuncion.booOcurrioError;
                                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                    if (b_DesdeOtraCapa == false) { trans.Rollback(); }
                                    return booOk;
                                }
                            }
                            for (n_row = 0; n_row <= lstOperacionesDesDet.Count - 1; n_row++)
                            {
                                lstOperacionesDesDet[n_row].n_idtes = n_IdGenerado;
                                if (xMiFuncion.StoreEjecutar("tes_tesoreriadesdet_insertar", lstOperacionesDesDet[n_row], mysConec, null) == false)
                                {
                                    b_OcurrioError = xMiFuncion.booOcurrioError;
                                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                                    if (b_DesdeOtraCapa == false) { trans.Rollback(); }
                                    return booOk;
                                }
                            }
                        }
                        else
                        {
                            b_OcurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            if (b_DesdeOtraCapa == false) { trans.Rollback(); }
                            return booOk;
                        }

                        booOk = true;
                        if (b_DesdeOtraCapa == false) { trans.Commit(); }
                        return booOk;
                    }
                    else
                    {
                        b_OcurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                        if (b_DesdeOtraCapa == false) { trans.Rollback(); }
                        return booOk;
                    }
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                if (b_DesdeOtraCapa == false) { trans.Rollback(); }
                return booOk;
            }
        }
        public bool AsientoCab(int n_IdEmpresa, int n_IdTesoreria, int TipoRegistro)
        {
            bool b_result = false;

            string[,] arrParametros = new string[3, 3] {
                                            {"n_id", "System.INT32",n_IdTesoreria.ToString()},
                                            {"n_idemp", "System.INT32",n_IdEmpresa.ToString()},
                                            {"n_tipreg", "System.INT32", TipoRegistro.ToString()}
                                      };

            DtLista1 = xMiFuncion.StoreDTLLenar("tes_tesoreria_asientocab", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return b_result;
            }
            else
            {
                string[,] arrParametros2 = new string[3, 3] {
                                            {"n_id", "System.INT32", n_IdTesoreria.ToString()},
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"n_tipreg", "System.INT32", TipoRegistro.ToString()}
                                      };
                DtLista2 = xMiFuncion.StoreDTLLenar("tes_tesoreria_asientodet", arrParametros2, mysConec);
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
            if (xMiFuncion.StoreEjecutar("tes_tesoreria_insertarnumasi", arrParametros, mysConec) == false)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return b_Result;
        }
    }
}
