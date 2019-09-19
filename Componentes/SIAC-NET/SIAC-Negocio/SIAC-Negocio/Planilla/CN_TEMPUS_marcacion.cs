using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades.Maestros;
using SIAC_DATOS.Planilla;
using MySql.Data.MySqlClient;

namespace SIAC_Negocio.Planilla
{
    public class CN_TEMPUS_marcacion
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public DataTable ListarIngresos(int n_AnoTrabajo, int n_MesTrabajo, string c_CodEmpresa)
        {
            DataTable dtResul = new DataTable();

            CD_TEMPUS_marcacion miFun = new CD_TEMPUS_marcacion();
            miFun.mysConec = mysConec;

            dtResul = miFun.ListarIngresos(n_AnoTrabajo, n_MesTrabajo, c_CodEmpresa);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return dtResul;
        }
        public DataTable ListarSalidas(int n_AnoTrabajo, int n_MesTrabajo, string c_CodEmpresa)
        {
            DataTable dtResul = new DataTable();

            CD_TEMPUS_marcacion miFun = new CD_TEMPUS_marcacion();
            miFun.mysConec = mysConec;

            dtResul = miFun.ListarSalidas(n_AnoTrabajo, n_MesTrabajo, c_CodEmpresa);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return dtResul;
        }
        public DataTable ListarPersonal(string c_CodEmpresa)
        {
            DataTable dtResul = new DataTable();

            CD_TEMPUS_marcacion miFun = new CD_TEMPUS_marcacion();
            miFun.mysConec = mysConec;

            dtResul = miFun.ListarPersonal(c_CodEmpresa);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return dtResul;
        }
        public DataTable AnosTrabajo()
        {
            DataTable dtResul = new DataTable();

            CD_TEMPUS_marcacion miFun = new CD_TEMPUS_marcacion();
            miFun.mysConec = mysConec;

            dtResul = miFun.AnosTrabajo();

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return dtResul;
        }
        public DataTable Empresas()
        {
            DataTable dtResul = new DataTable();

            CD_TEMPUS_marcacion miFun = new CD_TEMPUS_marcacion();
            miFun.mysConec = mysConec;

            dtResul = miFun.Empresas();

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return dtResul;
        }
        public void RptAsistencia(int n_opcion, int n_anotra, int n_mestra, string c_codempresa)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[7, 3];

            arrPara[0, 0] = "n_anotra";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = n_anotra.ToString();

            arrPara[1, 0] = "n_mestra";
            arrPara[1, 1] = "N";
            arrPara[1, 2] = n_mestra.ToString();

            arrPara[2, 0] = "c_nomemp";
            arrPara[2, 1] = "C";
            arrPara[2, 2] = STU_SISTEMA.EMPRESANOMBRE;

            arrPara[3, 0] = "c_numruc";
            arrPara[3, 1] = "C";
            arrPara[3, 2] = STU_SISTEMA.EMPRESARUC;

            arrPara[4, 0] = "c_titulo1";
            arrPara[4, 1] = "C";
            arrPara[4, 2] = "ASISTENCIA DEL PERSONAL";

            arrPara[5, 0] = "c_titulo2";
            arrPara[5, 1] = "C";
            arrPara[5, 2] = "POR EMPRESAS";

            arrPara[6, 0] = "c_idemp";
            arrPara[6, 1] = "C";
            arrPara[6, 2] = c_codempresa.ToString();

            if (n_opcion == 1) {c_NomArchivo = "RptAsistencia.rpt";}
            if (n_opcion == 2) {c_NomArchivo = "RptAsistencia-2.rpt";}
            if (n_opcion == 3) {c_NomArchivo = "RptAsistencia-3.rpt";}
            //c_Ruta = @"j:\ssf-net\reportes\planillas\" + c_NomArchivo;
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "planillas\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();

            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "PLANILLAS - REPORTE DE ASISTENCIA";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.VerCrystal();
        }
        public void RptCumpleaños(string c_IdEmpresa, int n_IdMes)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[6, 3];

            if (c_IdEmpresa == null)
            {
                c_IdEmpresa = "";
            }
            arrPara[0, 0] = "c_idemp";
            arrPara[0, 1] = "C";
            arrPara[0, 2] = c_IdEmpresa.ToString();

            arrPara[1, 0] = "n_idmes";
            arrPara[1, 1] = "N";
            arrPara[1, 2] = n_IdMes.ToString();

            arrPara[2, 0] = "c_nomemp";
            arrPara[2, 1] = "C";
            arrPara[2, 2] = STU_SISTEMA.EMPRESANOMBRE;

            arrPara[3, 0] = "c_numruc";
            arrPara[3, 1] = "C";
            arrPara[3, 2] = STU_SISTEMA.EMPRESARUC;

            arrPara[4, 0] = "c_titulo1";
            arrPara[4, 1] = "C";
            arrPara[4, 2] = "ASISTENCIA DEL PERSONAL";

            arrPara[5, 0] = "c_titulo2";
            arrPara[5, 1] = "C";
            arrPara[5, 2] = "POR EMPRESAS";


            c_NomArchivo = "RptCumpleanos.rpt";
            //c_Ruta = STU_SISTEMA.RUTAREPORTES +"planillas\\" + c_NomArchivo;
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "planillas\\" + c_NomArchivo;
            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();

            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "PLANILLAS - REPORTE DE CUMPLEAÑOS";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
        public void RptAsistenciaPersona(int n_anotra, int n_mesini, int n_mesfin, string c_codempresa, string c_codempleado)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[9, 3];

            arrPara[0, 0] = "n_anotra";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = n_anotra.ToString();

            arrPara[1, 0] = "n_mesini";
            arrPara[1, 1] = "N";
            arrPara[1, 2] = n_mesini.ToString();

            arrPara[2, 0] = "n_mesfin";
            arrPara[2, 1] = "N";
            arrPara[2, 2] = n_mesfin.ToString();

            arrPara[3, 0] = "c_idemp";
            arrPara[3, 1] = "C";
            arrPara[3, 2] = c_codempresa.ToString();

            arrPara[4, 0] = "c_codemp";
            arrPara[4, 1] = "C";
            arrPara[4, 2] = c_codempleado;

            arrPara[5, 0] = "c_numruc";
            arrPara[5, 1] = "C";
            arrPara[5, 2] = STU_SISTEMA.EMPRESARUC;

            arrPara[6, 0] = "c_nomemp";
            arrPara[6, 1] = "C";
            arrPara[6, 2] = STU_SISTEMA.EMPRESANOMBRE;

            arrPara[7, 0] = "c_titulo1";
            arrPara[7, 1] = "C";
            arrPara[7, 2] = "ASISTENCIA DEL PERSONAL";

            arrPara[8, 0] = "c_titulo2";
            arrPara[8, 1] = "C";
            arrPara[8, 2] = "POR EMPLEADO";           

            c_NomArchivo = "RptAsistenciaPersona.rpt"; 
            //c_Ruta = @"j:\ssf-net\reportes\planillas\" + c_NomArchivo;
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "planillas\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();

            xVisor.b_VisPrev = true;
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "PLANILLAS - REPORTE DE ASISTENCIA X PERSONA";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
         }
    }
}
