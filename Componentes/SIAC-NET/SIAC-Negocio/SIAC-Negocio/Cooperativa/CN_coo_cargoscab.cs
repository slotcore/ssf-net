using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades;
using SIAC_DATOS.Cooperativa;
using SIAC_DATOS.Ventas;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Cooperativa;

namespace SIAC_Negocio.Cooperativa
{
    public class CN_coo_cargoscab
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        
        public DataTable dtCargosCab = new DataTable();

        public void Consulta1(int n_idCargo, int n_Inicio)
        {
            DataTable dtResul = new DataTable();

            CD_coo_cargoscab miFun = new CD_coo_cargoscab();
            miFun.mysConec = mysConec;

            miFun.Consulta1(n_idCargo, n_Inicio);
            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else 
            {
                dtCargosCab = miFun.dtCargosCab;
            }

            return;
        }
        public bool ActualizarNumeracion(DataTable dtResultados)
        {
            bool b_result = false;
            int n_row = 0;
            int n_IdEmpresa;
            int n_IdCargo; 
            int n_IdSocio; 
            int n_IdSocioPuesto;
            string c_NumeroDocumento;

            CD_coo_cargoscab miFun = new CD_coo_cargoscab();
            miFun.mysConec = mysConec;

            for (n_row = 0; n_row <= dtResultados.Rows.Count-1; n_row++)
            {
                n_IdEmpresa = Convert.ToInt32(dtResultados.Rows[n_row]["n_idemp"]);
                n_IdCargo = Convert.ToInt32(dtResultados.Rows[n_row]["n_idcar"]);
                n_IdSocio = Convert.ToInt32(dtResultados.Rows[n_row]["n_idsoc"]);
                n_IdSocioPuesto = Convert.ToInt32(dtResultados.Rows[n_row]["n_idsocpue"]);
                c_NumeroDocumento = dtResultados.Rows[n_row]["c_numdoc"].ToString();

                if (miFun.ActualizarNumeracion(n_IdEmpresa, n_IdCargo, n_IdSocio, n_IdSocioPuesto, c_NumeroDocumento) == false)
                {
                    return b_result;
                }
            }
            b_result = true;
            return b_result;
        }
        public void ActualizarCargo(int n_IdCargo, int n_IdSocio, int n_IdPuesto, int n_IdConcepto, double n_IdVenta, double n_impabo)
        {
            CD_coo_cargoscab miFun = new CD_coo_cargoscab();
            miFun.mysConec = mysConec;
            miFun.ActualizarCargo(n_IdCargo, n_IdSocio, n_IdPuesto, n_IdConcepto, n_IdVenta, n_impabo);
            if (miFun.booOcurrioError == true)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
        }
        public bool InsertarCargo(BE_COO_CARGOSCAB entCargos, List<BE_COO_CARGOSDET> lstDetalle)
        {
            bool b_result = false;
            CD_coo_cargoscab miFun = new CD_coo_cargoscab();

            miFun.mysConec = mysConec;
            b_result = miFun.InsertarCargo(entCargos, lstDetalle);
            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return b_result;
        }
        public bool ExtornarPago(int n_IdDocumentoPago, int n_IdEmpresa, int n_IdSocio)
        {
            bool b_result = false;
            CD_coo_cargoscab miFun = new CD_coo_cargoscab();
            CD_vta_ventas miVen = new CD_vta_ventas();

            miFun.mysConec = mysConec;
            miFun.ExtornarPago(n_IdDocumentoPago, n_IdEmpresa, n_IdSocio);

            if (miFun.booOcurrioError == false)
            {
                miVen.mysConec = mysConec;
                if (miVen.AnularDocumento(n_IdDocumentoPago, n_IdEmpresa) == true)
                {
                    booOcurrioError = miFun.booOcurrioError;
                    StrErrorMensaje = miFun.StrErrorMensaje;
                    IntErrorNumber = miFun.IntErrorNumber;
                }
                b_result = true;
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return b_result;
        }
    }
}
