using MySql.Data.MySqlClient;
using SIAC_DATOS.Tesoreria;
using SIAC_DATOS.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using SIAC_Entidades.Tesoreria;
using SIAC_Entidades.Contabilidad;

namespace SIAC_Negocio.Tesoreria
{
    public class CN_tes_letras
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public BE_TES_LETRAS e_Letras = new BE_TES_LETRAS();
        public List<BE_TES_LETRASDET> l_LetDet = new List<BE_TES_LETRASDET>();
        public List<BE_TES_LETRASDOC> l_LetDoc = new List<BE_TES_LETRASDOC>();
        public DataTable dtListaDoc = new DataTable();
        public DataTable dtLista = new DataTable();

        public List<BE_CON_DIARIO> l_diario = new List<BE_CON_DIARIO>();

        public void Listar(int n_IdEmpresa, int n_AnoTrabajo, int n_mesTrabajo)
        {
            CD_tes_letras miFun = new CD_tes_letras();

            miFun.mysConec = mysConec;
            miFun.Listar(n_IdEmpresa, n_AnoTrabajo, n_mesTrabajo);
            dtLista = miFun.dtLista1;

            if (miFun.b_OcurrioError == true)
            {
                dtLista = null;
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return;
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            DataTable dtResult = new DataTable();
            CD_tes_letras miFun = new CD_tes_letras();
            miFun.mysConec = mysConec;
            int n_row = 0;

            if (miFun.TraerRegistro(n_IdRegistro) == true)
            {
                dtListaDoc = miFun.dtLista4;
                dtResult = miFun.dtLista1;

                e_Letras.n_idemp = Convert.ToInt32(dtResult.Rows[0]["n_idemp"]);
                e_Letras.n_ano = Convert.ToInt32(dtResult.Rows[0]["n_ano"]);
                e_Letras.n_mes = Convert.ToInt32(dtResult.Rows[0]["n_mes"]);
                e_Letras.n_idlib = Convert.ToInt32(dtResult.Rows[0]["n_idlib"]);
                e_Letras.n_id = Convert.ToInt32(dtResult.Rows[0]["n_id"]);
                e_Letras.c_numreg = dtResult.Rows[0]["c_numreg"].ToString();
                e_Letras.n_tiplet = Convert.ToInt32(dtResult.Rows[0]["n_tiplet"]);
                e_Letras.d_fchreg = Convert.ToDateTime(dtResult.Rows[0]["d_fchreg"]);
                e_Letras.d_fchini = Convert.ToDateTime(dtResult.Rows[0]["d_fchini"]);
                e_Letras.n_idclipro = Convert.ToInt32(dtResult.Rows[0]["n_idclipro"]);
                e_Letras.n_idmon = Convert.ToInt32(dtResult.Rows[0]["n_idmon"]);
                e_Letras.n_numlet = Convert.ToInt32(dtResult.Rows[0]["n_numlet"]);
                e_Letras.n_impcap = Convert.ToDouble(dtResult.Rows[0]["n_impcap"]);
                e_Letras.n_idtipint = Convert.ToInt32(dtResult.Rows[0]["n_idtipint"]);
                e_Letras.n_portas = Convert.ToDouble(dtResult.Rows[0]["n_portas"]);
                e_Letras.c_glosa = dtResult.Rows[0]["c_glosa"].ToString();
                e_Letras.n_numdiaint = Convert.ToInt32(dtResult.Rows[0]["n_numdiaint"]);
                e_Letras.n_numdiapla = Convert.ToInt32(dtResult.Rows[0]["n_numdiapla"]);
                e_Letras.n_tc = Convert.ToDouble(dtResult.Rows[0]["n_tc"]);
                e_Letras.c_girnumdoc = dtResult.Rows[0]["c_girnumdoc"].ToString();
                e_Letras.c_girnom = dtResult.Rows[0]["c_girnom"].ToString();
                e_Letras.c_girtel = dtResult.Rows[0]["c_girtel"].ToString();
                e_Letras.c_girdir = dtResult.Rows[0]["c_girdir"].ToString();
                e_Letras.n_imppor = Convert.ToDouble(dtResult.Rows[0]["n_imppor"]);
                e_Letras.n_idban = Convert.ToInt32(dtResult.Rows[0]["n_idban"]);
                e_Letras.n_idcueban = Convert.ToInt32(dtResult.Rows[0]["n_idcueban"]);

                dtResult = miFun.dtLista2;

                for (n_row = 0; n_row <= dtResult.Rows.Count - 1; n_row++)
                {
                    BE_TES_LETRASDET e_det = new BE_TES_LETRASDET();

                    e_det.n_idlet = Convert.ToInt32(dtResult.Rows[n_row]["n_idlet"]);
                    //e_det.n_id = Convert.ToInt32(dtResult.Rows[n_row]["n_id"]);
                    e_det.c_numser = dtResult.Rows[n_row]["c_numser"].ToString();
                    e_det.c_numlet = dtResult.Rows[n_row]["c_numlet"].ToString();
                    e_det.d_fchemi = Convert.ToDateTime(dtResult.Rows[n_row]["d_fchemi"]);
                    e_det.d_fchven = Convert.ToDateTime(dtResult.Rows[n_row]["d_fchven"]);
                    e_det.n_impcap = Convert.ToDouble(dtResult.Rows[n_row]["n_impcap"]);
                    e_det.n_imppor = Convert.ToDouble(dtResult.Rows[n_row]["n_imppor"]);
                    e_det.n_impint = Convert.ToDouble(dtResult.Rows[n_row]["n_impint"]);
                    e_det.n_implet = Convert.ToDouble(dtResult.Rows[n_row]["n_implet"]);
                    e_det.n_diapla = Convert.ToInt32(dtResult.Rows[n_row]["n_diapla"]);
                    e_det.n_impsal = Convert.ToDouble(dtResult.Rows[n_row]["n_impsal"]);
                    e_det.c_imptex = dtResult.Rows[n_row]["c_imptex"].ToString();

                    l_LetDet.Add(e_det);
                }


                dtResult = miFun.dtLista3;

                for (n_row = 0; n_row <= dtResult.Rows.Count - 1; n_row++)
                {
                    BE_TES_LETRASDOC e_doc = new BE_TES_LETRASDOC();

                    e_doc.n_idlet = Convert.ToInt32(dtResult.Rows[n_row]["n_idlet"]);
                    e_doc.n_iddoc = Convert.ToInt32(dtResult.Rows[n_row]["n_iddoc"]);
                    e_doc.n_impfin = Convert.ToDouble(dtResult.Rows[n_row]["n_impfin"]);

                    l_LetDoc.Add(e_doc);
                }
            }
            else
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool b_Result = false;
            CD_tes_letras miFun = new CD_tes_letras();

            miFun.mysConec = mysConec;
            if (miFun.Eliminar(n_IdRegistro) == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_Result;
            }
            b_Result = true;
            return b_Result;
        }
        public bool Insertar(BE_TES_LETRAS e_Letras, List<BE_TES_LETRASDET> l_LetrasDetalle, List<BE_TES_LETRASDOC> l_LetraDocumentos)
        {
            CD_tes_letras miFun = new CD_tes_letras();
            bool b_Result = false;

            miFun.mysConec = mysConec;
            miFun.l_diario = l_diario;
            if (miFun.Insertar(e_Letras, l_LetrasDetalle, l_LetraDocumentos) == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_Result;
            }
            b_Result = true;
            return b_Result;
        }
        public bool Actualizar(BE_TES_LETRAS e_Letras, List<BE_TES_LETRASDET> l_LetrasDetalle, List<BE_TES_LETRASDOC> l_LetraDocumentos)
        {
            CD_tes_letras miFun = new CD_tes_letras();
            bool b_Result = false;

            miFun.mysConec = mysConec;
            if (miFun.Actualizar(e_Letras, l_LetrasDetalle, l_LetraDocumentos) == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_Result;
            }
            b_Result = true;
            return b_Result;
        }
        public void ReportImprimirLetras(int n_idMovimiento)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[1, 3];

            arrPara[0, 0] = "n_idlet";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = n_idMovimiento.ToString();

            c_NomArchivo = "Rpt_Letras.rpt";
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "tesoreria\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "TESORERIA - IMPRESION DE LETRAS";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
    }
}
