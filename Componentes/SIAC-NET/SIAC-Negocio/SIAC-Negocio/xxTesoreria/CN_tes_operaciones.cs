using MySql.Data.MySqlClient;
using SIAC_DATOS.Almacen;
using SIAC_DATOS.Tesoreria;
using SIAC_DATOS.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using SIAC_Entidades.Tesoreria;


namespace SIAC_Negocio.Tesoreria
{
    public class CN_tes_operaciones
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();

        public DataTable dtLista = new DataTable();
        public BE_TES_OPERACIONES e_Operacioones = new BE_TES_OPERACIONES();
        public List<BE_TES_OPERACIONESORI> l_OperacionesOri = new List<BE_TES_OPERACIONESORI>();
        public List<BE_TES_OPERACIONESORIDET> l_OperacioonesOriDet = new List<BE_TES_OPERACIONESORIDET>();
        public List<BE_TES_OPERACIONESORI> l_OperacionesDes = new List<BE_TES_OPERACIONESORI>();
        public List<BE_TES_OPERACIONESORIDET> l_OperacioonesDesDet = new List<BE_TES_OPERACIONESORIDET>();

        public void Listar(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrasbajo)
        {
            DataTable dtResul = new DataTable();

            CD_tes_operaciones miFun = new CD_tes_operaciones();
            miFun.mysConec = mysConec;

            miFun.Listar(n_IdEmpresa, n_AnoTrabajo, n_MesTrasbajo);
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
            CD_tes_operaciones miFun = new CD_tes_operaciones();
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
                e_Operacioones.n_idemp = Convert.ToInt16(dtResult.Rows[0]["n_idemp"]);
                e_Operacioones.n_id = Convert.ToInt16(dtResult.Rows[0]["n_id"]);
                e_Operacioones.n_idtipmov = Convert.ToInt16(dtResult.Rows[0]["n_idtipmov"]);
                e_Operacioones.d_fchope = Convert.ToDateTime(dtResult.Rows[0]["d_fchope"]);
                e_Operacioones.d_fchreg = Convert.ToDateTime(dtResult.Rows[0]["d_fchreg"]);
                e_Operacioones.c_numreg = dtResult.Rows[0]["c_numreg"].ToString();
                e_Operacioones.n_idmon = Convert.ToInt16(dtResult.Rows[0]["n_idmon"]);
                e_Operacioones.n_importe = Convert.ToDouble(dtResult.Rows[0]["n_importe"]);
                e_Operacioones.c_glosa = dtResult.Rows[0]["c_glosa"].ToString();
                e_Operacioones.n_conciliado = Convert.ToInt16(dtResult.Rows[0]["n_idmon"]);
                e_Operacioones.n_anotra = Convert.ToInt16(dtResult.Rows[0]["n_anotra"]);
                e_Operacioones.n_mestra = Convert.ToInt16(dtResult.Rows[0]["n_mestra"]);

                // CARGAMOS LA LISTA DE ORIGENES
                for (n_row = 0; n_row <= dtResultOri.Rows.Count - 1; n_row++)
                {
                    BE_TES_OPERACIONESORI e_OperacionesOri = new BE_TES_OPERACIONESORI();

                    e_OperacionesOri.n_idope = Convert.ToInt32(dtResultOri.Rows[0]["n_idope"]);
                    e_OperacionesOri.n_idcuecon = Convert.ToInt16(dtResultOri.Rows[0]["n_idcuecon"]);
                    e_OperacionesOri.n_importe = Convert.ToDouble(dtResultOri.Rows[0]["n_importe"]);
                    e_OperacionesOri.n_idmod = Convert.ToInt16(dtResultOri.Rows[0]["n_idmod"]);
                    e_OperacionesOri.n_idbcocta = Convert.ToInt16(dtResultOri.Rows[0]["n_idbcocta"]);

                    l_OperacionesOri.Add(e_OperacionesOri);
                }

                // CARGAMOS EL DETALLE DE LOS ORIGENES
                for (n_row = 0; n_row <= dtResultOriDet.Rows.Count - 1; n_row++)
                {
                    BE_TES_OPERACIONESORIDET e_OperacioonesOriDet = new BE_TES_OPERACIONESORIDET();

                    e_OperacioonesOriDet.n_idope = Convert.ToInt32(dtResultOriDet.Rows[0]["n_idope"]);
                    e_OperacioonesOriDet.n_idcuecon = Convert.ToInt16(dtResultOriDet.Rows[0]["n_idcuecon"]);
                    e_OperacioonesOriDet.n_idtipper = Convert.ToInt16(dtResultOriDet.Rows[0]["n_idtipper"]);
                    e_OperacioonesOriDet.n_idmod = Convert.ToInt16(dtResultOriDet.Rows[0]["n_idmod"]);
                    e_OperacioonesOriDet.n_iddoc = Convert.ToInt16(dtResultOriDet.Rows[0]["n_iddoc"]);
                    e_OperacioonesOriDet.n_idper = Convert.ToInt16(dtResultOriDet.Rows[0]["n_idper"]);
                    e_OperacioonesOriDet.n_tipdoc = Convert.ToInt16(dtResultOriDet.Rows[0]["n_tipdoc"]);
                    e_OperacioonesOriDet.c_numser = dtResultOriDet.Rows[0]["c_numser"].ToString();
                    e_OperacioonesOriDet.c_numdoc = dtResultOriDet.Rows[0]["c_numdoc"].ToString();
                    e_OperacioonesOriDet.n_importe = Convert.ToDouble(dtResultOriDet.Rows[0]["n_importe"]);
                    e_OperacioonesOriDet.n_saldo = Convert.ToDouble(dtResultOriDet.Rows[0]["n_saldo"]);
                    e_OperacioonesOriDet.n_acuenta = Convert.ToDouble(dtResultOriDet.Rows[0]["n_acuenta"]);
                    e_OperacioonesOriDet.n_idmedpag = Convert.ToInt16(dtResultOriDet.Rows[0]["n_idmedpag"]);
                    e_OperacioonesOriDet.d_fchdoc = Convert.ToDateTime(dtResultOriDet.Rows[0]["d_fchdoc"]);

                    l_OperacioonesOriDet.Add(e_OperacioonesOriDet);
                }

                // CARGAMOS LA LISTA DE DESTINOS
                for (n_row = 0; n_row <= dtResultDes.Rows.Count - 1; n_row++)
                {
                    BE_TES_OPERACIONESORI e_OperacionesDes = new BE_TES_OPERACIONESORI();

                    e_OperacionesDes.n_idope = Convert.ToInt32(dtResultDes.Rows[0]["n_idope"]);
                    e_OperacionesDes.n_idcuecon = Convert.ToInt16(dtResultDes.Rows[0]["n_idcuecon"]);
                    e_OperacionesDes.n_importe = Convert.ToDouble(dtResultDes.Rows[0]["n_importe"]);
                    e_OperacionesDes.n_idmod = Convert.ToInt16(dtResultDes.Rows[0]["n_idmod"]);
                    e_OperacionesDes.n_idbcocta = Convert.ToInt16(dtResultDes.Rows[0]["n_idbcocta"]);

                    l_OperacionesDes.Add(e_OperacionesDes);
                }

                // CARGAMOS EL DETALLE DE LOS DESTINOS
                for (n_row = 0; n_row <= dtResultDesDet.Rows.Count - 1; n_row++)
                {
                    BE_TES_OPERACIONESORIDET e_OperacioonesDesDet = new BE_TES_OPERACIONESORIDET();

                    e_OperacioonesDesDet.n_idope = Convert.ToInt32(dtResultDesDet.Rows[0]["n_idope"]);
                    e_OperacioonesDesDet.n_idcuecon = Convert.ToInt16(dtResultDesDet.Rows[0]["n_idcuecon"]);
                    e_OperacioonesDesDet.n_idtipper = Convert.ToInt16(dtResultDesDet.Rows[0]["n_idtipper"]);
                    e_OperacioonesDesDet.n_idmod = Convert.ToInt16(dtResultDesDet.Rows[0]["n_idmod"]);
                    e_OperacioonesDesDet.n_iddoc = Convert.ToInt16(dtResultDesDet.Rows[0]["n_iddoc"]);
                    e_OperacioonesDesDet.n_idper = Convert.ToInt16(dtResultDesDet.Rows[0]["n_idper"]);
                    e_OperacioonesDesDet.n_tipdoc = Convert.ToInt16(dtResultDesDet.Rows[0]["n_tipdoc"]);
                    e_OperacioonesDesDet.c_numser = dtResultDesDet.Rows[0]["c_numser"].ToString();
                    e_OperacioonesDesDet.c_numdoc = dtResultDesDet.Rows[0]["c_numdoc"].ToString();
                    e_OperacioonesDesDet.n_importe = Convert.ToDouble(dtResultDesDet.Rows[0]["n_importe"]);
                    e_OperacioonesDesDet.n_saldo = Convert.ToDouble(dtResultDesDet.Rows[0]["n_saldo"]);
                    e_OperacioonesDesDet.n_acuenta = Convert.ToDouble(dtResultDesDet.Rows[0]["n_acuenta"]);
                    e_OperacioonesDesDet.n_idmedpag = Convert.ToInt16(dtResultDesDet.Rows[0]["n_idmedpag"]);
                    e_OperacioonesDesDet.d_fchdoc = Convert.ToDateTime(dtResultDesDet.Rows[0]["d_fchdoc"]);

                    l_OperacioonesDesDet.Add(e_OperacioonesDesDet);
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
            CD_tes_operaciones miFun = new CD_tes_operaciones();

            miFun.mysConec = mysConec;

            booResult = miFun.Eliminar(n_IdRegistro);
            if (booResult == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return booResult;
        }
        public bool Insertar(BE_TES_OPERACIONES entOperaciones, List<BE_TES_OPERACIONESORI> lstOperacionesOri, List<BE_TES_OPERACIONESORIDET> lstOperacionesOriDet,
            List<BE_TES_OPERACIONESORI> lstOperacionesDes, List<BE_TES_OPERACIONESORIDET> lstOperacionesDesDet)
        {
            CD_tes_operaciones miFun = new CD_tes_operaciones();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Insertar(entOperaciones, lstOperacionesOri, lstOperacionesOriDet, lstOperacionesDes, lstOperacionesDesDet);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
        public bool Actualizar(BE_TES_OPERACIONES entOperaciones, List<BE_TES_OPERACIONESORI> lstOperacionesOri, List<BE_TES_OPERACIONESORIDET> lstOperacionesOriDet,
            List<BE_TES_OPERACIONESORI> lstOperacionesDes, List<BE_TES_OPERACIONESORIDET> lstOperacionesDesDet)
        {
            CD_tes_operaciones miFun = new CD_tes_operaciones();
            bool booOk = false;

            miFun.mysConec = mysConec;
            booOk = miFun.Actualizar(entOperaciones, lstOperacionesOri, lstOperacionesOriDet, lstOperacionesDes, lstOperacionesDesDet);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
    }
}
