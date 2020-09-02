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
    public class CN_coo_puestos
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable Listar(int n_IdEmpresa)
        {
            DataTable dtResul = new DataTable();

            CD_coo_puestos miFun = new CD_coo_puestos();
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar(n_IdEmpresa);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public BE_COO_PUESTOS TraerRegistro(int n_IdRegistro)
        {
            BE_COO_PUESTOS entPuestos = new BE_COO_PUESTOS();
            CD_coo_puestos miFun = new CD_coo_puestos();
            DataTable DtResultado = new DataTable();
            
            miFun.mysConec = mysConec;
            DtResultado = miFun.TraerRegistro(n_IdRegistro);

            if (DtResultado.Rows.Count != 0)
            {
                entPuestos.n_idemp = Convert.ToInt32(DtResultado.Rows[0]["n_idemp"].ToString());
                entPuestos.n_id = Convert.ToInt32(DtResultado.Rows[0]["n_id"].ToString());
                entPuestos.n_idtippue = Convert.ToInt32(DtResultado.Rows[0]["n_idtippue"].ToString());
                entPuestos.c_puesto = DtResultado.Rows[0]["c_puesto"].ToString();
            }

            return entPuestos;
        }
        public bool Insertar(BE_COO_PUESTOS entPuesto)
        {
            bool b_result = false;
            BE_COO_PUESTOS entPuestos = new BE_COO_PUESTOS();
            CD_coo_puestos miFun = new CD_coo_puestos();
            
            miFun.mysConec = mysConec;
            b_result = miFun.Insertar(entPuesto);
            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return b_result;
        }
        public bool Actualizar(BE_COO_PUESTOS entPuesto)
        {
            bool b_result = false;
            BE_COO_PUESTOS entPuestos = new BE_COO_PUESTOS();
            CD_coo_puestos miFun = new CD_coo_puestos();

            miFun.mysConec = mysConec;
            b_result = miFun.Actualizar(entPuesto);
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
            CD_coo_puestos miFun = new CD_coo_puestos();

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
