using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Maestros
{
    public class BE_MAE_CLIPROITEMS
    {
        private int _n_idcli;
        private int _n_idite;
        private double _n_tasigv;
        private double _n_prebru;
        private double _n_prenet;
        public int n_idcli
        {
            get { return _n_idcli; }
            set { _n_idcli = value; }
        }
        public int n_idite
        {
            get { return _n_idite; }
            set { _n_idite = value; }
        }
        public double n_tasigv
        {
            get { return _n_tasigv; }
            set { _n_tasigv = value; }
        }
        public double n_prebru
        {
            get { return _n_prebru; }
            set { _n_prebru = value; }
        }
        public double n_prenet
        {
            get { return _n_prenet; }
            set { _n_prenet = value; }
        }
    }
}
