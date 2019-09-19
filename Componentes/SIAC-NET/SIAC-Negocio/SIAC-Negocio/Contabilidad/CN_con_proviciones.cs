using System;
using System.Data;
using MySql.Data.MySqlClient;
using SIAC_Entidades.Contabilidad;
using SIAC_DATOS.Contabilidad;
using System.Collections.Generic;

namespace SIAC_Negocio.Contabilidad
{
    public class CN_con_proviciones
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public DataTable dtLista = new DataTable();
        public BE_CON_PROVICIONES e_Provicion = new BE_CON_PROVICIONES();
        public List<BE_CON_PROVICIONESDET> l_ProvicionDet = new List<BE_CON_PROVICIONESDET>();
        public List<BE_CON_DIARIO> l_Diario = new List<BE_CON_DIARIO>();

        public void Listar(int n_IdEmpresa, int n_AnoTrabajo, int n_MesTrabajo)
        {
            CD_con_proviciones miFun = new CD_con_proviciones();
            miFun.mysConec = mysConec;

            miFun.Listar(n_IdEmpresa, n_AnoTrabajo, n_MesTrabajo);
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
            int n_row = 0;
            DataTable dtDet = new DataTable();
            CD_con_proviciones miFun = new CD_con_proviciones();
            miFun.mysConec = mysConec;

            miFun.TraerRegistro(n_IdRegistro);
            dtLista = miFun.dtLista;
            dtDet = miFun.dtDetalle;

            if (dtLista == null)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            else
            {
                e_Provicion.n_id = Convert.ToInt32(dtLista.Rows[0]["n_id"]);
                e_Provicion.n_idlib = Convert.ToInt32(dtLista.Rows[0]["n_idlib"]);
                e_Provicion.n_idsublib = Convert.ToInt32(dtLista.Rows[0]["n_idsublib"]);
                e_Provicion.n_ano = Convert.ToInt32(dtLista.Rows[0]["n_ano"]);
                e_Provicion.n_mes = Convert.ToInt32(dtLista.Rows[0]["n_mes"]);
                e_Provicion.d_fchreg = Convert.ToDateTime(dtLista.Rows[0]["d_fchreg"]);
                e_Provicion.d_fchdoc = Convert.ToDateTime(dtLista.Rows[0]["d_fchdoc"]);
                e_Provicion.n_idtipdoc = Convert.ToInt32(dtLista.Rows[0]["n_idtipdoc"]);
                e_Provicion.c_numser = dtLista.Rows[0]["c_numser"].ToString();
                e_Provicion.c_numdoc = dtLista.Rows[0]["c_numdoc"].ToString();
                e_Provicion.n_idcli = Convert.ToInt32(dtLista.Rows[0]["n_idcli"]);
                e_Provicion.c_nomcli = dtLista.Rows[0]["c_nomcli"].ToString();
                e_Provicion.n_idmon = Convert.ToInt32(dtLista.Rows[0]["n_idmon"]);
                e_Provicion.n_imp = Convert.ToDouble(dtLista.Rows[0]["n_imp"]);
                e_Provicion.c_glosa = dtLista.Rows[0]["c_glosa"].ToString();
                e_Provicion.c_numreg = dtLista.Rows[0]["c_numreg"].ToString();
                e_Provicion.n_tc = Convert.ToDouble(dtLista.Rows[0]["n_tc"]);
                e_Provicion.n_ajuste = Convert.ToInt32(dtLista.Rows[0]["n_ajuste"]);
                e_Provicion.n_idemp = Convert.ToInt32(dtLista.Rows[0]["n_idemp"]);

                l_ProvicionDet.Clear();
                for (n_row = 0; n_row <= dtDet.Rows.Count - 1; n_row++)
                { 
                    BE_CON_PROVICIONESDET e_Det = new BE_CON_PROVICIONESDET();
                    e_Det.n_idpro = Convert.ToInt32(dtDet.Rows[n_row]["n_idpro"]);
                    e_Det.n_idcuecon = Convert.ToInt32(dtDet.Rows[n_row]["n_idcuecon"]);
                    e_Det.n_tipo = Convert.ToInt32(dtDet.Rows[n_row]["n_tipo"]);
                    e_Det.n_impsol = Convert.ToDouble(dtDet.Rows[n_row]["n_impsol"]);
                    e_Det.n_impdol = Convert.ToDouble(dtDet.Rows[n_row]["n_impdol"]);

                    l_ProvicionDet.Add(e_Det);
                }
            }
            return;
        }
        public bool Eliminar(BE_CON_PROVICIONES e_Proviciones)
        {
            bool b_result = false;
            CD_con_proviciones miFun = new CD_con_proviciones();

            miFun.mysConec = mysConec;
            b_result = miFun.Eliminar(e_Proviciones);
            if (b_result == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return b_result;
        }
        public bool Insertar(BE_CON_PROVICIONES e_Proviciones, List<BE_CON_PROVICIONESDET> l_ProvicionesDet)
        {
            bool b_result = false;
            CD_con_proviciones miFun = new CD_con_proviciones();
            CN_con_diario funCon = new CN_con_diario();
            miFun.mysConec = mysConec;
            funCon.mysConec = mysConec;

            funCon.GenerarAsientoDiversos("", e_Proviciones, l_ProvicionesDet);
            l_Diario = funCon.l_Diario;
            e_Proviciones.c_numreg = l_Diario[0].c_numasi;

            b_result = miFun.Insertar(e_Proviciones, l_ProvicionesDet, l_Diario);
            if (b_result == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }

            return b_result;
        }
        public bool Actualizar(BE_CON_PROVICIONES e_Proviciones, List<BE_CON_PROVICIONESDET> l_ProvicionesDet)
        {
            bool b_result = false;
            CD_con_proviciones miFun = new CD_con_proviciones();
            CN_con_diario funCon = new CN_con_diario();
            miFun.mysConec = mysConec;
            funCon.mysConec = mysConec;

            funCon.GenerarAsientoDiversos(e_Proviciones.c_numreg, e_Proviciones, l_ProvicionesDet);
            l_Diario = funCon.l_Diario;
            //e_Proviciones.c_numreg = l_Diario[0].c_numasi;

            b_result = miFun.Actualizar(e_Proviciones, l_ProvicionesDet, l_Diario);
            if (b_result == false)
            {
                b_OcurrioError = miFun.b_OcurrioError;
                c_ErrorMensaje = miFun.c_ErrorMensaje;
                n_ErrorNumber = miFun.n_ErrorNumber;
            }
            return b_result;
        }
    }
}
