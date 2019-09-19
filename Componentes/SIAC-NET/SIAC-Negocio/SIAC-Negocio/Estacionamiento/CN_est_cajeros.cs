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
        
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA;
        public BE_EST_CAJEROS e_Cajero = new BE_EST_CAJEROS();
        public DataTable dtListar = new DataTable();
        CD_est_cajeros miFun;

        public CN_est_cajeros(SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA)
        {
            CD_est_cajeros xFun = new CD_est_cajeros(STU_SISTEMA.BD_IP, STU_SISTEMA.BD_NOMBASEDATOS, STU_SISTEMA.BD_USUARIO, STU_SISTEMA.BD_CONTRASEÑA, STU_SISTEMA.BD_PUERTO);
            miFun = xFun;
        }

        ~CN_est_cajeros()
        {
            miFun = null;
            e_Cajero = null;
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

            return dtResul;
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            DataTable dtResult = new DataTable();

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
            bool booOk = false;

            booOk = miFun.Insertar(e_Cajero);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_EST_CAJEROS e_Cajero)
        {
            bool booOk = false;

            booOk = miFun.Actualizar(e_Cajero);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
    }
}
