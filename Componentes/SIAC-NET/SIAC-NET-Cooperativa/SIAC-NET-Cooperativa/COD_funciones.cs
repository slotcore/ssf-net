using Helper;
using MySql.Data.MySqlClient;
using SIAC_Negocio.Cooperativa;
using SIAC_Objetos.Sistema;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_NET_Cooperativa
{
    public class COD_Funciones
    {
        public MySqlConnection mysConec = new MySqlConnection();
        public Sistema.STU_SISTEMA STU_SISTEMA = new Sistema.STU_SISTEMA();

        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;

        Genericas xFunGen = new Genericas();

        public DataTable BuscarSocios()
        {
            string[,] arrCabeceraDg1 = new string[3, 4];
            DataTable dtResult = new DataTable();
            CN_coo_socios objSocios = new CN_coo_socios();

            objSocios.mysConec = mysConec;
            dtResult = objSocios.Listar(STU_SISTEMA.EMPRESAID);
            if (objSocios.booOcurrioError == true)
            {
                return dtResult;
            }
                       
            arrCabeceraDg1[0, 0] = "Nº Doc. Identidad";
            arrCabeceraDg1[0, 1] = "80";
            arrCabeceraDg1[0, 2] = "C";
            arrCabeceraDg1[0, 3] = "c_idenumdoc";

            arrCabeceraDg1[1, 0] = "Nombre Socio";
            arrCabeceraDg1[1, 1] = "350";
            arrCabeceraDg1[1, 2] = "C";
            arrCabeceraDg1[1, 3] = "c_apenom";

            arrCabeceraDg1[2, 0] = "IdSocio";
            arrCabeceraDg1[2, 1] = "0";
            arrCabeceraDg1[2, 2] = "N";
            arrCabeceraDg1[2, 3] = "n_id";
                        
            xFunGen.Buscar_CampoBusqueda = "n_id";
            xFunGen.Buscar_CadFiltro = "";
            xFunGen.Buscar_CampoOrden = "c_apenom";
            dtResult = xFunGen.Buscar(arrCabeceraDg1, dtResult);

            if (dtResult == null) { return dtResult; }
            if (dtResult.Rows.Count == 0) { return dtResult; }

            return dtResult;
        }
    }
}
