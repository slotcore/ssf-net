﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades.Sunat;
using SIAC_DATOS.Sunat;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Contabilidad;
using Helper;

namespace SIAC_Negocio.Sunat
{
    public class CN_sun_librosub
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtLista = new DataTable();
        public BE_SUN_LIBROSUB e_LibroSub = new BE_SUN_LIBROSUB();

        Helper.Comunes.Funciones funfunciones = new Helper.Comunes.Funciones();

        public void Listar()
        {
            CD_sun_librosub miFun = new CD_sun_librosub();
            miFun.mysConec = mysConec;

            miFun.Listar();
            dtLista = miFun.dtLista;
            if (dtLista == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return;
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            CD_sun_librosub miFun = new CD_sun_librosub();
            miFun.mysConec = mysConec;

            miFun.TraerRegistro(n_IdRegistro);
            dtLista = miFun.dtLista;
            if (dtLista == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            else
            {
                e_LibroSub.n_id = Convert.ToInt32(dtLista.Rows[0]["n_id"]);
                e_LibroSub.n_idlib = Convert.ToInt32(dtLista.Rows[0]["n_idlib"]);
                e_LibroSub.c_des = dtLista.Rows[0]["c_des"].ToString();
                e_LibroSub.c_codsun = dtLista.Rows[0]["c_codsun"].ToString();

            }
            return;
        }
        public bool Eliminar(int n_Idregistro)
        {
            bool b_result = false;
            CD_sun_librosub miFun = new CD_sun_librosub();

            miFun.mysConec = mysConec;
            b_result = miFun.Eliminar(n_Idregistro);
            if (b_result == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return b_result;
        }
        public bool Insertar(BE_SUN_LIBROSUB e_LibroSUb)
        {
            bool b_result = false;
            CD_sun_librosub miFun = new CD_sun_librosub();

            miFun.mysConec = mysConec;
            b_result = miFun.Insertar(e_LibroSUb);
            if (b_result == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return b_result;
        }
        public bool Actualizar(BE_SUN_LIBROSUB e_LibroSUb)
        {
            bool b_result = false;
            CD_sun_librosub miFun = new CD_sun_librosub();

            miFun.mysConec = mysConec;
            b_result = miFun.Actualizar(e_LibroSUb);
            if (b_result == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return b_result;
        }
    }
}
