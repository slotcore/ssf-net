using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Produccion
{
    public class BE_PRO_SOLICITUDMATERIALESDET
    {
        private int _n_idsol;
        private int _n_idite;
        private int _n_idunimed;
        private double _n_canteo;
        private double _n_canent;
        private string _c_numlot;
        private double _n_impval;

        public int n_idsol
        {
            get { return _n_idsol; }
            set { _n_idsol = value; }
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
        public double n_canteo
        {
            get { return _n_canteo; }
            set { _n_canteo = value; }
        }
        public double n_canent
        {
            get { return _n_canent; }
            set { _n_canent = value; }
        }
        public string c_numlot
        {
            get { return _c_numlot; }
            set { _c_numlot = value; }
        }
        public double n_impval
        {
            get { return _n_impval; }
            set { _n_impval = value; }
        }
    }
}
