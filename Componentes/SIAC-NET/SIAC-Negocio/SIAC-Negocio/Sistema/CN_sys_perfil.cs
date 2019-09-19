using MySql.Data.MySqlClient;
using SIAC_DATOS.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIAC_Entidades.Sistema;
using System.Windows.Forms;

namespace SIAC_Negocio.Sistema
{
    public class CN_sys_perfil
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtLista = new DataTable();

        Helper.Comunes.Funciones funFunciones = new Helper.Comunes.Funciones();
        public void ActivarOpcionesPerfil(int n_IdPerfil, System.Windows.Forms.MenuStrip o_Menu)
        {
            DataTable dtResul = new DataTable();
            //TraerPerfil
            CD_sys_perfil miFun = new CD_sys_perfil();
            miFun.mysConec = mysConec;
            miFun.TraerPerfil(n_IdPerfil);
            dtLista = miFun.dtLista;
            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return;
            }
            int n_row = 0;
            int n_fil = 0;
            for (n_row = 0; n_row <= o_Menu.Items.Count - 1; n_row++)
            {
                for (n_fil = 0; n_fil <= dtLista.Rows.Count - 1; n_fil++)
                {
                    if (o_Menu.Items[n_row].Text == dtLista.Rows[n_fil]["c_des"].ToString())
                    {
                        if (Convert.ToInt16(funFunciones.NulosN(dtLista.Rows[n_fil]["n_visible"])) == 1)
                        {
                            o_Menu.Items[n_row].Enabled = true;
                        }
                        else 
                        {
                            o_Menu.Items[n_row].Enabled = false;
                        }
                        break;
                    }
                }
            }
            return;
        }
    }
}
