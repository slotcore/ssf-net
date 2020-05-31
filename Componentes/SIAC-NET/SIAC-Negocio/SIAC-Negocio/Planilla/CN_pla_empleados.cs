using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades.Planillas;
using SIAC_DATOS.Planilla;
using MySql.Data.MySqlClient;
using Helper;

namespace SIAC_Negocio.Planilla
{
    public class CN_pla_empleados
    {
        private DataTable _dtLista;
        private DataTable _dtRegistro;

        private BE_PLA_EMPLEADOS _entEmpleado;

        private bool _b_OcurrioError;
        private string _c_ErrorMensaje;
        private int _n_ErrorNumber;
        
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA;
        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();
        Helper.Comunes.Funciones xFunciones = new Helper.Comunes.Funciones();
        Cls_FlexGrid funFlex = new Cls_FlexGrid();
        CD_pla_empleados miFun;
        public CN_pla_empleados(SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA)
        { 
            CD_pla_empleados xFun = new CD_pla_empleados(STU_SISTEMA.BD_IP, STU_SISTEMA.BD_NOMBASEDATOS, STU_SISTEMA.BD_USUARIO, STU_SISTEMA.BD_CONTRASEÑA, STU_SISTEMA.BD_PUERTO);
            miFun = xFun;
        }
        ~CN_pla_empleados()
        {
            miFun = null;
            xMiFuncion = null;
            xFun = null;
            xFunGen = null;
            xFunciones = null;
            funFlex = null;
        }

