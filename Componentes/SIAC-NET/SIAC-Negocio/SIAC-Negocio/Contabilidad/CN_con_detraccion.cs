using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades;
using SIAC_DATOS.Contabilidad;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Contabilidad;
using Helper;

namespace SIAC_Negocio.Contabilidad
{
    public class CN_con_detraccion
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtLista = new DataTable();
        public BE_CON_DETRACCION e_Detraccion = new BE_CON_DETRACCION();

        Helper.Comunes.Funciones funfunciones = new Helper.Comunes.Funciones();

        public void Listar()
        {
            CD_con_detraccion miFun = new CD_con_detraccion();
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
            CD_con_detraccion miFun = new CD_con_detraccion();
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
                e_Detraccion.n_id = Convert.ToInt32(dtLista.Rows[0]["n_id"]);
                e_Detraccion.c_des = dtLista.Rows[0]["c_des"].ToString();
                e_Detraccion.n_tasa = Convert.ToDouble(funfunciones.NulosN(dtLista.Rows[0]["n_tasa"]));
                e_Detraccion.n_impbase = Convert.ToDouble(funfunciones.NulosN(dtLista.Rows[0]["n_impbase"]));
            }
            return;
        }
        public bool Eliminar(int n_Idregistro)
        {
            bool b_result = false;
            CD_con_detraccion miFun = new CD_con_detraccion();

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
        public bool Insertar(BE_CON_DETRACCION e_Detraccion)
        {
            bool b_result = false;
            CD_con_detraccion miFun = new CD_con_detraccion();

            miFun.mysConec = mysConec;
            b_result = miFun.Insertar(e_Detraccion);
            if (b_result == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return b_result;
        }
        public bool Actualizar(BE_CON_DETRACCION e_Detraccion)
        {
            bool b_result = false;
            CD_con_detraccion miFun = new CD_con_detraccion();

            miFun.mysConec = mysConec;
            b_result = miFun.Actualizar(e_Detraccion);
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
