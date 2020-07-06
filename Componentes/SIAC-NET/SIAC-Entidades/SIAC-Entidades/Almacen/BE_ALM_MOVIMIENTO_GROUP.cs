using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Almacen
{
    public class BE_ALM_MOVIMIENTO_GROUP
    {
        public BE_ALM_MOVIMIENTOS entCabecera { get; set; }

        public List<BE_ALM_MOVIMIENTOSDET> lstDetalle { get; set; }

        public List<BE_ALM_INVENTARIOLOTE> lstLote { get; set; }
    }
}
