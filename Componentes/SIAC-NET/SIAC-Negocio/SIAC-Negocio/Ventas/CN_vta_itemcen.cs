using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades.Ventas;
using SIAC_DATOS.Ventas;
using MySql.Data.MySqlClient;

namespace SIAC_Negocio.Ventas
{
    public class CN_vta_itemcen
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        public DataTable Listar()
        {
            DataTable dtResul = new DataTable();

            CD_vta_itemcen miFun = new CD_vta_itemcen();
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar();

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public BE_VTA_ITEMCEN TraerRegistro(int n_IdRegistro)
        {
            BE_VTA_ITEMCEN entItemCEN = new BE_VTA_ITEMCEN();

            CD_vta_itemcen miFun = new CD_vta_itemcen();
            miFun.mysConec = mysConec;
            entItemCEN = miFun.TraerRegistro(n_IdRegistro);

            return entItemCEN;
        }
        public bool Insertar(BE_VTA_ITEMCEN entItenCen)
        {
            BE_VTA_ITEMCEN entNuevoItenCen = new BE_VTA_ITEMCEN();
            CD_vta_itemcen miFun = new CD_vta_itemcen();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoItenCen.n_idemp = entItenCen.n_idemp;
            entNuevoItenCen.n_id = entItenCen.n_id;
            entNuevoItenCen.c_codcen = entItenCen.c_codcen;
            entNuevoItenCen.c_descen = entItenCen.c_descen;
            entNuevoItenCen.n_iditem = entItenCen.n_iditem;
            entNuevoItenCen.n_idcli = entItenCen.n_idcli;

            booOk = miFun.Insertar(entNuevoItenCen);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_VTA_ITEMCEN entItenCen)
        {
            BE_VTA_ITEMCEN entNuevoItenCen = new BE_VTA_ITEMCEN();
            CD_vta_itemcen miFun = new CD_vta_itemcen();
            bool booOk = false;

            miFun.mysConec = mysConec;

            entNuevoItenCen.n_idemp = entItenCen.n_idemp;
            entNuevoItenCen.n_id = entItenCen.n_id;
            entNuevoItenCen.c_codcen = entItenCen.c_codcen;
            entNuevoItenCen.c_descen = entItenCen.c_descen;
            entNuevoItenCen.n_iditem = entItenCen.n_iditem;
            entNuevoItenCen.n_idcli = entItenCen.n_idcli;

            booOk = miFun.Actualizar(entNuevoItenCen);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Eliminar(int n_Id)
        {
            CD_vta_itemcen miFun = new CD_vta_itemcen();
            bool booOk = false;

            miFun.mysConec = mysConec;

            booOk = miFun.Eliminar(n_Id);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
    }
}
