using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades;
using SIAC_DATOS.Cooperativa;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Cooperativa;

namespace SIAC_Negocio.Cooperativa
{
    public class CN_coo_socios
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();

        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFundat = new Helper.Genericas();

        public DataTable Listar(int n_IdEmpresa)
        {
            DataTable dtResul = new DataTable();

            CD_coo_socios miFun = new CD_coo_socios();
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar(n_IdEmpresa);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public BE_COO_SOCIOS TraerRegistro(int n_IdRegistro)
        {
            BE_COO_SOCIOS entSocios = new BE_COO_SOCIOS();
            CD_coo_socios miFun = new CD_coo_socios();
            DataTable DtResultado = new DataTable();

            miFun.mysConec = mysConec;
            DtResultado = miFun.TraerRegistro(n_IdRegistro);

            if (DtResultado.Rows.Count != 0)
            {
                entSocios.n_idemp = Convert.ToInt32(DtResultado.Rows[0]["n_idemp"]);
                entSocios.n_id = Convert.ToInt32(DtResultado.Rows[0]["n_id"]);
                entSocios.n_ideidtipdoc = Convert.ToInt32(DtResultado.Rows[0]["n_ideidtipdoc"]);
                entSocios.c_idenumdoc = DtResultado.Rows[0]["c_idenumdoc"].ToString();
                entSocios.c_ape1 = DtResultado.Rows[0]["c_ape1"].ToString();
                entSocios.c_ape2 = DtResultado.Rows[0]["c_ape2"].ToString();
                entSocios.c_nom1 = DtResultado.Rows[0]["c_nom1"].ToString();
                entSocios.c_nom2 = DtResultado.Rows[0]["c_nom2"].ToString();
                entSocios.c_apenom = DtResultado.Rows[0]["c_apenom"].ToString();
                entSocios.c_dir = DtResultado.Rows[0]["c_dir"].ToString();
                entSocios.n_iddis = Convert.ToInt32(DtResultado.Rows[0]["n_iddis"]);
                if (DtResultado.Rows[0]["d_fchnac"].ToString() == "")
                {
                    entSocios.d_fchnac = null;
                }
                else
                {
                    entSocios.d_fchnac = Convert.ToDateTime(DtResultado.Rows[0]["d_fchnac"]);
                }

                if (DtResultado.Rows[0]["d_fching"].ToString() == "")
                {
                    entSocios.d_fching = null;
                }
                else
                {
                    entSocios.d_fching = Convert.ToDateTime(DtResultado.Rows[0]["d_fching"]);
                }
                
                entSocios.c_numtel = DtResultado.Rows[0]["c_numtel"].ToString();
                entSocios.c_numcel = DtResultado.Rows[0]["c_numcel"].ToString();
                entSocios.c_email = DtResultado.Rows[0]["c_email"].ToString();
                
                if (Convert.ToInt16(xFun.NulosN(DtResultado.Rows[0]["n_idsex"])) == 0)
                {
                    entSocios.n_idsex = 0;
                }
                else
                { 
                    entSocios.n_idsex = Convert.ToInt32(DtResultado.Rows[0]["n_idsex"]);
                }

                if (Convert.ToInt16(xFun.NulosN(DtResultado.Rows[0]["n_idtipsoc"])) == 0)
                {
                    entSocios.n_idtipsoc = 0;
                }
                else 
                {
                    entSocios.n_idtipsoc = Convert.ToInt32(DtResultado.Rows[0]["n_idtipsoc"]);
                }    
            }

            return entSocios;
        }
        public bool Insertar(BE_COO_SOCIOS entSocio)
        {
            bool b_result = false;
            BE_COO_SOCIOS entPuestos = new BE_COO_SOCIOS();
            CD_coo_socios miFun = new CD_coo_socios();

            miFun.mysConec = mysConec;
            b_result = miFun.Insertar(entSocio);
            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return b_result;
        }
        public bool Actualizar(BE_COO_SOCIOS entSocio)
        {
            bool b_result = false;
            BE_COO_SOCIOS entPuestos = new BE_COO_SOCIOS();
            CD_coo_socios miFun = new CD_coo_socios();

            miFun.mysConec = mysConec;
            b_result = miFun.Actualizar(entSocio);
            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return b_result;
        }
        public bool Eliminar(int n_Idregistro)
        {
            bool b_result = false;
            CD_coo_socios miFun = new CD_coo_socios();

            miFun.mysConec = mysConec;
            b_result = miFun.Eliminar(n_Idregistro);
            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return b_result;
        }
        public void ReportePadronSocios(int n_IdEstadoSocio)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[6, 3];

            arrPara[0, 0] = "n_idemp";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = STU_SISTEMA.EMPRESAID.ToString();

            arrPara[1, 0] = "n_idest";
            arrPara[1, 1] = "N";
            arrPara[1, 2] = n_IdEstadoSocio.ToString();

            arrPara[2, 0] = "c_titulo1";
            arrPara[2, 1] = "C";
            if (n_IdEstadoSocio == 1)
            {
                arrPara[2, 2] = "PADRON DE SOCIOS ACTIVOS";
            }
            else
            {
                arrPara[2, 2] = "PADRON DE SOCIOS NO ACTIVOS";
            }

            arrPara[3, 0] = "c_titulo2";
            arrPara[3, 1] = "C";
            arrPara[3, 2] = "";

            arrPara[4, 0] = "c_nomemp";
            arrPara[4, 1] = "C";
            arrPara[4, 2] = STU_SISTEMA.EMPRESANOMBRE;

            arrPara[5, 0] = "c_numruc";
            arrPara[5, 1] = "C";
            arrPara[5, 2] = STU_SISTEMA.EMPRESARUC;

            c_NomArchivo = "RptPadronSocios.rpt";
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "cooperativa\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "COOPERATIVA - PADRON DE SOCIOS";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
    }
}
