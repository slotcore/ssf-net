using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Sunat;

namespace SIAC_DATOS.Sunat
{
    public class CD_sun_conceptonc
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        DatosMySql xMiFuncion = new DatosMySql();
        public DataTable Listar()
        {
            DataTable DtResultado = new DataTable();
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[1, 3] {
                                            {"", "",""}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("sun_conceptoncnd_select", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public BE_SUN_CONCEPTONC TraerRegistro(int n_IdRegistro)
        {
            BE_SUN_CONCEPTONC Ent_ConceptpNC = new BE_SUN_CONCEPTONC();
            DatosMySql xMiFuncion = new DatosMySql();

            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT16", n_IdRegistro.ToString()}
                                      };
            DtResultado = xMiFuncion.StoreDTLLenar("sun_conceptoncnd_obtenerregistro", arrParametros, mysConec);

            if (DtResultado.Rows.Count != 0)
            {
                Ent_ConceptpNC.n_id = Convert.ToInt16(DtResultado.Rows[0]["n_id"].ToString());
                Ent_ConceptpNC.c_codsun = DtResultado.Rows[0]["c_codsun"].ToString();
                Ent_ConceptpNC.c_des = DtResultado.Rows[0]["c_des"].ToString();
                Ent_ConceptpNC.n_tipo = Convert.ToInt16(DtResultado.Rows[0]["n_tipo"].ToString());
                Ent_ConceptpNC.n_hackar = Convert.ToInt16(DtResultado.Rows[0]["n_hackar"].ToString());
            }
            return Ent_ConceptpNC;
        }
        public bool Insertar(BE_SUN_CONCEPTONC entConceptoNC)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();

            if (xMiFuncion.StoreEjecutar("sun_conceptoncnd_insertar", entConceptoNC, mysConec, 0) == true)
            {
                booOk = true;
            }
            else
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booOk;
        }
        public bool Actualizar(BE_SUN_CONCEPTONC entConceptoNC)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();

            if (xMiFuncion.StoreEjecutar("sun_conceptoncnd_actualizar", entConceptoNC, mysConec, null) == true)
            {
                booOk = true;
            }
            else
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booOk;
        }
        public bool Eliminar(int n_IdItem)
        {
            bool booResult = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT16", n_IdItem.ToString()}
                                      };

            booResult = xMiFuncion.StoreEjecutar("sun_conceptoncnd_delete", arrParametros, mysConec);

            if (booResult == false)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booResult;
        }
    }
}
