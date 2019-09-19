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
    public class CN_ACC_pro_producciondet
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public OleDbConnection AccConec = new OleDbConnection();
        public int n_ItemProducido;

        public DataTable parteproduccionpendientes()
        {
            DataTable dtResult = new DataTable();
            CD_ACC_pro_producciondet xfunAcc = new CD_ACC_pro_producciondet();

            xfunAcc.AccConec = AccConec;
            dtResult = xfunAcc.parteproduccionpendientes_2();
            return dtResult;
        }
        public DataTable ListarRecetas()
        {
            DataTable dtResult = new DataTable();
            CD_ACC_pro_producciondet xfunAcc = new CD_ACC_pro_producciondet();

            xfunAcc.AccConec = AccConec;
            dtResult = xfunAcc.ListarRecetas();
            return dtResult;
        }
        public DataTable ProduccionSinSalidaItem()
        {
            DataTable dtResult = new DataTable();
            CD_ACC_pro_producciondet xfunAcc = new CD_ACC_pro_producciondet();

            xfunAcc.AccConec = AccConec;
            dtResult = xfunAcc.ProduccionSinSalidaItem();
            return dtResult;
        }
        public DataTable ProduccionInsumos(int n_IdProduccion, int n_IdReceta)
        {
            DataTable dtResult = new DataTable();
            CD_ACC_pro_producciondet xfunAcc = new CD_ACC_pro_producciondet();

            xfunAcc.AccConec = AccConec;
            dtResult = xfunAcc.ProduccionInsumos(n_IdProduccion, n_IdReceta);
            return dtResult;
        }
        public bool ActualizarEstadoParteProduccion(int n_IdProduccion, int n_idItem, int n_estado)
        {
            bool booOk = false;
            CD_ACC_pro_producciondet xfunAcc = new CD_ACC_pro_producciondet();

            xfunAcc.AccConec = AccConec;
            booOk = xfunAcc.ActualizarEstadoParteProduccion(n_IdProduccion, n_idItem, n_estado);
            return booOk;
        }
    }
}
