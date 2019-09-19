using System;
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
    public class CN_pro_solicitudamateriales
    {
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();

        private DataTable _dtLista;
        private DataTable _dtRegistro;
        private DataTable _dtlstInsumos;
        
        public DataTable dtListaMatEnt;

        private BE_PRO_SOLICITUDMATERIALES _entSolicitud;
        private List<BE_PRO_SOLICITUDMATERIALESDET> _lstSolicitudDet;

        private bool _b_OcurrioError;
        private string _c_ErrorMensaje;
        private int _n_ErrorNumber;

        private MySqlConnection _mysConec;

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();
        Genericas funDatos = new Genericas();
        
        public BE_PRO_SOLICITUDMATERIALES entSolicitud
        {
            get { return _entSolicitud; }
            set { _entSolicitud = value; }
        }
        public List<BE_PRO_SOLICITUDMATERIALESDET> lstSolicitudDet
        {
            get { return _lstSolicitudDet; }
            set { _lstSolicitudDet = value; }
        }
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

            CD_pro_solicitudamateriales miFun = new CD_pro_solicitudamateriales();
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
            CD_pro_solicitudamateriales miFun = new CD_pro_solicitudamateriales();
            List<BE_PRO_SOLICITUDMATERIALESDET> lstDetalle = new List<BE_PRO_SOLICITUDMATERIALESDET>(); 
            
            miFun.mysConec = mysConec;
            b_result = miFun.TraerRegistro(n_Idregistro);
            if (b_result == true)
            {
                dtResult = miFun.dtRegistro;
                dtResultLis= miFun.dtRegistroDet;

                BE_PRO_SOLICITUDMATERIALES entTmp = new BE_PRO_SOLICITUDMATERIALES();

                entTmp.n_idemp= Convert.ToInt32(dtResult.Rows[0]["n_idemp"]);
                entTmp.n_id = Convert.ToInt32(dtResult.Rows[0]["n_id"]);
                entTmp.n_idtipdoc = Convert.ToInt32(dtResult.Rows[0]["n_idtipdoc"]);
                entTmp.c_numser = dtResult.Rows[0]["c_numser"].ToString();
                entTmp.c_numdoc = dtResult.Rows[0]["c_numdoc"].ToString();
                entTmp.d_fchreg = Convert.ToDateTime(dtResult.Rows[0]["d_fchreg"]);
                entTmp.n_idsol = Convert.ToInt32(dtResult.Rows[0]["n_idsol"]);
                entTmp.n_idprogra = Convert.ToInt32(dtResult.Rows[0]["n_idprogra"]);
                entTmp.n_idordpro = Convert.ToInt32(dtResult.Rows[0]["n_idordpro"]);
                entTmp.n_idite = Convert.ToInt32(dtResult.Rows[0]["n_idite"]);
                entTmp.n_idrec = Convert.ToInt32(dtResult.Rows[0]["n_idrec"]);
                entTmp.c_obs = dtResult.Rows[0]["c_obs"].ToString();
                entTmp.n_idalm = Convert.ToInt32(dtResult.Rows[0]["n_idalm"]);
                entTmp.n_can = Convert.ToDouble(dtResult.Rows[0]["n_can"]);
                entTmp.d_fchent = Convert.ToDateTime(dtResult.Rows[0]["d_fchent"]);
                entTmp.n_idpro = Convert.ToInt32(dtResult.Rows[0]["n_idpro"]);
                entSolicitud = entTmp;

                for(n_row = 0;n_row <= dtResultLis.Rows.Count - 1; n_row++)
                {
                    BE_PRO_SOLICITUDMATERIALESDET entTmpDet = new BE_PRO_SOLICITUDMATERIALESDET();

                    entTmpDet.n_idsol = Convert.ToInt32(dtResultLis.Rows[n_row]["n_idsol"]);
                    entTmpDet.n_idite = Convert.ToInt32(dtResultLis.Rows[n_row]["n_idite"]);
                    entTmpDet.n_idunimed = Convert.ToInt32(dtResultLis.Rows[n_row]["n_idunimed"]);
                    entTmpDet.n_canteo = Convert.ToDouble(dtResultLis.Rows[n_row]["n_canteo"]);
                    entTmpDet.n_canent = Convert.ToDouble(dtResultLis.Rows[n_row]["n_canent"]);
                    entTmpDet.c_numlot = dtResultLis.Rows[n_row]["c_numlot"].ToString();
                    entTmpDet.n_impval = Convert.ToDouble(dtResultLis.Rows[n_row]["n_impval"]);

                    lstDetalle.Add(entTmpDet);
                }
                lstSolicitudDet = lstDetalle;
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
            CD_pro_solicitudamateriales miFun = new CD_pro_solicitudamateriales();
            BE_PRO_SOLICITUDMATERIALES entSolMat = new BE_PRO_SOLICITUDMATERIALES();

            miFun.mysConec = mysConec;
            TraerRegistro(n_Idregistro);
            entSolMat = entSolicitud;

            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            b_result = miFun.Eliminar(n_Idregistro);
            if (b_result == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_result;
            }
            else
            {
                b_result = ActualizarProduccion(miFun, entSolMat);
            }

            return b_result;
        }
        bool ActualizarProduccion(CD_pro_solicitudamateriales miFun, BE_PRO_SOLICITUDMATERIALES entSolicitudU)
        {
            int n_estado = 0;
            DataTable dtResult = new DataTable();
            bool booOk = false;
            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            // ACTUALIZAMOS EL ESTADO DE LA SOLICITUD DE MATERUALES EN LA PRODUCCION
            miFun.Consulta5(entSolicitudU.n_idpro, entSolicitudU.n_idite, entSolicitudU.n_idrec, entSolicitudU.n_can);
            dtResult = miFun.dtLista;
            if (dtResult.Rows.Count == 0)
            {
                n_estado = 2;      // SE CIERRA LA SOICITUD DE MATERIALES DE LA PRODUCCION
            }
            else
            {
                n_estado = 1;      // SE ABRE LA SOICITUD DE MATERIALES DE LA PRODUCCION
            }
            // SI LA LISTA DE ITEMS ESTA VACIA QUIERE DECIR QUE YA SE ENTREGARON TODOS LOS INSUMOS,
            // ENTONCES CERRAMOS LA SOLICITUD DE MATERIALES DE LA PRODUCCION
            CN_pro_produccion objProduccion = new CN_pro_produccion();
            objProduccion.mysConec = mysConec;
            objProduccion.ActualizarEstadoSolicitud(entSolicitudU.n_idpro, n_estado);
            if (objProduccion.booOcurrioError == true)
            {
                MessageBox.Show("¡ No se pudo cerrar la solicitud de materialea de la produccion !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else
            { 
                booOk = true;
            }
            return booOk;
        }
        public bool Insertar(BE_PRO_SOLICITUDMATERIALES entSolicitudU, List<BE_PRO_SOLICITUDMATERIALESDET> LstSolicitudDetU)
        {
            CD_pro_solicitudamateriales miFun = new CD_pro_solicitudamateriales();
            bool booOk = false;

            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            miFun.mysConec = mysConec;
            
            booOk = miFun.Insertar(entSolicitudU, LstSolicitudDetU);
            if (booOk == true)
            {
                CD_pro_programa xFun = new CD_pro_programa();
                
                xFun.mysConec = mysConec;
                booOk = xFun.ActualizarEstadoProductos(entSolicitudU.n_idprogra, entSolicitudU.n_idordpro, entSolicitudU.n_idite, entSolicitudU.d_fchent.ToString("dd/MM/yyyy").Substring(0,10), 1);

                if (booOk == true)
                {
                    booOk = ActualizarProduccion(miFun, entSolicitudU);
                }
                else
                {
                    b_OcurrioError = miFun.b_OcurrioError;
                    c_ErrorMensaje = miFun.c_ErrorMensaje;
                    n_ErrorNumber = miFun.n_ErrorNumber;
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
        public bool Actualizar(BE_PRO_SOLICITUDMATERIALES entSolicitudU, List<BE_PRO_SOLICITUDMATERIALESDET> LstSolicitudDetU)
        {
            CD_pro_solicitudamateriales miFun = new CD_pro_solicitudamateriales();
            bool booOk = false;
            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(entSolicitudU, LstSolicitudDetU);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public void ImprimirSolicitudMat( int n_Idregistro)
        {
            DataTable dtResult = new DataTable();
            int n_numpro = 0;
            int n_ordpro = 0;
            int n_idite = 0;
            string c_fchent = "";

            CD_pro_solicitudamateriales miFun = new CD_pro_solicitudamateriales();

            miFun.mysConec = mysConec;
            miFun.TraerRegistro(n_Idregistro);
            dtResult = miFun.dtRegistro;

            if (dtResult.Rows.Count != 0)
            {
                n_numpro = Convert.ToInt32(dtResult.Rows[0]["n_idprogra"]);
                n_ordpro = Convert.ToInt32(dtResult.Rows[0]["n_idordpro"]);
                n_idite = Convert.ToInt32(dtResult.Rows[0]["n_idite"]);
                c_fchent = dtResult.Rows[0]["d_fchent"].ToString().Substring(0, 10);

                string c_NomArchivo = "";
                string c_Ruta = "";
                string[,] arrPara = new string[3, 3];

                arrPara[0, 0] = "n_idsol";
                arrPara[0, 1] = "N";
                arrPara[0, 2] = n_Idregistro.ToString();

                arrPara[1, 0] = "c_nomemp";
                arrPara[1, 1] = "C";
                arrPara[1, 2] = STU_SISTEMA.EMPRESANOMBRE;

                arrPara[2, 0] = "c_numruc";
                arrPara[2, 1] = "C";
                arrPara[2, 2] = STU_SISTEMA.EMPRESARUC;

                c_NomArchivo = "RptSolicitudMateriales.rpt";
                //c_Ruta = @"j:\ssf-net\reportes\Produccion\" + c_NomArchivo;
                c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "produccion\\" + c_NomArchivo;

                Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
                xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
                xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
                xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
                xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
                xVisor.b_VisPrev = true;
                xVisor.c_Titulo = "PRODUCCION - SOLICITUD DE MATERIALES";
                xVisor.c_PathRep = c_Ruta;
                xVisor.arrParametros = arrPara;
                xVisor.VerCrystal();
            }
        }
        public bool Consulta2(int n_IdOrdPro, int n_idite, int n_idrec, string c_FechaEntrega)
        {
            bool b_result = false;

            CD_pro_solicitudamateriales miFun = new CD_pro_solicitudamateriales();
            miFun.mysConec = mysConec;

            if (miFun.Consulta2(n_IdOrdPro, n_idite, n_idrec, c_FechaEntrega) == true)
            {
                b_result = true;
                dtLista = miFun.dtLista;
                dtlstInsumos = miFun.dtlstInsumos;
            }
            else
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return b_result;
        }
        public bool Consulta3(int n_IdEmpresa)
        {
            bool b_result = false;

            CD_pro_solicitudamateriales miFun = new CD_pro_solicitudamateriales();
            miFun.mysConec = mysConec;

            if (miFun.Consulta3(n_IdEmpresa) == true)
            {
                b_result = true;
                dtLista = miFun.dtLista;
                dtlstInsumos = miFun.dtlstInsumos;
            }
            else
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return b_result;
        }
        public bool Consulta4(int n_IdEmpresa)
        {
            bool b_result = false;

            CD_pro_solicitudamateriales miFun = new CD_pro_solicitudamateriales();
            miFun.mysConec = mysConec;

            if (miFun.Consulta4(n_IdEmpresa) == true)
            {
                b_result = true;
                dtLista = miFun.dtLista;
                dtlstInsumos = miFun.dtlstInsumos;
            }
            else
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return b_result;
        }
        public bool Consulta5(int n_idProduccion, int n_idProducto, int n_IdReceta, double n_CantidadProducto)
        {
            bool b_result = false;

            CD_pro_solicitudamateriales miFun = new CD_pro_solicitudamateriales();
            miFun.mysConec = mysConec;

            if (miFun.Consulta5(n_idProduccion, n_idProducto, n_IdReceta, n_CantidadProducto) == true)
            {
                b_result = true;
                dtLista = miFun.dtLista;
                dtlstInsumos = miFun.dtlstInsumos;
            }
            else
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return b_result;
        }
        public bool BuscarProduccionEnSolicitud(int n_idProduccion, int n_IdEmpresa)
        {
            bool b_result = false;
            DataTable dtResult = new DataTable();
            CD_pro_solicitudamateriales miFun = new CD_pro_solicitudamateriales();
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
                    b_result = true;       // TRUE INDICA QUE SE ENCONTRO SOLICITUDES DE PRODUCCION REFERENCIADAS A AL PRODUCCION
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
        public bool ListarResumenSolMat(int n_IdProduccion, int n_IdEmpresa)
        {
            bool b_result = false;

            CD_pro_solicitudamateriales miFun = new CD_pro_solicitudamateriales();
            miFun.mysConec = mysConec;

            if (miFun.ListarResumenSolMat(n_IdProduccion, n_IdEmpresa) == true)
            {
                b_result = true;
                dtListaMatEnt = miFun.dtListaMatEnt;
            }
            else
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return b_result;
        }
        public DataTable SolicitudPendienteJalar(int n_IdEmpresa)
        {
            DataTable dtResult = new DataTable();
            CD_pro_solicitudamateriales objsol = new CD_pro_solicitudamateriales();
            string[,] arrCabeceraFlexFil = new string[9, 5];

            objsol.mysConec = mysConec;
            if (objsol.Consulta6(n_IdEmpresa)==true)
            {
                dtResult = objsol.dtLista;
                // FLEX GRID DE LOS TAREAS
                arrCabeceraFlexFil[0, 0] = "Nº Documento";
                arrCabeceraFlexFil[0, 1] = "110";
                arrCabeceraFlexFil[0, 2] = "C";
                arrCabeceraFlexFil[0, 3] = "c_numdoc";

                arrCabeceraFlexFil[1, 0] = "T.D.";
                arrCabeceraFlexFil[1, 1] = "40";
                arrCabeceraFlexFil[1, 2] = "C";
                arrCabeceraFlexFil[1, 3] = "c_abr";

                arrCabeceraFlexFil[2, 0] = "Dia";
                arrCabeceraFlexFil[2, 1] = "30";
                arrCabeceraFlexFil[2, 2] = "C";
                arrCabeceraFlexFil[2, 3] = "n_dia";

                arrCabeceraFlexFil[3, 0] = "Mes";
                arrCabeceraFlexFil[3, 1] = "30";
                arrCabeceraFlexFil[3, 2] = "C";
                arrCabeceraFlexFil[3, 3] = "n_mes";

                arrCabeceraFlexFil[4, 0] = "Año";
                arrCabeceraFlexFil[4, 1] = "40";
                arrCabeceraFlexFil[4, 2] = "C";
                arrCabeceraFlexFil[4, 3] = "n_ano";

                arrCabeceraFlexFil[5, 0] = "Producto";
                arrCabeceraFlexFil[5, 1] = "300";
                arrCabeceraFlexFil[5, 2] = "C";
                arrCabeceraFlexFil[5, 3] = "c_despro";

                arrCabeceraFlexFil[6, 0] = "Contidad";
                arrCabeceraFlexFil[6, 1] = "70";
                arrCabeceraFlexFil[6, 2] = "D";
                arrCabeceraFlexFil[6, 3] = "n_can";

                arrCabeceraFlexFil[7, 0] = "Responsable";
                arrCabeceraFlexFil[7, 1] = "200";
                arrCabeceraFlexFil[7, 2] = "D";
                arrCabeceraFlexFil[7, 3] = "c_resapenom";

                arrCabeceraFlexFil[8, 0] = "Id";
                arrCabeceraFlexFil[8, 1] = "0";
                arrCabeceraFlexFil[8, 2] = "N";
                arrCabeceraFlexFil[8, 3] = "n_id";

                funDatos.Buscar_CampoBusqueda = "c_numdoc";
                funDatos.Buscar_CadFiltro = "";
                funDatos.Buscar_CampoOrden = "c_numdoc";
                funDatos.Buscar_Titulo = "Documentos Pendientes de Visar por Almacen";
                dtResult = funDatos.Buscar(arrCabeceraFlexFil, dtResult);               
            }
            return dtResult;
        }
    }
}
