using MySql.Data.MySqlClient;
using SIAC_DATOS.Estacionamiento;
using SIAC_DATOS.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using SIAC_Entidades.Estacionamiento;
using SIAC_Entidades.Ventas;
using iTextSharp.text.pdf;
using System.Drawing;

namespace SIAC_Negocio.Estacionamiento
{
    public class CN_est_movimientos
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public int n_IdRegistro = 0;

        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public BE_EST_MOVIMIENTOS e_Movimiento = new BE_EST_MOVIMIENTOS();
        public DataTable dtListar = new DataTable();

        public BE_VTA_VENTAS e_Documento = new BE_VTA_VENTAS();
        public List<BE_VTA_VENTASDET> l_DocumentoDet = new List<BE_VTA_VENTASDET>();
        public List<BE_VTA_VENTASDOC> l_DetDoc = new List<BE_VTA_VENTASDOC>();
        public List<BE_VTA_VENTASOCT> l_DetOCT = new List<BE_VTA_VENTASOCT>();

        public void Listar(int n_IdEmpresa, int n_idLocal, string c_FechaMovimientos)
        {
            CD_est_movimientos miFun = new CD_est_movimientos();
            miFun.mysConec = mysConec;

            miFun.Listar(n_IdEmpresa, n_idLocal, c_FechaMovimientos);
            dtListar = miFun.dtListar;

            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return;
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            DataTable dtResult = new DataTable();
            CD_est_movimientos miFun = new CD_est_movimientos();

            miFun.mysConec = mysConec;

            miFun.TraerRegistro(n_IdRegistro);
            dtListar = miFun.dtListar;

            if (dtListar.Rows.Count != 0)
            {
                Helper.Comunes.Funciones funGen = new Helper.Comunes.Funciones();

                e_Movimiento.n_idemp = Convert.ToInt16(dtListar.Rows[0]["n_idemp"]);
                e_Movimiento.n_idloc = Convert.ToInt16(dtListar.Rows[0]["n_idloc"]);
                e_Movimiento.n_id = Convert.ToInt16(dtListar.Rows[0]["n_id"]);
                e_Movimiento.n_idcaj = Convert.ToInt16(dtListar.Rows[0]["n_idcaj"]);
                e_Movimiento.n_idcli = Convert.ToInt16(dtListar.Rows[0]["n_idcli"]);
                e_Movimiento.d_fchdoc = Convert.ToDateTime(dtListar.Rows[0]["d_fchdoc"]);
                e_Movimiento.c_horini = dtListar.Rows[0]["c_horini"].ToString();
                e_Movimiento.c_horfin = dtListar.Rows[0]["c_horfin"].ToString();
                e_Movimiento.n_tieuti = Convert.ToInt16(dtListar.Rows[0]["n_tieuti"]);
                e_Movimiento.n_idser = Convert.ToInt16(dtListar.Rows[0]["n_idser"]);
                e_Movimiento.n_importe = Convert.ToDouble(dtListar.Rows[0]["n_importe"]);
                e_Movimiento.n_impser = Convert.ToDouble(funGen.NulosN(dtListar.Rows[0]["n_impser"]));
                e_Movimiento.c_tieuit = dtListar.Rows[0]["c_tieuit"].ToString();
                e_Movimiento.c_numpla = dtListar.Rows[0]["c_numpla"].ToString();
                e_Movimiento.c_numdoc = dtListar.Rows[0]["c_numdoc"].ToString();
                e_Movimiento.n_idtipdoc = Convert.ToInt16(dtListar.Rows[0]["n_idtipdoc"]);
                e_Movimiento.n_finalizado = Convert.ToInt16(dtListar.Rows[0]["n_finalizado"]);
            }
            if (b_OcurrioError == true)
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
            CD_est_movimientos miFun = new CD_est_movimientos();

            miFun.mysConec = mysConec;

            booOk = miFun.Eliminar(n_IdRegistro);
            if (booOk == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;

            }
            return booOk;
        }
        public bool Insertar(BE_EST_MOVIMIENTOS e_Movimientos)
        {
            CD_est_movimientos miFun = new CD_est_movimientos();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(e_Movimientos);
            n_IdRegistro = miFun.n_IdRegistro;
            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_EST_MOVIMIENTOS e_Movimientos, bool b_GenerarCargo)
        {
            CD_est_movimientos miFun = new CD_est_movimientos();
            bool booOk = false;

            miFun.mysConec = mysConec;

            miFun.e_Documento = e_Documento;
            miFun.l_DocumentoDet = l_DocumentoDet;
            miFun.l_DetDoc = l_DetDoc;
            miFun.l_DetOCT = l_DetOCT;

            booOk = miFun.Actualizar(e_Movimientos, b_GenerarCargo);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public void ImprimirTicket(int n_IdRegistro, string c_Dato)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[2, 3];

            arrPara[0, 0] = "n_id";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = n_IdRegistro.ToString();

            arrPara[1, 0] = "rutaimagen";
            arrPara[1, 1] = "C";
            arrPara[1, 2] = CodigoBarra(n_IdRegistro, c_Dato).ToString();

            c_NomArchivo = "Rpt_Ticket.rpt";

            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "estacionamientos\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "ESTACIONAMIENTO - IMPRESION TICKET DE INGRESO";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.b_Exportar = false;
            xVisor.c_NombreArchivoExportar = "";
            xVisor.VerCrystal();
        }
        public void ImprimirComprobantePago(int n_IdEmpresa, int n_IdRegistro, string c_Dato)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[3, 3];

            arrPara[0, 0] = "n_idemp";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = n_IdEmpresa.ToString();

            arrPara[1, 0] = "n_idvta";
            arrPara[1, 1] = "N";
            arrPara[1, 2] = n_IdRegistro.ToString();

            arrPara[2, 0] = "rutaimagen";
            arrPara[2, 1] = "C";
            arrPara[2, 2] = CodigoBarra(n_IdRegistro, c_Dato).ToString();

            c_NomArchivo = "Rpt_DocumentoPV.rpt";

            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "ventas\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "VENTAS - IMPRESION COMPROBANTE DE PAGO";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.b_Exportar = false;
            xVisor.c_NombreArchivoExportar = "";
            xVisor.VerCrystal();
        }
        string CodigoBarra(int n_IdEmpresa, string c_Dato)
        {
            string c_nomarchivo = "";

            string texto = c_Dato;
            c_nomarchivo = n_IdEmpresa.ToString("00000000")+".bmp";
            BarcodePDF417 codigobarras = new BarcodePDF417();
            codigobarras.Options = BarcodePDF417.PDF417_USE_ASPECT_RATIO;
            codigobarras.ErrorLevel = 8;
            codigobarras.SetText(texto);
            System.Drawing.Bitmap bm = new Bitmap(codigobarras.CreateDrawingImage(Color.Black, Color.White));

            c_nomarchivo = @"c:\ssf-net\tmp\ticket\" + c_nomarchivo + ".bmp";

            bm.Save(c_nomarchivo);
            return c_nomarchivo;
        }
    }
}
