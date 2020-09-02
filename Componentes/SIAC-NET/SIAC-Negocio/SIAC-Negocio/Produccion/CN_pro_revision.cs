using MySql.Data.MySqlClient;
using SIAC_DATOS.Almacen;
using SIAC_DATOS.Produccion;
using SIAC_DATOS.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using SIAC_Entidades.Produccion;
using System.Data.OleDb;
using System.Diagnostics;

namespace SIAC_Negocio.Produccion
{
    public class CN_pro_revision
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public OleDbConnection AccConec = new OleDbConnection();

        public DataTable Listar(int n_idempresa, int n_idmes, int n_idano)
        {
            DataTable dtResul = new DataTable();

            CD_pro_revision miFun = new CD_pro_revision();
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
        public BE_PRO_REVISION TraerRegistro(Int64 n_IdRegistro)
        {
            BE_PRO_REVISION EntCabecera = new BE_PRO_REVISION();
            bool booResult;
            DataTable DtDetalle = new DataTable();
            DataTable DtResultado = new DataTable();
            CD_pro_revision miFun = new CD_pro_revision();
            
            miFun.mysConec = mysConec;

            booResult = miFun.TraerRegistro(n_IdRegistro);                        
            DtResultado = miFun.dtRevision;

            if (DtResultado.Rows.Count != 0)
            {
                EntCabecera.n_idemp = Convert.ToInt32(DtResultado.Rows[0]["n_idemp"].ToString());
                EntCabecera.n_id = Convert.ToInt32(DtResultado.Rows[0]["n_id"].ToString());
                EntCabecera.n_idpro = Convert.ToInt32(DtResultado.Rows[0]["n_idpro"].ToString());
                EntCabecera.n_idite = Convert.ToInt32(DtResultado.Rows[0]["n_idite"].ToString());
                EntCabecera.n_idrec = Convert.ToInt32(DtResultado.Rows[0]["n_idrec"].ToString());
                EntCabecera.n_idtipdoc = Convert.ToInt32(DtResultado.Rows[0]["n_idtipdoc"].ToString());
                EntCabecera.c_numser = DtResultado.Rows[0]["c_numser"].ToString();
                EntCabecera.c_numdoc = DtResultado.Rows[0]["c_numdoc"].ToString();
                EntCabecera.c_numlot = DtResultado.Rows[0]["c_numlot"].ToString();
                EntCabecera.n_canpro = Convert.ToDouble(DtResultado.Rows[0]["n_canpro"].ToString());
                EntCabecera.n_idunimed = Convert.ToInt32(DtResultado.Rows[0]["n_idunimed"].ToString());
                EntCabecera.d_fchini = Convert.ToDateTime(DtResultado.Rows[0]["d_fchini"].ToString());
                EntCabecera.d_fchfin = Convert.ToDateTime(DtResultado.Rows[0]["d_fchfin"].ToString());
                EntCabecera.h_horini = DtResultado.Rows[0]["h_horini"].ToString();
                EntCabecera.h_horfin = DtResultado.Rows[0]["h_horfin"].ToString();
                EntCabecera.n_canprocon = Convert.ToDouble(DtResultado.Rows[0]["n_canprocon"].ToString());
                EntCabecera.n_canpronocon = Convert.ToDouble(DtResultado.Rows[0]["n_canpronocon"].ToString());
                EntCabecera.c_obsprocon = DtResultado.Rows[0]["c_obsprocon"].ToString();
                EntCabecera.c_obspronocon = DtResultado.Rows[0]["c_obspronocon"].ToString();
                EntCabecera.n_idperrev = Convert.ToInt32(DtResultado.Rows[0]["n_idperrev"].ToString());
                EntCabecera.d_fchrev = Convert.ToDateTime(DtResultado.Rows[0]["d_fchrev"].ToString());
                EntCabecera.h_horrev = DtResultado.Rows[0]["h_horrev"].ToString();
                EntCabecera.n_iddocref = Convert.ToInt32(DtResultado.Rows[0]["n_iddocref"].ToString());
                EntCabecera.c_numdocref = DtResultado.Rows[0]["c_numdocref"].ToString();
                EntCabecera.n_idtipdocref = Convert.ToInt32(DtResultado.Rows[0]["n_idtipdocref"].ToString());
            }

            return EntCabecera;
        }
        public bool Eliminar(int n_IdRegistro, int n_IdProduccion)
        {
            bool booResult = false;
            CD_pro_revision miFun = new CD_pro_revision();
            miFun.mysConec = mysConec;

            booResult = miFun.Eliminar(n_IdRegistro, n_IdProduccion);
            if (booResult == false)
            {
                booOcurrioError = false;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return booResult;
        }
        public bool Insertar(BE_PRO_REVISION entRevision)
        {
            bool booOk = false;
            CD_pro_revision miFun = new CD_pro_revision();
            miFun.mysConec = mysConec;

            booOk = miFun.Insertar(entRevision);
            if (booOk == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return booOk;
        }
        public bool Actualizar(BE_PRO_REVISION entRevision)
        {
            bool booOk = false;
            CD_pro_revision miFun = new CD_pro_revision();
            miFun.mysConec = mysConec;

            booOk = miFun.Actualizar(entRevision);

            if (booOk == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return booOk;
        }
        public DataTable ConsultaRevisionPendientes(int n_idempresa)
        {
            DataTable dtResul = new DataTable();

            CD_pro_revision miFun = new CD_pro_revision();
            miFun.mysConec = mysConec;

            dtResul = miFun.ConsultaRevisionPendientes(n_idempresa);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public void ReporteProdNoConforme(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo, int n_Tipo)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[8, 3];

            arrPara[0, 0] = "n_idemp";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = n_IdEmpresa.ToString();

            arrPara[1, 0] = "n_anotra";
            arrPara[1, 1] = "N";
            arrPara[1, 2] = n_AnoTrabajo.ToString();

            arrPara[2, 0] = "n_mestra";
            arrPara[2, 1] = "N";
            arrPara[2, 2] = n_MesTrabajo.ToString();

            arrPara[3, 0] = "c_nomemp";
            arrPara[3, 1] = "C";
            arrPara[3, 2] = STU_SISTEMA.EMPRESANOMBRE;

            arrPara[4, 0] = "c_numruc";
            arrPara[4, 1] = "C";
            arrPara[4, 2] = STU_SISTEMA.EMPRESARUC;

            arrPara[5, 0] = "c_titulo1";
            arrPara[5, 1] = "C";
            if (n_Tipo == 1) { arrPara[5, 2] = "REPORTE DE PRODUCTOS CONFORMES"; }
            if (n_Tipo == 2) { arrPara[5, 2] = "REPORTE DE PRODUCTOS NO CONFORMES"; }

            arrPara[6, 0] = "c_titulo2";
            arrPara[6, 1] = "C";
            arrPara[6, 2] = "MES DE " + STU_SISTEMA.MESTRABAJO.ToString() + " DEL " + STU_SISTEMA.ANOTRABAJO.ToString();

            arrPara[7, 0] = "n_tipo";
            arrPara[7, 1] = "N";
            arrPara[7, 2] = n_Tipo.ToString();

            c_NomArchivo = "RptProdNoConforme.rpt";
            //c_Ruta = @"j:\ssf-net\reportes\produccion\" + c_NomArchivo;
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "produccion\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            if (n_Tipo == 1) { xVisor.c_Titulo = "PRODUCCION - PRODUCTOS CONFORMES"; }
            if (n_Tipo == 2) { xVisor.c_Titulo = "PRODUCCION - PRODUCTOS NO CONFORMES"; }
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
        public void ParteProduccion(int n_IdRegistro)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[1, 3];

            arrPara[0, 0] = "n_id";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = n_IdRegistro.ToString();

            c_NomArchivo = "RptParteProduccion2.rpt";
            
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "produccion\\" + c_NomArchivo;
            //c_Ruta = "D:\\Users\\jchac\\Source\\Repos\\App\\ssf-net\\SSF-NET-Produccion\\SSF-NET-Produccion\\Reportes\\" + c_NomArchivo;

            //string cs = "SSF";
            //if (!EventLog.SourceExists(cs)) { 
            //    EventLog.CreateEventSource(cs, cs); 
            //}
            //EventLog.WriteEntry(cs, c_Ruta, EventLogEntryType.Information);

            //var eventSource = "SSF";
            //if (!EventLog.SourceExists(eventSource))
            //{
            //    EventLog.CreateEventSource(eventSource, "Application");
            //}
            //using (EventLog eventLog = new EventLog("Application"))
            //{
            //    eventLog.Source = "Application";
            //    var message = string.Format("SSF - Ruta Produccion: {0}", c_Ruta);
            //    eventLog.WriteEntry(message, EventLogEntryType.Information);
            //}

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "PRODUCCION - PARTE DE PRODUCCION";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
    }
}
