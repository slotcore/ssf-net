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
    public class CN_coo_sociospuestos
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public List<BE_COO_SOCIOSPUESTOS> lstSociosPuestos = new List<BE_COO_SOCIOSPUESTOS>();

        public DataTable dtPuestos = new DataTable();
        public DataTable dtPuestosSocios = new DataTable();

        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFundat = new Helper.Genericas();
        public DataTable Listar(int n_IdEmpresa)
        {
            DataTable dtResul = new DataTable();

            CD_coo_sociospuestos miFun = new CD_coo_sociospuestos();
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
        public void Consulta1(int n_IdEmpresa, int n_IdEstado)
        {
            CD_coo_sociospuestos miFun = new CD_coo_sociospuestos();
            miFun.mysConec = mysConec;

            miFun.Consulta1(n_IdEmpresa, n_IdEstado);
            dtPuestosSocios = miFun.dtSociosPuestos;
            if (miFun.booOcurrioError == true)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return;
        }
        public void Consulta2(int n_IdEmpresa)
        {
            CD_coo_sociospuestos miFun = new CD_coo_sociospuestos();
            miFun.mysConec = mysConec;

            miFun.Consulta2(n_IdEmpresa);
            dtPuestosSocios = miFun.dtSociosPuestos;
            if (miFun.booOcurrioError == true)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return ;
        }
        public void TraerRegistro(int n_IdSocio)
        {
            CD_coo_sociospuestos miFunPue = new CD_coo_sociospuestos();
            DataTable DtResultado = new DataTable();
            int n_row = 0;

            miFunPue.mysConec = mysConec;
            miFunPue.TraerRegistro(n_IdSocio);
            lstSociosPuestos.Clear();
            if (miFunPue.booOcurrioError == false)
            {
                DtResultado = miFunPue.dtSociosPuestos;

                if (DtResultado.Rows.Count != 0)
                {
                    for (n_row = 0; n_row <= DtResultado.Rows.Count - 1; n_row++)
                    {
                        BE_COO_SOCIOSPUESTOS entSociosPue = new BE_COO_SOCIOSPUESTOS();

                        entSociosPue.n_idemp = Convert.ToInt32(DtResultado.Rows[n_row]["n_idemp"]);
                        entSociosPue.n_id = Convert.ToInt32(DtResultado.Rows[n_row]["n_id"]);
                        entSociosPue.n_idsoc = Convert.ToInt32(DtResultado.Rows[n_row]["n_idsoc"]);
                        entSociosPue.n_idpue = Convert.ToInt32(DtResultado.Rows[n_row]["n_idpue"]);
                        entSociosPue.c_puesto = DtResultado.Rows[n_row]["c_puesto"].ToString();
                        entSociosPue.c_numctt = DtResultado.Rows[n_row]["c_numctt"].ToString();
                        if (DtResultado.Rows[n_row]["d_fchini"].ToString() != "")
                        {
                            entSociosPue.d_fchini = Convert.ToDateTime(DtResultado.Rows[n_row]["d_fchini"]);
                        }
                        else
                        {
                            entSociosPue.d_fchini = null;
                        }
                        if (DtResultado.Rows[n_row]["d_fchfin"].ToString() != "")
                        {
                            entSociosPue.d_fchfin = Convert.ToDateTime(DtResultado.Rows[n_row]["d_fchfin"]);
                        }
                        else
                        {
                            entSociosPue.d_fchfin = null;
                        }
                        entSociosPue.n_activo = Convert.ToInt32(DtResultado.Rows[n_row]["n_activo"]);
                        entSociosPue.n_idtipdocemi = Convert.ToInt32(DtResultado.Rows[n_row]["n_idtipdocemi"]);
                        if (DtResultado.Rows[n_row]["d_fchter"].ToString() != "")
                        {
                            entSociosPue.d_fchter = Convert.ToDateTime(DtResultado.Rows[n_row]["d_fchter"]);
                        }
                        else
                        {
                            entSociosPue.d_fchter = null;
                        }
                        lstSociosPuestos.Add(entSociosPue);
                    }
                }
            }
            else
            {
                booOcurrioError = miFunPue.booOcurrioError;
                StrErrorMensaje = miFunPue.StrErrorMensaje;
                IntErrorNumber = miFunPue.IntErrorNumber;
            }
            return;
        }
        public void TraerPuestosHabiles(int n_IdEmpresa)
        {
            CD_coo_sociospuestos miFunPue = new CD_coo_sociospuestos();

            miFunPue.mysConec = mysConec;
            miFunPue.TraerPuestosHabiles(n_IdEmpresa);
            dtPuestos = miFunPue.dtSociosPuestos;

            if (miFunPue.booOcurrioError == true)
            {
                booOcurrioError = miFunPue.booOcurrioError;
                StrErrorMensaje = miFunPue.StrErrorMensaje;
                IntErrorNumber = miFunPue.IntErrorNumber;
            }
            return;
        }
        public bool Insertar(List<BE_COO_SOCIOSPUESTOS> LstSocPue, int n_IdSocio)
        {
            bool b_result = false;
            bool b_seencontro = false;
            int n_row = 0;
            int n_row2 = 0;
            DataTable dtResult = new DataTable();
            CD_coo_sociospuestos miFun = new CD_coo_sociospuestos();

            miFun.mysConec = mysConec;
            //miFun.TraerRegistro(LstSocPue[0].n_idsoc);
            miFun.TraerRegistro(n_IdSocio);
            dtResult = miFun.dtSociosPuestos;
                        
            // RECORREMOS LOS DATOS ANTERIOS PARA VER QUE REGISTRO HA SIDO ELIMINADOS
            for (n_row = 0; n_row <= dtResult.Rows.Count - 1; n_row++)
            {
                b_seencontro = false;
                for (n_row2 = 0; n_row2 <= LstSocPue.Count - 1; n_row2++)
                { 
                    if (Convert.ToInt32(dtResult.Rows[n_row]["n_id"]) == LstSocPue[n_row2].n_id)
                    {
                        b_seencontro = true;
                        break;
                    }
                }

                // SI NO ENCONTROEL REGISTRO LO ELIMINA
                if (b_seencontro == false)
                {
                    if (miFun.Eliminar(Convert.ToInt32(dtResult.Rows[n_row]["n_id"])) == false)
                    {
                        booOcurrioError = miFun.booOcurrioError;
                        StrErrorMensaje = miFun.StrErrorMensaje;
                        IntErrorNumber = miFun.IntErrorNumber;
                        return b_result;
                    }
                }
            }

            if (LstSocPue.Count != 0)
            { 
                for (n_row = 0; n_row <= LstSocPue.Count - 1; n_row++)
                {
                    BE_COO_SOCIOSPUESTOS entSocPue = new BE_COO_SOCIOSPUESTOS();

                    entSocPue = LstSocPue[n_row];

                    if (LstSocPue[n_row].n_id == 0)
                    {
                        b_result = miFun.Insertar(entSocPue);
                    }
                    else 
                    {
                        b_result = miFun.Actualizar(entSocPue);
                    }

                    if (b_result == false)
                    {
                        booOcurrioError = miFun.booOcurrioError;
                        StrErrorMensaje = miFun.StrErrorMensaje;
                        IntErrorNumber = miFun.IntErrorNumber;
                        return b_result;
                    }
                }
            }
            else
            {
                b_result = true;
            }
            return b_result;
        }
        public void ReporteSociosPuestos(int n_IdEmpresa)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[5, 3];

            arrPara[0, 0] = "n_idemp";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = STU_SISTEMA.EMPRESAID.ToString();

            arrPara[1, 0] = "c_titulo1";
            arrPara[1, 1] = "C";
            arrPara[1, 2] = "REPORTE DE PUESTOS ACTIVOS";

            arrPara[2, 0] = "c_titulo2";
            arrPara[2, 1] = "C";
            arrPara[2, 2] = "";

            arrPara[3, 0] = "c_nomemp";
            arrPara[3, 1] = "C";
            arrPara[3, 2] = STU_SISTEMA.EMPRESANOMBRE;

            arrPara[4, 0] = "c_numruc";
            arrPara[4, 1] = "C";
            arrPara[4, 2] = STU_SISTEMA.EMPRESARUC;

            c_NomArchivo = "RptPuestos.rpt";
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "cooperativa\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "COOPERATIVA - REPORTE DE PUESTOS ACTIVOS";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
    }
}
