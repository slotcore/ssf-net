using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Ventas;

namespace SIAC_DATOS.Ventas
{
    public class CD_vta_punvencli
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        DatosMySql xMiFuncion = new DatosMySql();
        public DataTable Listar()
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[0, 3] {
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("vta_punvencli_select", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public DataTable Listar2(int n_TipoRegistro)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[1, 3] {
                                       { "n_tipreg", "System.INT16", n_TipoRegistro.ToString() }
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("vta_punvencli_select2", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public BE_VTA_PUNVENCLI TraerRegistro(int n_IdRegistro)
        {
            BE_VTA_PUNVENCLI Ent_PunVenCli = new BE_VTA_PUNVENCLI();
            DatosMySql xMiFuncion = new DatosMySql();

            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT16", n_IdRegistro.ToString()}
                                      };
            DtResultado = xMiFuncion.StoreDTLLenar("vta_punvencli_obtenerregistro", arrParametros, mysConec);

            if (DtResultado.Rows.Count != 0)
            {
                Ent_PunVenCli.n_idemp = Convert.ToInt32(DtResultado.Rows[0]["n_idemp"].ToString());
                Ent_PunVenCli.n_idcli = Convert.ToInt32(DtResultado.Rows[0]["n_idcli"].ToString());
                Ent_PunVenCli.n_id = Convert.ToInt32(DtResultado.Rows[0]["n_id"].ToString());
                Ent_PunVenCli.c_codcen = DtResultado.Rows[0]["c_codcen"].ToString();
                Ent_PunVenCli.c_des = DtResultado.Rows[0]["c_des"].ToString();
                Ent_PunVenCli.c_dir = DtResultado.Rows[0]["c_dir"].ToString();
                Ent_PunVenCli.n_iddis = Convert.ToInt32(DtResultado.Rows[0]["n_iddis"].ToString());
                Ent_PunVenCli.n_idpro = Convert.ToInt32(DtResultado.Rows[0]["n_idpro"].ToString());
                Ent_PunVenCli.n_iddep = Convert.ToInt32(DtResultado.Rows[0]["n_iddep"].ToString());
            }
            return Ent_PunVenCli;
        }
        public bool Insertar(BE_VTA_PUNVENCLI entPuntoVenta)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();

            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            if (xMiFuncion.StoreEjecutar("vta_punvencli_insertar", entPuntoVenta, mysConec, 2) == true)
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
        public bool Actualizar(BE_VTA_PUNVENCLI entPuntoVenta)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();

            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            if (xMiFuncion.StoreEjecutar("vta_punvencli_actualizar", entPuntoVenta, mysConec, null) == true)
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
            //alm_inventario_delete
            //DataTable DtResultado = new DataTable();
            bool booResult = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT16", n_IdItem.ToString()}
                                      };

            booResult = xMiFuncion.StoreEjecutar("vta_punvencli_delete", arrParametros, mysConec);

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
