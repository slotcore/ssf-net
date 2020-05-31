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
    public class CN_pla_destajo
    {
        public DataTable dtLista;
        public DataTable dtRegistro;

        public bool b_OcurrioError;
        public string c_ErrorMensaje;
        public int n_ErrorNumber;

        public MySqlConnection mysConec;

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();
        
        public DataTable dtPago = new DataTable();
        public DataTable dtHora = new DataTable();

        public BE_PLA_DESTAJO e_Cargos = new BE_PLA_DESTAJO();
        public List<BE_PLA_DESTAJODET> l_CargosDetalle = new List<BE_PLA_DESTAJODET>();

        public bool Listar(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo)
        {
            bool b_result = false;

            CD_pla_destajo miFun = new CD_pla_destajo();
            miFun.mysConec = mysConec;

            if (miFun.Listar(n_IdEmpresa, n_AnoTrabajo, n_MesTrabajo) == true)
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
            DataTable dtLista = new DataTable();
            DataTable dtResultLis = new DataTable();
            CD_pla_destajo miFun = new CD_pla_destajo();
            int n_row = 0;
            
            miFun.mysConec = mysConec;
            b_result = miFun.TraerRegistro(n_Idregistro);
            if (b_result == true)
            {
                dtResult = miFun.dtRegistro;
                dtLista = miFun.dtLista;
                dtPago = miFun.dtPago;
                dtHora = miFun.dtHora;

                e_Cargos.n_idemp = Convert.ToInt32(dtResult.Rows[0]["n_idemp"]);
                e_Cargos.n_id = Convert.ToInt32(dtResult.Rows[0]["n_id"]);
                e_Cargos.n_idtipdoc = Convert.ToInt32(dtResult.Rows[0]["n_idtipdoc"]);
                e_Cargos.c_numser = dtResult.Rows[0]["c_numser"].ToString();
                e_Cargos.c_numdoc = dtResult.Rows[0]["c_numdoc"].ToString();
                e_Cargos.d_fchreg = Convert.ToDateTime(dtResult.Rows[0]["d_fchreg"]);
                e_Cargos.n_idres = Convert.ToInt32(dtResult.Rows[0]["n_idres"]);
                e_Cargos.d_fchini = Convert.ToDateTime(dtResult.Rows[0]["d_fchini"]);
                e_Cargos.d_fchfin = Convert.ToDateTime(dtResult.Rows[0]["d_fchfin"]);
                //e_Cargos.n_numper = Convert.ToInt32(dtResult.Rows[0]["n_numper"]);
                //e_Cargos.n_imptot = Convert.ToInt32(dtResult.Rows[0]["n_imptot"]);
                e_Cargos.n_mestra = Convert.ToInt32(dtResult.Rows[0]["n_mestra"]);
                e_Cargos.n_anotra = Convert.ToInt32(dtResult.Rows[0]["n_anotra"]);
                e_Cargos.c_glo = dtResult.Rows[0]["c_glo"].ToString();
                e_Cargos.n_idlocal = Convert.ToInt32(dtResult.Rows[0]["n_idlocal"]);
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
            CD_pla_destajo miFun = new CD_pla_destajo();

            miFun.mysConec = mysConec;
            b_result = miFun.Eliminar(n_Idregistro);
            if (b_result == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return b_result;
        }
        public bool Insertar(BE_PLA_DESTAJO e_Cargos)
        {
            CD_pla_destajo miFun = new CD_pla_destajo();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(e_Cargos);

            if (booOk == false)
            { 
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return booOk;
        }
        public bool Actualizar(BE_PLA_DESTAJO e_Cargos)
        {
            CD_pla_destajo miFun = new CD_pla_destajo();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(e_Cargos);

            if (booOk == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return booOk;
        }
        public void VerTareasDia(int n_idPlanilla, int n_IdEmpleado, string c_FechaConsulta)
        {
            DataTable dtResult = new DataTable();
            CD_pla_destajo miFun = new CD_pla_destajo();
            string[,] arrCabeceraFlexFil = new string[12, 5];
            miFun.mysConec = mysConec;

            if (miFun.VerTareasDia(n_idPlanilla, n_IdEmpleado, c_FechaConsulta) == true)
            {
                dtResult = miFun.dtLista;
            }
            else
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return;
            }
            
            // FLEX GRID DE LOS TAREAS
            arrCabeceraFlexFil[0, 0] = "Apellidos y Nombres";
            arrCabeceraFlexFil[0, 1] = "210";
            arrCabeceraFlexFil[0, 2] = "C";
            arrCabeceraFlexFil[0, 3] = "";
            arrCabeceraFlexFil[0, 4] = "c_apenom";

            arrCabeceraFlexFil[1, 0] = "Producto";
            arrCabeceraFlexFil[1, 1] = "210";
            arrCabeceraFlexFil[1, 2] = "C";
            arrCabeceraFlexFil[1, 3] = "";
            arrCabeceraFlexFil[1, 4] = "c_despro";
            
            arrCabeceraFlexFil[2, 0] = "Tarea";
            arrCabeceraFlexFil[2, 1] = "180";
            arrCabeceraFlexFil[2, 2] = "C";
            arrCabeceraFlexFil[2, 3] = "";
            arrCabeceraFlexFil[2, 4] = "c_des";

            arrCabeceraFlexFil[3, 0] = "Fecha Trabajo";
            arrCabeceraFlexFil[3, 1] = "70";
            arrCabeceraFlexFil[3, 2] = "F";
            arrCabeceraFlexFil[3, 3] = "";
            arrCabeceraFlexFil[3, 4] = "d_fchtar";

            arrCabeceraFlexFil[4, 0] = "Nº Sol. Tarea";
            arrCabeceraFlexFil[4, 1] = "100";
            arrCabeceraFlexFil[4, 2] = "C";
            arrCabeceraFlexFil[4, 3] = "";
            arrCabeceraFlexFil[4, 4] = "c_soltarnumdoc";
            
            arrCabeceraFlexFil[5, 0] = "Tipo";
            arrCabeceraFlexFil[5, 1] = "80";
            arrCabeceraFlexFil[5, 2] = "C";
            arrCabeceraFlexFil[5, 3] = "";
            arrCabeceraFlexFil[5, 4] = "c_tiptar";
                        
            arrCabeceraFlexFil[6, 0] = "Hor. Ini.";
            arrCabeceraFlexFil[6, 1] = "40";
            arrCabeceraFlexFil[6, 2] = "C";
            arrCabeceraFlexFil[6, 3] = "";
            arrCabeceraFlexFil[6, 4] = "c_horini";

            arrCabeceraFlexFil[7, 0] = "Hor. Ter.";
            arrCabeceraFlexFil[7, 1] = "40";
            arrCabeceraFlexFil[7, 2] = "C";
            arrCabeceraFlexFil[7, 3] = "";
            arrCabeceraFlexFil[7, 4] = "c_horter";

            arrCabeceraFlexFil[8, 0] = "Can. Prod.";
            arrCabeceraFlexFil[8, 1] = "50";
            arrCabeceraFlexFil[8, 2] = "D";
            arrCabeceraFlexFil[8, 3] = "0.00";
            arrCabeceraFlexFil[8, 4] = "n_can";

            arrCabeceraFlexFil[9, 0] = "Tiempo";
            arrCabeceraFlexFil[9, 1] = "45";
            arrCabeceraFlexFil[9, 2] = "C";
            arrCabeceraFlexFil[9, 3] = "";
            arrCabeceraFlexFil[9, 4] = "c_numhortra";

            arrCabeceraFlexFil[10, 0] = "Nº Horas";
            arrCabeceraFlexFil[10, 1] = "40";
            arrCabeceraFlexFil[10, 2] = "D";
            arrCabeceraFlexFil[10, 3] = "0.00";
            arrCabeceraFlexFil[10, 4] = "n_numhortra";

            arrCabeceraFlexFil[11, 0] = "Importe";
            arrCabeceraFlexFil[11, 1] = "60";
            arrCabeceraFlexFil[11, 2] = "D";
            arrCabeceraFlexFil[11, 3] = "0.00";
            arrCabeceraFlexFil[11, 4] = "n_imppaghrstra";

            xFunGen.Filtrar_Titulo = "TAREAS REALIZADAS X DIA";
            dtResult = xFunGen.MostrarDatos(arrCabeceraFlexFil, dtResult);          
        }
        public void ListarPendUni()
        {
            CD_pla_destajo miFun = new CD_pla_destajo();
            miFun.mysConec = mysConec;

            DataTable dtResult = new DataTable();
            CD_pla_destajo o_destajo = new CD_pla_destajo();
            string[,] arrCabeceraFlexFil = new string[6, 5];

            o_destajo.mysConec = mysConec;
            if (o_destajo.ListarPendUni() == true)
            {
                dtResult = o_destajo.dtLista;

                // FLEX GRID DE LOS TAREAS
                arrCabeceraFlexFil[0, 0] = "Fch Inici";
                arrCabeceraFlexFil[0, 1] = "70";
                arrCabeceraFlexFil[0, 2] = "F";
                arrCabeceraFlexFil[0, 3] = "";
                arrCabeceraFlexFil[0, 4] = "d_fchini";

                arrCabeceraFlexFil[1, 0] = "Fch. Termino";
                arrCabeceraFlexFil[1, 1] = "70";
                arrCabeceraFlexFil[1, 2] = "F";
                arrCabeceraFlexFil[1, 3] = "";
                arrCabeceraFlexFil[1, 4] = "d_fchfin";

                arrCabeceraFlexFil[2, 0] = "Empresa";
                arrCabeceraFlexFil[2, 1] = "300";
                arrCabeceraFlexFil[2, 2] = "C";
                arrCabeceraFlexFil[2, 3] = "";
                arrCabeceraFlexFil[2, 4] = "c_nomcorto";

                arrCabeceraFlexFil[3, 0] = "Local";
                arrCabeceraFlexFil[3, 1] = "200";
                arrCabeceraFlexFil[3, 2] = "C";
                arrCabeceraFlexFil[3, 3] = "";
                arrCabeceraFlexFil[3, 4] = "c_des";

                arrCabeceraFlexFil[4, 0] = "Sel.";
                arrCabeceraFlexFil[4, 1] = "40";
                arrCabeceraFlexFil[4, 2] = "B";
                arrCabeceraFlexFil[4, 3] = "";
                arrCabeceraFlexFil[4, 4] = "n_sel";

                arrCabeceraFlexFil[5, 0] = "IdPla";
                arrCabeceraFlexFil[5, 1] = "0";
                arrCabeceraFlexFil[5, 2] = "N";
                arrCabeceraFlexFil[5, 3] = "";
                arrCabeceraFlexFil[5, 4] = "n_id";

                xFunGen.Filtrar_CampoOrden = "d_fchini";
                xFunGen.Filtrar_Titulo = "Planillas Pendientes de Unificar";
                xFunGen.Filtrar_ColumnaCheck = 5;
                xFunGen.Filtrar_ColumnaBusqueda = 6;
                xFunGen.Filtrar_CampoBusqueda = "n_id";
                xFunGen.Filtrar_AplicarFiltro = true;
                dtLista = xFunGen.Filtrar(arrCabeceraFlexFil, dtResult);
            }
            return;
        }
        public bool PuedeImportarTareasParaPlanillaDestajo(int n_IdEmpresa, string c_FechaInicio, string c_FechaFinal, int n_IdLocal)
        {
            bool b_result = false;

            CD_pla_destajo miFun = new CD_pla_destajo();
            miFun.mysConec = mysConec;

            if (miFun.ListasTareasPlanilla(n_IdEmpresa, c_FechaInicio, c_FechaFinal, n_IdLocal) == true)
            {
                dtLista = miFun.dtLista;
                if (dtLista.Rows.Count != 0)
                {
                    b_result = true;
                }
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
