using MySql.Data.MySqlClient;
using SIAC_DATOS.Almacen;
using SIAC_DATOS.Produccion;
using SIAC_DATOS.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using SIAC_Entidades.Produccion;

namespace SIAC_Negocio.Produccion
{
    public class CN_pro_lineas
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public List<BE_PRO_LINEAS> lstLineas = new List<BE_PRO_LINEAS>();
        public List<BE_PRO_LINEASDET> lstLineasDet = new List<BE_PRO_LINEASDET>();
        public void obternerlineas(int n_IdEmpresa, int n_IdReceta)
        {
            DataTable dtCab = new DataTable();
            DataTable dtDet = new DataTable();
            bool booResult;
            CD_pro_lineas miFun = new CD_pro_lineas();
            int n_row = 0;

            lstLineas.Clear();
            lstLineasDet.Clear();

            miFun.mysConec = mysConec;
            booResult = miFun.obternerlineas(n_IdEmpresa, n_IdReceta);
            
            if (booResult == true)
            {
                dtCab = miFun.dtlineas;
                dtDet = miFun.dtlineasDet;

                for (n_row = 0; n_row <= dtCab.Rows.Count - 1; n_row++)
                { 
                    BE_PRO_LINEAS entLineas = new BE_PRO_LINEAS();

                    entLineas.n_idemp = Convert.ToInt16(dtCab.Rows[n_row]["n_idemp"]);
                    entLineas.n_id = Convert.ToInt16(dtCab.Rows[n_row]["n_id"]);
                    entLineas.n_idaux = Convert.ToInt16(dtCab.Rows[n_row]["n_idaux"]);
                    entLineas.n_idrec = Convert.ToInt16(dtCab.Rows[n_row]["n_idrec"]);
                    entLineas.c_des = dtCab.Rows[n_row]["c_des"].ToString();
                    entLineas.n_idunimed = Convert.ToInt16(dtCab.Rows[n_row]["n_idunimed"]);
                    entLineas.n_can = Convert.ToDouble(dtCab.Rows[n_row]["n_can"]);
                    entLineas.n_efi = Convert.ToDouble(dtCab.Rows[n_row]["n_efi"]);
                    entLineas.n_numope = Convert.ToDouble(dtCab.Rows[n_row]["n_numope"]);
                    entLineas.n_kghor = Convert.ToDouble(dtCab.Rows[n_row]["n_kghor"]);
                    entLineas.n_activo = Convert.ToInt16(dtCab.Rows[n_row]["n_activo"]);
                    lstLineas.Add(entLineas);
                }

                if (dtDet.Rows.Count!=0)
                { 
                    for (n_row = 0; n_row <= dtDet.Rows.Count - 1; n_row++)
                    {
                        BE_PRO_LINEASDET entLineasdet = new BE_PRO_LINEASDET();

                        entLineasdet.n_idlin = Convert.ToInt16(dtDet.Rows[n_row]["n_idlin"]);
                        entLineasdet.n_idrec = Convert.ToInt16(dtDet.Rows[n_row]["n_idrec"]);
                        entLineasdet.n_ord = Convert.ToInt16(dtDet.Rows[n_row]["n_ord"]);
                        entLineasdet.n_idtar = Convert.ToInt16(dtDet.Rows[n_row]["n_idtar"]);
                        entLineasdet.n_idunimed = Convert.ToInt16(dtDet.Rows[n_row]["n_idunimed"]);
                        entLineasdet.n_rdt = Convert.ToDouble(dtDet.Rows[n_row]["n_rdt"]);
                        entLineasdet.n_pri = Convert.ToInt16(dtDet.Rows[n_row]["n_pri"]);
                        entLineasdet.n_kghor = Convert.ToDouble(dtDet.Rows[n_row]["n_kghor"]);
                        entLineasdet.n_numope = Convert.ToDouble(dtDet.Rows[n_row]["n_numope"]);
                        entLineasdet.n_canpro = Convert.ToDouble(dtDet.Rows[n_row]["n_canpro"]);
                        entLineasdet.n_numopeide = Convert.ToDouble(dtDet.Rows[n_row]["n_numopeide"]);
                        entLineasdet.n_efiper = Convert.ToDouble(dtDet.Rows[n_row]["n_efiper"]);
                        entLineasdet.n_efitar = Convert.ToDouble(dtDet.Rows[n_row]["n_efitar"]);
                        entLineasdet.n_canrea = Convert.ToDouble(dtDet.Rows[n_row]["n_canrea"]);
                        entLineasdet.n_durtar = Convert.ToDouble(dtDet.Rows[n_row]["n_durtar"]);
                        entLineasdet.n_fac = Convert.ToDouble(dtDet.Rows[n_row]["n_fac"]);
                        entLineasdet.n_desval = Convert.ToDouble(dtDet.Rows[n_row]["n_desval"]);
                        entLineasdet.n_interv = Convert.ToDouble(dtDet.Rows[n_row]["n_interv"]);
                        entLineasdet.n_durtarrea = Convert.ToDouble(dtDet.Rows[n_row]["n_durtarrea"]);

                        lstLineasDet.Add(entLineasdet);
                    }
                }
            }
            if (booResult == false)
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            return;
        }
        public bool Eliminar(List<BE_PRO_LINEAS> lstLineas)
        {
            bool booResult = false;
            CD_pro_lineas objLineas = new CD_pro_lineas();
            booResult = objLineas.Eliminar(lstLineas);
            return booResult;
        }
    }
}
