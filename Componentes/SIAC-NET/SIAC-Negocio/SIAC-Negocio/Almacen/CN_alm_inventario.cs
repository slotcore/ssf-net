using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades;
using SIAC_DATOS.Almacen;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Almacen;
using Helper;
namespace SIAC_Negocio.Almacen
{
    public class CN_alm_inventario
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public Helper.Comunes.Funciones  myFun = new Helper.Comunes.Funciones();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        //public Int64 n_IdNuevoRegistro;                                                         // ID DEL NUEVO ITEM PARA CUANDO SE REGISTRE ITEMS DESDE OTROS MODULOS DEL SISTEMA
        public DataTable Listar(int n_IdEmpresa, int n_EsUnificado, int n_VerActivos)
        {
            DataTable dtResul = new DataTable();
            
            CD_alm_inventario miFun = new CD_alm_inventario();
            miFun.mysConec = mysConec;

            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            dtResul = miFun.Listar(n_IdEmpresa, n_EsUnificado, n_VerActivos);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public DataTable Listar()
        {
            DataTable dtResul = new DataTable();

            CD_alm_inventario miFun = new CD_alm_inventario();
            miFun.mysConec = mysConec;

            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            dtResul = miFun.Listar();

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public DataTable Stock(int n_IdEmpresa, int n_AnoTrabajo)
        {
            DataTable dtResul = new DataTable();

            CD_alm_inventario miFun = new CD_alm_inventario();
            miFun.mysConec = mysConec;

            dtResul = miFun.Stock(n_IdEmpresa, n_AnoTrabajo);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public DataTable ListarTabla(int n_idempresa)
        {
            DataTable dtResul = new DataTable();

            CD_alm_inventario miFun = new CD_alm_inventario();
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar_Tabla(n_idempresa);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public BE_ALM_INVENTARIO_CONSULTA TraerRegistro(Int64 n_IdRegistro)
        {
            DataTable dtResul = new DataTable();

            CD_alm_inventario miFun = new CD_alm_inventario();
            miFun.mysConec = mysConec;

            return miFun.TraerRegistro(n_IdRegistro);
        }
        public bool Insertar(ref BE_ALM_INVENTARIO_CONSULTA entInventario)
        {
            BE_ALM_INVENTARIO entNuevoInventario = new BE_ALM_INVENTARIO();
            List<BE_ALM_INVENTARIOUNIMED> lstInventarioUnidadMedida = new List<BE_ALM_INVENTARIOUNIMED>();
            bool b_result = false;

            CD_alm_inventario miFun = new CD_alm_inventario();
            miFun.mysConec = mysConec;
            entNuevoInventario.n_idemp = entInventario.n_idemp;
            entNuevoInventario.n_id = entInventario.n_id;
            entNuevoInventario.n_idtipexi = entInventario.n_idtipexi;
            entNuevoInventario.n_idfam = entInventario.n_idfam;
            entNuevoInventario.n_idclas = entInventario.n_idclas;
            entNuevoInventario.n_idsubclas = entInventario.n_idsubclas;
            entNuevoInventario.c_codpro = entInventario.c_codpro;
            entNuevoInventario.c_despro = entInventario.c_despro;
            entNuevoInventario.c_destec = entInventario.c_destec;
            entNuevoInventario.c_descar = entInventario.c_descar;
            entNuevoInventario.n_stkini = entInventario.n_stkini;
            entNuevoInventario.n_stkact = entInventario.n_stkact;
            entNuevoInventario.n_stkmin = entInventario.n_stkmin;
            entNuevoInventario.n_stkmax = entInventario.n_stkmax;
            entNuevoInventario.n_stkcon = entInventario.n_stkcon;
            entNuevoInventario.n_preini = entInventario.n_preini;
            entNuevoInventario.n_porgan = entInventario.n_porgan;
            entNuevoInventario.n_preuni = entInventario.n_preuni;
            entNuevoInventario.n_preven = entInventario.n_preven;
            entNuevoInventario.n_idmon = entInventario.n_idmon;
            entNuevoInventario.n_precom = entInventario.n_precom;
            entNuevoInventario.n_estado = entInventario.n_estado;
            entNuevoInventario.n_idcueconcom = entInventario.n_idcueconcom;
            entNuevoInventario.n_idcueconven = entInventario.n_idcueconven;
            entNuevoInventario.n_idret = entInventario.n_idret;
            entNuevoInventario.n_iddet = entInventario.n_iddet;
            entNuevoInventario.n_idper = entInventario.n_idper;
            entNuevoInventario.n_idtipcom = entInventario.n_idtipcom;
            entNuevoInventario.n_idtipven = entInventario.n_idtipven;
            entNuevoInventario.n_idimpsel = entInventario.n_idimpsel;
            entNuevoInventario.n_tipope = entInventario.n_tipope;
            entNuevoInventario.n_numdiavid = entInventario.n_numdiavid;
            entNuevoInventario.c_prelot = entInventario.c_prelot;

            foreach (BE_ALM_INVENTARIOUNIMED_CONSULTA element in entInventario.lst_unidadmedida)
            {
                BE_ALM_INVENTARIOUNIMED entUnidadItem = new BE_ALM_INVENTARIOUNIMED();

                entUnidadItem.n_idite = entNuevoInventario.n_id;
                entUnidadItem.n_id = element.n_id;
                entUnidadItem.c_despre = element.c_despre;
                entUnidadItem.c_abrpre = element.c_abrpre;
                entUnidadItem.n_idunimedbas = element.n_idunimedbas;
                entUnidadItem.n_canunimedbas = element.n_canunimedbas;
                entUnidadItem.n_default = element.n_default;
                entUnidadItem.n_preuni = element.n_preuni;
                entUnidadItem.n_preuniigv = element.n_preuniigv;
                            
                lstInventarioUnidadMedida.Add(entUnidadItem);
            }

            b_result = miFun.Insertar(entNuevoInventario, lstInventarioUnidadMedida);
            entInventario.n_id = entNuevoInventario.n_id; 
            return  b_result;
        }
        public bool Actualizar(BE_ALM_INVENTARIO_CONSULTA entInventario, List<BE_ALM_INVENTARIOIMAGEN> lstInventarioImagen)
        {
            BE_ALM_INVENTARIO entNuevoInventario = new BE_ALM_INVENTARIO();
            List<BE_ALM_INVENTARIOUNIMED> lstInventarioUnidadMedida = new List<BE_ALM_INVENTARIOUNIMED>();
            bool booResult = false;
            CD_alm_inventario miFun = new CD_alm_inventario();
            miFun.mysConec = mysConec;
                        
            entNuevoInventario.n_idemp = entInventario.n_idemp;
            entNuevoInventario.n_id = entInventario.n_id;
            entNuevoInventario.n_idtipexi = entInventario.n_idtipexi;  
            entNuevoInventario.n_idfam = entInventario.n_idfam;
            entNuevoInventario.n_idclas = entInventario.n_idclas;
            entNuevoInventario.n_idsubclas = entInventario.n_idsubclas;
            entNuevoInventario.c_codpro = entInventario.c_codpro;
            entNuevoInventario.c_despro = entInventario.c_despro;
            entNuevoInventario.c_destec = entInventario.c_destec; 
            entNuevoInventario.c_descar = entInventario.c_descar;
            entNuevoInventario.n_stkini = entInventario.n_stkini; 
            entNuevoInventario.n_stkact = entInventario.n_stkact;
            entNuevoInventario.n_stkmin = entInventario.n_stkmin;
            entNuevoInventario.n_stkmax = entInventario.n_stkmax;
            entNuevoInventario.n_stkcon = entInventario.n_stkcon;
            entNuevoInventario.n_preini = entInventario.n_preini;
            entNuevoInventario.n_porgan = entInventario.n_porgan;
            entNuevoInventario.n_preuni = entInventario.n_preuni;
            entNuevoInventario.n_preven = entInventario.n_preven;   
            entNuevoInventario.n_idmon = entInventario.n_idmon; 
            entNuevoInventario.n_precom = entInventario.n_precom;
            entNuevoInventario.n_estado = entInventario.n_estado;
            entNuevoInventario.n_idcueconcom = entInventario.n_idcueconcom; 
            entNuevoInventario.n_idcueconven = entInventario.n_idcueconven;
            entNuevoInventario.n_idret = entInventario.n_idret;
            entNuevoInventario.n_iddet = entInventario.n_iddet;
            entNuevoInventario.n_idper = entInventario.n_idper;
            entNuevoInventario.n_idtipcom = entInventario.n_idtipcom;
            entNuevoInventario.n_idtipven = entInventario.n_idtipven; 
            entNuevoInventario.n_idimpsel = entInventario.n_idimpsel;
            entNuevoInventario.n_tipope = entInventario.n_tipope;
            entNuevoInventario.n_numdiavid = entInventario.n_numdiavid;
            entNuevoInventario.c_prelot = entInventario.c_prelot;

            foreach (BE_ALM_INVENTARIOUNIMED_CONSULTA element in entInventario.lst_unidadmedida)
            {
                BE_ALM_INVENTARIOUNIMED entUnidadItem = new BE_ALM_INVENTARIOUNIMED();

                entUnidadItem.n_idite = entNuevoInventario.n_id;
                entUnidadItem.n_id = element.n_id;
                entUnidadItem.c_despre = element.c_despre;
                entUnidadItem.c_abrpre = element.c_abrpre;
                entUnidadItem.n_idunimedbas = element.n_idunimedbas;
                entUnidadItem.n_canunimedbas = element.n_canunimedbas;
                entUnidadItem.n_default = element.n_default;
                entUnidadItem.n_preuni = element.n_preuni;
                entUnidadItem.n_preuniigv = element.n_preuniigv;
                
                lstInventarioUnidadMedida.Add(entUnidadItem);
            }
            
            //return miFun.Actualizar(entNuevoInventario, lstInventarioUnidadMedida,lstInventarioImagen);
            
            // ESCRIBIMOS LOS DATOS DEL ITEM
            booResult = miFun.Actualizar(entNuevoInventario);

            // ESCRIBIMOS LAS UNIDADES DE MEDIDA DEL ITEM
            if (booResult == true)
            { 
                int n_fila = 0;
                CD_alm_inventariounimed funInventarioUniMed = new CD_alm_inventariounimed();
                funInventarioUniMed.mysConec = mysConec;
                for (n_fila = 0; n_fila <= lstInventarioUnidadMedida.Count - 1; n_fila++)
                {
                    if (lstInventarioUnidadMedida[n_fila].n_id == 0)
                    {
                        booResult = funInventarioUniMed.Insertar(lstInventarioUnidadMedida[n_fila]);
                    }
                    else 
                    { 
                        booResult = funInventarioUniMed.Actualizar(lstInventarioUnidadMedida[n_fila]);
                    }
                    if (booResult == false) { return booResult; }
                }

                booResult = true;
            }

            // ESCRIBIMOS LAS IMAGENES DEL ITEM
            if (booResult == true)
            {
                int n_fila = 0;
                int n_codigo = 0;
                string c_nomarchivo = "";
                string c_nomarchivoorigen = "";
                CD_alm_inventarioimagen funInventarioImagen = new CD_alm_inventarioimagen();
                funInventarioImagen.mysConec = mysConec;
                for (n_fila = 0; n_fila <= lstInventarioImagen.Count - 1; n_fila++)
                {
                    if (lstInventarioImagen[n_fila].n_id == 0)
                    {
                        n_codigo = n_codigo + 1;

                        c_nomarchivo = entInventario.n_id.ToString("0000") +"-"+ n_codigo.ToString() + ".jpg";
                        lstInventarioImagen[n_fila].n_idite = entNuevoInventario.n_id;
                        lstInventarioImagen[n_fila].n_id = n_codigo;

                        c_nomarchivoorigen = lstInventarioImagen[n_fila].c_nomfil;
                        lstInventarioImagen[n_fila].c_nomfil = c_nomarchivo;

                        booResult = funInventarioImagen.Insertar(lstInventarioImagen[n_fila]);
                        if (booResult == true)
                        {
                            myFun.ArchivoCopiar(c_nomarchivoorigen, STU_SISTEMA.RUTAFOTOITEMS, c_nomarchivo);
                        }
                    }
                    else
                    { 
                        booResult = funInventarioImagen.Actualizar(lstInventarioImagen[n_fila]);
                    }
                    
                    if (booResult == false) { return booResult; }
                }

                booResult = true;
            }
    
            return booResult;
        }
        public bool Eliminar(int n_IdItem)
        {
            CD_alm_inventario miFun = new CD_alm_inventario();
            bool b_Result = false;
            miFun.mysConec = mysConec;

            if (miFun.Eliminar(n_IdItem) == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
                return b_Result;
            }
            b_Result = true;
            return b_Result;
        }

        public bool Activar(int n_IdItem, int n_Estado)
        {
            CD_alm_inventario miFun = new CD_alm_inventario();
            bool b_Result = false;
            miFun.mysConec = mysConec;

            if (miFun.Activar(n_IdItem, n_Estado) == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
                return b_Result;
            }
            b_Result = true;
            return b_Result;
        }
        public void ReporteListaItems(int n_idTipExi, int n_Estado)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[7, 3];

            arrPara[0, 0] = "n_idemp";
            arrPara[0, 1] = "C";
            arrPara[0, 2] = STU_SISTEMA.EMPRESAID.ToString();

            arrPara[1, 0] = "n_idtipexi";
            arrPara[1, 1] = "C";
            arrPara[1, 2] = n_idTipExi.ToString();

            arrPara[2, 0] = "n_estado";
            arrPara[2, 1] = "C";
            arrPara[2, 2] = n_Estado.ToString();

            arrPara[3, 0] = "c_nomemp";
            arrPara[3, 1] = "C";
            arrPara[3, 2] = STU_SISTEMA.EMPRESANOMBRE;

            arrPara[4, 0] = "c_numruc";
            arrPara[4, 1] = "C";
            arrPara[4, 2] = STU_SISTEMA.EMPRESARUC;

            arrPara[5, 0] = "c_titulo1";
            arrPara[5, 1] = "C";
            arrPara[5, 2] = "LISTA DE ITEMS X TIPO DE EXISTENCIA";

            arrPara[6, 0] = "c_titulo2";
            arrPara[6, 1] = "C";

            if (n_Estado == 1)
            {
                arrPara[6, 2] = "ACTIVOS";
            }
            else
            {
                arrPara[6, 2] = "NO ACTIVOS";
            }
           
            c_NomArchivo = "RptListaItems.rpt";
            //c_Ruta = @"C:\ssf-net\reportes\almacen\" + c_NomArchivo;
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "almacen\\" + c_NomArchivo;
            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();

            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "ALMACEN - REPORTE ITEMS";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
        public void ReporteListaInventario(int n_idTipExi, int n_Estado)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[7, 3];

            arrPara[0, 0] = "n_idemp";
            arrPara[0, 1] = "C";
            arrPara[0, 2] = STU_SISTEMA.EMPRESAID.ToString();

            arrPara[1, 0] = "n_idtipexi";
            arrPara[1, 1] = "C";
            arrPara[1, 2] = n_idTipExi.ToString();

            arrPara[2, 0] = "n_estado";
            arrPara[2, 1] = "C";
            arrPara[2, 2] = n_Estado.ToString();

            arrPara[3, 0] = "c_nomemp";
            arrPara[3, 1] = "C";
            arrPara[3, 2] = STU_SISTEMA.EMPRESANOMBRE;

            arrPara[4, 0] = "c_numruc";
            arrPara[4, 1] = "C";
            arrPara[4, 2] = STU_SISTEMA.EMPRESARUC;

            arrPara[5, 0] = "c_titulo1";
            arrPara[5, 1] = "C";
            arrPara[5, 2] = "LISTA DE ITEMS - INVENTARIO FINAL";

            arrPara[6, 0] = "c_titulo2";
            arrPara[6, 1] = "C";

            if (n_Estado == 1)
            {
                arrPara[6, 2] = "ACTIVOS";
            }
            else
            {
                arrPara[6, 2] = "NO ACTIVOS";
            }

            c_NomArchivo = "RptListaInventario.rpt";
            //c_Ruta = @"C:\ssf-net\reportes\almacen\" + c_NomArchivo;
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "almacen\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "ALMACEN - REPORTE ITEMS";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
        public void ReporteStckMin(int n_idTipExi, int n_Estado)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[7, 3];

            arrPara[0, 0] = "n_idemp";
            arrPara[0, 1] = "C";
            arrPara[0, 2] = STU_SISTEMA.EMPRESAID.ToString();

            arrPara[1, 0] = "n_idtipexi";
            arrPara[1, 1] = "C";
            arrPara[1, 2] = n_idTipExi.ToString();

            arrPara[2, 0] = "n_estado";
            arrPara[2, 1] = "C";
            arrPara[2, 2] = n_Estado.ToString();

            arrPara[3, 0] = "c_nomemp";
            arrPara[3, 1] = "C";
            arrPara[3, 2] = STU_SISTEMA.EMPRESANOMBRE;

            arrPara[4, 0] = "c_numruc";
            arrPara[4, 1] = "C";
            arrPara[4, 2] = STU_SISTEMA.EMPRESARUC;

            arrPara[5, 0] = "c_titulo1";
            arrPara[5, 1] = "C";
            arrPara[5, 2] = "LISTA DE ITEMS - PRODUCTOS CON STOCK MINIMO";

            arrPara[6, 0] = "c_titulo2";
            arrPara[6, 1] = "C";

            if (n_Estado == 1)
            {
                arrPara[6, 2] = "ACTIVOS";
            }
            else
            {
                arrPara[6, 2] = "NO ACTIVOS";
            }

            c_NomArchivo = "RptListaMinimo.rpt";
            //c_Ruta = @"C:\ssf-net\reportes\almacen\" + c_NomArchivo;
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "almacen\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();

            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "ALMACEN - REPORTE ITEMS";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
        public void ReporteStckMax(int n_idTipExi, int n_Estado)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[7, 3];

            arrPara[0, 0] = "n_idemp";
            arrPara[0, 1] = "C";
            arrPara[0, 2] = STU_SISTEMA.EMPRESAID.ToString();

            arrPara[1, 0] = "n_idtipexi";
            arrPara[1, 1] = "C";
            arrPara[1, 2] = n_idTipExi.ToString();

            arrPara[2, 0] = "n_estado";
            arrPara[2, 1] = "C";
            arrPara[2, 2] = n_Estado.ToString();

            arrPara[3, 0] = "c_nomemp";
            arrPara[3, 1] = "C";
            arrPara[3, 2] = STU_SISTEMA.EMPRESANOMBRE;

            arrPara[4, 0] = "c_numruc";
            arrPara[4, 1] = "C";
            arrPara[4, 2] = STU_SISTEMA.EMPRESARUC;

            arrPara[5, 0] = "c_titulo1";
            arrPara[5, 1] = "C";
            arrPara[5, 2] = "LISTA DE ITEMS - PRODUCTOS CON STOCK MAXIMO";

            arrPara[6, 0] = "c_titulo2";
            arrPara[6, 1] = "C";

            if (n_Estado == 1)
            {
                arrPara[6, 2] = "ACTIVOS";
            }
            else
            {
                arrPara[6, 2] = "NO ACTIVOS";
            }

            c_NomArchivo = "RptListaMaximo.rpt";
            //c_Ruta = @"C:\ssf-net\reportes\almacen\" + c_NomArchivo;
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "almacen\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "ALMACEN - REPORTE ITEMS";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
        public DataTable ObtenerCodigo(int n_idempresa, int n_idtipexi, int n_idfam, int n_idclas, int n_idsubclas)
        {
            DataTable dtResul = new DataTable();

            CD_alm_inventario miFun = new CD_alm_inventario();
            miFun.mysConec = mysConec;

            dtResul = miFun.ObtenerCodigo(n_idempresa, n_idtipexi, n_idfam, n_idclas, n_idsubclas);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public DataTable BuscarItem(string c_CadenaFiltro, string c_CampoBuscar, DataTable dtItems, int n_IdTipExistencia)
        {
            string[,] arrCabeceraDg1 = new string[4, 4];
            DataTable dtResult = new DataTable();
            DataTable dtresulpre = new DataTable();
            Helper.Genericas funDatos = new Helper.Genericas();

            if (n_IdTipExistencia != 0)
            {
                dtResult = funDatos.DataTableFiltrar(dtItems, "n_idtipexi = " + n_IdTipExistencia + "");
            }
            else 
            {
                dtResult = dtItems;
            }

            arrCabeceraDg1[0, 0] = "Codigo";
            arrCabeceraDg1[0, 1] = "150";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "c_codpro";

            arrCabeceraDg1[1, 0] = "Descripcion";
            arrCabeceraDg1[1, 1] = "500";
            arrCabeceraDg1[1, 2] = "C";
            arrCabeceraDg1[1, 3] = "c_despro";

            arrCabeceraDg1[2, 0] = "Uni. Med.";
            arrCabeceraDg1[2, 1] = "50";
            arrCabeceraDg1[2, 2] = "C";
            arrCabeceraDg1[2, 3] = "c_abrpre";

            arrCabeceraDg1[3, 0] = "Id";
            arrCabeceraDg1[3, 1] = "0";
            arrCabeceraDg1[3, 2] = "N";
            arrCabeceraDg1[3, 3] = "n_id";

            Genericas xFun = new Genericas();
            xFun.Buscar_CampoBusqueda = c_CampoBuscar;
            xFun.Buscar_CadFiltro = c_CadenaFiltro;
            dtResult = xFun.Buscar(arrCabeceraDg1, dtResult);
            
            return dtResult;
        }
        public DataTable BuscarItemPuntoVenta(string c_CadenaFiltro, string c_CampoBuscar, DataTable dtItems, int n_IdTipExistencia)
        {
            string[,] arrCabeceraDg1 = new string[6, 4];
            DataTable dtResult = new DataTable();
            DataTable dtresulpre = new DataTable();
            Helper.Genericas funDatos = new Helper.Genericas();

            if (n_IdTipExistencia != 0)
            {
                dtResult = funDatos.DataTableFiltrar(dtItems, "n_idtipexi = " + n_IdTipExistencia + "");
            }
            else
            {
                dtResult = dtItems;
            }

            arrCabeceraDg1[0, 0] = "Codigo";
            arrCabeceraDg1[0, 1] = "150";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "c_codpro";

            arrCabeceraDg1[1, 0] = "Descripcion";
            arrCabeceraDg1[1, 1] = "500";
            arrCabeceraDg1[1, 2] = "C";
            arrCabeceraDg1[1, 3] = "c_despro";

            arrCabeceraDg1[2, 0] = "Uni. Med.";
            arrCabeceraDg1[2, 1] = "50";
            arrCabeceraDg1[2, 2] = "C";
            arrCabeceraDg1[2, 3] = "c_abrpre";

            arrCabeceraDg1[3, 0] = "Stock Actual";
            arrCabeceraDg1[3, 1] = "70";
            arrCabeceraDg1[3, 2] = "D";
            arrCabeceraDg1[3, 3] = "n_stkact";

            arrCabeceraDg1[4, 0] = "Precio venta";
            arrCabeceraDg1[4, 1] = "70";
            arrCabeceraDg1[4, 2] = "D";
            arrCabeceraDg1[4, 3] = "n_preven";

            arrCabeceraDg1[5, 0] = "Id";
            arrCabeceraDg1[5, 1] = "0";
            arrCabeceraDg1[5, 2] = "N";
            arrCabeceraDg1[5, 3] = "n_id";

            Genericas xFun = new Genericas();
            xFun.Buscar_CampoBusqueda = c_CampoBuscar;
            xFun.Buscar_CadFiltro = c_CadenaFiltro;
            dtResult = xFun.Buscar(arrCabeceraDg1, dtResult);

            return dtResult;
        }

        public DataTable FiltrarSelccionarItems(int n_IdEmpresa, string c_CadIN)
        { 
            Genericas funDatos = new Genericas();
            Cls_FlexGrid funFlex = new Cls_FlexGrid();
            
            DataTable dtResult = new DataTable();
            string[,] arrCabeceraFlexFil = new string[5, 5];

            dtResult = Listar();
            if (!string.IsNullOrEmpty(c_CadIN))
                dtResult = funDatos.DataTableFiltrar(dtResult, "n_id NOT IN(" + c_CadIN + ")");

            // FLEX GRID DE LOS TAREAS
            arrCabeceraFlexFil[0, 0] = "Codigo";
            arrCabeceraFlexFil[0, 1] = "80";
            arrCabeceraFlexFil[0, 2] = "C";
            arrCabeceraFlexFil[0, 3] = "c_codpro";

            arrCabeceraFlexFil[1, 0] = "Mercaderia / Producto / Servicio";
            arrCabeceraFlexFil[1, 1] = "400";
            arrCabeceraFlexFil[1, 2] = "C";
            arrCabeceraFlexFil[1, 3] = "c_despro";

            arrCabeceraFlexFil[2, 0] = "Uni. Med.";
            arrCabeceraFlexFil[2, 1] = "40";
            arrCabeceraFlexFil[2, 2] = "C";
            arrCabeceraFlexFil[2, 3] = "c_abrpre";

            arrCabeceraFlexFil[3, 0] = "Sel.";
            arrCabeceraFlexFil[3, 1] = "40";
            arrCabeceraFlexFil[3, 2] = "B";
            arrCabeceraFlexFil[3, 3] = "n_sel";

            arrCabeceraFlexFil[4, 0] = "ID";
            arrCabeceraFlexFil[4, 1] = "0";
            arrCabeceraFlexFil[4, 2] = "N";
            arrCabeceraFlexFil[4, 3] = "n_id";

            funDatos.Filtrar_CampoOrden = "c_despro";
            funDatos.Filtrar_Titulo = "Filtro de Trabajadores";
            funDatos.Filtrar_ColumnaCheck = 3;
            dtResult = funDatos.Filtrar2(arrCabeceraFlexFil, dtResult);
            return dtResult;
        }
    }
}
