using MySql.Data.MySqlClient;
using SIAC_DATOS.Estacionamiento;
using SIAC_DATOS.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using Helper;
using Helper.Comunes;
using SIAC_Entidades.Estacionamiento;

namespace SIAC_Negocio.Estacionamiento
{
    public class CN_est_clientes
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public int n_idcargogenerado = 0;                                                        // ESTA  VARIABLE ALMACENA EL ID DEL CARGO GENERADO EN LA TABLA EST_OTROCARGOSCAB
        
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA;
        public BE_EST_CLIENTES e_Cliente = new BE_EST_CLIENTES();
        public List<BE_EST_CLIENTESPLACAS> l_ClientePlaca = new List<BE_EST_CLIENTESPLACAS>();
        public DataTable dtListar = new DataTable();
        public DataTable dtListarPlacas = new DataTable();
        Funciones funFunciones = new Funciones();

        public BE_EST_OTROCARGOSCAB e_carcab = new BE_EST_OTROCARGOSCAB();
        public List<BE_EST_OTROCARGOSDET> l_cardet = new List<BE_EST_OTROCARGOSDET>();
        CD_est_clientes miFun;

        public CN_est_clientes(SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA)
        {
            CD_est_clientes xFun = new CD_est_clientes(STU_SISTEMA.BD_IP, STU_SISTEMA.BD_NOMBASEDATOS, STU_SISTEMA.BD_USUARIO, STU_SISTEMA.BD_CONTRASEÑA, STU_SISTEMA.BD_PUERTO);
            miFun = xFun;
        }

