using MySql.Data.MySqlClient;
using SIAC_DATOS.Estacionamiento;
using SIAC_DATOS.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using SIAC_Entidades.Estacionamiento;
using SIAC_Entidades.Tesoreria;
using SIAC_Objetos;
using Helper.Comunes;
using SIAC_Negocio.Contabilidad;
using SIAC_Negocio.Tesoreria;
using Helper;
using SIAC_DATOS.Tesoreria;

namespace SIAC_Negocio.Estacionamiento
{
    public class CN_est_liquidacion
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA;
        public BE_EST_LIQUIDACION e_Liquidacion = new BE_EST_LIQUIDACION();
        public List<BE_EST_LIQUIDACIONDET> l_LiquidacionDet = new List<BE_EST_LIQUIDACIONDET>();

        public BE_TES_TESORERIA e_tes = new BE_TES_TESORERIA();
        public List<BE_TES_TESORERIADES> l_tesdes = new List<BE_TES_TESORERIADES>();
        public List<BE_TES_TESORERIADESDET> l_tesdesdet = new List<BE_TES_TESORERIADESDET>();
        public List<BE_TES_TESORERIAORI> l_tesori = new List<BE_TES_TESORERIAORI>();
        public List<BE_TES_TESORERIAORIDET> l_tesoridet = new List<BE_TES_TESORERIAORIDET>();

        Helper.Comunes.Funciones fungen = new Helper.Comunes.Funciones();
        CD_est_liquidacion miFun;
        public DataTable dtListar = new DataTable();
        public DataTable dtListarDet = new DataTable();

        public CN_est_liquidacion(SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA)
        {
            CD_est_liquidacion xFun = new CD_est_liquidacion(STU_SISTEMA.BD_IP, STU_SISTEMA.BD_NOMBASEDATOS, STU_SISTEMA.BD_USUARIO, STU_SISTEMA.BD_CONTRASEÑA, STU_SISTEMA.BD_PUERTO);
            miFun = xFun;
        }

        ~CN_est_liquidacion()
        {
            miFun = null;
            fungen = null;
            e_Liquidacion = null;
            l_LiquidacionDet = null;
            e_tes = null;
            l_tesdes = null;
            l_tesdesdet = null;
            l_tesori = null;
            l_tesoridet = null;
        }

