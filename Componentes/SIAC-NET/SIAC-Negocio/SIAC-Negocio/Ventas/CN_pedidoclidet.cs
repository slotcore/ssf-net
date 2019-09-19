using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades;
using SIAC_DATOS.Ventas;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Almacen;
using SIAC_Entidades.Ventas;

namespace SIAC_Negocio.Ventas
{
    public class CN_pedidoclidet
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public DataTable dtOrdReqPendiente = new DataTable();
        public DataTable dtListaOrdenReq = new DataTable();

        public BE_VTA_PEDIDOCLI entPedCab = new BE_VTA_PEDIDOCLI();
        public List<BE_VTA_PEDIDOCLIDET> lstPedDet = new List<BE_VTA_PEDIDOCLIDET>();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
    }
}
