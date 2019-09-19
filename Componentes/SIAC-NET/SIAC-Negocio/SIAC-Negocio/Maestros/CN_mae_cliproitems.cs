using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades.Maestros;
using SIAC_DATOS.Maestros;
using MySql.Data.MySqlClient;
using Helper;


namespace SIAC_Negocio.Maestros
{
    public class CN_mae_cliproitems
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public double n_IdGenerado = 0;
        public DataTable dtListar = new DataTable();

        public void Listar(int n_IdCliente)
        {
            DataTable dtResul = new DataTable();

            CD_mae_cliproitems miFun = new CD_mae_cliproitems();
            miFun.mysConec = mysConec;

            miFun.Listar(n_IdCliente);
            dtListar = miFun.dtListar;

            if (dtListar == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return;
        }
        public bool Insertar(List<BE_MAE_CLIPROITEMS> l_Items)
        {
            CD_mae_cliproitems miFun = new CD_mae_cliproitems();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(l_Items);
            n_IdGenerado = miFun.n_IdGenerado;

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
    }
}
