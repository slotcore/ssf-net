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
    public class CN_coo_servicios
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();

        public BE_COO_SERVICIOS entServicios = new BE_COO_SERVICIOS();
        public List<BE_COO_SERVICIOSDET> lstServicios = new List<BE_COO_SERVICIOSDET>();

        public DataTable dtListar = new DataTable();

        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFundat = new Helper.Genericas();
        public void Listar(int n_IdEmpresa)
        {
            DataTable dtResul = new DataTable();

            CD_coo_servicios miFun = new CD_coo_servicios();
            miFun.mysConec = mysConec;

            miFun.Listar(n_IdEmpresa);
            dtListar = miFun.dtListar;
            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return;
        }
        public void Consulta2(int n_IdEmpresa, int n_IdTipoServicio)
        {
            DataTable dtResul = new DataTable();

            CD_coo_servicios miFun = new CD_coo_servicios();
            miFun.mysConec = mysConec;

            miFun.Consulta2(n_IdEmpresa, n_IdTipoServicio);
            dtListar = miFun.dtListar;
            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return;
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            CD_coo_servicios miFun = new CD_coo_servicios();
            DataTable DtResul = new DataTable();
            int n_row = 0;

            miFun.mysConec = mysConec;
            miFun.TraerRegistro(n_IdRegistro);
            
            if (miFun.booOcurrioError == false)
            {
                DtResul = miFun.dtServicios;
                if (DtResul.Rows.Count != 0)
                {
                    entServicios.n_id = Convert.ToInt32(DtResul.Rows[0]["n_id"]);
                    entServicios.n_idemp = Convert.ToInt32(DtResul.Rows[0]["n_idemp"]);
                    entServicios.n_anotra = Convert.ToInt32(DtResul.Rows[0]["n_anotra"]);
                    entServicios.n_mestra = Convert.ToInt32(DtResul.Rows[0]["n_mestra"]);
                    entServicios.d_fchini = Convert.ToDateTime(DtResul.Rows[0]["d_fchini"]);
                    entServicios.d_fchfin = Convert.ToDateTime(DtResul.Rows[0]["d_fchfin"]);
                    entServicios.n_imptot = Convert.ToDouble(DtResul.Rows[0]["n_imptot"]);
                    entServicios.c_obs = DtResul.Rows[0]["c_obs"].ToString();
                    entServicios.n_idtipser = Convert.ToInt32(DtResul.Rows[0]["n_idtipser"]);
                }

                DtResul = miFun.dtServiciosDet;
                lstServicios.Clear();

                for(n_row=0; n_row <= DtResul.Rows.Count-1; n_row++)
                {
                    BE_COO_SERVICIOSDET entServDet = new BE_COO_SERVICIOSDET();

                    entServDet.n_idser = Convert.ToInt32(DtResul.Rows[n_row]["n_idser"]);
                    entServDet.n_idpue = Convert.ToInt32(DtResul.Rows[n_row]["n_idpue"]);
                    entServDet.c_numlecini = DtResul.Rows[n_row]["c_numlecini"].ToString();
                    entServDet.c_numlecfin = DtResul.Rows[n_row]["c_numlecfin"].ToString();
                    entServDet.n_impcon = Convert.ToDouble(DtResul.Rows[n_row]["n_impcon"]);
                    entServDet.c_obs = DtResul.Rows[n_row]["c_obs"].ToString();

                    lstServicios.Add(entServDet);
                }
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return;
        }
        public bool Insertar(BE_COO_SERVICIOS entServicios, List<BE_COO_SERVICIOSDET> LstServicioscab)
        {
            bool b_result = false;
            CD_coo_servicios miFun = new CD_coo_servicios();

            miFun.mysConec = mysConec;
            b_result = miFun.Insertar(entServicios, LstServicioscab);
            if (b_result == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
                return b_result;
            }
            b_result = true;
            return b_result;
        }
        public bool Actualizar(BE_COO_SERVICIOS entServicios, List<BE_COO_SERVICIOSDET> LstServicioscab)
        {
            bool b_result = false;
            CD_coo_servicios miFun = new CD_coo_servicios();

            miFun.mysConec = mysConec;
            b_result = miFun.Actualizar(entServicios, LstServicioscab);
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
            CD_coo_servicios miFun = new CD_coo_servicios();

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
        public void ImprimirCargosServicios(int n_Idregistro)
        {
                string c_NomArchivo = "";
                string c_Ruta = "";
                string[,] arrPara = new string[6, 3];

                arrPara[0, 0] = "n_id";
                arrPara[0, 1] = "N";
                arrPara[0, 2] = n_Idregistro.ToString();

                arrPara[1, 0] = "n_idemp";
                arrPara[1, 1] = "N";
                arrPara[1, 2] = STU_SISTEMA.EMPRESAID.ToString();

                arrPara[2, 0] = "c_nomemp";
                arrPara[2, 1] = "C";
                arrPara[2, 2] = STU_SISTEMA.EMPRESANOMBRE;

                arrPara[3, 0] = "c_numruc";
                arrPara[3, 1] = "C";
                arrPara[3, 2] = STU_SISTEMA.EMPRESARUC;

                arrPara[4, 0] = "c_titulo1";
                arrPara[4, 1] = "C";
                arrPara[4, 2] = "CARGOS POR SERVICIOS";

                arrPara[5, 0] = "c_titulo2";
                arrPara[5, 1] = "C";
                arrPara[5, 2] = "POR TIPO SERVICIO";

                c_NomArchivo = "RptServiciosCargos.rpt";
                //c_Ruta = @"j:\ssf-net\reportes\cooperativa\" + c_NomArchivo;
                c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "cooperativa\\" + c_NomArchivo;

                Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
                xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
                xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
                xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
                xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
                xVisor.b_VisPrev = true;
                xVisor.c_Titulo = "COOPERATIVA - CARGOS POR SERVICIOS";
                xVisor.c_PathRep = c_Ruta;
                xVisor.arrParametros = arrPara;
                xVisor.VerCrystal();
        }
    }
}
