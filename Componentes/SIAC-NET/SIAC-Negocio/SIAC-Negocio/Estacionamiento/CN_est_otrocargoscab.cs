using MySql.Data.MySqlClient;
using SIAC_DATOS.Estacionamiento;
using SIAC_DATOS.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using SIAC_Entidades.Estacionamiento;
using SIAC_Objetos;
using Helper.Comunes;

namespace SIAC_Negocio.Estacionamiento
{
    public class CN_est_otrocargoscab
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public int n_idcargogenerado;
        //public BE_EST_CARGOS e_Cargos = new BE_EST_CARGOS();
        //public List<BE_EST_CARGOSCAB> l_CargosCab = new List<BE_EST_CARGOSCAB>();
        //public List<BE_EST_CARGOSDET> l_CargosDet = new List<BE_EST_CARGOSDET>();
        public bool Eliminar(int n_IdRegistro)
        {
            bool booOk = false;
            CD_est_otrocargoscab miFun = new CD_est_otrocargoscab();

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
        public bool Insertar(BE_EST_OTROCARGOSCAB e_CargosCabecera, List<BE_EST_OTROCARGOSDET> l_CargosDetalle)
        {
            CD_est_otrocargoscab miFun = new CD_est_otrocargoscab();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(e_CargosCabecera, l_CargosDetalle);
            n_idcargogenerado = miFun.n_idcargogenerado;

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_EST_OTROCARGOSCAB e_CargosCabecera, List<BE_EST_OTROCARGOSDET> l_CargosDetalle)
        {
            CD_est_otrocargoscab miFun = new CD_est_otrocargoscab();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(e_CargosCabecera, l_CargosDetalle);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool ActualizarDocVenta(int n_IdCargo, int n_IdDocumentoPago, string c_FechaPago)
        {
            bool b_result = false;

            CD_est_otrocargoscab miFun = new CD_est_otrocargoscab();
            miFun.mysConec = mysConec;

            if (miFun.ActualizarDocVenta(n_IdCargo, n_IdDocumentoPago, c_FechaPago) == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_result;
            }
            b_result = true;
            return b_result;
        }
    }
}
