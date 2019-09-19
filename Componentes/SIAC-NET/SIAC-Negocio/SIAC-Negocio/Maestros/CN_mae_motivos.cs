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

namespace SIAC_Negocio.Maestros
{
    public class CN_mae_motivos
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        public DataTable Listar(int n_IdEmpresa, int n_IdModulo)
        {
            DataTable dtResul = new DataTable();

            CD_mae_motivos miFun = new CD_mae_motivos();
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar(n_IdEmpresa, n_IdModulo);

            if (dtResul == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return dtResul;
        }
        public BE_MAE_MOTIVOS TraerRegistro(int n_IdRegistro)
        {
            BE_MAE_MOTIVOS e_Motivos = new BE_MAE_MOTIVOS();

            CD_mae_motivos miFun = new CD_mae_motivos();
            miFun.mysConec = mysConec;
            e_Motivos = miFun.TraerRegistro(n_IdRegistro);

            return e_Motivos;
        }
        public bool Insertar(BE_MAE_MOTIVOS e_Motivos)
        {
            CD_mae_motivos miFun = new CD_mae_motivos();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(e_Motivos);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_MAE_MOTIVOS e_Motivos)
        {
            CD_mae_motivos miFun = new CD_mae_motivos();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(e_Motivos);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool Eliminar(int n_Id)
        {
            CD_mae_motivos miFun = new CD_mae_motivos();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Eliminar(n_Id);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
    }
}
