using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Logistica
{
    public class BE_LOG_ORDENREQUERIMIENTODET
    {
        private int _n_idreq;
        private int _n_idite;
        private int _n_idunimed;
        private double _n_can;

        public int n_idreq
        {
            get { return _n_idreq; }
            set { _n_idreq = value; }
        }
        public int n_idite
        {
            get { return _n_idite; }
            set { _n_idite = value; }
        }
        public int n_idunimed
        {
            get { return _n_idunimed; }
            set { _n_idunimed = value; }
        }
        public double n_can
        {
            get { return _n_can; }
            set { _n_can = value; }
        }
    }
}
