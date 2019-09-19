using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Planillas;
using System.IO;

namespace SIAC_DATOS.Planilla
{
    public class CD_pla_marcacion
    {
        public DataTable dtLista;
        public DataTable dtRegistro;
        public DataTable dtMarcaciones;

        public bool b_OcurrioError;
        public string c_ErrorMensaje;
        public int n_ErrorNumber;

        public MySqlConnection mysConec;

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();
        Cls_IO funIO = new Cls_IO();

        public bool Listar(int n_IdEmpresa)
        {
            bool b_result = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT32",n_IdEmpresa.ToString()}
                                      };
            dtLista = xMiFuncion.StoreDTLLenar("pla_marcacion_listar", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                b_result = true;
            }
            return b_result;
        }
        public bool TraerRegistro(Int64 n_IdEmpresa)
        {
            bool b_result = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdEmpresa.ToString()}
                                      };
            dtRegistro = xMiFuncion.StoreDTLLenar("pla_marcacion_obtenerregistro", arrParametros, mysConec);
            if (xMiFuncion.IntErrorNumber == 0)
            {
                string[,] arrParametros2 = new string[1, 3] {
                                            {"n_idmar", "System.INT64", n_IdEmpresa.ToString()}
                                      };
                dtMarcaciones = xMiFuncion.StoreDTLLenar("pla_marcaciondet_listar", arrParametros2, mysConec);
                if (xMiFuncion.IntErrorNumber == 0)
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                }
                b_result = true;
            }
            else
            {
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
            }
            return b_result;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool booResult = false;
            MySqlTransaction trans;
                        
            // PARAMETROS PARA ELIMINAR LOS INSUMOS Y LAS TAREAS
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdRegistro.ToString()},
                                      };

            trans = mysConec.BeginTransaction();

            try
            {
                if (xMiFuncion.StoreEjecutar("pla_marcacion_delete", arrParametros, mysConec) == true)
                {
                    string[,] arrParametros2 = new string[1, 3] {
                                            {"n_idmar", "System.INT64", n_IdRegistro.ToString()},
                                      };
                    if (xMiFuncion.StoreEjecutar("pla_marcaciondet_delete", arrParametros2, mysConec) == false)
                    {
                        b_OcurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                        booResult = false;
                        trans.Rollback();
                        return booResult;
                    }
                    booResult = true;
                }
                else
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    booResult = false;
                    trans.Rollback();
                    return booResult;
                }
                trans.Commit();
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                booResult = false;
                trans.Rollback();
                return booResult;
            }
            return booResult;
        }
        public bool Insertar(BE_PLA_MARCACION entMarcacion, DataTable dtMarcaciones)
        {
            bool booOk = false;
            int n_row = 0;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans;

            trans = mysConec.BeginTransaction();

            try
            {
                if (xMiFuncion.StoreEjecutar("pla_marcacion_insertar", entMarcacion, mysConec, 1) == true)
                {
                    int n_id = Convert.ToInt32(xMiFuncion.intIdGenerado);
                    for (n_row = 0; n_row <= dtMarcaciones.Rows.Count - 1; n_row++)
                    {
                        dtMarcaciones.Rows[n_row]["n_idmar"] = n_id;
                    }

                    using (StreamWriter writer = new StreamWriter("C:\\SSF-Net\\marcacion.csv"))
                    {
                        funIO.DataTableToCVS(dtMarcaciones, writer, true);
                    }

                    string c_sql = "";

                    c_sql = "LOAD DATA LOCAL  INFILE 'C:/SSF-Net/marcacion.csv' " +
                      " INTO TABLE pla_marcaciondet FIELDS  terminated by ',' ENCLOSED BY '\"'  lines terminated by '\r\n' ignore 1 lines ";

                    if (xMiFuncion.EjecutarSQL(c_sql, mysConec)==false)
                    {
                        b_OcurrioError = xMiFuncion.booOcurrioError;
                        c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                        n_ErrorNumber = xMiFuncion.IntErrorNumber;
                        booOk = false;
                        trans.Rollback();
                        return booOk;
                    }

                    booOk = true;
                }
                else
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    booOk = false;
                    trans.Rollback();
                    return booOk;
                }
                //}
                trans.Commit();
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                booOk = false;
                trans.Rollback();
                return booOk;
            }

            return booOk;
        }
        public bool Actualizar(BE_PLA_MARCACION entMarcacion)
        {
            bool booOk = false;
            int n_row = 0;
            DatosMySql xMiFuncion = new DatosMySql();
            MySqlTransaction trans;

            trans = mysConec.BeginTransaction();
            try
            {
                //for (n_row = 0; n_row <= lstMarcacion.Count - 1; n_row++)
                //{
                if (xMiFuncion.StoreEjecutar("pla_marcacion_actualizar", entMarcacion, mysConec, null) == true)
                {
                    booOk = true;
                }
                else
                {
                    b_OcurrioError = xMiFuncion.booOcurrioError;
                    c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                    n_ErrorNumber = xMiFuncion.IntErrorNumber;
                    booOk = false;
                    trans.Rollback();
                    return booOk;
                }
                //}
                trans.Commit();
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                b_OcurrioError = xMiFuncion.booOcurrioError;
                c_ErrorMensaje = xMiFuncion.StrErrorMensaje;
                n_ErrorNumber = xMiFuncion.IntErrorNumber;
                booOk = false;
                trans.Rollback();
                return booOk;
            }

            return booOk;
        }
    }
}
