﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades.Produccion;
using SIAC_DATOS.Produccion;
using MySql.Data.MySqlClient;
using Helper;

namespace SIAC_Negocio.Produccion
{
    public class CN_pro_solicitudtareasdiversas
    {
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        Genericas funDatos = new Genericas();
        Helper.Comunes.Funciones funFunciones = new Helper.Comunes.Funciones();
        DatosMySql FunMysql = new DatosMySql();

        public BE_PRO_SOLICITUDTAREASDIVERSAS entSolicitud = new BE_PRO_SOLICITUDTAREASDIVERSAS();
        public List<BE_PRO_SOLICITUDTAREASDIVERSASCAB> lstSolicitudCab = new List<BE_PRO_SOLICITUDTAREASDIVERSASCAB>();
        public List<BE_PRO_SOLICITUDTAREASDIVERSASDET> lstSolicitudDet = new List<BE_PRO_SOLICITUDTAREASDIVERSASDET>();

        private DataTable _dtLista;
        private DataTable _dtRegistro;
        private DataTable _dtlstInsumos;

        public DataTable dtLista = new DataTable();

        public bool b_OcurrioError;
        public string c_ErrorMensaje;
        public int n_ErrorNumber;

        public MySqlConnection mysConec;

        public bool Listar(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo)
        {
            bool b_result = false;

            CD_pro_solicitudtareasdiversas miFun = new CD_pro_solicitudtareasdiversas();
            miFun.mysConec = mysConec;

            if (miFun.Listar(n_IdEmpresa, n_AnoTrabajo, n_MesTrabajo) == true)
            {
                b_result = true;
                dtLista = miFun.dtLista;
            }
            else
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return b_result;
        }
        public bool TraerRegistro(int n_Idregistro)
        {
            int n_row = 0;
            bool b_result = false;
            DataTable dtResult = new DataTable();
            DataTable dtResultLis = new DataTable();
            DataTable dtResultLisPer = new DataTable();
            CD_pro_solicitudtareasdiversas miFun = new CD_pro_solicitudtareasdiversas();

            lstSolicitudCab.Clear();
            lstSolicitudDet.Clear();

            miFun.mysConec = mysConec;
            b_result = miFun.TraerRegistro(n_Idregistro);
            if (b_result == true)
            {
                dtResult = miFun.dtRegistro;
                dtResultLis = miFun.dtRegistroDet;
                dtResultLisPer = miFun.dtRegistroDetPer;

                BE_PRO_SOLICITUDTAREASDIVERSAS entTmp = new BE_PRO_SOLICITUDTAREASDIVERSAS();

                entTmp.n_idemp = Convert.ToInt32(dtResult.Rows[0]["n_idemp"]);
                entTmp.n_id = Convert.ToInt32(dtResult.Rows[0]["n_id"]);
                entTmp.n_idpro = Convert.ToInt32(dtResult.Rows[0]["n_idpro"]);
                entTmp.n_idtipdoc = Convert.ToInt32(dtResult.Rows[0]["n_idtipdoc"]);
                entTmp.c_numser = dtResult.Rows[0]["c_numser"].ToString();
                entTmp.c_numdoc = dtResult.Rows[0]["c_numdoc"].ToString();
                entTmp.d_fchreg = Convert.ToDateTime(dtResult.Rows[0]["d_fchreg"]);
                entTmp.n_idsol = Convert.ToInt32(dtResult.Rows[0]["n_idsol"]);
                
                if (funFunciones.NulosC(dtResult.Rows[0]["d_fchejetar"]) != "")
                {
                    entTmp.d_fchejetar = Convert.ToDateTime(dtResult.Rows[0]["d_fchejetar"]);
                }
                else
                {
                    entTmp.d_fchejetar = null;
                }
                entTmp.c_obs = dtResult.Rows[0]["c_obs"].ToString();
                entTmp.n_idlocal = Convert.ToInt32(dtResult.Rows[0]["n_idlocal"]);
                entSolicitud = entTmp;

                dtResultLis = funDatos.DataTableOrdenar(dtResultLis, "n_ord");

                for (n_row = 0; n_row <= dtResultLis.Rows.Count - 1; n_row++)
                {
                    BE_PRO_SOLICITUDTAREASDIVERSASCAB entTmpDet = new BE_PRO_SOLICITUDTAREASDIVERSASCAB();

                    entTmpDet.n_idsol = Convert.ToInt32(dtResultLis.Rows[n_row]["n_idsol"]);
                    entTmpDet.n_id = Convert.ToInt32(dtResultLis.Rows[n_row]["n_id"]);
                    entTmpDet.n_idtar = Convert.ToInt32(dtResultLis.Rows[n_row]["n_idtar"]);
                    entTmpDet.n_can = Convert.ToDouble(dtResultLis.Rows[n_row]["n_can"]);
                    entTmpDet.h_horini = dtResultLis.Rows[n_row]["h_horini"].ToString();
                    entTmpDet.h_horfin = dtResultLis.Rows[n_row]["h_horfin"].ToString();
                    entTmpDet.n_numper = Convert.ToInt32(dtResultLis.Rows[n_row]["n_numper"]);
                    entTmpDet.n_costar = Convert.ToDouble(dtResultLis.Rows[n_row]["n_costar"]);
                    entTmpDet.n_ord = Convert.ToInt32(dtResultLis.Rows[n_row]["n_ord"]);
                    if (funFunciones.NulosC(dtResultLis.Rows[n_row]["d_fchtra"]) != "")
                    {
                        entTmpDet.d_fchtra = Convert.ToDateTime(dtResultLis.Rows[n_row]["d_fchtra"]);
                    }
                    lstSolicitudCab.Add(entTmpDet);
                }

                for (n_row = 0; n_row <= dtResultLisPer.Rows.Count - 1; n_row++)
                {
                    BE_PRO_SOLICITUDTAREASDIVERSASDET entTmpDetPer = new BE_PRO_SOLICITUDTAREASDIVERSASDET();

                    entTmpDetPer.n_idsol = Convert.ToInt32(dtResultLisPer.Rows[n_row]["n_idsol"]);
                    entTmpDetPer.n_idsoltar = Convert.ToInt32(dtResultLisPer.Rows[n_row]["n_idsoltar"]);
                    entTmpDetPer.n_idper = Convert.ToInt32(dtResultLisPer.Rows[n_row]["n_idper"]);
                    entTmpDetPer.n_can = Convert.ToDouble(dtResultLisPer.Rows[n_row]["n_can"]);
                    entTmpDetPer.c_obs = funFunciones.NulosC(dtResultLisPer.Rows[n_row]["c_obs"]);
                    entTmpDetPer.c_horini = funFunciones.NulosC(dtResultLisPer.Rows[n_row]["c_horini"]);
                    entTmpDetPer.c_horter = funFunciones.NulosC(dtResultLisPer.Rows[n_row]["c_horter"]);
                    entTmpDetPer.c_numhortra = funFunciones.NulosC(dtResultLisPer.Rows[n_row]["c_numhortra"]);
                    entTmpDetPer.n_numhortra = Convert.ToDouble(dtResultLisPer.Rows[n_row]["n_numhortra"]);
                    entTmpDetPer.n_canmaxpro = Convert.ToDouble(dtResultLisPer.Rows[n_row]["n_canmaxpro"]);
                    entTmpDetPer.n_preunipro = Convert.ToDouble(dtResultLisPer.Rows[n_row]["n_preunipro"]);
                    entTmpDetPer.n_prehorpag = Convert.ToDouble(dtResultLisPer.Rows[n_row]["n_prehorpag"]);
                    entTmpDetPer.n_imppaghrstra = Convert.ToDouble(dtResultLisPer.Rows[n_row]["n_imppaghrstra"]);
                    entTmpDetPer.n_subsidio = Convert.ToDouble(dtResultLisPer.Rows[n_row]["n_subsidio"]);

                    //entTmpDetPer.n_pretar = Convert.ToDouble(dtResultLisPer.Rows[n_row]["n_pretar"]);
                    //entTmpDetPer.n_imptot = Convert.ToDouble(dtResultLisPer.Rows[n_row]["n_imptot"]);
                    //entTmpDetPer.n_numhortra = Convert.ToDouble(dtResultLisPer.Rows[n_row]["n_numhortra"]);
                    //entTmpDetPer.c_numhortra = funFunciones.NulosC(dtResultLisPer.Rows[n_row]["c_numhortra"]);
                    //entTmpDetPer.n_canhorper = Convert.ToDouble(dtResultLisPer.Rows[n_row]["n_canhorper"]);
                    //entTmpDetPer.n_canhorpermax = Convert.ToDouble(dtResultLisPer.Rows[n_row]["n_canhorpermax"]);
                    //entTmpDetPer.n_preunidad = Convert.ToDouble(dtResultLisPer.Rows[n_row]["n_preunidad"]);
                    //entTmpDetPer.n_pagocal = Convert.ToDouble(dtResultLisPer.Rows[n_row]["n_pagocal"]);
                    //entTmpDetPer.n_pagproyhor = Convert.ToDouble(dtResultLisPer.Rows[n_row]["n_pagproyhor"]);
                    //entTmpDetPer.n_subsidio = Convert.ToDouble(dtResultLisPer.Rows[n_row]["n_subsidio"]);

                    lstSolicitudDet.Add(entTmpDetPer);
                }
            }
            else
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return b_result;
        }
        public bool ELiminar(int n_Idregistro)
        {
            DataTable dtResult = new DataTable();
            bool b_result = false;
            CN_pro_solicitudtareasdiversas xFun2 = new CN_pro_solicitudtareasdiversas();
            CD_pro_produccion xFun3 = new CD_pro_produccion();
            CD_pro_solicitudtareasdiversas miFun = new CD_pro_solicitudtareasdiversas();

            // TRAEMOS EL REGISTRO PARA OBTENER EL ID DE LA PRODUCCION
            xFun2.mysConec = mysConec;
            xFun2.TraerRegistro(n_Idregistro);
            entSolicitud = xFun2.entSolicitud;


            miFun.mysConec = mysConec;

            b_result = miFun.Eliminar(n_Idregistro);
            if (b_result == true)
            {
                // ACTUALIZAMOS EL ESTADO DE TAREA EN PRODUCCION PARA QUE SE PUEDA GENERAR TAREAS A LA PRODUCCION

                xFun3.mysConec = mysConec;
                b_result = xFun3.ActualizarEstadoTarea(entSolicitud.n_idpro, 1);

                if (b_result == false)
                {
                    b_OcurrioError = xFun3.booOcurrioError;
                    c_ErrorMensaje = xFun3.StrErrorMensaje;
                    n_ErrorNumber = xFun3.IntErrorNumber;
                }
            }
            else
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_result;
            }

