using MySql.Data.MySqlClient;
using SIAC_DATOS.Almacen;
using SIAC_DATOS.Produccion;
using SIAC_DATOS.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using SIAC_Entidades.Produccion;

namespace SIAC_Negocio.Produccion
{
    public class CN_pro_rendimiento
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public BE_PRO_RENDIMIENTO entRendimiento = new BE_PRO_RENDIMIENTO();

        public DataTable dtLista = new DataTable();

        public bool Listar(int n_IdEmpresa, int n_EsUnificado)
        {
            bool booResult = false;

            CD_pro_rendimiento miFun = new CD_pro_rendimiento();
            miFun.mysConec = mysConec;

            if (miFun.Listar(n_IdEmpresa, n_EsUnificado) != false)
            {
                dtLista = miFun.dtListar;
                booResult = true;
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return booResult;
        }
        public bool TraerRegistro(int n_IdRegistro)
        {
            DataTable dtResult = new DataTable();
            bool booResult;
            CD_pro_rendimiento miFun = new CD_pro_rendimiento();

            miFun.mysConec = mysConec;

            booResult = miFun.TraerRegistro(n_IdRegistro);
            dtResult = miFun.dtRegistro;

            if (dtResult.Rows.Count != 0)
            {
                entRendimiento.n_idemp = Convert.ToInt32(dtResult.Rows[0]["n_idemp"]);
                entRendimiento.n_id = Convert.ToInt32(dtResult.Rows[0]["n_id"]);
                entRendimiento.n_idmatpri = Convert.ToInt32(dtResult.Rows[0]["n_idmatpri"]);
                entRendimiento.n_idpro = Convert.ToInt32(dtResult.Rows[0]["n_idpro"]);
                entRendimiento.n_porren = Convert.ToDouble(dtResult.Rows[0]["n_porren"]);

                booResult = true;
            }
            if (booResult == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return booResult;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool booResult = false;
            CD_pro_rendimiento miFun = new CD_pro_rendimiento();

            miFun.mysConec = mysConec;

            booResult = miFun.Eliminar(n_IdRegistro);
            if (booResult == false)
            {
                booOcurrioError = false;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return booResult;
        }
        public bool Insertar(BE_PRO_RENDIMIENTO entRendimiento)
        {
            CD_pro_rendimiento miFun = new CD_pro_rendimiento();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(entRendimiento);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_PRO_RENDIMIENTO entRendimiento)
        {
            CD_pro_rendimiento miFun = new CD_pro_rendimiento();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(entRendimiento);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
    }
}
