using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Logistica
{
    public class BE_LOG_ORDENCOMPRADET
    {
        int _n_idoc;
        int _n_idite;
        int _n_idunimed;
        double _n_can;
        double _n_preuni;
        double _n_imptot;
        int _n_idtipafeigv;
        private double _n_canat;

        public int n_idoc
        {
            get { return _n_idoc; }
            set { _n_idoc = value; }
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
        public double n_preuni
        {
            get { return _n_preuni; }
            set { _n_preuni = value; }
        }
        public double n_imptot
        {
            get { return _n_imptot; }
            set { _n_imptot = value; }
        }
        public int n_idtipafeigv
        {
            get { return _n_idtipafeigv; }
            set { _n_idtipafeigv = value; }
        }

        public double n_canat
        {
            get { return _n_canat; }
            set { _n_canat = value; }
        }
    }
}
