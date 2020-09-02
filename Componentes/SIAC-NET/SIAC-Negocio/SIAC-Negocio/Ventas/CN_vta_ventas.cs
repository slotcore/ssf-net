using Helper;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using SIAC_DATOS.Ventas;
using SIAC_DATOS.Contabilidad;
using SIAC_Entidades.Ventas;
using SIAC_Entidades.Maestros;
using SIAC_Entidades.Sistema;
using SIAC_Entidades.Contabilidad;
using SIAC_Objetos.Ventas;
using SIAC_Negocio.Sistema;
using SIAC_Negocio.Sunat;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIAC_Negocio.Maestros;
using SIAC_Negocio.Contabilidad;

namespace SIAC_Negocio.Ventas
{
    public class CN_vta_ventas
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public bool b_GenerarAsiento = false;

        Genericas funDatos = new Genericas();
        DatosMySql FunMysql = new DatosMySql();

        public List<BE_VTA_VENTASDET> LstDetalle = new List<BE_VTA_VENTASDET>();
        public List<BE_VTA_VENTASDAT> LstDatos = new List<BE_VTA_VENTASDAT>();
        public List<BE_VTA_VENTASOCT> LstDetalleOCT = new List<BE_VTA_VENTASOCT>();
        public List<BE_VTA_VENTASDOC> LstDocumentos = new List<BE_VTA_VENTASDOC>();
        public List<BE_VTA_VENTASNCDOC> LstDocumentosNC = new List<BE_VTA_VENTASNCDOC>();
        public List<BE_CON_DIARIO> l_diario = new List<BE_CON_DIARIO>();

