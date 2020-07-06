using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSF_NET_Produccion.ViewModels
{
    public class OrdenProduccionDetalleItem
    {
        public int n_idite { get; set; }

        public string c_despro { get; set; }

        public double n_can { get; set; }

        public int n_idunimed { get; set; }

        public string c_desunimed { get; set; }

        public int n_idrec { get; set; }

        public string c_desrec { get; set; }

        public DateTime d_fchent { get; set; }
    }
}
