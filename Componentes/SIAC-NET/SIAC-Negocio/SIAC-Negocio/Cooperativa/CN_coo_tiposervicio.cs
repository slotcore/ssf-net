using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades;
using SIAC_DATOS.Cooperativa;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Cooperativa;

namespace SIAC_Negocio.Cooperativa
{
    public class CN_coo_tiposervicio
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;

        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();

        public BE_COO_TIPOSERVICIO entTipoServicio = new BE_COO_TIPOSERVICIO();

        public DataTable dtListar = new DataTable();
        public DataTable dtTipSer = new DataTable();
        public void Listar(int n_IdEmpresa)
        {
            CD_coo_tiposervicio miFun = new CD_coo_tiposervicio();
            miFun.mysConec = mysConec;

            miFun.Listar(n_IdEmpresa);
            dtListar = miFun.dtLista;

            if (miFun.booOcurrioError == true)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return;
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            CD_coo_tiposervicio miFun = new CD_coo_tiposervicio();
            DataTable DtResultado = new DataTable();

            miFun.mysConec = mysConec;
            miFun.TraerRegistro(n_IdRegistro);
            if (miFun.booOcurrioError == false)
            {
                DtResultado = miFun.dtTipSer;
                if (DtResultado.Rows.Count != 0)
                {
                    entTipoServicio.n_id = Convert.ToInt16(DtResultado.Rows[0]["n_id"].ToString());
                    entTipoServicio.c_des = DtResultado.Rows[0]["c_des"].ToString();
                }
            }
            return;
        }
        public bool Insertar(BE_COO_TIPOSERVICIO entTipoServicio)
        {
            bool b_result = false;
            CD_coo_tiposervicio miFun = new CD_coo_tiposervicio();

            miFun.mysConec = mysConec;
            b_result = miFun.Insertar(entTipoServicio);
            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return b_result;
        }
        public bool Actualizar(BE_COO_TIPOSERVICIO entTipoServicio)
        {
            bool b_result = false;
            CD_coo_tiposervicio miFun = new CD_coo_tiposervicio();

            miFun.mysConec = mysConec;
            b_result = miFun.Actualizar(entTipoServicio);
            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return b_result;
        }
        public bool Eliminar(int n_Idregistro)
        {
            bool b_result = false;
            CD_coo_tiposervicio miFun = new CD_coo_tiposervicio();

            miFun.mysConec = mysConec;
            b_result = miFun.Eliminar(n_Idregistro);
            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return b_result;
        }
    }
}
