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
    public class CN_ges_planproduccion
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();

        private CD_ges_planproduccion objdatos = new CD_ges_planproduccion();

        public List<BE_GES_PLANPRODUCCIONDET> l_DetPro;
        public BE_GES_PLANPRODUCCION e_Cabecera = new BE_GES_PLANPRODUCCION();  

        public DataTable dtLista;
        public DataTable dtLista2;
        public DataTable dtProducto;
        public DataTable dtIntermedio;
        public DataTable dtTodo;

        Genericas funDatos = new Genericas();

        public void Listar(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo)
        {
            objdatos.mysConec = mysConec;
            objdatos.Listar(n_IdEmpresa, n_AnoTrabajo, n_MesTrabajo);
            dtLista = objdatos.dtLista;

            if (objdatos.booOcurrioError == true)
            {
                b_OcurrioError = objdatos.booOcurrioError;
                c_ErrorMensaje = objdatos.StrErrorMensaje;
                n_ErrorNumber = objdatos.IntErrorNumber;
            }
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            DataTable dtres = new DataTable();
            objdatos.booOcurrioError = false;
            objdatos.mysConec = mysConec;
            objdatos.TraerRegistro(n_IdRegistro);
            dtres = objdatos.dtCabecera;

            if (objdatos.booOcurrioError == false)
            {
                e_Cabecera.n_idemp = Convert.ToInt32(dtres.Rows[0]["n_idemp"]);
                e_Cabecera.n_id = Convert.ToInt32(dtres.Rows[0]["n_id"]);
                e_Cabecera.c_des = dtres.Rows[0]["c_des"].ToString();
                e_Cabecera.d_fchini = Convert.ToDateTime(dtres.Rows[0]["d_fchini"]);
                e_Cabecera.d_fchfin = Convert.ToDateTime(dtres.Rows[0]["d_fchfin"]);
                e_Cabecera.n_mesini = Convert.ToInt32(dtres.Rows[0]["n_mesini"]);
                e_Cabecera.n_ano = Convert.ToInt32(dtres.Rows[0]["n_ano"]);
                e_Cabecera.n_activo = Convert.ToInt32(dtres.Rows[0]["n_activo"]);
                e_Cabecera.n_idplaven = Convert.ToInt32(dtres.Rows[0]["n_idplaven"]);

                dtProducto = objdatos.dtProducto;
                dtIntermedio = objdatos.dtIntermedio;
                dtTodo = objdatos.dtTodo;
            }
            else
            {
                b_OcurrioError = objdatos.booOcurrioError;
                c_ErrorMensaje = objdatos.StrErrorMensaje;
                n_ErrorNumber = objdatos.IntErrorNumber;
            }
        }
        public bool Insertar()
        {
            bool booOk = false;
            objdatos.mysConec = mysConec;
            booOk = objdatos.Insertar(e_Cabecera, l_DetPro);

            if (booOk == false)
            {
                b_OcurrioError = objdatos.booOcurrioError;
                c_ErrorMensaje = objdatos.StrErrorMensaje;
                n_ErrorNumber = objdatos.IntErrorNumber;
            }
            return booOk;
        }
        public bool Actualizar()
        {
            bool booOk = false;
            objdatos.mysConec = mysConec;
            booOk = objdatos.Actualizar(e_Cabecera, l_DetPro);

            if (booOk == false)
            {
                b_OcurrioError = objdatos.booOcurrioError;
                c_ErrorMensaje = objdatos.StrErrorMensaje;
                n_ErrorNumber = objdatos.IntErrorNumber;
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
                b_OcurrioError = objdatos.booOcurrioError;
                c_ErrorMensaje = objdatos.StrErrorMensaje;
                n_ErrorNumber = objdatos.IntErrorNumber;
            }

            return booOk;
        }
        public void BuscarPlanVentasActivo(int n_IdEmpresa, int n_Estado)
        {
            DataTable dtResult = new DataTable();
            CD_ges_planventas o_planventa = new CD_ges_planventas();
            string[,] arrCabeceraFlexFil = new string[5, 5];
            string n_ancho = "0";
            o_planventa.mysConec = mysConec;
            o_planventa.Consulta1(n_IdEmpresa, n_Estado);
            dtResult = o_planventa.dtLista;

            n_ancho = "40";
            //if (b_Seleccionar == false) { n_ancho = "0"; }

            // FLEX GRID DE LOS TAREAS
            arrCabeceraFlexFil[0, 0] = "Nº Plan";
            arrCabeceraFlexFil[0, 1] = "100";
            arrCabeceraFlexFil[0, 2] = "C";
            arrCabeceraFlexFil[0, 3] = "";
            arrCabeceraFlexFil[0, 4] = "n_id";

            arrCabeceraFlexFil[1, 0] = "Fch. Plan";
            arrCabeceraFlexFil[1, 1] = "70";
            arrCabeceraFlexFil[1, 2] = "F";
            arrCabeceraFlexFil[1, 3] = "dd/MM/yyyy";
            arrCabeceraFlexFil[1, 4] = "d_fchcre";

            arrCabeceraFlexFil[2, 0] = "Descripcion";
            arrCabeceraFlexFil[2, 1] = "300";
            arrCabeceraFlexFil[2, 2] = "C";
            arrCabeceraFlexFil[2, 3] = "";
            arrCabeceraFlexFil[2, 4] = "c_des";

            arrCabeceraFlexFil[3, 0] = "Sel";
            arrCabeceraFlexFil[3, 1] = n_ancho;
            arrCabeceraFlexFil[3, 2] = "B";
            arrCabeceraFlexFil[3, 3] = "";
            arrCabeceraFlexFil[3, 4] = "n_sel";

            arrCabeceraFlexFil[4, 0] = "Id";
            arrCabeceraFlexFil[4, 1] = "0";
            arrCabeceraFlexFil[4, 2] = "N";
            arrCabeceraFlexFil[4, 3] = "";
            arrCabeceraFlexFil[4, 4] = "n_id";

            funDatos.Filtrar_CampoOrden = "c_des";
            funDatos.Filtrar_Titulo = "Plan de Ventas Activos";
            funDatos.Filtrar_ColumnaCheck = 4;
            funDatos.Filtrar_ColumnaBusqueda = 5;
            funDatos.Filtrar_CampoBusqueda = "n_id";
            funDatos.Filtrar_AplicarFiltro = false;
            dtResult = funDatos.Filtrar(arrCabeceraFlexFil, dtResult);

            dtLista = dtResult;
            return;
        }
        public void BuscarPlanProduccionActivo(int n_IdEmpresa)
        {
            DataTable dtResult = new DataTable();
            CD_ges_planproduccion o_plapro = new CD_ges_planproduccion();
            string[,] arrCabeceraFlexFil = new string[5, 5];
            string n_ancho = "0";
            o_plapro.mysConec = mysConec;
            o_plapro.Consulta1(n_IdEmpresa);
            dtResult = o_plapro.dtLista;

            n_ancho = "40";

            // FLEX GRID DE LOS TAREAS
            arrCabeceraFlexFil[0, 0] = "Nº Plan";
            arrCabeceraFlexFil[0, 1] = "100";
            arrCabeceraFlexFil[0, 2] = "C";
            arrCabeceraFlexFil[0, 3] = "";
            arrCabeceraFlexFil[0, 4] = "n_id";

            arrCabeceraFlexFil[1, 0] = "Fch. Plan";
            arrCabeceraFlexFil[1, 1] = "70";
            arrCabeceraFlexFil[1, 2] = "F";
            arrCabeceraFlexFil[1, 3] = "dd/MM/yyyy";
            arrCabeceraFlexFil[1, 4] = "d_fchcre";

            arrCabeceraFlexFil[2, 0] = "Descripcion";
            arrCabeceraFlexFil[2, 1] = "300";
            arrCabeceraFlexFil[2, 2] = "C";
            arrCabeceraFlexFil[2, 3] = "";
            arrCabeceraFlexFil[2, 4] = "c_des";

            arrCabeceraFlexFil[3, 0] = "Sel";
            arrCabeceraFlexFil[3, 1] = n_ancho;
            arrCabeceraFlexFil[3, 2] = "B";
            arrCabeceraFlexFil[3, 3] = "";
            arrCabeceraFlexFil[3, 4] = "n_sel";

            arrCabeceraFlexFil[4, 0] = "Id";
            arrCabeceraFlexFil[4, 1] = "0";
            arrCabeceraFlexFil[4, 2] = "N";
            arrCabeceraFlexFil[4, 3] = "";
            arrCabeceraFlexFil[4, 4] = "n_id";

            funDatos.Filtrar_CampoOrden = "c_des";
            funDatos.Filtrar_Titulo = "Plan de Produccion Activos";
            funDatos.Filtrar_ColumnaCheck = 4;
            funDatos.Filtrar_ColumnaBusqueda = 5;
            funDatos.Filtrar_CampoBusqueda = "n_id";
            funDatos.Filtrar_AplicarFiltro = false;
            dtResult = funDatos.Filtrar(arrCabeceraFlexFil, dtResult);

            dtLista = dtResult;
            return;
        }
        public void CambiarEstadoPlanProduccion(int n_IdPlanVenta, int n_Estado)
        {
            objdatos.booOcurrioError = false;
            objdatos.mysConec = mysConec;
            objdatos.CambiarEstadoPlanProduccion(n_IdPlanVenta, n_Estado);

            if (objdatos.booOcurrioError == true)
            {
                b_OcurrioError = objdatos.booOcurrioError;
                c_ErrorMensaje = objdatos.StrErrorMensaje;
                n_ErrorNumber = objdatos.IntErrorNumber;
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
                b_OcurrioError = objdatos.booOcurrioError;
                c_ErrorMensaje = objdatos.StrErrorMensaje;
                n_ErrorNumber = objdatos.IntErrorNumber;
            }
            return;
        }
        public void PlanProduccionUnificado()
        {
            objdatos.booOcurrioError = false;
            objdatos.mysConec = mysConec;
            objdatos.PlanProduccionUnificado();

            dtLista = objdatos.dtLista;
            dtLista2 = objdatos.dtLista2;
            if (objdatos.booOcurrioError == true)
            {
                b_OcurrioError = objdatos.booOcurrioError;
                c_ErrorMensaje = objdatos.StrErrorMensaje;
                n_ErrorNumber = objdatos.IntErrorNumber;
            }
            return;
        }
    }
}
