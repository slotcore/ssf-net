using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using Helper;
using SIAC_Entidades.Planillas;
using SIAC_DATOS.Planilla;
using MySql.Data.MySqlClient;

namespace SIAC_Negocio.Planilla
{
    public class CN_TEMPUS_mostrar
    {
        public bool booOcurrioError = false;
        public string StrErrorMensaje = "";
        public int IntErrorNumber = 0;
        public MySqlConnection mysConec = new MySqlConnection();
        public SIAC_Objetos.Sistema.Sistema.STU_SISTEMA STU_SISTEMA = new SIAC_Objetos.Sistema.Sistema.STU_SISTEMA();
        public bool Insertar(List<BE_TEMPUS_MOSTRAR> lst_empleados)
        {
            CD_TEMPUS_mostrar miFun = new CD_TEMPUS_mostrar();
            
            bool booOk = false;

            miFun.mysConec = mysConec;

            foreach (BE_TEMPUS_MOSTRAR element in lst_empleados)
            {
                BE_TEMPUS_MOSTRAR ent_empleados = new BE_TEMPUS_MOSTRAR();

                ent_empleados.c_codemp = element.c_codemp;
                ent_empleados.c_idemp = element.c_idemp;
                ent_empleados.n_mostrar = element.n_mostrar;

                booOk = miFun.Insertar(ent_empleados);
            }

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
        public bool Eliminar(string c_codigoempresa)
        {
            CD_TEMPUS_mostrar miFun = new CD_TEMPUS_mostrar();
            bool booOk = false;

            miFun.mysConec = mysConec;

            booOk = miFun.Eliminar(c_codigoempresa);

            booOcurrioError = miFun.booOcurrioError;
            StrErrorMensaje = miFun.StrErrorMensaje;
            IntErrorNumber = miFun.IntErrorNumber;

            return booOk;
        }
    }
}
