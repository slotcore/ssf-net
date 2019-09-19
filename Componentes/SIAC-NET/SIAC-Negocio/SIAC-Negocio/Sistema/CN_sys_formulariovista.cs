using MySql.Data.MySqlClient;
using SIAC_DATOS.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Negocio.Sistema
{
    public class CN_sys_formulariovista
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public void ObtenerCabeceraLista(int n_idformulario, ref string[,] arrCabecera)
        {
            int A;
            DataTable dtResult = new DataTable();
            CD_sys_formulariovista miFun = new CD_sys_formulariovista();

            miFun.mysConec = mysConec;
            dtResult = miFun.ObtenerCabeceraLista(n_idformulario);

            if (arrCabecera.GetLength(1) == 4)
            {
                if (dtResult.Rows.Count != 0)
                {
                    for (A = 0; A <= dtResult.Rows.Count - 1; A++)
                    {
                        arrCabecera[A, 0] = dtResult.Rows[A]["c_tit"].ToString();
                        arrCabecera[A, 1] = dtResult.Rows[A]["n_anc"].ToString();
                        arrCabecera[A, 2] = dtResult.Rows[A]["c_tip"].ToString();
                        arrCabecera[A, 3] = dtResult.Rows[A]["c_nomcam"].ToString();
                    }
                }
            }
            else
            {
                if (dtResult.Rows.Count != 0)
                {
                    for (A = 0; A <= dtResult.Rows.Count - 1; A++)
                    {
                        arrCabecera[A, 0] = dtResult.Rows[A]["c_tit"].ToString();
                        arrCabecera[A, 1] = dtResult.Rows[A]["n_anc"].ToString();
                        arrCabecera[A, 2] = dtResult.Rows[A]["c_tip"].ToString();
                        arrCabecera[A, 3] = dtResult.Rows[A]["c_nomcam"].ToString();
                        arrCabecera[A, 4] = dtResult.Rows[A]["c_format"].ToString();
                    }
                }
            }
        }
    }
}
