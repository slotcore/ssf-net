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
    public class CD_vta_itemcen
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

            DtResultado = xMiFuncion.StoreDTLLenar("vta_itemscen_select", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public BE_VTA_ITEMCEN TraerRegistro(int n_IdRegistro)
        {
            BE_VTA_ITEMCEN Ent_EmpTra = new BE_VTA_ITEMCEN();
            DatosMySql xMiFuncion = new DatosMySql();

            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT16", n_IdRegistro.ToString()}
                                      };
            DtResultado = xMiFuncion.StoreDTLLenar("vta_itemscen_obtenerregistro", arrParametros, mysConec);

            if (DtResultado.Rows.Count != 0)
            {
                Ent_EmpTra.n_id = Convert.ToInt16(DtResultado.Rows[0]["n_id"].ToString());
                Ent_EmpTra.n_idcli = Convert.ToInt16(DtResultado.Rows[0]["n_idcli"].ToString());
                Ent_EmpTra.n_idemp = Convert.ToInt16(DtResultado.Rows[0]["n_idemp"].ToString());
                Ent_EmpTra.n_iditem = Convert.ToInt16(DtResultado.Rows[0]["n_iditem"].ToString());
                Ent_EmpTra.c_codcen = DtResultado.Rows[0]["c_codcen"].ToString();
                Ent_EmpTra.c_descen = DtResultado.Rows[0]["c_descen"].ToString();
            }
            return Ent_EmpTra;
        }
        public bool Insertar(BE_VTA_ITEMCEN entItemCen)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();

            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            if (xMiFuncion.StoreEjecutar("vta_itemscen_insertar", entItemCen, mysConec, 1) == true)
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
        public bool Actualizar(BE_VTA_ITEMCEN entItemCen)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();

            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            if (xMiFuncion.StoreEjecutar("vta_itemscen_actualizar", entItemCen, mysConec, null) == true)
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

            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            booResult = xMiFuncion.StoreEjecutar("vta_itemscen_delete", arrParametros, mysConec);

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
