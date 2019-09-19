using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades.Sunat;
using SIAC_DATOS.Sunat;
using MySql.Data.MySqlClient;

namespace SIAC_Negocio.Sunat
{
    public class CN_sun_conceptonc
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable Listar()
        {
            DataTable dtResul = new DataTable();

            CD_sun_conceptonc miFun = new CD_sun_conceptonc();
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar();

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public BE_SUN_CONCEPTONC TraerRegistro(int n_IdRegistro)
        {
            BE_SUN_CONCEPTONC Ent_ConceptpNC = new BE_SUN_CONCEPTONC();

            CD_sun_conceptonc miFun = new CD_sun_conceptonc();
            miFun.mysConec = mysConec;
            Ent_ConceptpNC = miFun.TraerRegistro(n_IdRegistro);

            return Ent_ConceptpNC;
        }
        public bool Insertar(BE_SUN_CONCEPTONC entConceptoNC)
        {
            BE_SUN_CONCEPTONC entNuevoConceptoNC = new BE_SUN_CONCEPTONC();
            CD_sun_conceptonc miFun = new CD_sun_conceptonc();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoConceptoNC.n_id = entConceptoNC.n_id;
            entNuevoConceptoNC.c_codsun = entConceptoNC.c_codsun;
            entNuevoConceptoNC.c_des = entConceptoNC.c_des;

            booOk = miFun.Insertar(entNuevoConceptoNC);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_SUN_CONCEPTONC entConceptoNC)
        {
            BE_SUN_CONCEPTONC entNuevoConceptoNC = new BE_SUN_CONCEPTONC();
            CD_sun_conceptonc miFun = new CD_sun_conceptonc();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoConceptoNC.n_id = entConceptoNC.n_id;
            entNuevoConceptoNC.c_codsun = entConceptoNC.c_codsun;
            entNuevoConceptoNC.c_des = entConceptoNC.c_des;

            booOk = miFun.Actualizar(entNuevoConceptoNC);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Eliminar(int n_Id)
        {
            CD_sun_conceptonc miFun = new CD_sun_conceptonc();
            bool booOk = false;

            miFun.mysConec = mysConec;

            booOk = miFun.Eliminar(n_Id);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
    }
}
