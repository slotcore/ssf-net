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
    public class CD_pro_productos
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public DataTable dtRegistro = new DataTable();
        public DataTable dtListar = new DataTable();
        public MySqlConnection mysConec = new MySqlConnection();

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();

        public List<BE_PRO_PRODUCTOSRECETAS> lstRecetaBus = new List<BE_PRO_PRODUCTOSRECETAS>();
        public List<BE_PRO_PRODUCTOSRECETASINSUMOS> lstRecetaInsBus = new List<BE_PRO_PRODUCTOSRECETASINSUMOS>();
        public List<BE_PRO_PRODUCTOSRECETASLINEAS> lstLineaBus = new List<BE_PRO_PRODUCTOSRECETASLINEAS>();
        public List<BE_PRO_PRODUCTOSRECETASLINEASTAREAS> lstLineaTarBus = new List<BE_PRO_PRODUCTOSRECETASLINEASTAREAS>();

        public bool Listar(int n_IdEmpresa, int n_EsUnificado)
        {
            bool b_result = false;
            
            string[,] arrParametros = new string[2, 3] {
                                            {"n_idemp", "System.INT64", n_IdEmpresa.ToString()},
                                            {"n_esuni", "System.INT16", n_EsUnificado.ToString()}
                                      };
            dtListar = xMiFuncion.StoreDTLLenar("pro_productos_listar", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                b_result = true;
            }
            return b_result;
        }
        public bool TraerRegistro(Int64 n_IdRegistro)
        {
            bool b_result = false;

            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdRegistro.ToString()}
                                      };
            dtRegistro = xMiFuncion.StoreDTLLenar("pro_productos_obtenerregistro", arrParametros, mysConec);

            if (xMiFuncion.IntErrorNumber != 0)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                b_result = true;
            }
            return b_result;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool booResult = false;
            // PARAMETROS PARA ELIMINAR LOS INSUMOS Y LAS TAREAS
            string[,] arrParametros = new string[1, 3] {
                                            {"n_id", "System.INT64", n_IdRegistro.ToString()}
                                      };

            booResult = xMiFuncion.StoreEjecutar("pro_productos_delete", arrParametros, mysConec);
            if (booResult == false)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
            else
            {
                booResult = true;
            }
            return booResult;
        }
        public bool Insertar(BE_PRO_PRODUCTOS entProducto, List<BE_PRO_PRODUCTOSRECETAS> lstRecetas, List<BE_PRO_PRODUCTOSRECETASINSUMOS> lstRecetasIns,
            List<BE_PRO_PRODUCTOSRECETASLINEAS> lstLineas, List<BE_PRO_PRODUCTOSRECETASLINEASTAREAS> lstLineasTar)
        {
            bool booResult = false;
            DatosMySql xMiFuncion = new DatosMySql();
            int n_row = 0;
            int n_idpro = 0;
            MySqlTransaction trans = null;

            try
            {
                trans = mysConec.BeginTransaction();
                booResult = xMiFuncion.StoreEjecutar("pro_productos_insertar", entProducto, mysConec, 1);
                if (booResult == true)
                {
                    n_idpro = Convert.ToInt32(xMiFuncion.intIdGenerado);
                    booResult = false;
                    // INSERTAMOS LAS RECETA DEL PRODUCTO
                    for (n_row = 0; n_row <= lstRecetas.Count - 1; n_row++)
                    {
                        lstRecetas[n_row].n_idpro = n_idpro;
                        if (xMiFuncion.StoreEjecutar("pro_productosrecetas_insertar", lstRecetas[n_row], mysConec, 1) == true)
                        {
                            int n_idrec = Convert.ToInt32(xMiFuncion.intIdGenerado);
                            // INSERTAMOS LOS INSUMOS DE LA RECETA
                            for (n_row = 0; n_row <= lstRecetasIns.Count - 1; n_row++)
                            {
                                lstRecetasIns[n_row].n_idpro = n_idpro;
                                lstRecetasIns[n_row].n_idrec = n_idrec;
                                if (xMiFuncion.StoreEjecutar("pro_productosrecetasinsumos_insertar", lstRecetasIns[n_row], mysConec, null) == false)
                                {
                                    booOcurrioError = xMiFuncion.booOcurrioError;
                                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                                    trans.Rollback();
                                    return booResult;
                                }
                            }

                            // INSERTAMOS LAS LINEAS DE LA RECETA
                            for (n_row = 0; n_row <= lstLineas.Count - 1; n_row++)
                            {
                                lstLineas[n_row].n_idpro = n_idpro;
                                lstLineas[n_row].n_idrec = n_idrec;
                                if (xMiFuncion.StoreEjecutar("pro_productosrecetaslineas_insertar", lstLineas[n_row], mysConec, 2) == true)
                                {
                                    int n_idlin = Convert.ToInt32(xMiFuncion.intIdGenerado);
                                    // INSERTAMOS LAS TAREAS DE LA LINEA
                                    for (n_row = 0; n_row <= lstLineasTar.Count - 1; n_row++)
                                    {
                                        lstLineasTar[n_row].n_idpro = n_idpro;
                                        lstLineasTar[n_row].n_idrec = n_idrec;
                                        lstLineasTar[n_row].n_idlin = n_idlin;
                                        if (xMiFuncion.StoreEjecutar("pro_productosrecetaslineastareas_insertar", lstLineasTar[n_row], mysConec, null) == false)
                                        {
                                            booOcurrioError = xMiFuncion.booOcurrioError;
                                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                                            trans.Rollback();
                                            return booResult;
                                        }
                                    }
                                }
                                else
                                {
                                    booOcurrioError = xMiFuncion.booOcurrioError;
                                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                                    trans.Rollback();
                                    return booResult;
                                }
                            }
                        }
                        else
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            trans.Rollback();
                            return booResult;
                        }
                    }
                }
                else
                {
                    booOcurrioError = xMiFuncion.booOcurrioError;
                    StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                    IntErrorNumber = xMiFuncion.IntErrorNumber;
                    trans.Rollback();
                    return booResult;
                }
                booResult = true;
                trans.Commit();
                return booResult;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = true;
                StrErrorMensaje = exc.Message.ToString();
                IntErrorNumber = exc.HResult;
                trans.Rollback();
                return booResult;
            }
        }
        public bool Actualizar(BE_PRO_PRODUCTOS entProducto, List<BE_PRO_PRODUCTOSRECETAS> lstRecetas, List<BE_PRO_PRODUCTOSRECETASINSUMOS> lstRecetasIns,
            List<BE_PRO_PRODUCTOSRECETASLINEAS> lstLineas, List<BE_PRO_PRODUCTOSRECETASLINEASTAREAS> lstLineasTar)
        {
            bool booResult = false;
            DatosMySql xMiFuncion = new DatosMySql();
            int n_row = 0;
            int n_rowbus = 0;
            int n_rowact = 0;
            bool b_noecontrado = false;
            int n_idantiguo = 0;

            // ELIMINAMOS LAS TAREAS DE LA LINEA DE LA RECETA QUE HAYAN SIDO ELIMINADOS EN EL FORMULARIO
            for (n_rowbus = 0; n_rowbus <= lstLineaTarBus.Count - 1; n_rowbus++)
            {
                b_noecontrado = false;
                for (n_row = 0; n_row <= lstLineasTar.Count - 1; n_row++)
                {
                    if ((lstLineaTarBus[n_rowbus].n_idrec == lstLineasTar[n_row].n_idrec) && (lstLineaTarBus[n_rowbus].n_idtar == lstLineasTar[n_row].n_idtar) &&
                        (lstLineaTarBus[n_rowbus].n_idlin == lstLineasTar[n_row].n_idlin))
                    {
                        b_noecontrado = true;
                        break;
                    }
                }

                if (b_noecontrado == false)
                {
                    booResult = xMiFuncion.StoreEjecutar("pro_productosrecetaslineastareas_delete", lstLineaTarBus[n_rowbus], mysConec, null);
                    if (booResult == false)
                    {
                        booOcurrioError = xMiFuncion.booOcurrioError;
                        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        IntErrorNumber = xMiFuncion.IntErrorNumber;
                        return booResult;
                    }
                }
            }

            // ELIMINAMOS LINEAS DE LA RECETA QUE HAYAN SIDO ELIMINADOS EN EL FORMULARIO
            for (n_rowbus = 0; n_rowbus <= lstLineaBus.Count - 1; n_rowbus++)
            {
                b_noecontrado = false;
                for (n_row = 0; n_row <= lstLineas.Count - 1; n_row++)
                {
                    if ((lstLineaBus[n_rowbus].n_idrec == lstLineas[n_row].n_idrec) && (lstLineaBus[n_rowbus].n_id == lstLineas[n_row].n_id))
                    {
                        b_noecontrado = true;
                        break;
                    }
                }

                if (b_noecontrado == false)
                {
                    booResult = xMiFuncion.StoreEjecutar("pro_productosrecetaslineas_delete", lstLineaBus[n_rowbus], mysConec, null);
                    if (booResult == false)
                    {
                        booOcurrioError = xMiFuncion.booOcurrioError;
                        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        IntErrorNumber = xMiFuncion.IntErrorNumber;
                        return booResult;
                    }
                }
            }

            // ELIMINAMOS LOS INSUMOS DE LA RECETA QUE HAYAN SIDO ELIMINADOS EN EL FORMULARIO
            for (n_rowbus = 0; n_rowbus <= lstRecetaInsBus.Count - 1; n_rowbus++)
            {
                b_noecontrado = false;
                for (n_row = 0; n_row <= lstRecetasIns.Count - 1; n_row++)
                {
                    if ((lstRecetaInsBus[n_rowbus].n_idrec == lstRecetasIns[n_row].n_idrec) && (lstRecetaInsBus[n_rowbus].n_idite == lstRecetasIns[n_row].n_idite))
                    {
                        b_noecontrado = true;
                        break;
                    }
                }

                if (b_noecontrado == false)
                {
                    booResult = xMiFuncion.StoreEjecutar("pro_productosrecetasinsumos_delete", lstRecetaInsBus[n_rowbus], mysConec, null);
                    if (booResult == false)
                    {
                        booOcurrioError = xMiFuncion.booOcurrioError;
                        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        IntErrorNumber = xMiFuncion.IntErrorNumber;
                        return booResult;
                    }
                }
            }

            // ELIMINAMOS LAS RECETAS QUE HAYAN SIDO ELIMINADAS EN EL FORMULARIO
            for (n_rowbus = 0; n_rowbus <= lstRecetaBus.Count - 1; n_rowbus++)
            {
                b_noecontrado = false;
                for (n_row = 0; n_row <= lstRecetas.Count - 1; n_row++)
                {
                    if (lstRecetaBus[n_rowbus].n_id == lstRecetas[n_row].n_id)
                    {
                        b_noecontrado = true;
                        break;
                    }
                }

                if (b_noecontrado == false)
                {
                    booResult = xMiFuncion.StoreEjecutar("pro_productosrecetas_delete", lstRecetaBus[n_rowbus], mysConec, null);
                    if (booResult == false)
                    {
                        booOcurrioError = xMiFuncion.booOcurrioError;
                        StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                        IntErrorNumber = xMiFuncion.IntErrorNumber;
                        return booResult;
                    }
                }
            }

            // MODIFICAMOS EL PRODUCTO
            booResult = xMiFuncion.StoreEjecutar("pro_productos_actualizar", entProducto, mysConec, null);
            if (booResult == true)
            {
                #region MODIFICAR_RECETA
                // MODIFICAMOS LAS RECETAS
                for (n_row = 0; n_row <= lstRecetas.Count - 1; n_row++)
                {
                    // BUSCAMOS SI LA RECETA EXISTE EN EL ORIGINAL
                    b_noecontrado = false;
                    for (n_rowbus = 0; n_rowbus <= lstRecetaBus.Count - 1; n_rowbus++)
                    {
                        // SI LA RECETA HA SIDO ENCONTRADA
                        if (lstRecetas[n_row].n_id == lstRecetaBus[n_rowbus].n_id)    
                        {
                            // MODIFICAMOS LA RECETA
                            lstRecetas[n_row].n_idpro = entProducto.n_id;
                            if (xMiFuncion.StoreEjecutar("pro_productosrecetas_actualizar", lstRecetas[n_row], mysConec, null) == false)
                            {
                                booOcurrioError = xMiFuncion.booOcurrioError;
                                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                IntErrorNumber = xMiFuncion.IntErrorNumber;
                                return booResult;
                            }
                            b_noecontrado = true;
                            break;
                        }
                    }

                    // SI LA RECETA NO HA SIDO ENCONTRADO LA AGREGAMOS
                    if (b_noecontrado == false)
                    {
                        // MODIFICAMOS LA RECETA
                        lstRecetas[n_row].n_idpro = entProducto.n_id;
                        if (xMiFuncion.StoreEjecutar("pro_productosrecetas_insertar", lstRecetas[n_row], mysConec, 1) == false)
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            return booResult;

                        }
                        // actualizamos el ID DE LA RECETA NUEVA
                        n_idantiguo = lstRecetas[n_row].n_id;
                        lstRecetas[n_row].n_id = Convert.ToInt32(xMiFuncion.intIdGenerado);

                        // AXTUALIZAMOS EL ID ANTIGUO DE LA LA RECETA EN LAS LISTA lstRecetasIns, lstLineas, lstLineasTar
                        // lstRecetasIns
                        for (n_rowact = 0; n_rowact <= lstRecetasIns.Count - 1; n_rowact++)
                        {
                            if (lstRecetasIns[n_rowact].n_idrec == n_idantiguo)
                            {
                                lstRecetasIns[n_rowact].n_idrec = lstRecetas[n_row].n_id;
                            }
                        }

                        //lstLineas
                        for (n_rowact = 0; n_rowact <= lstLineas.Count - 1; n_rowact++)
                        {
                            if (lstLineas[n_rowact].n_idrec == n_idantiguo)
                            {
                                lstLineas[n_rowact].n_idrec = lstRecetas[n_row].n_id;
                            }
                        }
                        //lstLineasTar
                        for (n_rowact = 0; n_rowact <= lstLineasTar.Count - 1; n_rowact++)
                        {
                            if (lstLineasTar[n_rowact].n_idrec == n_idantiguo)
                            {
                                lstLineasTar[n_rowact].n_idrec = lstRecetas[n_row].n_id;
                            }
                        }
                    }
                }
                #endregion MODIFICAR_RECETA

                #region MODIFICAR_INSUMOS_RECETA
                // MODIFICAMOS LOS INSUMOS DE LA RECETA
                for (n_row = 0; n_row <= lstRecetasIns.Count - 1; n_row++)
                {
                    b_noecontrado = false;
                    // BUSCAMOS SI LA INSUMO EXISTE EN LA RECETA ORIGINAL
                    for (n_rowbus = 0; n_rowbus <= lstRecetaInsBus.Count - 1; n_rowbus++)
                    {
                        // SI ENCONTRAMOS EL INSUMO
                        if ((lstRecetasIns[n_row].n_idrec == lstRecetaInsBus[n_rowbus].n_idrec) && (lstRecetasIns[n_row].n_idite == lstRecetaInsBus[n_rowbus].n_idite))
                        { 
                            lstRecetasIns[n_row].n_idpro = entProducto.n_id; 
                            if (xMiFuncion.StoreEjecutar("pro_productosrecetasinsumos_actualizar", lstRecetasIns[n_row], mysConec, null) == false)
                            {
                                booOcurrioError = xMiFuncion.booOcurrioError;
                                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                IntErrorNumber = xMiFuncion.IntErrorNumber;
                                return booResult;
                            }
                            b_noecontrado = true;
                            break;
                        }
                    }
                    // SI NO ENCONTRAMOS EL INSUMO LO AGREGAMOS
                    if (b_noecontrado ==false)
                    {
                        lstRecetasIns[n_row].n_idpro = entProducto.n_id; 
                        if (xMiFuncion.StoreEjecutar("pro_productosrecetasinsumos_insertar", lstRecetasIns[n_row], mysConec, null) == false)
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            return booResult;
                        }
                        
                    }
                }
                #endregion MODIFICAR_RECETA

                #region MODIFICAR_LINEAS
                // MODIFICAMOS LAS LINEAS
                for (n_row = 0; n_row <= lstLineas.Count - 1; n_row++)
                {
                    b_noecontrado = false;
                    // BUSCAMOS SI LA LINE EXISTE EN LA LINEA ORIGINAL
                    for (n_rowbus = 0; n_rowbus <= lstLineaBus.Count - 1; n_rowbus++)
                    {
                        // SI LA LINEA EXISTE LA ACTUALIZAMOS
                        if ((lstLineas[n_row].n_idrec == lstLineaBus[n_rowbus].n_idrec) && (lstLineas[n_row].n_id == lstLineaBus[n_rowbus].n_id))
                        {
                            lstLineas[n_row].n_idpro = entProducto.n_id;
                            lstLineas[n_row].n_efi = 0;
                            if (xMiFuncion.StoreEjecutar("pro_productosrecetaslineas_actualizar", lstLineas[n_row], mysConec, null) == false)
                            {
                                booOcurrioError = xMiFuncion.booOcurrioError;
                                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                IntErrorNumber = xMiFuncion.IntErrorNumber;
                                return booResult;
                            }
                            b_noecontrado = true;
                            break;
                        }
                    }

                    // SI LA LINEA LINEA NO EXISTE LA INSERTAMOS
                    if (b_noecontrado == false)
                    {
                        lstLineas[n_row].n_idpro = entProducto.n_id;
                        lstLineas[n_row].c_codlin = "00000";
                        lstLineas[n_row].n_efi = 0;
                        if (xMiFuncion.StoreEjecutar("pro_productosrecetaslineas_insertar", lstLineas[n_row], mysConec, 2) == false)
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            booResult = false;
                            return booResult;
                        }

                        // ACTUALIZAMOS El ID DE LA NUEVA LINEA
                        n_idantiguo = lstLineas[n_row].n_id;
                        lstLineas[n_row].n_id = Convert.ToInt32(xMiFuncion.intIdGenerado);
                        lstLineas[n_row].c_codlin = lstLineas[n_row].n_id.ToString("000000");

                        // ACTUALIZAMOS LA LINEA AGREGADA CON EL NUEVO CODIGO GENERADO
                        if (xMiFuncion.StoreEjecutar("pro_productosrecetaslineas_actualizar", lstLineas[n_row], mysConec, 2) == false)
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            return booResult;
                        }


                        // ACTUALIZAMOS EL ID ANTIGUO DE LA LINEA EN lstLineasTar
                        for (n_rowact = 0; n_rowact <= lstLineasTar.Count - 1; n_rowact++)
                        {
                            if (lstLineasTar[n_rowact].n_idlin == n_idantiguo)
                            {
                                lstLineasTar[n_rowact].n_idlin = lstLineas[n_row].n_id;
                            }
                        }
                    }
                }
                #endregion MODIFICAR_LINEAS


                #region MODIFICAR_LINEAS_TAREAS
                // MODIFICAMOS LAS TAREAS DE LAS LINEAS
                for (n_row = 0; n_row <= lstLineasTar.Count - 1; n_row++)
                {
                    b_noecontrado = false;
                    // BUSCAMOS SI LA LINE EXISTE EN LA LINEA ORIGINAL
                    for (n_rowbus = 0; n_rowbus <= lstLineaTarBus.Count - 1; n_rowbus++)
                    { 
                        // SI LA LINEA EXISTE LA ACTUALIZAMOS
                        if ((lstLineasTar[n_row].n_idrec == lstLineaTarBus[n_rowbus].n_idrec) && (lstLineasTar[n_row].n_idlin == lstLineaTarBus[n_rowbus].n_idlin)
                            && (lstLineasTar[n_row].n_idtar == lstLineaTarBus[n_rowbus].n_idtar))
                        {
                            lstLineasTar[n_row].n_idpro = entProducto.n_id; 
                            if (xMiFuncion.StoreEjecutar("pro_productosrecetaslineastareas_actualizar", lstLineasTar[n_row], mysConec, null) == false)
                            {
                                booOcurrioError = xMiFuncion.booOcurrioError;
                                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                                IntErrorNumber = xMiFuncion.IntErrorNumber;
                                return booResult;
                            }
                            b_noecontrado = true;
                            break;
                        }
                    }

                    // SI LA TAREA NO EXISTE LA INSERTAMOS
                    if (b_noecontrado == false)
                    {
                        lstLineasTar[n_row].n_idpro = entProducto.n_id;
                        if (xMiFuncion.StoreEjecutar("pro_productosrecetaslineastareas_insertar", lstLineasTar[n_row], mysConec, null) == false)
                        {
                            booOcurrioError = xMiFuncion.booOcurrioError;
                            StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                            IntErrorNumber = xMiFuncion.IntErrorNumber;
                            return booResult;
                        }
                    }
                }
                #endregion MODIFICAR_LINEAS_TAREAS
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
    }
}
