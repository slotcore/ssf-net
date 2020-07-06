using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Almacen
{
    public class BE_ALM_MOVIMIENTOS_CONSULTA : BE_ALM_MOVIMIENTOS
    {
        public BE_ALM_MOVIMIENTOS_CONSULTA()
        {
            _lst_items = new List<BE_ALM_MOVIMIENTOSDET_CONSULTA>();
        }

        private List<BE_ALM_MOVIMIENTOSDET_CONSULTA> _lst_items;

        public List<BE_ALM_MOVIMIENTOSDET_CONSULTA> lst_items
        {
            get { return _lst_items; }
            set { _lst_items = value; }
        }
    }
}
