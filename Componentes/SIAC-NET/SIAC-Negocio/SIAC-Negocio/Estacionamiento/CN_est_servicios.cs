using MySql.Data.MySqlClient;
using SIAC_DATOS.Estacionamiento;
using SIAC_DATOS.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using SIAC_Entidades.Estacionamiento;
using Helper;

namespace SIAC_Negocio.Estacionamiento
{
    public class CN_est_servicios
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
                
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA;
        public BE_EST_SERVICIOS e_Servicio = new BE_EST_SERVICIOS();
        public DataTable dtListar = new DataTable();

        Helper.Comunes.Funciones funfun = new Helper.Comunes.Funciones();
        CD_est_servicios miFun;

        public CN_est_servicios(SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA)
        {
            CD_est_servicios xFun = new CD_est_servicios(STU_SISTEMA.BD_IP, STU_SISTEMA.BD_NOMBASEDATOS, STU_SISTEMA.BD_USUARIO, STU_SISTEMA.BD_CONTRASEÑA, STU_SISTEMA.BD_PUERTO);
            miFun = xFun;
        }
        ~CN_est_servicios()
        {
            miFun = null;
            e_Servicio = null;
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
                e_Servicio.n_idemp = Convert.ToInt32(dtListar.Rows[0]["n_idemp"]);
                e_Servicio.n_id = Convert.ToInt32(dtListar.Rows[0]["n_id"]);
                e_Servicio.n_idpla = Convert.ToInt32(dtListar.Rows[0]["n_idpla"]);
                e_Servicio.c_des = dtListar.Rows[0]["c_des"].ToString();
                e_Servicio.c_cod = dtListar.Rows[0]["c_cod"].ToString();
                e_Servicio.n_idunimed = Convert.ToInt32(dtListar.Rows[0]["n_idunimed"]);
                e_Servicio.n_impbru = Convert.ToDouble(dtListar.Rows[0]["n_impbru"]);
                e_Servicio.n_imptot = Convert.ToDouble(dtListar.Rows[0]["n_imptot"]);
                e_Servicio.n_idmon = Convert.ToInt32(dtListar.Rows[0]["n_idmon"]);
                e_Servicio.n_idfan = Convert.ToInt32(dtListar.Rows[0]["n_idfan"]);
                e_Servicio.n_idcla = Convert.ToInt32(dtListar.Rows[0]["n_idcla"]);
                e_Servicio.n_idsubcla = Convert.ToInt32(dtListar.Rows[0]["n_idsubcla"]);
                e_Servicio.n_idtipexi = Convert.ToInt32(dtListar.Rows[0]["n_idtipexi"]);

                e_Servicio.n_pagser = Convert.ToInt32(dtListar.Rows[0]["n_pagser"]);
                e_Servicio.n_idcal = Convert.ToInt32(dtListar.Rows[0]["n_idcal"]);

                e_Servicio.c_horini = dtListar.Rows[0]["c_horini"].ToString();
                e_Servicio.c_horfin = dtListar.Rows[0]["c_horfin"].ToString();

                e_Servicio.n_numhorser = Convert.ToInt32(funfun.NulosN(dtListar.Rows[0]["n_numhorser"]));
                e_Servicio.n_aplfra = Convert.ToInt32(funfun.NulosN(dtListar.Rows[0]["n_aplfra"]));
                e_Servicio.n_apltolmedpag = Convert.ToInt32(funfun.NulosN(dtListar.Rows[0]["n_apltolmedpag"]));
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
        public bool Insertar(BE_EST_SERVICIOS e_Servicio)
        {
            bool booOk = false;

            booOk = miFun.Insertar(e_Servicio);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_EST_SERVICIOS e_Servicio)
        {
            bool booOk = false;

            booOk = miFun.Actualizar(e_Servicio);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
    }
}
