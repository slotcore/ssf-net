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
    public class CD_vta_tipexivender
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtLista = new DataTable();

        DatosMySql xMiFuncion = new DatosMySql();
        public void Listar(int n_idempresa)
        {
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT16",n_idempresa.ToString()}
                                      };

            dtLista = xMiFuncion.StoreDTLLenar("vta_tipexivender_select", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtLista = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public void TraerRegistro(int n_idEmpresa, int n_IdTipoExistencia)
        {
            BE_VTA_TIPEXIVENDER e_tipexitven = new BE_VTA_TIPEXIVENDER();
            DatosMySql xMiFuncion = new DatosMySql();

            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT16", n_idEmpresa.ToString()},
                                            {"n_idtipexi", "System.INT16", n_IdTipoExistencia.ToString()}
                                      };
            dtLista = xMiFuncion.StoreDTLLenar("vta_tipexivender_obtenerregistro", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtLista = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return;
        }
        public bool Insertar(BE_VTA_TIPEXIVENDER e_TipoExistenciaVender)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();

            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            if (xMiFuncion.StoreEjecutar("vta_tipexivender_insertar", e_TipoExistenciaVender, mysConec, 2) == true)
            {
                booOk = true;
            }
            else
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booOk;
        }
        public bool Actualizar(BE_VTA_TIPEXIVENDER e_TipoExistenciaVender)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();

            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            if (xMiFuncion.StoreEjecutar("vta_tipexivender_actualizar", e_TipoExistenciaVender, mysConec, null) == true)
            {
                booOk = true;
            }
            else
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booOk;
        }
        public bool Eliminar(int n_IdEmpresa, int n_IdTipoExistencia)
        {
            bool booResult = false;
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT16", n_IdEmpresa.ToString()},
                                            {"n_idtipexi", "System.INT16", n_IdTipoExistencia.ToString()}
                                      };

            DatosMySql FunMysql = new DatosMySql();
            mysConec = FunMysql.ReAbrirConeccion(mysConec);

            booResult = xMiFuncion.StoreEjecutar("vta_tipexivender_delete", arrParametros, mysConec);

            if (booResult == false)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booResult;
        }
    }
}
