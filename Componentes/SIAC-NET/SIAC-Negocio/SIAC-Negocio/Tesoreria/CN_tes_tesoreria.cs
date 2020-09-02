using MySql.Data.MySqlClient;
using SIAC_DATOS.Almacen;
using SIAC_DATOS.Tesoreria;
using SIAC_DATOS.Sistema;
using SIAC_DATOS.Contabilidad;
using System;
using System.Collections.Generic;
using System.Data;
using SIAC_Entidades.Tesoreria;
using SIAC_Negocio.Contabilidad;
using Helper;

namespace SIAC_Negocio.Tesoreria
{
    public class CN_tes_tesoreria
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        DatosMySql FunMysql = new DatosMySql();
        Genericas funDatos = new Genericas();

        public DataTable dtLista = new DataTable();
        public BE_TES_TESORERIA e_Tesoreria = new BE_TES_TESORERIA();
        public List<BE_TES_TESORERIADES> l_TesoreriaDes = new List<BE_TES_TESORERIADES>();
        public List<BE_TES_TESORERIADESDET> l_TesoreriaDesDet  = new List<BE_TES_TESORERIADESDET>();
        public List<BE_TES_TESORERIAORI> l_TesoreriaOri = new List<BE_TES_TESORERIAORI>();
        public List<BE_TES_TESORERIAORIDET> l_TesoreriaOriDet = new List<BE_TES_TESORERIAORIDET>();

