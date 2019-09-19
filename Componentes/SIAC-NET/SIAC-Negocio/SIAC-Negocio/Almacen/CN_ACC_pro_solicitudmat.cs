using MySql.Data.MySqlClient;
using SIAC_DATOS.Almacen;
using SIAC_Entidades;
using SIAC_Entidades.Almacen;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace SIAC_Negocio.Almacen
{
    public class CN_ACC_pro_solicitudmat
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public OleDbConnection AccConec = new OleDbConnection();
        public DataTable solicitamatpendientes()
        {
            DataTable dtResult = new DataTable();
            CD_ACC_pro_solicitudmat xfunAcc = new CD_ACC_pro_solicitudmat ();

            xfunAcc.AccConec = AccConec;
            dtResult = xfunAcc.solicitamatpendientes();
            return dtResult;
        }
        public DataTable solicitamatitems(int n_IdSolicitud)
        {
            DataTable dtResult = new DataTable();
            CD_ACC_pro_solicitudmat xfunAcc = new CD_ACC_pro_solicitudmat ();

            xfunAcc.AccConec = AccConec;
            dtResult = xfunAcc.solicitamatitems(n_IdSolicitud);
            return dtResult;
        }
    }
}
