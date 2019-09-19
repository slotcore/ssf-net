using MySql.Data.MySqlClient;
using SIAC_DATOS.Estacionamiento;
using SIAC_DATOS.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using SIAC_Entidades.Estacionamiento;

namespace SIAC_Negocio.Estacionamiento
{
    public class CN_est_cajeros
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public BE_EST_CAJEROS e_Cajero = new BE_EST_CAJEROS();
        public DataTable dtListar = new DataTable();

        public DataTable Listar(int n_IdEmpresa)
        {
            DataTable dtResul = new DataTable();

            CD_est_cajeros miFun = new CD_est_cajeros();
            miFun.mysConec = mysConec;

            miFun.Listar(n_IdEmpresa);
            dtListar = miFun.dtListar;

            if (dtResul == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return dtResul;
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            DataTable dtResult = new DataTable();
            CD_est_cajeros miFun = new CD_est_cajeros();

            miFun.mysConec = mysConec;

            miFun.TraerRegistro(n_IdRegistro);
            dtListar = miFun.dtListar;

            if (dtListar.Rows.Count != 0)
            {
                e_Cajero.n_idemp = Convert.ToInt16(dtListar.Rows[0]["n_idemp"]);
                e_Cajero.n_idloc = Convert.ToInt16(dtListar.Rows[0]["n_idloc"]);
                e_Cajero.n_id = Convert.ToInt16(dtListar.Rows[0]["n_id"]);
                e_Cajero.n_idtra = Convert.ToInt16(dtListar.Rows[0]["n_idtra"]);
                e_Cajero.n_idusu = Convert.ToInt16(dtListar.Rows[0]["n_idusu"]);
            }
            if (b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool booOk = false;
            CD_est_cajeros miFun = new CD_est_cajeros();

            miFun.mysConec = mysConec;

            booOk = miFun.Eliminar(n_IdRegistro);
            if (booOk == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;

            }
            return booOk;
        }
        public bool Insertar(BE_EST_CAJEROS e_Cajero)
        {
            CD_est_cajeros miFun = new CD_est_cajeros();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(e_Cajero);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_EST_CAJEROS e_Cajero)
        {
            CD_est_cajeros miFun = new CD_est_cajeros();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(e_Cajero);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
    }
}
