using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Almacen;
using System.Data.OleDb;

namespace SIAC_DATOS.Almacen
{
    public class CD_ACC_movimientoAlmacen
    {
        public OleDbConnection AccConec = new OleDbConnection();
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public string c_NomSolicitante;
        public bool Insertar(BE_ALM_MOVIMIENTOS entMovimientos, List<BE_ALM_MOVIMIENTOSDET> entDetalle)
        {
            bool booResult = false;
            DatosAcces objFunAcc = new DatosAcces();
            
            string c_Sql;
            bool b_tipmov = false;
            int n_tipdoc = 0;
            int n_tipope = 0;
            try
            {
                if (entMovimientos.n_idtipmov == 1)
                {
                    b_tipmov = true;
                }
                else
                {
                    b_tipmov = false;
                }

                if (entMovimientos.n_idtipope == 10) { n_tipope = 4; }    // SALIDA A PRODUCCIÓN
                if (entMovimientos.n_idtipope == 2) { n_tipope = 1; }     // COMPRA NACIONAL
                if (entMovimientos.n_idtipope == 19) { n_tipope = 3; }    // ENTRADA DE PRODUCCIÓN
                if (entMovimientos.n_idtipope == 30) { n_tipope = 2; }    // SALIDA DE BIENES EN PRÉSTAMO
                
                if (entMovimientos.n_idtipdoc == 50)  {n_tipdoc = 112;}   // NOTA DE SALIDA EN AMBAS VERSIONES
                if (entMovimientos.n_idtipdoc == 49) { n_tipdoc = 113; }  // NOTA DE INGRESO EN AMBAS VERSIONES
                if (entMovimientos.n_idtipdoc == 10) { n_tipdoc = 9; }    // GUIA DE REMISION EN AMBAS VERSIONES

                // ARMANOS LA CADENA SQL PARA ACTUALIZAR ACCES
                c_Sql = "INSERT INTO alm_ingreso ( " +
                    " id, " +
                    " fching, " +
                    " idpro, " +
                    " fchdoc, " +
                    " numser, " +
                    " numdoc, " +
                    " tipmov, " +
                    " idalm, " +
                    " ano, " +
                    " idmes, " +
                    " nombre, " +
                    " tipdoc, " +
                    " idope " +
                    " ) VALUES " +
                    " ( " +
                    " " + entMovimientos.n_id + ", " +
                    " cDate('" + entMovimientos.d_fching.ToString("dd/MM/yyyy") + "'), " +
                    " " + entMovimientos.n_idclipro + ", " +
                    " Cdate('" + entMovimientos.d_fchdoc.ToString("dd/MM/yyyy") + "'), " +
                    " '" + entMovimientos.c_numser + "', " +
                    " '" + entMovimientos.c_numdoc + "', " +
                    " " + b_tipmov + ", " +
                    " " + entMovimientos.n_idalm + ", " +
                    " " + entMovimientos.n_anotra + ", " +
                    " " + entMovimientos.n_idmes + ", " +
                    " '" + c_NomSolicitante + "'," +
                    " " + n_tipdoc + "," +
                    " " + n_tipope + "" +
                    " ) ";

                booResult = objFunAcc.EjecutarSQL(c_Sql, AccConec);

                foreach (BE_ALM_MOVIMIENTOSDET_CONSULTA element in entDetalle)
                {
                    string c_newhoring = "Null";
                    string c_newhorsal = "Null";

                    if (element.h_horsal != "") { c_newhorsal = "'" + element.h_horsal + "'"; }
                    if (element.h_horing != "") { c_newhoring = "'" + element.h_horing + "'"; }

                    c_Sql = "INSERT INTO alm_ingresodet ( " +
                    " iding, " +
                    " iditem, " +
                    " cantidad, " +
                    " revisado, " +
                    " c_numlot, " +
                    " h_horsal, " +
                    " h_horing " +
                    " ) VALUES " +
                    " ( " +
                    " " + entMovimientos.n_id + ", " +
                    " " + element.n_idite + ", " +
                    " " + element.n_can + ", " +
                    " " + false + ", " +
                    " '" + element.c_numlot + "', " +
                    " " + c_newhorsal + ", " +
                    " " + c_newhoring + " " +
                    " ) ";

                    booResult = objFunAcc.EjecutarSQL(c_Sql, AccConec);
                    if (booResult == false)
                    {
                        booOcurrioError = objFunAcc.booOcurrioError;
                        StrErrorMensaje = objFunAcc.StrErrorMensaje;
                        IntErrorNumber = objFunAcc.IntErrorNumber;

                        return booResult;
                    }
                }

                return booResult;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = objFunAcc.booOcurrioError;
                StrErrorMensaje = objFunAcc.StrErrorMensaje;
                IntErrorNumber = objFunAcc.IntErrorNumber;

                return booResult;
            }
        }
        public bool Actualizar(BE_ALM_MOVIMIENTOS entMovimientos, List<BE_ALM_MOVIMIENTOSDET> entDetalle)
        { 
            bool booResult = false;
            DatosAcces objFunAcc = new DatosAcces();
            string c_Sql;
            bool b_tipmov = false;
            int n_tipdoc = 0;
            int n_tipope = 0;
            try
            {
                if (entMovimientos.n_idtipmov == 1)
                {
                    b_tipmov = true;
                }
                else
                {
                    b_tipmov = false;
                }

                if (entMovimientos.n_idtipope == 10) { n_tipope = 4; }    // SALIDA A PRODUCCIÓN
                if (entMovimientos.n_idtipope == 2) { n_tipope = 1; }     // COMPRA NACIONAL
                if (entMovimientos.n_idtipope == 19) { n_tipope = 3; }    // ENTRADA DE PRODUCCIÓN
                if (entMovimientos.n_idtipope == 30) { n_tipope = 2; }    // SALIDA DE BIENES EN PRÉSTAMO

                if (entMovimientos.n_idtipdoc == 50) { n_tipdoc = 112; }  // NOTA DE SALIDA EN AMBAS VERSIONES
                if (entMovimientos.n_idtipdoc == 49) { n_tipdoc = 113; }  // NOTA DE INGRESO EN AMBAS VERSIONES
                if (entMovimientos.n_idtipdoc == 10) { n_tipdoc = 9; }    // GUIA DE REMISION EN AMBAS VERSIONES

                // ARMANOS LA CADENA SQL PARA ACTUALIZAR ACCES
                c_Sql = "UPDATE alm_ingreso SET " +              
                    " fching = cDate('" + entMovimientos.d_fching.ToString("dd/MM/yyyy") + "') ," +
                    " idpro = " + entMovimientos.n_idclipro + " ," +
                    " fchdoc = Cdate('" + entMovimientos.d_fchdoc.ToString("dd/MM/yyyy") + "') ," +
                    " numser = '" + entMovimientos.c_numser + "' ," +
                    " numdoc = '" + entMovimientos.c_numdoc + "' ," +
                    " tipmov  = " + b_tipmov + "," +
                    " idalm = " + entMovimientos.n_idalm + "," +
                    " ano = " + entMovimientos.n_anotra + "," +
                    " idmes = " + entMovimientos.n_idmes + ", " +
                    " tipdoc = " + n_tipdoc + ", " +
                    " nombre = '" + c_NomSolicitante + "', " +
                    " idope = " + n_tipope + "" +
                    " WHERE (id = " + entMovimientos.n_id + ")";

                booResult = objFunAcc.EjecutarSQL(c_Sql, AccConec);

                if (booResult == true)
                {
                    // ELIMINAMOS EL DETALLE
                    c_Sql = "DELETE FROM alm_ingresodet WHERE iding = " + entMovimientos.n_id + "";
                    booResult = objFunAcc.EjecutarSQL(c_Sql, AccConec);
                    if (booResult == true)
                    {
                        foreach (BE_ALM_MOVIMIENTOSDET element in entDetalle)
                        {
                            string c_newhoring = "Null";
                            string c_newhorsal = "Null";

                            if (element.h_horsal != "") { c_newhorsal = "'" + element.h_horsal + "'"; }
                            if (element.h_horing != "") { c_newhoring = "'" + element.h_horing + "'"; }

                            c_Sql = "INSERT INTO alm_ingresodet (" +
                            " iding, " +
                            " iditem, " +
                            " cantidad, " +
                            " revisado, " +
                            " c_numlot, " +
                            " h_horing, " +
                            " h_horsal )" +
                            " VALUES " +
                            " ( " +
                            " " + element.n_idmov + ", " +
                            " " + element.n_idite + ", " +
                            " " + element.n_can + ", " +
                            " " + false + ", " +
                            " '" + element.c_numlot + "', " +
                            " " + c_newhoring + ", " +
                            " " + c_newhorsal + " " +
                            " ) ";

                            booResult = objFunAcc.EjecutarSQL(c_Sql, AccConec);
                            if (booResult == false)
                            {
                                booOcurrioError = objFunAcc.booOcurrioError;
                                StrErrorMensaje = objFunAcc.StrErrorMensaje;
                                IntErrorNumber = objFunAcc.IntErrorNumber;

                                return booResult;
                            }
                        }
                    }
                    else
                    {
                        booOcurrioError = objFunAcc.booOcurrioError;
                        StrErrorMensaje = objFunAcc.StrErrorMensaje;
                        IntErrorNumber = objFunAcc.IntErrorNumber;
                        return booResult;
                    }
                }
                else
                {
                    booOcurrioError = objFunAcc.booOcurrioError;
                    StrErrorMensaje = objFunAcc.StrErrorMensaje;
                    IntErrorNumber = objFunAcc.IntErrorNumber;
                    return booResult;
                }
                return booResult;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = objFunAcc.booOcurrioError;
                StrErrorMensaje = objFunAcc.StrErrorMensaje;
                IntErrorNumber = objFunAcc.IntErrorNumber;

                return booResult;
            }
        }
        public bool Eliminar(Int32 n_IdRegistro)
        {
            bool booResult = false;
            DatosAcces objFunAcc = new DatosAcces();
            string c_Sql;
            
            try
            {
                c_Sql = "DELETE FROM alm_ingresodet WHERE iding = " + n_IdRegistro + "";
                booResult = objFunAcc.EjecutarSQL(c_Sql, AccConec);
                if (booResult == true)
                {
                    c_Sql = "DELETE FROM alm_ingreso WHERE id = " + n_IdRegistro + "";
                    booResult = objFunAcc.EjecutarSQL(c_Sql, AccConec);
                    if (booResult == false)
                    {
                        booOcurrioError = objFunAcc.booOcurrioError;
                        StrErrorMensaje = objFunAcc.StrErrorMensaje;
                        IntErrorNumber = objFunAcc.IntErrorNumber;

                        return booResult;
                    }
                }
                else 
                {
                    booOcurrioError = objFunAcc.booOcurrioError;
                    StrErrorMensaje = objFunAcc.StrErrorMensaje;
                    IntErrorNumber = objFunAcc.IntErrorNumber;
                    return booResult;
                }
                return booResult;
            }
            catch (Exception exc)
            {
                // SI SUCEDE UN ERROR DEVOLVEMOS FALSO
                booOcurrioError = objFunAcc.booOcurrioError;
                StrErrorMensaje = objFunAcc.StrErrorMensaje;
                IntErrorNumber = objFunAcc.IntErrorNumber;

                return booResult;
            }
        }
    }
}
