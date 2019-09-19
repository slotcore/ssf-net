using MySql.Data.MySqlClient;
using SIAC_DATOS.Estacionamiento;
using SIAC_DATOS.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using SIAC_Entidades.Estacionamiento;

namespace SIAC_Negocio.Estacionamiento
{
    public class CN_est_tipocliente
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        //public BE_EST_CAJEROS e_Cajero = new BE_EST_CAJEROS();
        public DataTable dtListar = new DataTable();

        public void Listar()
        {
            DataTable dtResul = new DataTable();

            CD_est_tipocliente miFun = new CD_est_tipocliente();
            miFun.mysConec = mysConec;

            miFun.Listar();
            dtListar = miFun.dtListar;

            if (dtResul == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return;
        }
    }
}
