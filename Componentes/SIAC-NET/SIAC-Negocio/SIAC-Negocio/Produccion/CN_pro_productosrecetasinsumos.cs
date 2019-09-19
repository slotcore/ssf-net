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
    public class CN_pro_productosrecetasinsumos
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();

        public DataTable dtInsumos = new DataTable();

        public bool ListarTodasLineas(int n_idempresa)
        {
            bool b_result = false;

            CD_pro_productosrecetasinsumos miFun = new CD_pro_productosrecetasinsumos();
            miFun.mysConec = mysConec;

            b_result = miFun.ListarTodo(n_idempresa);

            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                dtInsumos = miFun.dtInsumos;
            }

            return b_result;
        }
    }
}
