using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades.Maestros;
using SIAC_DATOS.Maestros;
using MySql.Data.MySqlClient;
using Helper;

namespace SIAC_Negocio.Maestros
{
    public class CN_mae_clipro
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public double n_IdGenerado = 0;

        public DataTable ListarProveedor(int n_IdEmpresa, int n_EsUnificado)
        {
            DataTable dtResul = new DataTable();

            CD_mae_clipro miFun = new CD_mae_clipro();
            miFun.mysConec = mysConec;

            dtResul = miFun.ListarProveedor(n_IdEmpresa, n_EsUnificado);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public DataTable ListarCliente(int n_IdEmpresa, int n_EsUnificado)
        {
            DataTable dtResul = new DataTable();

            CD_mae_clipro miFun = new CD_mae_clipro();
            miFun.mysConec = mysConec;

            dtResul = miFun.ListarCliente(n_IdEmpresa, n_EsUnificado);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public DataTable Listar(int n_IdEmpresa, int n_EsUnificado)
        {
            DataTable dtResul = new DataTable();

            CD_mae_clipro miFun = new CD_mae_clipro();
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar(n_IdEmpresa, n_EsUnificado);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public DataTable ClienteBuscar(string strRUCNumero)
        {
            DataTable dtResul = new DataTable();
            
            CD_mae_clipro miFun = new CD_mae_clipro();
            miFun.mysConec = mysConec;

            dtResul = miFun.ClienteBuscar(strRUCNumero);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public BE_MAE_CLIPRO TraerRegistro(int n_IdRegistro)
        {
            BE_MAE_CLIPRO entCliPro = new BE_MAE_CLIPRO();

            CD_mae_clipro miFun = new CD_mae_clipro();
            miFun.mysConec = mysConec;
            entCliPro = miFun.TraerRegistro(n_IdRegistro);

            return entCliPro;
        }
        public bool Insertar(BE_MAE_CLIPRO entCliPro)
        {
            CD_mae_clipro miFun = new CD_mae_clipro();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(entCliPro);
            n_IdGenerado = miFun.n_IdGenerado;

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_MAE_CLIPRO entCliPro)
        {
            CD_mae_clipro miFun = new CD_mae_clipro();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(entCliPro);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Eliminar(int n_Id)
        {
            CD_mae_clipro miFun = new CD_mae_clipro();
            bool booOk = false;

            miFun.mysConec = mysConec;

            booOk = miFun.Eliminar(n_Id);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public DataTable BuscarCliPro(DataTable DtCLiPro, int n_TipoRegistro, string c_CampoBuscar, string c_CadenaFiltro)
        { 
            //n_TipoRegistro = 1  Proveedor
            //n_TipoRegistro = 2  Cliente
            string[,] arrCabeceraDg1 = new string[3, 4];
            DataTable dtResult = new DataTable();
            DataTable dtresulpre = new DataTable();
            Helper.Genericas funDatos = new Helper.Genericas();

            dtResult = DtCLiPro;
            //dtResult = funDatos.DataTableFiltrar(DtCLiPro, "n_idtipreg = " + n_TipoRegistro + "");

            arrCabeceraDg1[0, 0] = "Nº R.U.C.";
            arrCabeceraDg1[0, 1] = "100";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "c_numdoc";

            arrCabeceraDg1[1, 0] = "Nombre Razon Social";
            arrCabeceraDg1[1, 1] = "600";
            arrCabeceraDg1[1, 2] = "C";
            arrCabeceraDg1[1, 3] = "c_nombre";

            arrCabeceraDg1[2, 0] = "Id";
            arrCabeceraDg1[2, 1] = "0";
            arrCabeceraDg1[2, 2] = "N";
            arrCabeceraDg1[2, 3] = "n_id";

            Genericas xFun = new Genericas();
            xFun.Buscar_CampoBusqueda = c_CampoBuscar;
            xFun.Buscar_CadFiltro = c_CadenaFiltro;
            xFun.Buscar_CampoOrden = "c_nombre";
            dtResult = xFun.Buscar(arrCabeceraDg1, dtResult);
            
            return dtResult;
        }
    }
}
