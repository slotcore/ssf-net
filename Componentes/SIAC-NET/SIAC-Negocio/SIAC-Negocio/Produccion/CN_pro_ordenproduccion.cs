using MySql.Data.MySqlClient;
using SIAC_DATOS.Almacen;
using SIAC_DATOS.Produccion;
using SIAC_DATOS.Logistica;
using SIAC_DATOS.Sistema;
using SIAC_DATOS.Ventas;
using System;
using System.Collections.Generic;
using System.Data;
using SIAC_Entidades.Produccion;

namespace SIAC_Negocio.Produccion
{
    public class CN_pro_ordenproduccion
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public BE_PRO_ORDENPRODUCCION entOrdenProd = new BE_PRO_ORDENPRODUCCION();
        public List<BE_PRO_ORDENPRODUCCIONDET> lstOrdenProdDet = new List<BE_PRO_ORDENPRODUCCIONDET>();
        public DataTable dtOrdenProdPendientes = new DataTable();
        public DataTable dtOrdenProdProdtPend = new DataTable();

        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFundat = new Helper.Genericas();

        public DataTable Listar(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo)
        {
            DataTable dtResul = new DataTable();

            CD_pro_ordenproduccion miFun = new CD_pro_ordenproduccion();
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar(n_IdEmpresa, n_AnoTrabajo, n_MesTrabajo);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public DataTable Listar(int n_IdEmpresa)
        {
            DataTable dtResul = new DataTable();

            CD_pro_ordenproduccion miFun = new CD_pro_ordenproduccion();
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar(n_IdEmpresa);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public bool TraeOrdenProduccionProductosPendientes(int n_IdOrdenProduccion)
        {
            bool b_Result = false;
            CD_pro_ordenproduccion miFun = new CD_pro_ordenproduccion();
            miFun.mysConec = mysConec;

            if (miFun.TraeOrdenProduccionProductosPendientes(n_IdOrdenProduccion) == true)
            {
                dtOrdenProdProdtPend = miFun.dtOrdProdProdtPends;
                b_Result = true;
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return b_Result;
        }
        public bool TraeOrdenProduccionPendientes(int n_IdEmpresa, string c_CadenaIN)
        {
            bool b_Result = false;
            CD_pro_ordenproduccion miFun = new CD_pro_ordenproduccion();
            miFun.mysConec = mysConec;

            if (miFun.TraeOrdenProduccionPendientes(n_IdEmpresa, c_CadenaIN) == true)
            {
                dtOrdenProdPendientes = miFun.dtOrdenProdPendientes;
                b_Result = true;
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return b_Result;
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            DataTable dtCab = new DataTable();
            DataTable dtDet = new DataTable();
            bool booResult;
            int n_row;
            CD_pro_ordenproduccion miFun = new CD_pro_ordenproduccion();

            lstOrdenProdDet.Clear();
            miFun.mysConec = mysConec;

            booResult = miFun.TraerRegistro(n_IdRegistro);
            dtCab = miFun.dtOrdenProd;
            dtDet = miFun.dtOrdenProdDet;
            if (dtCab.Rows.Count != 0)
            {
                entOrdenProd.n_idemp = Convert.ToInt32(dtCab.Rows[0]["n_idemp"]);
                entOrdenProd.n_id = Convert.ToInt32(dtCab.Rows[0]["n_id"]);
                entOrdenProd.n_idtipdoc = Convert.ToInt32(dtCab.Rows[0]["n_idtipdoc"]);
                entOrdenProd.c_numser = dtCab.Rows[0]["c_numser"].ToString();
                entOrdenProd.c_numdoc = dtCab.Rows[0]["c_numdoc"].ToString();
                entOrdenProd.d_fchemi = Convert.ToDateTime(dtCab.Rows[0]["d_fchemi"]);
                entOrdenProd.n_anotra = Convert.ToInt32(dtCab.Rows[0]["n_anotra"]);
                entOrdenProd.n_mestra = Convert.ToInt32(dtCab.Rows[0]["n_mestra"]);
                entOrdenProd.n_idres = Convert.ToInt32(dtCab.Rows[0]["n_idres"]);
                entOrdenProd.n_idtipdocref = Convert.ToInt32(dtCab.Rows[0]["n_idtipdocref"]);
                entOrdenProd.n_iddocref = Convert.ToInt32(dtCab.Rows[0]["n_iddocref"]);
                entOrdenProd.n_idpri = Convert.ToInt32(dtCab.Rows[0]["n_idpri"]);
                entOrdenProd.c_obs = dtCab.Rows[0]["c_obs"].ToString();
                entOrdenProd.d_fchent = Convert.ToDateTime(dtCab.Rows[0]["d_fchent"].ToString());
                entOrdenProd.n_idest = Convert.ToInt32(dtCab.Rows[0]["n_idest"]);
            }

            if (dtDet.Rows.Count != 0)
            {
                for (n_row = 0; n_row <= dtDet.Rows.Count-1; n_row++)
                {
                    BE_PRO_ORDENPRODUCCIONDET entDetalle = new BE_PRO_ORDENPRODUCCIONDET();
                    entDetalle.n_idord = Convert.ToInt32(dtDet.Rows[n_row]["n_idord"]);
                    entDetalle.n_idpro = Convert.ToInt32(dtDet.Rows[n_row]["n_idpro"]);
                    entDetalle.n_idrec = Convert.ToInt32(dtDet.Rows[n_row]["n_idrec"]);
                    entDetalle.n_idunimed = Convert.ToInt32(dtDet.Rows[n_row]["n_idunimed"]);
                    entDetalle.n_can = Convert.ToDouble(dtDet.Rows[n_row]["n_can"]);
                    entDetalle.c_obs = dtDet.Rows[n_row]["c_obs"].ToString();
                    entDetalle.n_numarm = Convert.ToInt32(dtDet.Rows[n_row]["n_numarm"].ToString());

                    if (xFun.NulosC(dtDet.Rows[n_row]["d_fchent"]) != "")
                    {
                        entDetalle.d_fchent = Convert.ToDateTime(dtDet.Rows[n_row]["d_fchent"]);
                    }
                    lstOrdenProdDet.Add(entDetalle);
                }
            }

            if (booResult == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return;
        }
        public bool Eliminar(BE_PRO_ORDENPRODUCCION entOrdenProduccion)
        {
            bool booResult = false;
            CD_pro_ordenproduccion miFun = new CD_pro_ordenproduccion();
            CD_log_ordenrequerimiento miReq = new CD_log_ordenrequerimiento();
            CD_vta_pedidocli miPed = new CD_vta_pedidocli();
            miFun.mysConec = mysConec;
            
            booResult = miFun.Eliminar(entOrdenProduccion.n_id);
            if (booResult == true)
            {
                if (entOrdenProduccion.n_idtipdocref == 75)
                {
                    miReq.mysConec = mysConec;
                    booResult = miReq.ActualizarEstadoRequerimiento(entOrdenProduccion.n_iddocref, 1);                                         // ACTUALIZAMOS EL ESTADO A 1 = PENDIENTE
                }
                if (entOrdenProduccion.n_idtipdocref == 77)
                {
                    miPed.mysConec = mysConec;
                    booResult = miPed.ActualizarEstadoPedido(entOrdenProduccion.n_iddocref, 0);                                                // ACTUALIZAMOS EL ESTADO EN ORDEN DE PRODUCCION A 0 = NO ESTA EN ORDEN DE PRODUCCION
                }

                if (booResult == false)
                {
                    booOcurrioError = miReq.booOcurrioError;
                    StrErrorMensaje = miReq.StrErrorMensaje;
                    IntErrorNumber = miReq.IntErrorNumber;
                }
            }
            else
            {
                booOcurrioError = false;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return booResult;
        }
        public bool Insertar(BE_PRO_ORDENPRODUCCION entOrdenProduccion, List<BE_PRO_ORDENPRODUCCIONDET> lstOrdenProduccionLista)
        {
            CD_pro_ordenproduccion miFun = new CD_pro_ordenproduccion();
            CD_log_ordenrequerimiento miReq = new CD_log_ordenrequerimiento();
            CD_vta_pedidocli miPed = new CD_vta_pedidocli();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(entOrdenProduccion, lstOrdenProduccionLista);
            if (booOk == true)
            {
                if (entOrdenProduccion.n_idtipdocref == 75)
                {
                    miReq.mysConec = mysConec;
                    booOk = miReq.ActualizarEstadoRequerimiento(entOrdenProduccion.n_iddocref, 3);                                  // ACTUALIZAMOS EL ESTADO A 3 = PROCESADO
                }
                //if (entOrdenProduccion.n_idtipdocref == 77)
                //{
                //    miPed.mysConec = mysConec;
                //    booOk = miPed.ActualizarEstadoPedido(entOrdenProduccion.n_iddocref, 3);                                         // ACTUALIZAMOS EL ESTADO A 3 = PROCESADO
                //}

                if (booOk == false)
                {
                    booOcurrioError = miReq.booOcurrioError;
                    StrErrorMensaje = miReq.StrErrorMensaje;
                    IntErrorNumber = miReq.IntErrorNumber;
                }
            }
            else
            { 
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return booOk;
        }
        public bool Actualizar(BE_PRO_ORDENPRODUCCION entOrdenProduccion, List<BE_PRO_ORDENPRODUCCIONDET> lstOrdenProduccionLista)
        {
            CD_pro_ordenproduccion miFun = new CD_pro_ordenproduccion();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(entOrdenProduccion, lstOrdenProduccionLista);
            if (booOk == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return booOk;
        }
    }
}
