using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Almacen;

namespace SIAC_DATOS.Almacen
{
    public class CD_alm_inventario
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        
        DatosMySql xMiFuncion = new DatosMySql();

        public DataTable Listar(int n_IdEmpresa, int n_EsUnificado, int n_VerActivos)
        {
            DataTable DtResultado = new DataTable();
            
            string[,] arrParametros = new string[3, 3] {
                                            {"n_idemp", "System.INT64",n_IdEmpresa.ToString()},
                                            {"n_esuni", "System.INT64",n_EsUnificado.ToString()},
                                            {"n_veract", "System.INT64",n_VerActivos.ToString()}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("alm_inventario_listar", arrParametros, mysConec);
            
            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public DataTable Listar()
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[0, 3] {
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("alm_inventario_listar2", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public DataTable Stock(int n_IdEmpresa, int n_AnoTrabajo)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT32",n_IdEmpresa.ToString()},
                                            {"n_anotra", "System.INT32",n_AnoTrabajo.ToString()}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("alm_inventario_stock", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }
        public DataTable Listar_Tabla(int n_idempresa)
        {
            DataTable DtResultado = new DataTable();
            
            string[,] arrParametros = new string[1, 3] {
                                            {"n_idemp", "System.INT64",n_idempresa.ToString()}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("alm_inventario_listar_tabla", arrParametros, mysConec);
            
            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }       
        public BE_ALM_INVENTARIO_CONSULTA TraerRegistro(Int64 n_IdRegistro)
        {
            BE_ALM_INVENTARIO_CONSULTA EntInventario = new BE_ALM_INVENTARIO_CONSULTA();
            List<BE_ALM_INVENTARIOUNIMED_CONSULTA> LstUnidades = new List<BE_ALM_INVENTARIOUNIMED_CONSULTA>();
           
            
            DataTable DtResultado = new DataTable();
            DataTable DtUnidadMedida = new DataTable();
            Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();

            string[,] arrParametros = new string[1, 3] {
                                            {"n_idregistro", "System.INT64", n_IdRegistro.ToString()}
                                      };
            DtResultado = xMiFuncion.StoreDTLLenar("alm_inventario_obtenerregistro", arrParametros, mysConec);

            string[,] arrParametrosUniMed = new string[1, 3] {
                                            {"n_iditem", "System.INT64", n_IdRegistro.ToString()}
                                      };
            DtUnidadMedida = xMiFuncion.StoreDTLLenar("alm_inventariounimed_obtener_x_item", arrParametrosUniMed, mysConec);

            if (DtUnidadMedida.Rows.Count != 0)
            {
                foreach (DataRow dr in DtUnidadMedida.Rows)
                {
                     BE_ALM_INVENTARIOUNIMED_CONSULTA Unidades = new BE_ALM_INVENTARIOUNIMED_CONSULTA();

                    Unidades.n_idite = Convert.ToInt64(dr["n_idite"].ToString());
                    Unidades.n_id = Convert.ToInt32(dr["n_id"].ToString());
                    Unidades.c_despre = dr["c_despre"].ToString();
                    Unidades.c_abrpre = dr["c_abrpre"].ToString();
                    Unidades.n_idunimedbas = Convert.ToInt32(dr["n_idunimedbas"].ToString());
                    Unidades.n_canunimedbas = Convert.ToDouble(dr["n_canunimedbas"].ToString());
                    Unidades.n_default = Convert.ToInt32(dr["n_default"].ToString());
                    Unidades.n_preuni = Convert.ToDouble(dr["n_preuni"].ToString());
                    Unidades.n_preuniigv = Convert.ToDouble(dr["n_preuniigv"].ToString());
		            
                    //Unidades.n_idunimed = Convert.ToInt32(dr["n_idunimed"]);
                    //Unidades.c_abrsun = dr["c_abrsun"].ToString();
                    //Unidades.c_dessun = dr["c_dessun"].ToString();
                    //Unidades.c_desunimedbas = dr["c_desunimedbas"].ToString();
                    LstUnidades.Add(Unidades);
                }
            }

            if (DtResultado.Rows.Count != 0)
            {
                EntInventario.n_idemp = Convert.ToInt32(DtResultado.Rows[0]["n_idemp"].ToString());
                EntInventario.n_id = Convert.ToInt32(DtResultado.Rows[0]["n_id"].ToString());
                EntInventario.n_idtipexi = Convert.ToInt32(DtResultado.Rows[0]["n_idtipexi"].ToString());
                //EntInventario.n_idtippro = Convert.ToInt32(DtResultado.Rows[0]["n_idtippro"].ToString());
                EntInventario.n_idfam = Convert.ToInt32(DtResultado.Rows[0]["n_idfam"].ToString());
                EntInventario.n_idclas = Convert.ToInt32(DtResultado.Rows[0]["n_idclas"].ToString());
                EntInventario.n_idsubclas = Convert.ToInt32(DtResultado.Rows[0]["n_idsubclas"].ToString());
                EntInventario.c_codpro = DtResultado.Rows[0]["c_codpro"].ToString();
                EntInventario.c_despro = DtResultado.Rows[0]["c_despro"].ToString();
                EntInventario.c_destec = DtResultado.Rows[0]["c_destec"].ToString();
                EntInventario.c_descar = DtResultado.Rows[0]["c_descar"].ToString();
                EntInventario.n_stkini = Convert.ToDouble(DtResultado.Rows[0]["n_stkini"].ToString());
                EntInventario.n_stkact = Convert.ToDouble(DtResultado.Rows[0]["n_stkact"].ToString());
                EntInventario.n_stkmin = Convert.ToDouble(DtResultado.Rows[0]["n_stkmin"].ToString());
                EntInventario.n_stkmax = Convert.ToDouble(DtResultado.Rows[0]["n_stkmax"].ToString());
                EntInventario.n_stkcon = Convert.ToDouble(DtResultado.Rows[0]["n_stkcon"].ToString());
                EntInventario.n_preini = Convert.ToDouble(DtResultado.Rows[0]["n_preini"].ToString());
                EntInventario.n_porgan = Convert.ToDouble(DtResultado.Rows[0]["n_porgan"].ToString());
                EntInventario.n_preuni = Convert.ToDouble(DtResultado.Rows[0]["n_preuni"].ToString());
                EntInventario.n_preven = Convert.ToDouble(DtResultado.Rows[0]["n_preven"].ToString());
                EntInventario.n_idmon = Convert.ToInt32(DtResultado.Rows[0]["n_idmon"].ToString());
                EntInventario.n_precom = Convert.ToDouble(xFun.NulosN(DtResultado.Rows[0]["n_precom"]));
                EntInventario.n_estado = Convert.ToInt32(DtResultado.Rows[0]["n_estado"].ToString());
                EntInventario.n_idcueconcom = Convert.ToInt32(DtResultado.Rows[0]["n_idcueconcom"].ToString());
                EntInventario.n_idcueconven = Convert.ToInt32(DtResultado.Rows[0]["n_idcueconven"].ToString());
                EntInventario.n_idret = Convert.ToInt32(DtResultado.Rows[0]["n_idret"].ToString());
                EntInventario.n_iddet = Convert.ToInt32(DtResultado.Rows[0]["n_iddet"].ToString());
                EntInventario.n_idper = Convert.ToInt32(DtResultado.Rows[0]["n_idper"].ToString());
                EntInventario.n_idtipcom = Convert.ToInt32(DtResultado.Rows[0]["n_idtipcom"].ToString());
                EntInventario.n_idtipven = Convert.ToInt32(DtResultado.Rows[0]["n_idtipven"].ToString());
                EntInventario.n_idimpsel = Convert.ToInt32(DtResultado.Rows[0]["n_idimpsel"].ToString());
                EntInventario.n_tipope = Convert.ToInt32(xFun.NulosN(DtResultado.Rows[0]["n_tipope"].ToString()));
                EntInventario.lst_unidadmedida = LstUnidades;
                EntInventario.c_prelot = xFun.NulosC(DtResultado.Rows[0]["c_prelot"].ToString());
                EntInventario.n_numdiavid = Convert.ToInt32(xFun.NulosN(DtResultado.Rows[0]["n_numdiavid"]));
            }
            return EntInventario;
        }
        public bool Insertar(BE_ALM_INVENTARIO entInventario, List<BE_ALM_INVENTARIOUNIMED> lstInventarioUnidadMedida)
        {
            bool booOk = false;
            int intFila = 0;
            DatosMySql xMiFuncion = new DatosMySql();

            if (xMiFuncion.StoreEjecutar("alm_inventario_insertar", entInventario, mysConec, 1) == true)
            {
                for (intFila = 0; intFila <= lstInventarioUnidadMedida.Count - 1; intFila++)
                {
                    lstInventarioUnidadMedida[intFila].n_idite = xMiFuncion.intIdGenerado;
                    entInventario.n_id = xMiFuncion.intIdGenerado;

                    if (xMiFuncion.StoreEjecutar("alm_inventariounimed_insertar", lstInventarioUnidadMedida[intFila], mysConec, null) == true)
                    {
                        booOk = true;
                    }
                    else
                    {
                        // CONTROLAR EL ERROR
                        booOcurrioError = xMiFuncion.booOcurrioError;
                        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        IntErrorNumber = xMiFuncion.IntErrorNumber;
                    }
                }
            }
            else
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return booOk;
        }
        
        //public bool Actualizar(BE_ALM_INVENTARIO entInventario, List<BE_ALM_INVENTARIOUNIMED> lstInventarioUnidadMedida, List<BE_ALM_INVENTARIOIMAGEN> lstInventarioImagen)
        public bool Actualizar(BE_ALM_INVENTARIO entInventario)
        {
            bool booOk = false;
            //int intFila = 0;
            //int intFilaImag = 0;
            DatosMySql xMiFuncion = new DatosMySql();

            if (xMiFuncion.StoreEjecutar("alm_inventario_actualizar", entInventario, mysConec, null) == true)
            {
                booOk = true;
                //// ESCRIBIMOS LAS UNIDADES DE MEDIDA DEL ITEM
                //for (intFila = 0; intFila <= lstInventarioUnidadMedida.Count - 1; intFila++)
                //{
                //    lstInventarioUnidadMedida[intFila].n_idite = xMiFuncion.intIdGenerado;
                //    if (xMiFuncion.StoreEjecutar("alm_inventariounimed_actualizar", lstInventarioUnidadMedida[intFila], mysConec, null) == true)
                //    {
                //        booOk = true;
                //        // ESCRIBIMOS LAS IMAGENES DEL ITEM
                //        for (intFilaImag = 0; intFilaImag <= lstInventarioImagen.Count - 1; intFilaImag++)
                //        {
                //            lstInventarioImagen[intFilaImag].n_idite = xMiFuncion.intIdGenerado;
                //            if (xMiFuncion.StoreEjecutar("alm_inventarioimagen_insertar", lstInventarioImagen[intFilaImag], mysConec, null) == true)
                //            {
                //                booOk = true;
                //            }
                //            else
                //            {
                //                booOk = false;
                //                booOcurrioError = xMiFuncion.booOcurrioError;
                //                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                //                IntErrorNumber = xMiFuncion.IntErrorNumber;
                //                return booOk;
                //            }
                //        }
                //    }
                //    else
                //    {
                //        booOk = false;
                //        booOcurrioError = xMiFuncion.booOcurrioError;
                //        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                //        IntErrorNumber = xMiFuncion.IntErrorNumber;
                //        return booOk;
                //    }
                //}
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
            bool b_Result = false;
            string[,] arrParametros = new string[1, 3] {
                                            {"n_iditem", "System.INT64", n_IdItem.ToString()}
                                      };

            if (xMiFuncion.StoreEjecutar("alm_inventariounimed_delete", arrParametros, mysConec) == true)
            {
                if (xMiFuncion.StoreEjecutar("alm_inventario_delete", arrParametros, mysConec)==false)
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                    return b_Result;
                }
            }
            else 
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                return b_Result;
            }
            b_Result = true;

            return b_Result;
        }
        public bool Activar(int n_IdItem, int n_Estado)
        {
            //alm_inventario_delete
            //DataTable DtResultado = new DataTable();
            bool b_Result = false;
            string[,] arrParametros = new string[2, 3] {
                                            {"n_iditem", "System.INT64", n_IdItem.ToString()},
                                            {"n_estado", "System.INT64", n_Estado.ToString()}
                                      };

            if (xMiFuncion.StoreEjecutar("alm_inventario_activar", arrParametros, mysConec) == false)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
                return b_Result;
            }
            b_Result = true;
            return b_Result;
        }
        public DataTable ObtenerCodigo(int n_idempresa, int n_idtipexi, int n_idfam, int n_idclas, int n_idsubclas)
        {
            DataTable DtResultado = new DataTable();

            string[,] arrParametros = new string[5, 3] {
                                            {"n_idemp", "System.INT64",n_idempresa.ToString()},
                                            {"n_idtipexi", "System.INT64",n_idtipexi.ToString()},
                                            {"n_idfam", "System.INT64",n_idfam.ToString()},
                                            {"n_idclas", "System.INT64",n_idclas.ToString()},
                                            {"n_idsubclas", "System.INT64",n_idsubclas.ToString()}
                                      };

            DtResultado = xMiFuncion.StoreDTLLenar("alm_inventario_generacodigo", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                DtResultado = null;
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }

            return DtResultado;
        }        
    }
}
