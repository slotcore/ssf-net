using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades;
using SIAC_DATOS.Contabilidad;
using SIAC_DATOS.Tesoreria;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Contabilidad;
using SIAC_Entidades.Tesoreria;
using Helper;

namespace SIAC_Negocio.Contabilidad
{
    public class CN_con_regdetracciones
    {
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtLista = new DataTable();
        public BE_CON_REGDETRACCIONES e_Detrac = new BE_CON_REGDETRACCIONES();

        public BE_TES_TESORERIA e_Tesoreria = new BE_TES_TESORERIA();
        public List<BE_TES_TESORERIAORI> l_TesOri = new List<BE_TES_TESORERIAORI>();
        public List<BE_TES_TESORERIADES> l_TesDes = new List<BE_TES_TESORERIADES>();
        public List<BE_TES_TESORERIADESDET> l_TesDesDet = new List<BE_TES_TESORERIADESDET>();
        public List<BE_TES_TESORERIAORIDET> l_TesOriDet = new List<BE_TES_TESORERIAORIDET>();

        DatosMySql FunMysql = new DatosMySql();
        Helper.Comunes.Funciones funfunciones = new Helper.Comunes.Funciones();
        public void Listar(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo, int n_TipoMovimiento)
        {
            CD_con_regdetracciones miFun = new CD_con_regdetracciones();
            miFun.mysConec = mysConec;

            miFun.Listar(n_IdEmpresa, n_AnoTrabajo, n_MesTrabajo, n_TipoMovimiento);
            dtLista = miFun.dtLista;
            if (dtLista == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return;
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            CD_con_regdetracciones miFun = new CD_con_regdetracciones();
            miFun.mysConec = mysConec;

            miFun.TraerRegistro(n_IdRegistro);
            dtLista = miFun.dtLista;
            if (dtLista == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            else
            {
                e_Detrac.n_id= Convert.ToInt32(dtLista.Rows[0]["n_id"]);
                e_Detrac.n_idemp = Convert.ToInt32(dtLista.Rows[0]["n_idemp"]);
                e_Detrac.n_iddet = Convert.ToInt32(dtLista.Rows[0]["n_iddet"]);
                e_Detrac.n_por = Convert.ToDouble(dtLista.Rows[0]["n_por"]);
                e_Detrac.n_iddoccom = Convert.ToInt32(dtLista.Rows[0]["n_iddoccom"]);
                e_Detrac.n_idmon = Convert.ToInt32(dtLista.Rows[0]["n_idmon"]);
                e_Detrac.n_tipmov = Convert.ToInt32(dtLista.Rows[0]["n_tipmov"]);
                e_Detrac.d_fchmov = Convert.ToDateTime(dtLista.Rows[0]["d_fchmov"]);
                e_Detrac.c_glosa = dtLista.Rows[0]["c_glosa"].ToString();
                e_Detrac.n_imp = Convert.ToDouble(dtLista.Rows[0]["n_imp"]);
                e_Detrac.c_numdet = dtLista.Rows[0]["c_numdet"].ToString();
                e_Detrac.d_fchpag = Convert.ToDateTime(dtLista.Rows[0]["d_fchpag"]);
                e_Detrac.n_idgrudet = Convert.ToInt32(dtLista.Rows[0]["n_idgrudet"]);
                e_Detrac.n_ano = Convert.ToInt32(dtLista.Rows[0]["n_ano"]);
                e_Detrac.n_mes = Convert.ToInt32(dtLista.Rows[0]["n_mes"]);
                e_Detrac.c_numreg = Convert.ToString(dtLista.Rows[0]["c_numreg"]);
                e_Detrac.n_idlib = Convert.ToInt32(dtLista.Rows[0]["n_idlib"]);
                e_Detrac.n_tc = Convert.ToDouble(funfunciones.NulosN(dtLista.Rows[0]["n_tc"]));
                e_Detrac.n_idtipdoc = Convert.ToInt32(funfunciones.NulosN(dtLista.Rows[0]["n_idtipdoc"]));
                e_Detrac.n_idtes = Convert.ToInt32(funfunciones.NulosN(dtLista.Rows[0]["n_idtes"]));
                e_Detrac.n_idtesori = Convert.ToInt32(funfunciones.NulosN(dtLista.Rows[0]["n_idtesori"]));
                e_Detrac.n_idmedpag = Convert.ToInt32(funfunciones.NulosN(dtLista.Rows[0]["n_idmedpag"]));
                e_Detrac.n_tippag = Convert.ToInt32(funfunciones.NulosN(dtLista.Rows[0]["n_tippag"]));
                e_Detrac.n_aplipag = Convert.ToInt32(funfunciones.NulosN(dtLista.Rows[0]["n_aplipag"]));
                e_Detrac.n_iddes = Convert.ToInt32(funfunciones.NulosN(dtLista.Rows[0]["n_iddes"]));
            }
            return;
        }
        public bool Eliminar(int n_Idregistro, int n_IdTesoreria)
        {
            bool b_result = false;
            CD_con_regdetracciones miFun = new CD_con_regdetracciones();
            CD_tes_tesoreria funTes = new CD_tes_tesoreria();

            miFun.mysConec = mysConec;
            b_result = miFun.Eliminar(n_Idregistro, n_IdTesoreria);
            if (b_result == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return b_result;
        }
        public bool Insertar(BE_CON_REGDETRACCIONES e_Detracciones)
        {
            bool b_result = false;
            CD_con_regdetracciones miFun = new CD_con_regdetracciones();

            miFun.mysConec = mysConec;
            miFun.e_Tesoreria = e_Tesoreria;
            miFun.l_TesOri = l_TesOri;
            miFun.l_TesOriDet = l_TesOriDet;
            miFun.l_TesDes = l_TesDes;
            miFun.l_TesDesDet = l_TesDesDet;

            b_result = miFun.Insertar(e_Detracciones);
            if (b_result == true)
            {
                if (e_Detracciones.n_aplipag == 1)
                { 
                    int n_idtes = 0;
                    string c_numasi;
                    int n_TipoRegistro = 0;
                    //GENERAMOS EL ASIENTO CONTABLE
                    mysConec = FunMysql.ReAbrirConeccion(mysConec);
                    n_idtes = miFun.n_IdTesoreria;
                    c_numasi = "";

                    CN_con_diario funCon = new CN_con_diario();
                
                    funCon.mysConec = mysConec;
                    funCon.STU_SISTEMA = STU_SISTEMA;

                    if (e_Detracciones.n_tipmov == 1) { n_TipoRegistro = 2; }  
                    if (e_Detracciones.n_tipmov == 2) { n_TipoRegistro = 1; }

                    if (funCon.GenerarAsientoTesoreria(STU_SISTEMA.EMPRESAID, n_idtes, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO, 1, c_numasi, n_TipoRegistro) == true)
                    {
                        if (e_Detracciones.n_aplipag == 1)
                        { 
                            mysConec = FunMysql.ReAbrirConeccion(mysConec);
                            CD_tes_tesoreria miFunTes = new CD_tes_tesoreria();
                            c_numasi = funCon.c_NewNumAsiento;
                            miFunTes.mysConec = mysConec;
                            miFunTes.AgregarNumAsi(n_idtes, c_numasi);
                        }
                        b_result = true;
                    }
                    else
                    {
                        b_OcurrioError = funCon.b_OcurrioError;
                        c_ErrorMensaje = funCon.c_ErrorMensaje;
                        n_ErrorNumber = funCon.n_ErrorNumber;
                    }
                }
            }
            else
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
           
            return b_result;
        }
        public bool Actualizar(BE_CON_REGDETRACCIONES e_Detracciones)
        {
            bool b_result = false;
            CD_con_regdetracciones miFun = new CD_con_regdetracciones();

            miFun.mysConec = mysConec;
            miFun.e_Tesoreria = e_Tesoreria;
            miFun.l_TesOri = l_TesOri;
            miFun.l_TesOriDet = l_TesOriDet;
            miFun.l_TesDes = l_TesDes;
            miFun.l_TesDesDet = l_TesDesDet;

            b_result = miFun.Actualizar(e_Detracciones);
            if (b_result == true)
            {
                if (e_Detracciones.n_aplipag == 1)
                {
                    int n_idtes = 0;
                    string c_numasi;
                    int n_TipoRegistro = 0;

                    //GENERAMOS EL ASIENTO CONTABLE
                    mysConec = FunMysql.ReAbrirConeccion(mysConec);
                    n_idtes = miFun.n_IdTesoreria;
                    c_numasi = "";

                    CD_tes_tesoreria objtes = new CD_tes_tesoreria();
                    objtes.mysConec = mysConec;
                    if (objtes.TraerRegistro(n_idtes) == true)
                    {
                        DataTable dtresult = new DataTable();
                        dtresult = objtes.DtRegistro;
                        c_numasi = dtresult.Rows[0]["c_numreg"].ToString();
                    }

                    CN_con_diario funCon = new CN_con_diario();

                    funCon.mysConec = mysConec;
                    funCon.STU_SISTEMA = STU_SISTEMA;

                    if (e_Detracciones.n_tipmov == 1) { n_TipoRegistro = 2; }
                    if (e_Detracciones.n_tipmov == 2) { n_TipoRegistro = 1; }

                    if (funCon.GenerarAsientoTesoreria(STU_SISTEMA.EMPRESAID, n_idtes, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO, 1, c_numasi, n_TipoRegistro) == true)
                    {
                        mysConec = FunMysql.ReAbrirConeccion(mysConec);
                        CD_tes_tesoreria miFunTes = new CD_tes_tesoreria();
                        c_numasi = funCon.c_NewNumAsiento;
                        miFunTes.mysConec = mysConec;
                        miFunTes.AgregarNumAsi(n_idtes, c_numasi);
                    }
                    else
                    {
                        b_OcurrioError = funCon.b_OcurrioError;
                        c_ErrorMensaje = funCon.c_ErrorMensaje;
                        n_ErrorNumber = funCon.n_ErrorNumber;
                        return b_result;
                    }
                }
                b_result = true;
            }
            else
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_result;
            }
            //else
            //{
            //    string c_NumAsi = e_Detracciones.c_numreg;
            //    CN_con_diario funCon = new CN_con_diario();
            //    funCon.mysConec = mysConec;
            //    funCon.STU_SISTEMA = STU_SISTEMA;

            //    funCon.GenerarAsientoRetencion(STU_SISTEMA.EMPRESAID, e_Detracciones.n_id, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO, 17, c_NumAsi);
            //}
            return b_result;
        }
    }
}
