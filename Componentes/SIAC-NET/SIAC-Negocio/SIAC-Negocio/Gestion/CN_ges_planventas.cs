using Helper;
using MySql.Data.MySqlClient;
using SIAC_DATOS.Almacen;
using SIAC_DATOS.Gestion;
using SIAC_DATOS.Sistema;
using SIAC_Entidades;
using SIAC_Entidades.Gestion;
using SIAC_Objetos.Constantes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIAC_Negocio.Gestion
{
    public class CN_ges_planventas
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        private CD_ges_planventas objdatos = new CD_ges_planventas();

        private List<BE_GES_PLANVENTASDET> _lstDetalle;
        private BE_GES_PLANVENTAS _entCabecera;
        private List<BE_GES_PLANVENTASANOS> _lstDetalleAnos;

        private DataTable _dtCabecera;
        private DataTable _dtLista;
        private DataTable _dtItems;
        private DataTable _dtDataAnos;
        private DataTable _dtVenHis;
        private DataTable _dtDetalle;
        private DataTable _dtDetalleAnos;
        private DataTable _dtOrdenMes;
        public List<BE_GES_PLANVENTASDET> lstDetalle
        {
            get { return _lstDetalle; }
            set { _lstDetalle = value; }
        }
        public BE_GES_PLANVENTAS entCabecera
        {
            get { return _entCabecera; }
            set { _entCabecera = value; }
        }
        public List<BE_GES_PLANVENTASANOS> lstDetalleAnos
        {
            get { return _lstDetalleAnos; }
            set { _lstDetalleAnos = value; }
        }

        DatosMySql xMiFuncion = new DatosMySql();
        Genericas o_fundat = new Genericas();
        public DataTable dtCabecera
        {
            get { return _dtCabecera; }
            set { _dtCabecera = value; }
        }
        public DataTable dtItems
        {
            get { return _dtItems; }
            set { _dtItems = value; }
        }
        public DataTable dtDataAnos
        {
            get { return _dtDataAnos; }
            set { _dtDataAnos = value; }
        }
        public DataTable dtVenHis
        {
            get { return _dtVenHis; }
            set { _dtVenHis = value; }
        }
        public DataTable dtDetalle
        {
            get { return _dtDetalle; }
            set { _dtDetalle = value; }
        }
        public DataTable dtDetalleAnos
        {
            get { return _dtDetalleAnos; }
            set { _dtDetalleAnos = value; }
        }
        public DataTable dtLista
        {
            get { return _dtLista; }
            set { _dtLista = value; }
        }
        public DataTable dtOrdenMes
        {
            get { return _dtOrdenMes; }
            set { _dtOrdenMes = value; }
        }

        public void Listar(int n_IdEmpresa, int n_AnoTrabajo)
        {
            objdatos.mysConec = mysConec;
            objdatos.Listar(n_IdEmpresa, n_AnoTrabajo);
            dtLista = objdatos.dtLista;
            dtDetalleAnos = objdatos.dtDetalleAnos;

            if (objdatos.booOcurrioError == true)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            objdatos.booOcurrioError = false;
            objdatos.mysConec = mysConec;
            objdatos.TraerRegistro(n_IdRegistro);

            if (objdatos.booOcurrioError == false)
            {
                dtCabecera = objdatos.dtCabecera;
                dtDetalle = objdatos.dtDetalle;
                dtDetalleAnos = objdatos.dtDetalleAnos;
                if (dtCabecera.Rows.Count != 0)
                {
                    entCabecera = new BE_GES_PLANVENTAS();
                    entCabecera.n_idemp = Convert.ToInt16(dtCabecera.Rows[0]["n_idemp"].ToString());
                    entCabecera.n_id = Convert.ToInt16(dtCabecera.Rows[0]["n_id"].ToString());
                    entCabecera.n_idano = Convert.ToInt16(dtCabecera.Rows[0]["n_idano"].ToString());
                    entCabecera.n_idmes = Convert.ToInt16(dtCabecera.Rows[0]["n_idmes"].ToString());
                    entCabecera.c_des = dtCabecera.Rows[0]["c_des"].ToString();
                    entCabecera.n_idmesini = Convert.ToInt16(dtCabecera.Rows[0]["n_idmesini"].ToString());
                    entCabecera.d_fchcre = Convert.ToDateTime(dtCabecera.Rows[0]["d_fchcre"]);
                    entCabecera.n_activo = Convert.ToInt16(dtCabecera.Rows[0]["n_activo"].ToString());
                }
            }
            else
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
        }
        public bool Insertar()
        {
            bool booOk = false;
            objdatos.mysConec = mysConec;
            objdatos.entCabecera = entCabecera;
            objdatos.lstDetalle = lstDetalle;
            objdatos.lstDetalleAnos = lstDetalleAnos;
            booOk = objdatos.Insertar();
                        
            if (booOk == false)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return booOk;
        }
        public bool Actualizar()
        {
            bool booOk = false;
            objdatos.mysConec = mysConec;
            objdatos.entCabecera = entCabecera;
            objdatos.lstDetalle = lstDetalle;
            objdatos.lstDetalleAnos = lstDetalleAnos;
            booOk = objdatos.Actualizar();

            if (booOk == false)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booOk;
        }
        public bool Eliminar(int n_IdItem)
        {
            bool booOk = false;
            objdatos.mysConec = mysConec;
            booOk = objdatos.Eliminar(n_IdItem);

            if (booOk == false)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booOk;
        }

        public void VentasAnuales(int n_IdEmpresa, int n_AnoTrabajo)
        {
            objdatos.mysConec = mysConec;
            objdatos.VentasAnuales(n_IdEmpresa, n_AnoTrabajo);
            dtLista = objdatos.dtLista;
            if (objdatos.booOcurrioError == true)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
        }
        public void VentasItemxPorAnos(int n_IdEmpresa, int n_AnoTrabajo)
        {
            objdatos.mysConec = mysConec;
            objdatos.VentasItemxPorAnos(n_IdEmpresa, n_AnoTrabajo);
            dtLista = objdatos.dtLista;
            if (objdatos.booOcurrioError == true)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
        }
        public DataTable BuscarItems(int n_IdEmpresa, string c_CadenaIN)
        {
            DataTable dtResult = new DataTable();
            CD_ges_planventas o_pla = new CD_ges_planventas();
            string[,] arrCabeceraFlexFil = new string[3, 5];

            o_pla.mysConec = mysConec;
            o_pla.Consulta1(n_IdEmpresa, c_CadenaIN);
            dtResult = o_pla.dtLista;

            // FLEX GRID DE LOS TAREAS
            arrCabeceraFlexFil[0, 0] = "Producto";
            arrCabeceraFlexFil[0, 1] = "600";
            arrCabeceraFlexFil[0, 2] = "C";
            arrCabeceraFlexFil[0, 3] = "c_despro";

            arrCabeceraFlexFil[1, 0] = "Uni. Med.";
            arrCabeceraFlexFil[1, 1] = "40";
            arrCabeceraFlexFil[1, 2] = "C";
            arrCabeceraFlexFil[1, 3] = "c_abrpre";

            arrCabeceraFlexFil[2, 0] = "Id";
            arrCabeceraFlexFil[2, 1] = "0";
            arrCabeceraFlexFil[2, 2] = "N";
            arrCabeceraFlexFil[2, 3] = "";
            arrCabeceraFlexFil[2, 3] = "n_iditem";

            o_fundat.Buscar_CampoBusqueda = "n_iditem";
            o_fundat.Buscar_CadFiltro = "";
            o_fundat.Buscar_CampoOrden = "c_despro";
            o_fundat.Buscar_Titulo = "Productos de la Empresa";
            dtResult = o_fundat.Buscar(arrCabeceraFlexFil, dtResult);
            return dtResult;
        }
        public void TraerDataAnos(int n_IdEmpresa)
        {
            objdatos.mysConec = mysConec;
            objdatos.TraerDataAnos(n_IdEmpresa);
            dtDataAnos = objdatos.dtDataAnos;
            if (objdatos.booOcurrioError == true)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
        }
        public void TraerVentaHistorica(int n_IdEmpresa, int n_NumMesesConsultar, string c_CadenaIN)
        {
            objdatos.mysConec = mysConec;
            objdatos.TraerVentaHistorica(n_IdEmpresa, n_NumMesesConsultar, c_CadenaIN);
            dtVenHis = objdatos.dtVenHis;
            if (objdatos.booOcurrioError == true)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
        }
        public void TraerVentaHistoricaDetalle(int n_IdEmpresa, string c_CadenaIN)
        {
            objdatos.mysConec = mysConec;
            objdatos.TraerVentaHistoricaDetalle(n_IdEmpresa, c_CadenaIN);
            dtDetalleAnos = objdatos.dtDetalleAnos;
            if (objdatos.booOcurrioError == true)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
        }
        public void OrdenMeses(int n_IdRegistro)
        {
            objdatos.booOcurrioError = false;
            objdatos.mysConec = mysConec;
            objdatos.OrdenMeses(n_IdRegistro);

            dtOrdenMes = objdatos.dtOrdenMes;

            if (objdatos.booOcurrioError == true)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return;
        }
        public void Consulta1(int n_IdEmpresa, int n_Estado)
        {
            objdatos.booOcurrioError = false;
            objdatos.mysConec = mysConec;
            objdatos.Consulta1(n_IdEmpresa, n_Estado);

            dtLista = objdatos.dtLista;

            if (objdatos.booOcurrioError == true)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return;
        }
        public void Consulta2(int n_IdPlanVenta)
        {
            objdatos.booOcurrioError = false;
            objdatos.mysConec = mysConec;
            objdatos.Consulta2(n_IdPlanVenta);

            dtLista = objdatos.dtLista;

            if (objdatos.booOcurrioError == true)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return;
        }
        public void CambiarEstadoPlanVentas(int n_IdPlanVenta, int n_Estado)
        {
            objdatos.booOcurrioError = false;
            objdatos.mysConec = mysConec;
            objdatos.CambiarEstadoPlanVentas(n_IdPlanVenta, n_Estado);

            if (objdatos.booOcurrioError == true)
            {
                booOcurrioError = objdatos.booOcurrioError;
                StrErrorMensaje = objdatos.StrErrorMensaje;
                IntErrorNumber = objdatos.IntErrorNumber;
            }
            return;
        }
        public void ListarActivos(int n_IdPlanVenta)
        {
            objdatos.booOcurrioError = false;
            objdatos.mysConec = mysConec;
            objdatos.ListarActivos(n_IdPlanVenta);

            dtLista = objdatos.dtLista;

            if (objdatos.booOcurrioError == true)
            {
                booOcurrioError = objdatos.booOcurrioError;
                StrErrorMensaje = objdatos.StrErrorMensaje;
                IntErrorNumber = objdatos.IntErrorNumber;
            }
            return;
        }
        public void PlanVentasUnificado()
        {
            objdatos.booOcurrioError = false;
            objdatos.mysConec = mysConec;
            objdatos.PlanVentasUnificado();

            dtLista = objdatos.dtLista;

            if (objdatos.booOcurrioError == true)
            {
                booOcurrioError = objdatos.booOcurrioError;
                StrErrorMensaje = objdatos.StrErrorMensaje;
                IntErrorNumber = objdatos.IntErrorNumber;
            }
            return;
        }
    }
}
