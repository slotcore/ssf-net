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
    public class CN_vta_vehiculo
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        public DataTable Listar(int n_IdEmp)
        {
            DataTable dtResul = new DataTable();

            CD_vta_vehiculo miFun = new CD_vta_vehiculo();
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
        public BE_VTA_VEHICULO TraerRegistro(int n_IdRegistro)
        {
            BE_VTA_VEHICULO entVehiculo = new BE_VTA_VEHICULO();

            CD_vta_vehiculo miFun = new CD_vta_vehiculo();
            miFun.mysConec = mysConec;
            entVehiculo = miFun.TraerRegistro(n_IdRegistro);

            return entVehiculo;
        }
        public bool Insertar(BE_VTA_VEHICULO entVehiculo)
        {
            BE_VTA_VEHICULO entNuevoVehiculo = new BE_VTA_VEHICULO();
            CD_vta_vehiculo miFun = new CD_vta_vehiculo();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoVehiculo.n_idemp = entVehiculo.n_idemp;
            entNuevoVehiculo.n_id = entVehiculo.n_id;
            entNuevoVehiculo.c_marca = entVehiculo.c_marca;
            entNuevoVehiculo.c_numpla = entVehiculo.c_numpla;

            booOk = miFun.Insertar(entNuevoVehiculo);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_VTA_VEHICULO entVehiculo)
        {
            BE_VTA_VEHICULO entNuevoVehiculo = new BE_VTA_VEHICULO();
            CD_vta_vehiculo miFun = new CD_vta_vehiculo();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoVehiculo.n_idemp = entVehiculo.n_idemp;
            entNuevoVehiculo.n_id = entVehiculo.n_id;
            entNuevoVehiculo.c_marca = entVehiculo.c_marca;
            entNuevoVehiculo.c_numpla = entVehiculo.c_numpla;

            booOk = miFun.Actualizar(entNuevoVehiculo);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Eliminar(int n_Id)
        {
            CD_vta_vehiculo miFun = new CD_vta_vehiculo();
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
