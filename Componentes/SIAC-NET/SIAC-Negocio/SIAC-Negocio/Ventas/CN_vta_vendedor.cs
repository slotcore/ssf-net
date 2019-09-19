using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades.Ventas;
using SIAC_DATOS.Ventas;
using MySql.Data.MySqlClient;

namespace SIAC_Negocio.Ventas
{
    public class CN_vta_vendedor
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable Listar(int n_IdEmp)
        {
            DataTable dtResul = new DataTable();

            CD_vta_vendedor miFun = new CD_vta_vendedor();
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar(n_IdEmp);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public BE_VTA_VENDEDOR TraerRegistro(int n_IdRegistro)
        {
            BE_VTA_VENDEDOR entVendedor = new BE_VTA_VENDEDOR();

            CD_vta_vendedor miFun = new CD_vta_vendedor();
            miFun.mysConec = mysConec;
            entVendedor = miFun.TraerRegistro(n_IdRegistro);

            return entVendedor;
        }
        public bool Insertar(BE_VTA_VENDEDOR entVendedor)
        {
            BE_VTA_VENDEDOR entNuevoVendedor = new BE_VTA_VENDEDOR();
            CD_vta_vendedor miFun = new CD_vta_vendedor();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoVendedor.n_id = entVendedor.n_id;
            entNuevoVendedor.n_idemp = entVendedor.n_idemp;
            entNuevoVendedor.n_idper = entVendedor.n_idper;
            entNuevoVendedor.n_impbas = entVendedor.n_impbas;
            entNuevoVendedor.n_porcom = entVendedor.n_porcom;

            booOk = miFun.Insertar(entNuevoVendedor);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_VTA_VENDEDOR entVendedor)
        {
            BE_VTA_VENDEDOR entNuevoVendedor = new BE_VTA_VENDEDOR();
            CD_vta_vendedor miFun = new CD_vta_vendedor();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoVendedor.n_id = entVendedor.n_id;
            entNuevoVendedor.n_idemp = entVendedor.n_idemp;
            entNuevoVendedor.n_idper = entVendedor.n_idper;
            entNuevoVendedor.n_impbas = entVendedor.n_impbas;
            entNuevoVendedor.n_porcom = entVendedor.n_porcom;

            booOk = miFun.Actualizar(entNuevoVendedor);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Eliminar(int n_Id)
        {
            CD_vta_vendedor miFun = new CD_vta_vendedor();
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
