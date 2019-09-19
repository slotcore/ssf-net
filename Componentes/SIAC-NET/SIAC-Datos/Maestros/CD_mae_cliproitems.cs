using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Maestros;

namespace SIAC_DATOS.Maestros
{
    public class CD_mae_cliproitems
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public double n_IdGenerado = 0;
        public DataTable dtListar = new DataTable();
        public void Listar(int n_IdCliente)
        {
            DataTable DtResultado = new DataTable();
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idcli", "System.INT16", n_IdCliente.ToString()}
                                      };

            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);

            dtListar = xMiFuncion.StoreDTLLenar("mae_cliproitems_listar", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public bool Insertar(List<BE_MAE_CLIPROITEMS> l_ListaItems)
        {
            int n_row = 0;
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_idcli", "System.INT16", l_ListaItems[0].n_idcli.ToString()}
                                      };

            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);
            trans = mysConec.BeginTransaction();

            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);

            try
            {
                if (xMiFuncion.StoreEjecutar("mae_cliproitems_delete", arrParametros, mysConec) == true)
                {
                    for (n_row = 0; n_row <= l_ListaItems.Count - 1; n_row++)
                    {
                        if (xMiFuncion.StoreEjecutar("mae_cliproitems_insertar", l_ListaItems[n_row], mysConec, null) == true)
                        {
                            n_IdGenerado = xMiFuncion.intIdGenerado;
                            booOk = true;
                        }
                        else
                        {
                            trans.Rollback();
                            b_OcurrioError = xMiFuncion.booOcurrioError;
                            c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                            n_ErrorNumber = xMiFuncion.IntErrorNumber;
                            booOk = false;
                            return booOk;
                        }
                    }
                }
                else
                {
                    trans.Rollback();
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    booOk = false;
                    return booOk;
                }
                trans.Commit();
                return booOk;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                b_OcurrioError = true;
                c_ErrorMensaje = exc.Message.ToString();
                n_ErrorNumber = exc.HResult;
                trans.Rollback();
                booOk = false;
                return booOk;
            }
        }
    }
}
