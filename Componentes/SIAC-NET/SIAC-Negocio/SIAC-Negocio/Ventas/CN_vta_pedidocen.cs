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
using Helper;
using SIAC_Objetos;

namespace SIAC_Negocio.Ventas
{
    public class CN_vta_pedidocen
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        
        string[]  a_CabeceraCEN;
        string[,] a_DetalleCEN;

        public DataTable dtLista = new DataTable();
        public MySqlConnection mysConec = new MySqlConnection();
        
        public BE_VTA_PEDIDOCEN entRegistro = new BE_VTA_PEDIDOCEN();
        public List<BE_VTA_PEDIDOCENDET> LstDetalle = new List<BE_VTA_PEDIDOCENDET>();
        
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public void Listar(int n_IdEmpresa, int n_IdMes, int n_AnoTrabajo)
        {
            DataTable dtResul = new DataTable();
            CD_vta_pedidocen miFun = new CD_vta_pedidocen();
            miFun.mysConec = mysConec;

            miFun.Listar(n_IdEmpresa, n_IdMes, n_AnoTrabajo);
            dtLista = miFun.dtLista;

            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return;
        }
        public bool TraerRegistro(int n_IdRegistro)
        {
            int n_row = 0;
            bool b_result = false;
            DataTable dtCab = new DataTable();
            DataTable dtDet = new DataTable();
            Helper.Genericas funfunciones = new Helper.Genericas();
            Helper.Comunes.Funciones funfun = new Helper.Comunes.Funciones();
            CD_vta_pedidocen miFun = new CD_vta_pedidocen();
            miFun.mysConec = mysConec;
            LstDetalle.Clear();
            miFun.TraerRegistro(n_IdRegistro);
            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                b_result = false;
            }
            dtCab = miFun.dtLista;
            dtDet = miFun.dtDetalle;

            entRegistro.n_idemp = Convert.ToInt32(dtCab.Rows[0]["n_idemp"]);
            entRegistro.n_id = Convert.ToInt32(dtCab.Rows[0]["n_id"]);
            entRegistro.c_codcli = dtCab.Rows[0]["c_codcli"].ToString();
            entRegistro.c_codpunven = dtCab.Rows[0]["c_codpunven"].ToString();
            entRegistro.c_codpunent = dtCab.Rows[0]["c_codpunent"].ToString();
            entRegistro.d_fchemi = Convert.ToDateTime(dtCab.Rows[0]["d_fchemi"]);
            entRegistro.d_fchent = Convert.ToDateTime(dtCab.Rows[0]["d_fchent"]);
            entRegistro.n_numite = Convert.ToInt32(dtCab.Rows[0]["n_numite"]);
            entRegistro.c_numped = dtCab.Rows[0]["c_numped"].ToString();
            entRegistro.n_idguides = Convert.ToInt32(funfun.NulosN(dtCab.Rows[0]["n_idguides"]));

