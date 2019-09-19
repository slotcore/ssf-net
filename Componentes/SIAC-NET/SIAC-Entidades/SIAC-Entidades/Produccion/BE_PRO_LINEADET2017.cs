using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Produccion
{
    public class BE_PRO_LINEADET2017
    {
        private int _n_idlin;
        private int _n_idtar;
        private double _n_porefi;
        private int _n_numpertar;
        private double _n_cankilper;
        public int n_idlin
        {
            get { return _n_idlin; }
            set { _n_idlin = value; }
        }
        public int n_idtar
        {
            get { return _n_idtar; }
            set { _n_idtar = value; }
        }
        public double n_porefi
        {
            get { return _n_porefi; }
            set { _n_porefi = value; }
        }
        public int n_numpertar
        {
            get { return _n_numpertar; }
            set { _n_numpertar = value; }
        }
        public double n_cankilper
        {
            get { return _n_cankilper; }
            set { _n_cankilper = value; }
        }
    }
}
