using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades;
using SIAC_DATOS.Contabilidad;
using SIAC_DATOS.Logistica;
using SIAC_DATOS.Ventas;
using SIAC_DATOS.Tesoreria;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Contabilidad;
using Helper;

namespace SIAC_Negocio.Contabilidad
{
    public class CN_con_diario
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtLista = new DataTable();
        public DataTable dtResumen = new DataTable();
        public BE_CON_DIARIO e_Diario = new BE_CON_DIARIO();
        public List<BE_CON_DIARIO> l_Diario = new List<BE_CON_DIARIO>();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public string c_NewNumAsiento;
        
        Helper.Comunes.Funciones funfunciones = new Helper.Comunes.Funciones();
        public string ObtenerUltimoAsiento(int n_AnoTrabajo, int n_MesTrabajo, int n_IdLibro, int n_IdEmpresa)
        {
            string c_valor = "";
            CD_con_diario miFun = new CD_con_diario();
            miFun.mysConec = mysConec;

            miFun.ObtenerUltimoAsiento(n_AnoTrabajo, n_MesTrabajo, n_IdLibro, n_IdEmpresa);
            dtLista = miFun.dtLista;
            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return "";
            }
            c_valor = dtLista.Rows[0]["c_newnumero"].ToString();
            return c_valor;
        }
        public bool GenerarAsientoCompras(int n_IdEmpresa, int n_IdCompra, int n_AnoTrabajo, int n_MesTrabajo, int n_IdLibro, string c_NumAsiento)
        {
            CD_con_diario funDat = new CD_con_diario();
            CD_log_compras funCompras = new CD_log_compras();
            DataTable dtUltAsi = new DataTable();
            DataTable dtComCab = new DataTable();
            DataTable dtComDet = new DataTable();
            bool b_result = false;
            int n_row = 0;
            string c_numasi = "";

            if (c_NumAsiento == "")
            {
                funDat.mysConec = mysConec;
                funDat.ObtenerUltimoAsiento(n_AnoTrabajo, n_MesTrabajo, n_IdLibro, n_IdEmpresa);
                dtUltAsi = funDat.dtLista;
                c_numasi = dtUltAsi.Rows[0]["c_newnumero"].ToString();
            }
            else
            {
                c_numasi = c_NumAsiento;
                funDat.mysConec = mysConec;
                if (c_NumAsiento.Length > 4)
                {
                    c_numasi = c_numasi.Substring(4, 4);
                }
                funDat.Eliminar(n_IdLibro, n_AnoTrabajo, n_MesTrabajo, c_numasi, n_IdEmpresa);
            }

            funCompras.mysConec = mysConec;
            funCompras.AsientoCab(n_IdEmpresa, n_IdCompra);
            dtComCab = funCompras.dtLista;
            dtComDet = funCompras.dtListaDet;

            l_Diario.Clear();

            // CUENTA HABER DEL ASIENTO - TOTAL DEL DOCUMENTO
            BE_CON_DIARIO e_DiarioH = new BE_CON_DIARIO();
            e_DiarioH.n_id = 0;
            e_DiarioH.n_idemp = n_IdEmpresa;
            e_DiarioH.n_ano = STU_SISTEMA.ANOTRABAJO;
            e_DiarioH.n_mes = STU_SISTEMA.MESTRABAJO;
            e_DiarioH.n_lib = n_IdLibro;
            e_DiarioH.c_numasi = c_numasi;
            e_DiarioH.n_idcue = Convert.ToInt16(funfunciones.NulosN(dtComCab.Rows[0]["n_idcuecom"]));
            e_DiarioH.n_tc = Convert.ToDouble(dtComCab.Rows[0]["n_imptc"]);
            if (Convert.ToInt16(dtComCab.Rows[0]["n_idtipdoc"]) == 8)
            {
                e_DiarioH.n_impdebsol = Convert.ToDouble(dtComCab.Rows[0]["n_impcomsol"]); ;
                e_DiarioH.n_imphabsol = 0;
                e_DiarioH.n_impdebdol = Convert.ToDouble(funfunciones.NulosN(dtComCab.Rows[0]["n_impcomdol"]));
                e_DiarioH.n_imphabdol = 0;
            }
            else
            {
                e_DiarioH.n_impdebsol = 0;
                e_DiarioH.n_imphabsol = Convert.ToDouble(dtComCab.Rows[0]["n_impcomsol"]);
                e_DiarioH.n_impdebdol = 0;
                e_DiarioH.n_imphabdol = Convert.ToDouble(funfunciones.NulosN(dtComCab.Rows[0]["n_impcomdol"]));
            }
            //e_DiarioH.n_impdebsol = 0;
            //e_DiarioH.n_imphabsol = Convert.ToDouble(dtComCab.Rows[0]["n_impcomsol"]);
            //e_DiarioH.n_impdebdol = 0;
            //e_DiarioH.n_imphabdol = Convert.ToDouble(funfunciones.NulosN(dtComCab.Rows[0]["n_impcomdol"]));
            e_DiarioH.d_fchasi = Convert.ToDateTime(dtComCab.Rows[0]["d_fchreg"]);
            e_DiarioH.d_orifchdoc = Convert.ToDateTime(dtComCab.Rows[0]["d_fchdoc"]);
            e_DiarioH.n_oriid = Convert.ToInt16(dtComCab.Rows[0]["n_iddoccom"]);
            e_DiarioH.n_oriidtipdoc = Convert.ToInt16(dtComCab.Rows[0]["n_idtipdoc"]);
            e_DiarioH.n_oriidtipmon = Convert.ToInt16(dtComCab.Rows[0]["n_idmon"]);
            e_DiarioH.c_orinumdoc = dtComCab.Rows[0]["c_numdoc"].ToString();
            e_DiarioH.c_origlo = dtComCab.Rows[0]["c_glosa"].ToString();
            e_DiarioH.c_oridestipmon = dtComCab.Rows[0]["c_destipmon"].ToString();
            e_DiarioH.c_oridestipdoc = dtComCab.Rows[0]["c_destipdoc"].ToString();
            e_DiarioH.c_orinomcli = dtComCab.Rows[0]["c_pronom"].ToString();
            e_DiarioH.c_orinumruc = dtComCab.Rows[0]["c_pronumruc"].ToString();

            l_Diario.Add(e_DiarioH);

            // CUENTA DEBE DEL ASIENTO - IGV DL DOCUMENTO
            if ((Convert.ToDouble(dtComCab.Rows[0]["n_impigvsol"]) != 0) && (Convert.ToDouble(funfunciones.NulosN(dtComCab.Rows[0]["n_impigvdol"])) != 0))
            {
                BE_CON_DIARIO e_DiarioD = new BE_CON_DIARIO();
                e_DiarioD.n_id = 0;
                e_DiarioD.n_idemp = n_IdEmpresa;
                e_DiarioD.n_ano = STU_SISTEMA.ANOTRABAJO;
                e_DiarioD.n_mes = STU_SISTEMA.MESTRABAJO;
                e_DiarioD.n_lib = n_IdLibro;
                e_DiarioD.c_numasi = c_numasi;
                e_DiarioD.n_idcue = Convert.ToInt16(funfunciones.NulosN(dtComCab.Rows[0]["n_idcueigv"]));
                e_DiarioD.n_tc = Convert.ToDouble(dtComCab.Rows[0]["n_imptc"]);

                if (Convert.ToInt16(dtComCab.Rows[0]["n_idtipdoc"]) == 8)
                {
                    if (n_IdLibro == 8)
                    {
                        e_DiarioD.n_impdebsol = 0;
                        e_DiarioD.n_imphabsol = Convert.ToDouble(dtComCab.Rows[0]["n_impigvsol"]);
                        e_DiarioD.n_impdebdol = 0;
                        e_DiarioD.n_imphabdol = Convert.ToDouble(dtComCab.Rows[0]["n_impigvdol"]);
                    }
                    if (n_IdLibro == 32)
                    {
                        e_DiarioD.n_impdebsol = Convert.ToDouble(dtComCab.Rows[0]["n_impigvsol"]);
                        e_DiarioD.n_imphabsol = 0;
                        e_DiarioD.n_impdebdol = Convert.ToDouble(dtComCab.Rows[0]["n_impigvdol"]);
                        e_DiarioD.n_imphabdol = 0;
                    }
                }
                else
                {
                    if (n_IdLibro == 8)
                    {
                        e_DiarioD.n_impdebsol = Convert.ToDouble(dtComCab.Rows[0]["n_impigvsol"]);
                        e_DiarioD.n_imphabsol = 0;
                        e_DiarioD.n_impdebdol = Convert.ToDouble(dtComCab.Rows[0]["n_impigvdol"]);
                        e_DiarioD.n_imphabdol = 0;
                    }
                    if (n_IdLibro == 32)
                    {
                        e_DiarioD.n_impdebsol = 0;
                        e_DiarioD.n_imphabsol = Convert.ToDouble(dtComCab.Rows[0]["n_impigvsol"]);
                        e_DiarioD.n_impdebdol = 0;
                        e_DiarioD.n_imphabdol = Convert.ToDouble(dtComCab.Rows[0]["n_impigvdol"]);
                    }
                }
                e_DiarioD.d_fchasi = Convert.ToDateTime(dtComCab.Rows[0]["d_fchreg"]);
                e_DiarioD.d_orifchdoc = Convert.ToDateTime(dtComCab.Rows[0]["d_fchdoc"]);
                e_DiarioD.n_oriid = Convert.ToInt16(dtComCab.Rows[0]["n_iddoccom"]);
                e_DiarioD.n_oriidtipdoc = Convert.ToInt16(dtComCab.Rows[0]["n_idtipdoc"]);
                e_DiarioD.n_oriidtipmon = Convert.ToInt16(dtComCab.Rows[0]["n_idmon"]);
                e_DiarioD.c_orinumdoc = dtComCab.Rows[0]["c_numdoc"].ToString();
                e_DiarioD.c_origlo = dtComCab.Rows[0]["c_glosa"].ToString();
                e_DiarioD.c_oridestipmon = dtComCab.Rows[0]["c_destipmon"].ToString();
                e_DiarioD.c_oridestipdoc = dtComCab.Rows[0]["c_destipdoc"].ToString();
                e_DiarioD.c_orinomcli = dtComCab.Rows[0]["c_pronom"].ToString();
                e_DiarioD.c_orinumruc = dtComCab.Rows[0]["c_pronumruc"].ToString();
                l_Diario.Add(e_DiarioD);
            }
            // AGREGAMOS EL DEBE DEL DETALLE DE LA COMPRA
            for (n_row = 0; n_row <= dtComDet.Rows.Count - 1; n_row++)
            {
                BE_CON_DIARIO e_DiarioA = new BE_CON_DIARIO();
                e_DiarioA.n_id = 0;
                e_DiarioA.n_idemp = n_IdEmpresa;
                e_DiarioA.n_ano = STU_SISTEMA.ANOTRABAJO;
                e_DiarioA.n_mes = STU_SISTEMA.MESTRABAJO;
                e_DiarioA.n_lib = n_IdLibro;
                e_DiarioA.c_numasi = c_numasi;
                e_DiarioA.n_idcue = Convert.ToInt16(funfunciones.NulosN(dtComDet.Rows[n_row]["n_idcuecon"]));
                e_DiarioA.n_tc = Convert.ToDouble(dtComCab.Rows[0]["n_imptc"]);

                if (Convert.ToInt16(dtComCab.Rows[0]["n_idtipdoc"]) == 8)
                {
                    if (Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impdebsol"])) != 0)
                    {
                        e_DiarioA.n_impdebsol = 0;
                        e_DiarioA.n_imphabsol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impdebsol"]));
                        e_DiarioA.n_impdebdol = 0;
                        e_DiarioA.n_imphabdol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impdebdol"]));
                    }
                    else
                    {
                        e_DiarioA.n_impdebsol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_imphabsol"]));
                        e_DiarioA.n_imphabsol = 0;
                        e_DiarioA.n_impdebdol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_imphabdol"]));
                        e_DiarioA.n_imphabdol = 0;
                    }
                }
                else
                {
                    //e_DiarioA.n_impdebsol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impdebsol"]));
                    //e_DiarioA.n_imphabsol = 0;
                    //e_DiarioA.n_impdebdol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impdebdol"]));
                    //e_DiarioA.n_imphabdol = 0;
                    e_DiarioA.n_impdebsol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impdebsol"]));
                    e_DiarioA.n_imphabsol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_imphabsol"]));
                    e_DiarioA.n_impdebdol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impdebdol"]));
                    e_DiarioA.n_imphabdol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_imphabdol"]));
                }


                //e_DiarioA.n_impdebsol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impdebsol"]));
                //e_DiarioA.n_imphabsol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_imphabsol"]));
                //e_DiarioA.n_impdebdol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impdebdol"]));
                //e_DiarioA.n_imphabdol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_imphabdol"]));
                e_DiarioA.d_fchasi = Convert.ToDateTime(dtComCab.Rows[0]["d_fchreg"]);
                e_DiarioA.d_orifchdoc = Convert.ToDateTime(dtComCab.Rows[0]["d_fchdoc"]);
                e_DiarioA.n_oriid = Convert.ToInt16(dtComCab.Rows[0]["n_iddoccom"]);
                e_DiarioA.n_oriidtipdoc = Convert.ToInt16(dtComCab.Rows[0]["n_idtipdoc"]);
                e_DiarioA.n_oriidtipmon = Convert.ToInt16(dtComCab.Rows[0]["n_idmon"]);
                e_DiarioA.c_orinumdoc = dtComCab.Rows[0]["c_numdoc"].ToString();
                e_DiarioA.c_origlo = dtComCab.Rows[0]["c_glosa"].ToString();
                e_DiarioA.c_oridestipmon = dtComCab.Rows[0]["c_destipmon"].ToString();
                e_DiarioA.c_oridestipdoc = dtComCab.Rows[0]["c_destipdoc"].ToString();
                e_DiarioA.c_orinomcli = dtComCab.Rows[0]["c_pronom"].ToString();
                e_DiarioA.c_orinumruc = dtComCab.Rows[0]["c_pronumruc"].ToString();
                l_Diario.Add(e_DiarioA);
            }
            funDat.mysConec = mysConec;
            if (funDat.Insertar(l_Diario) == false)
            {
                b_OcurrioError = funDat.b_OcurrioError;
                c_ErrorMensaje = funDat.c_ErrorMensaje;
                n_ErrorNumber = funDat.n_ErrorNumber;
                return b_result;
            }
            b_result = true;
            c_NewNumAsiento = c_numasi;
            return b_result;
        }
        //public bool GenerarAsientoCompras(int n_IdEmpresa, int n_IdCompra, int n_AnoTrabajo, int n_MesTrabajo, int n_IdLibro, string c_NumAsiento)
        //{
        //    CD_con_diario funDat = new CD_con_diario();
        //    CD_log_compras funCompras = new CD_log_compras();
        //    DataTable dtUltAsi = new DataTable();
        //    DataTable dtComCab = new DataTable();
        //    DataTable dtComDet = new DataTable();
        //    bool b_result = false;
        //    int n_row = 0;
        //    string c_numasi = "";

        //    if (c_NumAsiento == "")
        //    {
        //        funDat.mysConec = mysConec;
        //        funDat.ObtenerUltimoAsiento(n_AnoTrabajo, n_MesTrabajo, n_IdLibro, n_IdEmpresa);
        //        dtUltAsi = funDat.dtLista;
        //        c_numasi = dtUltAsi.Rows[0]["c_newnumero"].ToString();
        //    }
        //    else
        //    {
        //        c_numasi = c_NumAsiento;
        //        funDat.mysConec = mysConec;
        //        if (c_NumAsiento.Length > 4)
        //        {
        //            c_numasi = c_numasi.Substring(4, 4);
        //        }
        //        funDat.Eliminar(n_IdLibro, n_AnoTrabajo, n_MesTrabajo, c_numasi, n_IdEmpresa);
        //    }

        //    funCompras.mysConec = mysConec;
        //    funCompras.AsientoCab(n_IdEmpresa,n_IdCompra);
        //    dtComCab= funCompras.dtLista;
        //    dtComDet = funCompras.dtListaDet;

        //    l_Diario.Clear();

        //    // CUENTA HABER DEL ASIENTO - TOTAL DEL DOCUMENTO
        //    BE_CON_DIARIO e_DiarioH = new BE_CON_DIARIO();
        //    e_DiarioH.n_id = 0;
        //    e_DiarioH.n_idemp = n_IdEmpresa;
        //    e_DiarioH.n_ano = STU_SISTEMA.ANOTRABAJO;
        //    e_DiarioH.n_mes = STU_SISTEMA.MESTRABAJO;
        //    e_DiarioH.n_lib = n_IdLibro;
        //    e_DiarioH.c_numasi = c_numasi;
        //    e_DiarioH.n_idcue = Convert.ToInt16(funfunciones.NulosN(dtComCab.Rows[0]["n_idcuecom"]));
        //    e_DiarioH.n_tc = Convert.ToDouble(dtComCab.Rows[0]["n_imptc"]);
        //    e_DiarioH.n_impdebsol = 0;
        //    e_DiarioH.n_imphabsol = Convert.ToDouble(dtComCab.Rows[0]["n_impcomsol"]);
        //    e_DiarioH.n_impdebdol = 0;
        //    e_DiarioH.n_imphabdol = Convert.ToDouble(funfunciones.NulosN(dtComCab.Rows[0]["n_impcomdol"]));
        //    e_DiarioH.d_fchasi = Convert.ToDateTime(dtComCab.Rows[0]["d_fchreg"]);
        //    e_DiarioH.d_orifchdoc = Convert.ToDateTime(dtComCab.Rows[0]["d_fchdoc"]);
        //    e_DiarioH.n_oriid = Convert.ToInt16(dtComCab.Rows[0]["n_iddoccom"]);
        //    e_DiarioH.n_oriidtipdoc = Convert.ToInt16(dtComCab.Rows[0]["n_idtipdoc"]);
        //    e_DiarioH.n_oriidtipmon = Convert.ToInt16(dtComCab.Rows[0]["n_idmon"]);
        //    e_DiarioH.c_orinumdoc = dtComCab.Rows[0]["c_numdoc"].ToString();
        //    e_DiarioH.c_origlo = dtComCab.Rows[0]["c_glosa"].ToString();
        //    e_DiarioH.c_oridestipmon = dtComCab.Rows[0]["c_destipmon"].ToString();
        //    e_DiarioH.c_oridestipdoc = dtComCab.Rows[0]["c_destipdoc"].ToString();
        //    e_DiarioH.c_orinomcli = dtComCab.Rows[0]["c_pronom"].ToString();
        //    e_DiarioH.c_orinumruc = dtComCab.Rows[0]["c_pronumruc"].ToString();

        //    l_Diario.Add(e_DiarioH);

        //    // CUENTA DEBE DEL ASIENTO - IGV DL DOCUMENTO
        //    if ((Convert.ToDouble(dtComCab.Rows[0]["n_impigvsol"]) != 0) && (Convert.ToDouble(funfunciones.NulosN(dtComCab.Rows[0]["n_impigvdol"])) != 0))
        //    { 
        //        BE_CON_DIARIO e_DiarioD = new BE_CON_DIARIO();
        //        e_DiarioD.n_id = 0;
        //        e_DiarioD.n_idemp = n_IdEmpresa;
        //        e_DiarioD.n_ano = STU_SISTEMA.ANOTRABAJO;
        //        e_DiarioD.n_mes = STU_SISTEMA.MESTRABAJO;
        //        e_DiarioD.n_lib = n_IdLibro;
        //        e_DiarioD.c_numasi = c_numasi;
        //        e_DiarioD.n_idcue = Convert.ToInt16(funfunciones.NulosN(dtComCab.Rows[0]["n_idcueigv"]));
        //        e_DiarioD.n_tc = Convert.ToDouble(dtComCab.Rows[0]["n_imptc"]);

        //        if (n_IdLibro == 8)
        //        { 
        //            e_DiarioD.n_impdebsol = Convert.ToDouble(dtComCab.Rows[0]["n_impigvsol"]);
        //            e_DiarioD.n_imphabsol = 0;
        //            e_DiarioD.n_impdebdol = Convert.ToDouble(dtComCab.Rows[0]["n_impigvdol"]);
        //            e_DiarioD.n_imphabdol = 0;
        //        }
        //        if (n_IdLibro == 32)
        //        {
        //            e_DiarioD.n_impdebsol = 0;
        //            e_DiarioD.n_imphabsol = Convert.ToDouble(dtComCab.Rows[0]["n_impigvsol"]);
        //            e_DiarioD.n_impdebdol = 0;
        //            e_DiarioD.n_imphabdol = Convert.ToDouble(dtComCab.Rows[0]["n_impigvdol"]);
        //        }

        //        e_DiarioD.d_fchasi = Convert.ToDateTime(dtComCab.Rows[0]["d_fchreg"]);
        //        e_DiarioD.d_orifchdoc = Convert.ToDateTime(dtComCab.Rows[0]["d_fchdoc"]);
        //        e_DiarioD.n_oriid = Convert.ToInt16(dtComCab.Rows[0]["n_iddoccom"]);
        //        e_DiarioD.n_oriidtipdoc = Convert.ToInt16(dtComCab.Rows[0]["n_idtipdoc"]);
        //        e_DiarioD.n_oriidtipmon = Convert.ToInt16(dtComCab.Rows[0]["n_idmon"]);
        //        e_DiarioD.c_orinumdoc = dtComCab.Rows[0]["c_numdoc"].ToString();
        //        e_DiarioD.c_origlo = dtComCab.Rows[0]["c_glosa"].ToString();
        //        e_DiarioD.c_oridestipmon = dtComCab.Rows[0]["c_destipmon"].ToString();
        //        e_DiarioD.c_oridestipdoc = dtComCab.Rows[0]["c_destipdoc"].ToString();
        //        e_DiarioD.c_orinomcli = dtComCab.Rows[0]["c_pronom"].ToString();
        //        e_DiarioD.c_orinumruc = dtComCab.Rows[0]["c_pronumruc"].ToString();
        //        l_Diario.Add(e_DiarioD);
        //    }
        //    // AGREGAMOS EL DEBE DEL DETALLE DE LA COMPRA
        //    for (n_row = 0; n_row <= dtComDet.Rows.Count - 1; n_row++)
        //    {
        //        BE_CON_DIARIO e_DiarioA = new BE_CON_DIARIO();
        //        e_DiarioA.n_id = 0;
        //        e_DiarioA.n_idemp = n_IdEmpresa;
        //        e_DiarioA.n_ano = STU_SISTEMA.ANOTRABAJO;
        //        e_DiarioA.n_mes = STU_SISTEMA.MESTRABAJO;
        //        e_DiarioA.n_lib = n_IdLibro;
        //        e_DiarioA.c_numasi = c_numasi;
        //        e_DiarioA.n_idcue = Convert.ToInt16(funfunciones.NulosN(dtComDet.Rows[n_row]["n_idcuecon"]));
        //        e_DiarioA.n_tc = Convert.ToDouble(dtComCab.Rows[0]["n_imptc"]);
        //        e_DiarioA.n_impdebsol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impdebsol"]));
        //        e_DiarioA.n_imphabsol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_imphabsol"]));
        //        e_DiarioA.n_impdebdol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impdebdol"]));
        //        e_DiarioA.n_imphabdol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_imphabdol"]));
        //        e_DiarioA.d_fchasi = Convert.ToDateTime(dtComCab.Rows[0]["d_fchreg"]);
        //        e_DiarioA.d_orifchdoc = Convert.ToDateTime(dtComCab.Rows[0]["d_fchdoc"]);
        //        e_DiarioA.n_oriid = Convert.ToInt16(dtComCab.Rows[0]["n_iddoccom"]);
        //        e_DiarioA.n_oriidtipdoc = Convert.ToInt16(dtComCab.Rows[0]["n_idtipdoc"]);
        //        e_DiarioA.n_oriidtipmon = Convert.ToInt16(dtComCab.Rows[0]["n_idmon"]);
        //        e_DiarioA.c_orinumdoc = dtComCab.Rows[0]["c_numdoc"].ToString();
        //        e_DiarioA.c_origlo = dtComCab.Rows[0]["c_glosa"].ToString();
        //        e_DiarioA.c_oridestipmon = dtComCab.Rows[0]["c_destipmon"].ToString();
        //        e_DiarioA.c_oridestipdoc = dtComCab.Rows[0]["c_destipdoc"].ToString();
        //        e_DiarioA.c_orinomcli = dtComCab.Rows[0]["c_pronom"].ToString();
        //        e_DiarioA.c_orinumruc = dtComCab.Rows[0]["c_pronumruc"].ToString();
        //        l_Diario.Add(e_DiarioA);
        //    }
        //    funDat.mysConec = mysConec;
        //    if (funDat.Insertar(l_Diario) == false)
        //    {
        //        b_OcurrioError = funDat.b_OcurrioError;
        //        c_ErrorMensaje = funDat.c_ErrorMensaje;
        //        n_ErrorNumber = funDat.n_ErrorNumber;
        //        return b_result;
        //    }
        //    b_result = true;
        //    c_NewNumAsiento = c_numasi;
        //    return b_result;
        //}
        public bool GenerarAsientoVentas(int n_IdEmpresa, int n_IdCompra, int n_AnoTrabajo, int n_MesTrabajo, int n_IdLibro, string c_NumAsiento)
        {
            CD_con_diario funDat = new CD_con_diario();
            CD_vta_ventas funVentas = new CD_vta_ventas();
            DataTable dtUltAsi = new DataTable();
            DataTable dtComCab = new DataTable();
            DataTable dtComDet = new DataTable();
            bool b_result = false;
            int n_row = 0;
            string c_numasi = "";

            if (c_NumAsiento == "")
            {
                funDat.mysConec = mysConec;
                funDat.ObtenerUltimoAsiento(n_AnoTrabajo, n_MesTrabajo, n_IdLibro, n_IdEmpresa);
                dtUltAsi = funDat.dtLista;
                c_numasi = dtUltAsi.Rows[0]["c_newnumero"].ToString();
            }
            else
            {
                if (c_numasi.Length > 4)
                {
                    c_numasi = c_NumAsiento.Substring(5, 4);
                }
                else
                {
                    c_numasi = c_NumAsiento;
                }
                
                funDat.mysConec = mysConec;
                funDat.Eliminar(n_IdLibro, n_AnoTrabajo, n_MesTrabajo, c_numasi, n_IdEmpresa);
            }

            funVentas.mysConec = mysConec;
            funVentas.AsientoCab(n_IdEmpresa, n_IdCompra);
            dtComCab = funVentas.dtLista1;
            dtComDet = funVentas.dtLista2;

            l_Diario.Clear();

            // CUENTA HABER DEL ASIENTO - TOTAL DEL DOCUMENTO
            BE_CON_DIARIO e_DiarioH = new BE_CON_DIARIO();
            e_DiarioH.n_id = 0;
            e_DiarioH.n_idemp = n_IdEmpresa;
            e_DiarioH.n_ano = STU_SISTEMA.ANOTRABAJO;
            e_DiarioH.n_mes = STU_SISTEMA.MESTRABAJO;
            e_DiarioH.n_lib = n_IdLibro;
            e_DiarioH.c_numasi = c_numasi;
            e_DiarioH.n_idcue = Convert.ToInt16(funfunciones.NulosN(dtComCab.Rows[0]["n_idcueven"]));
            e_DiarioH.n_tc = Convert.ToDouble(dtComCab.Rows[0]["n_tc"]);

            if (Convert.ToInt16(dtComCab.Rows[0]["n_idtipdocven"]) == 8)
            {
                e_DiarioH.n_impdebsol = 0;
                e_DiarioH.n_imphabsol = Convert.ToDouble(dtComCab.Rows[0]["n_impvensol"]);
                e_DiarioH.n_impdebdol = 0;
                e_DiarioH.n_imphabdol = Convert.ToDouble(funfunciones.NulosN(dtComCab.Rows[0]["n_impvendol"]));
            }
            else
            { 
                e_DiarioH.n_impdebsol = Convert.ToDouble(dtComCab.Rows[0]["n_impvensol"]);
                e_DiarioH.n_imphabsol = 0;
                e_DiarioH.n_impdebdol = Convert.ToDouble(funfunciones.NulosN(dtComCab.Rows[0]["n_impvendol"]));
                e_DiarioH.n_imphabdol = 0;
            }
            e_DiarioH.d_fchasi = Convert.ToDateTime(dtComCab.Rows[0]["d_fchreg"]);
            e_DiarioH.d_orifchdoc = Convert.ToDateTime(dtComCab.Rows[0]["d_fchdoc"]);
            e_DiarioH.n_oriid = Convert.ToInt32(dtComCab.Rows[0]["n_iddocven"]);
            e_DiarioH.n_oriidtipdoc = Convert.ToInt16(dtComCab.Rows[0]["n_idtipdocven"]);
            e_DiarioH.n_oriidtipmon = Convert.ToInt16(dtComCab.Rows[0]["n_idmon"]);
            e_DiarioH.c_orinumdoc = dtComCab.Rows[0]["c_numdoc"].ToString();
            e_DiarioH.c_origlo = dtComCab.Rows[0]["c_glosa"].ToString();
            e_DiarioH.c_oridestipmon = dtComCab.Rows[0]["c_destipmon"].ToString();
            e_DiarioH.c_oridestipdoc = dtComCab.Rows[0]["c_destipdoc"].ToString();
            e_DiarioH.c_orinomcli = dtComCab.Rows[0]["c_clinom"].ToString();
            e_DiarioH.c_orinumruc = dtComCab.Rows[0]["c_clinumruc"].ToString();
            l_Diario.Add(e_DiarioH);

            // CUENTA DEBE DEL ASIENTO - IGV DL DOCUMENTO
            if ((Convert.ToDouble(dtComCab.Rows[0]["n_impigvsol"]) != 0) && (Convert.ToDouble(funfunciones.NulosN(dtComCab.Rows[0]["n_impigvdol"])) != 0))
            {
                BE_CON_DIARIO e_DiarioD = new BE_CON_DIARIO();
                e_DiarioD.n_id = 0;
                e_DiarioD.n_idemp = n_IdEmpresa;
                e_DiarioD.n_ano = STU_SISTEMA.ANOTRABAJO;
                e_DiarioD.n_mes = STU_SISTEMA.MESTRABAJO;
                e_DiarioD.n_lib = n_IdLibro;
                e_DiarioD.c_numasi = c_numasi;
                e_DiarioD.n_idcue = Convert.ToInt16(funfunciones.NulosN(dtComCab.Rows[0]["n_idcueigv"]));
                e_DiarioD.n_tc = Convert.ToDouble(dtComCab.Rows[0]["n_tc"]);

                if (Convert.ToInt16(dtComCab.Rows[0]["n_idtipdocven"]) == 8)
                {
                    e_DiarioD.n_impdebsol = Convert.ToDouble(dtComCab.Rows[0]["n_impigvsol"]);
                    e_DiarioD.n_imphabsol = 0;
                    e_DiarioD.n_impdebdol = Convert.ToDouble(dtComCab.Rows[0]["n_impigvdol"]);
                    e_DiarioD.n_imphabdol = 0;
                }
                else
                { 
                    e_DiarioD.n_impdebsol = 0;
                    e_DiarioD.n_imphabsol = Convert.ToDouble(dtComCab.Rows[0]["n_impigvsol"]);
                    e_DiarioD.n_impdebdol = 0;
                    e_DiarioD.n_imphabdol = Convert.ToDouble(dtComCab.Rows[0]["n_impigvdol"]);
                }
                e_DiarioD.d_fchasi = Convert.ToDateTime(dtComCab.Rows[0]["d_fchreg"]);
                e_DiarioD.d_orifchdoc = Convert.ToDateTime(dtComCab.Rows[0]["d_fchdoc"]);
                e_DiarioD.n_oriid = Convert.ToInt32(dtComCab.Rows[0]["n_iddocven"]);
                e_DiarioD.n_oriidtipdoc = Convert.ToInt16(dtComCab.Rows[0]["n_idtipdocven"]);
                e_DiarioD.n_oriidtipmon = Convert.ToInt16(dtComCab.Rows[0]["n_idmon"]);
                e_DiarioD.c_orinumdoc = dtComCab.Rows[0]["c_numdoc"].ToString();
                e_DiarioD.c_origlo = dtComCab.Rows[0]["c_glosa"].ToString();
                e_DiarioD.c_oridestipmon = dtComCab.Rows[0]["c_destipmon"].ToString();
                e_DiarioD.c_oridestipdoc = dtComCab.Rows[0]["c_destipdoc"].ToString();
                e_DiarioD.c_orinomcli = dtComCab.Rows[0]["c_clinom"].ToString();
                e_DiarioD.c_orinumruc = dtComCab.Rows[0]["c_clinumruc"].ToString();
                l_Diario.Add(e_DiarioD);
            }
            // AGREGAMOS EL DEBE DEL DETALLE DE LA COMPRA
            for (n_row = 0; n_row <= dtComDet.Rows.Count - 1; n_row++)
            {
                BE_CON_DIARIO e_DiarioA = new BE_CON_DIARIO();
                e_DiarioA.n_id = 0;
                e_DiarioA.n_idemp = n_IdEmpresa;
                e_DiarioA.n_ano = STU_SISTEMA.ANOTRABAJO;
                e_DiarioA.n_mes = STU_SISTEMA.MESTRABAJO;
                e_DiarioA.n_lib = n_IdLibro;
                e_DiarioA.c_numasi = c_numasi;
                e_DiarioA.n_idcue = Convert.ToInt16(funfunciones.NulosN(dtComDet.Rows[n_row]["n_idcuecon"]));
                e_DiarioA.n_tc = Convert.ToDouble(dtComCab.Rows[0]["n_tc"]);

                if (Convert.ToInt16(dtComCab.Rows[0]["n_idtipdocven"]) == 8)
                {
                    e_DiarioA.n_impdebsol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impsol"]));
                    e_DiarioA.n_imphabsol = 0;
                    e_DiarioA.n_impdebdol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impdol"]));
                    e_DiarioA.n_imphabdol = 0;
                }
                else
                { 
                    e_DiarioA.n_impdebsol = 0;
                    e_DiarioA.n_imphabsol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impsol"]));
                    e_DiarioA.n_impdebdol = 0;
                    e_DiarioA.n_imphabdol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impdol"]));
                }
                e_DiarioA.d_fchasi = Convert.ToDateTime(dtComCab.Rows[0]["d_fchreg"]);
                e_DiarioA.d_orifchdoc = Convert.ToDateTime(dtComCab.Rows[0]["d_fchdoc"]);
                e_DiarioA.n_oriid = Convert.ToInt32(dtComCab.Rows[0]["n_iddocven"]);
                e_DiarioA.n_oriidtipdoc = Convert.ToInt16(dtComCab.Rows[0]["n_idtipdocven"]);
                e_DiarioA.n_oriidtipmon = Convert.ToInt16(dtComCab.Rows[0]["n_idmon"]);
                e_DiarioA.c_orinumdoc = dtComCab.Rows[0]["c_numdoc"].ToString();
                e_DiarioA.c_origlo = dtComCab.Rows[0]["c_glosa"].ToString();
                e_DiarioA.c_oridestipmon = dtComCab.Rows[0]["c_destipmon"].ToString();
                e_DiarioA.c_oridestipdoc = dtComCab.Rows[0]["c_destipdoc"].ToString();
                e_DiarioA.c_orinomcli = dtComCab.Rows[0]["c_clinom"].ToString();
                e_DiarioA.c_orinumruc = dtComCab.Rows[0]["c_clinumruc"].ToString();
                l_Diario.Add(e_DiarioA);
            }
            funDat.b_DesdeOtraCapa = true;
            if (funDat.Insertar(l_Diario) == false)
            {
                b_OcurrioError = funDat.b_OcurrioError;
                c_ErrorMensaje = funDat.c_ErrorMensaje;
                n_ErrorNumber = funDat.n_ErrorNumber;
            }
            b_result = true;
            c_NewNumAsiento = c_numasi;
            return b_result;
        }
        public bool GenerarAsientoRetencion(int n_IdEmpresa, int n_IdRetencion, int n_AnoTrabajo, int n_MesTrabajo, int n_IdLibro, string c_NumAsiento)
        {
            CD_con_diario funDat = new CD_con_diario();
            CD_con_regretencion funReten = new CD_con_regretencion();
            DataTable dtUltAsi = new DataTable();
            DataTable dtComCab = new DataTable();
            DataTable dtComDet = new DataTable();
            bool b_result = false;
            int n_row = 0;
            string c_numasi = "";

            if (c_NumAsiento == "")
            {
                funDat.mysConec = mysConec;
                funDat.ObtenerUltimoAsiento(n_AnoTrabajo, n_MesTrabajo, n_IdLibro, n_IdEmpresa);
                dtUltAsi = funDat.dtLista;
                c_numasi = dtUltAsi.Rows[0]["c_newnumero"].ToString();
            }
            else
            {
                if (c_numasi.Length > 4)
                {
                    c_numasi = c_NumAsiento.Substring(5, 4);
                }
                else
                {
                    c_numasi = c_NumAsiento;
                }

                funDat.mysConec = mysConec;
                funDat.Eliminar(n_IdLibro, n_AnoTrabajo, n_MesTrabajo, c_numasi, n_IdEmpresa);
            }

            funReten.mysConec = mysConec;
            funReten.AsientoCab(n_IdEmpresa, n_IdRetencion);
            dtComCab = funReten.dtLista;
            dtComDet = funReten.dtDetalle;

            l_Diario.Clear();

            // CUENTA HABER DEL ASIENTO - TOTAL DEL DOCUMENTO
            BE_CON_DIARIO e_DiarioH = new BE_CON_DIARIO();
            e_DiarioH.n_id = 0;
            e_DiarioH.n_idemp = n_IdEmpresa;
            e_DiarioH.n_ano = STU_SISTEMA.ANOTRABAJO;
            e_DiarioH.n_mes = STU_SISTEMA.MESTRABAJO;
            e_DiarioH.n_lib = n_IdLibro;
            e_DiarioH.c_numasi = c_numasi;
            e_DiarioH.n_idcue = Convert.ToInt16(funfunciones.NulosN(dtComCab.Rows[0]["n_idcueven"]));
            e_DiarioH.n_tc = Convert.ToDouble(dtComCab.Rows[0]["n_tc"]);
            e_DiarioH.n_impdebsol = 0;
            e_DiarioH.n_imphabsol = Convert.ToDouble(dtComCab.Rows[0]["n_impvensol"]);
            e_DiarioH.n_impdebdol = 0;
            e_DiarioH.n_imphabdol = Convert.ToDouble(funfunciones.NulosN(dtComCab.Rows[0]["n_impvendol"]));
            e_DiarioH.d_fchasi = Convert.ToDateTime(dtComCab.Rows[0]["d_fchreg"]);
            e_DiarioH.d_orifchdoc = Convert.ToDateTime(dtComCab.Rows[0]["d_fchdoc"]);
            e_DiarioH.n_oriid = Convert.ToInt16(dtComCab.Rows[0]["n_iddocven"]);
            e_DiarioH.n_oriidtipdoc = Convert.ToInt16(dtComCab.Rows[0]["n_idtipdocven"]);
            e_DiarioH.n_oriidtipmon = Convert.ToInt16(dtComCab.Rows[0]["n_idmon"]);
            e_DiarioH.c_orinumdoc = dtComCab.Rows[0]["c_numdoc"].ToString();
            e_DiarioH.c_origlo = dtComCab.Rows[0]["c_glosa"].ToString();

            e_DiarioH.c_oridestipmon = dtComCab.Rows[0]["c_destipmon"].ToString();
            e_DiarioH.c_oridestipdoc = dtComCab.Rows[0]["c_destipdoc"].ToString();
            e_DiarioH.c_orinomcli = dtComCab.Rows[0]["c_clinom"].ToString();
            e_DiarioH.c_orinumruc = dtComCab.Rows[0]["c_clinumruc"].ToString();
            l_Diario.Add(e_DiarioH);

            // AGREGAMOS EL DEBE DEL DETALLE DE LA COMPRA
            for (n_row = 0; n_row <= dtComDet.Rows.Count - 1; n_row++)
            {
                BE_CON_DIARIO e_DiarioA = new BE_CON_DIARIO();
                e_DiarioA.n_id = 0;
                e_DiarioA.n_idemp = n_IdEmpresa;
                e_DiarioA.n_ano = STU_SISTEMA.ANOTRABAJO;
                e_DiarioA.n_mes = STU_SISTEMA.MESTRABAJO;
                e_DiarioA.n_lib = n_IdLibro;
                e_DiarioA.c_numasi = c_numasi;
                e_DiarioA.n_idcue = Convert.ToInt16(funfunciones.NulosN(dtComDet.Rows[n_row]["n_idcueven"]));
                e_DiarioA.n_tc = Convert.ToDouble(dtComCab.Rows[0]["n_tc"]);
                e_DiarioA.n_impdebsol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impvensol"]));
                e_DiarioA.n_imphabsol = 0;
                e_DiarioA.n_impdebdol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impvendol"]));
                e_DiarioA.n_imphabdol = 0;
                e_DiarioA.d_fchasi = Convert.ToDateTime(dtComDet.Rows[n_row]["d_fchreg"]);
                e_DiarioA.d_orifchdoc = Convert.ToDateTime(dtComDet.Rows[n_row]["d_fchdoc"]);
                e_DiarioA.n_oriid = Convert.ToInt16(dtComDet.Rows[n_row]["n_iddocven"]);
                e_DiarioA.n_oriidtipdoc = Convert.ToInt16(dtComDet.Rows[n_row]["n_idtipdocven"]);
                e_DiarioA.n_oriidtipmon = Convert.ToInt16(dtComDet.Rows[n_row]["n_idmon"]);
                e_DiarioA.c_orinumdoc = dtComDet.Rows[n_row]["c_numdoc"].ToString();
                e_DiarioA.c_origlo = dtComDet.Rows[n_row]["c_glosa"].ToString();

                e_DiarioA.c_oridestipmon = dtComDet.Rows[n_row]["c_destipmon"].ToString();
                e_DiarioA.c_oridestipdoc = dtComDet.Rows[n_row]["c_destipdoc"].ToString();
                e_DiarioA.c_orinomcli = dtComDet.Rows[n_row]["c_clinom"].ToString();
                e_DiarioA.c_orinumruc = dtComDet.Rows[n_row]["c_clinumruc"].ToString();
                l_Diario.Add(e_DiarioA);
            }
            if (funDat.Insertar(l_Diario) == false)
            {
                b_OcurrioError = funDat.b_OcurrioError;
                c_ErrorMensaje = funDat.c_ErrorMensaje;
                n_ErrorNumber = funDat.n_ErrorNumber;
            }
            b_result = true;
            c_NewNumAsiento = c_numasi;
            return b_result;
        }
        public bool GenerarAsientoPercepcion(int n_IdEmpresa, int n_IdPercepcion, int n_AnoTrabajo, int n_MesTrabajo, int n_IdLibro, string c_NumAsiento, int n_TipoRegistro)
        {
            CD_con_diario funDat = new CD_con_diario();
            CD_con_regpercepcion funPer = new CD_con_regpercepcion();
            DataTable dtUltAsi = new DataTable();
            DataTable dtComCab = new DataTable();
            DataTable dtComDet = new DataTable();
            bool b_result = false;
            int n_row = 0;
            string c_numasi = "";

            if (c_NumAsiento == "")
            {
                funDat.mysConec = mysConec;
                funDat.ObtenerUltimoAsiento(n_AnoTrabajo, n_MesTrabajo, n_IdLibro, n_IdEmpresa);
                dtUltAsi = funDat.dtLista;
                c_numasi = dtUltAsi.Rows[0]["c_newnumero"].ToString();
            }
            else
            {
                if (c_numasi.Length > 4)
                {
                    c_numasi = c_NumAsiento.Substring(5, 4);
                }
                else
                {
                    c_numasi = c_NumAsiento;
                }

                funDat.mysConec = mysConec;
                funDat.Eliminar(n_IdLibro, n_AnoTrabajo, n_MesTrabajo, c_numasi, n_IdEmpresa);
            }

            funPer.mysConec = mysConec;
            funPer.AsientoCab(n_IdEmpresa, n_IdPercepcion, n_TipoRegistro);
            dtComCab = funPer.dtLista;
            dtComDet = funPer.dtDetalle;

            l_Diario.Clear();

            // CUENTA HABER DEL ASIENTO - TOTAL DEL DOCUMENTO
            BE_CON_DIARIO e_DiarioH = new BE_CON_DIARIO();
            e_DiarioH.n_id = 0;
            e_DiarioH.n_idemp = n_IdEmpresa;
            e_DiarioH.n_ano = STU_SISTEMA.ANOTRABAJO;
            e_DiarioH.n_mes = STU_SISTEMA.MESTRABAJO;
            e_DiarioH.n_lib = n_IdLibro;
            e_DiarioH.c_numasi = c_numasi;
            e_DiarioH.n_idcue = Convert.ToInt16(funfunciones.NulosN(dtComCab.Rows[0]["n_idcueven"]));
            e_DiarioH.n_tc = Convert.ToDouble(dtComCab.Rows[0]["n_tc"]);

            if (n_TipoRegistro == 1)
            {
                e_DiarioH.n_impdebsol = 0;
                e_DiarioH.n_imphabsol = Convert.ToDouble(dtComCab.Rows[0]["n_impvensol"]);
                e_DiarioH.n_impdebdol = 0;
                e_DiarioH.n_imphabdol = Convert.ToDouble(funfunciones.NulosN(dtComCab.Rows[0]["n_impvendol"]));
            }
            else
            {
                e_DiarioH.n_impdebsol = Convert.ToDouble(dtComCab.Rows[0]["n_impvensol"]);
                e_DiarioH.n_imphabsol = 0;
                e_DiarioH.n_impdebdol = Convert.ToDouble(funfunciones.NulosN(dtComCab.Rows[0]["n_impvendol"]));
                e_DiarioH.n_imphabdol = 0;                
            }
            e_DiarioH.d_fchasi = Convert.ToDateTime(dtComCab.Rows[0]["d_fchreg"]);
            e_DiarioH.d_orifchdoc = Convert.ToDateTime(dtComCab.Rows[0]["d_fchdoc"]);
            e_DiarioH.n_oriid = Convert.ToInt16(dtComCab.Rows[0]["n_iddocven"]);
            e_DiarioH.n_oriidtipdoc = Convert.ToInt16(dtComCab.Rows[0]["n_idtipdocven"]);
            e_DiarioH.n_oriidtipmon = Convert.ToInt16(dtComCab.Rows[0]["n_idmon"]);
            e_DiarioH.c_orinumdoc = dtComCab.Rows[0]["c_numdoc"].ToString();
            e_DiarioH.c_origlo = dtComCab.Rows[0]["c_glosa"].ToString();
            l_Diario.Add(e_DiarioH);

            // AGREGAMOS EL DEBE DEL DETALLE DE LA COMPRA
            for (n_row = 0; n_row <= dtComDet.Rows.Count - 1; n_row++)
            {
                BE_CON_DIARIO e_DiarioA = new BE_CON_DIARIO();
                e_DiarioA.n_id = 0;
                e_DiarioA.n_idemp = n_IdEmpresa;
                e_DiarioA.n_ano = STU_SISTEMA.ANOTRABAJO;
                e_DiarioA.n_mes = STU_SISTEMA.MESTRABAJO;
                e_DiarioA.n_lib = n_IdLibro;
                e_DiarioA.c_numasi = c_numasi;
                e_DiarioA.n_idcue = Convert.ToInt16(funfunciones.NulosN(dtComDet.Rows[n_row]["n_idcueven"]));
                e_DiarioA.n_tc = Convert.ToDouble(dtComCab.Rows[0]["n_tc"]);
                if (n_TipoRegistro == 1)
                {
                    e_DiarioA.n_impdebsol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impvensol"]));
                    e_DiarioA.n_imphabsol = 0;
                    e_DiarioA.n_impdebdol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impvendol"]));
                    e_DiarioA.n_imphabdol = 0;
                }
                else
                {
                    e_DiarioA.n_impdebsol = 0;
                    e_DiarioA.n_imphabsol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impvensol"]));
                    e_DiarioA.n_impdebdol = 0;
                    e_DiarioA.n_imphabdol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impvendol"]));                    
                }
                e_DiarioA.d_fchasi = Convert.ToDateTime(dtComDet.Rows[n_row]["d_fchreg"]);
                e_DiarioA.d_orifchdoc = Convert.ToDateTime(dtComDet.Rows[n_row]["d_fchdoc"]);
                e_DiarioA.n_oriid = Convert.ToInt16(dtComDet.Rows[n_row]["n_iddocven"]);
                e_DiarioA.n_oriidtipdoc = Convert.ToInt16(dtComDet.Rows[n_row]["n_idtipdocven"]);
                e_DiarioA.n_oriidtipmon = Convert.ToInt16(dtComDet.Rows[n_row]["n_idmon"]);
                e_DiarioA.c_orinumdoc = dtComDet.Rows[n_row]["c_numdoc"].ToString();
                e_DiarioA.c_origlo = dtComDet.Rows[n_row]["c_glosa"].ToString();
                l_Diario.Add(e_DiarioA);
            }
            if (funDat.Insertar(l_Diario) == false)
            {
                b_OcurrioError = funDat.b_OcurrioError;
                c_ErrorMensaje = funDat.c_ErrorMensaje;
                n_ErrorNumber = funDat.n_ErrorNumber;
            }
            b_result = true;
            c_NewNumAsiento = c_numasi;
            return b_result;
        }
        public bool GenerarAsientoDetraccion(int n_IdEmpresa, int n_IdDetraccion, int n_AnoTrabajo, int n_MesTrabajo, int n_IdLibro, string c_NumAsiento)
        {
            CD_con_diario funDat = new CD_con_diario();
            CD_con_regdetracciones funDet = new CD_con_regdetracciones();
            DataTable dtUltAsi = new DataTable();
            DataTable dtComCab = new DataTable();
            DataTable dtComDet = new DataTable();
            bool b_result = false;
            int n_row = 0;
            string c_numasi = "";

            if (c_NumAsiento == "")
            {
                funDat.mysConec = mysConec;
                funDat.ObtenerUltimoAsiento(n_AnoTrabajo, n_MesTrabajo, n_IdLibro, n_IdEmpresa);
                dtUltAsi = funDat.dtLista;
                c_numasi = dtUltAsi.Rows[0]["c_newnumero"].ToString();
            }
            else
            {
                if (c_numasi.Length > 4)
                {
                    c_numasi = c_NumAsiento.Substring(5, 4);
                }
                else
                {
                    c_numasi = c_NumAsiento;
                }

                funDat.mysConec = mysConec;
                funDat.Eliminar(n_IdLibro, n_AnoTrabajo, n_MesTrabajo, c_numasi, n_IdEmpresa);
            }

            funDet.mysConec = mysConec;
            funDet.AsientoCab(n_IdEmpresa, n_IdDetraccion);
            dtComCab = funDet.dtLista;
            dtComDet = funDet.dtDetalle;

            l_Diario.Clear();

            // CUENTA HABER DEL ASIENTO - TOTAL DEL DOCUMENTO
            BE_CON_DIARIO e_DiarioH = new BE_CON_DIARIO();
            e_DiarioH.n_id = 0;
            e_DiarioH.n_idemp = n_IdEmpresa;
            e_DiarioH.n_ano = STU_SISTEMA.ANOTRABAJO;
            e_DiarioH.n_mes = STU_SISTEMA.MESTRABAJO;
            e_DiarioH.n_lib = n_IdLibro;
            e_DiarioH.c_numasi = c_numasi;
            e_DiarioH.n_idcue = Convert.ToInt16(funfunciones.NulosN(dtComCab.Rows[0]["n_idcueven"]));
            e_DiarioH.n_tc = Convert.ToDouble(dtComCab.Rows[0]["n_tc"]);
            e_DiarioH.n_impdebsol = 0;
            e_DiarioH.n_imphabsol = Convert.ToDouble(dtComCab.Rows[0]["n_impvensol"]);
            e_DiarioH.n_impdebdol = 0;
            e_DiarioH.n_imphabdol = Convert.ToDouble(funfunciones.NulosN(dtComCab.Rows[0]["n_impvendol"]));
            e_DiarioH.d_fchasi = Convert.ToDateTime(dtComCab.Rows[0]["d_fchreg"]);
            e_DiarioH.d_orifchdoc = Convert.ToDateTime(dtComCab.Rows[0]["d_fchdoc"]);
            e_DiarioH.n_oriid = Convert.ToInt16(dtComCab.Rows[0]["n_iddocven"]);
            e_DiarioH.n_oriidtipdoc = Convert.ToInt16(dtComCab.Rows[0]["n_idtipdocven"]);
            e_DiarioH.n_oriidtipmon = Convert.ToInt16(dtComCab.Rows[0]["n_idmon"]);
            e_DiarioH.c_orinumdoc = dtComCab.Rows[0]["c_numdoc"].ToString();
            e_DiarioH.c_origlo = dtComCab.Rows[0]["c_glosa"].ToString();
            l_Diario.Add(e_DiarioH);

            // AGREGAMOS EL DEBE DEL DETALLE DE LA COMPRA
            for (n_row = 0; n_row <= dtComDet.Rows.Count - 1; n_row++)
            {
                BE_CON_DIARIO e_DiarioA = new BE_CON_DIARIO();
                e_DiarioA.n_id = 0;
                e_DiarioA.n_idemp = n_IdEmpresa;
                e_DiarioA.n_ano = STU_SISTEMA.ANOTRABAJO;
                e_DiarioA.n_mes = STU_SISTEMA.MESTRABAJO;
                e_DiarioA.n_lib = n_IdLibro;
                e_DiarioA.c_numasi = c_numasi;
                e_DiarioA.n_idcue = Convert.ToInt16(funfunciones.NulosN(dtComDet.Rows[n_row]["n_idcueven"]));
                e_DiarioA.n_tc = Convert.ToDouble(dtComCab.Rows[0]["n_tc"]);
                e_DiarioA.n_impdebsol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impvensol"]));
                e_DiarioA.n_imphabsol = 0;
                e_DiarioA.n_impdebdol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impvendol"]));
                e_DiarioA.n_imphabdol = 0;
                e_DiarioA.d_fchasi = Convert.ToDateTime(dtComDet.Rows[n_row]["d_fchreg"]);
                e_DiarioA.d_orifchdoc = Convert.ToDateTime(dtComDet.Rows[n_row]["d_fchdoc"]);
                e_DiarioA.n_oriid = Convert.ToInt16(dtComDet.Rows[n_row]["n_iddocven"]);
                e_DiarioA.n_oriidtipdoc = Convert.ToInt16(dtComDet.Rows[n_row]["n_idtipdocven"]);
                e_DiarioA.n_oriidtipmon = Convert.ToInt16(dtComDet.Rows[n_row]["n_idmon"]);
                e_DiarioA.c_orinumdoc = dtComDet.Rows[n_row]["c_numdoc"].ToString();
                e_DiarioA.c_origlo = dtComDet.Rows[n_row]["c_glosa"].ToString();
                l_Diario.Add(e_DiarioA);
            }
            if (funDat.Insertar(l_Diario) == false)
            {
                b_OcurrioError = funDat.b_OcurrioError;
                c_ErrorMensaje = funDat.c_ErrorMensaje;
                n_ErrorNumber = funDat.n_ErrorNumber;
            }
            b_result = true;
            c_NewNumAsiento = c_numasi;
            return b_result;
        }
        public bool GenerarAsientoTesoreria(int n_IdEmpresa, int n_IdTesoreria, int n_AnoTrabajo, int n_MesTrabajo, int n_IdLibro, string c_NumAsiento, int n_TipoRegistro)
        {
            CD_con_diario funDat = new CD_con_diario();
            CD_tes_tesoreria funTes = new CD_tes_tesoreria();

            DataTable dtUltAsi = new DataTable();
            DataTable dtComCab = new DataTable();
            DataTable dtComDet = new DataTable();
            bool b_result = false;
            int n_row = 0;
            string c_numasi = "";

            if (c_NumAsiento == "")
            {
                funDat.mysConec = mysConec;
                funDat.ObtenerUltimoAsiento(n_AnoTrabajo, n_MesTrabajo, n_IdLibro, n_IdEmpresa);
                dtUltAsi = funDat.dtLista;
                c_numasi = dtUltAsi.Rows[0]["c_newnumero"].ToString();
            }
            else
            {
                if (c_numasi.Length > 4)
                {
                    c_numasi = c_NumAsiento.Substring(5, 4);
                }
                else
                {
                    c_numasi = c_NumAsiento;
                }

                funDat.mysConec = mysConec;
                funDat.Eliminar(n_IdLibro, n_AnoTrabajo, n_MesTrabajo, c_numasi, n_IdEmpresa);
            }

            funTes.mysConec = mysConec;
            funTes.AsientoCab(n_IdEmpresa, n_IdTesoreria, n_TipoRegistro);
            dtComCab = funTes.DtLista1;
            dtComDet = funTes.DtLista2;

            l_Diario.Clear();

            for (n_row = 0; n_row <= dtComCab.Rows.Count - 1; n_row++)
            {
                // CUENTA HABER DEL ASIENTO - TOTAL DEL DOCUMENTO
                BE_CON_DIARIO e_DiarioH = new BE_CON_DIARIO();
                e_DiarioH.n_id = 0;
                e_DiarioH.n_idemp = n_IdEmpresa;
                e_DiarioH.n_ano = n_AnoTrabajo;
                e_DiarioH.n_mes = n_MesTrabajo;
                e_DiarioH.n_lib = n_IdLibro;
                e_DiarioH.c_numasi = c_numasi;
                e_DiarioH.n_idcue = Convert.ToInt16(funfunciones.NulosN(dtComCab.Rows[n_row]["n_idcueven"]));
                e_DiarioH.n_tc = Convert.ToDouble(dtComCab.Rows[0]["n_tc"]);

                if (dtComCab.Rows[n_row]["c_tip"].ToString() == "D")
                {
                    e_DiarioH.n_impdebsol = Convert.ToDouble(dtComCab.Rows[n_row]["n_impvensol"]);
                    e_DiarioH.n_imphabsol = 0;
                    e_DiarioH.n_impdebdol = Convert.ToDouble(funfunciones.NulosN(dtComCab.Rows[n_row]["n_impvendol"]));
                    e_DiarioH.n_imphabdol = 0;
                }
                else
                {
                    e_DiarioH.n_impdebsol = 0;
                    e_DiarioH.n_imphabsol = Convert.ToDouble(dtComCab.Rows[n_row]["n_impvensol"]);
                    e_DiarioH.n_impdebdol = 0;
                    e_DiarioH.n_imphabdol = Convert.ToDouble(funfunciones.NulosN(dtComCab.Rows[n_row]["n_impvendol"]));
                }
                //if (n_TipoRegistro == 2)
                //{
                //    e_DiarioH.n_impdebsol = 0;
                //    e_DiarioH.n_imphabsol = Convert.ToDouble(dtComCab.Rows[n_row]["n_impvensol"]);
                //    e_DiarioH.n_impdebdol = 0;
                //    e_DiarioH.n_imphabdol = Convert.ToDouble(funfunciones.NulosN(dtComCab.Rows[n_row]["n_impvendol"]));
                //}
                //else
                //{
                //    e_DiarioH.n_impdebsol = Convert.ToDouble(dtComCab.Rows[n_row]["n_impvensol"]);
                //    e_DiarioH.n_imphabsol = 0;
                //    e_DiarioH.n_impdebdol = Convert.ToDouble(funfunciones.NulosN(dtComCab.Rows[n_row]["n_impvendol"]));
                //    e_DiarioH.n_imphabdol = 0;
                //}
                e_DiarioH.d_fchasi = Convert.ToDateTime(dtComCab.Rows[n_row]["d_fchreg"]);
                e_DiarioH.d_orifchdoc = Convert.ToDateTime(dtComCab.Rows[n_row]["d_fchdoc"]);
                e_DiarioH.n_oriid = Convert.ToInt16(dtComCab.Rows[n_row]["n_iddocven"]);

                if (Convert.ToInt16(funfunciones.NulosN(dtComCab.Rows[n_row]["n_idtipdocven"])) != 0)
                {
                    e_DiarioH.n_oriidtipdoc = Convert.ToInt16(dtComCab.Rows[n_row]["n_idtipdocven"]);
                }
                else
                {
                    e_DiarioH.n_oriidtipdoc = 0;
                }
                e_DiarioH.n_oriidtipmon = Convert.ToInt16(dtComCab.Rows[n_row]["n_idmon"]);
                e_DiarioH.c_orinumdoc = dtComCab.Rows[n_row]["c_numdoc"].ToString();
                e_DiarioH.c_origlo = dtComCab.Rows[n_row]["c_glosa"].ToString();

                e_DiarioH.c_oridestipmon = dtComCab.Rows[n_row]["c_destipmon"].ToString();
                e_DiarioH.c_oridestipdoc = dtComCab.Rows[n_row]["c_destipdoc"].ToString();
                e_DiarioH.c_orinomcli = dtComCab.Rows[n_row]["c_clinom"].ToString();
                e_DiarioH.c_orinumruc = dtComCab.Rows[n_row]["c_clinumruc"].ToString();
                l_Diario.Add(e_DiarioH);
            }


            // AGREGAMOS EL DEBE DEL DETALLE DE LA COMPRA
            for (n_row = 0; n_row <= dtComDet.Rows.Count - 1; n_row++)
            {
                BE_CON_DIARIO e_DiarioA = new BE_CON_DIARIO();
                e_DiarioA.n_id = 0;
                e_DiarioA.n_idemp = n_IdEmpresa;
                e_DiarioA.n_ano = n_AnoTrabajo;
                e_DiarioA.n_mes = n_MesTrabajo;
                e_DiarioA.n_lib = n_IdLibro;
                e_DiarioA.c_numasi = c_numasi;
                e_DiarioA.n_idcue = Convert.ToInt16(funfunciones.NulosN(dtComDet.Rows[n_row]["n_idcueven"]));
                e_DiarioA.n_tc = Convert.ToDouble(dtComCab.Rows[0]["n_tc"]);
                if (n_TipoRegistro == 2)
                {
                    if (dtComDet.Rows[n_row]["n_tip"].ToString() == "D")
                    {
                        e_DiarioA.n_impdebsol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impvensol"]));
                        e_DiarioA.n_imphabsol = 0;
                        e_DiarioA.n_impdebdol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impvendol"]));
                        e_DiarioA.n_imphabdol = 0;
                    }
                    else
                    {
                        e_DiarioA.n_impdebsol = 0;
                        e_DiarioA.n_imphabsol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impvensol"]));
                        e_DiarioA.n_impdebdol = 0;
                        e_DiarioA.n_imphabdol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impvendol"]));
                    }
                }
                else
                {
                    e_DiarioA.n_impdebsol = 0;
                    e_DiarioA.n_imphabsol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impvensol"]));
                    e_DiarioA.n_impdebdol = 0;
                    e_DiarioA.n_imphabdol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impvendol"]));
                }
                if (dtComDet.Rows[n_row]["d_fchreg"].ToString() != "")
                {
                    e_DiarioA.d_fchasi = Convert.ToDateTime(dtComDet.Rows[n_row]["d_fchreg"]);
                }
                if (dtComDet.Rows[n_row]["d_fchdoc"].ToString() != "")
                {
                    e_DiarioA.d_orifchdoc = Convert.ToDateTime(dtComDet.Rows[n_row]["d_fchdoc"]);
                }
                e_DiarioA.n_oriid = Convert.ToInt32(funfunciones.NulosN(dtComDet.Rows[n_row]["n_iddocven"]));
                e_DiarioA.n_oriidtipdoc = Convert.ToInt16(funfunciones.NulosN(dtComDet.Rows[n_row]["n_idtipdocven"]));
                e_DiarioA.n_oriidtipmon = Convert.ToInt16(funfunciones.NulosN(dtComDet.Rows[n_row]["n_idmon"]));
                e_DiarioA.c_orinumdoc = dtComDet.Rows[n_row]["c_numdoc"].ToString();
                e_DiarioA.c_origlo = dtComDet.Rows[n_row]["c_glosa"].ToString();

                if (n_TipoRegistro == 2)
                {
                    e_DiarioA.c_oridestipmon = dtComDet.Rows[n_row]["c_destipmon"].ToString();
                    e_DiarioA.c_oridestipdoc = dtComDet.Rows[n_row]["c_destipdoc"].ToString();
                    e_DiarioA.c_orinomcli = dtComDet.Rows[n_row]["c_pronom"].ToString();
                    e_DiarioA.c_orinumruc = dtComDet.Rows[n_row]["c_pronumruc"].ToString();
                }
                else
                {
                    e_DiarioA.c_oridestipmon = dtComDet.Rows[n_row]["c_destipmon"].ToString();
                    e_DiarioA.c_oridestipdoc = dtComDet.Rows[n_row]["c_destipdoc"].ToString();
                    e_DiarioA.c_orinomcli = dtComDet.Rows[n_row]["c_clinom"].ToString();
                    e_DiarioA.c_orinumruc = dtComDet.Rows[n_row]["c_clinumruc"].ToString();
                }
                l_Diario.Add(e_DiarioA);
            }
            if (funDat.Insertar(l_Diario) == false)
            {
                b_OcurrioError = funDat.b_OcurrioError;
                c_ErrorMensaje = funDat.c_ErrorMensaje;
                n_ErrorNumber = funDat.n_ErrorNumber;
            }
            b_result = true;
            c_NewNumAsiento = c_numasi;
            return b_result;

            //CD_con_diario funDat = new CD_con_diario();
            //CD_tes_tesoreria funTes = new CD_tes_tesoreria();

            //DataTable dtUltAsi = new DataTable();
            //DataTable dtComCab = new DataTable();
            //DataTable dtComDet = new DataTable();
            //bool b_result = false;
            //int n_row = 0;
            //string c_numasi = "";

            //if (c_NumAsiento == "")
            //{
            //    funDat.mysConec = mysConec;
            //    funDat.ObtenerUltimoAsiento(n_AnoTrabajo, n_MesTrabajo, n_IdLibro, n_IdEmpresa);
            //    dtUltAsi = funDat.dtLista;
            //    c_numasi = dtUltAsi.Rows[0]["c_newnumero"].ToString();
            //}
            //else
            //{
            //    if (c_numasi.Length > 4)
            //    {
            //        c_numasi = c_NumAsiento.Substring(5, 4);
            //    }
            //    else
            //    {
            //        c_numasi = c_NumAsiento;
            //    }

            //    funDat.mysConec = mysConec;
            //    funDat.Eliminar(n_IdLibro, n_AnoTrabajo, n_MesTrabajo, c_numasi, n_IdEmpresa);
            //}

            //funTes.mysConec = mysConec;
            //funTes.AsientoCab(n_IdEmpresa, n_IdTesoreria, n_TipoRegistro);
            //dtComCab = funTes.DtLista1;
            //dtComDet = funTes.DtLista2;

            //l_Diario.Clear();

            //for (n_row = 0; n_row <= dtComCab.Rows.Count - 1; n_row++)
            //{
            //    // CUENTA HABER DEL ASIENTO - TOTAL DEL DOCUMENTO
            //    BE_CON_DIARIO e_DiarioH = new BE_CON_DIARIO();
            //    e_DiarioH.n_id = 0;
            //    e_DiarioH.n_idemp = n_IdEmpresa;
            //    e_DiarioH.n_ano = n_AnoTrabajo;
            //    e_DiarioH.n_mes = n_MesTrabajo;
            //    e_DiarioH.n_lib = n_IdLibro;
            //    e_DiarioH.c_numasi = c_numasi;
            //    e_DiarioH.n_idcue = Convert.ToInt16(funfunciones.NulosN(dtComCab.Rows[n_row]["n_idcueven"]));
            //    e_DiarioH.n_tc = Convert.ToDouble(dtComCab.Rows[0]["n_tc"]);

            //    if (dtComCab.Rows[n_row]["c_tip"].ToString() == "D")
            //    {
            //        e_DiarioH.n_impdebsol = Convert.ToDouble(dtComCab.Rows[n_row]["n_impvensol"]);
            //        e_DiarioH.n_imphabsol = 0;
            //        e_DiarioH.n_impdebdol = Convert.ToDouble(funfunciones.NulosN(dtComCab.Rows[n_row]["n_impvendol"]));
            //        e_DiarioH.n_imphabdol = 0;
            //    }
            //    else
            //    {
            //        e_DiarioH.n_impdebsol = 0;
            //        e_DiarioH.n_imphabsol = Convert.ToDouble(dtComCab.Rows[n_row]["n_impvensol"]);
            //        e_DiarioH.n_impdebdol = 0;
            //        e_DiarioH.n_imphabdol = Convert.ToDouble(funfunciones.NulosN(dtComCab.Rows[n_row]["n_impvendol"]));
            //    }
            //    //if (n_TipoRegistro == 2)
            //    //{
            //    //    e_DiarioH.n_impdebsol = 0;
            //    //    e_DiarioH.n_imphabsol = Convert.ToDouble(dtComCab.Rows[n_row]["n_impvensol"]);
            //    //    e_DiarioH.n_impdebdol = 0;
            //    //    e_DiarioH.n_imphabdol = Convert.ToDouble(funfunciones.NulosN(dtComCab.Rows[n_row]["n_impvendol"]));
            //    //}
            //    //else
            //    //{
            //    //    e_DiarioH.n_impdebsol = Convert.ToDouble(dtComCab.Rows[n_row]["n_impvensol"]);
            //    //    e_DiarioH.n_imphabsol = 0;
            //    //    e_DiarioH.n_impdebdol = Convert.ToDouble(funfunciones.NulosN(dtComCab.Rows[n_row]["n_impvendol"]));
            //    //    e_DiarioH.n_imphabdol = 0;
            //    //}
            //    e_DiarioH.d_fchasi = Convert.ToDateTime(dtComCab.Rows[n_row]["d_fchreg"]);
            //    e_DiarioH.d_orifchdoc = Convert.ToDateTime(dtComCab.Rows[n_row]["d_fchdoc"]);
            //    e_DiarioH.n_oriid = Convert.ToInt16(dtComCab.Rows[n_row]["n_iddocven"]);

            //    if (Convert.ToInt16(funfunciones.NulosN(dtComCab.Rows[n_row]["n_idtipdocven"])) != 0)
            //    {
            //        e_DiarioH.n_oriidtipdoc = Convert.ToInt16(dtComCab.Rows[n_row]["n_idtipdocven"]);
            //    }
            //    else
            //    {
            //        e_DiarioH.n_oriidtipdoc = 0;
            //    }
            //    e_DiarioH.n_oriidtipmon = Convert.ToInt16(dtComCab.Rows[n_row]["n_idmon"]);
            //    e_DiarioH.c_orinumdoc = dtComCab.Rows[n_row]["c_numdoc"].ToString();
            //    e_DiarioH.c_origlo = dtComCab.Rows[n_row]["c_glosa"].ToString();

            //    e_DiarioH.c_oridestipmon = dtComCab.Rows[n_row]["c_destipmon"].ToString();
            //    e_DiarioH.c_oridestipdoc = dtComCab.Rows[n_row]["c_destipdoc"].ToString();
            //    e_DiarioH.c_orinomcli = dtComCab.Rows[n_row]["c_clinom"].ToString();
            //    e_DiarioH.c_orinumruc = dtComCab.Rows[n_row]["c_clinumruc"].ToString();
            //    l_Diario.Add(e_DiarioH);
            //}


            //// AGREGAMOS EL DEBE DEL DETALLE DE LA COMPRA
            //for (n_row = 0; n_row <= dtComDet.Rows.Count - 1; n_row++)
            //{
            //    BE_CON_DIARIO e_DiarioA = new BE_CON_DIARIO();
            //    e_DiarioA.n_id = 0;
            //    e_DiarioA.n_idemp = n_IdEmpresa;
            //    e_DiarioA.n_ano = n_AnoTrabajo;
            //    e_DiarioA.n_mes = n_MesTrabajo;
            //    e_DiarioA.n_lib = n_IdLibro;
            //    e_DiarioA.c_numasi = c_numasi;
            //    e_DiarioA.n_idcue = Convert.ToInt16(funfunciones.NulosN(dtComDet.Rows[n_row]["n_idcueven"]));
            //    e_DiarioA.n_tc = Convert.ToDouble(dtComCab.Rows[0]["n_tc"]);
            //    if (n_TipoRegistro == 2)
            //    {
            //        if (dtComDet.Rows[n_row]["n_tip"].ToString() == "D")
            //        {
            //            e_DiarioA.n_impdebsol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impvensol"]));
            //            e_DiarioA.n_imphabsol = 0;
            //            e_DiarioA.n_impdebdol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impvendol"]));
            //            e_DiarioA.n_imphabdol = 0;
            //        }
            //        else
            //        {
            //            e_DiarioA.n_impdebsol = 0;
            //            e_DiarioA.n_imphabsol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impvensol"]));
            //            e_DiarioA.n_impdebdol = 0;
            //            e_DiarioA.n_imphabdol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impvendol"]));
            //        }
            //    }
            //    else
            //    {
            //        e_DiarioA.n_impdebsol = 0;
            //        e_DiarioA.n_imphabsol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impvensol"]));
            //        e_DiarioA.n_impdebdol = 0;
            //        e_DiarioA.n_imphabdol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impvendol"]));
            //    }
            //    if (dtComDet.Rows[n_row]["d_fchreg"].ToString() != "")
            //    {
            //        e_DiarioA.d_fchasi = Convert.ToDateTime(dtComDet.Rows[n_row]["d_fchreg"]);
            //    }
            //    if (dtComDet.Rows[n_row]["d_fchdoc"].ToString() != "")
            //    {
            //        e_DiarioA.d_orifchdoc = Convert.ToDateTime(dtComDet.Rows[n_row]["d_fchdoc"]);
            //    }
            //    e_DiarioA.n_oriid = Convert.ToInt32(funfunciones.NulosN(dtComDet.Rows[n_row]["n_iddocven"]));
            //    e_DiarioA.n_oriidtipdoc = Convert.ToInt16(funfunciones.NulosN(dtComDet.Rows[n_row]["n_idtipdocven"]));
            //    e_DiarioA.n_oriidtipmon = Convert.ToInt16(funfunciones.NulosN(dtComDet.Rows[n_row]["n_idmon"]));
            //    e_DiarioA.c_orinumdoc = dtComDet.Rows[n_row]["c_numdoc"].ToString();
            //    e_DiarioA.c_origlo = dtComDet.Rows[n_row]["c_glosa"].ToString();

            //    if (n_TipoRegistro == 2)
            //    {
            //        e_DiarioA.c_oridestipmon = dtComDet.Rows[n_row]["c_destipmon"].ToString();
            //        e_DiarioA.c_oridestipdoc = dtComDet.Rows[n_row]["c_destipdoc"].ToString();
            //        e_DiarioA.c_orinomcli = dtComDet.Rows[n_row]["c_pronom"].ToString();
            //        e_DiarioA.c_orinumruc = dtComDet.Rows[n_row]["c_pronumruc"].ToString();
            //    }
            //    else
            //    {
            //        e_DiarioA.c_oridestipmon = dtComDet.Rows[n_row]["c_destipmon"].ToString();
            //        e_DiarioA.c_oridestipdoc = dtComDet.Rows[n_row]["c_destipdoc"].ToString();
            //        e_DiarioA.c_orinomcli = dtComDet.Rows[n_row]["c_clinom"].ToString();
            //        e_DiarioA.c_orinumruc = dtComDet.Rows[n_row]["c_clinumruc"].ToString();
            //    }
            //    l_Diario.Add(e_DiarioA);
            //}
            //if (funDat.Insertar(l_Diario) == false)
            //{
            //    b_OcurrioError = funDat.b_OcurrioError;
            //    c_ErrorMensaje = funDat.c_ErrorMensaje;
            //    n_ErrorNumber = funDat.n_ErrorNumber;
            //}
            //b_result = true;
            //c_NewNumAsiento = c_numasi;
            //return b_result;
        }
        //public bool GenerarAsientoTesoreria(int n_IdEmpresa, int n_IdTesoreria, int n_AnoTrabajo, int n_MesTrabajo, int n_IdLibro, string c_NumAsiento, int n_TipoRegistro)
        //{
        //    CD_con_diario funDat = new CD_con_diario();
        //    CD_tes_tesoreria funTes = new CD_tes_tesoreria();

        //    DataTable dtUltAsi = new DataTable();
        //    DataTable dtComCab = new DataTable();
        //    DataTable dtComDet = new DataTable();
        //    bool b_result = false;
        //    int n_row = 0;
        //    string c_numasi = "";

        //    if (c_NumAsiento == "")
        //    {
        //        funDat.mysConec = mysConec;
        //        funDat.ObtenerUltimoAsiento(n_AnoTrabajo, n_MesTrabajo, n_IdLibro, n_IdEmpresa);
        //        dtUltAsi = funDat.dtLista;
        //        c_numasi = dtUltAsi.Rows[0]["c_newnumero"].ToString();
        //    }
        //    else
        //    {
        //        if (c_numasi.Length > 4)
        //        {
        //            c_numasi = c_NumAsiento.Substring(5, 4);
        //        }
        //        else
        //        {
        //            c_numasi = c_NumAsiento;
        //        }

        //        funDat.mysConec = mysConec;
        //        funDat.Eliminar(n_IdLibro, n_AnoTrabajo, n_MesTrabajo, c_numasi, n_IdEmpresa);
        //    }

        //    funTes.mysConec = mysConec;
        //    funTes.AsientoCab(n_IdEmpresa, n_IdTesoreria, n_TipoRegistro);
        //    dtComCab = funTes.DtLista1;
        //    dtComDet = funTes.DtLista2;

        //    l_Diario.Clear();

        //    for (n_row = 0; n_row <= dtComCab.Rows.Count - 1; n_row++)
        //    {
        //        // CUENTA HABER DEL ASIENTO - TOTAL DEL DOCUMENTO
        //        BE_CON_DIARIO e_DiarioH = new BE_CON_DIARIO();
        //        e_DiarioH.n_id = 0;
        //        e_DiarioH.n_idemp = n_IdEmpresa;
        //        e_DiarioH.n_ano = n_AnoTrabajo;
        //        e_DiarioH.n_mes = n_MesTrabajo;
        //        e_DiarioH.n_lib = n_IdLibro;
        //        e_DiarioH.c_numasi = c_numasi;
        //        e_DiarioH.n_idcue = Convert.ToInt16(funfunciones.NulosN(dtComCab.Rows[n_row]["n_idcueven"]));
        //        e_DiarioH.n_tc = Convert.ToDouble(dtComCab.Rows[0]["n_tc"]);

        //        if (n_TipoRegistro == 2)
        //        {
        //            e_DiarioH.n_impdebsol = 0;
        //            e_DiarioH.n_imphabsol = Convert.ToDouble(dtComCab.Rows[n_row]["n_impvensol"]);
        //            e_DiarioH.n_impdebdol = 0;
        //            e_DiarioH.n_imphabdol = Convert.ToDouble(funfunciones.NulosN(dtComCab.Rows[n_row]["n_impvendol"]));
        //        }
        //        else
        //        {
        //            e_DiarioH.n_impdebsol = Convert.ToDouble(dtComCab.Rows[n_row]["n_impvensol"]);
        //            e_DiarioH.n_imphabsol = 0;
        //            e_DiarioH.n_impdebdol = Convert.ToDouble(funfunciones.NulosN(dtComCab.Rows[n_row]["n_impvendol"]));
        //            e_DiarioH.n_imphabdol = 0;
        //        }
        //        e_DiarioH.d_fchasi = Convert.ToDateTime(dtComCab.Rows[n_row]["d_fchreg"]);
        //        e_DiarioH.d_orifchdoc = Convert.ToDateTime(dtComCab.Rows[n_row]["d_fchdoc"]);
        //        e_DiarioH.n_oriid = Convert.ToInt16(dtComCab.Rows[n_row]["n_iddocven"]);

        //        if (Convert.ToInt16(funfunciones.NulosN(dtComCab.Rows[n_row]["n_idtipdocven"])) != 0)
        //        {
        //            e_DiarioH.n_oriidtipdoc = Convert.ToInt16(dtComCab.Rows[n_row]["n_idtipdocven"]);
        //        }
        //        else
        //        {
        //            e_DiarioH.n_oriidtipdoc = 0;
        //        }
        //        e_DiarioH.n_oriidtipmon = Convert.ToInt16(dtComCab.Rows[n_row]["n_idmon"]);
        //        e_DiarioH.c_orinumdoc = dtComCab.Rows[n_row]["c_numdoc"].ToString();
        //        e_DiarioH.c_origlo = dtComCab.Rows[n_row]["c_glosa"].ToString();

        //        e_DiarioH.c_oridestipmon = dtComCab.Rows[n_row]["c_destipmon"].ToString();
        //        e_DiarioH.c_oridestipdoc = dtComCab.Rows[n_row]["c_destipdoc"].ToString();
        //        e_DiarioH.c_orinomcli = dtComCab.Rows[n_row]["c_clinom"].ToString();
        //        e_DiarioH.c_orinumruc = dtComCab.Rows[n_row]["c_clinumruc"].ToString();
        //        l_Diario.Add(e_DiarioH);
        //    }


        //    // AGREGAMOS EL DEBE DEL DETALLE DE LA COMPRA
        //    for (n_row = 0; n_row <= dtComDet.Rows.Count - 1; n_row++)
        //    {
        //        BE_CON_DIARIO e_DiarioA = new BE_CON_DIARIO();
        //        e_DiarioA.n_id = 0;
        //        e_DiarioA.n_idemp = n_IdEmpresa;
        //        e_DiarioA.n_ano = n_AnoTrabajo;
        //        e_DiarioA.n_mes = n_MesTrabajo;
        //        e_DiarioA.n_lib = n_IdLibro;
        //        e_DiarioA.c_numasi = c_numasi;
        //        e_DiarioA.n_idcue = Convert.ToInt16(funfunciones.NulosN(dtComDet.Rows[n_row]["n_idcueven"]));
        //        e_DiarioA.n_tc = Convert.ToDouble(dtComCab.Rows[0]["n_tc"]);
        //        if (n_TipoRegistro == 2)
        //        {
        //            if (dtComDet.Rows[n_row]["n_tip"].ToString() == "D")
        //            {
        //                e_DiarioA.n_impdebsol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impvensol"]));
        //                e_DiarioA.n_imphabsol = 0;
        //                e_DiarioA.n_impdebdol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impvendol"]));
        //                e_DiarioA.n_imphabdol = 0;
        //            }
        //            else
        //            {
        //                e_DiarioA.n_impdebsol = 0;
        //                e_DiarioA.n_imphabsol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impvensol"]));
        //                e_DiarioA.n_impdebdol = 0;
        //                e_DiarioA.n_imphabdol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impvendol"]));
        //            }
        //        }
        //        else
        //        {
        //            e_DiarioA.n_impdebsol = 0;
        //            e_DiarioA.n_imphabsol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impvensol"]));
        //            e_DiarioA.n_impdebdol = 0;
        //            e_DiarioA.n_imphabdol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impvendol"]));
        //        }
        //        if (dtComDet.Rows[n_row]["d_fchreg"].ToString() != "")
        //        {
        //            e_DiarioA.d_fchasi = Convert.ToDateTime(dtComDet.Rows[n_row]["d_fchreg"]);
        //        }
        //        if (dtComDet.Rows[n_row]["d_fchdoc"].ToString() != "")
        //        {
        //            e_DiarioA.d_orifchdoc = Convert.ToDateTime(dtComDet.Rows[n_row]["d_fchdoc"]);
        //        }
        //        e_DiarioA.n_oriid = Convert.ToInt32(funfunciones.NulosN(dtComDet.Rows[n_row]["n_iddocven"]));
        //        e_DiarioA.n_oriidtipdoc = Convert.ToInt16(funfunciones.NulosN(dtComDet.Rows[n_row]["n_idtipdocven"]));
        //        e_DiarioA.n_oriidtipmon = Convert.ToInt16(funfunciones.NulosN(dtComDet.Rows[n_row]["n_idmon"]));
        //        e_DiarioA.c_orinumdoc = dtComDet.Rows[n_row]["c_numdoc"].ToString();
        //        e_DiarioA.c_origlo = dtComDet.Rows[n_row]["c_glosa"].ToString();

        //        if (n_TipoRegistro == 2)
        //        {
        //            e_DiarioA.c_oridestipmon = dtComDet.Rows[n_row]["c_destipmon"].ToString();
        //            e_DiarioA.c_oridestipdoc = dtComDet.Rows[n_row]["c_destipdoc"].ToString();
        //            e_DiarioA.c_orinomcli = dtComDet.Rows[n_row]["c_pronom"].ToString();
        //            e_DiarioA.c_orinumruc = dtComDet.Rows[n_row]["c_pronumruc"].ToString();
        //        }
        //        else
        //        {
        //            e_DiarioA.c_oridestipmon = dtComDet.Rows[n_row]["c_destipmon"].ToString();
        //            e_DiarioA.c_oridestipdoc = dtComDet.Rows[n_row]["c_destipdoc"].ToString();
        //            e_DiarioA.c_orinomcli = dtComDet.Rows[n_row]["c_clinom"].ToString();
        //            e_DiarioA.c_orinumruc = dtComDet.Rows[n_row]["c_clinumruc"].ToString();
        //        }
        //        l_Diario.Add(e_DiarioA);
        //    }
        //    if (funDat.Insertar(l_Diario) == false)
        //    {
        //        b_OcurrioError = funDat.b_OcurrioError;
        //        c_ErrorMensaje = funDat.c_ErrorMensaje;
        //        n_ErrorNumber = funDat.n_ErrorNumber;
        //    }
        //    b_result = true;
        //    c_NewNumAsiento = c_numasi;
        //    return b_result;
        //}
        //public bool GenerarAsientoTesoreria(int n_IdEmpresa, int n_IdTesoreria, int n_AnoTrabajo, int n_MesTrabajo, int n_IdLibro, string c_NumAsiento, int n_TipoRegistro)
        //{
        //    CD_con_diario funDat = new CD_con_diario();
        //    CD_tes_tesoreria funTes = new CD_tes_tesoreria();

        //    DataTable dtUltAsi = new DataTable();
        //    DataTable dtComCab = new DataTable();
        //    DataTable dtComDet = new DataTable();
        //    bool b_result = false;
        //    int n_row = 0;
        //    string c_numasi = "";

        //    if (c_NumAsiento == "")
        //    {
        //        funDat.mysConec = mysConec;
        //        funDat.ObtenerUltimoAsiento(n_AnoTrabajo, n_MesTrabajo, n_IdLibro, n_IdEmpresa);
        //        dtUltAsi = funDat.dtLista;
        //        c_numasi = dtUltAsi.Rows[0]["c_newnumero"].ToString();
        //    }
        //    else
        //    {
        //        if (c_numasi.Length > 4)
        //        {
        //            c_numasi = c_NumAsiento.Substring(5, 4);
        //        }
        //        else
        //        {
        //            c_numasi = c_NumAsiento;
        //        }

        //        funDat.mysConec = mysConec;
        //        funDat.Eliminar(n_IdLibro, n_AnoTrabajo, n_MesTrabajo, c_numasi, n_IdEmpresa);
        //    }

        //    funTes.mysConec = mysConec;
        //    funTes.AsientoCab(n_IdEmpresa, n_IdTesoreria, n_TipoRegistro);
        //    dtComCab = funTes.DtLista1;
        //    dtComDet = funTes.DtLista2;

        //    l_Diario.Clear();

        //    for (n_row = 0; n_row <= dtComCab.Rows.Count - 1; n_row++)
        //    { 
        //        // CUENTA HABER DEL ASIENTO - TOTAL DEL DOCUMENTO
        //        BE_CON_DIARIO e_DiarioH = new BE_CON_DIARIO();
        //        e_DiarioH.n_id = 0;
        //        e_DiarioH.n_idemp = n_IdEmpresa;
        //        e_DiarioH.n_ano = n_AnoTrabajo;
        //        e_DiarioH.n_mes = n_MesTrabajo;
        //        e_DiarioH.n_lib = n_IdLibro;
        //        e_DiarioH.c_numasi = c_numasi;
        //        e_DiarioH.n_idcue = Convert.ToInt16(funfunciones.NulosN(dtComCab.Rows[n_row]["n_idcueven"]));
        //        e_DiarioH.n_tc = Convert.ToDouble(dtComCab.Rows[0]["n_tc"]);

        //        if (n_TipoRegistro == 2)
        //        {
        //            e_DiarioH.n_impdebsol = 0;
        //            e_DiarioH.n_imphabsol = Convert.ToDouble(dtComCab.Rows[n_row]["n_impvensol"]);
        //            e_DiarioH.n_impdebdol = 0;
        //            e_DiarioH.n_imphabdol = Convert.ToDouble(funfunciones.NulosN(dtComCab.Rows[n_row]["n_impvendol"]));
        //        }
        //        else
        //        {
        //            e_DiarioH.n_impdebsol = Convert.ToDouble(dtComCab.Rows[n_row]["n_impvensol"]);
        //            e_DiarioH.n_imphabsol = 0;
        //            e_DiarioH.n_impdebdol = Convert.ToDouble(funfunciones.NulosN(dtComCab.Rows[n_row]["n_impvendol"]));
        //            e_DiarioH.n_imphabdol = 0;
        //        }
        //        e_DiarioH.d_fchasi = Convert.ToDateTime(dtComCab.Rows[n_row]["d_fchreg"]);
        //        e_DiarioH.d_orifchdoc = Convert.ToDateTime(dtComCab.Rows[n_row]["d_fchdoc"]);
        //        e_DiarioH.n_oriid = Convert.ToInt16(dtComCab.Rows[n_row]["n_iddocven"]);

        //        if (Convert.ToInt16(funfunciones.NulosN(dtComCab.Rows[n_row]["n_idtipdocven"])) != 0)
        //        {
        //            e_DiarioH.n_oriidtipdoc = Convert.ToInt16(dtComCab.Rows[n_row]["n_idtipdocven"]);
        //        }
        //        else 
        //        {
        //            e_DiarioH.n_oriidtipdoc = 0;
        //        }
        //        e_DiarioH.n_oriidtipmon = Convert.ToInt16(dtComCab.Rows[n_row]["n_idmon"]);
        //        e_DiarioH.c_orinumdoc = dtComCab.Rows[n_row]["c_numdoc"].ToString();
        //        e_DiarioH.c_origlo = dtComCab.Rows[n_row]["c_glosa"].ToString();

        //        e_DiarioH.c_oridestipmon = dtComCab.Rows[n_row]["c_destipmon"].ToString();
        //        e_DiarioH.c_oridestipdoc = dtComCab.Rows[n_row]["c_destipdoc"].ToString();
        //        e_DiarioH.c_orinomcli = dtComCab.Rows[n_row]["c_clinom"].ToString();
        //        e_DiarioH.c_orinumruc = dtComCab.Rows[n_row]["c_clinumruc"].ToString();
        //        l_Diario.Add(e_DiarioH);
        //    }


        //    // AGREGAMOS EL DEBE DEL DETALLE DE LA COMPRA
        //    for (n_row = 0; n_row <= dtComDet.Rows.Count - 1; n_row++)
        //    {
        //        BE_CON_DIARIO e_DiarioA = new BE_CON_DIARIO();
        //        e_DiarioA.n_id = 0;
        //        e_DiarioA.n_idemp = n_IdEmpresa;
        //        e_DiarioA.n_ano = n_AnoTrabajo;
        //        e_DiarioA.n_mes = n_MesTrabajo;
        //        e_DiarioA.n_lib = n_IdLibro;
        //        e_DiarioA.c_numasi = c_numasi;
        //        e_DiarioA.n_idcue = Convert.ToInt16(funfunciones.NulosN(dtComDet.Rows[n_row]["n_idcueven"]));
        //        e_DiarioA.n_tc = Convert.ToDouble(dtComCab.Rows[0]["n_tc"]);
        //        if (n_TipoRegistro == 2)
        //        {
        //            e_DiarioA.n_impdebsol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impvensol"]));
        //            e_DiarioA.n_imphabsol = 0;
        //            e_DiarioA.n_impdebdol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impvendol"]));
        //            e_DiarioA.n_imphabdol = 0;
        //        }
        //        else
        //        {
        //            e_DiarioA.n_impdebsol = 0;
        //            e_DiarioA.n_imphabsol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impvensol"]));
        //            e_DiarioA.n_impdebdol = 0;
        //            e_DiarioA.n_imphabdol = Convert.ToDouble(funfunciones.NulosN(dtComDet.Rows[n_row]["n_impvendol"]));
        //        }
        //        if (dtComDet.Rows[n_row]["d_fchreg"].ToString() != "")
        //        { 
        //            e_DiarioA.d_fchasi = Convert.ToDateTime(dtComDet.Rows[n_row]["d_fchreg"]);
        //        }
        //        if (dtComDet.Rows[n_row]["d_fchdoc"].ToString() != "")
        //        {
        //            e_DiarioA.d_orifchdoc = Convert.ToDateTime(dtComDet.Rows[n_row]["d_fchdoc"]);
        //        }
        //        e_DiarioA.n_oriid = Convert.ToInt32(funfunciones.NulosN(dtComDet.Rows[n_row]["n_iddocven"]));
        //        e_DiarioA.n_oriidtipdoc = Convert.ToInt16(funfunciones.NulosN(dtComDet.Rows[n_row]["n_idtipdocven"]));
        //        e_DiarioA.n_oriidtipmon = Convert.ToInt16(funfunciones.NulosN(dtComDet.Rows[n_row]["n_idmon"]));
        //        e_DiarioA.c_orinumdoc = dtComDet.Rows[n_row]["c_numdoc"].ToString();
        //        e_DiarioA.c_origlo = dtComDet.Rows[n_row]["c_glosa"].ToString();

        //        if (n_TipoRegistro == 2)
        //        {
        //            e_DiarioA.c_oridestipmon = dtComDet.Rows[n_row]["c_destipmon"].ToString();
        //            e_DiarioA.c_oridestipdoc = dtComDet.Rows[n_row]["c_destipdoc"].ToString();
        //            e_DiarioA.c_orinomcli = dtComDet.Rows[n_row]["c_pronom"].ToString();
        //            e_DiarioA.c_orinumruc = dtComDet.Rows[n_row]["c_pronumruc"].ToString();
        //        }
        //        else
        //        {
        //            e_DiarioA.c_oridestipmon = dtComDet.Rows[n_row]["c_destipmon"].ToString();
        //            e_DiarioA.c_oridestipdoc = dtComDet.Rows[n_row]["c_destipdoc"].ToString();
        //            e_DiarioA.c_orinomcli = dtComDet.Rows[n_row]["c_clinom"].ToString();
        //            e_DiarioA.c_orinumruc = dtComDet.Rows[n_row]["c_clinumruc"].ToString();
        //        }
        //        l_Diario.Add(e_DiarioA);
        //    }
        //    if (funDat.Insertar(l_Diario) == false)
        //    {
        //        b_OcurrioError = funDat.b_OcurrioError;
        //        c_ErrorMensaje = funDat.c_ErrorMensaje;
        //        n_ErrorNumber = funDat.n_ErrorNumber;
        //    }
        //    b_result = true;
        //    c_NewNumAsiento = c_numasi;
        //    return b_result;
        //}
        public bool GenerarAsientoDiversos(string c_NumAsiento, BE_CON_PROVICIONES e_Proviciones, List<BE_CON_PROVICIONESDET> l_ProvicionesDet)
        {
            CD_con_diario funDat = new CD_con_diario();
            CD_vta_ventas funVentas = new CD_vta_ventas();
            DataTable dtUltAsi = new DataTable();
            DataTable dtComCab = new DataTable();
            DataTable dtComDet = new DataTable();
            bool b_result = false;
            int n_row = 0;
            string c_numasi = "";

            if (c_NumAsiento == "")
            {
                funDat.mysConec = mysConec;
                funDat.ObtenerUltimoAsiento(e_Proviciones.n_ano, e_Proviciones.n_mes, e_Proviciones.n_idlib, e_Proviciones.n_idemp);
                if (funDat.b_OcurrioError == true)
                { 
                    b_OcurrioError = funDat.b_OcurrioError;
                    c_ErrorMensaje = funDat.c_ErrorMensaje;
                    n_ErrorNumber = funDat.n_ErrorNumber;
                    return b_result;
                }
                dtUltAsi = funDat.dtLista;
                c_numasi = dtUltAsi.Rows[0]["c_newnumero"].ToString();
            }
            else
            {
                if (c_numasi.Length > 4)
                {
                    c_numasi = c_NumAsiento.Substring(5, 4);
                }
                else
                {
                    c_numasi = c_NumAsiento;
                }

                funDat.mysConec = mysConec;
                //funDat.Eliminar(e_Proviciones.n_idlib, e_Proviciones.n_ano, e_Proviciones.n_mes, c_numasi, e_Proviciones.n_idemp);
            }

            l_Diario.Clear();

            // CUENTA HABER DEL ASIENTO - TOTAL DEL DOCUMENTO
            for (n_row = 0; n_row <= l_ProvicionesDet.Count - 1; n_row++)
            { 
                BE_CON_DIARIO e_DiarioH = new BE_CON_DIARIO();
                e_DiarioH.n_id = 0;
                e_DiarioH.n_idemp = e_Proviciones.n_idemp;
                e_DiarioH.n_ano = e_Proviciones.n_ano;
                e_DiarioH.n_mes = e_Proviciones.n_mes;
                e_DiarioH.n_lib = e_Proviciones.n_idlib;
                e_DiarioH.c_numasi = c_numasi;
                e_DiarioH.n_idcue = l_ProvicionesDet[n_row].n_idcuecon;
                e_DiarioH.n_tc = e_Proviciones.n_tc;
                if (l_ProvicionesDet[n_row].n_tipo == 1)
                {
                    e_DiarioH.n_impdebsol = l_ProvicionesDet[n_row].n_impsol;
                    e_DiarioH.n_imphabsol = 0;
                    e_DiarioH.n_impdebdol = l_ProvicionesDet[n_row].n_impdol;
                    e_DiarioH.n_imphabdol = 0;
                }
                else
                {
                    e_DiarioH.n_impdebsol = 0;
                    e_DiarioH.n_imphabsol = l_ProvicionesDet[n_row].n_impsol;
                    e_DiarioH.n_impdebdol = 0;
                    e_DiarioH.n_imphabdol = l_ProvicionesDet[n_row].n_impdol;
                }
                e_DiarioH.d_fchasi = e_Proviciones.d_fchdoc;
                e_DiarioH.d_orifchdoc = e_Proviciones.d_fchdoc; 
                e_DiarioH.n_oriid = 0;
                e_DiarioH.n_oriidtipdoc = e_Proviciones.n_idtipdoc;
                e_DiarioH.n_oriidtipmon = e_Proviciones.n_idmon;
                e_DiarioH.c_orinumdoc = e_Proviciones.c_numser + "-" + e_Proviciones.c_numdoc;
                e_DiarioH.c_origlo = e_Proviciones.c_glosa;
                
                e_DiarioH.c_oridestipmon = "";
                e_DiarioH.c_oridestipdoc = "";
                e_DiarioH.c_orinomcli = "";
                e_DiarioH.c_orinumruc = "";
                l_Diario.Add(e_DiarioH);
            }
            
            b_result = true;
            c_NewNumAsiento = c_numasi;
            return b_result;
        }
        public bool RegistroCompras(int n_AnoTrabajo, int n_MesTrabajo, int n_IdLibro, int n_IdEmpresa)
        {
            bool b_result = false;
            CD_con_diario miFun = new CD_con_diario();
            miFun.mysConec = mysConec;

            miFun.RegistroCompras(n_AnoTrabajo, n_MesTrabajo, n_IdLibro, n_IdEmpresa);
            dtLista = miFun.dtLista;
            dtResumen = miFun.dtResumen;
            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_result;
            }
            b_result = true;
            return b_result;
        }
        public bool RegistroHonorarios(int n_AnoTrabajo, int n_MesTrabajo, int n_IdLibro, int n_IdEmpresa)
        {
            bool b_result = false;
            CD_con_diario miFun = new CD_con_diario();
            miFun.mysConec = mysConec;

            miFun.RegistroHonorarios(n_AnoTrabajo, n_MesTrabajo, n_IdLibro, n_IdEmpresa);
            dtLista = miFun.dtLista;
            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_result;
            }
            b_result = true;
            return b_result;
        }
        public bool RegistroVentas(int n_AnoTrabajo, int n_MesTrabajo, int n_IdLibro, int n_IdEmpresa)
        {
            bool b_result = false;
            CD_con_diario miFun = new CD_con_diario();
            miFun.mysConec = mysConec;

            miFun.RegistroVentas(n_AnoTrabajo, n_MesTrabajo, n_IdLibro, n_IdEmpresa);
            dtLista = miFun.dtLista;
            dtResumen = miFun.dtResumen;
            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_result;
            }
            b_result = true;
            return b_result;
        }
        public bool ConsultaAsiento(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo, int n_IdLibro, string c_NumeroAsiento)
        {
            bool b_result = false;
            CD_con_diario miFun = new CD_con_diario();
            miFun.mysConec = mysConec;

            miFun.ConsultaAsiento(n_IdEmpresa, n_AnoTrabajo, n_MesTrabajo, n_IdLibro, c_NumeroAsiento);
            dtLista = miFun.dtLista;
            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_result;
            }
            b_result = true;
            return b_result;
        }
        public bool ConsultaDiario(int n_IdEmpresa, int n_AnoTrabajo, int n_IdLibro, int n_PeriodoInicio, int n_PeriodoFinal)
        {
            bool b_result = false;
            CD_con_diario miFun = new CD_con_diario();
            miFun.mysConec = mysConec;

            miFun.ConsultaDiario(n_IdEmpresa, n_AnoTrabajo, n_IdLibro, n_PeriodoInicio, n_PeriodoFinal);
            dtLista = miFun.dtLista;
            dtResumen = miFun.dtResumen;
            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_result;
            }
            b_result = true;
            return b_result;
        }
        public bool BalanceComprobacion(int n_IdEmpresa, int n_AnoTrabajo, int n_PeriodoInicio, int n_PeriodoFinal, int n_IdLibro)
        {
            bool b_result = false;
            CD_con_diario miFun = new CD_con_diario();
            miFun.mysConec = mysConec;

            miFun.BalanceComprobacion(n_IdEmpresa, n_AnoTrabajo, n_PeriodoInicio, n_PeriodoFinal, n_IdLibro);
            dtLista = miFun.dtLista;
            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_result;
            }
            b_result = true;
            return b_result;
        }
        public bool BalanceDetalle(int n_IdEmpresa, int n_AnoTrabajo, int n_PeriodoInicio, int n_PeriodoFinal, int n_IdCuentaContable)
        {
            bool b_result = false;
            CD_con_diario miFun = new CD_con_diario();
            miFun.mysConec = mysConec;

            miFun.BalanceDetalle(n_IdEmpresa, n_AnoTrabajo, n_PeriodoInicio, n_PeriodoFinal, n_IdCuentaContable);
            dtLista = miFun.dtLista;
            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_result;
            }
            b_result = true;
            return b_result;
        }
        public bool ConsultaMayor(int n_IdEmpresa, int n_AnoTrabajo, int n_PeriodoInicio, int n_PeriodoFinal, string c_CadenaIN)
        {
            bool b_result = false;
            CD_con_diario miFun = new CD_con_diario();
            miFun.mysConec = mysConec;

            miFun.ConsultaMayor(n_IdEmpresa, n_AnoTrabajo, n_PeriodoInicio, n_PeriodoFinal, c_CadenaIN);
            dtLista = miFun.dtLista;
            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_result;
            }
            b_result = true;
            return b_result;
        }
        public bool VerDescuadrados(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo, int n_IdLibro)
        {
            bool b_result = false;
            CD_con_diario miFun = new CD_con_diario();
            Genericas funDatos = new Genericas();
            string[,] arrCabeceraFlexFil = new string[7, 5];
            miFun.mysConec = mysConec;

             // FLEX GRID DE LOS TAREAS
            arrCabeceraFlexFil[0, 0] = "Nº Asiento";
            arrCabeceraFlexFil[0, 1] = "60";
            arrCabeceraFlexFil[0, 2] = "C";
            arrCabeceraFlexFil[0, 3] = "";
            arrCabeceraFlexFil[0, 4] = "c_numasi";

            arrCabeceraFlexFil[1, 0] = "Debe S/.";
            arrCabeceraFlexFil[1, 1] = "70";
            arrCabeceraFlexFil[1, 2] = "D";
            arrCabeceraFlexFil[1, 3] = "0.00";
            arrCabeceraFlexFil[1, 4] = "n_impdebsol";

            arrCabeceraFlexFil[2, 0] = "Haber S/.";
            arrCabeceraFlexFil[2, 1] = "70";
            arrCabeceraFlexFil[2, 2] = "D";
            arrCabeceraFlexFil[2, 3] = "0.00";
            arrCabeceraFlexFil[2, 4] = "n_imphabsol";

            arrCabeceraFlexFil[3, 0] = "Diferencia S/.";
            arrCabeceraFlexFil[3, 1] = "70";
            arrCabeceraFlexFil[3, 2] = "D";
            arrCabeceraFlexFil[3, 3] = "0.00";
            arrCabeceraFlexFil[3, 4] = "n_difsol";

            arrCabeceraFlexFil[4, 0] = "Debe US $";
            arrCabeceraFlexFil[4, 1] = "70";
            arrCabeceraFlexFil[4, 2] = "D";
            arrCabeceraFlexFil[4, 3] = "0.00";
            arrCabeceraFlexFil[4, 4] = "n_impdebdol";

            arrCabeceraFlexFil[5, 0] = "Haber US $";
            arrCabeceraFlexFil[5, 1] = "70";
            arrCabeceraFlexFil[5, 2] = "D";
            arrCabeceraFlexFil[5, 3] = "0.00";
            arrCabeceraFlexFil[5, 4] = "n_impdebdol";

            arrCabeceraFlexFil[6, 0] = "Diferencia US $";
            arrCabeceraFlexFil[6, 1] = "70";
            arrCabeceraFlexFil[6, 2] = "D";
            arrCabeceraFlexFil[6, 3] = "0.00";
            arrCabeceraFlexFil[6, 4] = "n_difdol";

            miFun.MostrarDescuadrados(n_IdEmpresa, n_AnoTrabajo, n_MesTrabajo, n_IdLibro);
            
            if (miFun.b_OcurrioError == true)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
                return b_result;
            }
            else
            {
                dtLista = miFun.dtLista;

                funDatos.MostrarDatos(arrCabeceraFlexFil, dtLista);
            }
            b_result = true;
            return b_result;
        }
    }
}
