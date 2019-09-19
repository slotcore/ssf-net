using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Produccion;

namespace SIAC_DATOS.Produccion
{
    public class CD_pro_lineas
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public DataTable dtlineas = new DataTable();
        public DataTable dtlineasDet = new DataTable();
        public MySqlConnection mysConec = new MySqlConnection();

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();

        public bool obternerlineas(int n_IdEmpresa, int n_IdReceta)
        {
            DataTable DtResultado = new DataTable();
            bool booResult = false;

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT64",n_IdEmpresa.ToString()},
                                            {"n_idrec", "System.INT64",n_IdReceta.ToString()},
                                      };

            dtlineas = xMiFuncion.StoreDTLLenar("pro_lineas_obtenerlineas", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber == 0)
            {
                string[,] arrParametros2 = new string[1, 3] {
                                                {"n_idrec", "System.INT64",n_IdReceta.ToString()},
                                          };
                dtlineasDet = xMiFuncion.StoreDTLLenar("pro_lineasdet_listar", arrParametros2, mysConec);

                if (xMiFuncion.IntErrorNumber != 0)
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                }
                else
                {
                    booResult = true;
                }
            }
            else
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            
            return booResult;
        }
        public bool Eliminar(List<BE_PRO_LINEAS> lstLineas)
        {
            bool booResult = false;
            int n_row;

             MySqlTransaction trans;

            trans = mysConec.BeginTransaction();

            try
            {
                // RECORREMOS TODOS LAS LINEAS PARA ELIMINAR EL DETALLE 1 A 1
                for (n_row = 0; n_row <= lstLineas.Count - 1; n_row++)
                {
                    // PARAMETROS PARA ELIMINAR EL DETALLE DE LA LINEA
                    string[,] arrParametros = new string[1, 3] {
                                                {"n_idlin", "System.INT64", lstLineas[n_row].n_id.ToString()}
                                          };

                    booResult = xMiFuncion.StoreEjecutar("pro_Lineasdet_delete", arrParametros, mysConec);
                    if (booResult == true)
                    {
                        //  SI SE ELIMINO EL DETALLE, ELIMINAMOS LA CABECERA
                        string[,] arrParametros2 = new string[1, 3] {
                                                {"n_id", "System.INT64", lstLineas[n_row].n_id.ToString()}
                                          };
                        booResult = xMiFuncion.StoreEjecutar("pro_Lineas_delete", arrParametros2, mysConec);
                        if (booResult == false)
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            break;
                        }
                    }
                    else
                    {
                        booOcurrioError = xMiFuncion.booOcurrioError;
                        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        IntErrorNumber = xMiFuncion.IntErrorNumber;
                        trans.Rollback();
                        break;
                    }
                }
                trans.Commit();
                return booResult;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                trans.Rollback();
                return booResult;
            }
        }
    }
}
