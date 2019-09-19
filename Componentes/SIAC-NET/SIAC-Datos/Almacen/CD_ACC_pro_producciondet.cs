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
    public class CD_ACC_pro_producciondet
    {
        public OleDbConnection AccConec = new OleDbConnection();
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public string c_NomSolicitante;
        public int n_ItemProducido;

        //public DataTable parteproduccionpendientes()
        //{
        //    DatosAcces objFunAcc = new DatosAcces();
        //    DataTable dtresult = new DataTable();
        //    string c_Sql;
                        
        //    c_Sql = "SELECT pro_producciondet.idpro, pro_producciondet.iditem, alm_inventario.descripcion, pro_produccion.dia AS fchpro, pro_producciondet.numlote, '0001' AS ppnumser, pro_producciondet.numparte AS ppnumdoc, " +
        //        " '0000-'+[pro_producciondet].[numparte] AS ppnumero, pro_ordenprod.numser AS opnumser, pro_ordenprod.numdoc AS opnumdoc, [pro_ordenprod].[numser]+'-'+[pro_ordenprod].[numdoc] AS opnumero, pro_producciondet.cantidad " +
        //        " FROM pro_produccion INNER JOIN ((pro_producciondet LEFT JOIN alm_inventario ON pro_producciondet.iditem = alm_inventario.id) LEFT JOIN pro_ordenprod ON pro_producciondet.idord = pro_ordenprod.id) " +
        //        " ON pro_produccion.id = pro_producciondet.idpro " +
        //        " WHERE chknewsis = 0 ORDER BY '0000-'+[pro_producciondet].[numparte] DESC";


        //    dtresult = objFunAcc.DtLLenar(c_Sql, AccConec);
        //    return dtresult;
        //}
        public DataTable ProduccionSinSalidaItem()
        {
            DatosAcces objFunAcc = new DatosAcces();
            DataTable dtresult = new DataTable();

            string c_Sql;
            //c_Sql = "SELECT pro_producciondet.idpro, pro_producciondet.numparte, pro_receta.codrec AS reccod, pro_receta.descripcion AS redes, alm_inventario.codpro AS procod, alm_inventario.descripcion AS prodes, " +
            //    " pro_producciondet.chknewsis, pro_producciondet.cantidad, pro_produccion.dia, pro_producciondet.iditem, pro_producciondet.idrec " +
            //    " FROM pro_produccion LEFT JOIN ((pro_producciondet LEFT JOIN alm_inventario ON pro_producciondet.iditem = alm_inventario.id) LEFT JOIN pro_receta ON pro_producciondet.idrec = pro_receta.id) " +
            //    " ON pro_produccion.id = pro_producciondet.idpro " +
            //    " WHERE (pro_producciondet.n_chkalm = 0) ORDER BY pro_produccion.dia, pro_producciondet.numparte ";

            c_Sql = " SELECT [pro_producciondet].[idpro] & '-' & [pro_producciondet].[idrec] AS c_id, pro_producciondet.idpro, pro_producciondet.numparte, pro_receta.codrec AS reccod, pro_receta.descripcion AS redes, alm_inventario.codpro AS procod, alm_inventario.descripcion AS prodes, " +
                " pro_producciondet.chknewsis, pro_producciondet.cantidad, pro_produccion.dia, pro_producciondet.iditem, pro_producciondet.idrec, (pla_empleados.apepat &' '& pla_empleados.apemat&', '& pla_empleados.nom) AS c_nomenc " +
                " FROM ((pro_produccion LEFT JOIN ((pro_producciondet LEFT JOIN alm_inventario ON pro_producciondet.iditem = alm_inventario.id) LEFT JOIN pro_receta ON pro_producciondet.idrec = pro_receta.id) " + 
                " ON pro_produccion.id = pro_producciondet.idpro) LEFT JOIN pro_emp ON pro_produccion.idsup = pro_emp.id) LEFT JOIN pla_empleados ON pro_emp.idemp = pla_empleados.id " +
                " WHERE (pro_producciondet.n_chkalm = 0) " +
                " ORDER BY pro_produccion.dia, pro_producciondet.numparte"; 


            dtresult = objFunAcc.DtLLenar(c_Sql, AccConec);
            return dtresult;
        }
        public DataTable parteproduccionpendientes_2()
        {
            DatosAcces objFunAcc = new DatosAcces();
            DataTable dtresult = new DataTable();
            string c_Sql;

            //c_Sql = " SELECT pro_producciondet.idpro, '0001-'+[numparte] AS numparteprod, pro_producciondet.iditem AS iditem, alm_inventario.descripcion AS desproducto, pro_receta.codrec, "
            //    + " pro_receta.descripcion AS desrec, pro_producciondet.cantidad, pro_producciondet.horini, pro_producciondet.horfin, pro_produccion.dia AS fchproduccion, pro_producciondet.numlote, "
            //    + " pro_producciondet.chknewsis "
            //    + " FROM pro_produccion RIGHT JOIN ((pro_producciondet LEFT JOIN alm_inventario ON pro_producciondet.iditem = alm_inventario.id) LEFT JOIN pro_receta ON pro_producciondet.idrec = pro_receta.id) "
            //    + " ON pro_produccion.id = pro_producciondet.idpro "
            //    + " WHERE (((pro_producciondet.chknewsis) <> 1)) "
            //    + " ORDER BY pro_produccion.dia DESC ";

            //c_Sql = "SELECT pro_producciondet.idpro,Trim(Str([idpro]))+Trim(Str([IDITEM])) AS idpro2, 73 AS idtipdocprod, '0001-'+[numparte] AS numparteprod, pro_producciondet.iditem AS iditem, alm_inventario.descripcion AS desproducto, "
            //    + " pro_producciondet.idrec, pro_receta.codrec, pro_receta.descripcion AS desrec, [pro_producciondet].[cantidad]+[pro_producciondet].[canrepro] AS cantidad, pro_producciondet.horini, pro_producciondet.horfin, pro_produccion.dia AS fchproduccion, "
            //    + " pro_producciondet.numlote, pro_producciondet.chknewsis "
            //    + " FROM pro_produccion RIGHT JOIN ((pro_producciondet LEFT JOIN alm_inventario ON pro_producciondet.iditem = alm_inventario.id) LEFT JOIN pro_receta ON pro_producciondet.idrec = pro_receta.id) "
            //    + " ON pro_produccion.id = pro_producciondet.idpro WHERE (((pro_producciondet.chknewsis)<>1)) ORDER BY pro_produccion.dia ";

            c_Sql = "SELECT pro_producciondet.idpro, Trim(Str([pro_producciondet].[idpro]))+Trim(Str([pro_producciondet].[IDITEM])) AS idpro2, 73 AS idtipdocprod, '0001-'+[numparte] AS numparteprod, "
                + " pro_producciondet.iditem AS iditem, alm_inventario.descripcion AS desproducto, pro_producciondet.idrec, pro_receta.codrec, pro_receta.descripcion AS desrec, "
                + " [pro_producciondet].[cantidad]+[pro_producciondet].[canrepro] AS cantidad, pro_producciondet.horini, pro_producciondet.horfin, pro_produccion.dia AS fchproduccion, pro_producciondet.numlote, "
                + " pro_producciondet.chknewsis, (ucase(pla_empleados.apepat)+' '+ ucase(pla_empleados.apemat) +', '+ ucase(pla_empleados.nom)) AS c_resnom "
                + " FROM ((pro_produccion LEFT JOIN ((pro_producciondet LEFT JOIN alm_inventario ON pro_producciondet.iditem = alm_inventario.id) LEFT JOIN pro_receta ON pro_producciondet.idrec = pro_receta.id) "
                + " ON pro_produccion.id = pro_producciondet.idpro) LEFT JOIN pro_emp ON pro_producciondet.idres = pro_emp.id) LEFT JOIN pla_empleados ON pro_emp.idemp = pla_empleados.id "
                + " WHERE (((pro_producciondet.chknewsis)<>1)) "
                + " ORDER BY pro_produccion.dia";

                
            dtresult = objFunAcc.DtLLenar(c_Sql, AccConec);
            return dtresult;
        }
        public Boolean ActualizarEstado(int n_IdProduccion, int n_IdItem, int n_estado)
        {
            DatosAcces objFunAcc = new DatosAcces();
            string c_Sql;
            bool booresult = false;

            c_Sql = "UPDATE pro_producciondet SET chknewsis = " + n_estado + " WHERE (idpro = " + n_IdProduccion + ") AND (iditem = " + n_IdItem + ") ";
            booresult = objFunAcc.EjecutarSQL(c_Sql, AccConec);
            return booresult;
        }
        public Boolean ActualizarEstadoParteProduccion(int n_IdProduccion, int n_idItem, int n_estado)
        {
            DatosAcces objFunAcc = new DatosAcces();
            string c_Sql;
            bool booresult = false;

            c_Sql = "UPDATE pro_producciondet SET n_chkalm = " + n_estado + " WHERE ((idpro = " + n_IdProduccion + ") AND (iditem = " + n_idItem + "))";
            booresult = objFunAcc.EjecutarSQL(c_Sql, AccConec);
            return booresult;
        }
        public DataTable ListarRecetas()
        {
            DatosAcces objFunAcc = new DatosAcces();
            DataTable dtresult = new DataTable();
            string c_Sql;

            c_Sql = "SELECT pro_receta.id, pro_receta.codrec, pro_receta.descripcion, pro_receta.prirec, pro_receta.iditem FROM pro_receta ORDER BY pro_receta.id";

            dtresult = objFunAcc.DtLLenar(c_Sql, AccConec);
            return dtresult;
        }
        public DataTable ProduccionInsumos(int n_IdProduccion, int n_IdReceta)
        {
            DatosAcces objFunAcc = new DatosAcces();
            DataTable dtresult = new DataTable();
            string c_Sql;

            c_Sql = "SELECT pro_producciondetins.idpro, pro_producciondetins.numparte, pro_producciondetins.idrec, pro_producciondetins.iditem, pro_producciondetins.canutil, alm_inventario.descripcion " +
                " FROM (pro_producciondet LEFT JOIN pro_producciondetins ON (pro_producciondet.idrec = pro_producciondetins.idrec) AND (pro_producciondet.numparte = pro_producciondetins.numparte) " +
                " AND (pro_producciondet.idpro = pro_producciondetins.idpro)) LEFT JOIN alm_inventario ON pro_producciondetins.iditem = alm_inventario.id " +
                " WHERE ((pro_producciondetins.idpro= " + n_IdProduccion + ")  AND ( pro_producciondetins.idrec = " + n_IdReceta + ")) " +
                " ORDER BY alm_inventario.descripcion ";
            
            dtresult = objFunAcc.DtLLenar(c_Sql, AccConec);
            return dtresult;
        }
    }
}
