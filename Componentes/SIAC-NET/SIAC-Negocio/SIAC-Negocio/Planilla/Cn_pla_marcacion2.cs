using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using SIAC_Entidades.Planillas;
using SIAC_DATOS.Planilla;
using MySql.Data.MySqlClient;
using Helper;

namespace SIAC_Negocio.Planilla
{
    public class CN_pla_marcacion2
    {
        public bool b_OcurrioError = false;
        public string c_ErrorMensaje = "";
        public int n_ErrorNumber = 0;
        
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA;
        public DataTable dtListar = new DataTable();
        CD_pla_marcacion2 miFun;

        public CN_pla_marcacion2(SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA)
        {
            CD_pla_marcacion2 xFun = new CD_pla_marcacion2(STU_SISTEMA.BD_IP, STU_SISTEMA.BD_NOMBASEDATOS, STU_SISTEMA.BD_USUARIO, STU_SISTEMA.BD_CONTRASEÑA, STU_SISTEMA.BD_PUERTO);
            miFun = xFun;
        }

        ~CN_pla_marcacion2()
        {
            miFun = null;
        }
        public bool Insertar(List<BE_PLA_MARCACION2> l_Marcacion, string c_FechaInicio, string c_FechaFinal)
        {
            bool booOk = false;

            booOk = miFun.Insertar(l_Marcacion, c_FechaInicio, c_FechaFinal);

            b_OcurrioError = miFun.b_OcurrioError;
            c_ErrorMensaje = miFun.c_ErrorMensaje;
            n_ErrorNumber = miFun.n_ErrorNumber;

            return booOk;
        }
    }
}