        public void Listar(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrasbajo, int n_TipoRegistro)
        {
            DataTable dtResul = new DataTable();

            CD_tes_tesoreria miFun = new CD_tes_tesoreria();
            miFun.mysConec = mysConec;

            miFun.Listar(n_IdEmpresa, n_AnoTrabajo, n_MesTrasbajo, n_TipoRegistro);
            dtLista = miFun.DtLista;

            if (dtResul == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return;
        }
        public void TraerRegistro(int n_IdRegistro)
        {
            DataTable dtResult = new DataTable();
            DataTable dtResultOri = new DataTable();
            DataTable dtResultOriDet = new DataTable();
            DataTable dtResultDes = new DataTable();
            DataTable dtResultDesDet = new DataTable();
            bool b_Result;
            CD_tes_tesoreria miFun = new CD_tes_tesoreria();
            int n_row = 0;
            miFun.mysConec = mysConec;

            b_Result = miFun.TraerRegistro(n_IdRegistro);
            dtResult = miFun.DtRegistro;
            dtResultOri = miFun.DtRegistroOri;
            dtResultOriDet = miFun.DtRegistroOriDet;
            dtResultDes = miFun.DtRegistroDes;
            dtResultDesDet = miFun.DtRegistroDesDet;

            if (dtResult.Rows.Count != 0)
            {
                e_Tesoreria.n_idemp = Convert.ToInt32(dtResult.Rows[0]["n_idemp"]);
                e_Tesoreria.n_ano = Convert.ToInt32(dtResult.Rows[0]["n_ano"]);
                e_Tesoreria.n_mes = Convert.ToInt32(dtResult.Rows[0]["n_mes"]);
                e_Tesoreria.n_idlib = Convert.ToInt32(dtResult.Rows[0]["n_idlib"]);
                e_Tesoreria.n_id = Convert.ToInt32(dtResult.Rows[0]["n_id"]);
                e_Tesoreria.c_numreg = dtResult.Rows[0]["c_numreg"].ToString();
                e_Tesoreria.d_fchope = Convert.ToDateTime(dtResult.Rows[0]["d_fchope"]);
                e_Tesoreria.n_idmon = Convert.ToInt32(dtResult.Rows[0]["n_idmon"]);
                e_Tesoreria.c_glo = dtResult.Rows[0]["c_glo"].ToString();
                e_Tesoreria.n_conciliado = Convert.ToInt32(dtResult.Rows[0]["n_conciliado"]);
                e_Tesoreria.n_tc = Convert.ToDouble(dtResult.Rows[0]["n_tc"]);
                e_Tesoreria.n_tipreg = Convert.ToInt32(dtResult.Rows[0]["n_tipreg"]);
                e_Tesoreria.n_dongen = Convert.ToInt32(dtResult.Rows[0]["n_dongen"]);

                // CARGAMOS LA LISTA DE ORIGENES
                for (n_row = 0; n_row <= dtResultOri.Rows.Count - 1; n_row++)
                {
                    BE_TES_TESORERIAORI e_TesoreriaOri = new BE_TES_TESORERIAORI();

                    e_TesoreriaOri.n_idtes = Convert.ToInt32(dtResultOri.Rows[n_row]["n_idtes"]);
                    e_TesoreriaOri.n_idori = Convert.ToInt32(dtResultOri.Rows[n_row]["n_idori"]);
                    e_TesoreriaOri.n_imp = Convert.ToDouble(dtResultOri.Rows[n_row]["n_imp"]);
                    e_TesoreriaOri.n_idmod = Convert.ToInt32(dtResultOri.Rows[n_row]["n_idmod"]);
                    e_TesoreriaOri.n_idbcocta = Convert.ToInt32(dtResultOri.Rows[n_row]["n_idbcocta"]);
                    e_TesoreriaOri.n_tc = Convert.ToDouble(dtResultOri.Rows[n_row]["n_tc"]);

                    l_TesoreriaOri.Add(e_TesoreriaOri);
                }

                // CARGAMOS EL DETALLE DE LOS ORIGENES DETALLE
                for (n_row = 0; n_row <= dtResultOriDet.Rows.Count - 1; n_row++)
                {
                    BE_TES_TESORERIAORIDET e_TesoreriaOriDet = new BE_TES_TESORERIAORIDET();

                    e_TesoreriaOriDet.n_idtes = Convert.ToInt32(dtResultOriDet.Rows[n_row]["n_idtes"]);
                    e_TesoreriaOriDet.n_idori = Convert.ToInt32(dtResultOriDet.Rows[n_row]["n_idori"]);
                    e_TesoreriaOriDet.n_idtipper = Convert.ToInt32(dtResultOriDet.Rows[n_row]["n_idtipper"]);
                    e_TesoreriaOriDet.n_idmod = Convert.ToInt32(dtResultOriDet.Rows[n_row]["n_idmod"]);
                    e_TesoreriaOriDet.n_iddoc = Convert.ToInt32(dtResultOriDet.Rows[n_row]["n_iddoc"]);
                    e_TesoreriaOriDet.n_idper = Convert.ToInt32(dtResultOriDet.Rows[n_row]["n_idper"]);
                    e_TesoreriaOriDet.n_idtipdoc = Convert.ToInt32(dtResultOriDet.Rows[n_row]["n_idtipdoc"]);
                    e_TesoreriaOriDet.c_numser = dtResultOriDet.Rows[n_row]["c_numser"].ToString();
                    e_TesoreriaOriDet.c_numdoc = dtResultOriDet.Rows[n_row]["c_numdoc"].ToString();
                    e_TesoreriaOriDet.n_imp = Convert.ToDouble(dtResultOriDet.Rows[n_row]["n_imp"]);
                    e_TesoreriaOriDet.n_sal = Convert.ToDouble(dtResultOriDet.Rows[n_row]["n_sal"]);
                    e_TesoreriaOriDet.n_acuenta = Convert.ToDouble(dtResultOriDet.Rows[n_row]["n_acuenta"]);
                    e_TesoreriaOriDet.n_idori = Convert.ToInt32(dtResultOriDet.Rows[n_row]["n_idori"]);
                    e_TesoreriaOriDet.d_fchdoc = Convert.ToDateTime(dtResultOriDet.Rows[n_row]["d_fchdoc"]);
                    e_TesoreriaOriDet.c_glo = dtResultOriDet.Rows[n_row]["c_glo"].ToString();
                    e_TesoreriaOriDet.n_cor = Convert.ToInt32(dtResultOriDet.Rows[n_row]["n_cor"]);
                    e_TesoreriaOriDet.n_idmon = Convert.ToInt32(dtResultOriDet.Rows[n_row]["n_idmon"]);
                    e_TesoreriaOriDet.n_idmedpag = Convert.ToInt32(dtResultOriDet.Rows[n_row]["n_idmedpag"]);
                    l_TesoreriaOriDet.Add(e_TesoreriaOriDet);
                }

                // CARGAMOS LA LISTA DE DESTINOS
                for (n_row = 0; n_row <= dtResultDes.Rows.Count - 1; n_row++)
                {
                    BE_TES_TESORERIADES e_TesoreriaDes = new BE_TES_TESORERIADES();

                    e_TesoreriaDes.n_idtes = Convert.ToInt32(dtResultDes.Rows[n_row]["n_idtes"]);
                    e_TesoreriaDes.n_iddes = Convert.ToInt32(dtResultDes.Rows[n_row]["n_iddes"]);
                    e_TesoreriaDes.n_imp = Convert.ToDouble(dtResultDes.Rows[n_row]["n_imp"]);
                    e_TesoreriaDes.n_idmod = Convert.ToInt32(dtResultDes.Rows[n_row]["n_idmod"]);
                    e_TesoreriaDes.n_idbcocta = Convert.ToInt32(dtResultDes.Rows[n_row]["n_idbcocta"]);
                    e_TesoreriaDes.n_tc = Convert.ToDouble(dtResultDes.Rows[n_row]["n_tc"]);

                    l_TesoreriaDes.Add(e_TesoreriaDes);
                }

                // CARGAMOS EL DETALLE DE LOS DESTINOS
                for (n_row = 0; n_row <= dtResultDesDet.Rows.Count - 1; n_row++)
                {
                    BE_TES_TESORERIADESDET e_TesoreriaDesDet = new BE_TES_TESORERIADESDET();

                    e_TesoreriaDesDet.n_idtes = Convert.ToInt32(dtResultDesDet.Rows[n_row]["n_idtes"]);
                    e_TesoreriaDesDet.n_iddes = Convert.ToInt32(dtResultDesDet.Rows[n_row]["n_iddes"]);
                    e_TesoreriaDesDet.n_idtipper = Convert.ToInt32(dtResultDesDet.Rows[n_row]["n_idtipper"]);
                    e_TesoreriaDesDet.n_idmod = Convert.ToInt32(dtResultDesDet.Rows[n_row]["n_idmod"]);
                    e_TesoreriaDesDet.n_iddoc = Convert.ToInt32(dtResultDesDet.Rows[n_row]["n_iddoc"]);
                    e_TesoreriaDesDet.n_idper = Convert.ToInt32(dtResultDesDet.Rows[n_row]["n_idper"]);
                    e_TesoreriaDesDet.n_idtipdoc = Convert.ToInt32(dtResultDesDet.Rows[n_row]["n_idtipdoc"]);
                    e_TesoreriaDesDet.c_numser = dtResultDesDet.Rows[n_row]["c_numser"].ToString();
                    e_TesoreriaDesDet.c_numdoc = dtResultDesDet.Rows[n_row]["c_numdoc"].ToString();
                    e_TesoreriaDesDet.n_imp = Convert.ToDouble(dtResultDesDet.Rows[n_row]["n_imp"]);
                    e_TesoreriaDesDet.n_sal = Convert.ToDouble(dtResultDesDet.Rows[n_row]["n_sal"]);
                    e_TesoreriaDesDet.n_acuenta = Convert.ToDouble(dtResultDesDet.Rows[n_row]["n_acuenta"]);
                    //e_TesoreriaDesDet.n_idori = Convert.ToInt32(dtResultDesDet.Rows[0]["n_idori"]);
                    e_TesoreriaDesDet.d_fchdoc = Convert.ToDateTime(dtResultDesDet.Rows[n_row]["d_fchdoc"]);
                    e_TesoreriaDesDet.c_glo = dtResultDesDet.Rows[n_row]["c_glo"].ToString();
                    e_TesoreriaDesDet.n_cor = Convert.ToInt32(dtResultDesDet.Rows[n_row]["n_cor"]);
                    e_TesoreriaDesDet.n_idmon = Convert.ToInt32(dtResultDesDet.Rows[n_row]["n_idmon"]);
                    e_TesoreriaDesDet.n_idlib = Convert.ToInt32(dtResultDesDet.Rows[n_row]["n_idlib"]);
                    l_TesoreriaDesDet.Add(e_TesoreriaDesDet);
                }
            }
            if (b_Result == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return;
        }
        public bool Eliminar(int n_IdRegistro)
        {
            bool booResult = false;
            CD_tes_tesoreria miFun = new CD_tes_tesoreria();

            miFun.mysConec = mysConec;

            if (miFun.TraerRegistro(n_IdRegistro) == true)
            {
                miFun.DtRegistroDesDet = miFun.DtRegistroDesDet;
            }

            booResult = miFun.Eliminar(n_IdRegistro);
            if (booResult == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return booResult;
        }
        public bool Insertar(BE_TES_TESORERIA entTesoreria, List<BE_TES_TESORERIAORI> lstTesoreriaOri, List<BE_TES_TESORERIAORIDET> lstTesoreriaOriDet,
            List<BE_TES_TESORERIADES> lstTesoreriaDes, List<BE_TES_TESORERIADESDET> lstTesoreriaDesDet, int n_TipoRegistro)
        {
            CD_tes_tesoreria miFun = new CD_tes_tesoreria();
            bool booOk = false;

            miFun.mysConec = mysConec;
            if (miFun.Insertar(entTesoreria, lstTesoreriaOri, lstTesoreriaOriDet, lstTesoreriaDes, lstTesoreriaDesDet) == true)
            { 
                 //GENERAMOS EL ASIENTO CONTABLE
                 mysConec = FunMysql.ReAbrirConeccion(mysConec);
                 entTesoreria.n_id = miFun.n_IdGenerado;

                 string c_NumAsi = entTesoreria.c_numreg;
                 
                 CN_con_diario funCon = new CN_con_diario();
                 funCon.mysConec = mysConec;
                 funCon.STU_SISTEMA = STU_SISTEMA;

                 if (funCon.GenerarAsientoTesoreria(STU_SISTEMA.EMPRESAID, Convert.ToInt32(entTesoreria.n_id), STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO, 1, c_NumAsi, n_TipoRegistro) == true)
                 {
                     mysConec = FunMysql.ReAbrirConeccion(mysConec);
                     c_NumAsi = funCon.c_NewNumAsiento;
                     miFun.AgregarNumAsi(entTesoreria.n_id, c_NumAsi);

                     booOk = true;
                 }
                 else
                 {
                     b_OcurrioError = funCon.b_OcurrioError;
                     c_ErrorMensaje = funCon.c_ErrorMensaje;
                     n_ErrorNumber = funCon.n_ErrorNumber;
                     return booOk;
                 }
            }
            else
            { 
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return booOk;
            }
            booOk = true;
            return booOk;
        }
        public bool Actualizar(BE_TES_TESORERIA entTesoreria, List<BE_TES_TESORERIAORI> lstTesoreriaOri, List<BE_TES_TESORERIAORIDET> lstTesoreriaOriDet,
            List<BE_TES_TESORERIADES> lstTesoreriaDes, List<BE_TES_TESORERIADESDET> lstTesoreriaDesDet, int n_TipoRegistro)
        {
            CD_tes_tesoreria miFun = new CD_tes_tesoreria();
            bool booOk = false;

            miFun.mysConec = mysConec;
            if (miFun.TraerRegistro(entTesoreria.n_id) == true)
            {
                miFun.DtRegistroDesDet = miFun.DtRegistroDesDet;
            }
            if (miFun.Actualizar(entTesoreria, lstTesoreriaOri, lstTesoreriaOriDet, lstTesoreriaDes, lstTesoreriaDesDet) == true)
            {
                //GENERAMOS EL ASIENTO CONTABLE
                mysConec = FunMysql.ReAbrirConeccion(mysConec);
                //entTesoreria.n_id = miFun.n_IdGenerado;

                string c_NumAsi = entTesoreria.c_numreg;

                CN_con_diario funCon = new CN_con_diario();
                funCon.mysConec = mysConec;
                funCon.STU_SISTEMA = STU_SISTEMA;

                if (funCon.GenerarAsientoTesoreria(STU_SISTEMA.EMPRESAID, Convert.ToInt32(entTesoreria.n_id), STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO, 1, c_NumAsi, n_TipoRegistro) == true)
                {
                    //mysConec = FunMysql.ReAbrirConeccion(mysConec);
                    //c_NumAsi = funCon.c_NewNumAsiento;
                    //miFun.AgregarNumAsi(entTesoreria.n_id, c_NumAsi);

                    booOk = true;
                }
                else
                {
                    b_OcurrioError = funCon.b_OcurrioError;
                    c_ErrorMensaje = funCon.c_ErrorMensaje;
                    n_ErrorNumber = funCon.n_ErrorNumber;
                }
            }
            else 
            { 
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return booOk;
        }
        public void ImprimirDocumento(double n_idPedido, int n_Tipo)
        {
            string c_Archivo = "";
            string c_Ruta = "";
            bool b_Exportar = false;
            string[,] arrPara = new string[1, 3];
            string[,] arrPara2 = new string[2, 3];

            arrPara[0, 0] = "n_id";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = n_idPedido.ToString();

            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "tesoreria\\Rpt_Voucher_Egresos.rpt";
 
            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "TESORERIA - VAUCHER DE EGRESOS";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.b_Exportar = b_Exportar;
            xVisor.c_NombreArchivoExportar = c_Archivo;
            xVisor.VerCrystal();
        }
        public bool RegeneraAsientos(int n_IdEmpresa, int n_IdMesTrabajo, int n_IdAnoTrabajo, int n_IdLibro, int n_TipoRegistro)
        {
            // n_TipoRegistro = 1 INGRESOS     n_TipoRegistro = 2 EGRESOS
            DataTable dtLis = new DataTable();
            bool b_result = false;
            int n_row = 0;
            int n_idreg = 0;
            CD_tes_tesoreria miFun = new CD_tes_tesoreria();
            CD_con_diario o_Conta = new CD_con_diario();
            CN_con_diario funCon = new CN_con_diario();
            miFun.mysConec = mysConec;

            o_Conta.mysConec = mysConec;
            //b_result = o_Conta.EliminarLibroMes(n_IdLibro, n_IdAnoTrabajo, n_IdMesTrabajo, n_IdEmpresa);
            b_result = true; //o_Conta.EliminarLibroMes(n_IdLibro, n_IdAnoTrabajo, n_IdMesTrabajo, n_IdEmpresa);
            if (b_result == true)
            {
                miFun.Listar(n_IdEmpresa, n_IdAnoTrabajo, n_IdMesTrabajo, n_TipoRegistro);
                dtLis = miFun.DtLista;
                // objRegistros.Listar(STU_SISTEMA.EMPRESAID, STU_SISTEMA.ANOTRABAJO, Convert.ToInt32(CboMeses.SelectedValue), 1);
                dtLis = funDatos.DataTableOrdenar(dtLis, "c_numreg");
                for (n_row = 0; n_row <= dtLis.Rows.Count - 1; n_row++)
                {
                    string c_NumAsi = dtLis.Rows[n_row]["c_numreg"].ToString().Substring(4, 4);
                    n_idreg = Convert.ToInt32(dtLis.Rows[n_row]["n_id"].ToString());
                    funCon.mysConec = mysConec;
                    funCon.STU_SISTEMA = STU_SISTEMA;
                    //funCon.GenerarAsientoVentas(STU_SISTEMA.EMPRESAID, n_idreg, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO, 14, c_NumAsi);
                    funCon.GenerarAsientoTesoreria(STU_SISTEMA.EMPRESAID, n_idreg, STU_SISTEMA.ANOTRABAJO, STU_SISTEMA.MESTRABAJO, 1, c_NumAsi, n_TipoRegistro);
                    //c_NumAsi = funCon.c_NewNumAsiento;
                    //miFun.AgregarNumAsi(n_idreg, c_NumAsi);
                }
                b_result = true;
            }
            return b_result;
        }
    }
}