            LstDetalle.Clear();
            for (n_row = 0; n_row <= dtDet.Rows.Count - 1; n_row++)
            {
                BE_VTA_PEDIDOCENDET detped = new BE_VTA_PEDIDOCENDET();

                detped.n_idped = Convert.ToInt32(dtDet.Rows[n_row]["n_idped"]);
                detped.c_coditecen = dtDet.Rows[n_row]["c_coditecen"].ToString();
                detped.n_canpro = Convert.ToInt32(dtDet.Rows[n_row]["n_canpro"]);
                detped.c_codunimedcen = dtDet.Rows[n_row]["c_codunimedcen"].ToString();

                LstDetalle.Add(detped);
            }
            b_result = true;
            return b_result;
        }
        public bool Insertar(BE_VTA_PEDIDOCEN entPedido, List<BE_VTA_PEDIDOCENDET> lstDetalle)
        {
            CD_vta_pedidocen miFun = new CD_vta_pedidocen();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(entPedido, lstDetalle);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_VTA_PEDIDOCEN entPedido, List<BE_VTA_PEDIDOCENDET> lstDetalle)
        {
            CD_vta_pedidocen miFun = new CD_vta_pedidocen();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(entPedido, lstDetalle);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool Eliminar(int n_Id)
        {
            CD_vta_pedidocen miFun = new CD_vta_pedidocen();
            bool booOk = false;

            miFun.mysConec = mysConec;

            booOk = miFun.Eliminar(n_Id);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool ProcesarCEN()
        {
            bool b_result = false;
            //bool b_result = false;
            Helper.Cls_IO funIO = new Helper.Cls_IO();
            string[] c_ListaArchivos;
            int n_numnarc = 0;
            int n_row = 0;
            string c_nomarc = "";
            int n_numarch = 0;
            DataTable dtOC = new DataTable();

            // CARGAMOS TODOS LOS ARCHIVOS ZIP
            c_ListaArchivos = funIO.Dir_LeerDirectorio("C:\\SSF-NET\\PEDIDOS", "*.txt");
            n_numnarc = Convert.ToInt32(c_ListaArchivos.GetLongLength(0));

            for (n_row = 0; n_row <= n_numnarc - 1; n_row++)
            {
                c_nomarc = c_ListaArchivos[n_row];
                LeerPedidoCEN(c_nomarc);

                PrepararEntidad(a_CabeceraCEN, a_DetalleCEN);

                CD_vta_pedidocen miFun = new CD_vta_pedidocen();
                bool booOk = false;

                miFun.mysConec = mysConec;
                entRegistro.n_anotra = STU_SISTEMA.ANOTRABAJO;
                entRegistro.n_mestra = STU_SISTEMA.MESTRABAJO;
                entRegistro.n_idemp = STU_SISTEMA.EMPRESAID;

                miFun.Consulta1(STU_SISTEMA.EMPRESAID, a_CabeceraCEN[0]);
                dtOC = miFun.dtLista;

                if (dtOC.Rows.Count == 0)
                {
                    if (miFun.Insertar(entRegistro, LstDetalle) == false)
                    {
                        MessageBox.Show("¡ No se pudo cargar el siguiente archivo : " + c_nomarc + " !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        return b_result;
                    }
                    else
                    {
                        n_numarch = n_numarch + 1;
                        funIO.Fil_EliminarArchivo(c_nomarc);
                    }
                }
                else
                {
                    MessageBox.Show("¡ El pedido Nº " + a_CabeceraCEN[0] + " ya existe !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    funIO.Fil_EliminarArchivo(c_nomarc);
                }
            }

            if (n_numarch > 0)
            {
                MessageBox.Show("¡ Se importaron " + n_numarch.ToString() + " pedidos del cliente CENCOSUD !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show("¡ No se encontraron pedidos para importar del cliente CENCOSUD !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            b_result = true;
            return b_result;
        }
        void PrepararEntidad(string[] c_Cabecera, string[,] c_Detalle)
        {
            int n_row = 0;

            Helper.Comunes.Funciones funFunciones = new Helper.Comunes.Funciones();

            entRegistro.n_idemp = STU_SISTEMA.EMPRESAID;
            entRegistro.n_id = 0;
            entRegistro.c_codcli = "7750174001558";
            entRegistro.c_codpunven = c_Cabecera[3];
            entRegistro.c_codpunent = c_Cabecera[7];
            entRegistro.d_fchemi = Convert.ToDateTime(c_Cabecera[1].Substring(6, 2) + "/" + c_Cabecera[1].Substring(4, 2)+"/"+c_Cabecera[1].Substring(0,4));
            entRegistro.d_fchent = Convert.ToDateTime(c_Cabecera[2].Substring(6, 2) + "/" + c_Cabecera[2].Substring(4, 2)+"/"+c_Cabecera[2].Substring(0,4));
            entRegistro.n_numite = 0;
            entRegistro.c_numped = c_Cabecera[0];
            entRegistro.d_fchdes = DateTime.Now;
            entRegistro.n_anotra = STU_SISTEMA.ANOTRABAJO;
            entRegistro.n_mestra = STU_SISTEMA.MESTRABAJO;
            
            int n_numite = Convert.ToInt32(c_Detalle.GetLongLength(0));
            LstDetalle.Clear();
            for (n_row = 0; n_row <= n_numite - 1; n_row++)
            {
                BE_VTA_PEDIDOCENDET entDet = new BE_VTA_PEDIDOCENDET();

                if (funFunciones.NulosC(c_Detalle[n_row, 0]).ToString() != "")
                {
                    entDet.n_idped = 0;
                    entDet.c_coditecen = c_Detalle[n_row, 0];
                    entDet.n_canpro = Convert.ToDouble(c_Detalle[n_row, 1]);
                    entDet.n_precio = Convert.ToDouble(c_Detalle[n_row, 2]);
                    entDet.c_codunimedcen = "";
                    LstDetalle.Add(entDet);
                }
            }
        }
        void LeerPedidoCEN(string c_ArchivoPedido)
        {
            Helper.Comunes.Funciones funFunciones = new Helper.Comunes.Funciones();
            Cls_IO funIo = new Cls_IO();
            int n_fil = 0;
            int n_row = 0;
            string[] arrTmp;
            string[] arrDatos = new string[100];
            string[,] a_Detalle = new string[50, 3];
            string[] a_Cabecera = new string[8];
            arrDatos =funIo.LeerLineaArchivo(c_ArchivoPedido);
            int n_NumeroElementos = Convert.ToInt32(arrDatos.GetLongLength(0)) - 1;   // OBTENEMOS EL NUMERO DE ELEMENTOS DEL ARRAY

            string c_numped = "";
            string c_fchemi = "";
            string c_fchent = "";

            string c_punent = "";
            string c_punven = "";
            string c_codcli = "";
            string c_impigv = "";
            string c_imptot = "";
            string c_moneda = "";

            string c_codite = "";
            string c_cantid = "";
            string c_precio = "";

            // CARGAMOS LA CABECERA DEL PEDIDO
            for (n_fil = 0; n_fil <= n_NumeroElementos - 1; n_fil++)
            {
                if (n_fil == 0)
                {       // NUMERO DEL PEDIDO
                    var arrayENC = arrDatos[n_fil].Split(',');
                    if (arrayENC.Length > 0)
                    {
                        c_numped = arrayENC[5];
                    }
                    else
                        c_numped = arrDatos[n_fil].ToString().Substring(44, 21);
                } 

                if (n_fil == 1)
                {
                    c_fchemi = arrDatos[n_fil].ToString().Substring(4, 8);                          // FECHA DE EMISION
                    c_fchent = arrDatos[n_fil].ToString().Substring(19, 8);                         // FECHA DE ENTREGA
                }

                //BYOC,7750892001588,,,7751324010802
                //1234567890123456789012345678901234567890
                if (n_fil == 2) { c_punven = arrDatos[n_fil].ToString().Substring(21, 13); }         // PUNTO DE VENTA
                if (n_fil == 4) { c_punent = arrDatos[n_fil].ToString().Substring(5, 13); }         // PUNTO DE ENTREGA
                if (n_fil == 6) { c_impigv = arrDatos[n_fil].ToString().Substring(7, 6); }          // IMPORTE IGV
                if (n_fil == 7) { c_moneda = arrDatos[n_fil].ToString().Substring(4, 3); }          // MONEDA
                if (n_fil == 9)                                                                     // IMPORTE TOTAL
                {
                    arrTmp = arrDatos[n_fil].Split(',');
                    c_imptot = arrTmp[1]; // arrDatos[n_fil].ToString().Substring(4, 8); 
                }   

                if (n_fil == 10) { break; }
            }

            a_Cabecera[0] = c_numped;
            a_Cabecera[1] = c_fchemi;
            a_Cabecera[2] = c_fchent;
            a_Cabecera[3] = c_punven;
            a_Cabecera[4] = c_impigv;
            a_Cabecera[5] = c_moneda;
            a_Cabecera[6] = c_imptot;
            a_Cabecera[7] = c_punent;

            n_row = 0;

            // CARGAMOS EL DETALLE DEL PEDIDO
            for (n_fil = 10; n_fil <= n_NumeroElementos - 1; n_fil++)
            {
                c_codite = ""; c_cantid = ""; c_precio = "";
                c_codite = arrDatos[n_fil];
                if (funFunciones.NulosC(c_codite) != "")
                {
                    if (arrDatos[n_fil].ToString().Substring(0, 3) == "LIN")
                    {
                        //BE_VTA_PEDIDOCENDET entDet = new BE_VTA_PEDIDOCENDET();

                        //c_codite = arrDatos[n_fil].ToString().Substring(6, 13);     // CODIGO DEL ITEM
                        string[] a_Dato2 = arrDatos[n_fil].Split(',');
                        c_codite = a_Dato2[2];


                        string[] a_Dato = arrDatos[n_fil + 1].Split(',');           // CANTIDAD DEL ITEM
                        c_cantid = a_Dato[1];

                        a_Dato = arrDatos[n_fil + 3].Split(',');                    // PRECIO UNITARIO
                        c_precio = a_Dato[3];

                        a_Detalle[n_row, 0] = c_codite;
                        a_Detalle[n_row, 1] = c_cantid;
                        a_Detalle[n_row, 2] = c_precio;

                        //entDet.c_coditecen = c_codite;
                        //entDet.c_codunimedcen = "";
                        //entDet.n_canpro = Convert.ToDouble(c_cantid);
                        //entDet.n_precio = Convert.ToDouble( c_precio);
                        //a_DetalleCEN.
                        n_fil = n_fil + 4;
                        n_row = n_row + 1;
                    }
                    else
                    {
                        //n_fil = n_fil + 1;
                        //break;
                    }
                    
                    //n_row = n_row + 1;
                }
            }

            a_CabeceraCEN = a_Cabecera;
            a_DetalleCEN = a_Detalle;
        }
        public bool ActualizarGuiaDespacho(int n_IdPedido, int n_IdGuia, int n_Estado)
        {
            CD_vta_pedidocen miFun = new CD_vta_pedidocen();
            bool booOk = false;

            miFun.mysConec = mysConec;

            booOk = miFun.ActualizarGuiaDespacho(n_IdPedido, n_IdGuia, n_Estado);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public void TraerDetallePedidos(int n_IdEmpresa, string c_CadenaIN)
        {
            DataTable dtResul = new DataTable();
            CD_vta_pedidocen miFun = new CD_vta_pedidocen();
            miFun.mysConec = mysConec;

            miFun.TraerDetallePedidos(n_IdEmpresa, c_CadenaIN);
            dtLista = miFun.dtLista;

            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return;
        }
        public void TraerPendienteEnvio(int n_IdEmpresa, int n_IdMes, int n_AnoTrabajo)
        {
            DataTable dtResul = new DataTable();
            CD_vta_pedidocen miFun = new CD_vta_pedidocen();
            miFun.mysConec = mysConec;

            miFun.TraerPendienteEnvio(n_IdEmpresa, n_IdMes, n_AnoTrabajo);
            dtLista = miFun.dtLista;

            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return;
        }
        public void ImprimirPedido(int n_IdRegistro)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[1, 3];

            arrPara[0, 0] = "n_id";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = n_IdRegistro.ToString();
            
            c_NomArchivo = "Rpt_PedidoCEN.rpt";
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "ventas\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "VENTAS - IMPRESION PEDIDOS CEN";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
        public void ImprimirPedidoDet(int n_IdRegistro)
        {
            DataTable dtresult = new DataTable();
            CD_vta_pedidocen o_pedcen = new CD_vta_pedidocen();
            Helper.Genericas fungen = new Helper.Genericas();
            string[,] arrCabeceraFlex1 = new string[12, 5];

            o_pedcen.mysConec = mysConec;
            o_pedcen.Consulta2(STU_SISTEMA.EMPRESAID,STU_SISTEMA.ANOTRABAJO,STU_SISTEMA.MESTRABAJO);
            dtresult = o_pedcen.dtLista;

            //Helper.Cls_FlexGrid o_flex = new Helper.Cls_FlexGrid();
            arrCabeceraFlex1[0, 0] = "Nº Pedido";
            arrCabeceraFlex1[0, 1] = "100";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_numped";

            arrCabeceraFlex1[1, 0] = "Fch. Emi.";
            arrCabeceraFlex1[1, 1] = "70";
            arrCabeceraFlex1[1, 2] = "F";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "d_fchemi";

            arrCabeceraFlex1[2, 0] = "Fch. Entrega";
            arrCabeceraFlex1[2, 1] = "70";
            arrCabeceraFlex1[2, 2] = "F";
            arrCabeceraFlex1[2, 3] = "";
            arrCabeceraFlex1[2, 4] = "d_fchent";

            arrCabeceraFlex1[3, 0] = "Codigo CEN";
            arrCabeceraFlex1[3, 1] = "100";
            arrCabeceraFlex1[3, 2] = "C";
            arrCabeceraFlex1[3, 3] = "";
            arrCabeceraFlex1[3, 4] = "c_codcen";

            arrCabeceraFlex1[4, 0] = "Tienda";
            arrCabeceraFlex1[4, 1] = "300";
            arrCabeceraFlex1[4, 2] = "C";
            arrCabeceraFlex1[4, 3] = "";
            arrCabeceraFlex1[4, 4] = "c_tienda";

            arrCabeceraFlex1[5, 0] = "Producto";
            arrCabeceraFlex1[5, 1] = "300";
            arrCabeceraFlex1[5, 2] = "C";
            arrCabeceraFlex1[5, 3] = "";
            arrCabeceraFlex1[5, 4] = "c_despro";

            arrCabeceraFlex1[6, 0] = "Uni. Med.";
            arrCabeceraFlex1[6, 1] = "40";
            arrCabeceraFlex1[6, 2] = "C";
            arrCabeceraFlex1[6, 3] = "";
            arrCabeceraFlex1[6, 4] = "c_abrpre";

            arrCabeceraFlex1[7, 0] = "Cantidad";
            arrCabeceraFlex1[7, 1] = "70";
            arrCabeceraFlex1[7, 2] = "D";
            arrCabeceraFlex1[7, 3] = "0.00";
            arrCabeceraFlex1[7, 4] = "n_canpro";

            arrCabeceraFlex1[8, 0] = "Nº GUIA";
            arrCabeceraFlex1[8, 1] = "110";
            arrCabeceraFlex1[8, 2] = "C";
            arrCabeceraFlex1[8, 3] = "";
            arrCabeceraFlex1[8, 4] = "c_numgui";

            arrCabeceraFlex1[9, 0] = "Fecha Entrega";
            arrCabeceraFlex1[9, 1] = "70";
            arrCabeceraFlex1[9, 2] = "F";
            arrCabeceraFlex1[9, 3] = "";
            arrCabeceraFlex1[9, 4] = "d_fchdoc";

            arrCabeceraFlex1[10, 0] = "Cantidad Entregada";
            arrCabeceraFlex1[10, 1] = "80";
            arrCabeceraFlex1[10, 2] = "D";
            arrCabeceraFlex1[10, 3] = "0.00";
            arrCabeceraFlex1[10, 4] = "n_entcan";

            arrCabeceraFlex1[11, 0] = "Quiebre";
            arrCabeceraFlex1[11, 1] = "70";
            arrCabeceraFlex1[11, 2] = "D";
            arrCabeceraFlex1[11, 3] = "0.00";
            arrCabeceraFlex1[11, 4] = "n_quiebre";

            fungen.MostrarDatos(arrCabeceraFlex1, dtresult);
            //string c_NomArchivo = "";
            //string c_Ruta = "";
            //string[,] arrPara = new string[1, 3];

            //arrPara[0, 0] = "n_id";
            //arrPara[0, 1] = "N";
            //arrPara[0, 2] = n_IdRegistro.ToString();

            //c_NomArchivo = "Rpt_PedidoCEN.rpt";
            //c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "ventas\\" + c_NomArchivo;

            //Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            //xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            //xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            //xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            //xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            //xVisor.b_VisPrev = true;
            //xVisor.c_Titulo = "VENTAS - IMPRESION PEDIDOS CEN";
            //xVisor.c_PathRep = c_Ruta;
            //xVisor.arrParametros = arrPara;
            //xVisor.VerCrystal();
        }
    }
}
