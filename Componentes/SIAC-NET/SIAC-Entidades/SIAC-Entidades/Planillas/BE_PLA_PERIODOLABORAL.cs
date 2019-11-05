using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Planillas
{
    public class BE_PLA_PERIODOLABORAL
    {
        public int n_id { get; set; }

        public int n_idcategoria { get; set; }

        public int n_idfinperiodo { get; set; }

        public int n_corr { get; set; }

        public DateTime d_fchini { get; set; }

        public DateTime d_fchfin { get; set; }
    }
}