        public BE_PLA_EMPLEADOS entEmpleado
        {
            get { return _entEmpleado; }
            set { _entEmpleado = value; }
        }
        public DataTable dtLista
        {
            get { return _dtLista; }
            set { _dtLista = value; }
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
        //public MySqlConnection mysConec
        //{
        //    get { return _mysConec; }
        //    set { _mysConec = value; }
        //}
        public bool Listar(int n_IdEmpresa)
        {
            bool b_result = false;

            //CD_pla_empleados miFun = new CD_pla_empleados();
            //miFun.mysConec = mysConec;

            if (miFun.Listar(n_IdEmpresa) == true)
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
        public bool ListarDetallado(int n_IdEmpresa)
        {
            bool b_result = false;

            if (miFun.ListarDetallado(n_IdEmpresa) == true)
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
        public bool ListarDetalladoActivo(int n_IdEmpresa)
        {
            bool b_result = false;

            if (miFun.ListarDetalladoActivo(n_IdEmpresa) == true)
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
        public bool ListarDetalladoInactivo(int n_IdEmpresa)
        {
            bool b_result = false;

            if (miFun.ListarDetalladoInactivo(n_IdEmpresa) == true)
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
            bool b_result = false;
            DataTable dtResult = new DataTable();
            DataTable dtResultLis = new DataTable();
            //CD_pla_empleados miFun = new CD_pla_empleados();
            //miFun.mysConec = mysConec;
            b_result = miFun.TraerRegistro(n_Idregistro);
            if (b_result == true)
            {
                dtResult = miFun.dtRegistro;

                entEmpleado = new BE_PLA_EMPLEADOS();

                entEmpleado.n_idemp = Convert.ToInt32(dtResult.Rows[0]["n_idemp"]);
                entEmpleado.n_id = Convert.ToInt32(dtResult.Rows[0]["n_id"]);
                entEmpleado.n_idtipdocide = Convert.ToInt32(dtResult.Rows[0]["n_idtipdocide"]);
                entEmpleado.c_numdocide = dtResult.Rows[0]["c_numdocide"].ToString();
                entEmpleado.c_ape1 = dtResult.Rows[0]["c_ape1"].ToString();
                entEmpleado.c_ape2 = dtResult.Rows[0]["c_ape2"].ToString();
                entEmpleado.c_nom1 = dtResult.Rows[0]["c_nom1"].ToString();
                entEmpleado.c_nom2 = dtResult.Rows[0]["c_nom2"].ToString();

                if (xFun.NulosC(dtResult.Rows[0]["d_fchnac"].ToString()) != "")
                {
                    entEmpleado.d_fchnac = Convert.ToDateTime(dtResult.Rows[0]["d_fchnac"]);
                }

                entEmpleado.n_idsex = Convert.ToInt32(dtResult.Rows[0]["n_idsex"]);
                entEmpleado.c_numtel = dtResult.Rows[0]["c_numtel"].ToString();
                entEmpleado.c_numesa = dtResult.Rows[0]["c_numesa"].ToString();
                
                if (xFun.NulosC(dtResult.Rows[0]["d_fching"].ToString()) != "")
                {
                    entEmpleado.d_fching = Convert.ToDateTime(dtResult.Rows[0]["d_fching"]);
                }
                if (xFun.NulosC(dtResult.Rows[0]["d_fchbaj"].ToString()) != "")
                {
                    entEmpleado.d_fchbaj = Convert.ToDateTime(dtResult.Rows[0]["d_fchbaj"]);
                }

                entEmpleado.n_asigfam = Convert.ToDouble(xFunciones.NulosN(dtResult.Rows[0]["n_asigfam"]));
                entEmpleado.n_suebas = Convert.ToDouble(xFunciones.NulosN(dtResult.Rows[0]["n_suebas"]));
                entEmpleado.n_bon = Convert.ToDouble(xFunciones.NulosN(dtResult.Rows[0]["n_bon"]));
                entEmpleado.n_imphornor = Convert.ToDouble(xFunciones.NulosN(dtResult.Rows[0]["n_imphornor"]));
                entEmpleado.n_imphorext = Convert.ToDouble(xFunciones.NulosN(dtResult.Rows[0]["n_imphorext"]));

                entEmpleado.n_destajo = Convert.ToInt32(xFunciones.NulosN(dtResult.Rows[0]["n_destajo"]));
                entEmpleado.n_destacado = Convert.ToInt32(xFunciones.NulosN(dtResult.Rows[0]["n_destacado"]));
                entEmpleado.c_dir = dtResult.Rows[0]["c_dir"].ToString();
                entEmpleado.c_email = dtResult.Rows[0]["c_email"].ToString();
                entEmpleado.n_idnacpro = Convert.ToInt32(xFunciones.NulosN(dtResult.Rows[0]["n_idnacpro"]));
                entEmpleado.n_idnacdep = Convert.ToInt32(xFunciones.NulosN(dtResult.Rows[0]["n_idnacdep"]));
                entEmpleado.n_idnacdis = Convert.ToInt32(xFunciones.NulosN(dtResult.Rows[0]["n_idnacdis"]));
                entEmpleado.n_idrespro = Convert.ToInt32(xFunciones.NulosN(dtResult.Rows[0]["n_idrespro"]));
                entEmpleado.n_idresdep = Convert.ToInt32(xFunciones.NulosN(dtResult.Rows[0]["n_idresdep"]));
                entEmpleado.n_idresdis = Convert.ToInt32(xFunciones.NulosN(dtResult.Rows[0]["n_idresdis"]));
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
            bool b_result = false;
            //CD_pla_empleados miFun = new CD_pla_empleados();

            //miFun.mysConec = mysConec;
            b_result = miFun.Eliminar(n_Idregistro);
            if (b_result == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return b_result;
        }
        public bool Insertar(BE_PLA_EMPLEADOS entEmpleadoU)
        {
            bool booOk = false;
            //CD_pla_empleados miFun = new CD_pla_empleados();

            //miFun.mysConec = mysConec;
            booOk = miFun.Insertar(entEmpleadoU);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_PLA_EMPLEADOS entEmpleadoU)
        {
            bool booOk = false;
            //CD_pla_empleados miFun = new CD_pla_empleados();

            //miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(entEmpleadoU);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool Consulta1(int n_IdEmpresa, string c_CondicionIN)
        {
            bool b_result = false;
            //CD_pla_empleados miFun = new CD_pla_empleados();
            //miFun.mysConec = mysConec;

            if (miFun.Consulta1(n_IdEmpresa, c_CondicionIN) == true)
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
        public void Reporte1()
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[5, 3];

            arrPara[0, 0] = "n_idemp";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = STU_SISTEMA.EMPRESAID.ToString();

            arrPara[1, 0] = "c_nomemp";
            arrPara[1, 1] = "C";
            arrPara[1, 2] = STU_SISTEMA.EMPRESANOMBRE;

            arrPara[2, 0] = "c_numruc";
            arrPara[2, 1] = "C";
            arrPara[2, 2] = STU_SISTEMA.EMPRESARUC;

            arrPara[3, 0] = "c_titulo1";
            arrPara[3, 1] = "C";
            arrPara[3, 2] = "LISTA DE EMPLEADOS";

            arrPara[4, 0] = "c_titulo2";
            arrPara[4, 1] = "C";
            arrPara[4, 2] = "ACTIVOS";

            c_NomArchivo = "RptListaEmpleados.rpt";
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "planillas\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "PLANILLAS - LISTA DE SOCIOS";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
        public void Reporte2(int n_IdEmpresa, int n_IdMes, string c_NombreMes)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[6, 3];

            arrPara[0, 0] = "n_idemp";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = n_IdEmpresa.ToString();

            arrPara[1, 0] = "n_idmes";
            arrPara[1, 1] = "N";
            arrPara[1, 2] = n_IdMes.ToString();

            arrPara[2, 0] = "c_nomemp";
            arrPara[2, 1] = "C";
            arrPara[2, 2] = STU_SISTEMA.EMPRESANOMBRE;

            arrPara[3, 0] = "c_numruc";
            arrPara[3, 1] = "C";
            arrPara[3, 2] = STU_SISTEMA.EMPRESARUC;

            arrPara[4, 0] = "c_titulo1";
            arrPara[4, 1] = "C";
            arrPara[4, 2] = "LISTA DE EMPLEADOS";

            arrPara[5, 0] = "c_titulo2";
            arrPara[5, 1] = "C";
            arrPara[5, 2] = "MES DE " + c_NombreMes;

            c_NomArchivo = "RptCumpleaños2.rpt";
            //c_Ruta = @"j:\ssf-net\reportes\planillas\" + c_NomArchivo;
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "planillas\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "PLANILLAS - REPORTE DE CUMPLEAÑOS";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
        public bool Desactivar(int n_Idregistro, int n_Estado)
        {
            bool b_result = false;
            //CD_pla_empleados miFun = new CD_pla_empleados();

            //miFun.mysConec = mysConec;
            b_result = miFun.Desactivar(n_Idregistro, n_Estado);
            if (b_result == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return b_result;
        }
        public void ReporteCumpleaños(int n_IdEmpresa, int n_Idmes, bool n_Destacados)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[3, 3];

            
            arrPara[0, 0] = "n_idemp";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = n_IdEmpresa.ToString();

            arrPara[1, 0] = "n_idmes";
            arrPara[1, 1] = "N";
            arrPara[1, 2] = n_Idmes.ToString();

            arrPara[2, 0] = "n_destacado";
            arrPara[2, 1] = "N";
            if (n_Destacados == false)
            {
                arrPara[2, 2] = "1";
            }
            else
            {
                arrPara[2, 2] = "2";
            }

            c_NomArchivo = "RptCumpleanos.rpt";
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "planillas\\" + c_NomArchivo;
            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();

            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "PLANILLAS - REPORTE DE CUMPLEAÑOS";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
        public void ReporteFormat1(string c_FechaInicio, string c_FechaFinal)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[2, 3];

            arrPara[0, 0] = "c_fchini";
            arrPara[0, 1] = "C";
            arrPara[0, 2] = c_FechaInicio.ToString();

            arrPara[1, 0] = "c_fchfin";
            arrPara[1, 1] = "C";
            arrPara[1, 2] = c_FechaFinal.ToString();
            
            c_NomArchivo = "RptFormato1.rpt";
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "planillas\\" + c_NomArchivo;
            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();

            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "PLANILLAS - FORMATO 1";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
        public void ReporteFormat2(string c_FechaInicio, string c_FechaFinal, int n_Porcentaje)
        {
            DataTable dtRes = new DataTable();
            //CD_pla_empleados o_emp = new CD_pla_empleados();
            string[,] arrPara = new string[3, 3];

            arrPara[0, 0] = "c_fchini";
            arrPara[0, 1] = "C";
            arrPara[0, 2] = c_FechaInicio.ToString();

            arrPara[1, 0] = "c_fchfin";
            arrPara[1, 1] = "C";
            arrPara[1, 2] = c_FechaFinal.ToString();

            arrPara[2, 0] = "n_por";
            arrPara[2, 1] = "N";
            arrPara[2, 2] = n_Porcentaje.ToString();

            
            string c_NomArchivo = "RptFormato2.rpt";
            string c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "planillas\\" + c_NomArchivo;
            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();

            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "PLANILLAS - FORMATO 2";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
        public bool ListaActivos()
        {
            bool b_result = false;
            //CD_pla_empleados miFun = new CD_pla_empleados();

            //miFun.mysConec = mysConec;
            b_result = miFun.ListaActivos();
            dtLista = miFun.dtLista;

            if (b_result == true)
            {
                DataTable dtResult = new DataTable();
                string[,] arrCabeceraFlexFil = new string[4, 5];
                string[,] arrCabeceraFlexFix = new string[3, 4];

                 // FLEX GRID DE LOS TAREAS
                arrCabeceraFlexFil[0, 0] = "Nº Documento";
                arrCabeceraFlexFil[0, 1] = "80";
                arrCabeceraFlexFil[0, 2] = "C";
                arrCabeceraFlexFil[0, 3] = "";
                arrCabeceraFlexFil[0, 4] = "c_numdocide";

                arrCabeceraFlexFil[1, 0] = "Apellidos y Nombres";
                arrCabeceraFlexFil[1, 1] = "400";
                arrCabeceraFlexFil[1, 2] = "C";
                arrCabeceraFlexFil[1, 3] = "";
                arrCabeceraFlexFil[1, 4] = "c_apenom";

                arrCabeceraFlexFil[2, 0] = "Fch. Ingreso";
                arrCabeceraFlexFil[2, 1] = "80";
                arrCabeceraFlexFil[2, 2] = "F";
                arrCabeceraFlexFil[2, 3] = "dd/MM/yyyy";
                arrCabeceraFlexFil[2, 4] = "d_fching";

                arrCabeceraFlexFil[3, 0] = "Sueldo Basico";
                arrCabeceraFlexFil[3, 1] = "70";
                arrCabeceraFlexFil[3, 2] = "D";
                arrCabeceraFlexFil[3, 3] = "0.00";
                arrCabeceraFlexFil[3, 4] = "n_suebas";

                xFunGen.Filtrar_Titulo = "LISTA DE EMPLEADOS ACTIVOS";
                xFunGen.MostrarDatos_NumFilasCabecera = 3;

                dtResult = xFunGen.MostrarDatos(arrCabeceraFlexFil, dtLista, arrCabeceraFlexFix);
            }
            else
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return b_result;
        }
        public bool DarBajaEmpleado(int n_Idregistro, string c_FechaBaja)
        {
            bool b_result = false;
            //CD_pla_empleados miFun = new CD_pla_empleados();

            //miFun.mysConec = mysConec;
            b_result = miFun.DarBajaEmpleado(n_Idregistro, c_FechaBaja);
            if (b_result == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return b_result;
        }
        public bool ListarAsistenciaDec(string c_FechaInicio, string c_FechaFInal)
        {
            bool b_result = false;

            if (miFun.ListarAsistenciaDec(c_FechaInicio, c_FechaFInal) == true)
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
        public bool ListarAsistenciaHor(string c_FechaInicio, string c_FechaFInal)
        {
            bool b_result = false;

            if (miFun.ListarAsistenciaHor(c_FechaInicio, c_FechaFInal) == true)
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
        public bool ListarAsistenciaDetallado(string c_FechaInicio, string c_FechaFInal)
        {
            bool b_result = false;

            if (miFun.ListarAsistenciaDetallado(c_FechaInicio, c_FechaFInal) == true)
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
        public bool ListarMarcacion(string c_NumeroDocumento, string c_FechaInicio, string c_FechaFInal)
        {
            bool b_result = false;

            if (miFun.ListarMarcacion(c_NumeroDocumento, c_FechaInicio, c_FechaFInal) == true)
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
    }
}
