using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades.Maestros;
using SIAC_Entidades.Planillas;
using SIAC_DATOS.Planilla;
using MySql.Data.MySqlClient;

namespace SIAC_Negocio.Planilla
{
    public class CN_pla_personal
    {
        private DataTable _dtLista;
        //private DataTable _dtRegistro;
        private BE_PLA_PERSONAL _entPersonal;
        //private List<BE_PLA_JORNALDESTAJODIA> _lstJornalDia;

        private bool _b_OcurrioError;
        private string _c_ErrorMensaje;
        private int _n_ErrorNumber;

        private MySqlConnection _mysConec;

        public DataTable dtLista
        {
            get { return _dtLista; }
            set { _dtLista = value; }
        }
        public BE_PLA_PERSONAL entPersonal
        {
            get { return _entPersonal; }
            set { _entPersonal = value; }
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

            CD_pla_personal miFun = new CD_pla_personal();
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
      
            CD_pla_personal miFun = new CD_pla_personal();

            miFun.mysConec = mysConec;
            b_result = miFun.TraerRegistro(n_Idregistro);
            if (b_result == true)
            {
                dtResult = miFun.dtRegistro;
                
                entPersonal.n_idemp = Convert.ToInt32(dtResult.Rows[0]["n_idemp"].ToString());
                entPersonal.n_id = Convert.ToInt32(dtResult.Rows[0]["n_id"].ToString());
                entPersonal.n_idtra = Convert.ToInt32(dtResult.Rows[0]["n_idtra"].ToString());
                entPersonal.n_idcargo = Convert.ToInt32(dtResult.Rows[0]["n_idcargo"].ToString());
                entPersonal.n_activo = Convert.ToInt32(dtResult.Rows[0]["n_activo"].ToString());
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
            CD_pla_personal miFun = new CD_pla_personal();

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
        public bool Insertar(BE_PLA_PERSONAL entPersonal)
        {
            CD_pla_personal miFun = new CD_pla_personal();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(entPersonal);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_PLA_PERSONAL entPersonal)
        {
            CD_pla_personal miFun = new CD_pla_personal();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(entPersonal);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
    }
}
