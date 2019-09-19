using Helper;
using MySql.Data.MySqlClient;
using SIAC_DATOS.Almacen;
using SIAC_DATOS.Gestion;
using SIAC_DATOS.Sistema;
using SIAC_Entidades;
using SIAC_Entidades.Gestion;
using SIAC_Objetos.Constantes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIAC_Negocio.Gestion
{
    public class CN_ges_plancompras
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        private CD_ges_plancompras objdatos = new CD_ges_plancompras();

        
        DatosMySql xMiFuncion = new DatosMySql();

        //private DataTable _dtCabecera;
        //private DataTable _dtLista;
        //private DataTable _dtItems;
        public DataTable dtDataAnos;
        //private DataTable _dtVenHis;
        //private DataTable _dtDetalle;
        //private DataTable _dtDetalleAnos;
        //private DataTable _dtOrdenMes;
        public void TraerDataAnos(int n_IdEmpresa)
        {
            objdatos.mysConec = mysConec;
            objdatos.TraerDataAnos(n_IdEmpresa);
            dtDataAnos = objdatos.dtDataAnos;
            if (objdatos.booOcurrioError == true)
            {
                booOcurrioError = xMiFuncion.booOcurrioError;
                StrErrorMensaje = xMiFuncion.StrErrorMensaje;
                IntErrorNumber = xMiFuncion.IntErrorNumber;
            }
        }
    }
}
