using MySql.Data.MySqlClient;
using SIAC_DATOS.Estacionamiento;
using SIAC_DATOS.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using SIAC_Entidades.Estacionamiento;
using SIAC_Entidades.Ventas;
using SIAC_Entidades.Contabilidad;
using iTextSharp.text.pdf;
using System.Drawing;
using Helper;
using System.Drawing.Printing;

namespace SIAC_Negocio.Estacionamiento
{
    public class CN_est_movimientos
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public int n_IdRegistro = 0;
        public bool b_GENERARASIENTO = false;                                  // INDICA SI SE GENERARA EL ASIENTO CONTABLE DE LA OPERACION
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA;
        public BE_EST_MOVIMIENTOS e_Movimiento = new BE_EST_MOVIMIENTOS();
        public DataTable dtListar = new DataTable();

        public BE_VTA_VENTAS e_Documento = new BE_VTA_VENTAS();
        public List<BE_VTA_VENTASDET> l_DocumentoDet = new List<BE_VTA_VENTASDET>();
        public List<BE_VTA_VENTASDOC> l_DetDoc = new List<BE_VTA_VENTASDOC>();
        public List<BE_VTA_VENTASOCT> l_DetOCT = new List<BE_VTA_VENTASOCT>();
        public List<BE_VTA_VENTASDAT> l_DetDat = new List<BE_VTA_VENTASDAT>();
        public List<BE_CON_DIARIO> l_Diario = new List<BE_CON_DIARIO>();

        CD_est_movimientos miFun;

        public List<BE_EST_MOVIMIENTOSDET> l_movdet = new List<BE_EST_MOVIMIENTOSDET>();
         public CN_est_movimientos(SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA)
        {
            CD_est_movimientos xFun = new CD_est_movimientos(STU_SISTEMA.BD_IP, STU_SISTEMA.BD_NOMBASEDATOS, STU_SISTEMA.BD_USUARIO, STU_SISTEMA.BD_CONTRASEÑA, STU_SISTEMA.BD_PUERTO);
            miFun = xFun;
        }

