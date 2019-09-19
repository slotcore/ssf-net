using MySql.Data.MySqlClient;
using SIAC_DATOS.Estacionamiento;
using SIAC_DATOS.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using SIAC_Entidades.Estacionamiento;

namespace SIAC_Negocio.Estacionamiento
{
    public class CN_est_localsetup
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public BE_EST_LOCALSETUP e_Local = new BE_EST_LOCALSETUP();
        public DataTable dtListar = new DataTable();

        public DataTable Listar(int n_IdEmpresa)
        {
            DataTable dtResul = new DataTable();

            CD_est_localsetup miFun = new CD_est_localsetup();
            miFun.mysConec = mysConec;

            miFun.Listar(n_IdEmpresa);
            dtListar = miFun.dtListar;

            if (dtResul == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return dtListar;
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            DataTable dtResult = new DataTable();
            CD_est_localsetup miFun = new CD_est_localsetup();

            miFun.mysConec = mysConec;

            miFun.TraerRegistro(n_IdRegistro);
            dtListar = miFun.dtListar;

            if (dtListar.Rows.Count != 0)
            {
                e_Local.n_idloc = Convert.ToInt16(dtListar.Rows[0]["n_idloc"]);
                e_Local.n_idtipcob = Convert.ToInt16(dtListar.Rows[0]["n_idtipcob"]);
                e_Local.c_numserfac = dtListar.Rows[0]["c_numserfac"].ToString();
                e_Local.c_numserbol = dtListar.Rows[0]["c_numserbol"].ToString();
                e_Local.c_numsertik = dtListar.Rows[0]["c_numsertik"].ToString();
                e_Local.n_iddocdef = Convert.ToInt16(dtListar.Rows[0]["n_iddocdef"]);
                e_Local.n_idserdef = Convert.ToInt16(dtListar.Rows[0]["n_idserdef"]);
                e_Local.n_vispre = Convert.ToInt16(dtListar.Rows[0]["n_vispre"]);
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
            CD_est_localsetup miFun = new CD_est_localsetup();

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
        public bool Insertar(BE_EST_LOCALSETUP e_Local)
        {
            CD_est_localsetup miFun = new CD_est_localsetup();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(e_Local);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_EST_LOCALSETUP e_Local)
        {
            CD_est_localsetup miFun = new CD_est_localsetup();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(e_Local);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
    }

}
