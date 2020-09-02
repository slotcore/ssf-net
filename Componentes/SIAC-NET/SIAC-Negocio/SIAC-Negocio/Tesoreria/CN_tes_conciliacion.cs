using MySql.Data.MySqlClient;
using SIAC_DATOS.Tesoreria;
using SIAC_DATOS.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using SIAC_Entidades.Tesoreria;
using Helper;

namespace SIAC_Negocio.Tesoreria
{
    public class CN_tes_conciliacion
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public BE_TES_CONCILIACION e_Conci = new BE_TES_CONCILIACION();
        Genericas funDatos = new Genericas();
        Helper.Comunes.Funciones funFun = new Helper.Comunes.Funciones();

        public DataTable dtLista = new DataTable();
        public DataTable dtListaDet = new DataTable();
        public void Listar(int n_IdEmpresa, int n_AñoTrabajo, int n_MesTrabajo)
        {
            CD_tes_conciliacion miFun = new CD_tes_conciliacion();
            miFun.mysConec = mysConec;

            miFun.Listar(n_IdEmpresa, n_AñoTrabajo, n_MesTrabajo);
            dtLista = miFun.dtLista;

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
            bool booResult;
            CD_tes_conciliacion miFun = new CD_tes_conciliacion();

            miFun.mysConec = mysConec;

            booResult = miFun.TraerRegistro(n_IdRegistro);
            dtResult = miFun.dtLista;
            dtListaDet = miFun.dtListaDet;
            if (miFun.b_OcurrioError == false)
            {
                if (dtResult.Rows.Count != 0)
                {
                    e_Conci.n_idemp = Convert.ToInt32(dtResult.Rows[0]["n_idemp"]);
                    e_Conci.n_id = Convert.ToInt32(dtResult.Rows[0]["n_id"]);
                    e_Conci.n_idmes = Convert.ToInt32(dtResult.Rows[0]["n_idmes"]);
                    e_Conci.n_idcue = Convert.ToInt32(dtResult.Rows[0]["n_idcue"]);
                    e_Conci.n_saliniban = Convert.ToDouble(dtResult.Rows[0]["n_saliniban"]);
                    e_Conci.n_salfinban = Convert.ToDouble(funFun.NulosN(dtResult.Rows[0]["n_salfinban"]));
                    e_Conci.d_fchcon = Convert.ToDateTime(dtResult.Rows[0]["d_fchcon"]);
                    e_Conci.c_glo = dtResult.Rows[0]["c_glo"].ToString();
                    e_Conci.n_idper = Convert.ToInt32(dtResult.Rows[0]["n_idper"]);
                    e_Conci.n_salfin = Convert.ToDouble(funFun.NulosN(dtResult.Rows[0]["n_salfin"]));
                    e_Conci.n_toting = Convert.ToDouble(funFun.NulosN(dtResult.Rows[0]["n_toting"]));
                    e_Conci.n_totegr = Convert.ToDouble(funFun.NulosN(dtResult.Rows[0]["n_totegr"]));
                }
            }
            else
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool booResult = false;
            CD_tes_conciliacion miFun = new CD_tes_conciliacion();

            miFun.mysConec = mysConec;

            booResult = miFun.Eliminar(n_IdRegistro);
            if (booResult == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return booResult;
        }
        public bool Insertar(BE_TES_CONCILIACION e_Conciliacion, List<BE_TES_CONCILIACIONDET> l_ConciliacionDetalle)
        {
            CD_tes_conciliacion miFun = new CD_tes_conciliacion();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(e_Conciliacion, l_ConciliacionDetalle);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_TES_CONCILIACION e_Conciliacion, List<BE_TES_CONCILIACIONDET> l_ConciliacionDetalle)
        {
            CD_tes_conciliacion miFun = new CD_tes_conciliacion();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(e_Conciliacion, l_ConciliacionDetalle);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public void TraerParaConciliacion(int n_IdEmpresa, int n_MesTrabajo, int n_IdBanco, int n_AnoTrabajo)
        {
            CD_tes_conciliacion miFun = new CD_tes_conciliacion();
            miFun.mysConec = mysConec;

            miFun.TraerParaConciliacion(n_IdEmpresa, n_MesTrabajo, n_IdBanco, n_AnoTrabajo);
            dtLista = miFun.dtLista;
            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return;
        }
        public DataTable PendientesOtrosMeses(int n_IdEmpresa, int n_IdCuentaBanco, int n_IdPeriodo)
        {
            DataTable dtResult = new DataTable();
            CD_tes_conciliacion o_Conci = new CD_tes_conciliacion();
            string[,] arrCabeceraFlex1 = new string[12, 5];
            string c_Fecha = "01/" + n_IdPeriodo.ToString("00") + "/" + STU_SISTEMA.ANOTRABAJO;
            
            o_Conci.mysConec = mysConec;
            o_Conci.TraerPenidentesOtroMes(n_IdEmpresa, n_IdCuentaBanco, c_Fecha);
            dtResult = o_Conci.dtLista;

            // FLEX GRID DE LOS TAREAS
            arrCabeceraFlex1[0, 0] = "Nº Registro";
            arrCabeceraFlex1[0, 1] = "60";
            arrCabeceraFlex1[0, 2] = "C";
            arrCabeceraFlex1[0, 3] = "";
            arrCabeceraFlex1[0, 4] = "c_numreg";

            arrCabeceraFlex1[1, 0] = "T.D.";
            arrCabeceraFlex1[1, 1] = "40";
            arrCabeceraFlex1[1, 2] = "C";
            arrCabeceraFlex1[1, 3] = "";
            arrCabeceraFlex1[1, 4] = "c_abr";

            arrCabeceraFlex1[2, 0] = "Nº Documento";
            arrCabeceraFlex1[2, 1] = "110";
            arrCabeceraFlex1[2, 2] = "C";
            arrCabeceraFlex1[2, 3] = "";
            arrCabeceraFlex1[2, 4] = "c_openumdoc";

            arrCabeceraFlex1[3, 0] = "Fch. Operacion";
            arrCabeceraFlex1[3, 1] = "70";
            arrCabeceraFlex1[3, 2] = "F";
            arrCabeceraFlex1[3, 3] = "dd/MM/yyyy";
            arrCabeceraFlex1[3, 4] = "d_fchope";

            arrCabeceraFlex1[4, 0] = "Medio Pago";
            arrCabeceraFlex1[4, 1] = "150";
            arrCabeceraFlex1[4, 2] = "C";
            arrCabeceraFlex1[4, 3] = "";
            arrCabeceraFlex1[4, 4] = "c_des3";

            arrCabeceraFlex1[5, 0] = "Glosa";
            arrCabeceraFlex1[5, 1] = "150";
            arrCabeceraFlex1[5, 2] = "C";
            arrCabeceraFlex1[5, 3] = "";
            arrCabeceraFlex1[5, 4] = "c_glo";

            arrCabeceraFlex1[6, 0] = "Debe";
            arrCabeceraFlex1[6, 1] = "70";
            arrCabeceraFlex1[6, 2] = "D";
            arrCabeceraFlex1[6, 3] = "0.00";
            arrCabeceraFlex1[6, 4] = "n_debe";

            arrCabeceraFlex1[7, 0] = "Haber";
            arrCabeceraFlex1[7, 1] = "70";
            arrCabeceraFlex1[7, 2] = "D";
            arrCabeceraFlex1[7, 3] = "0.00";
            arrCabeceraFlex1[7, 4] = "n_haber";

            arrCabeceraFlex1[8, 0] = "Check";
            arrCabeceraFlex1[8, 1] = "40";
            arrCabeceraFlex1[8, 2] = "B";
            arrCabeceraFlex1[8, 3] = "";
            arrCabeceraFlex1[8, 4] = "b_check";

            arrCabeceraFlex1[9, 0] = "n_idtes";
            arrCabeceraFlex1[9, 1] = "0";
            arrCabeceraFlex1[9, 2] = "N";
            arrCabeceraFlex1[9, 3] = "";
            arrCabeceraFlex1[9, 4] = "n_idtes";
            
            arrCabeceraFlex1[10, 0] = "n_idori";
            arrCabeceraFlex1[10, 1] = "0";
            arrCabeceraFlex1[10, 2] = "N";
            arrCabeceraFlex1[10, 3] = "";
            arrCabeceraFlex1[10, 4] = "n_idori";

            arrCabeceraFlex1[11, 0] = "Indice";
            arrCabeceraFlex1[11, 1] = "0";
            arrCabeceraFlex1[11, 2] = "C";
            arrCabeceraFlex1[11, 3] = "";
            arrCabeceraFlex1[11, 4] = "c_indice";

            funDatos.Filtrar_CampoOrden = "d_fchope";
            funDatos.Filtrar_Titulo = "Operaciones Pendientes de Conciliacion";
            funDatos.Filtrar_ColumnaCheck = 9;
            funDatos.Filtrar_ColumnaBusqueda = 12;
            funDatos.Filtrar_CampoBusqueda = "c_indice";
            funDatos.Filtrar_AplicarFiltro = true;
            dtResult = funDatos.Filtrar(arrCabeceraFlex1, dtResult);

            return dtResult;
        }
        public void ImprimirConciliacion(int n_Idregistro)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[4, 3];

            arrPara[0, 0] = "n_idemp";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = STU_SISTEMA.EMPRESAID.ToString();

            arrPara[1, 0] = "n_id";
            arrPara[1, 1] = "N";
            arrPara[1, 2] = n_Idregistro.ToString();

            arrPara[2, 0] = "c_apenom";
            arrPara[2, 1] = "C";
            arrPara[2, 2] = STU_SISTEMA.EMPRESANOMBRE;

            arrPara[3, 0] = "c_empnumdoc";
            arrPara[3, 1] = "C";
            arrPara[3, 2] = STU_SISTEMA.EMPRESARUC;

            c_NomArchivo = "Rpt_Conciliacion.rpt";
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "tesoreria\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "TESORERIA - CONCILIACION BANCARIA";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
    }
}
