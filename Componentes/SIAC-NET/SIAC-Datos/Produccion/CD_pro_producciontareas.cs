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
    public class CD_pro_producciontareas
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;

        public DataTable dtListar = new DataTable();
        public DataTable dtListar2 = new DataTable();
        public DataTable dtListarPersonal = new DataTable();

        public MySqlConnection mysConec = new MySqlConnection();

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();

        public bool Listar(int n_IdProduccion)
        {
            bool b_result = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idpro", "System.INT64",n_IdProduccion.ToString()}
                                      };
            dtListar = xMiFuncion.StoreDTLLenar("pro_producciontareas_listar", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber == 0)
            {
                dtListarPersonal = xMiFuncion.StoreDTLLenar("pro_producciontareasdet_listar", arrParametros, mysConec);
                if (xMiFuncion.IntErrorNumber == 0)
                {
                    b_result = true;
                }
                else
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                }
            }
            else
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return b_result;
        }
        public bool Actualizar(List<BE_PRO_PRODUCCIONTAREAS> lstTar, List<BE_PRO_PRODUCCIONTAREASDET> lstTarDet)
        {
            bool booOk = false;
            int n_row = 0;
            int n_idproduccion = 0;
            DatosMySql xMiFuncion = new DatosMySql();

            n_idproduccion = lstTar[0].n_idpro;
            if (Eliminar(n_idproduccion) == false)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                return booOk;
            }

            for (n_row = 0; n_row <= lstTar.Count - 1; n_row++)
            { 
                BE_PRO_PRODUCCIONTAREAS entTarea = new BE_PRO_PRODUCCIONTAREAS();

                entTarea = lstTar[n_row];
                if (xMiFuncion.StoreEjecutar("pro_producciontareas_insertar", entTarea, mysConec, 3) == false)
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                    return booOk;
                }
            }

            for (n_row = 0; n_row <= lstTarDet.Count - 1; n_row++)
            {
                BE_PRO_PRODUCCIONTAREASDET entTareaDet = new BE_PRO_PRODUCCIONTAREASDET();
                entTareaDet = lstTarDet[n_row];
                entTareaDet.n_idpro = lstTar[0].n_idpro;
                if (xMiFuncion.StoreEjecutar("pro_producciontareasdet_insertar", entTareaDet, mysConec, null) == false)
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                    return booOk;
                }
                //booOk = true;
            }

            // *******************************************************
            // ACTUALIZAMOS EL PRECIO DE PAGO DE LAS TAREAS INGRESADAS
            DataTable dtResult = new DataTable();
            int n_row2 = 0;

            for (n_row2 = 0; n_row2 <= lstTar.Count - 1; n_row2++)
            { 
                string[,] arrParametros = new string[2, 3] {
                                                {"n_idpro", "System.INT64",lstTar[n_row2].n_idpro.ToString()},
                                                {"n_idtar", "System.INT64",lstTar[n_row2].n_idtar.ToString()}
                                          };

                dtListar = xMiFuncion.StoreDTLLenar("pro_producciontareasdet_calcularpago", arrParametros, mysConec);

                if (xMiFuncion.IntErrorNumber == 0)
                {                                     
                    for (n_row = 0; n_row <= dtListar.Rows.Count - 1; n_row++)
                    {
                        string[,] arrParametros2 = new string[12, 3] {
                                                {"n_idpro", "System.INT64",lstTar[n_row2].n_idpro.ToString()},
                                                {"n_idtar", "System.INT64",lstTar[n_row2].n_idtar.ToString()},
                                                {"n_idper", "System.INT64",dtListar.Rows[n_row]["n_idper"].ToString()},
                                                {"n_id", "System.INT64",dtListar.Rows[n_row]["n_id"].ToString()},
                                                {"n_numhortra", "System.DOUBLE",dtListar.Rows[n_row]["n_numhortra"].ToString()},
                                                {"c_numhortra", "System.STRING",dtListar.Rows[n_row]["c_hortra"].ToString()},
                                                {"n_canhorper", "System.DOUBLE",dtListar.Rows[n_row]["n_canhorper"].ToString()},
                                                {"n_canhorpermax", "System.DOUBLE",dtListar.Rows[n_row]["n_canhorpermax"].ToString()},
                                                {"n_preunidad", "System.DOUBLE",dtListar.Rows[n_row]["n_preunidad"].ToString()},
                                                {"n_pagocal", "System.DOUBLE",dtListar.Rows[n_row]["n_pagocal"].ToString()},
                                                {"n_pagproyhor", "System.DOUBLE",dtListar.Rows[n_row]["n_pagproyhor"].ToString()},
                                                {"n_subsidio", "System.DOUBLE",dtListar.Rows[n_row]["n_subsidio"].ToString()}
                                          };

                        if (xMiFuncion.StoreEjecutar("pro_producciontareasdet_actualizarpago", arrParametros2, mysConec) == false)
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            return booOk;
                        }
                    }
                }
            }
            booOk = true;
            return booOk;
        }
        public bool Eliminar(int n_IdProduccion)
        {
            bool booResult = false;
            // PARAMETROS PARA ELIMINAR LOS INSUMOS Y LAS TAREAS
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idpro", "System.INT64", n_IdProduccion.ToString()}
                                      };

            booResult = xMiFuncion.StoreEjecutar("pro_producciontareasdet_delete", arrParametros, mysConec);
            if (booResult == true)
            {
                //booResult = xMiFuncion.StoreEjecutar("pro_producciontareas_delete", arrParametros, mysConec);
                //if (booResult == false)
                //{
                //    booOcurrioError = xMiFuncion.booOcurrioError;
                //    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                //    IntErrorNumber = xMiFuncion.IntErrorNumber;
                //}
                booResult = true;
            }
            else
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return booResult;
        }
        public bool ProcesarPagos(int n_IdEmpresa, string c_FechaInicio, string c_FechaTermino)
        {
            bool b_result = false;
            DateTime today = Convert.ToDateTime(c_FechaInicio);
            string c_fch1 = today.AddDays(0).ToString("dd/MM/yyyy");
            string c_fch2 = today.AddDays(1).ToString("dd/MM/yyyy");
            string c_fch3 = today.AddDays(2).ToString("dd/MM/yyyy");
            string c_fch4 = today.AddDays(3).ToString("dd/MM/yyyy");
            string c_fch5 = today.AddDays(4).ToString("dd/MM/yyyy");
            string c_fch6 = today.AddDays(5).ToString("dd/MM/yyyy");

            string[,] arrParametros = new string[9, 3] {
                                            {"n_idemp", "System.INT64",n_IdEmpresa.ToString()},
                                            {"c_fchini", "System.STRING",c_FechaInicio.ToString()},
                                            {"c_fchter", "System.STRING",c_FechaTermino.ToString()},
                                            {"c_fch1", "System.STRING",c_fch1.ToString()},
                                            {"c_fch2", "System.STRING",c_fch2.ToString()},
                                            {"c_fch3", "System.STRING",c_fch3.ToString()},
                                            {"c_fch4", "System.STRING",c_fch4.ToString()},
                                            {"c_fch5", "System.STRING",c_fch5.ToString()},
                                            {"c_fch6", "System.STRING",c_fch6.ToString()},
                                      };
            dtListar = xMiFuncion.StoreDTLLenar("pro_producciontareasdet_pagosemanal", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber == 0)
            {
                dtListar2 = xMiFuncion.StoreDTLLenar("pro_producciontareasdet_pagosemanal_horas", arrParametros, mysConec);
                if (xMiFuncion.IntErrorNumber == 0)
                {
                    b_result = true;
                }
                else
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                }
            }
            else
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return b_result;
        }
    }
}
