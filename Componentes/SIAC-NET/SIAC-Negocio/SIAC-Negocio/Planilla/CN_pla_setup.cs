using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades.Planillas;
using SIAC_DATOS.Planilla;
using MySql.Data.MySqlClient;
using Helper;

namespace SIAC_Negocio.Planilla
{
    public class CN_pla_setup
    {
        public DataTable dtLista;
        public DataTable dtRegistro;

        public bool b_OcurrioError;
        public string c_ErrorMensaje;
        public int n_ErrorNumber;

        public MySqlConnection mysConec;

        DatosMySql xMiFuncion = new DatosMySql();
        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFunGen = new Helper.Genericas();
        
        public bool Listar(int n_IdEmpresa)
        {
            bool b_result = false;

            CD_pla_setup miFun = new CD_pla_setup();
            miFun.mysConec = mysConec;

            if (miFun.Listar(n_IdEmpresa) == true)
            {
                b_result = true;
                dtLista = miFun.dtLista;
            }
            else
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return b_result;
        }
    }
}
