﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades;
using SIAC_DATOS.Logistica;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Almacen;
using SIAC_Entidades.Logistica;
using Helper;

namespace SIAC_Negocio.Logistica
{
    public class CN_log_ordencompra
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public DataTable dtOrdReqPendiente = new DataTable();
        public DataTable dtListaOrdenReq = new DataTable();
        public DataTable dtLista = new DataTable();

        public BE_LOG_ORDENCOMPRA e_OC = new BE_LOG_ORDENCOMPRA();
        public List<BE_LOG_ORDENCOMPRADET> l_OCDet = new List<BE_LOG_ORDENCOMPRADET>();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();

        Genericas funDatos = new Genericas();

        public void Listar(int n_idempresa, int n_idmes, int n_idano)
        {
            DataTable dtResul = new DataTable();

            CD_log_ordencompra miFun = new CD_log_ordencompra();
            miFun.mysConec = mysConec;

            miFun.Listar(n_idempresa, n_idmes, n_idano);
            dtLista = miFun.dtLista;
            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return;
        }

        public DataTable OrdenesPendienteJalar(int n_IdEmpresa)
        {
            DataTable dtResult = new DataTable();
            CD_log_ordencompra objsol = new CD_log_ordencompra();
            string[,] arrCabeceraFlexFil = new string[5, 5];

            objsol.mysConec = mysConec;
            if (objsol.ConsultaPendientes(n_IdEmpresa) == true)
            {
                dtResult = objsol.dtLista;
                // FLEX GRID DE LOS TAREAS
                arrCabeceraFlexFil[0, 0] = "Nº Documento";
                arrCabeceraFlexFil[0, 1] = "110";
                arrCabeceraFlexFil[0, 2] = "C";
                arrCabeceraFlexFil[0, 3] = "c_numdoc";

                arrCabeceraFlexFil[1, 0] = "Proveedor";
                arrCabeceraFlexFil[1, 1] = "110";
                arrCabeceraFlexFil[1, 2] = "C";
                arrCabeceraFlexFil[1, 3] = "c_pronom";

                arrCabeceraFlexFil[2, 0] = "Fecha";
                arrCabeceraFlexFil[2, 1] = "30";
                arrCabeceraFlexFil[2, 2] = "C";
                arrCabeceraFlexFil[2, 3] = "d_fchent";

                arrCabeceraFlexFil[3, 0] = "Local";
                arrCabeceraFlexFil[3, 1] = "110";
                arrCabeceraFlexFil[3, 2] = "C";
                arrCabeceraFlexFil[3, 3] = "c_locdes";

                arrCabeceraFlexFil[4, 0] = "Area";
                arrCabeceraFlexFil[4, 1] = "110";
                arrCabeceraFlexFil[4, 2] = "C";
                arrCabeceraFlexFil[4, 3] = "c_aredes";

                funDatos.Buscar_CampoBusqueda = "c_numdoc";
                funDatos.Buscar_CadFiltro = "";
                funDatos.Buscar_CampoOrden = "c_numdoc";
                funDatos.Buscar_Titulo = "Documentos Pendientes";
                dtResult = funDatos.Buscar(arrCabeceraFlexFil, dtResult);
            }
            return dtResult;
        }

