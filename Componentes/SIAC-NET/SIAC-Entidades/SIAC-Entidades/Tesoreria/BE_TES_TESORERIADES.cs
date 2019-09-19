using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Tesoreria
{
    public class BE_TES_TESORERIADES
    {
        private int _n_idtes;
        private int _n_iddes;
        private double _n_imp;
        private int _n_idmod;
        private int _n_idbcocta;
        private double _n_tc;

        public int n_idtes
        {
            get { return _n_idtes; }
            set { _n_idtes = value; }
        }
        public int n_iddes
        {
            get { return _n_iddes; }
            set { _n_iddes = value; }
        }
        public double n_imp
        {
            get { return _n_imp; }
            set { _n_imp = value; }
        }
        public int n_idmod
        {
            get { return _n_idmod; }
            set { _n_idmod = value; }
        }
        public int n_idbcocta
        {
            get { return _n_idbcocta; }
            set { _n_idbcocta = value; }
        }
        public double n_tc
        {
            get { return _n_tc; }
            set { _n_tc = value; }
        }
    }
}
