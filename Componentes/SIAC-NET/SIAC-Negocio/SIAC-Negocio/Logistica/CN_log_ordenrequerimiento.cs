using System;
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
    public class CN_log_ordenrequerimiento
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public DataTable dtOrdReqPendiente = new DataTable();
        public DataTable dtListaOrdenReq = new DataTable();
        public DataTable dtLista = new DataTable();
        Genericas funDatos = new Genericas();

        public BE_LOG_ORDENREQUERIMIENTO entReqCab = new BE_LOG_ORDENREQUERIMIENTO();
        public List<BE_LOG_ORDENREQUERIMIENTODET> lstReqDet = new  List<BE_LOG_ORDENREQUERIMIENTODET>();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();

        public DataTable ConsultaRequerimientoPendientes(int n_idempresa)
        {
            DataTable dtResul = new DataTable();

            CD_log_ordenrequerimiento miFun = new CD_log_ordenrequerimiento();
            miFun.mysConec = mysConec;

            dtResul = miFun.ConsultaRequerimientoPendientes(n_idempresa);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public bool ListarTodoRequerimientos(int n_IdEmpresa)
        {
            bool b_result = false;

            CD_log_ordenrequerimiento miFun = new CD_log_ordenrequerimiento();
            miFun.mysConec = mysConec;

            b_result = miFun.ListarTodoRequerimientos(n_IdEmpresa);

            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                dtListaOrdenReq = miFun.dtListaOrdenReq;
            }
            return b_result;
        }
        public bool ListarPendientes(int n_IdEmpresa, string c_CadenaIN)
        {
            bool b_Result;
            CD_log_ordenrequerimiento miFun = new CD_log_ordenrequerimiento();
            miFun.mysConec = mysConec;

            b_Result = miFun.ListarPendientes(n_IdEmpresa, c_CadenaIN);

            if (b_Result == true)
            {
                dtOrdReqPendiente = miFun.dtOrdenPendientes;
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return b_Result;
        }
        public DataTable OrdenesListarTodo(int n_IdEmpresa)
        {
            DataTable dtResult = new DataTable();
            CN_log_ordenrequerimiento objsol = new CN_log_ordenrequerimiento();
            string[,] arrCabeceraFlexFil = new string[7, 5];

            objsol.mysConec = mysConec;
            if (objsol.ListarTodoRequerimientos(n_IdEmpresa) == true)
            {
                dtResult = objsol.dtListaOrdenReq;
                // FLEX GRID DE LOS TAREAS
                arrCabeceraFlexFil[0, 0] = "Nº Documento";
                arrCabeceraFlexFil[0, 1] = "110";
                arrCabeceraFlexFil[0, 2] = "C";
                arrCabeceraFlexFil[0, 3] = "c_numdoc_cad";

                arrCabeceraFlexFil[1, 0] = "Fecha";
                arrCabeceraFlexFil[1, 1] = "40";
                arrCabeceraFlexFil[1, 2] = "C";
                arrCabeceraFlexFil[1, 3] = "d_fchemi";

                arrCabeceraFlexFil[2, 0] = "Obs";
                arrCabeceraFlexFil[2, 1] = "30";
                arrCabeceraFlexFil[2, 2] = "C";
                arrCabeceraFlexFil[2, 3] = "c_obs";

                arrCabeceraFlexFil[3, 0] = "Mes";
                arrCabeceraFlexFil[3, 1] = "30";
                arrCabeceraFlexFil[3, 2] = "C";
                arrCabeceraFlexFil[3, 3] = "n_mes";

                arrCabeceraFlexFil[4, 0] = "Año";
                arrCabeceraFlexFil[4, 1] = "40";
                arrCabeceraFlexFil[4, 2] = "C";
                arrCabeceraFlexFil[4, 3] = "n_ano";
                
                arrCabeceraFlexFil[5, 0] = "Area";
                arrCabeceraFlexFil[5, 1] = "110";
                arrCabeceraFlexFil[5, 2] = "C";
                arrCabeceraFlexFil[5, 3] = "c_area";

                arrCabeceraFlexFil[6, 0] = "Id";
                arrCabeceraFlexFil[6, 1] = "0";
                arrCabeceraFlexFil[6, 2] = "N";
                arrCabeceraFlexFil[6, 3] = "n_id";

                funDatos.Buscar_CampoBusqueda = "c_numdoc_cad";
                funDatos.Buscar_CadFiltro = "";
                funDatos.Buscar_CampoOrden = "c_numdoc_cad";
                funDatos.Buscar_Titulo = "Documentos Pendientes de Visar por Almacen";
                dtResult = funDatos.Buscar(arrCabeceraFlexFil, dtResult);
            }
            return dtResult;
        }
        public bool ActualizarEstadoRequerimiento(int n_IdRegistro, int n_IdEstado)
        {
            CD_log_ordenrequerimiento miFun = new CD_log_ordenrequerimiento();
            bool booOk = false;

            miFun.mysConec = mysConec;

            booOk = miFun.ActualizarEstadoRequerimiento(n_IdRegistro, n_IdEstado);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool TraerTodo(int n_IdEmpresa)
        {
            bool b_Result;
            CD_log_ordenrequerimiento miFun = new CD_log_ordenrequerimiento();
            miFun.mysConec = mysConec;

            b_Result = miFun.TraerTodo(n_IdEmpresa);

            if (b_Result == true)
            {
                dtLista = miFun.dtLista;
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return b_Result;
        }
        // ********************
        // ****    CRUD    ****
        // ********************
        public DataTable Listar(int n_idempresa, int n_idmes, int n_idano)
        {
            DataTable dtResul = new DataTable();

            CD_log_ordenrequerimiento miFun = new CD_log_ordenrequerimiento();
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar(n_idempresa, n_idmes, n_idano);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public bool TraerRegistro(int n_IdRegistro)
        {
            bool booresult = false;
            DataTable dtCab = new DataTable();
            DataTable dtDet = new DataTable();
            BE_LOG_ORDENREQUERIMIENTO entreque = new BE_LOG_ORDENREQUERIMIENTO();

            CD_log_ordenrequerimiento miFun = new CD_log_ordenrequerimiento();
            miFun.mysConec = mysConec;
            lstReqDet.Clear();
            if (miFun.TraerRegistro(n_IdRegistro) ==true)
            {
                dtCab = miFun.dtCabecera;
                dtDet = miFun.dtDetalle;

                entReqCab.n_idemp = Convert.ToInt32(dtCab.Rows[0]["n_idemp"]);
                entReqCab.n_id = Convert.ToInt32(dtCab.Rows[0]["n_id"]);
                entReqCab.n_idtipdoc = Convert.ToInt32(dtCab.Rows[0]["n_idtipdoc"]);
                entReqCab.c_numser = dtCab.Rows[0]["c_numser"].ToString();
                entReqCab.c_numdoc = dtCab.Rows[0]["c_numdoc"].ToString();
                entReqCab.n_anotra = Convert.ToInt16(dtCab.Rows[0]["n_anotra"]);
                entReqCab.n_mestra = Convert.ToInt16(dtCab.Rows[0]["n_mestra"]);

                if (xFun.NulosC(dtCab.Rows[0]["d_fchemi"].ToString()) != "")
                {
                    entReqCab.d_fchemi = Convert.ToDateTime(dtCab.Rows[0]["d_fchemi"].ToString());
                }
                if (xFun.NulosC(dtCab.Rows[0]["d_fchent"].ToString()) != "")
                {
                    entReqCab.d_fchent = Convert.ToDateTime(dtCab.Rows[0]["d_fchent"].ToString());
                }
                entReqCab.n_idloc = Convert.ToInt32(dtCab.Rows[0]["n_idloc"]);
                entReqCab.n_idare = Convert.ToInt32(dtCab.Rows[0]["n_idare"]);
                entReqCab.n_idpersol = Convert.ToInt32(dtCab.Rows[0]["n_idpersol"]);
                entReqCab.n_idpri = Convert.ToInt32(dtCab.Rows[0]["n_idpri"]);
                entReqCab.c_obs = dtCab.Rows[0]["c_obs"].ToString();
                entReqCab.n_idest =Convert.ToInt32( dtCab.Rows[0]["n_idest"]);
                entReqCab.n_idmot = Convert.ToInt32(dtCab.Rows[0]["n_idmot"]);
                int n_row = 0;

                for (n_row = 0; n_row <= dtDet.Rows.Count - 1; n_row++)
                { 
                    BE_LOG_ORDENREQUERIMIENTODET detreq = new BE_LOG_ORDENREQUERIMIENTODET();

                    detreq.n_idreq = Convert.ToInt16(dtDet.Rows[n_row]["n_idreq"]);
                    detreq.n_idite = Convert.ToInt16(dtDet.Rows[n_row]["n_idite"]);
                    detreq.n_idunimed = Convert.ToInt16(dtDet.Rows[n_row]["n_idunimed"]);
                    detreq.n_can = Convert.ToInt16(dtDet.Rows[n_row]["n_can"]);
                    lstReqDet.Add(detreq);
                }
                booresult = true;
            }
            return booresult;
        }
        public bool Insertar(BE_LOG_ORDENREQUERIMIENTO entRequerimiento, List<BE_LOG_ORDENREQUERIMIENTODET> lstDetalle)
        {

            CD_log_ordenrequerimiento miFun = new CD_log_ordenrequerimiento();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(entRequerimiento, lstDetalle);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_LOG_ORDENREQUERIMIENTO entRequerimiento, List<BE_LOG_ORDENREQUERIMIENTODET> lstDetalle)
        {
            CD_log_ordenrequerimiento miFun = new CD_log_ordenrequerimiento();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(entRequerimiento, lstDetalle);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Eliminar(int n_Id)
        {
            CD_log_ordenrequerimiento miFun = new CD_log_ordenrequerimiento();
            bool booOk = false;

            miFun.mysConec = mysConec;

            booOk = miFun.Eliminar(n_Id);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public void ReportImprimirOrdReq(int n_idMovimiento)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[3, 3];

            arrPara[0, 0] = "n_id";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = n_idMovimiento.ToString();

            arrPara[1, 0] = "c_nomemp";
            arrPara[1, 1] = "N";
            arrPara[1, 2] = STU_SISTEMA.EMPRESANOMBRE;

            arrPara[2, 0] = "c_numruc";
            arrPara[2, 1] = "N";
            arrPara[2, 2] = STU_SISTEMA.EMPRESARUC;

            c_NomArchivo = "RptOrdenReq.rpt";
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "logistica\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "LOGISTICA - IMPRESION ORDEN DE REQUERIMIENTO";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
    }
}