        public bool TraerRegistro(int n_IdRegistro)
        {
            bool booresult = false;
            DataTable dtCab = new DataTable();
            DataTable dtDet = new DataTable();
            BE_LOG_ORDENCOMPRA e_reque = new BE_LOG_ORDENCOMPRA();
            CD_log_ordencompra miFun = new CD_log_ordencompra();
            miFun.mysConec = mysConec;
            l_OCDet.Clear();

            if (miFun.TraerRegistro(n_IdRegistro) == true)
            {
                dtCab = miFun.dtCabecera;
                dtDet = miFun.dtDetalle;

                e_OC.n_idemp = Convert.ToInt32(dtCab.Rows[0]["n_idemp"]);
                e_OC.n_id = Convert.ToInt32(dtCab.Rows[0]["n_id"]);
                e_OC.n_idtipdoc = Convert.ToInt32(dtCab.Rows[0]["n_idtipdoc"]);
                e_OC.c_numser = dtCab.Rows[0]["c_numser"].ToString();
                e_OC.c_numdoc = dtCab.Rows[0]["c_numdoc"].ToString();
                e_OC.n_anotra = Convert.ToInt32(dtCab.Rows[0]["n_anotra"]);
                e_OC.n_mestra = Convert.ToInt32(dtCab.Rows[0]["n_mestra"]);
                e_OC.d_fchemi = Convert.ToDateTime(dtCab.Rows[0]["d_fchemi"]);
                e_OC.d_fchent = Convert.ToDateTime(dtCab.Rows[0]["d_fchent"]);
                e_OC.n_idloc = Convert.ToInt32(dtCab.Rows[0]["n_idloc"]);
                e_OC.n_idare = Convert.ToInt32(dtCab.Rows[0]["n_idare"]);
                e_OC.n_idpersol = Convert.ToInt32(dtCab.Rows[0]["n_idpersol"]);
                e_OC.n_idpri = Convert.ToInt32(dtCab.Rows[0]["n_idpri"]);
                e_OC.c_obs = dtCab.Rows[0]["c_obs"].ToString();
                e_OC.n_idest = Convert.ToInt32(dtCab.Rows[0]["n_idest"]);
                e_OC.n_idmot = Convert.ToInt32(dtCab.Rows[0]["n_idmot"]);
                e_OC.n_idpro = Convert.ToInt32(dtCab.Rows[0]["n_idpro"]);
                e_OC.n_idmon = Convert.ToInt32(dtCab.Rows[0]["n_idmon"]);
                e_OC.n_impbru = Convert.ToDouble(dtCab.Rows[0]["n_impbru"]);
                e_OC.n_impigv = Convert.ToDouble(dtCab.Rows[0]["n_impigv"]);
                e_OC.n_imptot = Convert.ToDouble(dtCab.Rows[0]["n_imptot"]);
                e_OC.n_idconpag = Convert.ToInt32(dtCab.Rows[0]["n_idconpag"]);
                e_OC.n_impina = Convert.ToDouble(dtCab.Rows[0]["n_impina"]);
                if (!string.IsNullOrEmpty(dtCab.Rows[0]["n_docrefidtipdoc"].ToString()))
                {
                    e_OC.n_docrefidtipdoc = Convert.ToInt32(dtCab.Rows[0]["n_docrefidtipdoc"]);
                }
                if (!string.IsNullOrEmpty(dtCab.Rows[0]["n_docrefiddocref"].ToString()))
                {
                    e_OC.n_docrefiddocref = Convert.ToInt32(dtCab.Rows[0]["n_docrefiddocref"]);
                }
                e_OC.c_docrefnumser = dtCab.Rows[0]["c_docrefnumser"].ToString();
                e_OC.c_docrefnumdoc = dtCab.Rows[0]["c_docrefnumdoc"].ToString();

                int n_row = 0;

                for (n_row = 0; n_row <= dtDet.Rows.Count - 1; n_row++)
                {
                    BE_LOG_ORDENCOMPRADET e_ocdet = new BE_LOG_ORDENCOMPRADET();

                    e_ocdet.n_idoc = Convert.ToInt32(dtDet.Rows[n_row]["n_idoc"]);
                    e_ocdet.n_idite = Convert.ToInt32(dtDet.Rows[n_row]["n_idite"]);
                    e_ocdet.n_idunimed = Convert.ToInt32(dtDet.Rows[n_row]["n_idunimed"]);
                    e_ocdet.n_can = Convert.ToDouble(dtDet.Rows[n_row]["n_can"]);
                    e_ocdet.n_preuni = Convert.ToDouble(dtDet.Rows[n_row]["n_preuni"]);
                    e_ocdet.n_imptot = Convert.ToDouble(dtDet.Rows[n_row]["n_imptot"]);
                    e_ocdet.n_idtipafeigv = Convert.ToInt32(dtDet.Rows[n_row]["n_idtipafeigv"]);
                    if (string.IsNullOrEmpty(dtDet.Rows[n_row]["n_canat"].ToString()))
                    {
                        e_ocdet.n_canat = 0;
                    }
                    else
                    {
                        e_ocdet.n_canat = Convert.ToDouble(dtDet.Rows[n_row]["n_canat"]);
                    }
                    l_OCDet.Add(e_ocdet);
                }
                booresult = true;
            }
            return booresult;
        }
        public bool Insertar(BE_LOG_ORDENCOMPRA e_OrdenCompra, List<BE_LOG_ORDENCOMPRADET> l_OrdenCompraDetalles)
        {

            CD_log_ordencompra miFun = new CD_log_ordencompra();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(e_OrdenCompra,l_OrdenCompraDetalles);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_LOG_ORDENCOMPRA e_OrdenCompra, List<BE_LOG_ORDENCOMPRADET> l_OrdenCompraDetalles)
        {
            CD_log_ordencompra miFun = new CD_log_ordencompra();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(e_OrdenCompra, l_OrdenCompraDetalles);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool Eliminar(int n_Id)
        {
            CD_log_ordencompra miFun = new CD_log_ordencompra();
            bool booOk = false;

            miFun.mysConec = mysConec;

            booOk = miFun.Eliminar(n_Id);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public void ReportImprimirOrdCompra(int n_IdOrdenCompra)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[1, 3];

            arrPara[0, 0] = "n_id";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = n_IdOrdenCompra.ToString();

            c_NomArchivo = "RptOrdenCompra.rpt";
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "logistica\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "COMPRAS - IMPRESION ORDEN DE COMPRA";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
    }
}
