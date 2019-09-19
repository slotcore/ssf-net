using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Ventas
{
    public class BE_VTA_VENTASOCT
    {
        private Int64 _n_idvta;
        private int _n_idcon;
        private double _n_importe;
        public Int64 n_idvta
        {
            get { return _n_idvta; }
            set { _n_idvta = value; }
        }
        public int n_idcon
        {
            get { return _n_idcon; }
            set { _n_idcon = value; }
        }
        public double n_importe
        {
            get { return _n_importe; }
            set { _n_importe = value; }
        }
    }
}
