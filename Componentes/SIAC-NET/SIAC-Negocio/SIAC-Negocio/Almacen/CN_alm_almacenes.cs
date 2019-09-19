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

namespace SIAC_Negocio.Almacen
{
    public class CN_alm_almacenes
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        public DataTable ListarNuevo(int n_IdEmpresa, int n_EsUnificado)
        {
            DataTable dtResul = new DataTable();
            CD_alm_almacenes miFun = new CD_alm_almacenes();
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
        public BE_ALM_ALMACENES_CONSULTA TraerRegistro(int n_IdRegistro)
        {
            BE_ALM_ALMACENES_CONSULTA entAlmacen = new BE_ALM_ALMACENES_CONSULTA();

            CD_alm_almacenes miFun = new CD_alm_almacenes();
            miFun.mysConec = mysConec;
            entAlmacen = miFun.TraerRegistro(n_IdRegistro);

            return entAlmacen;
        }
        public bool Insertar(BE_ALM_ALMACENES entAlmacenes)
        {
            BE_ALM_ALMACENES entNuevoAlmacen = new BE_ALM_ALMACENES();
            CD_alm_almacenes miFun = new CD_alm_almacenes();
            bool booOk = false;

            miFun.mysConec = mysConec;
            entNuevoAlmacen.n_idemp = entAlmacenes.n_idemp;
            entNuevoAlmacen.n_id = entAlmacenes.n_id;
            entNuevoAlmacen.n_idlocal = entAlmacenes.n_idlocal;
            entNuevoAlmacen.c_des = entAlmacenes.c_des;

            booOk = miFun.Insertar(entNuevoAlmacen);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_ALM_ALMACENES entAlmacenes)
        {
            BE_ALM_ALMACENES entNuevoAlmacen = new BE_ALM_ALMACENES();
            CD_alm_almacenes miFun = new CD_alm_almacenes();
            bool booOk = false;

            miFun.mysConec = mysConec;            
            entNuevoAlmacen.n_idemp = entAlmacenes.n_idemp;
            entNuevoAlmacen.n_id = entAlmacenes.n_id;
            entNuevoAlmacen.n_idlocal = entAlmacenes.n_idlocal;  
            entNuevoAlmacen.c_des = entAlmacenes.c_des;

            booOk = miFun.Actualizar(entNuevoAlmacen);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Eliminar(int n_Id)
        {
            CD_alm_almacenes miFun = new CD_alm_almacenes();
            bool booOk = false;

            miFun.mysConec = mysConec;

            booOk = miFun.Eliminar(n_Id);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
    }
}
