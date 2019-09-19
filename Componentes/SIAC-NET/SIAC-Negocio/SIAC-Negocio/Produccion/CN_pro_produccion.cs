using MySql.Data.MySqlClient;
using SIAC_DATOS.Almacen;
using SIAC_DATOS.Produccion;
using SIAC_DATOS.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using SIAC_Entidades.Produccion;
using Helper;

namespace SIAC_Negocio.Produccion
{
    public class CN_pro_produccion
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();

        public BE_PRO_PRODUCCION EntProduccion = new BE_PRO_PRODUCCION();
        public List<BE_PRO_PRODUCCIONINS> LstInsumos = new List<BE_PRO_PRODUCCIONINS>();
        public DataTable dtListar = new DataTable();

        Helper.Comunes.Funciones funFunciones = new Helper.Comunes.Funciones();
        DatosMySql FunMysql = new DatosMySql();
        Helper.Genericas xFunGen = new Helper.Genericas();
        //public bool ProcesarJornales(string c_FechaInicio, string c_FehaTermino)
        //{
        //    bool b_result = false;

        //    CD_pro_produccion miFun = new CD_pro_produccion();
        //    miFun.mysConec = mysConec;

        //    b_result = miFun.ProcesarJornales(c_FechaInicio, c_FehaTermino);

        //    if (b_result == false)
        //    {
        //        dtJornales = null;
        //        booOcurrioError = miFun.booOcurrioError;
        //        StrErrorMensaje = miFun.StrErrorMensaje;
        //        IntErrorNumber = miFun.IntErrorNumber;
        //    }
        //    else
        //    {
        //        dtJornales = miFun.dtJornales;
        //    }

        //    return b_result;
        //}
        //public bool VerTareasDiaPersonal(int IdEmpresa, int n_IdPersona, string c_DiaTrabajo)
        //{
        //    bool b_result = false;

        //    CD_pro_produccion miFun = new CD_pro_produccion();
        //    miFun.mysConec = mysConec;

        //    b_result = miFun.VerTareasDiaPersonal(IdEmpresa, n_IdPersona, c_DiaTrabajo);

        //    if (b_result == false)
        //    {
        //        dtJornales = null;
        //        booOcurrioError = miFun.booOcurrioError;
        //        StrErrorMensaje = miFun.StrErrorMensaje;
        //        IntErrorNumber = miFun.IntErrorNumber;
        //    }
        //    else
        //    {
        //        dtTareasDiaPersona = miFun.dtTareasDiaPersona;
        //    }

        //    return b_result;
        //}
        public bool TraerRegistro(Int64 n_IdRegistro)
        {
            bool booResult;
            DataTable DtResultado = new DataTable();
            DataTable DtInsumos = new DataTable();
            int n_row = 0;
            CD_pro_produccion miFun = new CD_pro_produccion();

            miFun.mysConec = mysConec;

            booResult = miFun.TraerRegistro(n_IdRegistro);
            DtResultado = miFun.dtRegistro;
            DtInsumos = miFun.dtInsumos;

            if (DtResultado!=null)
            { 
                if (DtResultado.Rows.Count != 0)
                {
                    EntProduccion.n_idemp = Convert.ToInt32(DtResultado.Rows[0]["n_idemp"]);
                    EntProduccion.n_id = Convert.ToInt32(DtResultado.Rows[0]["n_id"]);
                    EntProduccion.n_idtipdoc = Convert.ToInt32(DtResultado.Rows[0]["n_idtipdoc"]);
                    EntProduccion.c_numser = DtResultado.Rows[0]["c_numser"].ToString();
                    EntProduccion.c_numdoc = DtResultado.Rows[0]["c_numdoc"].ToString();
                    EntProduccion.n_anotra = Convert.ToInt32(DtResultado.Rows[0]["n_anotra"]);
                    EntProduccion.n_mestra = Convert.ToInt32(DtResultado.Rows[0]["n_mestra"]);
                    EntProduccion.n_idpro = Convert.ToInt32(DtResultado.Rows[0]["n_idpro"]);
                    EntProduccion.n_idunimed = Convert.ToInt32(DtResultado.Rows[0]["n_idunimed"]);
                    EntProduccion.n_canpro = Convert.ToDouble(DtResultado.Rows[0]["n_canpro"]);
                    EntProduccion.n_idres = Convert.ToInt32(DtResultado.Rows[0]["n_idres"]);
                    EntProduccion.d_fchpro = Convert.ToDateTime(DtResultado.Rows[0]["d_fchpro"].ToString());
                    EntProduccion.c_horini = DtResultado.Rows[0]["c_horini"].ToString();
                    EntProduccion.c_horfin = DtResultado.Rows[0]["c_horfin"].ToString();
                    EntProduccion.c_numlot = DtResultado.Rows[0]["c_numlot"].ToString();
                    EntProduccion.c_obs = DtResultado.Rows[0]["c_obs"].ToString();
                    EntProduccion.n_idrec = Convert.ToInt32(DtResultado.Rows[0]["n_idrec"]);
                    EntProduccion.n_idlin = Convert.ToInt32(DtResultado.Rows[0]["n_idlin"]);
                    EntProduccion.n_docrefidtipdoc = Convert.ToInt32(DtResultado.Rows[0]["n_docrefidtipdoc"]);
                    EntProduccion.n_docrefid = Convert.ToInt32(DtResultado.Rows[0]["n_docrefid"]);
                    EntProduccion.c_docrefnumser = DtResultado.Rows[0]["c_docrefnumser"].ToString();
                    EntProduccion.c_docrefnumdoc = DtResultado.Rows[0]["c_docrefnumdoc"].ToString();
                    EntProduccion.d_fchreg = Convert.ToDateTime(DtResultado.Rows[0]["d_fchreg"]);
                    EntProduccion.n_idsup = Convert.ToInt32(DtResultado.Rows[0]["n_idsup"]);
                    EntProduccion.n_idestado = Convert.ToInt32(DtResultado.Rows[0]["n_idestado"]);
                    EntProduccion.n_tipreg = Convert.ToInt16(DtResultado.Rows[0]["n_tipreg"]);
                    EntProduccion.n_renpor = Convert.ToDouble(funFunciones.NulosN( DtResultado.Rows[0]["n_renpor"]));
                    EntProduccion.n_idnoting = Convert.ToInt16(funFunciones.NulosN(DtResultado.Rows[0]["n_idnoting"]));
                    EntProduccion.n_conmp =  Convert.ToDouble(funFunciones.NulosN(DtResultado.Rows[0]["n_conmp"]));
                    EntProduccion.n_caningmp = Convert.ToDouble(funFunciones.NulosN(DtResultado.Rows[0]["n_caningmp"]));

                    EntProduccion.n_idcli = Convert.ToInt32(funFunciones.NulosN(DtResultado.Rows[0]["n_idcli"]));
                    EntProduccion.n_idped = Convert.ToInt32(funFunciones.NulosN(DtResultado.Rows[0]["n_idped"]));

                    if (Convert.ToInt32(funFunciones.NulosN(DtResultado.Rows[0]["n_canprorea"])) != 0)
                    {
                        EntProduccion.n_canprorea = Convert.ToDouble(DtResultado.Rows[0]["n_canprorea"]);
                    }
                    else
                    {
                        EntProduccion.n_canprorea = 0;
                    }

                    if (funFunciones.NulosC(DtResultado.Rows[0]["d_fchterpro"]) == "")
                    {
                        EntProduccion.d_fchterpro = null;
                    }
                    else
                    {
                        EntProduccion.d_fchterpro = Convert.ToDateTime(DtResultado.Rows[0]["d_fchterpro"]);
                    }
                }
            }
            // CARGAMOS LOS INSUMOS UTILIZADOS EN LA PRODUCCION
            if (DtInsumos!=null)
            { 
                for (n_row=0; n_row <= DtInsumos.Rows.Count-1; n_row++)
                {
                    BE_PRO_PRODUCCIONINS entInsumo = new  BE_PRO_PRODUCCIONINS();

                    entInsumo.n_idpro = Convert.ToInt32(DtInsumos.Rows[n_row]["n_idpro"]);
                    entInsumo.n_idins = Convert.ToInt32(DtInsumos.Rows[n_row]["n_idins"]);
                    entInsumo.n_canuti = Convert.ToDouble(DtInsumos.Rows[n_row]["n_canuti"]);
                    entInsumo.n_canent = Convert.ToDouble(DtInsumos.Rows[n_row]["n_canent"]);
                    entInsumo.n_idunimed = Convert.ToInt32(DtInsumos.Rows[n_row]["n_idunimed"]);
                    entInsumo.n_consumido = Convert.ToInt32(DtInsumos.Rows[n_row]["n_consumido"]);
                    LstInsumos.Add(entInsumo);
                }
            }
            return booResult;
        }
        public bool Listar(int n_IdEmpresa, int n_AnoTra, int n_MesTra)
        {
            bool b_result = false;

            CD_pro_produccion miFun = new CD_pro_produccion();
            miFun.mysConec = mysConec;

            b_result = miFun.Listar(n_IdEmpresa, n_AnoTra, n_MesTra);

            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                dtListar = miFun.dtListar;
            }

