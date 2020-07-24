using MySql.Data.MySqlClient;
using SIAC_DATOS.Almacen;
using SIAC_Entidades;
using SIAC_Entidades.Almacen;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIAC_Negocio.Almacen
{
    public class CN_alm_transferencias
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();

        public DataTable Listar(int n_idempresa, int n_idmes, int n_idano)
        {
            DataTable dtResul = new DataTable();
            CD_alm_transferencias miFun = new CD_alm_transferencias();
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar(n_idempresa, n_idmes, n_idano);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public BE_ALM_TRANSFERENCIAS TraerRegistro(int n_IdRegistro)
        {
            BE_ALM_TRANSFERENCIAS entAlmacen = new BE_ALM_TRANSFERENCIAS();

            CD_alm_transferencias miFun = new CD_alm_transferencias();
            miFun.mysConec = mysConec;
            entAlmacen = miFun.TraerRegistro(n_IdRegistro);

            return entAlmacen;
        }
        public bool Insertar(BE_ALM_TRANSFERENCIAS entTransferencias)
        {
            BE_ALM_TRANSFERENCIAS entNuevoAlmacen = new BE_ALM_TRANSFERENCIAS();
            CD_alm_transferencias miFun = new CD_alm_transferencias();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(entTransferencias);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_ALM_TRANSFERENCIAS entTransferencias)
        {
            BE_ALM_TRANSFERENCIAS entNuevoAlmacen = new BE_ALM_TRANSFERENCIAS();
            CD_alm_transferencias miFun = new CD_alm_transferencias();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(entTransferencias);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Eliminar(int n_Id)
        {
            CD_alm_transferencias miFun = new CD_alm_transferencias();
            bool booOk = false;

            miFun.mysConec = mysConec;

            booOk = miFun.Eliminar(n_Id);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }

        public bool DocumentoExiste(int n_IdEmpresa, string c_NumSerie, string c_NumDocumento)
        {
            DataTable dtResul = new DataTable();
            bool b_resul = false;

            CD_alm_transferencias miFun = new CD_alm_transferencias();
            miFun.mysConec = mysConec;

            if (miFun.DocumentoExiste(n_IdEmpresa, c_NumSerie, c_NumDocumento) == true)
            {
                dtResul = miFun.dtMovimiento;
                if (dtResul.Rows.Count >= 1)
                {
                    //  SI SE ENCONTRO DOCUMENTO
                    b_resul = true;
                }
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return b_resul;
        }
    }
}
