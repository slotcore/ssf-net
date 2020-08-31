using MySql.Data.MySqlClient;
using SIAC_DATOS.Almacen;
using SIAC_DATOS.Sistema;
using SIAC_DATOS.Produccion;
using SIAC_DATOS.Ventas;
using SIAC_Entidades;
using SIAC_Entidades.Almacen;
using SIAC_Objetos.Constantes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Helper;
using System.Data.OleDb;

namespace SIAC_Negocio.Almacen
{
    public class CN_alm_movimientos
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public int n_ItemProducido;

        Helper.Comunes.Funciones xFun = new Helper.Comunes.Funciones();
        Helper.Genericas xFundat = new Helper.Genericas();

        public DataTable Listar(int n_idempresa, int n_idmes, int n_idano, int n_idtipmov, int n_idtipitem)
        {
            DataTable dtResul = new DataTable();
            CD_alm_movimientos miFun = new CD_alm_movimientos();

            miFun.mysConec = mysConec;

            dtResul = miFun.Listar(n_idempresa, n_idmes, n_idano, n_idtipmov, n_idtipitem);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public DataTable Consulta6(int n_IdEmpresa)
        {
            DataTable dtResul = new DataTable();

            CD_alm_movimientos miFun = new CD_alm_movimientos();
            miFun.mysConec = mysConec;

            dtResul = miFun.Consulta6(n_IdEmpresa);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public DataTable Consulta7(string c_CadenaIN)
        {
            DataTable dtResul = new DataTable();

            CD_alm_movimientos miFun = new CD_alm_movimientos();
            miFun.mysConec = mysConec;

            dtResul = miFun.Consulta7(c_CadenaIN);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public DataTable Consulta8(int n_IdEmpresa, int n_IdProveedor)
        {
            DataTable dtResul = new DataTable();

            CD_alm_movimientos miFun = new CD_alm_movimientos();
            miFun.mysConec = mysConec;

            dtResul = miFun.Consulta8(n_IdEmpresa, n_IdProveedor);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public BE_ALM_MOVIMIENTOS_CONSULTA TraerRegistro(Int64 n_IdRegistro)
        {
            BE_ALM_MOVIMIENTOS_CONSULTA EntCabecera = new BE_ALM_MOVIMIENTOS_CONSULTA();
            List<BE_ALM_MOVIMIENTOSDET_CONSULTA> LstDetalle = new List<BE_ALM_MOVIMIENTOSDET_CONSULTA>();
            bool booResult;
            DataTable DtDetalle = new DataTable();
            DataTable DtResultado = new DataTable();

            CD_alm_movimientos miFun = new CD_alm_movimientos();
            miFun.mysConec = mysConec;

            booResult = miFun.TraerRegistro(n_IdRegistro);

            DtDetalle = miFun.dtMovimientoDet;
            DtResultado = miFun.dtMovimiento;

            if (DtDetalle.Rows.Count != 0)
            {
                foreach (DataRow dr in DtDetalle.Rows)
                {
                    BE_ALM_MOVIMIENTOSDET_CONSULTA Detalle = new BE_ALM_MOVIMIENTOSDET_CONSULTA();

                    Detalle.n_idmov = Convert.ToInt32(dr["n_idmov"].ToString());
                    Detalle.n_idite = Convert.ToInt32(dr["n_idite"].ToString());
                    Detalle.n_idpre = Convert.ToInt32(dr["n_idpre"].ToString());
                    Detalle.n_can = Convert.ToDouble(dr["n_can"].ToString());
                    Detalle.n_preuni = Convert.ToDouble(xFun.NulosN(dr["n_preuni"]));
                    Detalle.n_pretot = Convert.ToDouble(xFun.NulosN(dr["n_pretot"]));
                    // Detalle.n_canconmp = Convert.ToDouble(xFun.NulosN(dr["n_conmp"]));
                    Detalle.n_idalm = 0;
                    if (xFun.NulosC(dr["n_idalm"].ToString()) != "")
                    {
                        Detalle.n_idalm = Convert.ToInt32(dr["n_idalm"].ToString());
                    }
                    Detalle.c_numlot = xFun.NulosC(dr["c_numlot"].ToString());
                    Detalle.c_itedes = dr["c_itedes"].ToString();
                    Detalle.c_itepredes = dr["c_itepredes"].ToString();
                    Detalle.c_tipexides = dr["c_tipexides"].ToString();
                    if (xFun.NulosC(dr["d_fchpro"].ToString()) != "")
                    {
                        Detalle.d_fchpro = Convert.ToDateTime(dr["d_fchpro"]);
                    }
                    if (xFun.NulosC(dr["d_fchven"].ToString()) != "")
                    {
                        Detalle.d_fchven = Convert.ToDateTime(dr["d_fchven"]);
                    }

                    Detalle.n_iddep = 0;
                    if (xFun.NulosC(dr["n_iddep"].ToString()) != "")
                    {
                        Detalle.n_iddep = Convert.ToInt32(dr["n_iddep"].ToString());
                    }
                    Detalle.n_idpro = 0;
                    if (xFun.NulosC(dr["n_idpro"].ToString()) != "")
                    {
                        Detalle.n_idpro = Convert.ToInt32(dr["n_idpro"].ToString());
                    }
                    Detalle.n_iddis = 0;
                    if (xFun.NulosC(dr["n_iddis"].ToString()) != "")
                    {
                        Detalle.n_iddis = Convert.ToInt32(dr["n_iddis"].ToString());
                    }
                    Detalle.c_desori = "";
                    if (xFun.NulosC(dr["c_desori"].ToString()) != "")
                    {
                        Detalle.c_desori = xFun.NulosC(dr["c_desori"].ToString());
                    }
                    Detalle.c_marca = "";
                    if (xFun.NulosC(dr["c_marca"].ToString()) != "")
                    {
                        Detalle.c_marca = xFun.NulosC(dr["c_marca"].ToString());
                    }
                    Detalle.h_horing = null;
                    if (xFun.NulosC(dr["h_horing"].ToString()) != "")
                    {
                        Detalle.h_horing = dr["h_horing"].ToString();
                    }
                    Detalle.h_horsal = null;
                    if (xFun.NulosC(dr["h_horsal"].ToString()) != "")
                    {
                        Detalle.h_horsal = dr["h_horsal"].ToString();
                    }
                    Detalle.n_estpro = Convert.ToInt32(xFun.NulosN(dr["n_estpro"]));
                    LstDetalle.Add(Detalle);
                }
            }

            if (DtResultado.Rows.Count != 0)
            {
                EntCabecera.n_id = Convert.ToInt32(DtResultado.Rows[0]["n_id"].ToString());
                EntCabecera.n_idemp = Convert.ToInt32(DtResultado.Rows[0]["n_idemp"].ToString());
                EntCabecera.n_idtipmov = Convert.ToInt32(DtResultado.Rows[0]["n_idtipmov"].ToString());
                EntCabecera.n_idclipro = Convert.ToInt32(DtResultado.Rows[0]["n_idclipro"].ToString());
                EntCabecera.d_fchdoc = Convert.ToDateTime(DtResultado.Rows[0]["d_fchdoc"].ToString());
                EntCabecera.d_fching = Convert.ToDateTime(DtResultado.Rows[0]["d_fching"].ToString());
                EntCabecera.n_idtipdoc = Convert.ToInt32(DtResultado.Rows[0]["n_idtipdoc"].ToString());
                EntCabecera.c_numser = DtResultado.Rows[0]["c_numser"].ToString();
                EntCabecera.c_numdoc = DtResultado.Rows[0]["c_numdoc"].ToString();
                EntCabecera.n_idalm = Convert.ToInt32(DtResultado.Rows[0]["n_idalm"].ToString());
                EntCabecera.n_anotra = Convert.ToInt32(DtResultado.Rows[0]["n_anotra"].ToString());
                EntCabecera.n_idmes = Convert.ToInt32(DtResultado.Rows[0]["n_idmes"].ToString());
                EntCabecera.c_obs = DtResultado.Rows[0]["c_obs"].ToString();
                EntCabecera.n_idtipope = Convert.ToInt32(DtResultado.Rows[0]["n_idtipope"].ToString());
                EntCabecera.n_docrefidtipdoc = Convert.ToInt32(xFun.NulosN(DtResultado.Rows[0]["n_docrefidtipdoc"]));
                EntCabecera.c_docrefnumser = DtResultado.Rows[0]["c_docrefnumser"].ToString();
                EntCabecera.c_docrefnumdoc = DtResultado.Rows[0]["c_docrefnumdoc"].ToString();
                EntCabecera.n_perid = Convert.ToInt32(xFun.NulosN(DtResultado.Rows[0]["n_perid"]));
                EntCabecera.n_docrefiddocref = Convert.ToInt32(xFun.NulosN(DtResultado.Rows[0]["n_docrefiddocref"]));
                EntCabecera.lst_items = LstDetalle;
            }

            return EntCabecera;
        }
        public bool Eliminar(int n_IdRegistro, BE_ALM_MOVIMIENTOS entMovimiento, int n_TipoMovimiento)
        {
            bool booResult = false;
            CD_alm_movimientos miFun = new CD_alm_movimientos();
            miFun.mysConec = mysConec;

            if (miFun.Eliminar(n_IdRegistro) == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
                return booResult;
            }
            booResult = true;
            return booResult;

            //if (booResult == true)
            //{
            //    // ELIMINAMOS LA RELACION CON EL DOCUMENTO DE REFERENCIA
            //    if (entMovimiento.n_docrefidtipdoc == 74)                          // SI ES PARTE DE CONFORMIDAD
            //    {
            //        CD_pro_revision objRev = new CD_pro_revision();
            //        objRev.mysConec = mysConec;
            //        booResult = objRev.ActualizarEstado(entMovimiento.n_docrefiddocref,0);
            //    }
                
            //    if (entMovimiento.n_docrefidtipdoc == 73)                          // SI EL TIPO DE DOCUMENTO ES 73 = PARTE DE PRODUCCION
            //    {
            //        CN_ACC_pro_producciondet objPro = new CN_ACC_pro_producciondet();
            //        objPro.AccConec = AccConec;
            //        booResult = objPro.ActualizarEstadoParteProduccion(Convert.ToInt32(entMovimiento.n_docrefiddocref), n_ItemProducido, 0);
            //        if (booResult == false)
            //        {
            //            booOcurrioError = objPro.booOcurrioError;
            //            StrErrorMensaje = objPro.StrErrorMensaje;
            //            IntErrorNumber = objPro.IntErrorNumber;
            //        }
            //    }
            //    if (entMovimiento.n_docrefidtipdoc == 32)                          // SI EL TIPO DE DOCUMENTO ES 73 = PARTE DE PRODUCCION
            //    {
            //        CD_vta_guias objRev = new CD_vta_guias();
            //        objRev.mysConec = mysConec;

            //        if (n_TipoMovimiento == 1)
            //        {
            //            booResult = objRev.ActualizarSalidaEstado(Convert.ToInt32(entMovimiento.n_docrefiddocref), 1);
            //        }
            //        else 
            //        {
            //            booResult = objRev.ActualizarEntradaEstado(Convert.ToInt32(entMovimiento.n_docrefiddocref), 1);
            //        }

            //        if (booResult == false)
            //        {
            //            booOcurrioError = objRev.booOcurrioError;
            //            StrErrorMensaje = objRev.StrErrorMensaje;
            //            IntErrorNumber = objRev.IntErrorNumber;
            //        }
            //    }
            //    // ELIMINAMOS EN LA BD DE ACCESS
            //    CD_ACC_movimientoAlmacen xFunAcc = new CD_ACC_movimientoAlmacen();
            //    xFunAcc.AccConec = AccConec;
            //    booResult = xFunAcc.Eliminar(n_IdRegistro);
            //    if (booResult == false)
            //    {
            //        booOcurrioError = false;
            //        StrErrorMensaje = xFunAcc.StrErrorMensaje;
            //        IntErrorNumber = xFunAcc.IntErrorNumber;
            //    }
            //}
            //else
            //{
            //    booOcurrioError = false;
            //    StrErrorMensaje = miFun.StrErrorMensaje;
            //    IntErrorNumber = miFun.IntErrorNumber;
            //}
            
        }

        public bool Insertar(BE_ALM_MOVIMIENTOS_CONSULTA entMovimiento, int n_TipoMovimiento)
        {
            bool booOk = false;
            BE_ALM_MOVIMIENTOS entCabecera = new BE_ALM_MOVIMIENTOS();
            List<BE_ALM_MOVIMIENTOSDET> lstDetalle = new List<BE_ALM_MOVIMIENTOSDET>();
            List<BE_ALM_INVENTARIOLOTE> lstLote = new List<BE_ALM_INVENTARIOLOTE>();

            CD_alm_movimientos miFun = new CD_alm_movimientos();
            miFun.mysConec = mysConec;

            entCabecera.n_id = entMovimiento.n_id;
            entCabecera.n_idemp = entMovimiento.n_idemp;
            entCabecera.n_idtipmov = entMovimiento.n_idtipmov;
            entCabecera.n_idclipro = entMovimiento.n_idclipro;
            entCabecera.d_fchdoc = entMovimiento.d_fchdoc;
            entCabecera.d_fching = entMovimiento.d_fching;
            entCabecera.n_idtipdoc = entMovimiento.n_idtipdoc;
            entCabecera.c_numser = entMovimiento.c_numser;
            entCabecera.c_numdoc = entMovimiento.c_numdoc;
            entCabecera.n_idalm = entMovimiento.n_idalm;
            entCabecera.n_anotra = entMovimiento.n_anotra;
            entCabecera.n_idmes = entMovimiento.n_idmes;
            entCabecera.c_obs = entMovimiento.c_obs;
            entCabecera.n_idtipope = entMovimiento.n_idtipope;
            entCabecera.n_tipite = entMovimiento.n_tipite;

            entCabecera.n_docrefidtipdoc = null;
            if (entMovimiento.n_docrefidtipdoc != null)
            { 
                entCabecera.n_docrefidtipdoc = entMovimiento.n_docrefidtipdoc;
            }
            entCabecera.c_docrefnumser = entMovimiento.c_docrefnumser;
            entCabecera.c_docrefnumdoc = entMovimiento.c_docrefnumdoc;

            entCabecera.n_docrefiddocref = null;
            if (entMovimiento.n_docrefiddocref != null)
            {
                entCabecera.n_docrefiddocref = entMovimiento.n_docrefiddocref;
            }

            entCabecera.n_perid = entMovimiento.n_perid;

            foreach (BE_ALM_MOVIMIENTOSDET_CONSULTA element in entMovimiento.lst_items)
            {
                BE_ALM_MOVIMIENTOSDET_CONSULTA entNewDetalle = new BE_ALM_MOVIMIENTOSDET_CONSULTA();
                BE_ALM_INVENTARIOLOTE entNewLote = new BE_ALM_INVENTARIOLOTE();

                entNewDetalle.n_idmov = element.n_idmov;
                entNewDetalle.n_idite = element.n_idite;
                entNewDetalle.n_idpre = element.n_idpre;
                entNewDetalle.n_can = element.n_can;
                entNewDetalle.n_idalm = element.n_idalm;
                entNewDetalle.c_numlot = element.c_numlot;
                entNewDetalle.n_idtippro = element.n_idtippro;

                entNewDetalle.d_fchpro = null;
                if (xFun.NulosC(element.d_fchpro) != "")
                {
                    entNewDetalle.d_fchpro = element.d_fchpro;
                }

                entNewDetalle.d_fchven = null;
                if (xFun.NulosC(element.d_fchven) != "")
                {
                    entNewDetalle.d_fchven = element.d_fchven;
                }
                entNewDetalle.n_iddep = element.n_iddep;
                entNewDetalle.n_idpro = element.n_idpro;
                entNewDetalle.n_iddis = element.n_iddis;
                entNewDetalle.c_desori = element.c_desori;
                entNewDetalle.c_marca = element.c_marca;

                entNewDetalle.n_preuni = element.n_preuni;
                entNewDetalle.n_pretot = element.n_pretot;

                entNewDetalle.h_horing = "";
                if (xFun.NulosC(element.h_horing) != "")
                {
                    entNewDetalle.h_horing = element.h_horing;
                }

                entNewDetalle.h_horsal = "";
                if (xFun.NulosC(element.h_horsal) != "")
                {
                    entNewDetalle.h_horsal = element.h_horsal;
                }
                entNewDetalle.n_estpro = element.n_estpro;
                lstDetalle.Add(entNewDetalle);

                // AGREGAMOS LOS LOTES
                entNewLote.n_idemp = entMovimiento.n_idemp;
                entNewLote.n_idite = element.n_idite;
                entNewLote.n_iddocmov = 0;
                entNewLote.d_fchmov = entMovimiento.d_fching;
                entNewLote.c_numlot = element.c_numlot;
                entNewLote.n_idunimed = 0;

                entNewLote.n_caning = 0;
                entNewLote.n_cansal = 0;
                if (entCabecera.n_idtipmov == 1) { entNewLote.n_caning = element.n_can; }
                if (entCabecera.n_idtipmov == 2) { entNewLote.n_cansal = element.n_can; }
                                
                if (xFun.NulosC(element.d_fchpro) != "")
                {
                    entNewLote.d_fchpro = element.d_fchpro;
                }
                entNewDetalle.d_fchven = null;
                if (xFun.NulosC(element.d_fchven) != "")
                {
                    entNewLote.d_fchven = element.d_fchven;
                }
                
                entNewLote.n_iddep = element.n_iddep;
                entNewLote.n_idpro = element.n_idpro;
                entNewLote.n_iddis = element.n_iddis;
                entNewLote.c_oriite = element.c_desori;

                entNewLote.h_horing = "";
                if (xFun.NulosC(element.h_horing) != "")
                {
                    entNewLote.h_horing = element.h_horing;
                }

                entNewLote.h_horsal = "";
                if (xFun.NulosC(element.h_horsal) != "")
                {
                    entNewLote.h_horsal = element.h_horsal;
                }

                entNewLote.n_estpro = element.n_estpro;
                lstLote.Add(entNewLote);
            }
            if (miFun.Insertar(entCabecera, lstDetalle, lstLote) == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
                return booOk;
            }
            booOk = true;
            return booOk;
        }

        public bool InsertarLista(List<BE_ALM_MOVIMIENTOS_CONSULTA> entMovimientos, int n_TipoMovimiento)
        {
            bool booOk = false;

            CD_alm_movimientos miFun = new CD_alm_movimientos();
            miFun.mysConec = mysConec;

            List<BE_ALM_MOVIMIENTO_GROUP> entCabecera_Groups = new List<BE_ALM_MOVIMIENTO_GROUP>();

            foreach (BE_ALM_MOVIMIENTOS_CONSULTA entMovimiento in entMovimientos)
            {
                BE_ALM_MOVIMIENTOS entCabecera = new BE_ALM_MOVIMIENTOS();
                List<BE_ALM_MOVIMIENTOSDET> lstDetalle = new List<BE_ALM_MOVIMIENTOSDET>();
                List<BE_ALM_INVENTARIOLOTE> lstLote = new List<BE_ALM_INVENTARIOLOTE>();

                entCabecera.n_id = entMovimiento.n_id;
                entCabecera.n_idemp = entMovimiento.n_idemp;
                entCabecera.n_idtipmov = entMovimiento.n_idtipmov;
                entCabecera.n_idclipro = entMovimiento.n_idclipro;
                entCabecera.d_fchdoc = entMovimiento.d_fchdoc;
                entCabecera.d_fching = entMovimiento.d_fching;
                entCabecera.n_idtipdoc = entMovimiento.n_idtipdoc;
                entCabecera.c_numser = entMovimiento.c_numser;
                entCabecera.c_numdoc = entMovimiento.c_numdoc;
                entCabecera.n_idalm = entMovimiento.n_idalm;
                entCabecera.n_anotra = entMovimiento.n_anotra;
                entCabecera.n_idmes = entMovimiento.n_idmes;
                entCabecera.c_obs = entMovimiento.c_obs;
                entCabecera.n_idtipope = entMovimiento.n_idtipope;
                entCabecera.n_tipite = entMovimiento.n_tipite;

                entCabecera.n_docrefidtipdoc = null;
                if (entMovimiento.n_docrefidtipdoc != null)
                {
                    entCabecera.n_docrefidtipdoc = entMovimiento.n_docrefidtipdoc;
                }
                entCabecera.c_docrefnumser = entMovimiento.c_docrefnumser;
                entCabecera.c_docrefnumdoc = entMovimiento.c_docrefnumdoc;

                entCabecera.n_docrefiddocref = null;
                if (entMovimiento.n_docrefiddocref != null)
                {
                    entCabecera.n_docrefiddocref = entMovimiento.n_docrefiddocref;
                }

                entCabecera.n_perid = entMovimiento.n_perid;

                foreach (BE_ALM_MOVIMIENTOSDET_CONSULTA element in entMovimiento.lst_items)
                {
                    BE_ALM_MOVIMIENTOSDET_CONSULTA entNewDetalle = new BE_ALM_MOVIMIENTOSDET_CONSULTA();
                    BE_ALM_INVENTARIOLOTE entNewLote = new BE_ALM_INVENTARIOLOTE();

                    entNewDetalle.n_idmov = element.n_idmov;
                    entNewDetalle.n_idite = element.n_idite;
                    entNewDetalle.n_idpre = element.n_idpre;
                    entNewDetalle.n_can = element.n_can;
                    entNewDetalle.n_idalm = element.n_idalm;
                    entNewDetalle.c_numlot = element.c_numlot;
                    entNewDetalle.n_idtippro = element.n_idtippro;

                    entNewDetalle.d_fchpro = null;
                    if (xFun.NulosC(element.d_fchpro) != "")
                    {
                        entNewDetalle.d_fchpro = element.d_fchpro;
                    }

                    entNewDetalle.d_fchven = null;
                    if (xFun.NulosC(element.d_fchven) != "")
                    {
                        entNewDetalle.d_fchven = element.d_fchven;
                    }
                    entNewDetalle.n_iddep = element.n_iddep;
                    entNewDetalle.n_idpro = element.n_idpro;
                    entNewDetalle.n_iddis = element.n_iddis;
                    entNewDetalle.c_desori = element.c_desori;
                    entNewDetalle.c_marca = element.c_marca;

                    entNewDetalle.n_preuni = element.n_preuni;
                    entNewDetalle.n_pretot = element.n_pretot;

                    entNewDetalle.h_horing = "";
                    if (xFun.NulosC(element.h_horing) != "")
                    {
                        entNewDetalle.h_horing = element.h_horing;
                    }

                    entNewDetalle.h_horsal = "";
                    if (xFun.NulosC(element.h_horsal) != "")
                    {
                        entNewDetalle.h_horsal = element.h_horsal;
                    }
                    entNewDetalle.n_estpro = element.n_estpro;
                    lstDetalle.Add(entNewDetalle);

                    // AGREGAMOS LOS LOTES
                    entNewLote.n_idemp = entMovimiento.n_idemp;
                    entNewLote.n_idite = element.n_idite;
                    entNewLote.n_iddocmov = 0;
                    entNewLote.d_fchmov = entMovimiento.d_fching;
                    entNewLote.c_numlot = element.c_numlot;
                    entNewLote.n_idunimed = 0;

                    entNewLote.n_caning = 0;
                    entNewLote.n_cansal = 0;
                    if (entCabecera.n_idtipmov == 1) { entNewLote.n_caning = element.n_can; }
                    if (entCabecera.n_idtipmov == 2) { entNewLote.n_cansal = element.n_can; }

                    if (xFun.NulosC(element.d_fchpro) != "")
                    {
                        entNewLote.d_fchpro = element.d_fchpro;
                    }
                    entNewDetalle.d_fchven = null;
                    if (xFun.NulosC(element.d_fchven) != "")
                    {
                        entNewLote.d_fchven = element.d_fchven;
                    }

                    entNewLote.n_iddep = element.n_iddep;
                    entNewLote.n_idpro = element.n_idpro;
                    entNewLote.n_iddis = element.n_iddis;
                    entNewLote.c_oriite = element.c_desori;

                    entNewLote.h_horing = "";
                    if (xFun.NulosC(element.h_horing) != "")
                    {
                        entNewLote.h_horing = element.h_horing;
                    }

                    entNewLote.h_horsal = "";
                    if (xFun.NulosC(element.h_horsal) != "")
                    {
                        entNewLote.h_horsal = element.h_horsal;
                    }

                    entNewLote.n_estpro = element.n_estpro;
                    lstLote.Add(entNewLote);
                }

                BE_ALM_MOVIMIENTO_GROUP entCabecera_Group = new BE_ALM_MOVIMIENTO_GROUP();
                entCabecera_Group.entCabecera = entCabecera;
                entCabecera_Group.lstDetalle = lstDetalle;
                entCabecera_Group.lstLote = lstLote;
                entCabecera_Groups.Add(entCabecera_Group);
            }

            if (miFun.InsertarLista(entCabecera_Groups) == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
                return booOk;
            }
            booOk = true;
            return booOk;
        }

        public bool Actualizar(BE_ALM_MOVIMIENTOS_CONSULTA entMovimiento)
        {
            BE_ALM_MOVIMIENTOS entCabecera = new BE_ALM_MOVIMIENTOS();
            List<BE_ALM_MOVIMIENTOSDET> lstDetalle = new List<BE_ALM_MOVIMIENTOSDET>();
            List<BE_ALM_INVENTARIOLOTE> lstLote = new List<BE_ALM_INVENTARIOLOTE>();
            bool booOk = false;
            CD_alm_movimientos miFun = new CD_alm_movimientos();
            miFun.mysConec = mysConec;

            entCabecera.n_id = entMovimiento.n_id;
            entCabecera.n_idemp = entMovimiento.n_idemp;
            entCabecera.n_idtipmov = entMovimiento.n_idtipmov;
            entCabecera.n_idclipro = entMovimiento.n_idclipro;
            entCabecera.d_fchdoc = entMovimiento.d_fchdoc;
            entCabecera.d_fching = entMovimiento.d_fching;
            entCabecera.n_idtipdoc = entMovimiento.n_idtipdoc;
            entCabecera.c_numser = entMovimiento.c_numser;
            entCabecera.c_numdoc = entMovimiento.c_numdoc;
            entCabecera.n_idalm = entMovimiento.n_idalm;
            entCabecera.n_anotra = entMovimiento.n_anotra;
            entCabecera.n_idmes = entMovimiento.n_idmes;
            entCabecera.c_obs = entMovimiento.c_obs;
            entCabecera.n_idtipope = entMovimiento.n_idtipope;
            
            if (entMovimiento.n_docrefidtipdoc != null)
            {
                entCabecera.n_docrefidtipdoc = entMovimiento.n_docrefidtipdoc;
            }
            
            entCabecera.n_tipite = entMovimiento.n_tipite;
            entCabecera.c_docrefnumser = entMovimiento.c_docrefnumser;
            entCabecera.c_docrefnumdoc = entMovimiento.c_docrefnumdoc;
            entCabecera.n_perid = entMovimiento.n_perid;
            entCabecera.n_docrefiddocref = null;
            if (entMovimiento.n_docrefiddocref != null)
            {
                entCabecera.n_docrefiddocref = entMovimiento.n_docrefiddocref;
            }

            foreach (BE_ALM_MOVIMIENTOSDET_CONSULTA element in entMovimiento.lst_items)
            {
                BE_ALM_MOVIMIENTOSDET entNewDetalle = new BE_ALM_MOVIMIENTOSDET();
                BE_ALM_INVENTARIOLOTE entNewLote = new BE_ALM_INVENTARIOLOTE();

                entNewDetalle.n_idmov = entCabecera.n_id;
                entNewDetalle.n_idite = element.n_idite;
                entNewDetalle.n_idpre = element.n_idpre;
                entNewDetalle.n_can = element.n_can;
                entNewDetalle.n_idalm = element.n_idalm;
                entNewDetalle.c_numlot = element.c_numlot;
                entNewDetalle.n_idtippro = element.n_idtippro;
                entNewDetalle.n_preuni = element.n_preuni;
                entNewDetalle.n_pretot = element.n_pretot;

                entNewDetalle.d_fchpro = null;
                if (xFun.NulosC(element.d_fchpro) != "")
                {
                    entNewDetalle.d_fchpro = element.d_fchpro;
                }

                entNewDetalle.d_fchven = null;
                if (xFun.NulosC(element.d_fchven) != "")
                {
                    entNewDetalle.d_fchven = element.d_fchven;
                }
                entNewDetalle.n_iddep = element.n_iddep;
                entNewDetalle.n_idpro = element.n_idpro;
                entNewDetalle.n_iddis = element.n_iddis;
                entNewDetalle.c_desori = element.c_desori;
                entNewDetalle.c_marca = element.c_marca;

                entNewDetalle.h_horing = "";
                if (xFun.NulosC(element.h_horing) != "")
                {
                    entNewDetalle.h_horing = element.h_horing;
                }

                entNewDetalle.h_horsal = "";
                if (xFun.NulosC(element.h_horsal) != "")
                {
                    entNewDetalle.h_horsal = element.h_horsal;
                }
                lstDetalle.Add(entNewDetalle);

                // *******************
                // AGREGAMOS LOS LOTES
                entNewLote.n_idemp = entMovimiento.n_idemp;
                entNewLote.n_idite = element.n_idite;
                entNewLote.n_iddocmov = entCabecera.n_id;
                entNewLote.d_fchmov = entMovimiento.d_fching;
                entNewLote.c_numlot = element.c_numlot;
                entNewLote.n_idunimed = 0;

                if (entCabecera.n_idtipmov == 1)
                {
                    entNewLote.n_caning = element.n_can;
                    entNewLote.n_cansal = 0;
                }
                else 
                {
                    entNewLote.n_caning = 0;
                    entNewLote.n_cansal = element.n_can;
                }
                
                if (xFun.NulosC(element.d_fchpro) != "")
                {
                    entNewLote.d_fchpro = element.d_fchpro;
                }
                entNewLote.d_fchven = null;
                if (xFun.NulosC(element.d_fchven) != "")
                {
                    entNewLote.d_fchven = element.d_fchven;
                }

                entNewLote.n_iddep = element.n_iddep;
                entNewLote.n_idpro = element.n_idpro;
                entNewLote.n_iddis = element.n_iddis;
                entNewLote.c_oriite = element.c_desori;
               
                entNewLote.h_horing = "";
                if (xFun.NulosC(element.h_horing) != "")
                {
                    entNewLote.h_horing = element.h_horing;
                }

                entNewLote.h_horsal = "";
                if (xFun.NulosC(element.h_horsal) != "")
                {
                    entNewLote.h_horsal = element.h_horsal;
                }
                
                lstLote.Add(entNewLote);
            }

            //booOk = miFun.Actualizar(entCabecera, lstDetalle, lstLote);
            if (miFun.Actualizar(entCabecera, lstDetalle, lstLote) == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            booOk = true;
            return booOk;
            //booOk = miFun.booOcurrioError;
            //if (booOk == true)
            //{
            //    // ACTUALIZAMOS LA BD DE ACCESS
            //    CD_ACC_movimientoAlmacen xFunAcc = new CD_ACC_movimientoAlmacen();
            //    CD_sys_personaladjunto objPer = new CD_sys_personaladjunto();
            //    DataTable DtPersonal = new DataTable();
            //    DataTable DtResult = new DataTable();
            //    xFunAcc.c_NomSolicitante = "";

            //    objPer.mysConec = mysConec;
            //    DtPersonal = objPer.Listar(entCabecera.n_idemp);
            //    if (DtPersonal.Rows.Count != 0)
            //    {
            //        DtResult = xFundat.DataTableFiltrar(DtPersonal, "n_id = " + entCabecera.n_perid + "", "n_id");
            //        if (DtResult.Rows.Count != 0)
            //        {
            //            xFunAcc.c_NomSolicitante = DtResult.Rows[0]["c_apenom"].ToString();
            //        }
            //    }

            //    xFunAcc.AccConec = AccConec;
            //    booOk = xFunAcc.Actualizar(entCabecera, lstDetalle);
            //    //booOk = xFunAcc.Insertar(entCabecera, lstDetalle);
            //    if (booOk == false)
            //    {
            //        booOcurrioError = xFunAcc.booOcurrioError;
            //        StrErrorMensaje = xFunAcc.StrErrorMensaje;
            //        IntErrorNumber = xFunAcc.IntErrorNumber;
            //    }
            //}
            //else
            //{
            //    booOcurrioError = miFun.booOcurrioError;
            //    StrErrorMensaje = miFun.StrErrorMensaje;
            //    IntErrorNumber = miFun.IntErrorNumber;
            //}
            //return booOk;
        }

        public bool DocumentoExiste(int n_IdEmpresa, int n_idclipro, int n_IdTipoDocumento, string c_NumSerie, string c_NumDocumento, int n_TipoMovimiento)
        {
            DataTable dtResul = new DataTable();
            bool b_resul = false;

            CD_alm_movimientos miFun = new CD_alm_movimientos();
            miFun.mysConec = mysConec;

            if (miFun.DocumentoExiste(n_IdEmpresa, n_idclipro, n_IdTipoDocumento, c_NumSerie, c_NumDocumento, n_TipoMovimiento) == true)
            {
                dtResul = miFun.dtMovimiento;
                if (dtResul.Rows.Count >=1)
                {
                    //  SI SE ENCONTRO DOCUMENTO
                    b_resul = true;
                }
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return b_resul;
        }

        public bool DocumentoExiste(int n_IdEmpresa, int n_IdTipoDocumento, string c_NumSerie, string c_NumDocumento, int n_TipoMovimiento)
        {
            DataTable dtResul = new DataTable();
            bool b_resul = false;

            CD_alm_movimientos miFun = new CD_alm_movimientos();
            miFun.mysConec = mysConec;

            if (miFun.DocumentoExiste(n_IdEmpresa, n_IdTipoDocumento, c_NumSerie, c_NumDocumento, n_TipoMovimiento) == true)
            {
                dtResul = miFun.dtMovimiento;
                if (dtResul.Rows.Count >= 1)
                {
                    //  SI SE ENCONTRO DOCUMENTO
                    b_resul = true;
                }
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return b_resul;
        }
        public void ReporteMovimientos(string c_fchIni, string c_fchfin, int n_TipMov, int n_IdAlmacen)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[9, 3];

            arrPara[0, 0] = "n_idemp";
            arrPara[0, 1] = "C";
            arrPara[0, 2] = STU_SISTEMA.EMPRESAID.ToString();

            arrPara[1, 0] = "c_fchini";
            arrPara[1, 1] = "C";
            arrPara[1, 2] = c_fchIni;

            arrPara[2, 0] = "c_fchfin";
            arrPara[2, 1] = "C";
            arrPara[2, 2] = c_fchfin;

            arrPara[3, 0] = "c_nomemp";
            arrPara[3, 1] = "C";
            arrPara[3, 2] = STU_SISTEMA.EMPRESANOMBRE;

            arrPara[4, 0] = "c_numruc";
            arrPara[4, 1] = "C";
            arrPara[4, 2] = STU_SISTEMA.EMPRESARUC;

            arrPara[5, 0] = "c_titulo1";
            arrPara[5, 1] = "C";
            arrPara[5, 2] = "MOVIMIENTOS DE EXISTENCIAS";

            arrPara[6, 0] = "c_titulo2";
            arrPara[6, 1] = "C";
            if (n_TipMov == 1)
            {
                arrPara[6, 2] = "INGRESOS X ALMACEN";
            }
            else
            {
                arrPara[6, 2] = "SALIDAS X ALMACEN";
            }

            arrPara[7, 0] = "n_idtipmov";
            arrPara[7, 1] = "N";
            arrPara[7, 2] = n_TipMov.ToString(); ;

            arrPara[8, 0] = "n_idalm";
            arrPara[8, 1] = "N";
            arrPara[8, 2] = n_IdAlmacen.ToString(); ;

            c_NomArchivo = "RptMovimiento.rpt";
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "Almacen\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "ALMACEN - REPORTE MOVIMIENTOS DE ALMACEN";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.VerCrystal();
        }
        public void ReportImprimirNotaSalida(int n_idMovimiento)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[1, 3];

            arrPara[0, 0] = "n_idmov";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = n_idMovimiento.ToString();


            c_NomArchivo = "RptNotaSalida.rpt";
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "Almacen\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "ALMACEN - IMPRESION NOTAS DE SALIDA";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
        public void ReportImprimirNotaIngreso(int n_idMovimiento, int n_Formato)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[1, 3];

            arrPara[0, 0] = "n_idmov";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = n_idMovimiento.ToString();

            if (n_Formato == 1) { c_NomArchivo = "RptNotaIngreso.rpt"; }
            if (n_Formato == 2) { c_NomArchivo = "RptNotaIngreso_FB.rpt"; }
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "Almacen\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "ALMACEN - IMPRESION NOTAS DE INGRESO";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
        public void ReportSaldoPorLotes(int n_IdEmp)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[5, 3];

            arrPara[0, 0] = "n_idemp";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = n_IdEmp.ToString();

            arrPara[1, 0] = "c_nomemp";
            arrPara[1, 1] = "C";
            arrPara[1, 2] = STU_SISTEMA.EMPRESANOMBRE;

            arrPara[2, 0] = "c_numruc";
            arrPara[2, 1] = "C";
            arrPara[2, 2] = STU_SISTEMA.EMPRESARUC;

            arrPara[3, 0] = "c_titulo1";
            arrPara[3, 1] = "C";
            arrPara[3, 2] = "SALDO POR LOTES";

            arrPara[4, 0] = "c_titulo2";
            arrPara[4, 1] = "C";
            arrPara[4, 2] = "";

            c_NomArchivo = "RptLotes.rpt";
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "Almacen\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "ALMACEN - SALDOS POR LOTES";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
        public void ReportSaldoPorLotesDet(int n_IdEmp)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[5, 3];

            arrPara[0, 0] = "n_idemp";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = n_IdEmp.ToString();

            arrPara[1, 0] = "c_nomemp";
            arrPara[1, 1] = "C";
            arrPara[1, 2] = STU_SISTEMA.EMPRESANOMBRE;

            arrPara[2, 0] = "c_numruc";
            arrPara[2, 1] = "C";
            arrPara[2, 2] = STU_SISTEMA.EMPRESARUC;

            arrPara[3, 0] = "c_titulo1";
            arrPara[3, 1] = "C";
            arrPara[3, 2] = "SALDO POR LOTES DETALLADO";

            arrPara[4, 0] = "c_titulo2";
            arrPara[4, 1] = "C";
            arrPara[4, 2] = "";

            c_NomArchivo = "RptLotesDet.rpt";
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "Almacen\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "ALMACEN - SALDOS POR LOTES DETALLADO";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
        //public DataTable VerKardexResumido(int n_IdEmpresa, int n_AnoConsulta, int n_PeriodoConsulta, int n_IdTipoExistencia, int n_IdAlmacen)
        //{
        //    DataTable dtResul = new DataTable();

        //    CD_alm_movimientos miFun = new CD_alm_movimientos();
        //    miFun.mysConec = mysConec;

        //    if (miFun.VerKardexResumido(n_IdEmpresa, n_AnoConsulta, n_PeriodoConsulta, n_IdTipoExistencia, n_IdAlmacen) == true)
        //    {
        //        dtResul = miFun.dtResumenKardex;
        //    }
        //    else
        //    {
        //        booOcurrioError = miFun.booOcurrioError;
        //        StrErrorMensaje = miFun.StrErrorMensaje;
        //        IntErrorNumber = miFun.IntErrorNumber;
        //    }

        //    return dtResul;
        //}
        public DataTable VerKardexResumido(int n_IdEmpresa, int n_AnoConsulta, string c_FechaInicio, string c_FechaFinal, int n_IdTipoExistencia, int n_IdAlmacen)
        {
            DataTable dtResul = new DataTable();

            CD_alm_movimientos miFun = new CD_alm_movimientos();
            miFun.mysConec = mysConec;

            if (miFun.VerKardexResumido(n_IdEmpresa, n_AnoConsulta, c_FechaInicio, c_FechaFinal, n_IdTipoExistencia, n_IdAlmacen) == true)
            {
                dtResul = miFun.dtResumenKardex;
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        //public DataTable VerKardexDetallado(int n_IdEmpresa, string c_ano, string c_mes, int n_iddelproducto, int n_idtipoexistencia, int n_IdAlmacen)
        //{
        //    DataTable dtResul = new DataTable();

        //    CD_alm_movimientos miFun = new CD_alm_movimientos();
        //    miFun.mysConec = mysConec;

        //    if (miFun.VerKardexDetallado(n_IdEmpresa, c_ano, c_mes, n_iddelproducto, n_idtipoexistencia, n_IdAlmacen) == true)
        //    {
        //        dtResul = miFun.dtResumenKardex;
        //    }
        //    else
        //    {
        //        booOcurrioError = miFun.booOcurrioError;
        //        StrErrorMensaje = miFun.StrErrorMensaje;
        //        IntErrorNumber = miFun.IntErrorNumber;
        //    }

        //    return dtResul;
        //}
        public DataTable VerKardexDetallado(int n_IdEmpresa, string c_Ano, string c_FechaInicio, string c_FechaFinal, int n_IdProducto, int n_IdAlmacen)
        {
            DataTable dtResul = new DataTable();

            CD_alm_movimientos miFun = new CD_alm_movimientos();
            miFun.mysConec = mysConec;

            if (miFun.VerKardexDetallado(n_IdEmpresa, c_Ano, c_FechaInicio, c_FechaFinal, n_IdProducto, n_IdAlmacen) == true)
            {
                dtResul = miFun.dtResumenKardex;
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        //public void ReporteKardexResumen(string c_FechaInicio, string c_FechaTermino, int n_TipoExistencia)
        //{
        //    string c_NomArchivo = "";
        //    string c_Ruta = "";
        //    string[,] arrPara = new string[8, 3];

        //    arrPara[0, 0] = "n_idemp";
        //    arrPara[0, 1] = "N";
        //    arrPara[0, 2] = STU_SISTEMA.EMPRESAID.ToString();

        //    arrPara[1, 0] = "c_fchini";
        //    arrPara[1, 1] = "C";
        //    arrPara[1, 2] = c_FechaInicio;

        //    arrPara[2, 0] = "c_fchfin";
        //    arrPara[2, 1] = "C";
        //    arrPara[2, 2] = c_FechaTermino;

        //    arrPara[3, 0] = "n_idtipexi";
        //    arrPara[3, 1] = "N";
        //    arrPara[3, 2] = n_TipoExistencia.ToString();

        //    arrPara[4, 0] = "c_titulo1";
        //    arrPara[4, 1] = "C";
        //    arrPara[4, 2] = "SALDO POR LOTES DETALLADO";

        //    arrPara[5, 0] = "c_titulo2";
        //    arrPara[5, 1] = "C";
        //    arrPara[5, 2] = "";

        //    arrPara[6, 0] = "c_nomemp";
        //    arrPara[6, 1] = "C";
        //    arrPara[6, 2] = STU_SISTEMA.EMPRESANOMBRE;

        //    arrPara[7, 0] = "c_numruc";
        //    arrPara[7, 1] = "C";
        //    arrPara[7, 2] = STU_SISTEMA.EMPRESARUC;


        //    c_NomArchivo = "RptResumenKardex.rpt";
        //    c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "Almacen\\" + c_NomArchivo;

        //    Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
        //    xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
        //    xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
        //    xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
        //    xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
        //    xVisor.b_VisPrev = true;
        //    xVisor.c_Titulo = "ALMACEN - KARDEX RESUMIDO";
        //    xVisor.c_PathRep = c_Ruta;
        //    xVisor.arrParametros = arrPara;
        //    xVisor.VerCrystal();
        //}
        public void ReporteKardexDetalle(string c_AnoTrabajo, string c_MesTrabajo, int n_IdProducto, int n_IdTipoExistencia)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[9, 3];

            arrPara[0, 0] = "n_idemp";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = STU_SISTEMA.EMPRESAID.ToString();

            arrPara[1, 0] = "c_ano";
            arrPara[1, 1] = "C";
            arrPara[1, 2] = c_AnoTrabajo;

            arrPara[2, 0] = "c_mes";
            arrPara[2, 1] = "C";
            arrPara[2, 2] = c_MesTrabajo;

            arrPara[3, 0] = "n_idpro";
            arrPara[3, 1] = "N";
            arrPara[3, 2] = n_IdProducto.ToString();

            arrPara[4, 0] = "n_idtipexi";
            arrPara[4, 1] = "N";
            arrPara[4, 2] = n_IdTipoExistencia.ToString();

            arrPara[5, 0] = "c_titulo1";
            arrPara[5, 1] = "C";
            arrPara[5, 2] = "INGRESO DE INVETARIO PERMANENTE EN UNIDADES FISICAS";

            arrPara[6, 0] = "c_titulo2";
            arrPara[6, 1] = "C";
            arrPara[6, 2] = ".";

            arrPara[7, 0] = "c_nomemp";
            arrPara[7, 1] = "C";
            arrPara[7, 2] = STU_SISTEMA.EMPRESANOMBRE;

            arrPara[8, 0] = "c_numruc";
            arrPara[8, 1] = "C";
            arrPara[8, 2] = STU_SISTEMA.EMPRESARUC;

            c_NomArchivo = "RptKardexDetalle.rpt";
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "Almacen\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "ALMACEN - INGRESO DE INVETARIO PERMANENTE EN UNIDADES FISICAS";
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
        public DataTable VerMovimientosAlmacen(int n_IdEmpresa, int n_IdAlmacen, int n_TipoOperacion, int n_IdTipExistencia, int n_IdItem, int n_TipoMovimiento, string c_FchInicio, string c_FchFin)
        {
            DataTable dtResul = new DataTable();

            CD_alm_movimientos miFun = new CD_alm_movimientos();
            miFun.mysConec = mysConec;

            dtResul = miFun.VerMovimientosAlmacen(n_IdEmpresa, n_IdAlmacen, n_TipoOperacion, n_IdTipExistencia, n_IdItem, n_TipoMovimiento, c_FchInicio, c_FchFin);

            if (dtResul == null)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }

            return dtResul;
        }
        public DataTable BuscarMovimientosMP(int n_IdEmpresa, int n_IdProveedor, string c_DocumentosCargados)
        {
            Helper.Genericas funDatos = new Helper.Genericas();
            string[,] arrCabeceraDg1 = new string[5, 4];
            DataTable dtResult = new DataTable();

            dtResult = Consulta6(n_IdEmpresa);
            //if (n_IdProveedor != 0)
            //{
            //    dtResult = funDatos.DataTableFiltrar(dtResult, "n_idclipro = " + n_IdProveedor.ToString() + "");  // FILTRAMOS LOS DOCUMENTOS DEL PROVEEDOR
            //}

            //if (c_DocumentosCargados != "")
            //{
            //    dtResult = funDatos.DataTableFiltrar(dtResult, "n_id NOT IN (" + c_DocumentosCargados + ")");         // QUITAMOS LOS DOCUMENTOS QUE YA FUERON AGREGADOS
            //}
            dtResult = funDatos.DataTableFiltrar(dtResult, "n_tipite  = 1");         // QUITAMOS LOS DOCUMENTOS QUE YA FUERON AGREGADOS

            arrCabeceraDg1[0, 0] = "Proveedor";
            arrCabeceraDg1[0, 1] = "200";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "c_nombre";

            arrCabeceraDg1[1, 0] = "Tip. Doc.";
            arrCabeceraDg1[1, 1] = "40";
            arrCabeceraDg1[1, 2] = "C";
            arrCabeceraDg1[1, 3] = "c_abr";

            arrCabeceraDg1[2, 0] = "Fch. Documento";
            arrCabeceraDg1[2, 1] = "80";
            arrCabeceraDg1[2, 2] = "F";
            arrCabeceraDg1[2, 3] = "d_fchdoc";

            arrCabeceraDg1[3, 0] = "Nº Documento";
            arrCabeceraDg1[3, 1] = "120";
            arrCabeceraDg1[3, 2] = "F";
            arrCabeceraDg1[3, 3] = "c_numdoc";

            arrCabeceraDg1[4, 0] = "Id";
            arrCabeceraDg1[4, 1] = "0";
            arrCabeceraDg1[4, 2] = "N";
            arrCabeceraDg1[4, 3] = "n_id";

            Genericas xFun = new Genericas();
            xFun.Buscar_CampoBusqueda = "n_id";
            xFun.Buscar_CadFiltro = "";
            dtResult = xFun.Buscar(arrCabeceraDg1, dtResult);

            return dtResult;
        }
        public DataTable BuscarMovimientos(int n_IdEmpresa, int n_IdProveedor, string c_DocumentosCargados)
        {
            Helper.Genericas funDatos = new Helper.Genericas();
            string[,] arrCabeceraDg1 = new string[5, 4];
            DataTable dtResult = new DataTable();

            dtResult = Consulta6(n_IdEmpresa);
            if (n_IdProveedor != 0)
            {
                dtResult = funDatos.DataTableFiltrar(dtResult, "n_idclipro = " + n_IdProveedor.ToString() + "");  // FILTRAMOS LOS DOCUMENTOS DEL PROVEEDOR
            }
            
            if (c_DocumentosCargados != "")
            { 
                dtResult = funDatos.DataTableFiltrar(dtResult, "n_id NOT IN (" + c_DocumentosCargados + ")");         // QUITAMOS LOS DOCUMENTOS QUE YA FUERON AGREGADOS
            }

            arrCabeceraDg1[0, 0] = "Proveedor";
            arrCabeceraDg1[0, 1] = "200";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "c_nombre";

            arrCabeceraDg1[1, 0] = "Tip. Doc.";
            arrCabeceraDg1[1, 1] = "40";
            arrCabeceraDg1[1, 2] = "C";
            arrCabeceraDg1[1, 3] = "c_abr";

            arrCabeceraDg1[2, 0] = "Fch. Documento";
            arrCabeceraDg1[2, 1] = "80";
            arrCabeceraDg1[2, 2] = "F";
            arrCabeceraDg1[2, 3] = "d_fchdoc";

            arrCabeceraDg1[3, 0] = "Nº Documento";
            arrCabeceraDg1[3, 1] = "120";
            arrCabeceraDg1[3, 2] = "F";
            arrCabeceraDg1[3, 3] = "c_numdoc";

            arrCabeceraDg1[4, 0] = "Id";
            arrCabeceraDg1[4, 1] = "0";
            arrCabeceraDg1[4, 2] = "N";
            arrCabeceraDg1[4, 3] = "n_id";

            Genericas xFun = new Genericas();
            xFun.Buscar_CampoBusqueda = "n_id";
            xFun.Buscar_CadFiltro = "";
            dtResult = xFun.Buscar(arrCabeceraDg1, dtResult);

            return dtResult;
        }
        public DataTable BuscarMovimientosProcesados(int n_IdEmpresa, int n_IdProveedor, string c_DocumentosCargados)
        {
            Helper.Genericas funDatos = new Helper.Genericas();
            string[,] arrCabeceraDg1 = new string[5, 4];
            DataTable dtResult = new DataTable();
            CD_alm_movimientos o_mov = new CD_alm_movimientos();
            o_mov.mysConec = mysConec;
            dtResult = o_mov.Consulta11(n_IdEmpresa, n_IdProveedor);
            if (n_IdProveedor != 0)
            {
                dtResult = funDatos.DataTableFiltrar(dtResult, "n_idclipro = " + n_IdProveedor.ToString() + "");  // FILTRAMOS LOS DOCUMENTOS DEL PROVEEDOR
            }

            if (c_DocumentosCargados != "")
            {
                dtResult = funDatos.DataTableFiltrar(dtResult, "n_id NOT IN (" + c_DocumentosCargados + ")");         // QUITAMOS LOS DOCUMENTOS QUE YA FUERON AGREGADOS
            }

            arrCabeceraDg1[0, 0] = "Proveedor";
            arrCabeceraDg1[0, 1] = "200";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "c_nombre";

            arrCabeceraDg1[1, 0] = "Tip. Doc.";
            arrCabeceraDg1[1, 1] = "40";
            arrCabeceraDg1[1, 2] = "C";
            arrCabeceraDg1[1, 3] = "c_abr";

            arrCabeceraDg1[2, 0] = "Fch. Documento";
            arrCabeceraDg1[2, 1] = "80";
            arrCabeceraDg1[2, 2] = "F";
            arrCabeceraDg1[2, 3] = "d_fchdoc";

            arrCabeceraDg1[3, 0] = "Nº Documento";
            arrCabeceraDg1[3, 1] = "120";
            arrCabeceraDg1[3, 2] = "F";
            arrCabeceraDg1[3, 3] = "c_numdoc";

            arrCabeceraDg1[4, 0] = "Id";
            arrCabeceraDg1[4, 1] = "0";
            arrCabeceraDg1[4, 2] = "N";
            arrCabeceraDg1[4, 3] = "n_id";

            Genericas xFun = new Genericas();
            xFun.Buscar_CampoBusqueda = "n_id";
            xFun.Buscar_CadFiltro = "";
            dtResult = xFun.Buscar(arrCabeceraDg1, dtResult);

            return dtResult;
        }
        public DataTable BuscarIngresosFacturados(int n_IdEmpresa, int n_IdProveedor)
        {
            Helper.Genericas funDatos = new Helper.Genericas();
            string[,] arrCabeceraDg1 = new string[7, 4];
            DataTable dtResult = new DataTable();

            dtResult = Consulta8(n_IdEmpresa, n_IdProveedor);

            arrCabeceraDg1[0, 0] = "Fch Documento";
            arrCabeceraDg1[0, 1] = "80";
            arrCabeceraDg1[0, 2] = "F";
            arrCabeceraDg1[0, 3] = "d_fchdoc";

            arrCabeceraDg1[1, 0] = "Tip. Doc.";
            arrCabeceraDg1[1, 1] = "40";
            arrCabeceraDg1[1, 2] = "C";
            arrCabeceraDg1[1, 3] = "c_abr";

            arrCabeceraDg1[2, 0] = "Nº Doc. Ingreso";
            arrCabeceraDg1[2, 1] = "120";
            arrCabeceraDg1[2, 2] = "C";
            arrCabeceraDg1[2, 3] = "c_numdoc";

            arrCabeceraDg1[3, 0] = "Fch. Doc. Facturacion";
            arrCabeceraDg1[3, 1] = "80";
            arrCabeceraDg1[3, 2] = "F";
            arrCabeceraDg1[3, 3] = "c_comfchdoc";

            arrCabeceraDg1[4, 0] = "Tip. Doc. Facturacion";
            arrCabeceraDg1[4, 1] = "60";
            arrCabeceraDg1[4, 2] = "C";
            arrCabeceraDg1[4, 3] = "c_comtipdoc";

            arrCabeceraDg1[5, 0] = "Nº Doc. Facturacion";
            arrCabeceraDg1[5, 1] = "120";
            arrCabeceraDg1[5, 2] = "C";
            arrCabeceraDg1[5, 3] = "c_comnumdoc";

            arrCabeceraDg1[6, 0] = "Id";
            arrCabeceraDg1[6, 1] = "0";
            arrCabeceraDg1[6, 2] = "C";
            arrCabeceraDg1[6, 3] = "n_id";

            Genericas xFun = new Genericas();
            xFun.Buscar_CampoBusqueda = "n_id";
            xFun.Buscar_CadFiltro = "";
            dtResult = xFun.Buscar(arrCabeceraDg1, dtResult);

            return dtResult;
        }
        public void ReportConsulta9(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo, int n_TipoVista)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[4, 3];

            arrPara[0, 0] = "n_idemp";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = n_IdEmpresa.ToString();

            arrPara[1, 0] = "n_anotra";
            arrPara[1, 1] = "C";
            arrPara[1, 2] = n_AnoTrabajo.ToString();

            arrPara[2, 0] = "n_mestra";
            arrPara[2, 1] = "C";
            arrPara[2, 2] = n_MesTrabajo.ToString();

            arrPara[3, 0] = "n_tipo";
            arrPara[3, 1] = "C";
            arrPara[3, 2] = n_TipoVista.ToString();

            c_NomArchivo = "RptMovimientoMP.rpt";
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "Almacen\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            if (n_TipoVista == 1) { xVisor.c_Titulo = "ALMACEN - INGRESOS CON COMPRA"; }
            if (n_TipoVista == 2) { xVisor.c_Titulo = "ALMACEN - INGRESOS SIN COMPRA"; }
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
        public void ReportConsulta10(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo, int n_TipoVista)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[4, 3];

            arrPara[0, 0] = "n_idemp";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = n_IdEmpresa.ToString();

            arrPara[1, 0] = "n_anotra";
            arrPara[1, 1] = "C";
            arrPara[1, 2] = n_AnoTrabajo.ToString();

            arrPara[2, 0] = "n_mestra";
            arrPara[2, 1] = "C";
            arrPara[2, 2] = n_MesTrabajo.ToString();

            arrPara[3, 0] = "n_tipo";
            arrPara[3, 1] = "C";
            arrPara[3, 2] = n_TipoVista.ToString();

            c_NomArchivo = "RptMovimientoPro.rpt";
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "Almacen\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            if (n_TipoVista == 1) { xVisor.c_Titulo = "ALMACEN - INGRESOS CON PRODUCCION"; }
            if (n_TipoVista == 2) { xVisor.c_Titulo = "ALMACEN - INGRESOS SIN PRODUCCION"; }
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
        public void ReportKardexResumen(string c_FechaInicio, string c_FechaTermino, int n_IdTipoExistencia, int n_IdAlmacen)
        {
            string c_NomArchivo = "";
            string c_Ruta = "";
            string[,] arrPara = new string[8, 3];

            arrPara[0, 0] = "n_idemp";
            arrPara[0, 1] = "N";
            arrPara[0, 2] = STU_SISTEMA.EMPRESAID.ToString();

            arrPara[1, 0] = "n_anocon";
            arrPara[1, 1] = "N";
            arrPara[1, 2] = STU_SISTEMA.ANOTRABAJO.ToString();

            arrPara[2, 0] = "c_fchini";
            arrPara[2, 1] = "C";
            arrPara[2, 2] = c_FechaInicio;

            arrPara[3, 0] = "c_fchfin";
            arrPara[3, 1] = "C";
            arrPara[3, 2] = c_FechaTermino;

            arrPara[4, 0] = "n_idtipexi";
            arrPara[4, 1] = "N";
            arrPara[4, 2] = n_IdTipoExistencia.ToString();

            arrPara[5, 0] = "n_idalm";
            arrPara[5, 1] = "N";
            arrPara[5, 2] = n_IdAlmacen.ToString();

            arrPara[6, 0] = "c_numruc";
            arrPara[6, 1] = "C";
            arrPara[6, 2] = STU_SISTEMA.EMPRESARUC;

            arrPara[7, 0] = "c_nomemp";
            arrPara[7, 1] = "C";
            arrPara[7, 2] = STU_SISTEMA.EMPRESANOMBRE;

            c_NomArchivo = "RptKardexResumen.rpt";
            c_Ruta = "" + STU_SISTEMA.RUTAREPORTES + "Almacen\\" + c_NomArchivo;

            Helper.Cls_VisorCrystal xVisor = new Helper.Cls_VisorCrystal();
            xVisor.c_NombreServidor = STU_SISTEMA.BD_NOMSERVIDOR;
            xVisor.c_NombreBD = STU_SISTEMA.BD_NOMBASEDATOS;
            xVisor.c_Usuario = STU_SISTEMA.BD_USUARIO;
            xVisor.c_Contraseña = STU_SISTEMA.BD_CONTRASEÑA;
            xVisor.b_VisPrev = true;
            xVisor.c_Titulo = "ALMACEN - KARDEX RESUMEN"; 
            xVisor.c_PathRep = c_Ruta;
            xVisor.arrParametros = arrPara;
            xVisor.VerCrystal();
        }
    }
}
