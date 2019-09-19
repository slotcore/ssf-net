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
    public class CN_coo_cargos
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();

        public BE_COO_CARGOS entCargos = new BE_COO_CARGOS();
        public List<BE_COO_CARGOSCAB> lstCargosCab = new List<BE_COO_CARGOSCAB>();
        public List<BE_COO_CARGOSDET> lstCargosDet = new List<BE_COO_CARGOSDET>();

        public DataTable dtLista = new DataTable();
        public DataTable dtCargosCab = new DataTable();
        public DataTable dtCargosDet = new DataTable();
        public DataTable Listar(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo)
        {
            DataTable dtResul = new DataTable();

            CD_coo_cargos miFun = new CD_coo_cargos();
            miFun.mysConec = mysConec;

            dtResul = miFun.Listar(n_IdEmpresa, n_AnoTrabajo, n_MesTrabajo);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public void GenerarCargos(int n_IdEmpresa, int n_anotra, int n_mestra, string c_glosa, string c_fchini, string c_fchfin, int n_idtipdoc)
        {
            DataTable dtResul = new DataTable();

            CD_coo_cargos miFun = new CD_coo_cargos();
            miFun.mysConec = mysConec;

            miFun.GenerarCargos(n_IdEmpresa, n_anotra, n_mestra, c_glosa, c_fchini, c_fchfin, n_idtipdoc);
            if (miFun.booOcurrioError == true)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                dtCargosCab = miFun.dtCargosCab;
                dtCargosDet = miFun.dtCargosDet;
            }

            return;
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            int n_row = 0;

            CD_coo_cargos miFun = new CD_coo_cargos();
            DataTable dtCargos = new DataTable();
            DataTable dtCargosCab = new DataTable();
            DataTable dtCargosDet = new DataTable();

            miFun.mysConec = mysConec;
            miFun.TraerRegistro(n_IdRegistro);
            dtCargos = miFun.dtCargos;
            dtCargosCab = miFun.dtCargosCab;
            dtCargosDet = miFun.dtCargosDet;

            if (dtCargos.Rows.Count != 0)
            {
                entCargos.n_idemp = Convert.ToInt16(dtCargos.Rows[0]["n_idemp"].ToString());
                entCargos.n_id = Convert.ToInt32(dtCargos.Rows[0]["n_id"].ToString());
                entCargos.n_anotra = Convert.ToInt16(dtCargos.Rows[0]["n_anotra"].ToString());
                entCargos.n_mestra = Convert.ToInt16(dtCargos.Rows[0]["n_mestra"].ToString());
                entCargos.d_fchemi = Convert.ToDateTime(dtCargos.Rows[0]["d_fchemi"]);
                entCargos.d_fchini = Convert.ToDateTime(dtCargos.Rows[0]["d_fchini"]);
                entCargos.d_fchfin = Convert.ToDateTime(dtCargos.Rows[0]["d_fchfin"]);
                entCargos.n_numsoc = Convert.ToInt16(dtCargos.Rows[0]["n_numsoc"].ToString());
                entCargos.n_impbru = Convert.ToDouble(dtCargos.Rows[0]["n_impbru"].ToString());
                entCargos.n_impigv = Convert.ToDouble(dtCargos.Rows[0]["n_impigv"].ToString());
                entCargos.n_imptot = Convert.ToDouble(dtCargos.Rows[0]["n_imptot"].ToString());
                entCargos.n_idtipdoc = Convert.ToInt16(dtCargos.Rows[0]["n_idtipdoc"].ToString());
            }

            if (dtCargosCab.Rows.Count != 0)
            {
                for (n_row = 0; n_row <= dtCargosCab.Rows.Count - 1; n_row++)
                {
                    BE_COO_CARGOSCAB entCargosCab = new BE_COO_CARGOSCAB();

                    entCargosCab.n_idemp = Convert.ToInt16(dtCargosCab.Rows[n_row]["n_idemp"].ToString());
                    entCargosCab.n_idcar = Convert.ToInt16(dtCargosCab.Rows[n_row]["n_idcar"].ToString());
                    entCargosCab.n_idsoc = Convert.ToInt16(dtCargosCab.Rows[n_row]["n_idsoc"].ToString());
                    entCargosCab.n_idsocpue = Convert.ToInt16(dtCargosCab.Rows[n_row]["n_idsocpue"].ToString());
                    entCargosCab.n_idtipdoc = Convert.ToInt16(dtCargosCab.Rows[n_row]["n_idtipdoc"].ToString());
                    entCargosCab.c_numser = dtCargosCab.Rows[n_row]["c_numser"].ToString();
                    entCargosCab.c_numdoc = dtCargosCab.Rows[n_row]["c_numdoc"].ToString();
                    entCargosCab.d_fchemi = Convert.ToDateTime(dtCargosCab.Rows[n_row]["d_fchemi"].ToString());
                    entCargosCab.d_fchven = Convert.ToDateTime(dtCargosCab.Rows[n_row]["d_fchven"].ToString());
                    entCargosCab.n_impbru = Convert.ToDouble(dtCargosCab.Rows[n_row]["n_impbru"].ToString());
                    entCargosCab.n_impigv = Convert.ToDouble(dtCargosCab.Rows[n_row]["n_impigv"].ToString());
                    entCargosCab.n_imptot = Convert.ToDouble(dtCargosCab.Rows[n_row]["n_imptot"].ToString());
                    entCargosCab.c_glosa = dtCargosCab.Rows[n_row]["c_glosa"].ToString();
                    entCargosCab.n_impsal = Convert.ToDouble(dtCargosCab.Rows[n_row]["n_impsal"].ToString());
                    entCargosCab.n_anotra = Convert.ToInt16(dtCargosCab.Rows[n_row]["n_anotra"].ToString());
                    entCargosCab.n_mestra = Convert.ToInt16(dtCargosCab.Rows[n_row]["n_mestra"].ToString());

                    lstCargosCab.Add(entCargosCab);
                }
            }
            if (dtCargosDet.Rows.Count != 0)
            {
                for (n_row = 0; n_row <= dtCargosDet.Rows.Count - 1; n_row++)
                {
                    BE_COO_CARGOSDET entCargosDet = new BE_COO_CARGOSDET();

                    entCargosDet.n_idemp = Convert.ToInt16(dtCargosDet.Rows[n_row]["n_idemp"].ToString());
                    entCargosDet.n_idcar = Convert.ToInt16(dtCargosDet.Rows[n_row]["n_idcar"].ToString());
                    entCargosDet.n_idsoc = Convert.ToInt16(dtCargosDet.Rows[n_row]["n_idsoc"].ToString());
                    entCargosDet.n_idcon = Convert.ToInt16(dtCargosDet.Rows[n_row]["n_idcon"].ToString());
                    entCargosDet.n_can = Convert.ToDouble(dtCargosDet.Rows[n_row]["n_can"].ToString());
                    entCargosDet.n_impbru = Convert.ToDouble(dtCargosDet.Rows[n_row]["n_impbru"].ToString());
                    entCargosDet.n_impnet = Convert.ToDouble(dtCargosDet.Rows[n_row]["n_impnet"].ToString());
                    entCargosDet.n_imptotbru = Convert.ToDouble(dtCargosDet.Rows[n_row]["n_imptotbru"].ToString());
                    entCargosDet.n_imptotnet = Convert.ToDouble(dtCargosDet.Rows[n_row]["n_imptotnet"].ToString());
                    entCargosDet.n_idpue = Convert.ToInt16(dtCargosDet.Rows[n_row]["n_idpue"].ToString());

                    lstCargosDet.Add(entCargosDet);
                }
            }
            return;
        }
        public bool Insertar(BE_COO_CARGOS entCargos, List<BE_COO_CARGOSCAB> LstCargosCab, List<BE_COO_CARGOSDET> LstCargosDet)
        {
            bool b_result = false;
            CD_coo_cargos miFun = new CD_coo_cargos();

            miFun.mysConec = mysConec;
            b_result = miFun.Insertar(entCargos, LstCargosCab, LstCargosDet);
            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return b_result;
        }
        public bool Actualizar(BE_COO_CARGOS entCargos, List<BE_COO_CARGOSCAB> LstCargosCab, List<BE_COO_CARGOSDET> LstCargosDet)
        {
            bool b_result = false;
            CD_coo_cargos miFun = new CD_coo_cargos();

            miFun.mysConec = mysConec;
            b_result = miFun.Actualizar(entCargos, LstCargosCab, LstCargosDet);
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
            CD_coo_cargos miFun = new CD_coo_cargos();

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
        public void EmitirFacturas(int n_IdRegistro)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[2, 3];

            arrPara[0, 0] = "n_idemp";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = STU_SISTEMA.EMPRESAID.ToString();

            arrPara[1, 0] = "n_id";
            arrPara[1, 1] = "C";
            arrPara[1, 2] = n_IdRegistro.ToString();

            c_NomArchivo = "RptFacturas.rpt";
            //c_Ruta = @"C:\ssf-net\reportes\cooperativa\" + c_NomArchivo;
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "cooperativa\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "CONTROL DE ASOCIADOS - CARGOS";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
        public void Consulta1(int n_IdPuesto)
        {
            CD_coo_cargos miFun = new CD_coo_cargos();
            miFun.mysConec = mysConec;

            miFun.Consulta1(n_IdPuesto);

            if (miFun.booOcurrioError == true)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                dtLista = miFun.dtLista;
            }

            return;
        }
        public void Consulta3(int n_IdSocio)
        {
            CD_coo_cargos miFun = new CD_coo_cargos();
            miFun.mysConec = mysConec;

            miFun.Consulta3(n_IdSocio);

            if (miFun.booOcurrioError == true)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                dtLista = miFun.dtLista;
            }

            return;
        }
        public void Consulta2(string c_CadenaIn)
        {
            CD_coo_cargos miFun = new CD_coo_cargos();
            miFun.mysConec = mysConec;

            miFun.Consulta2(c_CadenaIn);

            if (miFun.booOcurrioError == true)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                dtLista = miFun.dtLista;
            }

            return;
        }
        public void ObtenerMesValido(int n_IdEmpresa, int n_AnoTrabajo, int n_IdTipoDocumento)
        {
            CD_coo_cargos miFun = new CD_coo_cargos();
            miFun.mysConec = mysConec;

            miFun.ObtenerMesValido(n_IdEmpresa, n_AnoTrabajo, n_IdTipoDocumento);

            if (miFun.booOcurrioError == true)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            else
            {
                dtLista = miFun.dtLista;
            }

            return;
        }
        public void ReportDeuda(int n_IdSocio)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[5, 3];

            arrPara[0, 0] = "n_idsoc";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = n_IdSocio.ToString();

            arrPara[1, 0] = "c_nomemp";
            arrPara[1, 1] = "N";
            arrPara[1, 2] = STU_SISTEMA.EMPRESANOMBRE;

            arrPara[2, 0] = "c_numruc";
            arrPara[2, 1] = "N";
            arrPara[2, 2] = STU_SISTEMA.EMPRESARUC;

            arrPara[3, 0] = "c_titulo1";
            arrPara[3, 1] = "C";
            arrPara[3, 2] = "CTA CTE DEL ASOCIADO";

            arrPara[4, 0] = "c_titulo2";
            arrPara[4, 1] = "C";
            arrPara[4, 2] = "DEUDA AL "+ DateTime.Now.ToString("dd/MM/yyyy");

            c_NomArchivo = "RptCtaCteDeuda.rpt";
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "cooperativa\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "COOPERATIVA - CTA. CTE. DEL ASOCIADO";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
        public bool EliminarConcepto(int n_IdCargo, int n_IdCarDet)
        {
            bool b_result = false;
            CD_coo_cargos miFun = new CD_coo_cargos();

            miFun.mysConec = mysConec;
            b_result = miFun.EliminarConcepto(n_IdCargo, n_IdCarDet);
            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return b_result;
        }
        public void VerReporteDeudaSocios()
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[5, 3];

            arrPara[0, 0] = "n_idemp";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = STU_SISTEMA.EMPRESAID.ToString();

            arrPara[1, 0] = "c_nomemp";
            arrPara[1, 1] = "C";
            arrPara[1, 2] = STU_SISTEMA.EMPRESANOMBRE;

            arrPara[2, 0] = "c_numruc";
            arrPara[2, 1] = "C";
            arrPara[2, 2] = STU_SISTEMA.EMPRESARUC;
            
            arrPara[3, 0] = "c_titulo1";
            arrPara[3, 1] = "C";
            arrPara[3, 2] = "DEUDA DE SOCIOS";

            arrPara[4, 0] = "c_titulo2";
            arrPara[4, 1] = "C";
            arrPara[4, 2] = "AL " + DateTime.Now.ToString("dd/MM/yyyy");

            
            c_NomArchivo = "RptSociosDeuda.rpt";
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "cooperativa\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "COOPERATIVA - DEUDA DE SOCIOS";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
    }
}
