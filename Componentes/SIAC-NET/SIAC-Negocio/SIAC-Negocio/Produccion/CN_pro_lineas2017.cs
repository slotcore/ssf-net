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
    public class CN_pro_lineas2017
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        
        public BE_PRO_LINEA2017 entLinea = new BE_PRO_LINEA2017();
        public List<BE_PRO_LINEADET2017> lstLineDet = new List<BE_PRO_LINEADET2017>();

        public DataTable dtProductos = new DataTable();
        public DataTable dtLineas = new DataTable();

        public bool ListarProductosLinea(int n_IdEmpresa)
        {
            bool booResult;
            CD_pro_lineas2017 miFun = new CD_pro_lineas2017();
            
            miFun.mysConec = mysConec;
            booResult = miFun.ListarProductosLinea(n_IdEmpresa);
            if (booResult == true)
            {
                dtProductos = miFun.dtProductos;
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            booResult = true;
            return booResult;
        }
        public bool ListarLineas(int n_IdEmpresa)
        {
            bool booResult;
            CD_pro_lineas2017 miFun = new CD_pro_lineas2017();

            miFun.mysConec = mysConec;
            booResult = miFun.ListarLineas(n_IdEmpresa);
            if (booResult == true)
            {
                dtLineas = miFun.dtLineas;
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            booResult = true;
            return booResult;
        }
        public bool TraerRegistro(int n_IdLinea)
        {
            bool booResult;
            CD_pro_lineas2017 miFun = new CD_pro_lineas2017();
            DataTable dtResult = new DataTable();
            int n_row =0;

            miFun.mysConec = mysConec;
            booResult = miFun.TraerRegistro(n_IdLinea);
            if (booResult == true)
            {
                dtResult = miFun.dtCab;
                if (dtResult.Rows.Count != 0)
                {
                    entLinea.n_idemp = Convert.ToInt32(dtResult.Rows[0]["n_idemp"]);
                    entLinea.n_idpro = Convert.ToInt32(dtResult.Rows[0]["n_idpro"]);
                    entLinea.n_id = Convert.ToInt32(dtResult.Rows[0]["n_id"]);
                    entLinea.c_codlin = dtResult.Rows[0]["c_codlin"].ToString();
                    entLinea.c_deslin = dtResult.Rows[0]["c_deslin"].ToString();
                    entLinea.n_idunimed = Convert.ToInt32(dtResult.Rows[0]["n_idunimed"]);
                    entLinea.n_can = Convert.ToDouble(dtResult.Rows[0]["n_can"]);
                    entLinea.n_numope = Convert.ToDouble(dtResult.Rows[0]["n_numope"]);
                    entLinea.n_efi = Convert.ToDouble(dtResult.Rows[0]["n_efi"]);
                    entLinea.h_tie = Convert.ToString(dtResult.Rows[0]["h_tie"].ToString());
                    entLinea.n_prelin = Convert.ToDouble(dtResult.Rows[0]["n_prelin"]);

                    dtResult = miFun.dtDet;
                    lstLineDet.Clear();
                    for (n_row = 0; n_row <= dtResult.Rows.Count - 1; n_row++)
                    {
                        BE_PRO_LINEADET2017 entDet = new BE_PRO_LINEADET2017();
                        entDet.n_idlin = Convert.ToInt32(dtResult.Rows[n_row]["n_idlin"]);
                        entDet.n_idtar =  Convert.ToInt32(dtResult.Rows[n_row]["n_idtar"]);
                        entDet.n_porefi =  Convert.ToDouble(dtResult.Rows[n_row]["n_porefi"]);
                        entDet.n_numpertar = Convert.ToInt32(dtResult.Rows[n_row]["n_numpertar"]);
                        entDet.n_cankilper = Convert.ToDouble(dtResult.Rows[n_row]["n_cankilper"]);
                      
                        lstLineDet.Add(entDet);
                    }
                }
            }
            else
            {
                booOcurrioError = miFun.booOcurrioError;
                StrErrorMensaje = miFun.StrErrorMensaje;
                IntErrorNumber = miFun.IntErrorNumber;
            }
            booResult = true;
            return booResult;
        }
    }
}
