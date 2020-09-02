using MySql.Data.MySqlClient;
using SIAC_DATOS.Almacen;
using SIAC_DATOS.Sistema;
using SIAC_DATOS.Tesoreria;
using SIAC_DATOS.Ventas;
using SIAC_Entidades;
using SIAC_Entidades.Almacen;
using SIAC_Entidades.Tesoreria;
using SIAC_Objetos.Constantes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helper;

namespace SIAC_Negocio.Tesoreria
{
    public class CN_tes_mediopago
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public BE_TES_MEDIOPAGO entMedioPago = new BE_TES_MEDIOPAGO();
        public DataTable Listar()
        {
            DataTable dtResul = new DataTable();

            CD_tes_mediopago miFun = new CD_tes_mediopago();
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar();

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            DataTable dtResult = new DataTable();
            bool booResult;
            CD_tes_mediopago miFun = new CD_tes_mediopago();

            miFun.mysConec = mysConec;

            booResult = miFun.TraerRegistro(n_IdRegistro);
            dtResult = miFun.dtTarea;

            if (dtResult.Rows.Count != 0)
            {
                entMedioPago.n_id = Convert.ToInt32(dtResult.Rows[0]["n_id"]);
                entMedioPago.c_des = dtResult.Rows[0]["c_des"].ToString();
                entMedioPago.c_codsun = dtResult.Rows[0]["c_codsun"].ToString();
            }
            if (booResult == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool booResult = false;
            CD_tes_mediopago miFun = new CD_tes_mediopago();

            miFun.mysConec = mysConec;

            booResult = miFun.Eliminar(n_IdRegistro);
            if (booResult == false)
            {
                booOcurrioError = false;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return booResult;
        }
        public bool Insertar(BE_TES_MEDIOPAGO entMedioPago)
        {
            CD_tes_mediopago miFun = new CD_tes_mediopago();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(entMedioPago);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_TES_MEDIOPAGO entMedioPago)
        {
            CD_tes_mediopago miFun = new CD_tes_mediopago();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(entMedioPago);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public DataTable BuscarMedioPago(DataTable dtMedioPago)
        {
            Helper.Genericas funDatos = new Helper.Genericas();
            string[,] arrCabeceraDg1 = new string[3, 4];
            DataTable dtResult = new DataTable();

            arrCabeceraDg1[0, 0] = "Descripcion";
            arrCabeceraDg1[0, 1] = "400";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "c_des";

            arrCabeceraDg1[1, 0] = "Codigo SUnat";
            arrCabeceraDg1[1, 1] = "50";
            arrCabeceraDg1[1, 2] = "C";
            arrCabeceraDg1[1, 3] = "c_codsun";

            arrCabeceraDg1[2, 0] = "Id";
            arrCabeceraDg1[2, 1] = "0";
            arrCabeceraDg1[2, 2] = "N";
            arrCabeceraDg1[2, 3] = "n_id";

            Genericas xFun = new Genericas();
            xFun.Buscar_CampoBusqueda = "n_id";
            xFun.Buscar_CadFiltro = "";
            dtResult = xFun.Buscar(arrCabeceraDg1, dtMedioPago);

            return dtResult;
        }
    }
}
