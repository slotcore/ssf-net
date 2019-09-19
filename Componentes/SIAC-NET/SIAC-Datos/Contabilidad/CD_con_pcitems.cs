using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Contabilidad;
namespace SIAC_DATOS.Contabilidad
{
    public class CD_con_pcitems
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtLista = new DataTable();

        DatosMySql xMiFuncion = new DatosMySql();
        public void Listar(int n_IdEmpresa, int n_Tipo)
        {
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.STRING", n_IdEmpresa.ToString()},
                                            {"n_tipo", "System.STRING", n_Tipo.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("con_pcitems_listar", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public void TraerRegistro(int n_IdRegistro, int n_IdEmpresa)
        {
            DatosMySql xMiFuncion = new DatosMySql();
            string[,] arrParametros = new string[2, 3] {
                                            {"n_id", "System.INT32", n_IdRegistro.ToString()},
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()}
                                      };
            dtLista = xMiFuncion.StoreDTLLenar("con_pcitems_obtenerregistro", arrParametros, mysConec);
            if (xMiFuncion.booOcurrioError == true)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public bool Eliminar(int n_IdCargo)
        {
            DatosMySql xMiFuncion = new DatosMySql();
            bool booOk = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT16", n_IdCargo.ToString()}
                                      };

            if (xMiFuncion.StoreEjecutar("con_pcitems_delete", arrParametros, mysConec) == false)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                booOk = true;
            }

            return booOk;
        }
        public bool Insertar(BE_CON_PCITEMS e_Items)
        {
            bool booOk = false;

            DatosMySql xMiFuncion = new DatosMySql();

            if (xMiFuncion.StoreEjecutar("con_pcitems_insertar", e_Items, mysConec, null) == false)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                booOk = true;
            }
            return booOk;
        }
        public bool Actualizar(BE_CON_PCITEMS e_Items)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();

            if (xMiFuncion.StoreEjecutar("con_pcitems_actualizar", e_Items, mysConec, null) == false)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                booOk = true;
            }
            return booOk;
        }
        public void ListarItemCtaventa(int n_IdEmpresa)
        {
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.STRING", n_IdEmpresa.ToString()},
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("con_pcitems_listaritemctaventa", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public void Consulta1(int n_IdEmpresa)
        {
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.STRING", n_IdEmpresa.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("con_pcitems_consulta1", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public void Consulta2(int n_IdEmpresa)
        {
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.STRING", n_IdEmpresa.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("con_pcitems_consulta2", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        
    }
}
