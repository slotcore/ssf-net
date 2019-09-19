using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Produccion
{
    public class BE_PRO_RECETATAREA
    {
        private int _n_idrec;
        private int _n_idtar;
        private int _n_idunimed;
        private double _n_can;
        private int _n_ord;
        private double _n_factor;
        private double _n_cosklg;
        private double _n_coshor;
        private double _n_jorklg;
        private string _c_despro;
        private int _n_numper;
        private string _h_horarr;
        private int _n_aplpor;
        private int _n_idare;
        private int _n_idtiptra;
        private int _n_idforpag;

        public int n_idrec
        {
            get { return _n_idrec; }
            set { _n_idrec = value; }
        }
        public int n_idtar
        {
            get { return _n_idtar; }
            set { _n_idtar = value; }
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
        public int n_ord
        {
            get { return _n_ord; }
            set { _n_ord = value; }
        }
        public double n_factor
        {
            get { return _n_factor; }
            set { _n_factor = value; }
        }
        public double n_cosklg
        {
            get { return _n_cosklg; }
            set { _n_cosklg = value; }
        }
        public double n_coshor
        {
            get { return _n_coshor; }
            set { _n_coshor = value; }
        }
        public double n_jorklg
        {
            get { return _n_jorklg; }
            set { _n_jorklg = value; }
        }
        public string c_despro
        {
            get { return _c_despro; }
            set { _c_despro = value; }
        }
        public int n_numper
        {
            get { return _n_numper; }
            set { _n_numper = value; }
        }
        public string h_horarr
        {
            get { return _h_horarr; }
            set { _h_horarr = value; }
        }
        public int n_aplpor
        {
            get { return _n_aplpor; }
            set { _n_aplpor = value; }
        }
        public int n_idare
        {
            get { return _n_idare; }
            set { _n_idare = value; }
        }
        public int n_idtiptra
        {
            get { return _n_idtiptra; }
            set { _n_idtiptra = value; }
        }
        public int n_idforpag
        {
            get { return _n_idforpag; }
            set { _n_idforpag = value; }
        }
    }
}