        public DataTable Listar(int n_IdEmpresa, int AnoTrabajo, int MesTrabajo)
        {
            DataTable dtResul = new DataTable();

            miFun.Listar(n_IdEmpresa, AnoTrabajo, MesTrabajo);
            dtListar = miFun.dtListar;

            if (dtResul == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return dtResul;
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            DataTable dtResult = new DataTable();
            DataTable dtliquidadet = new DataTable();
            int n_row = 0;
            
            if (miFun.TraerRegistro(n_IdRegistro) == true)
            {
                dtListar = miFun.dtListar;
                dtliquidadet = miFun.dtLiquidaDet;
                dtListarDet = miFun.dtListarDetalle;

                if (dtListar.Rows.Count != 0)
                {
                    e_Liquidacion.n_idemp = Convert.ToInt32(dtListar.Rows[0]["n_idemp"]);
                    e_Liquidacion.n_idtipdoc = Convert.ToInt32(dtListar.Rows[0]["n_idtipdoc"]);
                    e_Liquidacion.c_numser = dtListar.Rows[0]["c_numser"].ToString();
                    e_Liquidacion.c_numdoc = dtListar.Rows[0]["c_numdoc"].ToString();
		            e_Liquidacion.n_id = Convert.ToInt32(dtListar.Rows[0]["n_idemp"]);
                    e_Liquidacion.n_idpla = Convert.ToInt32(dtListar.Rows[0]["n_idpla"]);
                    e_Liquidacion.n_idcaj = Convert.ToInt32(dtListar.Rows[0]["n_idcaj"]);
                    e_Liquidacion.d_fchemi = Convert.ToDateTime(dtListar.Rows[0]["d_fchemi"]);
                    e_Liquidacion.d_fchini = Convert.ToDateTime(dtListar.Rows[0]["d_fchini"]);
                    e_Liquidacion.d_fchfin = Convert.ToDateTime(dtListar.Rows[0]["d_fchfin"]);
                    e_Liquidacion.n_importe = Convert.ToDouble(dtListar.Rows[0]["n_importe"]);
                    e_Liquidacion.c_obs = dtListar.Rows[0]["c_obs"].ToString();
                    e_Liquidacion.n_numdoccob = Convert.ToInt32(dtListar.Rows[0]["n_numdoccob"]);
                    e_Liquidacion.h_horliq = dtListar.Rows[0]["h_horliq"].ToString();
                    e_Liquidacion.n_tipo = Convert.ToInt32(dtListar.Rows[0]["n_tipo"]);
                }

                if (dtliquidadet.Rows.Count != 0)
                {
                    for (n_row = 0; n_row <= dtliquidadet.Rows.Count - 1; n_row++)
                    {
                        BE_EST_LIQUIDACIONDET e_LiquidaDet = new BE_EST_LIQUIDACIONDET();

                        e_LiquidaDet.n_idliq = Convert.ToInt32(dtliquidadet.Rows[n_row]["n_idliq"]);
                        e_LiquidaDet.n_idven = Convert.ToInt32(dtliquidadet.Rows[n_row]["n_idven"]);
                        e_LiquidaDet.n_impcob = Convert.ToDouble(dtliquidadet.Rows[n_row]["n_impcob"]);
                        
                        l_LiquidacionDet.Add(e_LiquidaDet);
                    }
                }
            }
            else
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool booOk = false;

            booOk = miFun.Eliminar(n_IdRegistro);
            if (booOk == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;

            }
            return booOk;
        }
        public bool Insertar(BE_EST_LIQUIDACION e_Liquidacion, List<BE_EST_LIQUIDACIONDET> e_LiquidacionDet)
        {
            MySqlConnection mysConec = new MySqlConnection();
            DatosMySql FunMysql = new DatosMySql();
            bool booOk = false;

            miFun.e_tes = e_tes;
            miFun.l_tesdes = l_tesdes;
            miFun.l_tesdesdet = l_tesdesdet;
            miFun.l_tesori = l_tesori;
            miFun.l_tesoridet = l_tesoridet;
            if (miFun.Insertar(e_Liquidacion, e_LiquidacionDet) == true)
            {
                CD_est_conecta micon = new CD_est_conecta(STU_SISTEMA.BD_IP, STU_SISTEMA.BD_NOMBASEDATOS, STU_SISTEMA.BD_USUARIO, STU_SISTEMA.BD_CONTRASEÑA, STU_SISTEMA.BD_PUERTO);
                mysConec = micon.mysConec;
                int n_idtes = miFun.n_idtesoreria;
                string c_NumAsi = "";
                int n_tipregistro = 1;                               // LE INDICAMOS A TESORERIA QUE ESTAMOS GENERANDO UN INGRESO     
                CN_con_diario funCon = new CN_con_diario();
                CD_tes_tesoreria miFuntes = new CD_tes_tesoreria();

                funCon.mysConec = mysConec;
                funCon.STU_SISTEMA = STU_SISTEMA;

                if (funCon.GenerarAsientoTesoreria(e_tes.n_idemp, Convert.ToInt32(n_idtes), e_tes.n_ano, e_tes.n_mes, 1, c_NumAsi, n_tipregistro) == true)
                {
                    mysConec = FunMysql.ReAbrirConeccion(mysConec);
                    c_NumAsi = funCon.c_NewNumAsiento;
                    miFuntes.mysConec = mysConec;
                    miFuntes.AgregarNumAsi(n_idtes, c_NumAsi);

                    booOk = true;
                }
                else
                {
                    b_OcurrioError = funCon.b_OcurrioError;
                    c_ErrorMensaje = funCon.c_ErrorMensaje;
                    n_ErrorNumber = funCon.n_ErrorNumber;
                    return booOk;
                }
            }
            

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_EST_LIQUIDACION e_Liquidacion, List<BE_EST_LIQUIDACIONDET> e_LiquidacionDet)
        {
            bool booOk = false;

            booOk = miFun.Actualizar(e_Liquidacion, e_LiquidacionDet);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public void Consulta1(int n_IdEmpresa, int n_IdPlaya, int n_Idcajero, string c_FechaInicio, string c_FechaFin, int n_Tipo)
        {
            miFun.Consulta1(n_IdEmpresa, n_IdPlaya, n_Idcajero, c_FechaInicio, c_FechaFin, n_Tipo);
            dtListar = miFun.dtListar;

            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return;
        }
        public void ImprimirLiquidacion(int n_IdRegistro, int n_TipoImpresora)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[1, 3];

            arrPara[0, 0] = "n_idreg";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = n_IdRegistro.ToString();

            if (n_TipoImpresora == 1) { c_NomArchivo = "RptLiquidacion.rpt"; }
            if (n_TipoImpresora == 2) { c_NomArchivo = "Rpt_LiquidacionTiket.rpt"; }
            

            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "estacionamientos\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "ESTACIONAMIENTO - LIQUIDACION DE CAJA";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.b_Exportar = false;
            xVisor.c_NombreArchivoExportar = "";
            xVisor.VerCrystal();
        }
        public void ImprimirLiquidacionDia(int n_IdEmpresa, int n_IdPlaya, string c_Fecha)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[3, 3];

            arrPara[0, 0] = "n_idemp";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = n_IdEmpresa.ToString();

            arrPara[1, 0] = "n_idpla";
            arrPara[1, 1] = "N";
            arrPara[1, 2] = n_IdPlaya.ToString();

            arrPara[2, 0] = "c_fecha";
            arrPara[2, 1] = "N";
            arrPara[2, 2] = c_Fecha;

            c_NomArchivo = "Rpt_LiquidacionDia.rpt"; 
            
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "estacionamientos\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "ESTACIONAMIENTO - LIQUIDACION DE CAJA X DIA";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.b_Exportar = false;
            xVisor.c_NombreArchivoExportar = "";
            xVisor.VerCrystal();
        }
    }
}
