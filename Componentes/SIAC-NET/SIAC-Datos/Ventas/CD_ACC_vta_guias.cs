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

namespace SIAC_DATOS.Ventas
{
    public class CD_ACC_vta_guias
    {
        public OleDbConnection AccConec = new OleDbConnection();
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public string c_NomSolicitante;
        public int n_ItemProducido;
        
        public DataTable GuiasEmitida(int n_AnoTrabajo)
        {
            DatosAcces objFunAcc = new DatosAcces();
            DataTable dtresult = new DataTable();
            string c_Sql;

            c_Sql = "SELECT vta_guia.numser, vta_guia.numdoc, vta_guia.numser &'-'& vta_guia.numdoc as numdoc2, [idcli]+7000 AS n_idcli, mae_cliente.nombre, vta_guia.fecgiro, vta_guia.id, Year([fecgiro]) AS ano " +
                 " FROM vta_guia LEFT JOIN mae_cliente ON vta_guia.idcli = mae_cliente.id " +
                 " WHERE ((vta_guia.visalm = False) AND (vta_guia.Anulado = False) AND (Year([fecgiro]) = " + n_AnoTrabajo + ")) " +
                 " ORDER BY vta_guia.fecgiro";

            dtresult = objFunAcc.DtLLenar(c_Sql, AccConec);
            return dtresult;
        }
        public DataTable GuiasDetalle(int n_IdGuia)
        {
            DatosAcces objFunAcc = new DatosAcces();
            DataTable dtresult = new DataTable();
            string c_Sql;

            c_Sql = "SELECT vta_guiadet.iditem, vta_guiadet.canpro, alm_inventario.descripcion " +
                " FROM vta_guiadet LEFT JOIN alm_inventario ON vta_guiadet.iditem = alm_inventario.id " +
                " WHERE (vta_guiadet.idgui= " + n_IdGuia + ")";

            dtresult = objFunAcc.DtLLenar(c_Sql, AccConec);
            return dtresult;
        }
        public Boolean ActualizarEstado(int n_IdGuia, bool n_estado)
        {
            DatosAcces objFunAcc = new DatosAcces();
            string c_Sql;
            bool booresult = false;

            c_Sql = "UPDATE vta_guia SET visalm = " + n_estado + " WHERE (id = " + n_IdGuia + ") ";
            booresult = objFunAcc.EjecutarSQL(c_Sql, AccConec);
            return booresult;
        }
    }
}
