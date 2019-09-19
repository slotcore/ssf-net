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
    public class CN_ges_planabastecimiento
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();

        private CD_ges_planabastecimiento objdatos = new CD_ges_planabastecimiento();

        public List<BE_GES_PLANABASTECIMIENTODET> l_DetPro = new List<BE_GES_PLANABASTECIMIENTODET>();
        public BE_GES_PLANABASTECIMIENTO e_Cabecera = new BE_GES_PLANABASTECIMIENTO();

        public DataTable dtLista;
        public DataTable dtLista2;
        public DataTable dtLista3;
        public DataTable dtLista4;
        public DataTable dtLista5;
        public DataTable dtLista6;
        public DataTable dtProducto;
        public DataTable dtIntermedio;
        public DataTable dtTodo;
        public DataTable dtProductoIns;
        public DataTable dtIntermedioIns;
        public DataTable dtTodoIns;

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
                e_Cabecera.n_idplapro = Convert.ToInt32(dtres.Rows[0]["n_idplapro"]);

                dtProducto = objdatos.dtProducto;
                dtIntermedio = objdatos.dtIntermedio;
                dtTodo = objdatos.dtTodo;

                dtProductoIns = objdatos.dtProductoIns;
                dtIntermedioIns = objdatos.dtIntermedioIns;
                dtTodoIns = objdatos.dtTodoIns;
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
        public void CambiarEstadoPlanAbastecimiento(int n_IdPlanVenta, int n_Estado)
        {
            objdatos.booOcurrioError = false;
            objdatos.mysConec = mysConec;
            objdatos.CambiarEstadoPlanAbastecimiento(n_IdPlanVenta, n_Estado);

            if (objdatos.booOcurrioError == true)
            {
                b_OcurrioError = objdatos.booOcurrioError;
                c_ErrorMensaje = objdatos.StrErrorMensaje;
                n_ErrorNumber = objdatos.IntErrorNumber;
            }
            return;
        }
        public void ListarActivos(int n_IdPlanAbastecimiento)
        {
            objdatos.booOcurrioError = false;
            objdatos.mysConec = mysConec;
            objdatos.ListarActivos(n_IdPlanAbastecimiento);

            dtLista = objdatos.dtLista;

            if (objdatos.booOcurrioError == true)
            {
                b_OcurrioError = objdatos.booOcurrioError;
                c_ErrorMensaje = objdatos.StrErrorMensaje;
                n_ErrorNumber = objdatos.IntErrorNumber;
            }
            return;
        }
        public void PlanAbastecimientoUnificado()
        {
            objdatos.booOcurrioError = false;
            objdatos.mysConec = mysConec;
            objdatos.PlanAbastecimientoUnificado();

            dtLista = objdatos.dtLista;
            dtLista2 = objdatos.dtLista2;
            dtLista3 = objdatos.dtLista3;
            dtLista4 = objdatos.dtLista4;
            dtLista5 = objdatos.dtLista5;
            dtLista6 = objdatos.dtLista6;
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
