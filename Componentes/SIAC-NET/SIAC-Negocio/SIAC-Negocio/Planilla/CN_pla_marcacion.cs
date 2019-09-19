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
    public class CN_pla_marcacion
    {
        public DataTable dtLista;
        public DataTable _dtRegistro;
        public DataTable dtMarcaciones;

        public BE_PLA_MARCACION entMarcacion = new BE_PLA_MARCACION();

        public bool b_OcurrioError;
        public string c_ErrorMensaje;
        public int n_ErrorNumber;

        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();
        Helper.Comunes.Funciones xFunciones = new Helper.Comunes.Funciones();

        public bool Listar(int n_IdEmpresa)
        {
            bool b_result = false;

            CD_pla_marcacion miFun = new CD_pla_marcacion();
            miFun.mysConec = mysConec;

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
        public bool TraerRegistro(int n_Idregistro)
        {
            bool b_result = false;
            DataTable dtResult = new DataTable();
            DataTable dtResultLis = new DataTable();
            CD_pla_marcacion miFun = new CD_pla_marcacion();
            
            miFun.mysConec = mysConec;
            b_result = miFun.TraerRegistro(n_Idregistro);
           
            if (b_result == true)
            {
                dtResult = miFun.dtRegistro;

                entMarcacion.n_idemp = Convert.ToInt32(dtResult.Rows[0]["n_idemp"]);
                entMarcacion.n_id =  Convert.ToInt32(dtResult.Rows[0]["n_id"]);
                entMarcacion.d_fchini = Convert.ToDateTime(dtResult.Rows[0]["d_fchini"]);
                entMarcacion.d_fchfin = Convert.ToDateTime(dtResult.Rows[0]["d_fchfin"]);
                entMarcacion.d_fchimp = Convert.ToDateTime(dtResult.Rows[0]["d_fchimp"]);
                entMarcacion.n_idper =  Convert.ToInt32(dtResult.Rows[0]["n_idper"]);
                entMarcacion.n_nummar =  Convert.ToInt32(dtResult.Rows[0]["n_nummar"]);

                dtMarcaciones = miFun.dtMarcaciones;
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
            CD_pla_marcacion miFun = new CD_pla_marcacion();

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
        public bool Insertar(BE_PLA_MARCACION entMarcacion, DataTable dtMarcaciones)
        {
            CD_pla_marcacion miFun = new CD_pla_marcacion();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(entMarcacion, dtMarcaciones);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_PLA_MARCACION entMarcacion)
        {
            CD_pla_marcacion miFun = new CD_pla_marcacion();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(entMarcacion);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
    }
}