        ~CN_est_clientes()
        {
            miFun = null;
            e_Cliente = null;
            l_ClientePlaca = null;
        }
        public void Listar()
        {
            DataTable dtResul = new DataTable();

            miFun.Listar();
            dtListar = miFun.dtListar;

            if (dtResul == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            
            return;
        }
        public void Listar2()
        {
            DataTable dtResul = new DataTable();

            miFun.Listar2();
            dtListar = miFun.dtListar;

            if (dtResul == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return;
        }
        public void Listar3(int n_IdEmpresa)
        {
            DataTable dtResul = new DataTable();

            miFun.Listar3(n_IdEmpresa);
            dtListar = miFun.dtListar;

            if (dtResul == null)
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
            dtListarPlacas = miFun.dtListarPlacas;

            if (dtListar.Rows.Count != 0)
            {
                e_Cliente.n_idemp = Convert.ToInt16(dtListar.Rows[0]["n_idemp"]);
                e_Cliente.n_idpla = Convert.ToInt16(dtListar.Rows[0]["n_idpla"]);
                e_Cliente.n_id = Convert.ToInt16(dtListar.Rows[0]["n_id"]);
                e_Cliente.n_idtipdocide = Convert.ToInt16(dtListar.Rows[0]["n_idtipdocide"]);
                e_Cliente.c_numdocide = dtListar.Rows[0]["c_numdocide"].ToString();
                e_Cliente.c_nom = dtListar.Rows[0]["c_nom"].ToString();
                e_Cliente.c_ape1 = dtListar.Rows[0]["c_ape1"].ToString();
                e_Cliente.c_ape2 = dtListar.Rows[0]["c_ape2"].ToString();
                e_Cliente.c_nom1 = dtListar.Rows[0]["c_nom1"].ToString();
                e_Cliente.c_nom2 = dtListar.Rows[0]["c_nom2"].ToString();
                e_Cliente.c_dir = dtListar.Rows[0]["c_dir"].ToString();
                e_Cliente.n_iddep = Convert.ToInt16(funFunciones.NulosN(dtListar.Rows[0]["n_iddep"]));
                e_Cliente.n_idpro = Convert.ToInt16(funFunciones.NulosN(dtListar.Rows[0]["n_idpro"]));
                e_Cliente.n_iddis = Convert.ToInt16(funFunciones.NulosN(dtListar.Rows[0]["n_iddis"]));
                e_Cliente.c_numtel = dtListar.Rows[0]["c_numtel"].ToString();
                e_Cliente.n_idtipcli = Convert.ToInt16(dtListar.Rows[0]["n_idtipcli"]);
                e_Cliente.d_fching = Convert.ToDateTime(dtListar.Rows[0]["d_fching"]);
                e_Cliente.n_tipdocfac = Convert.ToInt16(dtListar.Rows[0]["n_tipdocfac"]);
                e_Cliente.n_idser = Convert.ToInt16(dtListar.Rows[0]["n_idser"]);
                e_Cliente.n_idtipcon = Convert.ToInt16(funFunciones.NulosN(dtListar.Rows[0]["n_idtipcon"]));
                e_Cliente.n_importe = Convert.ToDouble(dtListar.Rows[0]["n_importe"]);
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
        public bool Anular(int n_IdRegistro)
        {
            bool booOk = false;

            booOk = miFun.Anular(n_IdRegistro);
            if (booOk == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;

            }
            return booOk;
        }
        public bool Activar(int n_IdRegistro)
        {
            bool booOk = false;

            booOk = miFun.Activar(n_IdRegistro);
            if (booOk == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;

            }
            return booOk;
        }
        public bool Insertar(BE_EST_CLIENTES e_Clientes)
        {
            bool booOk = false;

            miFun.l_ClientePlaca = l_ClientePlaca;
            miFun.e_carcab = e_carcab;
            miFun.l_cardet = l_cardet;
            booOk = miFun.Insertar(e_Clientes);
            n_idcargogenerado = miFun.n_idcargogenerado;

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_EST_CLIENTES e_Clientes)
        {
            bool booOk = false;

            miFun.l_ClientePlaca = l_ClientePlaca;
            booOk = miFun.Actualizar(e_Clientes);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public DataTable BuscarCliente(DataTable DtCliente, string c_CampoBuscar, string c_CadenaFiltro)
        {
            string[,] arrCabeceraDg1 = new string[3, 4];
            DataTable dtResult = new DataTable();
            DataTable dtresulpre = new DataTable();
            Helper.Genericas funDatos = new Helper.Genericas();

            dtResult = DtCliente;

            arrCabeceraDg1[0, 0] = "Nº Placa";
            arrCabeceraDg1[0, 1] = "80";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "c_numpla";

            arrCabeceraDg1[1, 0] = "Nombre / Razon Social";
            arrCabeceraDg1[1, 1] = "600";
            arrCabeceraDg1[1, 2] = "C";
            arrCabeceraDg1[1, 3] = "c_nom";

            arrCabeceraDg1[2, 0] = "Id";
            arrCabeceraDg1[2, 1] = "0";
            arrCabeceraDg1[2, 2] = "N";
            arrCabeceraDg1[2, 3] = "n_id";

            Genericas xFun = new Genericas();
            xFun.Buscar_CampoBusqueda = c_CampoBuscar;
            xFun.Buscar_CadFiltro = c_CadenaFiltro;
            xFun.Buscar_CampoOrden = "c_nom";
            dtResult = xFun.Buscar(arrCabeceraDg1, dtResult);

            return dtResult;
        }
        public void Consulta1(int IdEmpresa, int IdPlaya)
        {
            DataTable dtResul = new DataTable();

            miFun.Consulta1(IdEmpresa, IdPlaya);
            dtListar = miFun.dtListar;

            if (dtResul == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return;
        }
        public void ReporteClientes(int n_IdEmpresa, int n_IdPlaya)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[2, 3];

            arrPara[0, 0] = "n_idemp";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = n_IdEmpresa.ToString();

            arrPara[1, 0] = "n_idpla";
            arrPara[1, 1] = "N";
            arrPara[1, 2] = n_IdPlaya.ToString();

            c_NomArchivo = "Rpt_Clientes.rpt";
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "estacionamientos\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "ESTACIONAMIENTOS - LISTA DE CLIENTES";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
        public int BuscarNoActivos(int n_IdEmpresa, int n_IdPlaya)
        {
            int n_Id = 0;
            string[,] arrCabeceraDg1 = new string[3, 4];
            DataTable dtResult = new DataTable();
            DataTable dtresulpre = new DataTable();
            Helper.Genericas funDatos = new Helper.Genericas();

            miFun.ListarNoActivos(n_IdEmpresa, n_IdPlaya);
            dtResult = miFun.dtListar;

            arrCabeceraDg1[0, 0] = "Nombre / Razon Social";
            arrCabeceraDg1[0, 1] = "600";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "c_nom";

            arrCabeceraDg1[1, 0] = "Nº Doc. Identidad";
            arrCabeceraDg1[1, 1] = "80";
            arrCabeceraDg1[1, 2] = "C";
            arrCabeceraDg1[1, 3] = "c_numdocide";
            
            arrCabeceraDg1[2, 0] = "Id";
            arrCabeceraDg1[2, 1] = "0";
            arrCabeceraDg1[2, 2] = "N";
            arrCabeceraDg1[2, 3] = "n_id";

            Genericas xFun = new Genericas();
            xFun.Buscar_CampoBusqueda = "n_id";
            xFun.Buscar_CadFiltro = "";
            xFun.Buscar_CampoOrden = "c_nom";
            dtResult = xFun.Buscar(arrCabeceraDg1, dtResult);

            if (dtResult != null)
            { 
                if (dtResult.Rows.Count != 0)
                {
                    n_Id = Convert.ToInt32(dtResult.Rows[0]["n_id"]);
                }
            }
            return n_Id;
        }
        public Boolean ExisteDocIdentidad(int n_IdEmpresa, int n_IdPlaya, string c_NumeroDocumento, int n_IdTipoDocumento)
        {
            Boolean b_result = false;
            miFun.Consulta3(n_IdEmpresa, n_IdPlaya, c_NumeroDocumento, n_IdTipoDocumento);

            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_result;
            }
            if (miFun.dtListar.Rows.Count >= 1) { b_result = true; }
            return b_result;
        }
        public Boolean ExisteNumPla(int n_IdEmpresa, int n_IdPlaya, string c_NumPlaca)
        {
            Boolean b_result = false;
            miFun.Consulta4(n_IdEmpresa, n_IdPlaya, c_NumPlaca);

            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_result;
            }
            if (miFun.dtListar.Rows.Count >= 1) { b_result = true; }
            return b_result;
        }
    }
}
