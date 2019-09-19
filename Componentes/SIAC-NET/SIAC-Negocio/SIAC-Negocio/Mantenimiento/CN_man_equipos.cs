using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades.Mantenimiento;
using SIAC_DATOS.Mantenimiento;
using MySql.Data.MySqlClient;
using Helper;

namespace SIAC_Negocio.Mantenimiento
{
    public class CN_man_equipos
    {
        private DataTable _dtLista;
        private DataTable _dtRegistro;

        private BE_MAN_EQUIPOS _entEquipos;

        private bool _b_OcurrioError;
        private string _c_ErrorMensaje;
        private int _n_ErrorNumber;

        private MySqlConnection _mysConec;

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();

        public BE_MAN_EQUIPOS entEquipos
        {
            get { return _entEquipos; }
            set { _entEquipos = value; }
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
        public MySqlConnection mysConec
        {
            get { return _mysConec; }
            set { _mysConec = value; }
        }
        public bool Listar(int n_IdEmpresa)
        {
            bool b_result = false;

            CD_man_equipos miFun = new CD_man_equipos();
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
            CD_man_equipos miFun = new CD_man_equipos();

            miFun.mysConec = mysConec;
            b_result = miFun.TraerRegistro(n_Idregistro);
            if (b_result == true)
            {
                dtResult = miFun.dtRegistro;
                BE_MAN_EQUIPOS entEqui = new BE_MAN_EQUIPOS();

                entEqui.n_idemp = Convert.ToInt32(dtResult.Rows[0]["n_idemp"]);
                entEqui.n_id = Convert.ToInt32(dtResult.Rows[0]["n_id"]);
                entEqui.c_des = dtResult.Rows[0]["c_des"].ToString();

                entEquipos = entEqui;
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
            CD_man_equipos miFun = new CD_man_equipos();

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
        public bool Insertar(BE_MAN_EQUIPOS entEquiposU)
        {
            CD_man_equipos miFun = new CD_man_equipos();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(entEquiposU);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_MAN_EQUIPOS entEquiposU)
        {
            CD_man_equipos miFun = new CD_man_equipos();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(entEquiposU);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
    }
}