         ~CN_est_movimientos()
        {
            miFun = null;
            e_Movimiento = null;
            e_Documento = null;
            l_DocumentoDet = null;
            l_DetDoc = null;
            l_DetOCT = null;
            l_DetDat = null;
            l_Diario = null;
        }
        public void Listar(int n_IdEmpresa, int n_idLocal, string c_FechaMovimientos)
        {
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

            miFun.TraerRegistro(n_IdRegistro);
            dtListar = miFun.dtListar;

            if (dtListar.Rows.Count != 0)
            {
                Helper.Comunes.Funciones funGen = new Helper.Comunes.Funciones();

                e_Movimiento.n_idemp = Convert.ToInt16(dtListar.Rows[0]["n_idemp"]);
                e_Movimiento.n_idloc = Convert.ToInt16(dtListar.Rows[0]["n_idloc"]);
                e_Movimiento.n_id = Convert.ToInt32(dtListar.Rows[0]["n_id"]);
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

            booOk = miFun.Eliminar(n_IdRegistro);
            if (booOk == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;

            }
            return booOk;
        }
        public bool AnularPago(int n_IdRegistro, int n_IdDocVenta, int n_IdEmpresa)
        {
            bool booOk = false;

            booOk = miFun.AnularPago(n_IdRegistro, n_IdDocVenta, n_IdEmpresa);
            if (booOk == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;

            }
            return booOk;
        }
        public bool Anular(int n_IdRegistro, int n_IdDocumentoVenta)
        {
            bool booOk = false;

            booOk = miFun.Anular(n_IdRegistro, n_IdDocumentoVenta);
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
            bool booOk = false;

            miFun.l_movdet = l_movdet;
            booOk = miFun.Insertar(e_Movimientos);
            n_IdRegistro = miFun.n_IdRegistro;
            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_EST_MOVIMIENTOS e_Movimientos, bool b_GenerarCargo)
        {
            bool booOk = false;

            miFun.e_Documento = e_Documento;
            miFun.l_DocumentoDet = l_DocumentoDet;
            miFun.l_DetDoc = l_DetDoc;
            miFun.l_DetOCT = l_DetOCT;
            miFun.l_DetDat = l_DetDat;
            miFun.l_Diario = l_Diario;
            miFun.l_movdet = l_movdet;
            miFun.b_GENERARASIENTO = b_GENERARASIENTO;

            booOk = miFun.Actualizar(e_Movimientos, b_GenerarCargo);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public void ImprimirTicket(int n_IdRegistro, string c_Dato, int n_VistaPrevia)
        {
            miFun.ImprimirTicket(n_IdRegistro);
            dtListar = miFun.dtListar;
            if (dtListar == null) { return; }
            if (dtListar.Rows.Count != 0)
            {
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrintPage += PrintDocumentOnPrintPage2;
                Cls_Printer p1 = new Cls_Printer();
                p1.N_NUMEROCOPIAS = 1;
                p1.printDocument = printDocument;
                if (n_VistaPrevia == 2)
                {
                    p1.Print_VistaPrevia();
                }
                else
                {
                    p1.Print_Imprimir();
                }

            }

            //string c_NomArchivo = "";
            //string c_Ruta = "";
            //string[,] arrPara = new string[2, 3];

            //arrPara[0, 0] = "n_id";
            //arrPara[0, 1] = "N";
            //arrPara[0, 2] = n_IdRegistro.ToString();

            //arrPara[1, 0] = "rutaimagen";
            //arrPara[1, 1] = "C";
            //arrPara[1, 2] = CodigoBarra(n_IdRegistro, c_Dato).ToString();

            //c_NomArchivo = "Rpt_Ticket.rpt";

            //c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "estacionamientos\\" + c_NomArchivo;

            //Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            //xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            //xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            //xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            //xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            //if (n_VistaPrevia == 1) { xVisor.b_VisPrev = false; }
            //if (n_VistaPrevia == 2) { xVisor.b_VisPrev = true; }
            //xVisor.c_Titulo = "ESTACIONAMIENTO - IMPRESION TICKET DE INGRESO";
            //xVisor.c_PathRep = c_Ruta;
            //xVisor.arrParametros = arrPara;
            //xVisor.b_Exportar = false;
            //xVisor.c_NombreArchivoExportar = "";
            //xVisor.VerCrystal();
        }
        private void PrintDocumentOnPrintPage2(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Cls_Printer p = new Cls_Printer();
            string c_NomEmpresa = dtListar.Rows[0]["c_nomemp"].ToString();
            string c_RUC = dtListar.Rows[0]["c_numdoccli"].ToString();
            string c_dir2 = dtListar.Rows[0]["c_diremp"].ToString();

            string c_local = dtListar.Rows[0]["c_locdes"].ToString();
            string c_dir1 = dtListar.Rows[0]["c_dir"].ToString();

            string c_nomcli = dtListar.Rows[0]["c_clides"].ToString();
            string c_cajero = dtListar.Rows[0]["c_cajusu"].ToString();
            string c_placa = dtListar.Rows[0]["c_numpla"].ToString();
            string c_fchemi = Convert.ToDateTime(dtListar.Rows[0]["d_fchdoc"]).ToString("dd/MM/yyyy");
            string c_tiempo = dtListar.Rows[0]["c_horini"].ToString();
            string c_servicio = dtListar.Rows[0]["c_serdes"].ToString(); ;
            string c_glosa = dtListar.Rows[0]["c_gloserv"].ToString();

            string c_dato = dtListar.Rows[0]["c_numpla"].ToString();
            //c_dato = c_dato + "|" + dtListar.Rows[0]["c_clides"].ToString();
            //c_dato = c_dato + "|" + Convert.ToDateTime(dtListar.Rows[0]["d_fchdoc"]).ToString("dd/MM/yyyy");
            //c_dato = c_dato + "|" + dtListar.Rows[0]["c_horini"].ToString();
            //c_dato = c_dato + "|" + dtListar.Rows[0]["c_locdes"].ToString();
            //c_dato = c_dato + "|" + dtListar.Rows[0]["c_cajnom"].ToString();
            //c_dato = TxtNumPla.Text + "|" + TxtCliente.Text + "|" + DateTime.Now.ToString("dd/MM/yyyy") + "|" + TxtHorIni.Text + "|" + CboLocal.Text + "|" + CboCajero.Text;

            int n_IdRegistro = Convert.ToInt32(dtListar.Rows[0]["n_id"]);
            string c_archivo = CodigoBarra2(c_dato).ToString();

            int n_altocampo = 16;
            int n_altoseparacion = 14;
            Color o_colorlinea = Color.Transparent;
            int n_tam1 = 11-2;
            int n_tam2 = 10-2;
            int n_tam3 = 9-2;
            int n_tam4 = 8-2;

            p.e = e;

            //p.drawFont = new Font("Arial", n_tam1);
            //p.Print_TextoCuadro(c_NomEmpresa, 10, 30, 280, 20, 1, Color.Black, o_colorlinea);
            //p.Print_TextoCuadro(c_RUC, 10, 46, 280, 20, 1, Color.Black, o_colorlinea);
            //p.drawFont = new Font("Arial", n_tam3);
            //p.Print_TextoCuadro(c_dir1, 10, 60, 280, 20, 1, Color.Black, o_colorlinea);

            p.drawFont = new Font("Arial", n_tam1, FontStyle.Bold);
            p.Print_TextoCuadro(c_local, 10, 30, 300, 20, 1, Color.Black, o_colorlinea);

            p.drawFont = new Font("Arial", n_tam1);
            p.drawFont = new Font("Arial", n_tam3);
            p.Print_TextoCuadro(c_dir1, 10, 46, 280, 20, 1, Color.Black, o_colorlinea);

            p.drawFont = new Font("Arial", n_tam1, FontStyle.Bold);
            p.Print_TextoCuadro("TICKET DE INGRESO", 10, 60, 280, 20, 1, Color.Black, o_colorlinea);

            p.drawFont = new Font("Arial", n_tam3);
            int fila = 76;
            int n_ancho = 70;
            p.Print_TextoCuadro("Cliente", 10, fila, n_ancho, n_altocampo, 2, Color.Black, o_colorlinea);
            p.Print_TextoCuadro(":", 80, fila, 10, n_altocampo, 2, Color.Black, o_colorlinea);
            p.Print_TextoCuadroWrap(c_nomcli, 90, ref fila, 210, n_altocampo, 2, Color.Black, o_colorlinea);

            fila = fila + 16;
            p.Print_TextoCuadro("Cajero", 10, fila, n_ancho, n_altocampo, 2, Color.Black, o_colorlinea);
            p.Print_TextoCuadro(":", 80, fila, 10, n_altocampo, 2, Color.Black, o_colorlinea);
            p.Print_TextoCuadro(c_cajero, 90, fila, 210, n_altocampo, 2, Color.Black, o_colorlinea);

            fila = fila + 16;
            p.Print_Linea(10, fila, 310, fila, 3, Color.Black, 3);

            fila = fila + 5;
            
            p.Print_TextoCuadro("Nº Placa", 10, fila, n_ancho, n_altocampo, 2, Color.Black, o_colorlinea);
            p.Print_TextoCuadro(":", 80, fila, 10, n_altocampo, 2, Color.Black, o_colorlinea);

            p.drawFont = new Font("Arial", n_tam1);
            p.Print_TextoCuadro(c_placa, 90, fila, 210, n_altocampo, 2, Color.Black, o_colorlinea);

            p.drawFont = new Font("Arial", n_tam3);
            fila = fila + n_altoseparacion;
            p.Print_TextoCuadro("Fch Emi.", 10, fila, n_ancho, n_altocampo, 2, Color.Black, o_colorlinea);
            p.Print_TextoCuadro(":", 80, fila, 10, n_altocampo, 2, Color.Black, o_colorlinea);
            p.Print_TextoCuadro(c_fchemi, 90, fila, 210, n_altocampo, 2, Color.Black, o_colorlinea);

            fila = fila + n_altoseparacion;
            p.Print_TextoCuadro("Hota Ing.", 10, fila, n_ancho, n_altocampo, 2, Color.Black, o_colorlinea);
            p.Print_TextoCuadro(":", 80, fila, 10, n_altocampo, 2, Color.Black, o_colorlinea);
            p.Print_TextoCuadro(c_tiempo, 90, fila, 210, n_altocampo, 2, Color.Black, o_colorlinea);

            fila = fila + n_altoseparacion;
            p.Print_TextoCuadro("Servicio", 10, fila, n_ancho, n_altocampo, 2, Color.Black, o_colorlinea);
            p.Print_TextoCuadro(":", 80, fila, 10, n_altocampo, 2, Color.Black, o_colorlinea);

            fila = fila + n_altoseparacion;
            p.Print_TextoCuadroWrap(c_servicio, 10, ref fila, 210, n_altocampo, 2, Color.Black, o_colorlinea);

            fila = fila + 5;
            p.Print_Linea(10, fila, 310, fila, 3, Color.Black, 3);

            fila = fila + 10;
            p.Print_TextoCuadroWrap(c_glosa, 10, ref fila, 210, n_altocampo, 2, Color.Black, o_colorlinea);

            fila = fila + 10;
            p.Print_Imagen(c_archivo, 80, fila, 150, 60, 1, Color.Transparent, 1);

            string c_datos = "Tolerancia 5 minutos pasada la hora";
            fila = fila + 70;
            p.drawFont = new Font("Arial", n_tam2);
            p.Print_TextoCuadro(c_datos, 10, fila, 280, n_altocampo, 2, Color.Black, o_colorlinea);
        } 
        public void ImprimirComprobantePago(int n_IdEmpresa, int n_IdRegistro, string c_Dato, int n_TipDocumento, int n_VistaPrevia, short n_NumeroCopias)
        {
            miFun.ImprimirVenta(n_IdEmpresa, n_IdRegistro);
            dtListar = miFun.dtListar;
            if (dtListar == null) { return; }
            if (dtListar.Rows.Count != 0)
            {
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrintPage += PrintDocumentOnPrintPage;
                Cls_Printer p1 = new Cls_Printer();
                p1.N_NUMEROCOPIAS = n_NumeroCopias;
                p1.printDocument = printDocument;
                if (n_VistaPrevia == 2)
                {
                    p1.Print_VistaPrevia();
                }
                else
                {
                    p1.Print_Imprimir();
                }
            }
        }
        public void ExportarPdf(int n_IdEmpresa, int n_IdRegistro)
        {
            miFun.ImprimirVenta(n_IdEmpresa, n_IdRegistro);
            dtListar = miFun.dtListar;
            if (dtListar == null) { return; }
            if (dtListar.Rows.Count != 0)
            {
                PrintDocument printDocument = new PrintDocument();
                printDocument.PrintPage += PrintDocumentOnPrintPage;
                Cls_Printer p1 = new Cls_Printer();
                p1.N_NUMEROCOPIAS = 1;
                p1.printDocument = printDocument;
                p1.Print_ExportarPDF();
            }
        }
        private void PrintDocumentOnPrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Helper.Comunes.Funciones fun = new Helper.Comunes.Funciones();
            Cls_Printer p = new Cls_Printer();

            string c_correo = dtListar.Rows[0]["c_corctaven"].ToString();;
            string c_NomEmpresa = dtListar.Rows[0]["c_nomemp"].ToString();
            string c_NomLocal = dtListar.Rows[0]["c_locdes"].ToString();
            string c_RUC = dtListar.Rows[0]["c_numdoc1"].ToString();
            string c_dir1 = "D.F. : " + dtListar.Rows[0]["c_dir"].ToString();
            string c_dir2 = dtListar.Rows[0]["c_locdir"].ToString();
            string c_tipdoc = dtListar.Rows[0]["c_desabr"].ToString();
            string c_numdoc = "Nº " + dtListar.Rows[0]["c_numdoc"].ToString();
            string c_ruccli = dtListar.Rows[0]["c_clinumdoc"].ToString();
            string c_nomcli = dtListar.Rows[0]["c_clinom"].ToString();
            string c_fchemi = Convert.ToDateTime(dtListar.Rows[0]["d_fchdoc"]).ToString("dd/MM/yyyy");
            string c_cajero = dtListar.Rows[0]["c_cajnom"].ToString();
            string c_plasoc = dtListar.Rows[0]["c_plasoc"].ToString();

            string c_placa = dtListar.Rows[0]["c_numpla"].ToString();
            string c_horini = fun.NulosC(dtListar.Rows[0]["c_horini"]).ToString();
            string c_horfin = fun.NulosC(dtListar.Rows[0]["c_horfin"]).ToString();
            string c_tiempo = fun.NulosC(dtListar.Rows[0]["c_tiempousu"]).ToString();
            string c_impbru = Convert.ToDouble(dtListar.Rows[0]["n_impbru"]).ToString("0.00");
            string c_impigv = Convert.ToDouble(dtListar.Rows[0]["n_impigv"]).ToString("0.00");
            string c_total = Convert.ToDouble(dtListar.Rows[0]["n_imptotven"]).ToString("0.00");
            string c_totallet = dtListar.Rows[0]["c_numlet"].ToString();

            string c_dato = dtListar.Rows[0]["c_numpla"].ToString();
            c_dato = c_dato + "|" + dtListar.Rows[0]["c_desabr"].ToString();
            c_dato = c_dato + "|" + dtListar.Rows[0]["c_numdoc"].ToString();
            c_dato = c_dato + "|" + dtListar.Rows[0]["c_clinom"].ToString();
            c_dato = c_dato + "|" + Convert.ToDateTime(dtListar.Rows[0]["d_fchdoc"]).ToString("dd/MM/yyyy");
            c_dato = c_dato + "|" + Convert.ToDouble(dtListar.Rows[0]["n_imptotven"]).ToString("0.00");
            c_dato = c_dato + "|" + fun.NulosC(dtListar.Rows[0]["c_horini"]).ToString();
            c_dato = c_dato + "|" + fun.NulosC(dtListar.Rows[0]["c_horfin"]).ToString();
            c_dato = c_dato + "|" + dtListar.Rows[0]["c_locdes"].ToString();
            c_dato = c_dato + "|" + dtListar.Rows[0]["c_cajnom"].ToString();
            //c_dato = TxtNumPla2.Text + "|" + TxtCliente2.Text + "|" + DateTime.Now.ToString("dd/MM/yyyy") + "|" + TxtHorIni2.Text + "|" + TxtHorFin2.Text + "|" + CboLocal.Text + "|" + CboCajero.Text + "|" + TxtNumDoc.Text + "-" + TxtNumDoc.Text + "|" + TxtTotPag.Text + "|" + CboTipDoc.Text;

            int n_IdRegistro = Convert.ToInt32(dtListar.Rows[0]["n_id"]);
            string c_archivo = CodigoBarra(n_IdRegistro, c_dato).ToString();

            int n_altocampo = 18-3;
            int n_altoseparacion = 20-6;
            Color o_colorlinea = Color.Transparent;
            int n_dat = 66;
            int n_tam_1 = 11-2;
            int n_tam_2 = 10-2;
            int n_tam_3 = 9-2;
            int n_tam_4 = 8-2;

            p.e = e;
            p.drawFont = new Font("Arial", n_tam_1);
            p.Print_TextoCuadro(c_NomEmpresa, 10, 30, 270, 16, 1, Color.Black, o_colorlinea);
            p.Print_TextoCuadro(c_RUC, 10, 46, 270, 16, 1, Color.Black, o_colorlinea);

            p.drawFont = new Font("Arial", n_tam_3);
            p.Print_TextoCuadro(c_dir1, 10, 60, 270, 16, 1, Color.Black, o_colorlinea);
            //p.Print_TextoCuadro(c_dir1, 10, ref n_dat, 270, 16, 1, Color.Black, o_colorlinea);

            p.drawFont = new Font("Arial", n_tam_2, FontStyle.Bold);
            p.Print_TextoCuadro(c_NomLocal, 10, n_dat + 5, 270, 16, 1, Color.Black, o_colorlinea);
            p.drawFont = new Font("Arial", n_tam_3);
            p.Print_TextoCuadro(c_dir2, 10, n_dat + 20, 270, 16, 1, Color.Black, o_colorlinea);

            p.drawFont = new Font("Arial", n_tam_2, FontStyle.Bold);
            p.Print_TextoCuadro(c_tipdoc, 10, 105, 270, 16, 1, Color.Black, o_colorlinea);
            p.drawFont = new Font("Arial", n_tam_2);
            p.Print_TextoCuadro(c_numdoc, 10, 120, 270, 16, 1, Color.Black, o_colorlinea);

            p.drawFont = new Font("Arial", n_tam_3);
            int fila = 138;
            int n_ancho = 70;
            p.Print_TextoCuadro("RUC / DNI", 10, fila, n_ancho, n_altocampo, 2, Color.Black, o_colorlinea);
            p.Print_TextoCuadro(":", 80, fila, 10, n_altocampo, 2, Color.Black, o_colorlinea);
            p.Print_TextoCuadro(c_ruccli, 90, fila, 220, n_altocampo, 2, Color.Black, o_colorlinea);

            fila = fila + n_altoseparacion;
            p.Print_TextoCuadro("Nombre", 10, fila, n_ancho, n_altocampo, 2, Color.Black, o_colorlinea);
            p.Print_TextoCuadro(":", 80, fila, 10, n_altocampo, 2, Color.Black, o_colorlinea);
            p.Print_TextoCuadroWrap(c_nomcli, 90, ref fila, 220, n_altocampo, 2, Color.Black, o_colorlinea);

            fila = fila + 10;
            p.Print_TextoCuadro("Fch. Emi.", 10, fila, n_ancho, n_altocampo, 2, Color.Black, o_colorlinea);
            p.Print_TextoCuadro(":", 80, fila, 10, n_altocampo, 2, Color.Black, o_colorlinea);
            p.Print_TextoCuadro(c_fchemi, 90, fila, 220, n_altocampo, 2, Color.Black, o_colorlinea);

            //p.Print_TextoCuadro("Hora Emi.", 150, fila, n_ancho, n_altocampo, 2, Color.Black, Color.Black);
            //p.Print_TextoCuadro(":", 220, fila, 10, n_altocampo, 2, Color.Black, Color.Black);

            fila = fila + n_altoseparacion;
            p.Print_TextoCuadro("Cajero", 10, fila, n_ancho, n_altocampo, 2, Color.Black, o_colorlinea);
            p.Print_TextoCuadro(":", 80, fila, 10, n_altocampo, 2, Color.Black, o_colorlinea);
            p.Print_TextoCuadro(c_cajero, 90, fila, 220, n_altocampo, 2, Color.Black, o_colorlinea);

            fila = fila + n_altoseparacion;
            p.Print_TextoCuadro("Placa", 10, fila, n_ancho, n_altocampo, 2, Color.Black, o_colorlinea);
            p.Print_TextoCuadro(":", 80, fila, 10, n_altocampo, 2, Color.Black, o_colorlinea);
            p.Print_TextoCuadro(c_placa, 90, fila, 220, n_altocampo, 2, Color.Black, o_colorlinea);

            fila = fila + n_altoseparacion;
            p.Print_TextoCuadro("Ingreso", 10, fila, n_ancho, n_altocampo, 2, Color.Black, o_colorlinea);
            p.Print_TextoCuadro(":", 80, fila, 10, n_altocampo, 2, Color.Black, o_colorlinea);
            p.Print_TextoCuadro(c_horini, 90, fila, 220, n_altocampo, 2, Color.Black, o_colorlinea);

            fila = fila + n_altoseparacion;
            p.Print_TextoCuadro("Salida", 10, fila, n_ancho, n_altocampo, 2, Color.Black, o_colorlinea);
            p.Print_TextoCuadro(":", 80, fila, 10, n_altocampo, 2, Color.Black, o_colorlinea);
            p.Print_TextoCuadro(c_horfin, 90, fila, 220, n_altocampo, 2, Color.Black, o_colorlinea);

            fila = fila + n_altoseparacion;
            p.Print_TextoCuadro("Tiempo", 10, fila, n_ancho, n_altocampo, 2, Color.Black, o_colorlinea);
            p.Print_TextoCuadro(":", 80, fila, 10, n_altocampo, 2, Color.Black, o_colorlinea);
            p.Print_TextoCuadro(c_tiempo, 90, fila, 220, n_altocampo, 2, Color.Black, o_colorlinea);

            fila = fila + n_altoseparacion;
            p.Print_Linea(10, fila, 270, fila, 3, Color.Black, 3);

            int n_row = 0;
            string c_servicio = "";
            string c_imp = "";
            fila = fila + 7;

            for (n_row = 0; n_row <= dtListar.Rows.Count - 1; n_row++)
            {
                c_imp = Convert.ToDouble(dtListar.Rows[n_row]["n_detimptotigv"]).ToString("0.00");
                c_servicio = dtListar.Rows[n_row]["c_detdesprousu"].ToString();

                p.Print_TextoCuadro("Servicio", 10, fila, n_ancho, n_altocampo, 2, Color.Black, o_colorlinea);
                p.Print_TextoCuadro(":", 80, fila, 10, n_altocampo, 2, Color.Black, o_colorlinea);
                //o_colorlinea = Color.Black;
                p.Print_TextoCuadroWrap(c_servicio, 90, ref fila, 210, n_altocampo, 2, Color.Black, o_colorlinea);

                fila = fila + 5;
                p.Print_TextoCuadro("Importe", 10, fila, n_ancho, n_altocampo, 2, Color.Black, o_colorlinea);
                p.Print_TextoCuadro(":", 80, fila, 10, n_altocampo, 2, Color.Black, o_colorlinea);
                p.Print_TextoCuadro(c_imp, 90, fila, 210, n_altocampo, 2, Color.Black, o_colorlinea);

                fila = fila + n_altoseparacion;
            }

            fila = fila + 5;
            p.Print_Linea(10, fila, 270, fila, 3, Color.Black, 3);
            fila = fila + 7;
            int n_anchotot = 100;
            if (Convert.ToInt32(dtListar.Rows[0]["n_idtipdoc"]) == 2)
            {
                //fila = fila + n_altoseparacion;
                p.Print_TextoCuadro("Imp. Bruto  S/", 10, fila, n_anchotot, n_altocampo, 2, Color.Black, o_colorlinea);
                p.Print_TextoCuadro(":", 110, fila, 10, n_altocampo, 2, Color.Black, o_colorlinea);
                p.Print_TextoCuadro(c_impbru, 120, fila, 100, n_altocampo, 3, Color.Black, o_colorlinea);

                fila = fila + n_altoseparacion;
                p.Print_TextoCuadro("IGV (18 %)  S/", 10, fila, n_anchotot, n_altocampo, 2, Color.Black, o_colorlinea);
                p.Print_TextoCuadro(":", 110, fila, 10, n_altocampo, 2, Color.Black, o_colorlinea);
                p.Print_TextoCuadro(c_impigv, 120, fila, 100, n_altocampo, 3, Color.Black, o_colorlinea);

                fila = fila + n_altoseparacion;
            }

            //fila = fila + n_altoseparacion;
            p.Print_TextoCuadro("Total   S/", 10, fila, n_anchotot, n_altocampo, 2, Color.Black, o_colorlinea);
            p.Print_TextoCuadro(":", 110, fila, 10, n_altocampo, 2, Color.Black, o_colorlinea);
            p.Print_TextoCuadro(c_total, 120, fila, 100, n_altocampo, 3, Color.Black, o_colorlinea);

            fila = fila + n_altoseparacion;
            p.Print_TextoCuadro("SON :", 10, fila, n_ancho, n_altocampo, 2, Color.Black, o_colorlinea);

            fila = fila + n_altoseparacion;
            p.Print_TextoCuadroWrap(c_totallet, 10, ref fila, 280, n_altocampo, 2, Color.Black, o_colorlinea);

            if (fun.NulosC(c_plasoc) != "")
            { 
                fila = fila + n_altoseparacion;
                c_plasoc = "Ref. a placas: " + c_plasoc;
                p.Print_TextoCuadroWrap(c_plasoc, 10, ref fila, 280, n_altocampo, 2, Color.Black, o_colorlinea);
            }
            
            fila = fila + n_altoseparacion;
            //p.Print_Imagen(@"C:\\SSF-NET\\tmp\\101F0010000000002.bmp", 80, fila, 170, 80, 1, Color.Transparent, 1);
            p.Print_Imagen(c_archivo, 70, fila, 150, 60, 1, Color.Transparent, 1);

            p.drawFont = new Font("Arial", n_tam_3);
            if (c_tipdoc == "VALE")
            {
                c_dato = "Este documento provicional podra ser canjeado por una Factura o Boleta dentro del mes";
                p.Print_TextoCuadro(c_dato, 1, fila + 65, 270, 16, 1, Color.Black, o_colorlinea);
            }
            else
            {
                c_dato = "El documento electronico puede ser consultado en Sunat Virtual www.sunat.gob.pe, opciones sin clave SOL, consulta validez de CPE";
                fila = fila + 65;
                p.Print_TextoCuadro(c_dato, 1, fila, 270, 32, 1, Color.Black, o_colorlinea);
             
                c_dato = "Solicitar el archivo .pdf y .xml al siguiente correo: " + c_correo;
                fila = fila + 35;
                p.Print_TextoCuadro(c_dato, 1, fila, 270, 32, 1, Color.Black, o_colorlinea);
            }
        } 
        string CodigoBarra(int n_IdEmpresa, string c_Dato)
        {
            string c_nomarchivo = "";

            string texto = c_Dato;
            c_nomarchivo = DateTime.Now.ToString("hhmmss") +"_"+ n_IdEmpresa.ToString("00000000")+".bmp";
            BarcodePDF417 codigobarras = new BarcodePDF417();
            
            codigobarras.Options = BarcodePDF417.PDF417_USE_ASPECT_RATIO;
            
            codigobarras.ErrorLevel = 8;
            codigobarras.SetText(texto);
            System.Drawing.Bitmap bm = new Bitmap(codigobarras.CreateDrawingImage(Color.Black, Color.White));

            c_nomarchivo = @"c:\ssf-net\tmp\ticket\" + c_nomarchivo;
            Cls_IO o_io = new Cls_IO();
            o_io.Fil_EliminarArchivo(c_nomarchivo);
            bm.Save(c_nomarchivo);
            return c_nomarchivo;
        }
        string CodigoBarra2(string c_Dato)
        {
            string c_nomarchivo = "";

            string texto = c_Dato;
            c_nomarchivo = DateTime.Now.ToString("hhmmss") + "_" + c_Dato + ".bmp";
            //BarcodePDF417 codigobarras = new BarcodePDF417();
            Barcode128 codigobarras = new Barcode128();
            //codigobarras.Options = BarcodePDF417.PDF417_USE_ASPECT_RATIO;
            //codigobarras.ErrorLevel = 8;
            //codigobarras.SetText(texto);

            //codigobarras.TextAlignment = Element.ALIGN_CENTER;
            codigobarras.Code = c_Dato;
            codigobarras.StartStopText = false;
            codigobarras.CodeType = iTextSharp.text.pdf.Barcode128.EAN13;
            codigobarras.Extended = true;

            System.Drawing.Bitmap bm = new Bitmap(codigobarras.CreateDrawingImage(Color.Black, Color.White));

            c_nomarchivo = @"c:\ssf-net\tmp\ticket\" + c_nomarchivo;
            Cls_IO o_io = new Cls_IO();
            o_io.Fil_EliminarArchivo(c_nomarchivo);
            bm.Save(c_nomarchivo);
            return c_nomarchivo;
        }
        public void Consulta2(int n_IdEmpresa, int n_IdPlaya, int n_IdCaja, string c_FechaInicio, string c_FechaFinal)
        {
            miFun.Consulta2(n_IdEmpresa, n_IdPlaya, n_IdCaja, c_FechaInicio, c_FechaFinal);
            dtListar = miFun.dtListar;

            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return;
        }
    }
}
