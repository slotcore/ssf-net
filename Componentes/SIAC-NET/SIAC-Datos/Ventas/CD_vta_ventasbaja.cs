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
    public class CD_vta_ventasbaja
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        DatosMySql xMiFuncion = new DatosMySql();

        public DataTable dtLista = new DataTable();
        public bool Correlativo(int n_IdEmpresa)
        {
            bool b_result = false;
            DatosMySql xMiFuncion = new DatosMySql();
            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);

            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT16",n_IdEmpresa.ToString()}
                                        };

            dtLista = xMiFuncion.StoreDTLLenar("vta_ventasbaja_correlativo", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtLista = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return b_result;
            }
            b_result = true;
            return b_result;
        }
        public bool BuscarArchivo(int n_IdEmpresa, string c_NombreArchivo)
        {
            bool b_result = false;
            DatosMySql xMiFuncion = new DatosMySql();
            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT16",n_IdEmpresa.ToString()},
                                            {"c_nomarc", "System.STRING",c_NombreArchivo.ToString()}
                                        };

            dtLista = xMiFuncion.StoreDTLLenar("vta_ventasbaja_buscararchivo", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                dtLista = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                return b_result;
            }
            b_result = true;
            return b_result;
        }
        public bool Insertar(BE_VTA_VENTASBAJA e_VentaBaja)
        {
            bool booOk = false;
            DatosMySql xMiFuncion = new DatosMySql();
            mysConec = xMiFuncion.ReAbrirConeccion(mysConec);

            if (xMiFuncion.StoreEjecutar("vta_ventasbaja_insertar", e_VentaBaja, mysConec,null) == true)
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
    }
}
