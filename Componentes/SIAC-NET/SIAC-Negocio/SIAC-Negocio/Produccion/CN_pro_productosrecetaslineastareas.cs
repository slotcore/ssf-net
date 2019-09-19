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
    public class CN_pro_productosrecetaslineastareas
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();

        public DataTable dtLineasTar = new DataTable();

        public bool ListarTodasTareas(int n_IdEmpresa)
        {
            bool b_result = false;

            CD_pro_productosrecetaslineastareas miFun = new CD_pro_productosrecetaslineastareas();
            miFun.mysConec = mysConec;

            b_result = miFun.ListarTodasTareas(n_IdEmpresa);

            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                dtLineasTar = miFun.dtLineasTar;
            }

            return b_result;
        }
        public bool Consulta1(int n_IdProducto, int n_IdReceta, int n_idLinea, string c_Condicion)
        {
            bool b_result = false;

            CD_pro_productosrecetaslineastareas miFun = new CD_pro_productosrecetaslineastareas();
            miFun.mysConec = mysConec;

            b_result = miFun.Consulta1(n_IdProducto, n_IdReceta, n_idLinea, c_Condicion);

            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                dtLineasTar = miFun.dtLineasTar;
            }

            return b_result;
        }
    }
}
