using MySql.Data.MySqlClient;
using SIAC_DATOS.Tesoreria;
using SIAC_DATOS.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using SIAC_Entidades.Tesoreria;

namespace SIAC_Negocio.Tesoreria
{
    public class CN_tes_lettippla
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public BE_TES_LETTIPPLA e_TipPla = new BE_TES_LETTIPPLA();
        public DataTable dtLista = new DataTable();

        public void Listar()
        {
            CD_tes_lettippla miFun = new CD_tes_lettippla();
            miFun.mysConec = mysConec;

            dtLista = miFun.Listar();

            if (miFun.b_OcurrioError==true)
            {
                dtLista = null;
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return;
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            DataTable dtResult = new DataTable();
            CD_tes_lettippla miFun = new CD_tes_lettippla();
            miFun.mysConec = mysConec;

            if (miFun.TraerRegistro(n_IdRegistro) == true) 
            { 
                dtResult = miFun.dtLista;

                e_TipPla.n_id = Convert.ToInt32(dtResult.Rows[0]["n_id"]);
                e_TipPla.c_des = dtResult.Rows[0]["c_des"].ToString();
                e_TipPla.n_numdia = Convert.ToInt32(dtResult.Rows[0]["n_numdia"]);
            }
            else
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool b_Result = false;
            CD_tes_lettippla miFun = new CD_tes_lettippla();

            miFun.mysConec = mysConec;
            if (miFun.Eliminar(n_IdRegistro) == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_Result;
            }
            b_Result = true;
            return b_Result;
        }
        public bool Insertar(BE_TES_LETTIPPLA e_TipoPlazo)
        {
            CD_tes_lettippla miFun = new CD_tes_lettippla();
            bool b_Result = false;

            miFun.mysConec = mysConec;
            if (miFun.Insertar(e_TipoPlazo) == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_Result;
            }
            b_Result = true;
            return b_Result;
        }
        public bool Actualizar(BE_TES_LETTIPPLA e_TipoPlazo)
        {
            CD_tes_lettippla miFun = new CD_tes_lettippla();
            bool b_Result = false;

            miFun.mysConec = mysConec;
            if (miFun.Actualizar(e_TipoPlazo) == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_Result;
            }
            b_Result = true;
            return b_Result;
        }
    }
}
