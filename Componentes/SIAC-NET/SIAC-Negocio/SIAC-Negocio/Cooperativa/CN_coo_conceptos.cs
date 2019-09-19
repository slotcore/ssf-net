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
    public class CN_coo_conceptos
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();

        public BE_COO_CONCEPTOS entConceptos = new BE_COO_CONCEPTOS();

        public DataTable dtConceptos = new DataTable();
        public DataTable Listar(int n_IdEmpresa)
        {
            DataTable dtResul = new DataTable();

            CD_coo_conceptos miFun = new CD_coo_conceptos();
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
        public void TraerRegistro(int n_IdRegistro)
        {
            CD_coo_conceptos miFun = new CD_coo_conceptos();
            DataTable DtResultado = new DataTable();

            miFun.mysConec = mysConec;
            miFun.TraerRegistro(n_IdRegistro);
            if (miFun.booOcurrioError == false)
            {
                DtResultado = miFun.dtConceptos;
                if (DtResultado.Rows.Count != 0)
                {
                    entConceptos.n_idemp = Convert.ToInt16(DtResultado.Rows[0]["n_idemp"].ToString());
                    entConceptos.n_id = Convert.ToInt16(DtResultado.Rows[0]["n_id"].ToString());
                    entConceptos.c_cod = DtResultado.Rows[0]["c_cod"].ToString();
                    entConceptos.c_des = DtResultado.Rows[0]["c_des"].ToString();
                    entConceptos.n_imp = Convert.ToDouble(DtResultado.Rows[0]["n_imp"]);
                    entConceptos.n_afeigv = Convert.ToInt16(DtResultado.Rows[0]["n_afeigv"]);
                }
            }
            return;
        }
        public bool Insertar(BE_COO_CONCEPTOS entConceptos)
        {
            bool b_result = false;
            CD_coo_conceptos miFun = new CD_coo_conceptos();

            miFun.mysConec = mysConec;
            b_result = miFun.Insertar(entConceptos);
            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return b_result;
        }
        public bool Actualizar(BE_COO_CONCEPTOS entConceptos)
        {
            bool b_result = false;
            CD_coo_conceptos miFun = new CD_coo_conceptos();

            miFun.mysConec = mysConec;
            b_result = miFun.Actualizar(entConceptos);
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
            CD_coo_conceptos miFun = new CD_coo_conceptos();

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
