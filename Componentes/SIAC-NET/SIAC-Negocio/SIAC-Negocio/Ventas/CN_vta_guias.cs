using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades.Ventas;
using SIAC_DATOS.Ventas;
using SIAC_DATOS.Logistica;
using MySql.Data.MySqlClient;
using SIAC_Objetos.Ventas;
using SIAC_Entidades.Sistema;
using SIAC_Negocio.Sistema;
using Helper;

namespace SIAC_Negocio.Ventas
{
    public class CN_vta_guias
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public DataTable dtLista = new DataTable();
        public MySqlConnection mysConec = new MySqlConnection();
        public List<BE_VTA_GUIASDET> LstDetalle = new List<BE_VTA_GUIASDET>();
        public List<BE_VTA_GUIASDOC> LstGuiasDoc = new List<BE_VTA_GUIASDOC>();
        public BE_VTA_GUIASDATOS e_GuiaDatos = new BE_VTA_GUIASDATOS();

        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public double n_IdGenerado = 0;
        Genericas funDatos = new Genericas();

        public DataTable Listar(int n_IdEmp, int n_idmes, int n_anotra, int n_TipoOrigen)
        {
            DataTable dtResul = new DataTable();

            CD_vta_guias miFun = new CD_vta_guias();
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar(n_IdEmp, n_idmes, n_anotra, n_TipoOrigen);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public BE_VTA_GUIAS TraerRegistro(int n_IdRegistro)
        {
            int n_row =0;
            BE_VTA_GUIAS entGuias = new BE_VTA_GUIAS();
            DataTable dtResult = new DataTable();
            DataTable dtDatos = new DataTable();
            CD_vta_guias miFun = new CD_vta_guias();
            Helper.Comunes.Funciones ofun = new Helper.Comunes.Funciones();

            miFun.mysConec = mysConec;
            entGuias = miFun.TraerRegistro(n_IdRegistro);
            LstDetalle = miFun.LstDetalle;
            dtResult = miFun.dtGuiasDoc;
            dtDatos = miFun.dtGuiasDatos;

            if (miFun.IntErrorNumber == 0)
            {
                for (n_row =0;n_row<= dtResult.Rows.Count-1;n_row++)
                {
                    BE_VTA_GUIASDOC entguidoc = new BE_VTA_GUIASDOC();

                    entguidoc.n_idgui = Convert.ToInt32(dtResult.Rows[n_row]["n_idgui"]);
                    entguidoc.n_idtipdoc = Convert.ToInt32(dtResult.Rows[n_row]["n_idtipdoc"]);
                    entguidoc.c_numdoc = dtResult.Rows[n_row]["c_numdoc"].ToString();
                    entguidoc.n_iddoc = Convert.ToInt32(dtResult.Rows[n_row]["n_iddoc"]);

                    LstGuiasDoc.Add(entguidoc);
                }

                if (dtDatos.Rows.Count != 0)
                {
                    e_GuiaDatos = new BE_VTA_GUIASDATOS();
                    e_GuiaDatos.n_idgui = Convert.ToInt32(ofun.NulosN(dtDatos.Rows[0]["n_idgui"]));
                    e_GuiaDatos.n_idtipdoc = Convert.ToInt32(ofun.NulosN(dtDatos.Rows[0]["n_idtipdoc"]));
                    e_GuiaDatos._c_facnumser = ofun.NulosC(dtDatos.Rows[0]["c_facnumser"]).ToString();
                    e_GuiaDatos.c_facnumdoc = ofun.NulosC(dtDatos.Rows[0]["c_facnumdoc"]).ToString();
                }
                else
                {
                    e_GuiaDatos = null;
                }
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return entGuias;
        }
        public bool Insertar(BE_VTA_GUIAS entGuias, List<BE_VTA_GUIASDOC> lstGuiasDoc, BE_VTA_GUIASDATOS e_GuiaDatos)
        {
            BE_VTA_GUIAS entNuevoGuias = new BE_VTA_GUIAS();
            CD_vta_guias miFun = new CD_vta_guias();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoGuias.n_idemp = entGuias.n_idemp;
            entNuevoGuias.n_id = entGuias.n_id;
            entNuevoGuias.n_idano = entGuias.n_idano;
            entNuevoGuias.n_idmes = entGuias.n_idmes;
            entNuevoGuias.n_idcli = entGuias.n_idcli;
            entNuevoGuias.n_idtipdoc = entGuias.n_idtipdoc;
            entNuevoGuias.c_numser = entGuias.c_numser;
            entNuevoGuias.c_numdoc = entGuias.c_numdoc;
            entNuevoGuias.d_fchdoc = entGuias.d_fchdoc;
            entNuevoGuias.n_idemptra = entGuias.n_idemptra;
            entNuevoGuias.n_idmottra = entGuias.n_idmottra;
            entNuevoGuias.n_idtipdocref = entGuias.n_idtipdocref;
            entNuevoGuias.n_iddocref = entGuias.n_iddocref;
            entNuevoGuias.c_numordcom = entGuias.c_numordcom;
            entNuevoGuias.d_fchpeddocref = entGuias.d_fchpeddocref;
            entNuevoGuias.d_fchentdocref = entGuias.d_fchentdocref;
            entNuevoGuias.n_idpunvencli = entGuias.n_idpunvencli;
            entNuevoGuias.c_dirpunlle = entGuias.c_dirpunlle;
            entNuevoGuias.c_dirpunpar = entGuias.c_dirpunpar;
            entNuevoGuias.n_idemptra = entGuias.n_idemptra;
            entNuevoGuias.n_idcho = entGuias.n_idcho;
            entNuevoGuias.n_idvehtra = entGuias.n_idvehtra;
            entNuevoGuias.n_anulado = entGuias.n_anulado;
            entNuevoGuias.n_tipgui = entGuias.n_tipgui;
            entNuevoGuias.c_numdocref = entGuias.c_numdocref;
            entNuevoGuias.n_idpunpar = entGuias.n_idpunpar;
            entNuevoGuias.n_idpunlle = entGuias.n_idpunlle;
            entNuevoGuias.n_tipori = entGuias.n_tipori;
            entNuevoGuias.n_idclides = entGuias.n_idclides;
            entNuevoGuias.n_aplotrpro = entGuias.n_aplotrpro;

            miFun.LstDetalle = LstDetalle;
            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            booOk = miFun.Insertar(entNuevoGuias, lstGuiasDoc, e_GuiaDatos);

            if (booOk == true)
            {
                if (entGuias.n_idtipdocref == 3)
                { 
                    n_IdGenerado = miFun.n_IdGenerado;
                    CD_log_ordenrequerimiento objReq = new CD_log_ordenrequerimiento();
                    objReq.mysConec = mysConec;

                    int n_row = 0;
                    int n_iddocref = 0;

                    for (n_row = 0; n_row <= lstGuiasDoc.Count - 1; n_row++)
                    {
                        n_iddocref = lstGuiasDoc[n_row].n_iddoc;
                        booOk = objReq.ActualizarEstadoRequerimiento(n_iddocref, 3);             // ACTUALIZAMOS LA ORDEN DE REQUERIMIENTO A 3 QUE INDICA EL ESTADO DE PROCESADO
                        if (booOk == false)
                        {
                            booOcurrioError = miFun.booOcurrioError;
                            StrErrorMensaje = miFun.StrErrorMensaje;
                            IntErrorNumber = miFun.IntErrorNumber;

                            break;
                        }
                    }
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
        public bool Actualizar(BE_VTA_GUIAS entGuias, List<BE_VTA_GUIASDOC> lstGuiasDoc, BE_VTA_GUIASDATOS e_GuiaDatos)
        {
            BE_VTA_GUIAS entNuevoGuias = new BE_VTA_GUIAS();
            CD_vta_guias miFun = new CD_vta_guias();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoGuias.n_idemp = entGuias.n_idemp;
            entNuevoGuias.n_id = entGuias.n_id;
            entNuevoGuias.n_idano = entGuias.n_idano;
            entNuevoGuias.n_idmes = entGuias.n_idmes;
            entNuevoGuias.n_idcli = entGuias.n_idcli;
            entNuevoGuias.n_idtipdoc = entGuias.n_idtipdoc;
            entNuevoGuias.c_numser = entGuias.c_numser;
            entNuevoGuias.c_numdoc = entGuias.c_numdoc;
            entNuevoGuias.d_fchdoc = entGuias.d_fchdoc;
            entNuevoGuias.n_idemptra = entGuias.n_idemptra;
            entNuevoGuias.n_idmottra = entGuias.n_idmottra;
            entNuevoGuias.c_numordcom = entGuias.c_numordcom;
            entNuevoGuias.n_idtipdocref = entGuias.n_idtipdocref;
            entNuevoGuias.n_iddocref = entGuias.n_iddocref;
            entNuevoGuias.c_numdocref = entGuias.c_numdocref;
            entNuevoGuias.d_fchpeddocref = entGuias.d_fchpeddocref;
            entNuevoGuias.d_fchentdocref = entGuias.d_fchentdocref;
            entNuevoGuias.n_idpunvencli = entGuias.n_idpunvencli;
            entNuevoGuias.c_dirpunlle = entGuias.c_dirpunlle;
            entNuevoGuias.c_dirpunpar = entGuias.c_dirpunpar;
            entNuevoGuias.n_idemptra = entGuias.n_idemptra;
            entNuevoGuias.n_idcho = entGuias.n_idcho;
            entNuevoGuias.n_idvehtra = entGuias.n_idvehtra;
            entNuevoGuias.n_anulado = entGuias.n_anulado;
            entNuevoGuias.n_idpunpar = entGuias.n_idpunpar;
            entNuevoGuias.n_idpunlle = entGuias.n_idpunlle;
            entNuevoGuias.n_tipori = entGuias.n_tipori;
            entNuevoGuias.n_idclides = entGuias.n_idclides;
            entNuevoGuias.n_aplotrpro = entGuias.n_aplotrpro;
            entNuevoGuias.n_tipgui = entGuias.n_tipgui;

            miFun.LstDetalle = LstDetalle;
            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            booOk = miFun.Actualizar(entNuevoGuias, lstGuiasDoc, e_GuiaDatos);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Eliminar(int n_Id, int n_TipoOrigen)
        {
            DataTable dtResul = new DataTable();
            CD_vta_guias miFun = new CD_vta_guias();

            miFun.mysConec = mysConec;
            miFun.TraerRegistro(n_Id);
            dtResul = miFun.dtGuiasDoc;

            bool booOk = false;

            miFun.mysConec = mysConec;
            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            if (miFun.Eliminar(n_Id, n_TipoOrigen) == true)
            {
                CD_log_ordenrequerimiento objReq = new CD_log_ordenrequerimiento();
                objReq.mysConec = mysConec;

                int n_row = 0;
                int n_iddocref = 0;

                for (n_row = 0; n_row <= dtResul.Rows.Count - 1; n_row++)
                {
                    n_iddocref = Convert.ToInt32(dtResul.Rows[n_row]["n_iddoc"]);
                    booOk = objReq.ActualizarEstadoRequerimiento(n_iddocref, 1);             // ACTUALIZAMOS LA ORDEN DE REQUERIMIENTO A 1 QUE INDICA EL ESTADO DE PENDIENTE
                    if (booOk == false)
                    {
                        booOcurrioError = miFun.booOcurrioError;
                        StrErrorMensaje = miFun.StrErrorMensaje;
                        IntErrorNumber = miFun.IntErrorNumber;
                    }
                    else
                    {
                        break;
                    }
                }
                booOk = true;
            }
            else 
            { 
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return booOk;
        }
        public bool CambiarEstado(int n_IdGuia, int n_IdEstado)
        {
            DataTable dtResul = new DataTable();
            CD_vta_guias miFun = new CD_vta_guias();

            miFun.mysConec = mysConec;
            miFun.TraerRegistro(n_IdGuia);
            dtResul = miFun.dtGuiasDoc;

            bool booOk = false;

            miFun.mysConec = mysConec;

            if (miFun.CambiarEstado(n_IdGuia, n_IdEstado) == true)
            {
                if (n_IdEstado == 1)
                {
                    booOk = true;
                }
                else
                { 
                    CD_log_ordenrequerimiento objReq = new CD_log_ordenrequerimiento();
                    objReq.mysConec = mysConec;

                    int n_row = 0;
                    int n_iddocref = 0;

                    for (n_row = 0; n_row <= dtResul.Rows.Count - 1; n_row++)
                    {
                        n_iddocref = Convert.ToInt32(dtResul.Rows[n_row]["n_iddoc"]);
                        booOk = objReq.ActualizarEstadoRequerimiento(n_iddocref, 1);             // ACTUALIZAMOS LA ORDEN DE REQUERIMIENTO A 1 QUE INDICA EL ESTADO DE PENDIENTE
                        if (booOk == false)
                        {
                            booOcurrioError = miFun.booOcurrioError;
                            StrErrorMensaje = miFun.StrErrorMensaje;
                            IntErrorNumber = miFun.IntErrorNumber;
                        }
                        else
                        {
                            break;
                        }
                    }
                    booOk = true;
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
        public void ReportImprimirGuia(int n_idguia)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[1, 3];

            arrPara[0, 0] = "n_id";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = n_idguia.ToString();

            BE_SYS_EMPRESA entEmp = new BE_SYS_EMPRESA();
            DataTable dtresut = new DataTable();
            CN_sys_empresa funsys = new CN_sys_empresa();
            funsys.mysConec = mysConec;
            funsys.TraerRegistro(STU_SISTEMA.EMPRESAID);
            entEmp = funsys.e_Empresa;
            dtresut = funsys.dtFolderDocImpresion;
            dtresut = funDatos.DataTableFiltrar(dtresut, "n_idtipdoc = 10");

            if (dtresut.Rows.Count != 0)
            {
                c_NomArchivo = dtresut.Rows[0]["c_nomfil"].ToString();
            }

            if (c_NomArchivo == "")
            {
                booOcurrioError = true;
                StrErrorMensaje = "No se ha archivo de impresion para este documento, asigne un archivo de impresion en elsetup del sistema";
                IntErrorNumber = 1;
                return;
            }

            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "ventas\\" + entEmp.c_folderimp +"\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "VENTAS - IMPRESION DE GUIAS";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
        public void ReportGuiasMes(int n_idemp, int n_idano, int n_idmes)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[5, 3];

            arrPara[0, 0] = "n_idemp";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = n_idemp.ToString();

            arrPara[1, 0] = "n_idano";
            arrPara[1, 1] = "N";
            arrPara[1, 2] = n_idano.ToString();

            arrPara[2, 0] = "n_idmes";
            arrPara[2, 1] = "N";
            arrPara[2, 2] = n_idmes.ToString();

            arrPara[3, 0] = "c_titulo1";
            arrPara[3, 1] = "C";
            arrPara[3, 2] = "REPORTE DE GUIAS EMITIDAS";

            arrPara[4, 0] = "c_titulo2";
            arrPara[4, 1] = "C";
            arrPara[4, 2] = "POR MESES";

            c_NomArchivo = "RptGuiasMes.rpt";
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "ventas\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "VENTAS - REPORTE DE GUIAS";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
        public void GuiasTranportistasPendiente(int n_IdEmpresa, int n_TipoMovimiento, int n_TipoOrigen, int n_AnoTrabajo)
        {
            DataTable dtResul = new DataTable();

            CD_vta_guias miFun = new CD_vta_guias();
            miFun.mysConec = mysConec;

            miFun.GuiasTranportistasPendiente(n_IdEmpresa, n_TipoMovimiento, n_TipoOrigen, n_AnoTrabajo);
            dtLista =  miFun.dtLista;
            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return;
        }
        public void Consulta1(int n_IdEmpresa, int n_IdCliente)
        {
            CD_vta_guias miFun = new CD_vta_guias();
            miFun.mysConec = mysConec;

            miFun.Consulta1(n_IdEmpresa, n_IdCliente);
            dtLista = miFun.dtLista;

            if (miFun.IntErrorNumber != null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return;
        }
        public void Consulta2(string c_CadenaIN)
        {
            CD_vta_guias miFun = new CD_vta_guias();
            miFun.mysConec = mysConec;

            miFun.Consulta2(c_CadenaIN);
            dtLista = miFun.dtLista;

            if (miFun.IntErrorNumber != null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return;
        }
        public DataTable BuscarGuias(int n_IdEmpresa, int n_IdCliente, string c_DocumentosCargados)
        {
            Helper.Genericas funDatos = new Helper.Genericas();
            string[,] arrCabeceraDg1 = new string[5, 4];
            DataTable dtResult = new DataTable();

            Consulta1(n_IdEmpresa, n_IdCliente);
            dtResult = dtLista;
            if (n_IdCliente != 0)
            {
                dtResult = funDatos.DataTableFiltrar(dtResult, "n_idcli = " + n_IdCliente.ToString() + "");  // FILTRAMOS LOS DOCUMENTOS DEL PROVEEDOR
            }
            if (c_DocumentosCargados != "")
            {
                dtResult = funDatos.DataTableFiltrar(dtResult, "n_id NOT IN (" + c_DocumentosCargados + ")");         // QUITAMOS LOS DOCUMENTOS QUE YA FUERON AGREGADOS
            }

            arrCabeceraDg1[0, 0] = "Cliente";
            arrCabeceraDg1[0, 1] = "200";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "c_nombre";

            arrCabeceraDg1[1, 0] = "Tip. Doc.";
            arrCabeceraDg1[1, 1] = "40";
            arrCabeceraDg1[1, 2] = "C";
            arrCabeceraDg1[1, 3] = "c_abr";

            arrCabeceraDg1[2, 0] = "Fch. Documento";
            arrCabeceraDg1[2, 1] = "80";
            arrCabeceraDg1[2, 2] = "F";
            arrCabeceraDg1[2, 3] = "d_fchdoc";

            arrCabeceraDg1[3, 0] = "Nº Documento";
            arrCabeceraDg1[3, 1] = "120";
            arrCabeceraDg1[3, 2] = "F";
            arrCabeceraDg1[3, 3] = "c_numdoc";

            arrCabeceraDg1[4, 0] = "Id";
            arrCabeceraDg1[4, 1] = "0";
            arrCabeceraDg1[4, 2] = "N";
            arrCabeceraDg1[4, 3] = "n_id";

            Genericas xFun = new Genericas();
            xFun.Buscar_CampoBusqueda = "n_id";
            xFun.Buscar_CadFiltro = "";
            dtResult = xFun.Buscar(arrCabeceraDg1, dtResult);

            return dtResult;
        }
        public bool InsertarAnulado(int n_IdEmpresa, int n_Id, int n_IdTipDoc, DateTime d_FchDoc, int n_IdMes, int n_IdAno, string c_NumSer, string c_NumDoc, int n_TipoMovimiento)
        {
            bool booOk = false;
            CD_vta_guias miFun = new CD_vta_guias();

            miFun.mysConec = mysConec;
            if (miFun.InsertarAnulado(n_IdEmpresa, n_Id, n_IdTipDoc, d_FchDoc, n_IdMes, n_IdAno, c_NumSer, c_NumDoc, n_TipoMovimiento) == false)
            { 
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            booOk = true;
            return booOk;
        }
        public bool Consulta10(int n_idempresa, int n_TipoReporte, string c_FechaInicio, string c_FechaTermino, string c_CadenaIN)
        {
            bool b_result = false;
            CD_vta_guias miFun = new CD_vta_guias();
            miFun.mysConec = mysConec;

            if (miFun.Consulta10(n_idempresa, n_TipoReporte, c_FechaInicio, c_FechaTermino, c_CadenaIN) == true)
            {
                dtLista = miFun.dtLista;
                b_result = true;
            }
            else
            { 
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
                return b_result;
            }

            return b_result;
        }
        public DataTable BuscarGuiasPendientesAlmacen(int n_IdEmpresa, int n_TipoMovimiento, int n_TipoOrigen, int n_AnoTrabajo)
        {
            // n_Tipo = 1 BUSCA GUIA DE INGRESO PENDIENTES DE JALAR ALMACEN
            // n_Tipo = 2 BUSCA GUIA DE SALIDA PENDIENTES DE JALAR ALMACEN
            CN_vta_guias objguia = new CN_vta_guias();
            DataTable dtResult = new DataTable();
            string[,] arrCabeceraDg1 = new string[4, 4];

            objguia.mysConec = mysConec;
            objguia.GuiasTranportistasPendiente(n_IdEmpresa, n_TipoMovimiento, n_TipoOrigen, n_AnoTrabajo);
            dtResult = funDatos.DataTableOrdenar(objguia.dtLista,"c_numdoc DESC");

            arrCabeceraDg1[0, 0] = "Nº Guia";
            arrCabeceraDg1[0, 1] = "110";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "c_numdoc";

            arrCabeceraDg1[1, 0] = "Fch. Emision";
            arrCabeceraDg1[1, 1] = "80";
            arrCabeceraDg1[1, 2] = "C";
            arrCabeceraDg1[1, 3] = "d_fchdoc";

            arrCabeceraDg1[2, 0] = "Cliente";
            arrCabeceraDg1[2, 1] = "400";
            arrCabeceraDg1[2, 2] = "C";
            arrCabeceraDg1[2, 3] = "c_nombre";

            arrCabeceraDg1[3, 0] = "Id";
            arrCabeceraDg1[3, 1] = "0";
            arrCabeceraDg1[3, 2] = "N";
            arrCabeceraDg1[3, 3] = "n_id";

            Genericas xFun = new Genericas();
            xFun.Buscar_CampoBusqueda = "n_id";
            xFun.Buscar_CampoOrden = "c_numdoc";
            xFun.Buscar_CadFiltro = "";
            dtResult = xFun.Buscar(arrCabeceraDg1, dtResult);
            return dtResult;
        }
        public void GuiasAnuales(int n_IdEmpresa, int n_Tipo, int n_AnoTrabajo, int n_OrigenGuia, string CadenaIn) 
        {
            CD_vta_guias miFun = new CD_vta_guias();
            miFun.mysConec = mysConec;

            miFun.GuiasAnuales(n_IdEmpresa, n_Tipo, n_AnoTrabajo, n_OrigenGuia, CadenaIn);
            dtLista = miFun.dtLista;

            if(miFun.booOcurrioError == true)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return;
        }
    }
}
