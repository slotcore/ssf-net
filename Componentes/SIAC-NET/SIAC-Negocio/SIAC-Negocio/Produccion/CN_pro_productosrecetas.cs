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
    public class CN_pro_productosrecetas
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();

        public DataTable dtRecetas = new DataTable();
        public DataTable dtLista = new DataTable();
        //public BE_PRO_PRODUCTOS entRegistro = new BE_PRO_PRODUCTOS();
        //public List<BE_PRO_PRODUCTOSRECETAS> lstRecetas = new List<BE_PRO_PRODUCTOSRECETAS>();
        //public List<BE_PRO_PRODUCTOSRECETASLINEAS> lstRecetas = new List<BE_PRO_PRODUCTOSRECETASLINEAS>();
        public bool ListarTodasRecetas(int n_idempresa)
        {
            bool b_result = false;

            CD_pro_productosrecetas miFun = new CD_pro_productosrecetas();
            miFun.mysConec = mysConec;

            b_result = miFun.ListarTodasRecetas(n_idempresa);

            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                dtRecetas = miFun.dtRecetas;
            }

            return b_result;
        }
        public void Consulta1(string c_ListaId)
        {
            CD_pro_productosrecetas miFun = new CD_pro_productosrecetas();
            miFun.mysConec = mysConec;
            miFun.Consulta1(c_ListaId);
            dtLista = miFun.dtLista;
            if (miFun.IntErrorNumber != 0)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return;
        }
        public void Consulta3()
        {
            CD_pro_productosrecetas miFun = new CD_pro_productosrecetas();
            miFun.mysConec = mysConec;
            miFun.Consulta3();
            dtLista = miFun.dtLista;
            if (miFun.IntErrorNumber != 0)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return;
        }
    }
}
