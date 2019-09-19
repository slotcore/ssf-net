using MySql.Data.MySqlClient;
using SIAC_DATOS.Almacen;
using SIAC_DATOS.Sistema;
using SIAC_DATOS.Tesoreria;
using SIAC_DATOS.Ventas;
using SIAC_Entidades;
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
using Helper.Comunes;

namespace SIAC_Negocio.Tesoreria
{
    public class CN_tes_origen
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        
        public DataTable dtOrigen = new DataTable();
        public BE_TES_ORIGEN e_Origen = new BE_TES_ORIGEN();

        Funciones funFunciones = new Funciones();

        public void Listar(int n_IdEmpresa)
        {
            CD_tes_origen miFun = new CD_tes_origen();
            miFun.mysConec = mysConec;

            miFun.Listar(n_IdEmpresa);
            dtOrigen = miFun.dtOrigen;

            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
        }
        public void ListarVista(int n_IdEmpresa)
        {
            CD_tes_origen miFun = new CD_tes_origen();
            miFun.mysConec = mysConec;

            miFun.ListarVista(n_IdEmpresa);
            dtOrigen = miFun.dtOrigen;

            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
        }
        public DataTable BuscarOrigen(DataTable dtOrigen)
        {
            Helper.Genericas funDatos = new Helper.Genericas();
            string[,] arrCabeceraDg1 = new string[3, 4];
            DataTable dtResult = new DataTable();

            arrCabeceraDg1[0, 0] = "Cuenta";
            arrCabeceraDg1[0, 1] = "80";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "c_cuecon";

            arrCabeceraDg1[1, 0] = "Descripcion";
            arrCabeceraDg1[1, 1] = "500";
            arrCabeceraDg1[1, 2] = "C";
            arrCabeceraDg1[1, 3] = "c_des";

            arrCabeceraDg1[2, 0] = "Id";
            arrCabeceraDg1[2, 1] = "0";
            arrCabeceraDg1[2, 2] = "N";
            arrCabeceraDg1[2, 3] = "n_id";

            Genericas xFun = new Genericas();
            xFun.Buscar_CampoBusqueda = "n_id";
            xFun.Buscar_CadFiltro = "";
            dtResult = xFun.Buscar(arrCabeceraDg1, dtOrigen);

            return dtResult;
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            DataTable dtResult = new DataTable();
            bool booResult = false;
            CD_tes_origen miFun = new CD_tes_origen();

            miFun.mysConec = mysConec;

            booResult = miFun.TraerRegistro(n_IdRegistro);
            dtResult = miFun.dtOrigen;

            if (dtResult.Rows.Count != 0)
            {
                e_Origen.n_id = Convert.ToInt16(dtResult.Rows[0]["n_id"]);
                e_Origen.n_idcue = Convert.ToInt16(dtResult.Rows[0]["n_idcue"]);

                if (Convert.ToInt32(funFunciones.NulosN(dtResult.Rows[0]["n_idmod"])) != 0)
                { 
                    e_Origen.n_idmod = Convert.ToInt16(dtResult.Rows[0]["n_idmod"]);
                }
                e_Origen.n_tipo = Convert.ToInt16(dtResult.Rows[0]["n_tipo"]);
                e_Origen.n_idmon = Convert.ToInt16(dtResult.Rows[0]["n_idmon"]);
                e_Origen.n_detalla = Convert.ToInt16(dtResult.Rows[0]["n_detalla"]);
                e_Origen.n_oridin = Convert.ToInt16(dtResult.Rows[0]["n_oridin"]);
                e_Origen.n_idemp = Convert.ToInt16(dtResult.Rows[0]["n_idemp"]);
                e_Origen.n_idbanco = Convert.ToInt16(funFunciones.NulosN(dtResult.Rows[0]["n_idbanco"]));
                e_Origen.n_idcueban = Convert.ToInt16(funFunciones.NulosN(dtResult.Rows[0]["n_idcueban"]));
            }
            if (booResult == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool booResult = false;
            CD_tes_origen miFun = new CD_tes_origen();

            miFun.mysConec = mysConec;

            booResult = miFun.Eliminar(n_IdRegistro);
            if (booResult == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return booResult;
        }
        public bool Insertar(BE_TES_ORIGEN e_Origen)
        {
            CD_tes_origen miFun = new CD_tes_origen();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(e_Origen);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_TES_ORIGEN e_Origen)
        {
            CD_tes_origen miFun = new CD_tes_origen();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(e_Origen);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
    }
}
