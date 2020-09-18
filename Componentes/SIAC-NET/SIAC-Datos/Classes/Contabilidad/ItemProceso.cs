using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_DATOS.Classes.Contabilidad
{
    public class ItemProceso
    {
        public int n_idite { get; set; }
        public int n_idalm { get; set; }

        public ItemProceso(MySqlDataReader reader)
        {
            n_idite = reader.GetInt32("n_idite");
            n_idalm = reader.GetInt32("n_idalm");
        }
    }
}
