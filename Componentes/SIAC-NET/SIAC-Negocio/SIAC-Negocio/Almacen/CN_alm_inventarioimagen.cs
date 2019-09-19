using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades.Almacen;
using SIAC_DATOS.Almacen;
using MySql.Data.MySqlClient;

namespace SIAC_Negocio.Almacen
{
    public class CN_alm_inventarioimagen
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();

        public List<BE_ALM_INVENTARIOIMAGEN> Listar(Int64 n_IdItem)
        {
            DataTable dtResul = new DataTable();
            CD_alm_inventarioimagen miFun = new CD_alm_inventarioimagen();
            List<BE_ALM_INVENTARIOIMAGEN> lstImagen = new List<BE_ALM_INVENTARIOIMAGEN>();
            
            int n_Fila = 0;
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar(n_IdItem);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else 
            {
                for (n_Fila = 0; n_Fila <= dtResul.Rows.Count - 1; n_Fila++)
                {
                    BE_ALM_INVENTARIOIMAGEN BE_Imagen = new BE_ALM_INVENTARIOIMAGEN();

                    BE_Imagen.c_des = dtResul.Rows[n_Fila]["c_des"].ToString();
                    BE_Imagen.c_nomfil = dtResul.Rows[n_Fila]["c_nomfil"].ToString();
                    BE_Imagen.n_idite = Convert.ToInt64(dtResul.Rows[n_Fila]["n_idite"]);
                    BE_Imagen.n_id = Convert.ToInt16(dtResul.Rows[n_Fila]["n_id"]);

                    lstImagen.Add(BE_Imagen);
                }
            }
            return lstImagen;
        }
        public bool Insertar(BE_ALM_INVENTARIOIMAGEN entImagenes)
        {
            CD_alm_inventarioimagen miFun = new CD_alm_inventarioimagen();
            miFun.mysConec = mysConec;

            return miFun.Insertar(entImagenes);
        }
    }
}