            return b_result;
        }
        public bool Insertar(BE_PRO_SOLICITUDTAREASDIVERSAS entSolicitud, List<BE_PRO_SOLICITUDTAREASDIVERSASCAB> LstSolicitudCab, List<BE_PRO_SOLICITUDTAREASDIVERSASDET> LstSolicitudDet)
        {
            CD_pro_solicitudtareasdiversas miFun = new CD_pro_solicitudtareasdiversas();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(entSolicitud, LstSolicitudCab, LstSolicitudDet);
            if (booOk == true)
            {
                // ACTUALIZAMOS EL ESTADO DE TAREA EN PRODUCCION PARA QUE NO VUELVA A GENERAR TAREAS A LA PRODUCCION
                CD_pro_produccion xFun = new CD_pro_produccion();

                xFun.mysConec = mysConec;
                booOk = xFun.ActualizarEstadoTarea(entSolicitud.n_idpro, 2);

                if (booOk == false)
                {
                    b_OcurrioError = xFun.booOcurrioError;
                    c_ErrorMensaje = xFun.StrErrorMensaje;
                    n_ErrorNumber = xFun.IntErrorNumber;
                }
            }
            else
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return booOk;
        }
        public bool Actualizar(BE_PRO_SOLICITUDTAREASDIVERSAS entSolicitud, List<BE_PRO_SOLICITUDTAREASDIVERSASCAB> LstSolicitudCab, List<BE_PRO_SOLICITUDTAREASDIVERSASDET> LstSolicitudDet)
        {
            CD_pro_solicitudtareasdiversas miFun = new CD_pro_solicitudtareasdiversas();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(entSolicitud, LstSolicitudCab, LstSolicitudDet);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public void ImprimirSolicitudTar(int n_Idregistro, string c_CadenaIN)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[4, 3];

            arrPara[0, 0] = "n_idreg";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = n_Idregistro.ToString();

            if (STU_SISTEMA.SYS_VERDATALT == 2)
            {
                arrPara[1, 0] = "c_numruc";
                arrPara[1, 1] = "C";
                arrPara[1, 2] = "0000000000" + STU_SISTEMA.EMPRESAID.ToString();

                arrPara[2, 0] = "c_nomemp";
                arrPara[2, 1] = "C";
                arrPara[2, 2] = STU_SISTEMA.SYS_NOMALT;
            }
            else
            {
                arrPara[1, 0] = "c_numruc";
                arrPara[1, 1] = "C";
                arrPara[1, 2] = STU_SISTEMA.EMPRESARUC;

                arrPara[2, 0] = "c_nomemp";
                arrPara[2, 1] = "C";
                arrPara[2, 2] = STU_SISTEMA.EMPRESANOMBRE;
            }

            arrPara[3, 0] = "c_con";
            arrPara[3, 1] = "C";
            arrPara[3, 2] = c_CadenaIN;

            c_NomArchivo = "RptSolicitudTareas.rpt";
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "produccion\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "PRODUCCION - SOLICITUD DE TAREAS";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
        public void ImprimirSolicitudTarPer(int n_Idregistro, string c_CadenaIN)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[4, 3];

            arrPara[0, 0] = "n_idreg";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = n_Idregistro.ToString();

            if (STU_SISTEMA.SYS_VERDATALT == 2)
            {
                arrPara[1, 0] = "c_numruc";
                arrPara[1, 1] = "C";
                arrPara[1, 2] = "0000000000" + STU_SISTEMA.EMPRESAID.ToString();

                arrPara[2, 0] = "c_nomemp";
                arrPara[2, 1] = "C";
                arrPara[2, 2] = STU_SISTEMA.SYS_NOMALT;
            }
            else
            {
                arrPara[1, 0] = "c_numruc";
                arrPara[1, 1] = "C";
                arrPara[1, 2] = STU_SISTEMA.EMPRESARUC;

                arrPara[2, 0] = "c_nomemp";
                arrPara[2, 1] = "C";
                arrPara[2, 2] = STU_SISTEMA.EMPRESANOMBRE;
            }

            arrPara[3, 0] = "c_con";
            arrPara[3, 1] = "C";
            arrPara[3, 2] = c_CadenaIN;

            c_NomArchivo = "RptSolicitudTareasTra.rpt";
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "produccion\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "PRODUCCION - SOLICITUD DE TAREAS";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
        public bool BuscarProduccionEnSolicitud(int n_idProduccion, int n_IdEmpresa)
        {
            bool b_result = false;
            DataTable dtResult = new DataTable();
            CD_pro_solicitudtareasdiversas miFun = new CD_pro_solicitudtareasdiversas();
            miFun.mysConec = mysConec;

            if (miFun.BuscarProduccionEnSolicitud(n_idProduccion, n_IdEmpresa) == true)
            {
                dtResult = miFun.dtLista;
                if (dtResult.Rows.Count == 0)
                {
                    b_result = false;       // FALSO INDICA QUE NO SE ENCONTRO SOLICITUDES DE PRODUCCION REFERENCIADAS A AL PRODUCCION
                }
                else
                {
                    b_result = true;       // FALSO INDICA QUE SE ENCONTRO SOLICITUDES DE PRODUCCION REFERENCIADAS A AL PRODUCCION
                }
            }
            else
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return b_result;
        }
        public bool MarcarRevisado(int n_IdSolicitud, int n_IdEstado)
        {
            int n_estado = 0;
            DataTable dtResult = new DataTable();
            bool booOk = false;
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            CD_pro_solicitudtareasdiversas o_SolTar = new CD_pro_solicitudtareasdiversas();
            o_SolTar.mysConec = mysConec;
            o_SolTar.MarcarRevisado(n_IdSolicitud, n_IdEstado);
            if (o_SolTar.b_OcurrioError == true)
            {
                MessageBox.Show("¡ No se pudo visar como revisado la solicituda de tarea !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else
            {
                booOk = true;
            }
            return booOk;
        }
    }
}
