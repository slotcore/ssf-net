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
    public class CN_alm_inventariolotes
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();

        public bool Insertar(BE_ALM_INVENTARIOLOTE entLotes)
        {
            BE_ALM_INVENTARIOLOTE entNuevoLote = new BE_ALM_INVENTARIOLOTE();

            CD_alm_inventariolotes miFun = new CD_alm_inventariolotes();
            miFun.mysConec = mysConec;

            entNuevoLote.n_idite = entLotes.n_idite;
            entNuevoLote.n_iddocmov = entLotes.n_iddocmov;
            entNuevoLote.d_fchmov = entLotes.d_fchmov;
            entNuevoLote.c_numlot = entLotes.c_numlot;
            entNuevoLote.n_idunimed = entLotes.n_idunimed;
            entNuevoLote.n_caning = entLotes.n_caning;
            entNuevoLote.n_cansal = entLotes.n_cansal;
            entNuevoLote.d_fchpro = entLotes.d_fchpro;
            entNuevoLote.d_fchven = entLotes.d_fchven;
            entNuevoLote.n_iddep = entLotes.n_iddep;
            entNuevoLote.n_idpro = entLotes.n_idpro;
            entNuevoLote.n_iddis = entLotes.n_iddis;
            entNuevoLote.c_oriite = entLotes.c_oriite;

            return miFun.Insertar(entLotes);
        }
        public DataTable TraerLotesConSaldo(int n_idemp, int n_iditem)
        {
            DataTable dtResult = new DataTable();
            //alm_inventariolotes_traerlotesconsaldo
            CD_alm_inventariolotes miFun = new CD_alm_inventariolotes();
            miFun.mysConec = mysConec;

            dtResult = miFun.TraerLotesConSaldo(n_idemp, n_iditem);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return dtResult;
        }
        public DataTable TraerLotesConSaldo(int n_idemp)
        {
            DataTable dtResult = new DataTable();
            //alm_inventariolotes_traerlotesconsaldo
            CD_alm_inventariolotes miFun = new CD_alm_inventariolotes();
            miFun.mysConec = mysConec;

            dtResult = miFun.TraerLotesConSaldo(n_idemp);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return dtResult;
        }
        public DataTable TraerLotesItem(int n_idemp, int n_iditem, string c_FechaIngreso)
        {
            DataTable dtResult = new DataTable();
            CD_alm_inventariolotes miFun = new CD_alm_inventariolotes();
            miFun.mysConec = mysConec;

            dtResult = miFun.TraerLotesItem(n_idemp, n_iditem, c_FechaIngreso);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return dtResult;
        }
    }
}
