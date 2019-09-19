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
    public class CD_ACC_pro_solicitudmat
    {
        public OleDbConnection AccConec = new OleDbConnection();
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public string c_NomSolicitante;

        public DataTable solicitamatpendientes()
        {
            DatosAcces objFunAcc = new DatosAcces();
            DataTable dtresult = new DataTable();
            string c_Sql;
            
            //c_Sql = "SELECT pro_solicitudmat.fchdoc, pro_solicitudmat.numser AS solmatnumser, pro_solicitudmat.numdoc AS solmatnumdoc, (pro_solicitudmat.numser+'-'+pro_solicitudmat.numdoc) AS numsolmat, pro_solicitudmat.estado, " +
            //    " pro_ordenprod.numser AS ordpronumser, pro_ordenprod.numdoc AS ordpronumdoc, (pro_ordenprod.numser+'-'+pro_ordenprod.numdoc) AS numordpro, (pla_empleados.apepat+' '+pla_empleados.apemat+', '+pla_empleados.nom) AS apenom, " +
            //    " pro_solicitudmat.id " +
            //    " FROM (pro_solicitudmat LEFT JOIN pro_ordenprod ON pro_solicitudmat.iddocref = pro_ordenprod.id) LEFT JOIN pla_empleados ON pro_solicitudmat.idresp = pla_empleados.id " +
            //    " WHERE (((pro_solicitudmat.estado) = 1)) " +
            //    " ORDER BY pro_solicitudmat.numser, pro_solicitudmat.numdoc DESC";

            c_Sql = "SELECT pro_solicitudmat.fchdoc, pro_solicitudmat.numser AS solmatnumser, pro_solicitudmat.numdoc AS solmatnumdoc, (pro_solicitudmat.numser+'-'+pro_solicitudmat.numdoc) AS numsolmat, " +
                " pro_solicitudmat.estado, pro_ordenprod.numser AS ordpronumser, pro_ordenprod.numdoc AS ordpronumdoc, (pro_ordenprod.numser+'-'+pro_ordenprod.numdoc) AS numordpro, " +
                " (pla_empleados.apepat+' '+pla_empleados.apemat+', '+pla_empleados.nom) AS apenom, pro_solicitudmat.id, pro_solicitudmat.n_atealm, pro_receta.descripcion AS desrec, alm_inventario.descripcion AS despro " +
                " FROM (((pro_solicitudmat LEFT JOIN pro_ordenprod ON pro_solicitudmat.iddocref = pro_ordenprod.id) LEFT JOIN pla_empleados ON pro_solicitudmat.idresp = pla_empleados.id) LEFT JOIN pro_receta " +
                " ON pro_ordenprod.idrec = pro_receta.id) LEFT JOIN alm_inventario ON pro_receta.iditem = alm_inventario.id " +
                " WHERE (((pro_solicitudmat.n_atealm)=0)) " +
                " ORDER BY pro_solicitudmat.numser, pro_solicitudmat.numdoc DESC";


            dtresult = objFunAcc.DtLLenar(c_Sql, AccConec);
            return dtresult;
        }
        public DataTable solicitamatitems(int n_IdSolicitud)
        { 
            DatosAcces objFunAcc = new DatosAcces();
            DataTable dtresult = new DataTable();
            string c_Sql;

            c_Sql = " SELECT pro_solicitudmatdet.id, pro_solicitudmatdet.idsol, pro_solicitudmatdet.iditem, pro_solicitudmatdet.cantidad, pro_solicitudmatdet.idunimed, pro_solicitudmatdet.idlote, pro_solicitudmatdet.idlotedet, " +
                " alm_inventario.descripcion " +
                " FROM pro_solicitudmatdet LEFT JOIN alm_inventario ON pro_solicitudmatdet.iditem = alm_inventario.id " +
                " WHERE (((pro_solicitudmatdet.idsol) = " + n_IdSolicitud + ")) " +
                " ORDER BY alm_inventario.descripcion";
            dtresult = objFunAcc.DtLLenar(c_Sql, AccConec);
            return dtresult;
        }
        public Boolean ActualizarEstado(int n_IdSolicitudMateriales)
        {
            DatosAcces objFunAcc = new DatosAcces();
            string c_Sql;
            bool booresult = false;

            c_Sql = "UPDATE pro_solicitudmat SET estado = 2 WHERE id = " + n_IdSolicitudMateriales + "";
            booresult = objFunAcc.EjecutarSQL(c_Sql, AccConec);
            return booresult;
        }
    }
}
