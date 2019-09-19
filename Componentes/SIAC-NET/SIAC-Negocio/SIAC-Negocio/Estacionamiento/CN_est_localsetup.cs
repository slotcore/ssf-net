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

        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA;
        public BE_EST_LOCALSETUP e_Local = new BE_EST_LOCALSETUP();
        public DataTable dtListar = new DataTable();

        CD_est_localsetup miFun;
         public CN_est_localsetup(SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA)
        {
            CD_est_localsetup xFun = new CD_est_localsetup(STU_SISTEMA.BD_IP, STU_SISTEMA.BD_NOMBASEDATOS, STU_SISTEMA.BD_USUARIO, STU_SISTEMA.BD_CONTRASEÑA, STU_SISTEMA.BD_PUERTO);
            miFun = xFun;
        }

         ~CN_est_localsetup()
        {
            miFun = null;

            e_Local = null;
        }
        public DataTable Listar(int n_IdEmpresa)
        {
            DataTable dtResul = new DataTable();

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
                e_Local.n_idserhor = Convert.ToInt16(dtListar.Rows[0]["n_idserhor"]);
                e_Local.n_tolmin = Convert.ToInt16(dtListar.Rows[0]["n_tolmin"]);
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
            bool booOk = false;

            booOk = miFun.Insertar(e_Local);
            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_EST_LOCALSETUP e_Local)
        {
            bool booOk = false;

            booOk = miFun.Actualizar(e_Local);
            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
    }
}
