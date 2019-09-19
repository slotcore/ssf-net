using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades;
using SIAC_DATOS.Logistica;
using SIAC_DATOS.Contabilidad;
using SIAC_Negocio.Contabilidad;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Almacen;
using SIAC_Entidades.Logistica;

namespace SIAC_Negocio.Logistica
{
    public class CN_log_compras
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public string c_mensaje = "";
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public DataTable dtLista = new DataTable();
        public BE_LOG_COMPRAS e_Compras = new BE_LOG_COMPRAS();
        public List<BE_LOG_COMPRASDET> e_ComprasDet = new List<BE_LOG_COMPRASDET>();
        public List<BE_LOG_COMPRASDOC> e_ComprasDoc = new List<BE_LOG_COMPRASDOC>();

        Helper.Genericas fundatos = new Helper.Genericas();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        public bool BuscarDocumento(int n_IdProveedor, int n_IdTipoDocumento, string c_NumeroSerie, string c_NumeroDocumento, int n_IdEmpresa)
        {
            bool b_result = false;
            CD_log_compras miFun = new CD_log_compras();
            miFun.mysConec = mysConec;

            if (miFun.BuscarDocumento(n_IdProveedor, n_IdTipoDocumento, c_NumeroSerie, c_NumeroDocumento, n_IdEmpresa) == false)
            {
                b_OcurrioError = miFun.b_ocurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_result;
            }
            dtLista = miFun.dtLista;
            if (dtLista.Rows.Count != 0)
            {
                b_result = true;
            }
            
            return b_result;
        }
        public bool BuscarDocumento(int n_IdProveedor, int n_IdTipoDocumento, string c_NumeroSerie, string c_NumeroDocumento, int n_IdCompra, int n_IdEmpresa)
        {
            bool b_result = false;
            CD_log_compras miFun = new CD_log_compras();
            miFun.mysConec = mysConec;

            if (miFun.BuscarDocumento(n_IdProveedor, n_IdTipoDocumento, c_NumeroSerie, c_NumeroDocumento, n_IdCompra, n_IdEmpresa) == false)
            {
                b_OcurrioError = miFun.b_ocurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_result;
            }
            dtLista = miFun.dtLista;
            if (dtLista.Rows.Count != 0)
            {
                b_result = true;
            }

            return b_result;
        }
        public bool Listar(int n_idempresa, int n_idmes, int n_idano, int n_IdTipoCompra)
        {
            bool b_result = false;
            CD_log_compras miFun = new CD_log_compras();
            miFun.mysConec = mysConec;

            if (miFun.Listar(n_idempresa, n_idmes, n_idano, n_IdTipoCompra) == false)
            {
                b_OcurrioError = miFun.b_ocurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_result;
            }
            dtLista = miFun.dtLista;
            b_result = true;
            return b_result;
        }
        public bool Consulta1(int n_IdEmpresa, int n_IdProveedor)
        {
            bool b_result = false;
            CD_log_compras miFun = new CD_log_compras();
            miFun.mysConec = mysConec;

            if (miFun.Consulta1(n_IdEmpresa, n_IdProveedor) == false)
            {
                b_OcurrioError = miFun.b_ocurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_result;
            }
            dtLista = miFun.dtLista;
            b_result = true;
            return b_result;
        }
        public bool TraerRegistro(Int64 n_IdRegistro)
        {
            bool b_result = false;
            DataTable dtCab = new DataTable();
            DataTable dtDet = new DataTable();
            DataTable dtDoc = new DataTable();
            CD_log_compras miFun = new CD_log_compras();
            miFun.mysConec = mysConec;

            if (miFun.TraerRegistro(n_IdRegistro) == false)
            {
                b_OcurrioError = miFun.b_ocurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_result;
            }
            else
            { 
                dtCab = miFun.dtRegistro;
                dtDet = miFun.dtListaDet;
                dtDoc = miFun.dtListaDoc;
                // CARGAMOS LA CABECERA
                e_Compras.n_id = Convert.ToInt32(dtCab.Rows[0]["n_id"]);
                e_Compras.n_idemp = Convert.ToInt16(dtCab.Rows[0]["n_idemp"].ToString());
                e_Compras.n_anotra = Convert.ToInt16(dtCab.Rows[0]["n_anotra"].ToString());
                e_Compras.n_idmes = Convert.ToInt16(dtCab.Rows[0]["n_idmes"].ToString());
                e_Compras.n_idlib = Convert.ToInt16(dtCab.Rows[0]["n_idlib"].ToString());
                e_Compras.c_numreg = dtCab.Rows[0]["c_numreg"].ToString();
                e_Compras.n_idtippro = Convert.ToInt16(dtCab.Rows[0]["n_idtippro"].ToString());
                e_Compras.n_idpro = Convert.ToInt16(dtCab.Rows[0]["n_idpro"].ToString());
                e_Compras.n_idtipdoc = Convert.ToInt16(dtCab.Rows[0]["n_idtipdoc"].ToString());
                e_Compras.c_numser = dtCab.Rows[0]["c_numser"].ToString();
                e_Compras.c_numdoc = dtCab.Rows[0]["c_numdoc"].ToString();
                e_Compras.d_fchdoc = Convert.ToDateTime(dtCab.Rows[0]["d_fchdoc"].ToString());
                e_Compras.d_fchreg = Convert.ToDateTime(dtCab.Rows[0]["d_fchreg"].ToString());
                e_Compras.n_idconpag = Convert.ToInt16(dtCab.Rows[0]["n_idconpag"].ToString());
                e_Compras.d_fchven = Convert.ToDateTime(dtCab.Rows[0]["d_fchven"].ToString());
                e_Compras.n_idmon = Convert.ToInt16(dtCab.Rows[0]["n_idmon"].ToString());
                e_Compras.n_impbru = Convert.ToDouble(dtCab.Rows[0]["n_impbru"].ToString());
                e_Compras.n_impbru2 = Convert.ToDouble(dtCab.Rows[0]["n_impbru2"].ToString());
                e_Compras.n_impbru3 = Convert.ToDouble(dtCab.Rows[0]["n_impbru3"].ToString());
                e_Compras.n_impinaf = Convert.ToDouble(dtCab.Rows[0]["n_impinaf"].ToString());
                e_Compras.n_impigv = Convert.ToDouble(dtCab.Rows[0]["n_impigv"].ToString());
                e_Compras.n_impigv2 = Convert.ToDouble(dtCab.Rows[0]["n_impigv2"].ToString());
                e_Compras.n_impigv3 = Convert.ToDouble(dtCab.Rows[0]["n_impigv3"].ToString());
                e_Compras.n_impisc = Convert.ToDouble(dtCab.Rows[0]["n_impisc"].ToString());
                e_Compras.n_impotr = Convert.ToDouble(dtCab.Rows[0]["n_impotr"].ToString());
                e_Compras.n_imptotcom = Convert.ToDouble(dtCab.Rows[0]["n_imptotcom"].ToString());
                e_Compras.n_tc = Convert.ToDouble(dtCab.Rows[0]["n_tc"].ToString());
                e_Compras.n_impsal = Convert.ToDouble(dtCab.Rows[0]["n_impsal"].ToString());
                e_Compras.n_tasaigv = Convert.ToDouble(dtCab.Rows[0]["n_tasaigv"].ToString());
                e_Compras.c_glosa = dtCab.Rows[0]["c_glosa"].ToString();
                e_Compras.n_estado = Convert.ToInt16(dtCab.Rows[0]["n_estado"].ToString());
                e_Compras.n_idtipdocref = Convert.ToInt16(dtCab.Rows[0]["n_idtipdocref"].ToString());
                e_Compras.n_iddocref = Convert.ToInt16(dtCab.Rows[0]["n_iddocref"].ToString());
                e_Compras.n_idtipope = Convert.ToInt16(dtCab.Rows[0]["n_idtipope"].ToString());
                e_Compras.n_idtipcom = Convert.ToInt16(dtCab.Rows[0]["n_idtipcom"].ToString());

                e_Compras.n_idtipdocmod = Convert.ToInt16(xFun.NulosN(dtCab.Rows[0]["n_idtipdocmod"]));
                e_Compras.n_iddocmod = Convert.ToInt16(xFun.NulosN(dtCab.Rows[0]["n_iddocmod"]));
                e_Compras.n_idmotnc = Convert.ToInt16(xFun.NulosN(dtCab.Rows[0]["n_idmotnc"]));
                e_Compras.n_idmotnd = Convert.ToInt16(xFun.NulosN(dtCab.Rows[0]["n_idmotnd"]));

                e_Compras.n_tasa4ta = Convert.ToDouble(xFun.NulosN(dtCab.Rows[0]["n_tasa4ta"]));
                e_Compras.n_imp4ta = Convert.ToDouble(xFun.NulosN(dtCab.Rows[0]["n_imp4ta"]));

                e_Compras.n_tipcom = Convert.ToInt16(xFun.NulosN(dtCab.Rows[0]["n_tipcom"]));
                e_Compras.n_idordpro = Convert.ToInt16(xFun.NulosN(dtCab.Rows[0]["n_idordpro"]));

                // CARGAMOS EL DETALLE
                e_ComprasDet.Clear();
                foreach (DataRow dr in dtDet.Rows)
                {
                    BE_LOG_COMPRASDET Detalle = new BE_LOG_COMPRASDET();

                    Detalle.n_idcom = Convert.ToInt16(dr["n_idcom"].ToString());
                    Detalle.n_iditem = Convert.ToInt16(dr["n_iditem"].ToString());
                    Detalle.n_idunimed = Convert.ToInt16(dr["n_idunimed"].ToString());
                    Detalle.n_canpro = Convert.ToDouble(dr["n_canpro"].ToString());
                    Detalle.n_preunibru = Convert.ToDouble(dr["n_preunibru"].ToString());
                    Detalle.n_idpordsc = Convert.ToDouble(dr["n_idpordsc"].ToString());
                    Detalle.n_impdsc = Convert.ToDouble(dr["n_impdsc"].ToString());
                    Detalle.n_preuni = Convert.ToDouble(dr["n_preuni"].ToString());
                    Detalle.n_imptot = Convert.ToDouble(dr["n_imptot"].ToString());
                    Detalle.n_idtipafeigv = Convert.ToInt16(xFun.NulosN(dr["n_idtipafeigv"]));

                    e_ComprasDet.Add(Detalle);
                }

                // CARGAMOS LOS DOCUMENTOS REFERENCIADOS
                e_ComprasDoc.Clear();
                foreach (DataRow dr in dtDoc.Rows)
                {
                    BE_LOG_COMPRASDOC Detalle = new BE_LOG_COMPRASDOC();

                    Detalle.n_idcom = Convert.ToInt16(dr["n_idcom"].ToString());
                    Detalle.n_idtipdoc = Convert.ToInt16(dr["n_idtipdoc"].ToString());
                    Detalle.n_iddoc = Convert.ToInt16(dr["n_iddoc"].ToString());

                    e_ComprasDoc.Add(Detalle);
                }
            }
            b_result = true;
            return b_result;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool b_result = false;
            CD_log_compras miFun = new CD_log_compras();
            miFun.mysConec = mysConec;

            if (miFun.Eliminar(n_IdRegistro) == false)
            {
                b_OcurrioError = miFun.b_ocurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_result;
            }
            dtLista = miFun.dtLista;
            b_result = true;
            return b_result;
        }
        public bool Insertar(BE_LOG_COMPRAS e_Cabecera, List<BE_LOG_COMPRASDET> l_Detalle, List<BE_LOG_COMPRASDOC> lstDocumentos)
        {
            bool b_result = false;
            CD_log_compras miFun = new CD_log_compras();
            miFun.mysConec = mysConec;

            if (miFun.Insertar(e_Cabecera, l_Detalle, lstDocumentos) == false)
            {
                b_OcurrioError = miFun.b_ocurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_result;
            }
            else
            {
                string c_NumAsi = "";
                CN_con_diario funCon = new CN_con_diario();
                funCon.mysConec = mysConec;
                funCon.STU_SISTEMA = STU_SISTEMA;
                if (e_Cabecera.n_idtipdoc == 3)
                {
                    funCon.GenerarAsientoCompras(STU_SISTEMA.EMPRESAID, e_Cabecera.n_id, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO, 32, "");
                }
                else
                {
                    funCon.GenerarAsientoCompras(STU_SISTEMA.EMPRESAID, e_Cabecera.n_id, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO, 8, "");
                }
                c_NumAsi = funCon.c_NewNumAsiento;
                miFun.AgregarNumAsi(e_Cabecera.n_id, c_NumAsi);
            }
            dtLista = miFun.dtLista;
            b_result = true;
            return b_result;
        }
        public bool Actualizar(BE_LOG_COMPRAS e_Cabecera, List<BE_LOG_COMPRASDET> l_Detalle, List<BE_LOG_COMPRASDOC> lstDocumentos)
        {
            bool b_result = false;
            CD_log_compras miFun = new CD_log_compras();
            miFun.mysConec = mysConec;

            if (miFun.Actualizar(e_Cabecera, l_Detalle, lstDocumentos) == false)
            {
                b_OcurrioError = miFun.b_ocurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_result;
            }
            else
            {
                string c_NumAsi = e_Cabecera.c_numreg;
                CN_con_diario funCon = new CN_con_diario();
                funCon.mysConec = mysConec;
                funCon.STU_SISTEMA = STU_SISTEMA;

                if (e_Cabecera.n_idtipdoc == 3)
                {
                    funCon.GenerarAsientoCompras(STU_SISTEMA.EMPRESAID, e_Cabecera.n_id, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO, 32, c_NumAsi);
                }
                else
                {
                    funCon.GenerarAsientoCompras(STU_SISTEMA.EMPRESAID, e_Cabecera.n_id, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO, 8, c_NumAsi);
                }
            }
            dtLista = miFun.dtLista;
            b_result = true;
            return b_result;
        }
        public DataTable BuscarDocumentoProveedor(int n_IdEmpresa, int n_IdProveedor)
        {
            string[,] arrCabeceraDg1 = new string[5, 4];
            DataTable dtResult = new DataTable();
            string c_CampoBuscar = "n_id";
            string c_CadenaFiltro = "";

            Helper.Genericas funDatos = new Helper.Genericas();
            Consulta1(n_IdEmpresa, n_IdProveedor);
            dtResult = dtLista;
            
            arrCabeceraDg1[0, 0] = "Tipo Doc.";
            arrCabeceraDg1[0, 1] = "50";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "c_abr";

            arrCabeceraDg1[1, 0] = "Nº Documento";
            arrCabeceraDg1[1, 1] = "140";
            arrCabeceraDg1[1, 2] = "C";
            arrCabeceraDg1[1, 3] = "c_numdoc";

            arrCabeceraDg1[2, 0] = "Fecha Documento";
            arrCabeceraDg1[2, 1] = "90";
            arrCabeceraDg1[2, 2] = "F";
            arrCabeceraDg1[2, 3] = "d_fchdoc";

            arrCabeceraDg1[3, 0] = "Importe";
            arrCabeceraDg1[3, 1] = "80";
            arrCabeceraDg1[3, 2] = "D";
            arrCabeceraDg1[3, 3] = "n_imptotcom";

            arrCabeceraDg1[4, 0] = "Id";
            arrCabeceraDg1[4, 1] = "0";
            arrCabeceraDg1[4, 2] = "N";
            arrCabeceraDg1[4, 3] = "n_id";

            Helper.Genericas xFun = new Helper.Genericas();
            xFun.Buscar_CampoBusqueda = c_CampoBuscar;
            xFun.Buscar_CadFiltro = c_CadenaFiltro;
            dtResult = xFun.Buscar(arrCabeceraDg1, dtResult);

            return dtResult;
        }
        public bool RegeneraAsientos(int n_IdEmpresa, int n_IdMesTrabajo, int n_IdAnoTrabajo, int n_IdLibro, int n_IdTipoCompra)
        {
            DataTable dtLis = new DataTable();
            bool b_result = false;
            int n_row = 0;
            int n_idreg = 0;
            CD_log_compras miFun = new CD_log_compras();
            CD_con_diario o_Conta = new CD_con_diario();
            CN_con_diario funCon = new CN_con_diario();

            // hacer seguimientyo para ver si esta generando bien 
            miFun.mysConec = mysConec;
            o_Conta.mysConec = mysConec;
            b_result = o_Conta.EliminarLibroMes(n_IdLibro, n_IdAnoTrabajo, n_IdMesTrabajo, n_IdEmpresa);
            if (b_result == true)
            {
                if (miFun.Listar(n_IdEmpresa, n_IdMesTrabajo, n_IdAnoTrabajo, n_IdTipoCompra) == false)
                {
                    b_OcurrioError = miFun.b_ocurrioError;
                    c_ErrorMensaje = miFun.c_ErrorMensaje;
                    n_ErrorNumber = miFun.n_ErrorNumber;
                    return b_result;
                }
                dtLis = miFun.dtLista;
                if (n_IdLibro == 8) { dtLis = fundatos.DataTableFiltrar(dtLis, "(n_idtipdoc IN(2,4,5,6,11,13,15,16,17,21,38))"); }
                if (n_IdLibro == 32) { dtLis = fundatos.DataTableFiltrar(dtLis, "(n_idtipdoc = 3)"); }

                dtLis = fundatos.DataTableOrdenar(dtLis, "c_numreg");

                bool b_newasi = false;
                for (n_row = 0; n_row <= dtLis.Rows.Count - 1; n_row++)
                {
                    b_newasi = false;
                    string c_NumAsi = dtLis.Rows[n_row]["c_numreg"].ToString();
                    if (c_NumAsi == "")
                    {
                        b_newasi = true;
                    }
                    n_idreg = Convert.ToInt32(dtLis.Rows[n_row]["n_id"].ToString());
                    funCon.mysConec = mysConec;
                    funCon.STU_SISTEMA = STU_SISTEMA;

                    if (n_IdLibro == 8) 
                    { 
                        funCon.GenerarAsientoCompras(STU_SISTEMA.EMPRESAID, n_idreg, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO, 8, c_NumAsi);
                    }
                    if (n_IdLibro == 32)
                    {
                        funCon.GenerarAsientoCompras(STU_SISTEMA.EMPRESAID, n_idreg, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO, 32, c_NumAsi);
                    }

                    if (b_newasi == true)
                    {
                        c_NumAsi = funCon.c_NewNumAsiento;
                        miFun.AgregarNumAsi(n_idreg, c_NumAsi);
                    }
                }
            }
            b_result = true;
            return b_result;
        }
        public bool Consulta2(int n_IdEmpresa, string c_FechaInicio, string c_FchFinal, int n_IdMoneda, int n_TipoFecha, int n_TipoSaldo, int n_IdLibro, int n_TipoConsulta)
        {
            bool b_result = false;
            CD_log_compras miFun = new CD_log_compras();
            miFun.mysConec = mysConec;

            if (miFun.Consulta2(n_IdEmpresa, c_FechaInicio, c_FchFinal, n_IdMoneda, n_TipoFecha, n_TipoSaldo, n_IdLibro, n_TipoConsulta) == false)
            {
                b_OcurrioError = miFun.b_ocurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_result;
            }
            dtLista = miFun.dtLista;
            b_result = true;
            return b_result;
        }
        public bool Consulta3(int n_IdEmpresa, int n_IdProveedor)
        {
            bool b_result = false;
            CD_log_compras miFun = new CD_log_compras();
            miFun.mysConec = mysConec;

            if (miFun.Consulta3(n_IdEmpresa, n_IdProveedor) == false)
            {
                b_OcurrioError = miFun.b_ocurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_result;
            }
            dtLista = miFun.dtLista;
            b_result = true;
            return b_result;
        }
        public DataTable BuscarDocumentoDetraccion(int n_IdEmpresa, int n_IdProveedor)
        {
            string[,] arrCabeceraDg1 = new string[5, 4];
            DataTable dtResult = new DataTable();
            string c_CampoBuscar = "n_id";
            string c_CadenaFiltro = "";

            Helper.Genericas funDatos = new Helper.Genericas();

            Consulta3(n_IdEmpresa, n_IdProveedor);
            dtResult = dtLista;

            arrCabeceraDg1[0, 0] = "Tipo Doc.";
            arrCabeceraDg1[0, 1] = "50";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "c_codsun";

            arrCabeceraDg1[1, 0] = "Nº Documento";
            arrCabeceraDg1[1, 1] = "140";
            arrCabeceraDg1[1, 2] = "C";
            arrCabeceraDg1[1, 3] = "c_numdoc";

            arrCabeceraDg1[2, 0] = "Fecha Documento";
            arrCabeceraDg1[2, 1] = "90";
            arrCabeceraDg1[2, 2] = "F";
            arrCabeceraDg1[2, 3] = "d_fchdoc";

            arrCabeceraDg1[3, 0] = "Importe";
            arrCabeceraDg1[3, 1] = "80";
            arrCabeceraDg1[3, 2] = "D";
            arrCabeceraDg1[3, 3] = "n_imptotcom";

            arrCabeceraDg1[4, 0] = "Id";
            arrCabeceraDg1[4, 1] = "0";
            arrCabeceraDg1[4, 2] = "N";
            arrCabeceraDg1[4, 3] = "n_id";

            Helper.Genericas xFun = new Helper.Genericas();
            xFun.Buscar_CampoBusqueda = c_CampoBuscar;
            xFun.Buscar_CadFiltro = c_CadenaFiltro;
            dtResult = xFun.Buscar(arrCabeceraDg1, dtResult);

            return dtResult;
        }
        public DataTable DocumentosPercepcion(int n_IdEmpresa, int n_IdProveedor)
        {
            DataTable dtResul = new DataTable();

            CD_log_compras miFun = new CD_log_compras();
            miFun.mysConec = mysConec;

            dtResul = miFun.DocumentosPercepcion(n_IdEmpresa, n_IdProveedor);

            if (dtResul == null)
            {
                b_OcurrioError = miFun.b_ocurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return dtResul;
        }
        public DataTable DocumentosConSaldo(int n_IdEmpresa, int n_IdCliente)
        {
            DataTable dtResult = new DataTable();
            CD_log_compras objCom = new CD_log_compras();
            string[,] arrCabeceraFlexFil = new string[12, 5];

            objCom.mysConec = mysConec;
            dtResult = objCom.Consulta5(n_IdEmpresa, n_IdCliente);

            // FLEX GRID DE LOS TAREAS
            
            arrCabeceraFlexFil[0, 0] = "Proveedor";
            arrCabeceraFlexFil[0, 1] = "300";
            arrCabeceraFlexFil[0, 2] = "C";
            arrCabeceraFlexFil[0, 3] = "";
            arrCabeceraFlexFil[0, 4] = "c_nombre";

            arrCabeceraFlexFil[1, 0] = "Nº Documento";
            arrCabeceraFlexFil[1, 1] = "110";
            arrCabeceraFlexFil[1, 2] = "C";
            arrCabeceraFlexFil[1, 3] = "";
            arrCabeceraFlexFil[1, 4] = "c_numdoc";

            arrCabeceraFlexFil[2, 0] = "T.D.";
            arrCabeceraFlexFil[2, 1] = "40";
            arrCabeceraFlexFil[2, 2] = "C";
            arrCabeceraFlexFil[2, 3] = "";
            arrCabeceraFlexFil[2, 4] = "c_destipdoc";

            arrCabeceraFlexFil[3, 0] = "Dia";
            arrCabeceraFlexFil[3, 1] = "30";
            arrCabeceraFlexFil[3, 2] = "C";
            arrCabeceraFlexFil[3, 3] = "";
            arrCabeceraFlexFil[3, 4] = "n_dia";

            arrCabeceraFlexFil[4, 0] = "Mes";
            arrCabeceraFlexFil[4, 1] = "30";
            arrCabeceraFlexFil[4, 2] = "C";
            arrCabeceraFlexFil[4, 3] = "";
            arrCabeceraFlexFil[4, 4] = "n_mes";

            arrCabeceraFlexFil[5, 0] = "Año";
            arrCabeceraFlexFil[5, 1] = "40";
            arrCabeceraFlexFil[5, 2] = "C";
            arrCabeceraFlexFil[5, 3] = "";
            arrCabeceraFlexFil[5, 4] = "n_ano";

            arrCabeceraFlexFil[6, 0] = "Condicion de Pago";
            arrCabeceraFlexFil[6, 1] = "100";
            arrCabeceraFlexFil[6, 2] = "C";
            arrCabeceraFlexFil[6, 3] = "";
            arrCabeceraFlexFil[6, 4] = "c_desconpag";

            arrCabeceraFlexFil[7, 0] = "Moneda";
            arrCabeceraFlexFil[7, 1] = "40";
            arrCabeceraFlexFil[7, 2] = "C";
            arrCabeceraFlexFil[7, 3] = "";
            arrCabeceraFlexFil[7, 4] = "c_desmon";

            arrCabeceraFlexFil[8, 0] = "Importe";
            arrCabeceraFlexFil[8, 1] = "80";
            arrCabeceraFlexFil[8, 2] = "D";
            arrCabeceraFlexFil[8, 3] = "0.00";
            arrCabeceraFlexFil[8, 4] = "n_importe";

            arrCabeceraFlexFil[9, 0] = "Saldo";
            arrCabeceraFlexFil[9, 1] = "80";
            arrCabeceraFlexFil[9, 2] = "D";
            arrCabeceraFlexFil[9, 3] = "0.00";
            arrCabeceraFlexFil[9, 4] = "n_saldo";

            arrCabeceraFlexFil[10, 0] = "Sel";
            arrCabeceraFlexFil[10, 1] = "40";
            arrCabeceraFlexFil[10, 2] = "B";
            arrCabeceraFlexFil[10, 3] = "";
            arrCabeceraFlexFil[10, 4] = "n_sel";

            arrCabeceraFlexFil[11, 0] = "Id";
            arrCabeceraFlexFil[11, 1] = "0";
            arrCabeceraFlexFil[11, 2] = "N";
            arrCabeceraFlexFil[11, 3] = "";
            arrCabeceraFlexFil[11, 4] = "n_id";

            fundatos.Filtrar_CampoOrden = "c_numdoc";
            fundatos.Filtrar_Titulo = "Documentos Pendientes de Pago";
            fundatos.Filtrar_ColumnaCheck = 11;
            fundatos.Filtrar_ColumnaBusqueda = 12;
            fundatos.Filtrar_CampoBusqueda = "n_id";
            fundatos.Filtrar_AplicarFiltro = true;
            dtResult = fundatos.Filtrar(arrCabeceraFlexFil, dtResult);

            return dtResult;
        }
        public bool TieneCtaContable(string c_CadenaIn, int n_IdEmpresa)
        {
            bool b_result = false;
            DataTable dtResult = new DataTable();
            int n_fil = 0;
            string c_dato = "";
            CD_log_compras miFun = new CD_log_compras();
            miFun.mysConec = mysConec;

            miFun.TieneCtaContable(c_CadenaIn, n_IdEmpresa);
            dtResult = miFun.dtLista;

            if (dtResult.Rows.Count == 0)
            {
                MessageBox.Show("¡ los item registrados no tienen cuenta contable asignada, asignele una cuenta contable en el menu contabilidsad opcion cuenta contable a items  !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                b_OcurrioError = miFun.b_ocurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_result;
            }
            else
            {
                //int n_fil2 = 0;
                //bool b_econtro = false;

                //string[] c_dat = c_CadenaIn.Split(',');

                //for (n_fil2 = 0; n_fil2 <= c_dat.Length - 1; n_fil2++)
                //{
                //    b_econtro = false;
                //    for (n_fil = 0; n_fil <= dtResult.Rows.Count - 1; n_fil++)
                //    {
                //        if (c_dat[n_fil2].ToString() == Convert.ToString(xFun.NulosC(dtResult.Rows[n_fil]["n_idite"]).ToString()))
                //        {
                //            c_dato = Convert.ToString(xFun.NulosC(dtResult.Rows[n_fil]["n_idpc"]));
                //            if ((c_dato == "") || (c_dato == "0"))                                                    // PREGUNTAMOS SI EL ITEM TIENE CUENTA CONTABLE ASIGHNADA
                //            {
                //                c_dato = Convert.ToString(xFun.NulosC(dtResult.Rows[n_fil]["c_despro"]));
                //                MessageBox.Show("¡ El item:" + c_dato + " no tiene una cuenta contable asignada, asignele una cuenta en el menu contabilidad, opcion cuenta contable a items  !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //                return b_result;
                //            }
                //            b_econtro = true;
                //            break;
                //        }

                //    }
                //    if (b_econtro == false)
                //    {
                //        c_dato = c_dat[n_fil2].ToString();
                //        //c_dato = fundatos.DataTableBuscar(dt)
                //        MessageBox.Show("¡ El item:" + c_dato + " no tiene una cuenta contable asignada, asignele una cuenta en el menu contabilidad, opcion cuenta contable a items  !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //        return b_result;
                //    }



                    //for (n_fil = 0; n_fil <= dtResult.Rows.Count - 1; n_fil++)
                    //{
                    //    if (c_dat[n_fil2].ToString() == Convert.ToString(xFun.NulosC(dtResult.Rows[n_fil]["n_idite"]).ToString()))
                    //    {
                    //        c_dato = Convert.ToString(xFun.NulosC(dtResult.Rows[n_fil]["n_idpc"]));
                    //        if ((c_dato == "") || (c_dato == "0"))                                                    // PREGUNTAMOS SI EL ITEM TIENE CUENTA CONTABLE ASIGHNADA
                    //        {
                    //            c_dato = Convert.ToString(xFun.NulosC(dtResult.Rows[n_fil]["c_despro"]));
                    //            MessageBox.Show("¡ El item:" + c_dato + " no tiene una cuenta contable asignada, asignele una cuenta en el menu contabilidad, opcion cuenta contable a items  !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    //            return b_result;
                    //        }
                    //        else
                    //        {
                    //            c_dato = Convert.ToString(xFun.NulosN(dtResult.Rows[n_fil]["n_ctadesdeb"]));
                    //            if (c_dato == "")                                                   // PREGUNTAMOS SI EL ITEM TIENE CUENTA CONTABLE DE DESTINO EN EL DEBE
                    //            {
                    //                c_dato = Convert.ToString(xFun.NulosC(dtResult.Rows[n_fil]["c_cuecon"]));
                    //                MessageBox.Show("¡ la cuenta contable Nº" + c_dato + " no tiene una cuenta destino debe asignada, asignele una cuenta destino en el menu contabilidad opcion plan de cuentas  !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    //            }

                    //            c_dato = Convert.ToString(xFun.NulosN(dtResult.Rows[n_fil]["n_ctadeshab"]));
                    //            if (c_dato == "")                                                   // PREGUNTAMOS SI EL ITEM TIENE CUENTA CONTABLE DE DESTINO EN EL HABER
                    //            {
                    //                c_dato = Convert.ToString(xFun.NulosC(dtResult.Rows[n_fil]["c_cuecon"]));
                    //                MessageBox.Show("¡ la cuenta contable Nº" + c_dato + " no tiene una cuenta destino haber asignada, asignele una cuenta destino en el menu contabilidad opcion plan de cuentas  !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    //            }
                    //        }
                    //    }
                    //}
                //}
            }

            b_result = true;
            return b_result;
        }
        public bool ImportarApertura(int n_IdEmpresa, int n_AnoTrabajo, int n_IdLibro)
        {
            bool b_res = false;
            DataTable dtResult = new DataTable();
            DataTable dtResult2 = new DataTable();
            CD_log_compras objCom = new CD_log_compras();
            string[,] arrCabeceraFlexFil = new string[12, 5];

            objCom.mysConec = mysConec;
            dtResult = objCom.Consulta8(n_IdEmpresa, n_AnoTrabajo, n_IdLibro);
            dtResult2 = objCom.Consulta9(n_IdEmpresa, n_AnoTrabajo+1, n_IdLibro);
            string c_cadin = fundatos.DataTableCadenaIN(dtResult2, "n_id");
            if (c_cadin != "")
            { 
                dtResult = fundatos.DataTableFiltrar(dtResult, "n_id NOT IN ("+ c_cadin +")");
            }

            arrCabeceraFlexFil[0, 0] = "Proveedor";
            arrCabeceraFlexFil[0, 1] = "300";
            arrCabeceraFlexFil[0, 2] = "C";
            arrCabeceraFlexFil[0, 3] = "";
            arrCabeceraFlexFil[0, 4] = "c_nombre";

            arrCabeceraFlexFil[1, 0] = "Nº Documento";
            arrCabeceraFlexFil[1, 1] = "110";
            arrCabeceraFlexFil[1, 2] = "C";
            arrCabeceraFlexFil[1, 3] = "";
            arrCabeceraFlexFil[1, 4] = "c_numdoc";

            arrCabeceraFlexFil[2, 0] = "T.D.";
            arrCabeceraFlexFil[2, 1] = "40";
            arrCabeceraFlexFil[2, 2] = "C";
            arrCabeceraFlexFil[2, 3] = "";
            arrCabeceraFlexFil[2, 4] = "c_destipdoc";

            arrCabeceraFlexFil[3, 0] = "Dia";
            arrCabeceraFlexFil[3, 1] = "30";
            arrCabeceraFlexFil[3, 2] = "C";
            arrCabeceraFlexFil[3, 3] = "";
            arrCabeceraFlexFil[3, 4] = "n_dia";

            arrCabeceraFlexFil[4, 0] = "Mes";
            arrCabeceraFlexFil[4, 1] = "30";
            arrCabeceraFlexFil[4, 2] = "C";
            arrCabeceraFlexFil[4, 3] = "";
            arrCabeceraFlexFil[4, 4] = "n_mes";

            arrCabeceraFlexFil[5, 0] = "Año";
            arrCabeceraFlexFil[5, 1] = "40";
            arrCabeceraFlexFil[5, 2] = "C";
            arrCabeceraFlexFil[5, 3] = "";
            arrCabeceraFlexFil[5, 4] = "n_ano";

            arrCabeceraFlexFil[6, 0] = "Condicion de Pago";
            arrCabeceraFlexFil[6, 1] = "100";
            arrCabeceraFlexFil[6, 2] = "C";
            arrCabeceraFlexFil[6, 3] = "";
            arrCabeceraFlexFil[6, 4] = "c_desconpag";

            arrCabeceraFlexFil[7, 0] = "Moneda";
            arrCabeceraFlexFil[7, 1] = "40";
            arrCabeceraFlexFil[7, 2] = "C";
            arrCabeceraFlexFil[7, 3] = "";
            arrCabeceraFlexFil[7, 4] = "c_desmon";

            arrCabeceraFlexFil[8, 0] = "Importe";
            arrCabeceraFlexFil[8, 1] = "80";
            arrCabeceraFlexFil[8, 2] = "D";
            arrCabeceraFlexFil[8, 3] = "0.00";
            arrCabeceraFlexFil[8, 4] = "n_importe";

            arrCabeceraFlexFil[9, 0] = "Saldo";
            arrCabeceraFlexFil[9, 1] = "80";
            arrCabeceraFlexFil[9, 2] = "D";
            arrCabeceraFlexFil[9, 3] = "0.00";
            arrCabeceraFlexFil[9, 4] = "n_saldo";

            arrCabeceraFlexFil[10, 0] = "Sel";
            arrCabeceraFlexFil[10, 1] = "40";
            arrCabeceraFlexFil[10, 2] = "B";
            arrCabeceraFlexFil[10, 3] = "";
            arrCabeceraFlexFil[10, 4] = "n_sel";

            arrCabeceraFlexFil[11, 0] = "Id";
            arrCabeceraFlexFil[11, 1] = "0";
            arrCabeceraFlexFil[11, 2] = "N";
            arrCabeceraFlexFil[11, 3] = "";
            arrCabeceraFlexFil[11, 4] = "n_id";

            fundatos.Filtrar_CampoOrden = "c_nombre, c_numdoc";
            fundatos.Filtrar_Titulo = "Documentos Pendientes de Pago";
            fundatos.Filtrar_ColumnaCheck = 11;
            fundatos.Filtrar_ColumnaBusqueda = 12;
            fundatos.Filtrar_CampoBusqueda = "n_id";
            fundatos.Filtrar_AplicarFiltro = true;
            dtResult = fundatos.Filtrar(arrCabeceraFlexFil, dtResult);
            if (dtResult != null)
            {
                if (dtResult.Rows.Count != 0)
                {
                    string c_CadIN = fundatos.DataTableCadenaIN(dtResult,"n_id");
                    if (objCom.ImportarApertura(n_IdEmpresa, n_AnoTrabajo, n_IdLibro, c_CadIN) == false)
                    {
                        b_OcurrioError = objCom.b_ocurrioError;
                        c_ErrorMensaje = objCom.c_ErrorMensaje;
                        n_ErrorNumber = objCom.n_ErrorNumber;
                        return b_res;
                    }
                    b_res = true;
                }
            }

            return b_res;
        }
        public bool Consulta10(int n_IdEmpresa, int n_AnoTrabajo, int n_tipo)
        {
            bool b_result = false;
            CD_log_compras miFun = new CD_log_compras();
            miFun.mysConec = mysConec;
            miFun.Consulta10(n_IdEmpresa, n_AnoTrabajo, n_tipo);
            if (miFun.b_ocurrioError == true)
            {
                b_OcurrioError = miFun.b_ocurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_result;
            }
            dtLista = miFun.dtLista;
            b_result = true;
            return b_result;
        }
        public void Consulta14(int n_Idempresa, int n_TipoMoneda, int n_TipoImporte)
        {
            CD_log_compras miFun = new CD_log_compras();
            miFun.mysConec = mysConec;

            miFun.Consulta14(n_Idempresa, n_TipoMoneda, n_TipoImporte);

            if (miFun.b_ocurrioError == true)
            {
                dtLista = null;
                b_OcurrioError = miFun.b_ocurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return;
            }
            dtLista = miFun.dtLista;
            return;
        }
        public void Consulta17(int n_Idempresa, int n_AnoTrabajo, int n_TipoMoneda, int n_TipoImporte)
        {
            CD_log_compras miFun = new CD_log_compras();
            miFun.mysConec = mysConec;

            miFun.Consulta17(n_Idempresa, n_AnoTrabajo, n_TipoMoneda, n_TipoImporte);

            if (miFun.b_ocurrioError == true)
            {
                dtLista = null;
                b_OcurrioError = miFun.b_ocurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return;
            }
            dtLista = miFun.dtLista;
            return;
        }
    }
}
