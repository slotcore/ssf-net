using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Tesoreria;
using SIAC_DATOS.Contabilidad;

namespace SIAC_DATOS.Tesoreria
{
    public class CD_tes_ctacte
    {
        public MySqlConnection mysConec = new MySqlConnection();
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public int n_IdGenerado = 0;
        public bool b_DesdeOtraCapa = false;
        public DataTable DtLista = new DataTable();

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();

        public void ListarCtaCteCli(int n_IdEmpresa, string c_FechaInicio, string c_FechaTermino, int n_IdMoneda)
        {           
            string[,] arrParametros = new string[4, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"c_fchini", "System.STRING", c_FechaInicio.ToString()},
                                            {"c_fchfin", "System.STRING", c_FechaTermino.ToString()},
                                            {"n_idmon", "System.INT32", n_IdMoneda.ToString()}
                                      };

            DtLista = xMiFuncion.StoreDTLLenar("tes_ctactecli_resumen", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtLista = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public void ListarCtaCtePro(int n_IdEmpresa, string c_FechaInicio, string c_FechaTermino, int n_IdMoneda)
        {
            string[,] arrParametros = new string[4, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"c_fchini", "System.STRING", c_FechaInicio.ToString()},
                                            {"c_fchfin", "System.STRING", c_FechaTermino.ToString()},
                                            {"n_idmon", "System.INT32", n_IdMoneda.ToString()}
                                      };

            DtLista = xMiFuncion.StoreDTLLenar("tes_ctactepro_resumen", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtLista = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public void ListarCtaCteProDetalle(int n_IdEmpresa, string c_FechaInicio, string c_FechaTermino, int n_IdMoneda, int n_TipoConsulta, string c_ClientesConsultar)
        {
            string[,] arrParametros = new string[6, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"c_fchini", "System.STRING", c_FechaInicio.ToString()},
                                            {"c_fchfin", "System.STRING", c_FechaTermino.ToString()},
                                            {"n_idmon", "System.INT32", n_IdMoneda.ToString()},
                                            {"n_tipo", "System.INT32", n_TipoConsulta.ToString()},
                                            {"c_cadincli", "System.STRING", c_ClientesConsultar.ToString()}
                                      };

            DtLista = xMiFuncion.StoreDTLLenar("tes_ctactepro_detalle", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtLista = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
        public void ListarCtaCteCliDetalle(int n_IdEmpresa, string c_FechaInicio, string c_FechaTermino, int n_IdMoneda, int n_TipoConsulta, string c_ClientesConsultar)
        {
            string[,] arrParametros = new string[6, 3] {
                                            {"n_idemp", "System.INT32", n_IdEmpresa.ToString()},
                                            {"c_fchini", "System.STRING", c_FechaInicio.ToString()},
                                            {"c_fchfin", "System.STRING", c_FechaTermino.ToString()},
                                            {"n_idmon", "System.INT32", n_IdMoneda.ToString()},
                                            {"n_tipo", "System.INT32", n_TipoConsulta.ToString()},
                                            {"c_cadincli", "System.STRING", c_ClientesConsultar.ToString()}
                                      };

            DtLista = xMiFuncion.StoreDTLLenar("tes_ctactecli_detalle", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtLista = null;
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return;
        }
    }
}