            return b_result;
        }
        //public bool TraerTareasLinea(int n_IdProducto, int n_IdReceta, int n_IdLinea)
        //{
        //    bool b_result = false;

        //    CD_pro_produccion miFun = new CD_pro_produccion();
        //    miFun.mysConec = mysConec;

        //    b_result = miFun.TraerTareasLinea(n_IdProducto, n_IdReceta, n_IdLinea);

        //    if (b_result == false)
        //    {
        //        booOcurrioError = miFun.booOcurrioError;
        //        StrErrorMensaje = miFun.StrErrorMensaje;
        //        IntErrorNumber = miFun.IntErrorNumber;
        //    }
        //    else
        //    {
        //        dtListar = miFun.dtListar;
        //    }

        //    return b_result;
        //}
        public bool Eliminar(int n_IdProduccion)
        {
            bool b_result = false;

            CD_pro_produccion miFun = new CD_pro_produccion();
            miFun.mysConec = mysConec;

            b_result = miFun.Eliminar(n_IdProduccion);

            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                dtListar = miFun.dtListar;
            }

            return b_result;
        }
        public bool Insertar(BE_PRO_PRODUCCION entProduccion, List<BE_PRO_PRODUCCIONINS> LstInsumos)
        {
            bool b_result = false;

            CD_pro_produccion miFun = new CD_pro_produccion();

            mysConec = FunMysql.ReAbrirConeccion(mysConec);
            miFun.mysConec = mysConec;

            b_result = miFun.Insertar(entProduccion, LstInsumos);

            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                dtListar = miFun.dtListar;
            }
            b_result = true;
            return b_result;
        }
        public bool Actualizar(BE_PRO_PRODUCCION entProduccion, List<BE_PRO_PRODUCCIONINS> LstInsumos)
        {
            bool b_result = false;

            CD_pro_produccion miFun = new CD_pro_produccion();

            mysConec = FunMysql.ReAbrirConeccion(mysConec);
            miFun.mysConec = mysConec;

            b_result = miFun.Actualizar(entProduccion, LstInsumos);

            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
                return b_result;
            }
            else
            {
                dtListar = miFun.dtListar;
            }
            b_result = true;
            return b_result;
        }
        public bool Consulta2(int n_IdEmpresa, int n_AnoTra, int n_MesTra)
        {
            bool b_result = false;

            CD_pro_produccion miFun = new CD_pro_produccion();
            miFun.mysConec = mysConec;

            b_result = miFun.Consulta2(n_IdEmpresa, n_AnoTra, n_MesTra);

            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                dtListar = miFun.dtListar;
            }

            return b_result;
        }
        public bool ConsultaSolMatPendientes(int n_IdEmpresa)
        {
            bool b_result = false;

            CD_pro_produccion miFun = new CD_pro_produccion();
            miFun.mysConec = mysConec;

            b_result = miFun.ConsultaSolMatPendientes(n_IdEmpresa);

            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                dtListar = miFun.dtListar;
            }

            return b_result;
        }
        public bool Consulta1(int n_IdEmpresa, string c_FechaInicio, string c_FechaFInal, string c_CadInPro, string c_CadInIns)
        {
            bool b_result = false;

            CD_pro_produccion miFun = new CD_pro_produccion();
            miFun.mysConec = mysConec;
            b_result = miFun.Consulta1(n_IdEmpresa, c_FechaInicio, c_FechaFInal, c_CadInPro, c_CadInIns);

            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                dtListar = miFun.dtListar;
            }

            return b_result;
        }
        public bool Consulta4(int n_IdEmpresa, string c_FechaInicio, string c_FechaFInal, string c_CadInPro, string c_CadInIns)
        {
            bool b_result = false;

            CD_pro_produccion miFun = new CD_pro_produccion();
            miFun.mysConec = mysConec;
            b_result = miFun.Consulta4(n_IdEmpresa, c_FechaInicio, c_FechaFInal, c_CadInPro, c_CadInIns);

            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                dtListar = miFun.dtListar;
            }

            return b_result;
        }
        public bool ConsultaSolMatProcesadas(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo)
        {
            bool b_result = false;

            CD_pro_produccion miFun = new CD_pro_produccion();
            miFun.mysConec = mysConec;

            b_result = miFun.ConsultaSolMatProcesadas(n_IdEmpresa, n_AnoTrabajo, n_MesTrabajo);

            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                dtListar = miFun.dtListar;
            }

            return b_result;
        }
        public bool ConsultaTareasPendientes(int n_IdEmpresa)
        {
            // *******************************************************************
            // ESTA FUNCION DEVUELVE LAS PRODUCCIONES QUE NO TIENEN TAREA IMPRESAS
            bool b_result = false;

            CD_pro_produccion miFun = new CD_pro_produccion();
            miFun.mysConec = mysConec;

            b_result = miFun.ConsultaTareasPendientes(n_IdEmpresa);

            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                dtListar = miFun.dtListar;
            }

            return b_result;
        }
        public bool ConsultaProduccionConTarea(int n_IdEmpresa)
        {
            // *********************************************************************************************************************
            // ESTA FUNCION DEVUELVE LAS PRODUCCIONES QUE TIENEN TAREAS IMPRESAS PERO QUE SE REQUIERE IMPRIMIR LAS TAREAS QUE FALTAN
            bool b_result = false;

            CD_pro_produccion miFun = new CD_pro_produccion();
            miFun.mysConec = mysConec;

            b_result = miFun.ConsultaProduccionConTarea(n_IdEmpresa);

            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                dtListar = miFun.dtListar;
            }

            return b_result;
        }
        public bool ActualizarEstadoSolicitud(int n_IdProduccion, int n_Estado)
        {
            bool b_result = false;

            CD_pro_produccion miFun = new CD_pro_produccion();
            miFun.mysConec = mysConec;

            b_result = miFun.ActualizarEstadoSolicitud(n_IdProduccion, n_Estado);

            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return b_result;
        }
        public bool ProductosTerminados(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo)
        {
            bool b_result = false;

            CD_pro_produccion miFun = new CD_pro_produccion();
            miFun.mysConec = mysConec;

            b_result = miFun.ProductosTerminados(n_IdEmpresa, n_AnoTrabajo, n_MesTrabajo);

            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                dtListar = miFun.dtListar;
            }

            return b_result;
        }
        public bool AbrirProduccion(int n_IdProduccion)
        {
            bool b_result = false;

            CD_pro_produccion miFun = new CD_pro_produccion();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);
            miFun.mysConec = mysConec;

            b_result = miFun.AbrirProduccion(n_IdProduccion);

            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                b_result = true;
            }
            return b_result;
        }
        public bool ConsultaProduccionPendienteLiquidar(int n_IdEmpresa)
        {
            bool b_result = false;

            CD_pro_produccion miFun = new CD_pro_produccion();
            miFun.mysConec = mysConec;

            b_result = miFun.ConsultaProduccionPendienteLiquidar(n_IdEmpresa);

            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                dtListar = miFun.dtListar;
            }

            return b_result;
        }
        public DataTable BuscarProduccionPendLiquidacion(int n_IdEmpresa)
        {
            string[,] arrCabeceraDg1 = new string[9, 4];
            DataTable dtResult = new DataTable();
            string c_CampoBuscar = "n_id";
            string c_CadenaFiltro = "";

            Helper.Genericas funDatos = new Helper.Genericas();

            ConsultaProduccionPendienteLiquidar(n_IdEmpresa);
            dtResult = dtListar;

            arrCabeceraDg1[0, 0] = "Nº Produccion";
            arrCabeceraDg1[0, 1] = "110";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "c_numdoc";

            arrCabeceraDg1[1, 0] = "Fch. Produccion";
            arrCabeceraDg1[1, 1] = "80";
            arrCabeceraDg1[1, 2] = "F";
            arrCabeceraDg1[1, 3] = "d_fchpro";

            arrCabeceraDg1[2, 0] = "Resposanble";
            arrCabeceraDg1[2, 1] = "300";
            arrCabeceraDg1[2, 2] = "C";
            arrCabeceraDg1[2, 3] = "c_apenom";

            arrCabeceraDg1[3, 0] = "Can. Producida";
            arrCabeceraDg1[3, 1] = "80";
            arrCabeceraDg1[3, 2] = "D";
            arrCabeceraDg1[3, 3] = "n_canprorea";

            arrCabeceraDg1[4, 0] = "Rendimiento";
            arrCabeceraDg1[4, 1] = "70";
            arrCabeceraDg1[4, 2] = "D";
            arrCabeceraDg1[4, 3] = "n_renpor";

            arrCabeceraDg1[5, 0] = "Tip. Doc. Ingreso";
            arrCabeceraDg1[5, 1] = "50";
            arrCabeceraDg1[5, 2] = "C";
            arrCabeceraDg1[5, 3] = "c_notingtipdoc";

            arrCabeceraDg1[6, 0] = "Nº Ingreso";
            arrCabeceraDg1[6, 1] = "110";
            arrCabeceraDg1[6, 2] = "C";
            arrCabeceraDg1[6, 3] = "c_notingnumdoc";

            arrCabeceraDg1[7, 0] = "Fecha";
            arrCabeceraDg1[7, 1] = "70";
            arrCabeceraDg1[7, 2] = "F";
            arrCabeceraDg1[7, 3] = "d_fchdoc";

            arrCabeceraDg1[8, 0] = "Id";
            arrCabeceraDg1[8, 1] = "0";
            arrCabeceraDg1[8, 2] = "N";
            arrCabeceraDg1[8, 3] = "n_id";

            Helper.Genericas xFun = new Helper.Genericas();
            xFun.Buscar_CampoBusqueda = c_CampoBuscar;
            xFun.Buscar_CadFiltro = c_CadenaFiltro;
            dtResult = xFun.Buscar(arrCabeceraDg1, dtResult);

            return dtResult;
        }
        public bool ConsultarAnos(int n_IdEmpresa)
        {
            bool b_result = false;

            CD_pro_produccion miFun = new CD_pro_produccion();
            miFun.mysConec = mysConec;

            b_result = miFun.ConsultarAnos(n_IdEmpresa);

            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                dtListar = miFun.dtListar;
            }

            return b_result;
        }
        public bool Consulta7(int n_IdEmpresa, string c_AnoTrabajo)
        {
            bool b_result = false;

            CD_pro_produccion miFun = new CD_pro_produccion();
            miFun.mysConec = mysConec;

            b_result = miFun.Consulta7(n_IdEmpresa, c_AnoTrabajo);

            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                dtListar = miFun.dtListar;
            }

            return b_result;
        }
        public bool Consulta9(int n_IdEmpresa)
        {
            bool b_result = false;

            CD_pro_produccion miFun = new CD_pro_produccion();
            miFun.mysConec = mysConec;

            b_result = miFun.Consulta9(n_IdEmpresa);

            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                dtListar = miFun.dtListar;
                DataTable dtResult = new DataTable();
                string[,] arrCabeceraFlexFil = new string[17, 5];
                string[,] arrCabeceraFlexFix = new string[3, 4];

                // FLEX GRID DE LOS TAREAS
                arrCabeceraFlexFil[0, 0] = "Tip. Doc.";
                arrCabeceraFlexFil[0, 1] = "40";
                arrCabeceraFlexFil[0, 2] = "C";
                arrCabeceraFlexFil[0, 3] = "";
                arrCabeceraFlexFil[0, 4] = "c_abr";

                arrCabeceraFlexFil[1, 0] = "Nº Revision";
                arrCabeceraFlexFil[1, 1] = "110";
                arrCabeceraFlexFil[1, 2] = "C";
                arrCabeceraFlexFil[1, 3] = "";
                arrCabeceraFlexFil[1, 4] = "c_numrev";

                arrCabeceraFlexFil[2, 0] = "Fch. Revision";
                arrCabeceraFlexFil[2, 1] = "80";
                arrCabeceraFlexFil[2, 2] = "F";
                arrCabeceraFlexFil[2, 3] = "dd/MM/yyyy";
                arrCabeceraFlexFil[2, 4] = "d_fchrev";

                arrCabeceraFlexFil[3, 0] = "Hora Revision";
                arrCabeceraFlexFil[3, 1] = "60";
                arrCabeceraFlexFil[3, 2] = "C";
                arrCabeceraFlexFil[3, 3] = "";
                arrCabeceraFlexFil[3, 4] = "h_horrev";

                arrCabeceraFlexFil[4, 0] = "Can. Pro. Conf.";
                arrCabeceraFlexFil[4, 1] = "70";
                arrCabeceraFlexFil[4, 2] = "D";
                arrCabeceraFlexFil[4, 3] = "0.00";
                arrCabeceraFlexFil[4, 4] = "n_canprocon";

                arrCabeceraFlexFil[5, 0] = "Can. Pro. No Conf.";
                arrCabeceraFlexFil[5, 1] = "70";
                arrCabeceraFlexFil[5, 2] = "D";
                arrCabeceraFlexFil[5, 3] = "0.00";
                arrCabeceraFlexFil[5, 4] = "n_canpronocon";
                
                arrCabeceraFlexFil[6, 0] = "Contidad Revisada";
                arrCabeceraFlexFil[6, 1] = "70";
                arrCabeceraFlexFil[6, 2] = "D";
                arrCabeceraFlexFil[6, 3] = "0.00";
                arrCabeceraFlexFil[6, 4] = "n_canrev";

                arrCabeceraFlexFil[7, 0] = "Nº Lote";
                arrCabeceraFlexFil[7, 1] = "70";
                arrCabeceraFlexFil[7, 2] = "C";
                arrCabeceraFlexFil[7, 3] = "";
                arrCabeceraFlexFil[7, 4] = "c_numlot";

                arrCabeceraFlexFil[8, 0] = "Tipo. Producto";
                arrCabeceraFlexFil[8, 1] = "100";
                arrCabeceraFlexFil[8, 2] = "C";
                arrCabeceraFlexFil[8, 3] = "";
                arrCabeceraFlexFil[8, 4] = "c_destippro";
                
                arrCabeceraFlexFil[9, 0] = "Nº Produccion";
                arrCabeceraFlexFil[9, 1] = "110";
                arrCabeceraFlexFil[9, 2] = "C";
                arrCabeceraFlexFil[9, 3] = "";
                arrCabeceraFlexFil[9, 4] = "c_numpro";

                arrCabeceraFlexFil[10, 0] = "Fch. Produccion";
                arrCabeceraFlexFil[10, 1] = "70";
                arrCabeceraFlexFil[10, 2] = "F";
                arrCabeceraFlexFil[10, 3] = "dd/MM/yyyy";
                arrCabeceraFlexFil[10, 4] = "d_fchpro";

                arrCabeceraFlexFil[11, 0] = "Cod. Producto";
                arrCabeceraFlexFil[11, 1] = "100";
                arrCabeceraFlexFil[11, 2] = "C";
                arrCabeceraFlexFil[11, 3] = "";
                arrCabeceraFlexFil[11, 4] = "c_codpro";

                arrCabeceraFlexFil[12, 0] = "Producto";
                arrCabeceraFlexFil[12, 1] = "300";
                arrCabeceraFlexFil[12, 2] = "C";
                arrCabeceraFlexFil[12, 3] = "";
                arrCabeceraFlexFil[12, 4] = "c_despro";

                arrCabeceraFlexFil[13, 0] = "Uni. Med.";
                arrCabeceraFlexFil[13, 1] = "50";
                arrCabeceraFlexFil[13, 2] = "C";
                arrCabeceraFlexFil[13, 3] = "";
                arrCabeceraFlexFil[13, 4] = "c_abrpre";

                arrCabeceraFlexFil[14, 0] = "Cantidad";
                arrCabeceraFlexFil[14, 1] = "70";
                arrCabeceraFlexFil[14, 2] = "D";
                arrCabeceraFlexFil[14, 3] = "0.00";
                arrCabeceraFlexFil[14, 4] = "n_canprorea";

                arrCabeceraFlexFil[15, 0] = "Cliente";
                arrCabeceraFlexFil[15, 1] = "300";
                arrCabeceraFlexFil[15, 2] = "C";
                arrCabeceraFlexFil[15, 3] = "0.00";
                arrCabeceraFlexFil[15, 4] = "c_clinom";

                arrCabeceraFlexFil[16, 0] = "Nº Pedido";
                arrCabeceraFlexFil[16, 1] = "70";
                arrCabeceraFlexFil[16, 2] = "C";
                arrCabeceraFlexFil[16, 3] = "";
                arrCabeceraFlexFil[16, 4] = "c_numped";


                arrCabeceraFlexFix[0, 0] = "0";
                arrCabeceraFlexFix[0, 1] = "1";
                arrCabeceraFlexFix[0, 2] = "7";
                arrCabeceraFlexFix[0, 3] = "DATOS DE LA REVISION";

                arrCabeceraFlexFix[1, 0] = "0";
                arrCabeceraFlexFix[1, 1] = "8";
                arrCabeceraFlexFix[1, 2] = "12";
                arrCabeceraFlexFix[1, 3] = "DATOS DE PRODUCCION";

                arrCabeceraFlexFix[2, 0] = "0";
                arrCabeceraFlexFix[2, 1] = "15";
                arrCabeceraFlexFix[2, 2] = "16";
                arrCabeceraFlexFix[2, 3] = "DATOS DE LA ORDEN DE COMPRA DEL CLIENTE";


                xFunGen.Filtrar_Titulo = "LISTA DE PARTES DE REVISION PENDIENTES DE JALAR POR ALMACEN";
                xFunGen.MostrarDatos_NumFilasCabecera = 3;

                dtResult = xFunGen.MostrarDatos(arrCabeceraFlexFil, dtListar, arrCabeceraFlexFix);
            }

            return b_result;
        }
        public bool Consulta9_A(int n_IdEmpresa)
        {
            bool b_result = false;
            Helper.Genericas o_gen = new Helper.Genericas();
            Helper.Cls_IO o_io = new Helper.Cls_IO();
            CD_pro_produccion miFun = new CD_pro_produccion();
            
            miFun.mysConec = mysConec;

            b_result = miFun.Consulta9(n_IdEmpresa);

            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                string[,] arrCabeceraFlexFil = new string[17, 5];
                string c_correo = "pcp@agro-vado.com, rrhh01@agro-vado.com, produccion@agro-vado.com, lrincon@agro-vado.com, lrincon@gmail.com, marketing@agro-vado.com, vanerri25@gmail.com";
                //string c_correo = "epollongo@hotmail.com";

                string[] a_correo = c_correo.Split(',');
                dtListar = miFun.dtListar;

                string c_nomarch = "c:\\ssf-net\\VISTA0001.XLS";
                o_io.Fil_EliminarArchivo(c_nomarch);

                // FLEX GRID DE LOS TAREAS
                arrCabeceraFlexFil[0, 0] = "Tip. Doc.";
                arrCabeceraFlexFil[0, 1] = "40";
                arrCabeceraFlexFil[0, 2] = "C";
                arrCabeceraFlexFil[0, 3] = "";
                arrCabeceraFlexFil[0, 4] = "c_abr";

                arrCabeceraFlexFil[1, 0] = "Nº Revision";
                arrCabeceraFlexFil[1, 1] = "110";
                arrCabeceraFlexFil[1, 2] = "C";
                arrCabeceraFlexFil[1, 3] = "";
                arrCabeceraFlexFil[1, 4] = "c_numrev";

                arrCabeceraFlexFil[2, 0] = "Fch. Revision";
                arrCabeceraFlexFil[2, 1] = "80";
                arrCabeceraFlexFil[2, 2] = "F";
                arrCabeceraFlexFil[2, 3] = "dd/MM/yyyy";
                arrCabeceraFlexFil[2, 4] = "d_fchrev";

                arrCabeceraFlexFil[3, 0] = "Hora Revision";
                arrCabeceraFlexFil[3, 1] = "60";
                arrCabeceraFlexFil[3, 2] = "C";
                arrCabeceraFlexFil[3, 3] = "";
                arrCabeceraFlexFil[3, 4] = "h_horrev";

                arrCabeceraFlexFil[4, 0] = "Can. Pro. Conf.";
                arrCabeceraFlexFil[4, 1] = "70";
                arrCabeceraFlexFil[4, 2] = "D";
                arrCabeceraFlexFil[4, 3] = "0.00";
                arrCabeceraFlexFil[4, 4] = "n_canprocon";

                arrCabeceraFlexFil[5, 0] = "Can. Pro. No Conf.";
                arrCabeceraFlexFil[5, 1] = "70";
                arrCabeceraFlexFil[5, 2] = "D";
                arrCabeceraFlexFil[5, 3] = "0.00";
                arrCabeceraFlexFil[5, 4] = "n_canpronocon";

                arrCabeceraFlexFil[6, 0] = "Contidad Revisada";
                arrCabeceraFlexFil[6, 1] = "70";
                arrCabeceraFlexFil[6, 2] = "D";
                arrCabeceraFlexFil[6, 3] = "0.00";
                arrCabeceraFlexFil[6, 4] = "n_canrev";

                arrCabeceraFlexFil[7, 0] = "Nº Lote";
                arrCabeceraFlexFil[7, 1] = "70";
                arrCabeceraFlexFil[7, 2] = "C";
                arrCabeceraFlexFil[7, 3] = "";
                arrCabeceraFlexFil[7, 4] = "c_numlot";

                arrCabeceraFlexFil[8, 0] = "Tipo. Producto";
                arrCabeceraFlexFil[8, 1] = "100";
                arrCabeceraFlexFil[8, 2] = "C";
                arrCabeceraFlexFil[8, 3] = "";
                arrCabeceraFlexFil[8, 4] = "c_destippro";

                arrCabeceraFlexFil[9, 0] = "Nº Produccion";
                arrCabeceraFlexFil[9, 1] = "110";
                arrCabeceraFlexFil[9, 2] = "C";
                arrCabeceraFlexFil[9, 3] = "";
                arrCabeceraFlexFil[9, 4] = "c_numpro";

                arrCabeceraFlexFil[10, 0] = "Fch. Produccion";
                arrCabeceraFlexFil[10, 1] = "70";
                arrCabeceraFlexFil[10, 2] = "F";
                arrCabeceraFlexFil[10, 3] = "dd/MM/yyyy";
                arrCabeceraFlexFil[10, 4] = "d_fchpro";

                arrCabeceraFlexFil[11, 0] = "Cod. Producto";
                arrCabeceraFlexFil[11, 1] = "100";
                arrCabeceraFlexFil[11, 2] = "C";
                arrCabeceraFlexFil[11, 3] = "";
                arrCabeceraFlexFil[11, 4] = "c_codpro";

                arrCabeceraFlexFil[12, 0] = "Producto";
                arrCabeceraFlexFil[12, 1] = "300";
                arrCabeceraFlexFil[12, 2] = "C";
                arrCabeceraFlexFil[12, 3] = "";
                arrCabeceraFlexFil[12, 4] = "c_despro";

                arrCabeceraFlexFil[13, 0] = "Uni. Med.";
                arrCabeceraFlexFil[13, 1] = "50";
                arrCabeceraFlexFil[13, 2] = "C";
                arrCabeceraFlexFil[13, 3] = "";
                arrCabeceraFlexFil[13, 4] = "c_abrpre";

                arrCabeceraFlexFil[14, 0] = "Cantidad";
                arrCabeceraFlexFil[14, 1] = "70";
                arrCabeceraFlexFil[14, 2] = "D";
                arrCabeceraFlexFil[14, 3] = "0.00";
                arrCabeceraFlexFil[14, 4] = "n_canprorea";

                arrCabeceraFlexFil[15, 0] = "Cliente";
                arrCabeceraFlexFil[15, 1] = "300";
                arrCabeceraFlexFil[15, 2] = "C";
                arrCabeceraFlexFil[15, 3] = "0.00";
                arrCabeceraFlexFil[15, 4] = "c_clinom";

                arrCabeceraFlexFil[16, 0] = "Nº Pedido";
                arrCabeceraFlexFil[16, 1] = "70";
                arrCabeceraFlexFil[16, 2] = "C";
                arrCabeceraFlexFil[16, 3] = "";
                arrCabeceraFlexFil[16, 4] = "c_numped";

                o_gen.EXP_EMPRESA = STU_SISTEMA.EMPRESANOMBRE;
                o_gen.EXP_RUC = STU_SISTEMA.EMPRESARUC;
                o_gen.EXP_TITULO1 = "LISTA DE PRODUCCIONES PENDIENTES DE CULMINACION";
                o_gen.EXP_TITULO2 = " AL " + DateTime.Today.ToString("dd/MM/yyyy");

                o_gen.DataTable_Exportar(dtListar, c_nomarch, arrCabeceraFlexFil);
                o_gen = null;

                string c_Asunto = STU_SISTEMA.EMPRESANOMBRE + " - Partes de produccion pendientes de culminacion";
                string c_Cuerpo = "Srs buenos dias, les adjunto la lista de partes de produccion pendientes de culminacion, la omision de este proceso causa que almacen no pueda tener el control de saldos de los productos terminados e intermedios al dia" + "\r\n" +
                    "Atentamente" + "\r\n" +
                    " " + "\r\n" +
                    "SSF-Soft" + "\r\n" +
                    "Control de Procesos";

                Helper.Correo miCorreo = new Helper.Correo();

                string[] c_ListaArchivos = { c_nomarch };

                miCorreo.c_AsuntoCorreo = c_Asunto;
                miCorreo.c_CorreoRecibeCopia = "epollongo@hotmail.com";
                miCorreo.c_CuentaOrigen = "pcp@agro-vado.com";
                miCorreo.c_CuerpoCorreo = c_Cuerpo;
                miCorreo.c_Destinatarios = a_correo;
                miCorreo.c_CuentaContraseña = "Agrovado087701";
                miCorreo.c_DominioCuenta = "mail.agro-vado.com";
                miCorreo.c_ListaArchivos = c_ListaArchivos;
                miCorreo.EnviarCorreo();
                miCorreo = null;
            }

            return b_result;
        }
        public bool Consulta10(int n_IdEmpresa)
        {
            bool b_result = false;

            CD_pro_produccion miFun = new CD_pro_produccion();
            miFun.mysConec = mysConec;

            b_result = miFun.Consulta10(n_IdEmpresa);

            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                dtListar = miFun.dtListar;
                DataTable dtResult = new DataTable();
                string[,] arrCabeceraFlexFil = new string[13, 5];
                string[,] arrCabeceraFlexFix = new string[2, 4];

                // FLEX GRID DE LOS TAREAS
                arrCabeceraFlexFil[0, 0] = "Tip. Doc.";
                arrCabeceraFlexFil[0, 1] = "40";
                arrCabeceraFlexFil[0, 2] = "C";
                arrCabeceraFlexFil[0, 3] = "";
                arrCabeceraFlexFil[0, 4] = "c_abr";

                arrCabeceraFlexFil[1, 0] = "Nº Produccion";
                arrCabeceraFlexFil[1, 1] = "110";
                arrCabeceraFlexFil[1, 2] = "C";
                arrCabeceraFlexFil[1, 3] = "";
                arrCabeceraFlexFil[1, 4] = "c_numdoc";

                arrCabeceraFlexFil[2, 0] = "Tipo. Producto";
                arrCabeceraFlexFil[2, 1] = "100";
                arrCabeceraFlexFil[2, 2] = "C";
                arrCabeceraFlexFil[2, 3] = "";
                arrCabeceraFlexFil[2, 4] = "c_destippro";

                arrCabeceraFlexFil[3, 0] = "Producto";
                arrCabeceraFlexFil[3, 1] = "300";
                arrCabeceraFlexFil[3, 2] = "C";
                arrCabeceraFlexFil[3, 3] = "";
                arrCabeceraFlexFil[3, 4] = "c_despro";

                arrCabeceraFlexFil[4, 0] = "Uni. Med.";
                arrCabeceraFlexFil[4, 1] = "50";
                arrCabeceraFlexFil[4, 2] = "C";
                arrCabeceraFlexFil[4, 3] = "";
                arrCabeceraFlexFil[4, 4] = "c_abrpre";

                arrCabeceraFlexFil[5, 0] = "Fch. Produccion";
                arrCabeceraFlexFil[5, 1] = "70";
                arrCabeceraFlexFil[5, 2] = "F";
                arrCabeceraFlexFil[5, 3] = "dd/MM/yyy";
                arrCabeceraFlexFil[5, 4] = "d_fchpro";

                arrCabeceraFlexFil[6, 0] = "Hora Inicio";
                arrCabeceraFlexFil[6, 1] = "60";
                arrCabeceraFlexFil[6, 2] = "C";
                arrCabeceraFlexFil[6, 3] = "";
                arrCabeceraFlexFil[6, 4] = "c_horini";

                arrCabeceraFlexFil[7, 0] = "Hora Final";
                arrCabeceraFlexFil[7, 1] = "60";
                arrCabeceraFlexFil[7, 2] = "C";
                arrCabeceraFlexFil[7, 3] = "";
                arrCabeceraFlexFil[7, 4] = "c_horfin";

                arrCabeceraFlexFil[8, 0] = "Nº Lote";
                arrCabeceraFlexFil[8, 1] = "70";
                arrCabeceraFlexFil[8, 2] = "C";
                arrCabeceraFlexFil[8, 3] = "";
                arrCabeceraFlexFil[8, 4] = "c_numlot";

                arrCabeceraFlexFil[9, 0] = "Cantidad";
                arrCabeceraFlexFil[9, 1] = "80";
                arrCabeceraFlexFil[9, 2] = "D";
                arrCabeceraFlexFil[9, 3] = "";
                arrCabeceraFlexFil[9, 4] = "n_canprorea";

                arrCabeceraFlexFil[10, 0] = "Dias de Atrazo";
                arrCabeceraFlexFil[10, 1] = "70";
                arrCabeceraFlexFil[10, 2] = "N";
                arrCabeceraFlexFil[10, 3] = "";
                arrCabeceraFlexFil[10, 4] = "n_diadif";

                arrCabeceraFlexFil[11, 0] = "Cliente";
                arrCabeceraFlexFil[11, 1] = "300";
                arrCabeceraFlexFil[11, 2] = "C";
                arrCabeceraFlexFil[11, 3] = "";
                arrCabeceraFlexFil[11, 4] = "c_clinom";

                arrCabeceraFlexFil[12, 0] = "Nº Pedido";
                arrCabeceraFlexFil[12, 1] = "80";
                arrCabeceraFlexFil[12, 2] = "C";
                arrCabeceraFlexFil[12, 3] = "";
                arrCabeceraFlexFil[12, 4] = "c_numped";


                arrCabeceraFlexFix[0, 0] = "0";
                arrCabeceraFlexFix[0, 1] = "1";
                arrCabeceraFlexFix[0, 2] = "9";
                arrCabeceraFlexFix[0, 3] = "DATOS DE LA PRODUCCION";

                arrCabeceraFlexFix[1, 0] = "0";
                arrCabeceraFlexFix[1, 1] = "11";
                arrCabeceraFlexFix[1, 2] = "12";
                arrCabeceraFlexFix[1, 3] = "DATOS DE LA ORDEN DE COMPRA DEL CLIENTE";

                xFunGen.Filtrar_Titulo = "PRODUCCION PENDIENTES DE CIERRE";
                xFunGen.MostrarDatos_NumFilasCabecera = 3;

                dtResult = xFunGen.MostrarDatos(arrCabeceraFlexFil, dtListar, arrCabeceraFlexFix);
            }

            return b_result;
        }
        public bool Consulta10_A(int n_IdEmpresa)
        {
            bool b_result = false;
            Helper.Genericas o_gen = new Helper.Genericas();
            Helper.Cls_IO o_io = new Helper.Cls_IO();
            CD_pro_produccion miFun = new CD_pro_produccion();

            miFun.mysConec = mysConec;

            b_result = miFun.Consulta10(n_IdEmpresa);

            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                string[,] arrCabeceraFlexFil = new string[13, 5];
                string c_correo = "pcp@agro-vado.com, rrhh01@agro-vado.com, produccion@agro-vado.com, lrincon@agro-vado.com, lrincon@gmail.com, marketing@agro-vado.com, vanerri25@gmail.com";
                //string c_correo = "epollongo@hotmail.com";
                
                string[] a_correo = c_correo.Split(',');  
                dtListar = miFun.dtListar;
                
                string c_nomarch = "c:\\ssf-net\\VISTA0001.XLS";
                o_io.Fil_EliminarArchivo(c_nomarch);

                arrCabeceraFlexFil[0, 0] = "Tip. Doc.";
                arrCabeceraFlexFil[0, 1] = "40";
                arrCabeceraFlexFil[0, 2] = "C";
                arrCabeceraFlexFil[0, 3] = "";
                arrCabeceraFlexFil[0, 4] = "c_abr";

                arrCabeceraFlexFil[1, 0] = "Nº Produccion";
                arrCabeceraFlexFil[1, 1] = "110";
                arrCabeceraFlexFil[1, 2] = "C";
                arrCabeceraFlexFil[1, 3] = "";
                arrCabeceraFlexFil[1, 4] = "c_numdoc";

                arrCabeceraFlexFil[2, 0] = "Tipo. Producto";
                arrCabeceraFlexFil[2, 1] = "100";
                arrCabeceraFlexFil[2, 2] = "C";
                arrCabeceraFlexFil[2, 3] = "";
                arrCabeceraFlexFil[2, 4] = "c_destippro";

                arrCabeceraFlexFil[3, 0] = "Producto";
                arrCabeceraFlexFil[3, 1] = "300";
                arrCabeceraFlexFil[3, 2] = "C";
                arrCabeceraFlexFil[3, 3] = "";
                arrCabeceraFlexFil[3, 4] = "c_despro";

                arrCabeceraFlexFil[4, 0] = "Uni. Med.";
                arrCabeceraFlexFil[4, 1] = "50";
                arrCabeceraFlexFil[4, 2] = "C";
                arrCabeceraFlexFil[4, 3] = "";
                arrCabeceraFlexFil[4, 4] = "c_abrpre";

                arrCabeceraFlexFil[5, 0] = "Fch. Produccion";
                arrCabeceraFlexFil[5, 1] = "70";
                arrCabeceraFlexFil[5, 2] = "F";
                arrCabeceraFlexFil[5, 3] = "dd/MM/yyy";
                arrCabeceraFlexFil[5, 4] = "d_fchpro";

                arrCabeceraFlexFil[6, 0] = "Hora Inicio";
                arrCabeceraFlexFil[6, 1] = "60";
                arrCabeceraFlexFil[6, 2] = "C";
                arrCabeceraFlexFil[6, 3] = "";
                arrCabeceraFlexFil[6, 4] = "c_horini";

                arrCabeceraFlexFil[7, 0] = "Hora Final";
                arrCabeceraFlexFil[7, 1] = "60";
                arrCabeceraFlexFil[7, 2] = "C";
                arrCabeceraFlexFil[7, 3] = "";
                arrCabeceraFlexFil[7, 4] = "c_horfin";

                arrCabeceraFlexFil[8, 0] = "Nº Lote";
                arrCabeceraFlexFil[8, 1] = "70";
                arrCabeceraFlexFil[8, 2] = "C";
                arrCabeceraFlexFil[8, 3] = "";
                arrCabeceraFlexFil[8, 4] = "c_numlot";

                arrCabeceraFlexFil[9, 0] = "Cantidad";
                arrCabeceraFlexFil[9, 1] = "80";
                arrCabeceraFlexFil[9, 2] = "D";
                arrCabeceraFlexFil[9, 3] = "";
                arrCabeceraFlexFil[9, 4] = "n_canprorea";

                arrCabeceraFlexFil[10, 0] = "Dias de Atrazo";
                arrCabeceraFlexFil[10, 1] = "70";
                arrCabeceraFlexFil[10, 2] = "N";
                arrCabeceraFlexFil[10, 3] = "";
                arrCabeceraFlexFil[10, 4] = "n_diadif";

                arrCabeceraFlexFil[11, 0] = "Cliente";
                arrCabeceraFlexFil[11, 1] = "300";
                arrCabeceraFlexFil[11, 2] = "C";
                arrCabeceraFlexFil[11, 3] = "";
                arrCabeceraFlexFil[11, 4] = "c_clinom";

                arrCabeceraFlexFil[12, 0] = "Nº Pedido";
                arrCabeceraFlexFil[12, 1] = "80";
                arrCabeceraFlexFil[12, 2] = "C";
                arrCabeceraFlexFil[12, 3] = "";
                arrCabeceraFlexFil[12, 4] = "c_numped";

                o_gen.EXP_EMPRESA = STU_SISTEMA.EMPRESANOMBRE;
                o_gen.EXP_RUC = STU_SISTEMA.EMPRESARUC;
                o_gen.EXP_TITULO1 = "LISTA DE PRODUCCIONES PENDIENTES DE PROCESO X ALMACEN";
                o_gen.EXP_TITULO2 = " AL " + DateTime.Today.ToString("dd/MM/yyyy");

                o_gen.DataTable_Exportar(dtListar, c_nomarch, arrCabeceraFlexFil);
                o_gen = null;

                string c_Asunto = STU_SISTEMA.EMPRESANOMBRE + " - Partes de produccion pendientes de ser procesados por almacen";
                string c_Cuerpo = "Srs buenos dias, les adjunto la lista de partes de produccion pendientes de procesar por almacem, si almacen no procesa esta informacion no podra tener actualizado el saldo de los productos terminados e intermedios en el sistema" + "\r\n" +
                    "Atentamente" + "\r\n" +
                    " " + "\r\n" +
                    "SSF-Soft" + "\r\n" +
                    "Control de Procesos";

                Helper.Correo miCorreo = new Helper.Correo();

                string[] c_ListaArchivos = { c_nomarch };

                miCorreo.c_AsuntoCorreo = c_Asunto;
                miCorreo.c_CorreoRecibeCopia = "epollongo@hotmail.com";
                miCorreo.c_CuentaOrigen = "pcp@agro-vado.com";
                miCorreo.c_CuerpoCorreo = c_Cuerpo;
                miCorreo.c_Destinatarios = a_correo;
                miCorreo.c_CuentaContraseña = "Agrovado087701";
                miCorreo.c_DominioCuenta = "mail.agro-vado.com";
                miCorreo.c_ListaArchivos = c_ListaArchivos;
                miCorreo.EnviarCorreo();
                miCorreo = null;
            }

            return b_result;
        }
        public bool Consulta12(int n_IdEmpresa, int n_AnoTrabajo, int n_TipoProducto)
        {
            bool b_result = false;

            CD_pro_produccion miFun = new CD_pro_produccion();
            miFun.mysConec = mysConec;

            b_result = miFun.Consulta12(n_IdEmpresa, n_AnoTrabajo, n_TipoProducto);

            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                dtListar = miFun.dtListar;
            }

            return b_result;
        }
    }
}
