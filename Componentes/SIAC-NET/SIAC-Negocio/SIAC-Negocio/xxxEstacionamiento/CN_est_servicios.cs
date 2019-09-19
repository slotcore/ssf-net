using MySql.Data.MySqlClient;
using SIAC_DATOS.Estacionamiento;
using SIAC_DATOS.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using SIAC_Entidades.Estacionamiento;

namespace SIAC_Negocio.Estacionamiento
{
    public class CN_est_servicios
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public BE_EST_SERVICIOS e_Servicio = new BE_EST_SERVICIOS();
        public DataTable dtListar = new DataTable();
        public DataTable Listar(int n_IdEmpresa)
        {
            DataTable dtResul = new DataTable();

            CD_est_servicios miFun = new CD_est_servicios();
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
            CD_est_servicios miFun = new CD_est_servicios();

            miFun.mysConec = mysConec;

            miFun.TraerRegistro(n_IdRegistro);
            dtListar = miFun.dtListar;

            if (dtListar.Rows.Count != 0)
            {
                e_Servicio.n_idemp = Convert.ToInt16(dtListar.Rows[0]["n_idemp"]);
                e_Servicio.n_id = Convert.ToInt16(dtListar.Rows[0]["n_id"]);
                e_Servicio.n_idpla = Convert.ToInt16(dtListar.Rows[0]["n_idpla"]);
                e_Servicio.c_des = dtListar.Rows[0]["c_des"].ToString();
                e_Servicio.c_cod = dtListar.Rows[0]["c_cod"].ToString();
                e_Servicio.n_idunimed = Convert.ToInt16(dtListar.Rows[0]["n_idunimed"]);
                e_Servicio.n_impbru = Convert.ToDouble(dtListar.Rows[0]["n_impbru"]);
                e_Servicio.n_imptot = Convert.ToDouble(dtListar.Rows[0]["n_imptot"]);
                e_Servicio.n_idmon = Convert.ToInt16(dtListar.Rows[0]["n_idmon"]);
                e_Servicio.n_idfan = Convert.ToInt16(dtListar.Rows[0]["n_idfan"]);
                e_Servicio.n_idcla = Convert.ToInt16(dtListar.Rows[0]["n_idcla"]);
                e_Servicio.n_idsubcla = Convert.ToInt16(dtListar.Rows[0]["n_idsubcla"]);
                e_Servicio.n_idtipexi = Convert.ToInt16(dtListar.Rows[0]["n_idtipexi"]);
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
            CD_est_servicios miFun = new CD_est_servicios();

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
        public bool Insertar(BE_EST_SERVICIOS e_Servicio)
        {
            CD_est_servicios miFun = new CD_est_servicios();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(e_Servicio);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_EST_SERVICIOS e_Servicio)
        {
            CD_est_servicios miFun = new CD_est_servicios();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(e_Servicio);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
    }
}
