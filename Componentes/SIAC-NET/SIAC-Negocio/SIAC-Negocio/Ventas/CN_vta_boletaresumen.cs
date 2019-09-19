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
using Helper;

namespace SIAC_Negocio.Ventas
{
    public class CN_vta_boletaresumen
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public MySqlConnection mysConec = new MySqlConnection();
        public BE_VTA_BOLETARESUMEN e_Boletas = new BE_VTA_BOLETARESUMEN();
        public List<BE_VTA_BOLETARESUMENDET> l_BoletasDet = new List<BE_VTA_BOLETARESUMENDET>();
        public DataTable dtBoletasdet = new DataTable();
        public DataTable dtLista = new DataTable();

        Genericas funDatos = new Genericas();

        public void Listar(int n_idempresa, int n_anotrabajo, int n_mestrabajo)
        {
            DataTable dtResul = new DataTable();

            CD_vta_boletaresumen miFun = new CD_vta_boletaresumen();
            miFun.mysConec = mysConec;

            miFun.Listar(n_idempresa, n_anotrabajo, n_mestrabajo);
            if (miFun.IntErrorNumber != 0)
            {
                dtLista = null;
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            dtLista = miFun.dtLista;
            return;
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            BE_VTA_BOLETARESUMEN e_boleta = new BE_VTA_BOLETARESUMEN();
            CD_vta_boletaresumen miFun = new CD_vta_boletaresumen();
            DataTable dtBol = new DataTable();
            DataTable dtBolDet = new DataTable();

            miFun.mysConec = mysConec;
            miFun.TraerRegistro(n_IdRegistro);
            dtBol = miFun.dtLista;
            dtBolDet = miFun.dtListaDet;
            
            e_Boletas.n_idemp = Convert.ToInt32(dtBol.Rows[0]["n_idemp"]);
            e_Boletas.n_id = Convert.ToInt32(dtBol.Rows[0]["n_id"]);
            e_Boletas.n_ano = Convert.ToInt32(dtBol.Rows[0]["n_ano"]);
            e_Boletas.n_mes = Convert.ToInt32(dtBol.Rows[0]["n_mes"]);
            e_Boletas.d_fchdoc = Convert.ToDateTime(dtBol.Rows[0]["d_fchdoc"]);
            e_Boletas.d_fchven = Convert.ToDateTime(dtBol.Rows[0]["d_fchven"]);
            e_Boletas.n_idtipdoc = Convert.ToInt32(dtBol.Rows[0]["n_idtipdoc"]);
            e_Boletas.n_numdoc = Convert.ToInt32(dtBol.Rows[0]["n_numdoc"]);
            e_Boletas.n_importe =  Convert.ToDouble(dtBol.Rows[0]["n_importe"]);
            e_Boletas.n_numarc = Convert.ToInt32(dtBol.Rows[0]["n_numarc"]);
            
            dtBoletasdet = dtBolDet;
            return;
        }
        public bool Insertar(BE_VTA_BOLETARESUMEN e_boleta, List<BE_VTA_BOLETARESUMENDET> l_boletadet)
        {
            BE_VTA_CHOFER entNuevoChofer = new BE_VTA_CHOFER();
            CD_vta_boletaresumen miFun = new CD_vta_boletaresumen();
            bool booOk = false;

            miFun.mysConec = mysConec;

            booOk = miFun.Insertar(e_boleta, l_boletadet);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_VTA_BOLETARESUMEN e_boleta, List<BE_VTA_BOLETARESUMENDET> l_boletadet)
        {
            BE_VTA_CHOFER entNuevoChofer = new BE_VTA_CHOFER();
            CD_vta_boletaresumen miFun = new CD_vta_boletaresumen();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(e_boleta, l_boletadet);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Eliminar(int n_Id)
        {
            CD_vta_boletaresumen miFun = new CD_vta_boletaresumen();
            bool booOk = false;

            miFun.mysConec = mysConec;

            booOk = miFun.Eliminar(n_Id);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool GenerarBoletaResumen(int n_IdRegistro, string c_FechaVenta, int n_NumeroDocumentos, string c_RutaDestino)
        {
            CD_vta_boletaresumen o_bol = new CD_vta_boletaresumen();
            bool b_result = false;
            DataTable dtresult = new DataTable();
            DataTable dtRow = new DataTable();
            int n_numarch = 1;
            int n_numfil = 0;
            string c_nomarch = "";
            string c_rutenv = c_RutaDestino;
            int n_numpag = 1;
            int n_topepag = 99;
            Helper.Cls_IO funIO = new Helper.Cls_IO();

            o_bol.mysConec = mysConec;
            o_bol.Consulta1(n_IdRegistro);
            dtresult = o_bol.dtLista;
            for (n_numfil = 0; n_numfil <= dtresult.Rows.Count - 1; n_numfil++)
            {
                dtresult.Rows[n_numfil]["n_idpag"] = n_numpag;

                if (n_numfil == n_topepag)
                {
                    n_numpag = n_numpag + 1;
                    n_topepag = n_topepag + 100;
                }
            }

            for (n_numarch = 1; n_numarch <= n_NumeroDocumentos; n_numarch++)
            {
                c_nomarch = STU_SISTEMA.EMPRESARUC + "-RC-" + Convert.ToDateTime(c_FechaVenta).ToString("yyyyMMdd") + "-" + n_numarch.ToString("000")+".RDI";
                dtRow = funDatos.DataTableFiltrar(dtresult, "n_idpag = " + n_numarch.ToString() + "");
                
                if (funIO.Fil_GenerarTxt(dtRow, c_nomarch, c_rutenv, 2) == false)
                {
                    booOcurrioError = true;
                    StrErrorMensaje = funIO.c_err_mensaje;
                    IntErrorNumber = funIO.n_err_numero;
                    booOcurrioError = true;
                    b_result = false;
                    break;
                }
            }
            b_result = true;
            return b_result;
        }
    }
}