        public DataTable dtLista1 = new DataTable();
        public DataTable dtLista2 = new DataTable();
        DataTable dtLista = new DataTable();

        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public double n_IdGenerado = 0;
        public DataTable Listar(int n_IdEmp, int n_idmes, int n_anotra, int n_IdLibro)
        {
            DataTable dtResul = new DataTable();

            CD_vta_ventas miFun = new CD_vta_ventas();
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar(n_IdEmp, n_idmes, n_anotra, n_IdLibro);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public bool Consulta2(int n_IdEmpresa, string c_FechaInicio, string c_FchFinal, int n_IdMoneda, int n_TipoFecha, int n_TipoSaldo, int n_IdLibro, int n_TipoConsulta, string c_CadINCliente, string c_CadINItem)
        {
            bool b_result = false;
            //CD_log_compras miFun = new CD_log_compras();
            CD_vta_ventas miFun = new CD_vta_ventas();
            miFun.mysConec = mysConec;

            if (miFun.Consulta2(n_IdEmpresa, c_FechaInicio, c_FchFinal, n_IdMoneda, n_TipoFecha, n_TipoSaldo, n_IdLibro, n_TipoConsulta, c_CadINCliente, c_CadINItem) == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
                return b_result;
            }
            dtLista1 = miFun.dtLista1;
            b_result = true;
            return b_result;
        }
        public DataTable Consulta3(int n_IdPuesto)
        {
            DataTable dtResul = new DataTable();

            CD_vta_ventas miFun = new CD_vta_ventas();
            miFun.mysConec = mysConec;

            dtResul = miFun.Consulta3(n_IdPuesto);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        //public DataTable TraerPrecios(int n_IdEmpresa, int n_IdCliente)
        //{
        //    DataTable dtResul = new DataTable();

        //    CD_vta_ventas miFun = new CD_vta_ventas();
        //    miFun.mysConec = mysConec;

        //    dtResul = miFun.TraerPrecios(n_IdEmpresa, n_IdCliente);

        //    if (dtResul == null)
        //    {
        //        booOcurrioError = miFun.booOcurrioError;
        //        StrErrorMensaje = miFun.StrErrorMensaje;
        //        IntErrorNumber = miFun.IntErrorNumber;
        //    }

        //    return dtResul;
        //}     
        public bool EnviarCorreo(int n_IdVenta, bool b_EsfacturaElectronica)
        {
            bool b_result = false;
            CN_sun_tipdoccom funDocCom = new CN_sun_tipdoccom();
            CN_sys_empresa funemp = new CN_sys_empresa();
            Helper.Cls_IO funIO = new Helper.Cls_IO();
            Helper.Correo funcorr = new Helper.Correo();
            CD_vta_ventas FunVen = new CD_vta_ventas();
            CN_mae_clipro objCli = new CN_mae_clipro();
            BE_VTA_VENTAS entVenta = new BE_VTA_VENTAS();
            BE_MAE_CLIPRO entCliente = new BE_MAE_CLIPRO();
            BE_SYS_EMPRESA entEmp = new BE_SYS_EMPRESA();
            DataTable dtResul = new DataTable();

            FunVen.mysConec = mysConec;
            entVenta = FunVen.TraerRegistro(n_IdVenta);
            
            funDocCom.mysConec = mysConec;
            dtResul = funDocCom.Listar();

            string c_tipdoc = funDatos.DataTableBuscar(dtResul, "n_id", "c_codsun", entVenta.n_idtipdoc.ToString(), "N").ToString();

            objCli.mysConec = mysConec;
            entCliente = objCli.TraerRegistro(entVenta.n_idcli);      // OBTENEMOS LOS DATOS DEL CLIENTE

            funemp.mysConec = mysConec;
            funemp.TraerRegistro(STU_SISTEMA.EMPRESAID);
            entEmp = funemp.e_Empresa;

            string c_nomArchivo = "";
            string[] c_Destinatarios = { entCliente.c_email };
            string c_nomarchzip = entEmp.c_vtafealmenv + "\\" + entEmp.c_numdoc + "-" + c_tipdoc + "-" + entVenta.c_numser + "-" + entVenta.c_numdoc.ToString().Substring(2, 8) + ".zip";
            string c_nomarchpdf = "c:\\ssf-net\\tmp\\" + entEmp.c_numdoc + "-" + c_tipdoc + "-" + entVenta.c_numser + "-" + entVenta.c_numdoc.ToString().Substring(2, 8) + ".pdf";
            string[] c_ListaArchivos = { c_nomarchpdf, c_nomarchzip };

            ReportImprimirDocumento(n_IdVenta, entVenta.n_idtipdoc, true, c_nomarchpdf, b_EsfacturaElectronica);

            if (entEmp.c_corctaven == "")
            { 
                booOcurrioError = true;
                StrErrorMensaje = "No ha definido la cuenta de correo del area de ventas ";     
                IntErrorNumber = 1;
                return b_result;
            }
            if (entEmp.c_corrctapopcon == "")
            {
                booOcurrioError = true;
                StrErrorMensaje = "No ha definido la contraseña de la cuenta POP de correo ";
                IntErrorNumber = 1;
                return b_result;
            }
            if (entEmp.c_corrctapop == "")
            {
                booOcurrioError = true;
                StrErrorMensaje = "No ha definido la cuenta POP de correo ";
                IntErrorNumber = 1;
                return b_result;
            }
            if (entCliente.c_email == "")
            {
                booOcurrioError = true;
                StrErrorMensaje = "El cliente no tiene una cuenta de correo asignada";
                IntErrorNumber = 1;
                return b_result;
            }
            if (entEmp.c_nomjefven == "")
            {
                booOcurrioError = true;
                StrErrorMensaje = "No ha especificado el nombre del jefe de ventas";
                IntErrorNumber = 1;
                return b_result;
            }

            funcorr.c_AsuntoCorreo = "Remision del Comprobante de pago Nº " + entVenta.c_numser + "-" + entVenta.c_numdoc + " emitido el dia " + entVenta.d_fchdoc.ToString("dd/MM/yyyy") + " a la razon social " + entCliente.c_nombre + " identificada con RUC Nº " + entCliente.c_numdoc;
            funcorr.c_CorreoRecibeCopia = entEmp.c_corctaven; 
            funcorr.c_CuentaOrigen = entEmp.c_corctaven;
            funcorr.c_Destinatarios = c_Destinatarios;
            funcorr.c_CuentaContraseña = entEmp.c_corrctapopcon;
            funcorr.c_DominioCuenta = entEmp.c_corrctapop;           
            funcorr.c_CuerpoCorreo = "Sres(as). " + entCliente.c_nombre + ", mediante la presente tengo a bien hacerles llegar el documento de venta Nº " + entVenta.c_numser + "-" + entVenta.c_numdoc + ", emitido el dia " + entVenta.d_fchdoc.ToString("dd/MM/yyyy") + "\r\n" +
                  " " + "\r\n" +
                  "Atentamente" + "\r\n" +
                  " " + "\r\n" +
                  entEmp.c_nomjefven + "\r\n" +
                  "JEFE DE VENTAS";

            funcorr.c_ListaArchivos = c_ListaArchivos;
            funcorr.EnviarCorreo();
            funcorr = null;
            // ELIMINAMOS EL ARCHIVO PDF CREADO
            // funIO.Fil_EliminarArchivo(c_nomarchpdf);
            b_result = true;
            return b_result;
        }
        string CodigoBarra(int n_IdEmpresa, double n_IdVenta)
        {
            DataTable dtResul = new DataTable();
            string c_nomarchivo = "";

            CD_vta_ventas miFun = new CD_vta_ventas();
            miFun.mysConec = mysConec;

            dtResul = miFun.CodigoBarra(n_IdEmpresa, n_IdVenta);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                string texto = dtResul.Rows[0]["c_dato"].ToString();
                c_nomarchivo = dtResul.Rows[0]["nomarch"].ToString();
                BarcodePDF417 codigobarras = new BarcodePDF417();
                codigobarras.Options = BarcodePDF417.PDF417_USE_ASPECT_RATIO;
                codigobarras.ErrorLevel = 8;
                codigobarras.SetText(texto);
                System.Drawing.Bitmap bm = new Bitmap(codigobarras.CreateDrawingImage(Color.Black, Color.White));
                
                c_nomarchivo = @"c:\ssf-net\tmp\" + c_nomarchivo + ".bmp";
                
                bm.Save(c_nomarchivo);
                //c_nomarchivo = dtResul.Rows[0]["nomarch"].ToString() + ".bmp";
            }

            return c_nomarchivo;
        }
        public DataTable Consulta4(int n_IdEmpresa, int n_idTipoDocumento, int IdCliente)
        {
            DataTable dtResul = new DataTable();

            CD_vta_ventas miFun = new CD_vta_ventas();
            miFun.mysConec = mysConec;

            dtResul = miFun.Consulta4(n_IdEmpresa, n_idTipoDocumento, IdCliente);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public DataTable Consulta5(int n_IdEmpresa, int n_IdDocumento)
        {
            DataTable dtResul = new DataTable();

            CD_vta_ventas miFun = new CD_vta_ventas();
            miFun.mysConec = mysConec;

            dtResul = miFun.Consulta5(n_IdEmpresa, n_IdDocumento);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public BE_VTA_VENTAS TraerRegistro(int n_IdRegistro)
        {
            BE_VTA_VENTAS entVentas = new BE_VTA_VENTAS();

            CD_vta_ventas miFun = new CD_vta_ventas();
            miFun.mysConec = mysConec;
            entVentas = miFun.TraerRegistro(n_IdRegistro);
            LstDetalle = miFun.LstDetalle;
            LstDocumentos = miFun.LstDocumentos; 
            return entVentas;
        }
        public bool Insertar(BE_VTA_VENTAS entVentas)
        {
            CD_vta_ventas miFun = new CD_vta_ventas();
            bool booOk = false;

            miFun.mysConec = mysConec;

            miFun.LstDetalle = LstDetalle;
            miFun.LstDetalleOCT = LstDetalleOCT;
            miFun.LstDocumentos = LstDocumentos;
            miFun.LstDocumentosNC = LstDocumentosNC;
            miFun.LstDatos = LstDatos;

            if (miFun.Insertar(entVentas) == true)
            {
                if (entVentas.n_idlib != 33)
                {
                    if (b_GenerarAsiento == true)
                    { 
                        string c_NumAsi = "";
                
                        CN_con_diario funCon = new CN_con_diario();
                        //mysConec = FunMysql.ReAbrirConeccion(mysConec);
                        funCon.mysConec = mysConec;
                        funCon.STU_SISTEMA = STU_SISTEMA;
                        funCon.GenerarAsientoVentas(STU_SISTEMA.EMPRESAID, Convert.ToInt32(entVentas.n_id), STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO, entVentas.n_idlib, "");
                        c_NumAsi = funCon.c_NewNumAsiento;
                        miFun.AgregarNumAsi(Convert.ToInt32(miFun.n_IdGenerado), c_NumAsi);
                        booOk = true;
                    }
                }
                else
                {
                    booOk = true;
                }
            }
                        
            n_IdGenerado = miFun.n_IdGenerado;
                        
            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Insertar2(BE_VTA_VENTAS entVentas, int n_IdCargo)
        {
            // SE LLAMARA A ESTE METODO CUANDO VENA DEL MODULO DE ESTACIONAMIENTO
            CD_vta_ventas miFun = new CD_vta_ventas();
            bool booOk = false;

            miFun.mysConec = mysConec;

            miFun.LstDetalle = LstDetalle;
            miFun.LstDetalleOCT = LstDetalleOCT;
            miFun.LstDocumentos = LstDocumentos;
            miFun.LstDocumentosNC = LstDocumentosNC;
            miFun.LstDatos = LstDatos;
            miFun.l_diario = l_diario;

            if (miFun.Insertar2(entVentas, n_IdCargo) == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
                return booOk;
            }
            n_IdGenerado = miFun.n_IdGenerado;
            booOk = true;
            return booOk;
        }
        public bool Actualizar(BE_VTA_VENTAS entVentas)
        {
            BE_VTA_VENTAS entNuevoVentas = new BE_VTA_VENTAS();
            CD_vta_ventas miFun = new CD_vta_ventas();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoVentas.n_id = entVentas.n_id;
            entNuevoVentas.n_idemp = entVentas.n_idemp;
            entNuevoVentas.n_anotra = entVentas.n_anotra;
            entNuevoVentas.n_idmes = entVentas.n_idmes;      
            entNuevoVentas.n_idlib = entVentas.n_idlib;
            entNuevoVentas.c_numreg = entVentas.c_numreg;
            entNuevoVentas.n_idtippro = entVentas.n_idtippro;
            entNuevoVentas.n_idcli = entVentas.n_idcli;
            entNuevoVentas.n_idpunvencli = entVentas.n_idpunvencli;
            entNuevoVentas.n_idtipdoc = entVentas.n_idtipdoc;
            entNuevoVentas.c_numser = entVentas.c_numser;
            entNuevoVentas.c_numdoc = entVentas.c_numdoc;
            entNuevoVentas.d_fchreg = entVentas.d_fchreg;
            entNuevoVentas.d_fchdoc = entVentas.d_fchdoc;
            entNuevoVentas.d_fchven = entVentas.d_fchven;
            entNuevoVentas.n_idconpag = entVentas.n_idconpag;
            entNuevoVentas.n_idmon = entVentas.n_idmon;
            entNuevoVentas.n_impbru = entVentas.n_impbru;
            entNuevoVentas.n_impbru2 = entVentas.n_impbru2;
            entNuevoVentas.n_impbru3 = entVentas.n_impbru3;
            entNuevoVentas.n_impinaf = entVentas.n_impinaf;
            entNuevoVentas.n_impigv = entVentas.n_impigv;
            entNuevoVentas.n_impisc = entVentas.n_impisc;
            entNuevoVentas.n_impotr = entVentas.n_impotr;
            entNuevoVentas.n_imptotven = entVentas.n_imptotven;
            entNuevoVentas.n_tc = entVentas.n_tc;
            entNuevoVentas.n_impsal = entVentas.n_impsal;
            entNuevoVentas.n_idven = entVentas.n_idven;
            entNuevoVentas.n_tasaigv = entVentas.n_tasaigv;
            entNuevoVentas.c_glosa = entVentas.c_glosa;
            entNuevoVentas.n_oriitem = entVentas.n_oriitem;
            entNuevoVentas.n_anulado = entVentas.n_anulado;
            entNuevoVentas.n_idtipven = entVentas.n_idtipven;
            entNuevoVentas.n_tc = entVentas.n_tc;
            entNuevoVentas.n_idtipdocref = entVentas.n_idtipdocref;
            entNuevoVentas.n_iddocref = entVentas.n_iddocref;
            entNuevoVentas.n_idtipdes = entVentas.n_idtipdes;
            entNuevoVentas.n_impdes = entVentas.n_impdes;
            entNuevoVentas.c_nomcli = entVentas.c_nomcli;
            entNuevoVentas.c_dircli = entVentas.c_dircli;
            entNuevoVentas.n_idpue = entVentas.n_idpue;

            entNuevoVentas.n_impsubtot = entVentas.n_impsubtot;
            entNuevoVentas.n_pordsc = entVentas.n_pordsc;
            entNuevoVentas.c_numlet = entVentas.c_numlet;

            entNuevoVentas.n_idtipmot = entVentas.n_idtipmot;
            entNuevoVentas.n_idtipdocmod = entVentas.n_idtipdocmod;
            entNuevoVentas.n_iddocmod = entVentas.n_iddocmod;
            entNuevoVentas.n_idtipope = entVentas.n_idtipope;

            entNuevoVentas.c_serdocref = entVentas.c_serdocref;
            entNuevoVentas.c_numdocref = entVentas.c_numdocref;
            entNuevoVentas.c_motnc = entVentas.c_motnc;
            miFun.LstDetalle = LstDetalle;
            miFun.LstDetalleOCT = LstDetalleOCT;
            miFun.LstDocumentos = LstDocumentos;
            miFun.LstDocumentosNC = LstDocumentosNC;

            // TRAEMOS LOS DOCUMENTOS ORIGINALES DE LA VENTA
            CD_vta_ventas objTMP = new CD_vta_ventas();
            List<BE_VTA_VENTASDOC> lstDocTMP = new List<BE_VTA_VENTASDOC>();

            objTMP.mysConec = mysConec;
            objTMP.TraerRegistro(Convert.ToInt32(entVentas.n_id));
            lstDocTMP = objTMP.LstDocumentos;

            if (miFun.Actualizar(entNuevoVentas, lstDocTMP) == true)
            {
                string c_NumAsi = entNuevoVentas.c_numreg;
                CN_con_diario funCon = new CN_con_diario();
                mysConec = FunMysql.ReAbrirConeccion(mysConec);
                funCon.mysConec = mysConec;
                funCon.STU_SISTEMA = STU_SISTEMA;

                funCon.GenerarAsientoVentas(STU_SISTEMA.EMPRESAID, Convert.ToInt32(entNuevoVentas.n_id), STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO, entVentas.n_idlib, entNuevoVentas.c_numreg);
            }

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;
            booOk = true;
            return booOk;
        }
        public bool Eliminar(int n_Id)
        {
            CD_vta_ventas miFun = new CD_vta_ventas();
            bool booOk = false;

            miFun.mysConec = mysConec;
            miFun.LstDocumentos = LstDocumentos;

            // TRAEMOS LOS DOCUMENTOS ORIGINALES DE LA VENTA
            CD_vta_ventas objTMP = new CD_vta_ventas();
            List<BE_VTA_VENTASDOC> lstDocTMP = new List<BE_VTA_VENTASDOC>();

            objTMP.mysConec = mysConec;
            objTMP.TraerRegistro(Convert.ToInt32(n_Id));
            lstDocTMP = objTMP.LstDocumentos;

            booOk = miFun.Eliminar(n_Id, lstDocTMP);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Anular(int n_Id)
        {
            CD_vta_ventas miFun = new CD_vta_ventas();
            bool booOk = false;

            miFun.mysConec = mysConec;
            //miFun.LstDocumentos = LstDocumentos;

            // TRAEMOS LOS DOCUMENTOS ORIGINALES DE LA VENTA
            CD_vta_ventas objTMP = new CD_vta_ventas();
            List<BE_VTA_VENTASDOC> lstDocTMP = new List<BE_VTA_VENTASDOC>();

            objTMP.mysConec = mysConec;
            objTMP.TraerRegistro(Convert.ToInt32(n_Id));
            lstDocTMP = objTMP.LstDocumentos;

            booOk = miFun.Anular(n_Id, lstDocTMP);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool CambiarEstadoEnviado(int n_IdEmpresa, int n_IdAno, int n_IdMes)
        {
            CD_vta_ventas miFun = new CD_vta_ventas();
            bool booOk = false;

            miFun.mysConec = mysConec;

            booOk = miFun.CambiarEstadoEnviado(n_IdEmpresa, n_IdAno, n_IdMes);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public void ReportImprimirDocumento(double n_idventa, int n_IdTipoDocumento, bool b_Exportar, string c_Archivo, bool b_EsFacturaElectronica)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[3, 3];
            string[,] arrPara2 = new string[2, 3];

            arrPara[0, 0] = "n_idemp";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = STU_SISTEMA.EMPRESAID.ToString();

            arrPara[1, 0] = "n_idvta";
            arrPara[1, 1] = "N";
            arrPara[1, 2] = n_idventa.ToString();

            arrPara[2, 0] = "rutaimagen";
            arrPara[2, 1] = "C";
            arrPara[2, 2] = CodigoBarra(STU_SISTEMA.EMPRESAID, n_idventa).ToString();

            if (b_EsFacturaElectronica == true)
            {
                if (n_IdTipoDocumento == 2) { c_NomArchivo = "Rpt_DocE_Factura.rpt"; }
                if (n_IdTipoDocumento == 4) { c_NomArchivo = "Rpt_DocE_Boleta.rpt"; }
                if (n_IdTipoDocumento == 8) { c_NomArchivo = "Rpt_DocE_NotaCredito.rpt"; }
                if (n_IdTipoDocumento == 9) { c_NomArchivo = "Rpt_DocE_NotaDebito.rpt"; }
                if (n_IdTipoDocumento == 81) { c_NomArchivo = "Rpt_DocE_Recibo.rpt"; }

                c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "ventas\\" + c_NomArchivo;
            }
            else
            {
                BE_SYS_EMPRESA entEmp = new BE_SYS_EMPRESA();
                DataTable dtresut = new DataTable();
                CN_sys_empresa funsys = new CN_sys_empresa();
                funsys.mysConec = mysConec;
                funsys.TraerRegistro(STU_SISTEMA.EMPRESAID);
                entEmp = funsys.e_Empresa;
                dtresut = funsys.dtFolderDocImpresion;
                dtresut = funDatos.DataTableFiltrar(dtresut, "n_idtipdoc = " + n_IdTipoDocumento.ToString() + "");

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

                //CN_sys_empresa funemp = new CN_sys_empresa();
                
                c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "ventas\\" + entEmp.c_folderimp + "\\" + c_NomArchivo;
            }

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "VENTAS - IMPRESION DE DOCUMENTOS DE VENTA";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara; 
            xVisor.b_Exportar = b_Exportar;
            xVisor.c_NombreArchivoExportar = c_Archivo;
            xVisor.VerCrystal();
        }
        public void ReportImprimirDocumentoPV(double n_idventa, int n_IdTipoDocumento, bool b_Exportar, string c_Archivo, bool b_EsFacturaElectronica)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[3, 3];
            string[,] arrPara2 = new string[2, 3];

            arrPara[0, 0] = "n_idemp";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = STU_SISTEMA.EMPRESAID.ToString();

            arrPara[1, 0] = "n_idvta";
            arrPara[1, 1] = "N";
            arrPara[1, 2] = n_idventa.ToString();

            arrPara[2, 0] = "rutaimagen";
            arrPara[2, 1] = "C";
            arrPara[2, 2] = CodigoBarra(STU_SISTEMA.EMPRESAID, n_idventa).ToString();

            if (b_EsFacturaElectronica == true)
            {
                if (n_IdTipoDocumento == 2) { c_NomArchivo = "Rpt_DocumentoPV.rpt"; }
                if (n_IdTipoDocumento == 4) { c_NomArchivo = "Rpt_DocumentoPV.rpt"; }
                if (n_IdTipoDocumento == 13) { c_NomArchivo = "Rpt_DocumentoPV.rpt"; }

                c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "ventas\\" + c_NomArchivo;
            }
            else
            {
                BE_SYS_EMPRESA entEmp = new BE_SYS_EMPRESA();
                DataTable dtresut = new DataTable();
                CN_sys_empresa funsys = new CN_sys_empresa();
                funsys.mysConec = mysConec;
                funsys.TraerRegistro(STU_SISTEMA.EMPRESAID);
                entEmp = funsys.e_Empresa;
                dtresut = funsys.dtFolderDocImpresion;
                dtresut = funDatos.DataTableFiltrar(dtresut, "n_idtipdoc = " + n_IdTipoDocumento.ToString() + "");

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

                //CN_sys_empresa funemp = new CN_sys_empresa();

                c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "ventas\\" + entEmp.c_folderimp + "\\" + c_NomArchivo;
            }

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "VENTAS - IMPRESION DE DOCUMENTOS DE VENTA";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.b_Exportar = b_Exportar;
            xVisor.c_NombreArchivoExportar = c_Archivo;
            xVisor.VerCrystal();
        }
        public void ReportDocVentasMes(int n_idemp, int n_idano, int n_idmes)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[5, 3];

            arrPara[0, 0] = "c_mes";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = n_idmes.ToString();

            arrPara[1, 0] = "n_ano";
            arrPara[1, 1] = "N";
            arrPara[1, 2] = n_idano.ToString();

            arrPara[2, 0] = "n_idemp";
            arrPara[2, 1] = "N";
            arrPara[2, 2] = n_idemp.ToString();

            arrPara[3, 0] = "c_titulo1";
            arrPara[3, 1] = "C";
            arrPara[3, 2] = "REPORTE DE DOCUMENTOS EMITIDOS";

            arrPara[4, 0] = "c_titulo2";
            arrPara[4, 1] = "C";
            arrPara[4, 2] = "POR MESES";

            c_NomArchivo = "RptDocVentasMes.rpt";
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "ventas\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "VENTAS - REPORTE DE DOCUMENTOS DE VENTA";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
        public void VerReporteLiquidacion(int n_TipoReporte, string c_FechaInicio, string c_FchTermino)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[7, 3];

            arrPara[0, 0] = "n_idemp";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = STU_SISTEMA.EMPRESAID.ToString();

            arrPara[1, 0] = "c_fchini";
            arrPara[1, 1] = "C";
            arrPara[1, 2] = c_FechaInicio;

            arrPara[2, 0] = "c_fchfin";
            arrPara[2, 1] = "C";
            arrPara[2, 2] = c_FchTermino;

            if (n_TipoReporte == 1)
            {
                arrPara[3, 0] = "c_titulo1";
                arrPara[3, 1] = "C";
                arrPara[3, 2] = "LIQUIDACION DE COBRANZA - RESUMIDO";
            }
            else
            {
                arrPara[3, 0] = "c_titulo1";
                arrPara[3, 1] = "C";
                arrPara[3, 2] = "LIQUIDACION DE COBRANZA - DETALLADO";
            }

            arrPara[4, 0] = "c_titulo2";
            arrPara[4, 1] = "C";
            arrPara[4, 2] = "DEL " + c_FechaInicio + " AL " + c_FchTermino;

            arrPara[5, 0] = "c_apenom";
            arrPara[5, 1] = "C";
            arrPara[5, 2] = STU_SISTEMA.EMPRESANOMBRE;

            arrPara[6, 0] = "c_empnumdoc";
            arrPara[6, 1] = "C";
            arrPara[6, 2] = STU_SISTEMA.EMPRESARUC;

            c_NomArchivo = "Rpt_Cttehojaliqdia.rpt";
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "ventas\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "VENTAS - LIQUIDACION DE COBRANZAS";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
        //public bool CargarVentasPendEnvio(int n_Idempresa, int n_IdTipoDocumento)
        //{
        //    bool b_Result = false;

        //    CD_vta_ventas miFun = new CD_vta_ventas();
        //    miFun.mysConec = mysConec;

        //    if (miFun.CargarVentasPendEnvio(n_Idempresa, n_IdTipoDocumento) == false)
        //    {
        //        booOcurrioError = miFun.booOcurrioError;
        //        StrErrorMensaje = miFun.StrErrorMensaje;
        //        IntErrorNumber = miFun.IntErrorNumber;
        //    }
        //    else
        //    {
        //        dtLista1 = miFun.dtLista1;
        //        dtLista2 = miFun.dtLista2;
        //    }
        //    b_Result = true;
        //    return b_Result;
        //}
        public bool GenerarArchivoPlano(int n_IdEmpresa, int n_IdTipoDocumento, string c_RutaDestino, string c_VersionFacturador)
        {
            //Helper.Genericas funGen = new Helper.Genericas();
            Helper.Cls_IO funIO = new Helper.Cls_IO();
            bool b_Result = false;
            int n_row = 0;
            string c_numdocide = STU_SISTEMA.EMPRESARUC;
            string c_tipdocven = "";
            string c_rutenv = c_RutaDestino;
            string c_ext = "";
            string c_archivo = "";
            DataTable dtCab = new DataTable();
            DataTable dtDet = new DataTable();
            DataTable dtLey = new DataTable();
            DataTable dtRow = new DataTable();
            DataTable dtRel = new DataTable();
            DataTable dtTri = new DataTable();

            CD_vta_ventas miFun = new CD_vta_ventas();
            miFun.mysConec = mysConec;

            if (c_VersionFacturador == "1.1")
            { 
                if (miFun.CargarVentasPendEnvio(n_IdEmpresa, n_IdTipoDocumento, STU_SISTEMA.MESTRABAJO) == false)
                {
                    StrErrorMensaje = miFun.StrErrorMensaje;
                    IntErrorNumber = miFun.IntErrorNumber;
                    booOcurrioError = miFun.booOcurrioError;
                    b_Result = false;
                    return b_Result;
                }
            }
            if (c_VersionFacturador == "1.2")
            {
                if (miFun.CargarVentasPendEnvioV12(n_IdEmpresa, n_IdTipoDocumento, STU_SISTEMA.MESTRABAJO) == false)
                {
                    StrErrorMensaje = miFun.StrErrorMensaje;
                    IntErrorNumber = miFun.IntErrorNumber;
                    booOcurrioError = miFun.booOcurrioError;
                    b_Result = false;
                    return b_Result;
                }
            }
            if (c_VersionFacturador == "1.3")
            {
                if (miFun.CargarVentasPendEnvioV13(n_IdEmpresa, n_IdTipoDocumento, STU_SISTEMA.MESTRABAJO) == false)
                {
                    StrErrorMensaje = miFun.StrErrorMensaje;
                    IntErrorNumber = miFun.IntErrorNumber;
                    booOcurrioError = miFun.booOcurrioError;
                    b_Result = false;
                    return b_Result;
                }
            }
            dtCab = miFun.dtLista1;
            dtDet = miFun.dtLista2;
            dtLey = miFun.dtLista3;
            dtRel = miFun.dtLista4;
            dtTri = miFun.dtLista5;

            //if ((n_IdTipoDocumento == 2) || (n_IdTipoDocumento == 4))
            //{
            //    c_ext = ".cab";
            //}
            
            //if ((n_IdTipoDocumento == 8) || (n_IdTipoDocumento == 9))
            //{
            //    c_ext = ".not";
            //}

            if (dtCab.Rows.Count != 0)
            {
                for (n_row = 0; n_row <= dtCab.Rows.Count - 1; n_row++)
                {
                    if ((n_IdTipoDocumento == 2) || (n_IdTipoDocumento == 4))
                    {
                        c_ext = ".cab";
                        // IMPRIMIMOS LA CABECERA DE LA FACTURA Y BOLETA - ARHIVO CAB
                        c_tipdocven = dtCab.Rows[n_row]["c_tipdocsun"].ToString();
                        dtRow = funDatos.DataTableFiltrar(dtCab, "n_id = " + dtCab.Rows[n_row]["n_id"].ToString() + "");
                        c_archivo = c_numdocide + "-" + c_tipdocven + "-" + dtCab.Rows[n_row]["c_numdoc"].ToString().Substring(0, 4) + "-" + dtCab.Rows[n_row]["c_numdoc"].ToString().Substring(7, 8) + c_ext;

                        if (funIO.Fil_GenerarTxt(dtRow, c_archivo, c_rutenv, 3) == false)
                        {
                            booOcurrioError = true;
                            StrErrorMensaje = funIO.c_err_mensaje;
                            IntErrorNumber = funIO.n_err_numero;
                            booOcurrioError = true;
                            b_Result = false;
                            break;
                        }
                    }
                    if ((n_IdTipoDocumento == 8) || (n_IdTipoDocumento == 9))
                    {
                        c_ext = ".not";
                        // IMPRIMIMOS LA CABECERA DE LA NOTA CREDITO Y DEBITO - ARHIVO CAB
                        c_tipdocven = dtCab.Rows[n_row]["c_tipdocsun"].ToString();
                        dtRow = funDatos.DataTableFiltrar(dtCab, "n_id = " + dtCab.Rows[n_row]["n_id"].ToString() + "");
                        c_archivo = c_numdocide + "-" + c_tipdocven + "-" + dtCab.Rows[n_row]["c_numdoc"].ToString().Substring(0, 4) + "-" + dtCab.Rows[n_row]["c_numdoc"].ToString().Substring(7, 8) + c_ext;

                        if (funIO.Fil_GenerarTxt(dtRow, c_archivo, c_rutenv, 3) == false)
                        {
                            booOcurrioError = true;
                            StrErrorMensaje = funIO.c_err_mensaje;
                            IntErrorNumber = funIO.n_err_numero;
                            booOcurrioError = true;
                            b_Result = false;
                            break;
                        }
                    }

                    // IMPRIMIMOS EL DETALLE DE LA FACTURA - ARCHIVO DET
                    dtRow = funDatos.DataTableFiltrar(dtDet, "n_idvta = " + dtCab.Rows[n_row]["n_id"].ToString() + "");
                    c_archivo = c_numdocide + "-" + c_tipdocven + "-" + dtCab.Rows[n_row]["c_numdoc"].ToString().Substring(0, 4) + "-" + dtCab.Rows[n_row]["c_numdoc"].ToString().Substring(7, 8) + ".det";
                    if (funIO.Fil_GenerarTxt(dtRow, c_archivo, c_rutenv, 1) == false)
                    {
                        booOcurrioError = true;
                        StrErrorMensaje = funIO.c_err_mensaje;
                        IntErrorNumber = funIO.n_err_numero;
                        booOcurrioError = true;
                        b_Result = false;
                        break;
                    }

                    // IMPRIMIMOS EL ARCHIVO LEY - ARCHIVO LEY
                    dtRow = funDatos.DataTableFiltrar(dtLey, "n_idvta = " + dtCab.Rows[n_row]["n_id"].ToString() + "");
                    c_archivo = c_numdocide + "-" + c_tipdocven + "-" + dtCab.Rows[n_row]["c_numdoc"].ToString().Substring(0, 4) + "-" + dtCab.Rows[n_row]["c_numdoc"].ToString().Substring(7, 8) + ".ley";
                    if (funIO.Fil_GenerarTxt(dtRow, c_archivo, c_rutenv, 1) == false)
                    {
                        booOcurrioError = true;
                        StrErrorMensaje = funIO.c_err_mensaje;
                        IntErrorNumber = funIO.n_err_numero;
                        booOcurrioError = true;
                        b_Result = false;
                        break;
                    }

                    // IMPRIMIMOS EL ARCHIVO RELACIONADOS - ARCHIVO TRI
                    dtRow = funDatos.DataTableFiltrar(dtTri, "n_idvta = " + dtCab.Rows[n_row]["n_id"].ToString() + "");
                    c_archivo = c_numdocide + "-" + c_tipdocven + "-" + dtCab.Rows[n_row]["c_numdoc"].ToString().Substring(0, 4) + "-" + dtCab.Rows[n_row]["c_numdoc"].ToString().Substring(7, 8) + ".tri";
                    if (funIO.Fil_GenerarTxt(dtRow, c_archivo, c_rutenv, 1) == false)
                    {
                        booOcurrioError = true;
                        StrErrorMensaje = funIO.c_err_mensaje;
                        IntErrorNumber = funIO.n_err_numero;
                        booOcurrioError = true;
                        b_Result = false;
                        break;
                    }

                    // IMPRIMIMOS EL ARCHIVO RELACIONADOS - ARCHIVO REL
                    dtRow = funDatos.DataTableFiltrar(dtRel, "n_idvta = " + dtCab.Rows[n_row]["n_id"].ToString() + "");
                    c_archivo = c_numdocide + "-" + c_tipdocven + "-" + dtCab.Rows[n_row]["c_numdoc"].ToString().Substring(0, 4) + "-" + dtCab.Rows[n_row]["c_numdoc"].ToString().Substring(7, 8) + ".rel";
                    if (funIO.Fil_GenerarTxt(dtRow, c_archivo, c_rutenv, 1) == false)
                    {
                        booOcurrioError = true;
                        StrErrorMensaje = funIO.c_err_mensaje;
                        IntErrorNumber = funIO.n_err_numero;
                        booOcurrioError = true;
                        b_Result = false;
                        break;
                    }
                }

                //ACTUALIZAMOS EL FLAG DE ENVIADO AL REGISTRO
                for (n_row = 0; n_row <= dtCab.Rows.Count - 1; n_row++)
                {
                    // ACTUALIZAMOS ES ESTADO DE NO ENVIADO A ENVIANDO 
                    if (miFun.ActualizarEstadoEnvio(Convert.ToInt32(dtCab.Rows[n_row]["n_id"]), 2) == false)
                    {
                        StrErrorMensaje = miFun.StrErrorMensaje;
                        IntErrorNumber = miFun.IntErrorNumber;
                        booOcurrioError = miFun.booOcurrioError;
                        b_Result = false;
                    }
                }
            }
            b_Result = true;
            return b_Result;
        }
        public bool GenerarArchivoPlanoPorDias(int n_IdEmpresa, int n_IdTipoDocumento, string c_FechaEmisionDocumento, string c_RutaDestino, string c_VersionFacturador)
        {
            //Helper.Genericas funGen = new Helper.Genericas();
            Helper.Cls_IO funIO = new Helper.Cls_IO();
            bool b_Result = false;
            int n_row = 0;
            string c_numdocide = STU_SISTEMA.EMPRESARUC;
            string c_tipdocven = "";
            string c_rutenv = c_RutaDestino;
            string c_ext = "";
            string c_archivo = "";
            DataTable dtCab = new DataTable();
            DataTable dtDet = new DataTable();
            DataTable dtLey = new DataTable();
            DataTable dtRow = new DataTable();
            DataTable dtRel = new DataTable();
            DataTable dtTri = new DataTable();

            CD_vta_ventas miFun = new CD_vta_ventas();
            miFun.mysConec = mysConec;

            //if (c_VersionFacturador == "1.1")
            //{
            //    if (miFun.CargarVentasPendEnvio(n_IdEmpresa, n_IdTipoDocumento, STU_SISTEMA.MESTRABAJO) == false)
            //    {
            //        StrErrorMensaje = miFun.StrErrorMensaje;
            //        IntErrorNumber = miFun.IntErrorNumber;
            //        booOcurrioError = miFun.booOcurrioError;
            //        b_Result = false;
            //        return b_Result;
            //    }
            //}
            if (c_VersionFacturador == "1.2")
            {
                if (miFun.CargarVentasPendEnvioV12PorDias(n_IdEmpresa, n_IdTipoDocumento, c_FechaEmisionDocumento) == false)
                {
                    StrErrorMensaje = miFun.StrErrorMensaje;
                    IntErrorNumber = miFun.IntErrorNumber;
                    booOcurrioError = miFun.booOcurrioError;
                    b_Result = false;
                    return b_Result;
                }
            }
            dtCab = miFun.dtLista1;
            dtDet = miFun.dtLista2;
            dtLey = miFun.dtLista3;
            dtRel = miFun.dtLista4;
            dtTri = miFun.dtLista5;

            //if ((n_IdTipoDocumento == 2) || (n_IdTipoDocumento == 4))
            //{
            //    c_ext = ".cab";
            //}

            //if ((n_IdTipoDocumento == 8) || (n_IdTipoDocumento == 9))
            //{
            //    c_ext = ".not";
            //}

            if (dtCab.Rows.Count != 0)
            {
                for (n_row = 0; n_row <= dtCab.Rows.Count - 1; n_row++)
                {
                    if ((n_IdTipoDocumento == 2) || (n_IdTipoDocumento == 4))
                    {
                        c_ext = ".cab";
                        // IMPRIMIMOS LA CABECERA DE LA FACTURA Y BOLETA - ARHIVO CAB
                        c_tipdocven = dtCab.Rows[n_row]["c_tipdocsun"].ToString();
                        dtRow = funDatos.DataTableFiltrar(dtCab, "n_id = " + dtCab.Rows[n_row]["n_id"].ToString() + "");
                        c_archivo = c_numdocide + "-" + c_tipdocven + "-" + dtCab.Rows[n_row]["c_numdoc"].ToString().Substring(0, 4) + "-" + dtCab.Rows[n_row]["c_numdoc"].ToString().Substring(7, 8) + c_ext;

                        if (funIO.Fil_GenerarTxt(dtRow, c_archivo, c_rutenv, 3) == false)
                        {
                            booOcurrioError = true;
                            StrErrorMensaje = funIO.c_err_mensaje;
                            IntErrorNumber = funIO.n_err_numero;
                            booOcurrioError = true;
                            b_Result = false;
                            break;
                        }
                    }
                    if ((n_IdTipoDocumento == 8) || (n_IdTipoDocumento == 9))
                    {
                        c_ext = ".not";
                        // IMPRIMIMOS LA CABECERA DE LA NOTA CREDITO Y DEBITO - ARHIVO CAB
                        c_tipdocven = dtCab.Rows[n_row]["c_tipdocsun"].ToString();
                        dtRow = funDatos.DataTableFiltrar(dtCab, "n_id = " + dtCab.Rows[n_row]["n_id"].ToString() + "");
                        c_archivo = c_numdocide + "-" + c_tipdocven + "-" + dtCab.Rows[n_row]["c_numdoc"].ToString().Substring(0, 4) + "-" + dtCab.Rows[n_row]["c_numdoc"].ToString().Substring(7, 8) + c_ext;

                        if (funIO.Fil_GenerarTxt(dtRow, c_archivo, c_rutenv, 3) == false)
                        {
                            booOcurrioError = true;
                            StrErrorMensaje = funIO.c_err_mensaje;
                            IntErrorNumber = funIO.n_err_numero;
                            booOcurrioError = true;
                            b_Result = false;
                            break;
                        }
                    }

                    // IMPRIMIMOS EL DETALLE DE LA FACTURA - ARCHIVO DET
                    dtRow = funDatos.DataTableFiltrar(dtDet, "n_idvta = " + dtCab.Rows[n_row]["n_id"].ToString() + "");
                    c_archivo = c_numdocide + "-" + c_tipdocven + "-" + dtCab.Rows[n_row]["c_numdoc"].ToString().Substring(0, 4) + "-" + dtCab.Rows[n_row]["c_numdoc"].ToString().Substring(7, 8) + ".det";
                    if (funIO.Fil_GenerarTxt(dtRow, c_archivo, c_rutenv, 1) == false)
                    {
                        booOcurrioError = true;
                        StrErrorMensaje = funIO.c_err_mensaje;
                        IntErrorNumber = funIO.n_err_numero;
                        booOcurrioError = true;
                        b_Result = false;
                        break;
                    }

                    // IMPRIMIMOS EL ARCHIVO LEY - ARCHIVO LEY
                    dtRow = funDatos.DataTableFiltrar(dtLey, "n_idvta = " + dtCab.Rows[n_row]["n_id"].ToString() + "");
                    c_archivo = c_numdocide + "-" + c_tipdocven + "-" + dtCab.Rows[n_row]["c_numdoc"].ToString().Substring(0, 4) + "-" + dtCab.Rows[n_row]["c_numdoc"].ToString().Substring(7, 8) + ".ley";
                    if (funIO.Fil_GenerarTxt(dtRow, c_archivo, c_rutenv, 1) == false)
                    {
                        booOcurrioError = true;
                        StrErrorMensaje = funIO.c_err_mensaje;
                        IntErrorNumber = funIO.n_err_numero;
                        booOcurrioError = true;
                        b_Result = false;
                        break;
                    }

                    // IMPRIMIMOS EL ARCHIVO RELACIONADOS - ARCHIVO TRI
                    dtRow = funDatos.DataTableFiltrar(dtTri, "n_idvta = " + dtCab.Rows[n_row]["n_id"].ToString() + "");
                    c_archivo = c_numdocide + "-" + c_tipdocven + "-" + dtCab.Rows[n_row]["c_numdoc"].ToString().Substring(0, 4) + "-" + dtCab.Rows[n_row]["c_numdoc"].ToString().Substring(7, 8) + ".tri";
                    if (funIO.Fil_GenerarTxt(dtRow, c_archivo, c_rutenv, 1) == false)
                    {
                        booOcurrioError = true;
                        StrErrorMensaje = funIO.c_err_mensaje;
                        IntErrorNumber = funIO.n_err_numero;
                        booOcurrioError = true;
                        b_Result = false;
                        break;
                    }

                    // IMPRIMIMOS EL ARCHIVO RELACIONADOS - ARCHIVO REL
                    dtRow = funDatos.DataTableFiltrar(dtRel, "n_idvta = " + dtCab.Rows[n_row]["n_id"].ToString() + "");
                    c_archivo = c_numdocide + "-" + c_tipdocven + "-" + dtCab.Rows[n_row]["c_numdoc"].ToString().Substring(0, 4) + "-" + dtCab.Rows[n_row]["c_numdoc"].ToString().Substring(7, 8) + ".rel";
                    if (funIO.Fil_GenerarTxt(dtRow, c_archivo, c_rutenv, 1) == false)
                    {
                        booOcurrioError = true;
                        StrErrorMensaje = funIO.c_err_mensaje;
                        IntErrorNumber = funIO.n_err_numero;
                        booOcurrioError = true;
                        b_Result = false;
                        break;
                    }
                }

                //ACTUALIZAMOS EL FLAG DE ENVIADO AL REGISTRO
                for (n_row = 0; n_row <= dtCab.Rows.Count - 1; n_row++)
                {
                    // ACTUALIZAMOS ES ESTADO DE NO ENVIADO A ENVIANDO 
                    if (miFun.ActualizarEstadoEnvio(Convert.ToInt32(dtCab.Rows[n_row]["n_id"]), 2) == false)
                    {
                        StrErrorMensaje = miFun.StrErrorMensaje;
                        IntErrorNumber = miFun.IntErrorNumber;
                        booOcurrioError = miFun.booOcurrioError;
                        b_Result = false;
                    }
                }
            }
            b_Result = true;
            return b_Result;
        }
        public bool ActualizarDocumento(string c_NombreArchivo, string [,] c_Informacion, int n_IdTipoDocumento)
        {
            CD_vta_ventas miFun = new CD_vta_ventas();
            bool b_resul = false;
            int n_row = 0;
            int n_numlin = Convert.ToInt32(c_Informacion.GetLongLength(0));
            string c_nomarch = Path.GetFileName(c_NombreArchivo);
            string c_numser = c_nomarch.Substring(17, 4);
            string c_numdoc = "00"+c_nomarch.Substring(22, 8);
            int n_IdTipDoc = n_IdTipoDocumento;
            string c_fchaut = c_Informacion[0,1];
            string c_horaut = c_Informacion[1,1];
            
            miFun.mysConec = mysConec;
            b_resul = miFun.ActualizarEstadoRecep(STU_SISTEMA.EMPRESAID, n_IdTipDoc, c_numser, c_numdoc, c_fchaut, c_horaut, c_nomarch);
            if (b_resul == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return b_resul;
        }
        public bool GenerarArchivoBaja(int n_IdEmpresa, int n_IdVenta, string c_RutaDestino)
        {
            Helper.Cls_IO funIO = new Helper.Cls_IO();
            bool b_result = false;
            DataTable dtResult = new DataTable();
            CD_vta_ventas miFun = new CD_vta_ventas();
            CN_vta_ventasbaja miFunbBaj = new CN_vta_ventasbaja();
            miFun.mysConec = mysConec;

            if (miFun.TraerParaBaja(n_IdEmpresa, n_IdVenta) == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
                return b_result;
            }
            else
            {
                dtResult = miFun.dtLista1;

                // TRAEMOS EL ULTIMO ID DE ENVIO DE BAJA
                miFunbBaj.mysConec = mysConec;
                string c_corr = miFunbBaj.Correlativo(n_IdEmpresa);
                if (miFunbBaj.b_OcurrioError == true)
                {
                    booOcurrioError = miFunbBaj.b_OcurrioError;
                    StrErrorMensaje = miFunbBaj.c_ErrorMensaje;
                    IntErrorNumber = miFunbBaj.n_ErrorNumber;
                    return b_result;
                }

                string c_fecha = Convert.ToDateTime(dtResult.Rows[0]["c_fchcom"].ToString()).ToString("yyyyMMdd");
                string c_archivo = STU_SISTEMA.EMPRESARUC + "-RA-" + c_fecha + "-" + c_corr + ".CBA";

                // INSERTAMOS LA BAJA EN LA TABLA DE BAJAS
                BE_VTA_VENTASBAJA e_baja = new BE_VTA_VENTASBAJA();
                e_baja.n_idemp = n_IdEmpresa;
                e_baja.n_idvta = n_IdVenta;
                e_baja.n_cor = Convert.ToInt32(c_corr);
                e_baja.c_arcbaj = c_archivo;
                e_baja.d_fchbaj = DateTime.Now;

                if (miFunbBaj.Insertar(e_baja) == false)
                {
                    booOcurrioError = miFunbBaj.b_OcurrioError;
                    StrErrorMensaje = miFunbBaj.c_ErrorMensaje;
                    IntErrorNumber = miFunbBaj.n_ErrorNumber;
                    return b_result;
                }
                                
                if (dtResult.Rows.Count == 1)
                { 
                    //string c_fecha = Convert.ToDateTime(dtResult.Rows[0]["c_fchcom"].ToString()).ToString("yyyyMMdd");
                    //string c_archivo = STU_SISTEMA.EMPRESARUC + "-RA-" + c_fecha + "-001"+".CBA";
                    if (funIO.Fil_GenerarTxt(dtResult, c_archivo, c_RutaDestino, 2) == false)
                    {
                        StrErrorMensaje = miFun.StrErrorMensaje;
                        IntErrorNumber = miFun.IntErrorNumber;
                        booOcurrioError = miFun.booOcurrioError;
                        return b_result;
                    }
                }
            }
            b_result = true;
            return b_result;
        }
        public bool LeerBajas(string c_RutaSunRes, string c_RutaSunEnv, string c_RutaSunDat, string c_RutaAlmRes, string c_RutaAlmEnv, int n_IdVenta)
        {
            bool b_result = false;
            Helper.Cls_IO funIO = new Helper.Cls_IO();
            string[] c_ListaArchivos;
            string[] c_ListaXml;
            int n_numnarc = 0;
            int n_row = 0;
            string c_nomarcbaj = "";

            // CARGAMOS TODOS LOS ARCHIVOS ZIP
            c_ListaArchivos = funIO.Dir_LeerDirectorio(c_RutaSunRes, "*.zip");
            n_numnarc = Convert.ToInt32(c_ListaArchivos.GetLongLength(0));

            if (n_numnarc == 0)
            {
                MessageBox.Show("No han encontrado bajas aceptadas por SUNAT", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return b_result;
            }

            if (n_numnarc != 0)
            { 
                for (n_row=0; n_row <= n_numnarc-1; n_row++)
                {
                    //201801710704680.zip
                    //1234567890123456789
                    if (Path.GetFileName(c_ListaArchivos[n_row]).Length == 19)              //  SI EL ARCHIVO DE RESPUESTA ES DE 19 CARACTERES ASUMINOS QUE ES UN ARCHIVO DE BAJA
                    {
                        c_nomarcbaj = c_ListaArchivos[n_row];            // ALMACENAMOS EL NOMBRE DEL ARCHIVO DE BAJA
                        if (funIO.Zip_Descomprimir(c_nomarcbaj, c_RutaSunRes, "xml") == true)
                        {
                            c_ListaXml = funIO.Dir_LeerDirectorio(c_RutaSunRes, "*.xml"); // LEEMMOS EL ARCHIVO XML DESCOMPRIMIDO
                            string[,] arrInf = new string[4, 2];

                            if (EstaAprobado(c_ListaXml[0], ref arrInf) == true)           // SI EL ARCHIVO ESTA ACEPTADO DAMOS DE BAJA AL DOCUMENTO
                            {
                                string c_nomarch = Path.GetFileName(c_ListaXml[0]).Substring(2,27)+".CBA";
                                CN_vta_ventasbaja funbaj = new CN_vta_ventasbaja();
                                funbaj.mysConec = mysConec;
                                if (funbaj.BuscarArchivo(STU_SISTEMA.EMPRESAID, c_nomarch) == false)
                                {
                                    return b_result;
                                }
                                DataTable dtRestul = new DataTable();
                                string c_numdoc = "";
                                dtRestul = funbaj.dtLista;
                                n_IdVenta = Convert.ToInt32(dtRestul.Rows[0]["n_idvta"].ToString());
                                c_numdoc = dtRestul.Rows[0]["c_numdoc"].ToString();

                                if (Anular(n_IdVenta) == true)                           // ANULAMOS EL DOCUMENTO DE VENTA
                                {
                                    
                                    MessageBox.Show("¡ Se de dio de baja con exito al documento Nº " + c_numdoc + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                                    string c_arch = Path.GetFileName(c_ListaXml[0]).Substring(2, 27) + ".zip";
                                    funIO.Fil_CopiarArchivo(c_nomarcbaj, c_RutaAlmRes);        // CAPIAMOS EL ARCHIVO ZIP DE RESPUESTA
                                    funIO.Fil_CopiarArchivo(c_RutaSunEnv + "\\" + c_arch, c_RutaAlmEnv);             // CAPIAMOS EL ARCHIVO DE ENVIO A SUNATDE LA BAJA

                                    funIO.Fil_EliminarArchivo(c_ListaXml[0]);                  // BORRAMOS EL XML DESEMPAQUETADO
                                    funIO.Fil_EliminarArchivo(c_nomarcbaj);                    // BORRAMOS EL ARCHIVO ZIP DE BAJA
                                    funIO.Fil_EliminarArchivo(c_RutaSunEnv + "\\" + c_arch);   // BORRAMOS EL ARCHIVO ZIP DE ENVIO

                                    c_arch = Path.GetFileName(c_ListaXml[0]).Substring(2, 27) + ".cba";
                                    funIO.Fil_EliminarArchivo(c_RutaSunDat + "\\" + c_arch);   // BORRAMOS EL ARCHIVO TXT DE BAJA

                                    b_result = true;
                                }
                            }
                            else
                            {
                                funIO.Fil_EliminarArchivo(c_ListaXml[0]);
                                StrErrorMensaje = arrInf[3,1];
                                IntErrorNumber = Convert.ToInt32(arrInf[2, 1].ToString());
                            }
                        }
                    }
                }
            }

            return b_result;
        }
        public bool EstaAprobado(string c_NombreArchivo, ref string[,] c_Informacion)
        {
            bool b_result = false;
            XML_funciones funXML = new XML_funciones();
            Cls_IO funIO = new Cls_IO();

            string[,] arrDatos = new string[4, 2];

            arrDatos[0, 0] = "cbc:ResponseDate";
            arrDatos[0, 1] = "";

            arrDatos[1, 0] = "cbc:ResponseTime";
            arrDatos[1, 1] = "";

            arrDatos[2, 0] = "cbc:ResponseCode";
            arrDatos[2, 1] = "";

            arrDatos[3, 0] = "cbc:Description";
            arrDatos[3, 1] = "";

            if (funXML.XML_LeerNodo(ref arrDatos, c_NombreArchivo) == false)
            {
                return b_result;
            }
            //}
            c_Informacion = arrDatos;
            if (arrDatos[2, 1] == "0")       // SI EL CODIGO DE ERROR ES IGUAL A 0 QUIERE DECIR QUE SE APROBO LA BAJA
            {
                b_result = true;
            }

            return b_result;
        }
        public bool RegeneraAsientos(int n_IdEmpresa, int n_IdMesTrabajo, int n_IdAnoTrabajo, int n_IdLibro)
        {
            DataTable dtLis = new DataTable();
            bool b_result = false;
            int n_row = 0;
            int n_idreg = 0;
            CD_vta_ventas miFun = new CD_vta_ventas();
            CD_con_diario o_Conta = new CD_con_diario();
            CN_con_diario funCon = new CN_con_diario();
            miFun.mysConec = mysConec;

            o_Conta.mysConec = mysConec;
            b_result = o_Conta.EliminarLibroMes(n_IdLibro, n_IdAnoTrabajo, n_IdMesTrabajo, n_IdEmpresa);
            if (b_result == true)
            {
                dtLis = miFun.Listar(n_IdEmpresa, n_IdMesTrabajo, n_IdAnoTrabajo, n_IdLibro);
                dtLis = funDatos.DataTableOrdenar(dtLis, "c_numreg");
                for (n_row = 0; n_row <= dtLis.Rows.Count - 1; n_row++)
                {
                    string c_NumAsi = dtLis.Rows[n_row]["c_numreg"].ToString().Substring(4,4);
                    n_idreg = Convert.ToInt32(dtLis.Rows[n_row]["n_id"].ToString());
                    funCon.mysConec = mysConec;
                    funCon.STU_SISTEMA = STU_SISTEMA;
                    funCon.GenerarAsientoVentas(STU_SISTEMA.EMPRESAID, n_idreg, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO, 14, c_NumAsi);
                    c_NumAsi = funCon.c_NewNumAsiento;
                    miFun.AgregarNumAsi(n_idreg, c_NumAsi);
                }
                b_result = true;
            }
            return b_result;
        }
        public DataTable DocumentosRetencion(int n_IdEmpresa, int n_IdCliente)
        {
            DataTable dtResul = new DataTable();

            CD_vta_ventas miFun = new CD_vta_ventas();
            miFun.mysConec = mysConec;

            dtResul = miFun.DocumentosRetencion(n_IdEmpresa, n_IdCliente);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public DataTable DocumentosPercepcion(int n_IdEmpresa, int n_IdProveedor)
        {
            DataTable dtResul = new DataTable();

            CD_vta_ventas miFun = new CD_vta_ventas();
            miFun.mysConec = mysConec;

            dtResul = miFun.DocumentosPercepcion(n_IdEmpresa, n_IdProveedor);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public DataTable DocumentosConSaldo(int n_IdEmpresa, int n_IdCliente)
        {
            DataTable dtResult = new DataTable();
            CD_vta_ventas objVen = new CD_vta_ventas();
            string[,] arrCabeceraFlexFil = new string[12, 5];

            objVen.mysConec = mysConec;
            dtResult = objVen.Consulta8(n_IdEmpresa, n_IdCliente);

            // FLEX GRID DE LOS TAREAS
            arrCabeceraFlexFil[0, 0] = "Cliente";
            arrCabeceraFlexFil[0, 1] = "300";
            arrCabeceraFlexFil[0, 2] = "C";
            arrCabeceraFlexFil[0, 3] = "";
            arrCabeceraFlexFil[0, 4] = "c_nombre";

            arrCabeceraFlexFil[1, 0] = "Nº Documento";
            arrCabeceraFlexFil[1, 1] = "100";
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

            funDatos.Filtrar_CampoOrden = "c_numdoc";
            funDatos.Filtrar_Titulo = "Documentos Pendientes de Pago";
            funDatos.Filtrar_ColumnaCheck = 11;
            funDatos.Filtrar_ColumnaBusqueda = 12;
            funDatos.Filtrar_CampoBusqueda = "n_id";
            funDatos.Filtrar_AplicarFiltro = true;
            dtResult = funDatos.Filtrar(arrCabeceraFlexFil, dtResult);

            return dtResult;
        }
        public bool InsertarAnulado(int n_IdEmpresa, int n_Id, int n_IdTipDoc, string c_FchDocumento, int n_IdMes, int n_IdAno, string c_NumSer, string c_NumDoc, string c_FechaRegistro, double n_TipoCambio, int n_IdLibro)
        {
            bool booOk = false;
            CD_vta_ventas miFun = new CD_vta_ventas();

            miFun.mysConec = mysConec;
            if (miFun.InsertarAnulado(n_IdEmpresa, n_Id, n_IdTipDoc, c_FchDocumento, n_IdMes, n_IdAno, c_NumSer, c_NumDoc, c_FechaRegistro, n_TipoCambio, n_IdLibro) == true)
            {
                string c_NumAsi = "";
                int n_idgen = Convert.ToInt32(miFun.n_IdGenerado);
                CN_con_diario funCon = new CN_con_diario();
                mysConec = FunMysql.ReAbrirConeccion(mysConec);
                funCon.mysConec = mysConec;
                funCon.STU_SISTEMA = STU_SISTEMA;
                if (n_IdLibro != 33)
                {
                    funCon.GenerarAsientoVentas(n_IdEmpresa, n_idgen, n_IdAno, n_IdMes, n_IdLibro, "");
                    c_NumAsi = funCon.c_NewNumAsiento;
                    miFun.AgregarNumAsi(Convert.ToInt32(miFun.n_IdGenerado), c_NumAsi);
                }
                booOk = true;
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            booOk = true;
            return booOk;
        }
        public DataTable DocumentosPendJalAlmacen(int n_IdEmpresa, int n_AnoTrabajo)
        {
            DataTable dtResult = new DataTable();
            CD_vta_ventas objVen = new CD_vta_ventas();
            string[,] arrCabeceraFlexFil = new string[7, 5];

            objVen.mysConec = mysConec;
            dtResult = objVen.Consulta9(n_IdEmpresa, n_AnoTrabajo);
 
            // FLEX GRID DE LOS TAREAS
            arrCabeceraFlexFil[0, 0] = "Nº Documento";
            arrCabeceraFlexFil[0, 1] = "120";
            arrCabeceraFlexFil[0, 2] = "C";
            arrCabeceraFlexFil[0, 3] = "c_numdoc";

            arrCabeceraFlexFil[1, 0] = "T.D.";
            arrCabeceraFlexFil[1, 1] = "40";
            arrCabeceraFlexFil[1, 2] = "C";
            arrCabeceraFlexFil[1, 3] = "c_tipdocabr";

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

            arrCabeceraFlexFil[5, 0] = "Cliente";
            arrCabeceraFlexFil[5, 1] = "400";
            arrCabeceraFlexFil[5, 2] = "C";
            arrCabeceraFlexFil[5, 3] = "c_clinom";

            arrCabeceraFlexFil[6, 0] = "Id";
            arrCabeceraFlexFil[6, 1] = "0";
            arrCabeceraFlexFil[6, 2] = "N";
            arrCabeceraFlexFil[6, 3] = "";
            arrCabeceraFlexFil[6, 3] = "n_id";

            funDatos.Buscar_CampoBusqueda = "c_numdoc";
            funDatos.Buscar_CadFiltro = "";
            funDatos.Buscar_CampoOrden = "c_numdoc";
            funDatos.Buscar_Titulo = "Documentos Pendientes de Visar por Almacen";
            dtResult = funDatos.Buscar(arrCabeceraFlexFil, dtResult);

            return dtResult;
        }
        public DataTable ProformasPendJalAlmacen(int n_IdEmpresa, int n_AnoTrabajo)
        {
            DataTable dtResult = new DataTable();
            CD_vta_ventas objVen = new CD_vta_ventas();
            string[,] arrCabeceraFlexFil = new string[7, 5];

            objVen.mysConec = mysConec;
            dtResult = objVen.Consulta21(n_IdEmpresa, n_AnoTrabajo);

            // FLEX GRID DE LOS TAREAS
            arrCabeceraFlexFil[0, 0] = "Nº Documento";
            arrCabeceraFlexFil[0, 1] = "120";
            arrCabeceraFlexFil[0, 2] = "C";
            arrCabeceraFlexFil[0, 3] = "c_numdoc";

            arrCabeceraFlexFil[1, 0] = "T.D.";
            arrCabeceraFlexFil[1, 1] = "40";
            arrCabeceraFlexFil[1, 2] = "C";
            arrCabeceraFlexFil[1, 3] = "c_tipdocabr";

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

            arrCabeceraFlexFil[5, 0] = "Cliente";
            arrCabeceraFlexFil[5, 1] = "400";
            arrCabeceraFlexFil[5, 2] = "C";
            arrCabeceraFlexFil[5, 3] = "c_clinom";

            arrCabeceraFlexFil[6, 0] = "Id";
            arrCabeceraFlexFil[6, 1] = "0";
            arrCabeceraFlexFil[6, 2] = "N";
            arrCabeceraFlexFil[6, 3] = "";
            arrCabeceraFlexFil[6, 3] = "n_id";

            funDatos.Buscar_CampoBusqueda = "c_numdoc";
            funDatos.Buscar_CadFiltro = "";
            funDatos.Buscar_CampoOrden = "c_numdoc";
            funDatos.Buscar_Titulo = "Proformas Pendientes de Visar por Almacen";
            dtResult = funDatos.Buscar(arrCabeceraFlexFil, dtResult);

            return dtResult;
        }
        public void VentasAnuales(int n_IdEmpresa, int n_Tipo, int n_AnoTrabajo, string c_CadenaIN)
        {
            CD_vta_ventas miFun = new CD_vta_ventas();
            miFun.mysConec = mysConec;

            miFun.VentasAnuales(n_IdEmpresa, n_Tipo, n_AnoTrabajo, c_CadenaIN);
            dtLista1 = miFun.dtLista1;
            if (miFun.booOcurrioError == true)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return;
        }
        public bool Consulta3(int n_IdEmpresa, int n_IdProveedor)
        {
            bool b_result = false;
            CD_vta_ventas miFun = new CD_vta_ventas();
            miFun.mysConec = mysConec;

            if (miFun.Consulta10(n_IdEmpresa, n_IdProveedor) == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
                return b_result;
            }
            dtLista = miFun.dtLista1;
            b_result = true;
            return b_result;
        }
        public DataTable BuscarDocumentoDetraccion(int n_IdEmpresa, int n_IdCliente)
        {
            string[,] arrCabeceraDg1 = new string[5, 4];
            DataTable dtResult = new DataTable();
            string c_CampoBuscar = "n_id";
            string c_CadenaFiltro = "";

            Helper.Genericas funDatos = new Helper.Genericas();

            Consulta3(n_IdEmpresa, n_IdCliente);
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
            arrCabeceraDg1[3, 3] = "n_imptotven";

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
        public void VentasAnualesxCliente(int n_IdEmpresa, string c_FechaInicio, string c_FechaTermino)
        {
            CD_vta_ventas miFun = new CD_vta_ventas();
            miFun.mysConec = mysConec;

            miFun.VentasAnualesxCliente(n_IdEmpresa, c_FechaInicio, c_FechaTermino);
            dtLista1 = miFun.dtLista1;
            if (miFun.booOcurrioError == true)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return;
        }
        public void VentasAnualesxItems(int n_IdEmpresa, string c_FechaInicio, string c_FechaTermino)
        {
            CD_vta_ventas miFun = new CD_vta_ventas();
            miFun.mysConec = mysConec;

            miFun.VentasAnualesxItems(n_IdEmpresa, c_FechaInicio, c_FechaTermino);
            dtLista1 = miFun.dtLista1;
            if (miFun.booOcurrioError == true)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return;
        }
        public void ActualizarDocumentosNoGenerados(int n_IdRegistro, int n_Estado)
        { 
            CD_vta_ventas miFun = new CD_vta_ventas();

            DialogResult Rpta = MessageBox.Show("¿ Desea cambiar el estado a procesado de este documento ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                miFun.mysConec = mysConec;
                
                if (miFun.ActualizarEstadoNoEnviados(n_IdRegistro, n_Estado) == false)
                {
                    MessageBox.Show("¿ No se pudo actualizar el estado de aceptado por el siguiente motivo: " + miFun.StrErrorMensaje + " ?", "", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                }
            }
        }
        public void ActualizarDocumentosNoGenerados(int n_IdRegistro, int n_EstadoEnviado, int n_EstadoACeptado)
        {
            CD_vta_ventas miFun = new CD_vta_ventas();

            DialogResult Rpta = MessageBox.Show("¿ Desea cambiar el estado a aceptado y procesado de este documento ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                miFun.mysConec = mysConec;

                if (miFun.ActualizarEstadoNoEnviados(n_IdRegistro, n_EstadoEnviado, n_EstadoACeptado) == false)
                {
                    MessageBox.Show("¿ No se pudo actualizar el estado de aceptado por el siguiente motivo: " + miFun.StrErrorMensaje + " ?", "", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                }
            }
        }
        public DataTable UltimoPrecioCliente(int n_Idempresa, int n_IdCliente)
        {
            DataTable dtResul = new DataTable();

            CD_vta_ventas miFun = new CD_vta_ventas();
            miFun.mysConec = mysConec;

            dtResul = miFun.UltimoPrecioCliente(n_Idempresa, n_IdCliente);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public void Consulta14(int n_Idempresa, int n_TipoMoneda, int n_TipoImporte)
        {
            CD_vta_ventas miFun = new CD_vta_ventas();
            miFun.mysConec = mysConec;

            miFun.Consulta14(n_Idempresa, n_TipoMoneda, n_TipoImporte);

            if (miFun.booOcurrioError == true)
            {
                dtLista = null;
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
                return;
            }
            dtLista1 = miFun.dtLista1;
            return;
        }
        public void Consulta17(int n_Idempresa, int n_AnoTrabajo, int n_TipoMoneda, int n_TipoImporte)
        {
            CD_vta_ventas miFun = new CD_vta_ventas();
            miFun.mysConec = mysConec;

            miFun.Consulta17(n_Idempresa, n_AnoTrabajo, n_TipoMoneda, n_TipoImporte);

            if (miFun.booOcurrioError == true)
            {
                dtLista = null;
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
                return;
            }
            dtLista1 = miFun.dtLista1;
            return;
        }
        public void Consulta18(int n_Idempresa, int n_AnoTrabajo, int n_TipoMoneda, int n_TipoImporte)
        {
            CD_vta_ventas miFun = new CD_vta_ventas();
            miFun.mysConec = mysConec;

            miFun.Consulta18(n_Idempresa, n_AnoTrabajo, n_TipoMoneda, n_TipoImporte);

            if (miFun.booOcurrioError == true)
            {
                dtLista = null;
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
                return;
            }
            dtLista1 = miFun.dtLista1;
            return;
        }
        public void Consulta19(int n_Idempresa, int n_AnoTrabajo, int n_TipoMoneda, int n_TipoImporte)
        {
            CD_vta_ventas miFun = new CD_vta_ventas();
            miFun.mysConec = mysConec;

            miFun.Consulta19(n_Idempresa, n_AnoTrabajo, n_TipoMoneda, n_TipoImporte);

            if (miFun.booOcurrioError == true)
            {
                dtLista = null;
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
                return;
            }
            dtLista1 = miFun.dtLista1;
            return;
        }
        //public void Consulta1(int n_IdEmpresa)
        //{
        //    CD_vta_ventas miFun = new CD_vta_ventas();
        //    miFun.mysConec = mysConec;

        //    miFun.Consulta1(n_IdEmpresa);
        //    dtLista = miFun.dtLista1;

        //    if (miFun.IntErrorNumber != null)
        //    {
        //        booOcurrioError = miFun.booOcurrioError;
        //        StrErrorMensaje = miFun.StrErrorMensaje;
        //        IntErrorNumber = miFun.IntErrorNumber;
        //    }
        //    return;
        //}
        public void Consulta20(int n_IdEmpresa)
        {
            CD_vta_ventas miFun = new CD_vta_ventas();
            miFun.mysConec = mysConec;

            miFun.Consulta20(n_IdEmpresa);
            dtLista = miFun.dtLista1;

            if (miFun.IntErrorNumber != null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return;
        }
        public DataTable BuscarProforma(int n_IdEmpresa, string c_DocumentosCargados)
        {
            Helper.Genericas funDatos = new Helper.Genericas();
            string[,] arrCabeceraDg1 = new string[5, 4];
            DataTable dtResult = new DataTable();

            Consulta20(n_IdEmpresa);
            dtResult = dtLista;
            //if (n_IdCliente != 0)
            //{
            //    dtResult = funDatos.DataTableFiltrar(dtResult, "n_idcli = " + n_IdCliente.ToString() + "");  // FILTRAMOS LOS DOCUMENTOS DEL PROVEEDOR
            //}
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
        public void Consulta6(string c_CadenaIN)
        {
            CD_vta_ventas miFun = new CD_vta_ventas();
            miFun.mysConec = mysConec;
            miFun.Consulta6(c_CadenaIN);
            dtLista1 = miFun.dtLista1;

            if (miFun.IntErrorNumber != 0)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return;
        }
        public bool BuscarDocumento(int n_IdCliente, int n_IdTipoDocumento, string c_NumeroSerie, string c_NumeroDocumento, int n_IdEmpresa)
        {
            CD_vta_ventas miFun = new CD_vta_ventas();
            bool b_ok = false;
            DataTable dtresult = new DataTable();
            miFun.mysConec = mysConec;
            miFun.BuscarDocumento(n_IdCliente, n_IdTipoDocumento, c_NumeroSerie, c_NumeroDocumento, n_IdEmpresa);
            dtresult = miFun.dtLista1;

            if (miFun.IntErrorNumber != 0)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
                return true;
            }
            else 
            {
                if (dtresult.Rows.Count >= 1)
                {
                    b_ok = true;
                }
                else
                {
                    b_ok = false;
                }
            }
            return b_ok;
        }
        public bool ConsultaNotaCredito(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo)
        {
            bool b_result = false;
            DataTable dtListar = new DataTable();
            CD_vta_ventas miFun = new CD_vta_ventas();
            miFun.mysConec = mysConec;
            Helper.Genericas xFunGen = new Helper.Genericas();

            miFun.NotaCreditoConsultaDetalle(n_IdEmpresa, n_AnoTrabajo, n_MesTrabajo);

                dtListar = miFun.dtLista1;
                DataTable dtResult = new DataTable();
                string[,] arrCabeceraFlexFil = new string[10, 5];
                string[,] arrCabeceraFlexFix = new string[1, 4];

                // FLEX GRID DE LOS TAREAS
                arrCabeceraFlexFil[0, 0] = "Fch. Documento";
                arrCabeceraFlexFil[0, 1] = "70";
                arrCabeceraFlexFil[0, 2] = "F";
                arrCabeceraFlexFil[0, 3] = "dd/MM/yyyy";
                arrCabeceraFlexFil[0, 4] = "d_fchdoc";

                arrCabeceraFlexFil[1, 0] = "T.D.";
                arrCabeceraFlexFil[1, 1] = "50";
                arrCabeceraFlexFil[1, 2] = "C";
                arrCabeceraFlexFil[1, 3] = "";
                arrCabeceraFlexFil[1, 4] = "c_abr";

                arrCabeceraFlexFil[2, 0] = "Nº Documento";
                arrCabeceraFlexFil[2, 1] = "110";
                arrCabeceraFlexFil[2, 2] = "C";
                arrCabeceraFlexFil[2, 3] = "";
                arrCabeceraFlexFil[2, 4] = "c_numdoc";

                arrCabeceraFlexFil[3, 0] = "Nombre";
                arrCabeceraFlexFil[3, 1] = "300";
                arrCabeceraFlexFil[3, 2] = "C";
                arrCabeceraFlexFil[3, 3] = "";
                arrCabeceraFlexFil[3, 4] = "c_nombre";

                arrCabeceraFlexFil[4, 0] = "Item";
                arrCabeceraFlexFil[4, 1] = "300";
                arrCabeceraFlexFil[4, 2] = "C";
                arrCabeceraFlexFil[4, 3] = "";
                arrCabeceraFlexFil[4, 4] = "c_despro";

                arrCabeceraFlexFil[5, 0] = "Uni. Med";
                arrCabeceraFlexFil[5, 1] = "60";
                arrCabeceraFlexFil[5, 2] = "C";
                arrCabeceraFlexFil[5, 3] = "";
                arrCabeceraFlexFil[5, 4] = "c_abrpre";

                arrCabeceraFlexFil[6, 0] = "Cantidad";
                arrCabeceraFlexFil[6, 1] = "70";
                arrCabeceraFlexFil[6, 2] = "D";
                arrCabeceraFlexFil[6, 3] = "0.00";
                arrCabeceraFlexFil[6, 4] = "n_canpro";

                arrCabeceraFlexFil[7, 0] = "P. Unitario";
                arrCabeceraFlexFil[7, 1] = "70";
                arrCabeceraFlexFil[7, 2] = "D";
                arrCabeceraFlexFil[7, 3] = "0.00";
                arrCabeceraFlexFil[7, 4] = "n_preunibru";

                arrCabeceraFlexFil[8, 0] = "Total";
                arrCabeceraFlexFil[8, 1] = "80";
                arrCabeceraFlexFil[8, 2] = "D";
                arrCabeceraFlexFil[8, 3] = "0.00";
                arrCabeceraFlexFil[8, 4] = "n_imptot";

                arrCabeceraFlexFil[9, 0] = "Motivo Devolucion";
                arrCabeceraFlexFil[9, 1] = "300";
                arrCabeceraFlexFil[9, 2] = "C";
                arrCabeceraFlexFil[9, 3] = "";
                arrCabeceraFlexFil[9, 4] = "c_des";


                arrCabeceraFlexFix[0, 0] = "0";
                arrCabeceraFlexFix[0, 1] = "1";
                arrCabeceraFlexFix[0, 2] = "9";
                arrCabeceraFlexFix[0, 3] = "DATOS DE LA VENTA";

                xFunGen.Filtrar_Titulo = "VENTAS - ITEMS DEVUELTOS CON NOTA DE CREDITO";
                xFunGen.MostrarDatos_NumFilasCabecera = 3;

                dtResult = xFunGen.MostrarDatos(arrCabeceraFlexFil, dtListar, arrCabeceraFlexFix);
                b_result = true;
                return b_result;
        }
        public bool ImportarApertura(int n_IdEmpresa, int n_AnoTrabajo, int n_IdLibro)
        {
            bool b_result = false;

            CD_vta_ventas miFun = new CD_vta_ventas();
            miFun.mysConec = mysConec;

            if (miFun.ImportarApertura(n_IdEmpresa, n_AnoTrabajo, n_IdLibro) == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
                return b_result;
            }
            b_result = true;
            return b_result;
        }
        public void Consulta22(int n_IdEmpresa, int n_IdLocal, string c_FechaVenta)
        {
            CD_vta_ventas miFun = new CD_vta_ventas();
            miFun.mysConec = mysConec;
            miFun.Consulta22(n_IdEmpresa, n_IdLocal, c_FechaVenta);
            dtLista1 = miFun.dtLista1;

            if (miFun.IntErrorNumber != 0)
            {
                dtLista1 = null;
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return;
        }
        public void Consulta23(int n_Idempresa, int n_AnoTrabajo, int n_TipoMoneda, int n_TipoImporte)
        {
            CD_vta_ventas miFun = new CD_vta_ventas();
            miFun.mysConec = mysConec;

            miFun.Consulta23(n_Idempresa, n_AnoTrabajo, n_TipoMoneda, n_TipoImporte);

            if (miFun.booOcurrioError == true)
            {
                dtLista = null;
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
                return;
            }
            dtLista1 = miFun.dtLista1;
            return;
        }
        public void Consulta25(int n_IdEmpresa, int n_IdTipDoc, string c_FechaVenta)
        {
            CD_vta_ventas miFun = new CD_vta_ventas();
            miFun.mysConec = mysConec;

            miFun.Consulta25(n_IdEmpresa, n_IdTipDoc, c_FechaVenta);

            if (miFun.booOcurrioError == true)
            {
                dtLista = null;
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
                return;
            }
            dtLista1 = miFun.dtLista1;
            return;
        }
        public void Consulta26(int n_IdEmpresa, int n_IdCliente, string c_CadenaIN)
        {
            CD_vta_ventas miFun = new CD_vta_ventas();
            miFun.mysConec = mysConec;

            miFun.Consulta26(n_IdEmpresa, n_IdCliente, c_CadenaIN);

            if (miFun.booOcurrioError == true)
            {
                dtLista = null;
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
                return;
            }
            dtLista1 = miFun.dtLista1;
            return;
        }
        public void Consulta27(int n_IdVenta)
        {
            CD_vta_ventas miFun = new CD_vta_ventas();
            miFun.mysConec = mysConec;

            miFun.Consulta27(n_IdVenta);

            if (miFun.booOcurrioError == true)
            {
                dtLista = null;
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
                return;
            }
            dtLista1 = miFun.dtLista1;
            return;
        }
        public DataTable BuscarDocumentoParaNotaCredito(int n_IdEmpresa, int n_IdCliente, string c_CadenaIN)
        {
            string[,] arrCabeceraDg1 = new string[5, 4];
            DataTable dtResult = new DataTable();
            string c_CampoBuscar = "n_id";
            string c_CadenaFiltro = "";

            Consulta26(n_IdEmpresa, n_IdCliente, c_CadenaIN);

            Helper.Genericas funDatos = new Helper.Genericas();

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
            arrCabeceraDg1[3, 3] = "n_imptotven";

            arrCabeceraDg1[4, 0] = "Id";
            arrCabeceraDg1[4, 1] = "0";
            arrCabeceraDg1[4, 2] = "N";
            arrCabeceraDg1[4, 3] = "n_id";

            Helper.Genericas xFun = new Helper.Genericas();
            xFun.Buscar_CampoBusqueda = c_CampoBuscar;
            xFun.Buscar_CadFiltro = c_CadenaFiltro;
            dtResult = xFun.Buscar(arrCabeceraDg1, dtLista1);

            return dtResult;
        }
        public void CambiarDocVentaAProforma(int n_IdDocumento, int n_IdEmpresa, int n_IdTipDocConvertir, string c_NumeroSerie, ref string c_NumeroDocumento)
        {
            string c_numdoc = "";
            CD_vta_ventas miFun = new CD_vta_ventas();
            CN_sun_tipdoccom objTipDoc = new CN_sun_tipdoccom();
            miFun.mysConec = mysConec;
            objTipDoc.mysConec = mysConec;

            c_numdoc = objTipDoc.UltimoNumero(n_IdEmpresa, n_IdTipDocConvertir, c_NumeroSerie);

            c_NumeroDocumento = c_NumeroSerie + "-" + c_numdoc;
            miFun.CambiarDocVentaAProforma(n_IdDocumento, n_IdTipDocConvertir, c_NumeroSerie, c_numdoc);
            if (miFun.booOcurrioError == true)
            {
                c_NumeroDocumento = "";
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
                return;
            }
            return;
        }
        public void Consulta28(int n_IdEmpresa, string c_FechaInicio, string c_FechaFinal, int n_IdLocal)
        {
            CD_vta_ventas miFun = new CD_vta_ventas();
            miFun.mysConec = mysConec;

            miFun.Consulta28(n_IdEmpresa, c_FechaInicio, c_FechaFinal, n_IdLocal);

            if (miFun.booOcurrioError == true)
            {
                dtLista = null;
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
                return;
            }
            dtLista1 = miFun.dtLista1;
            return;
        }
        public void ActivarDocumento(int n_IdRegistro)
        {
            CD_vta_ventas miFun = new CD_vta_ventas();

            DialogResult Rpta = MessageBox.Show("¿ Desea activar este documento ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (DialogResult.Yes == Rpta)
            {
                miFun.mysConec = mysConec;

                if (miFun.ActivarDocumento(n_IdRegistro) == false)
                {
                    MessageBox.Show("¿ No se pudo activar el documento por el siguiente motivo: " + miFun.StrErrorMensaje + " ?", "", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                }
            }
            if (miFun.IntErrorNumber != 0)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
        }
    }
}
