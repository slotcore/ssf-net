using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Produccion
{
    public class BE_PRO_PRODUCCIONTAREAS
    {
        private int _n_idpro;
        private int _n_idtar;
        private double _n_can;
        private string _c_horini;
        private string _c_horter;
        private DateTime _d_fchfab;
        private int _n_numper;
        private double _n_impcostar;
        public int n_idpro
        {
            get { return _n_idpro; }
            set { _n_idpro = value; }
        }
        public int n_idtar
        {
            get { return _n_idtar; }
            set { _n_idtar = value; }
        }
        public double n_can
        {
            get { return _n_can; }
            set { _n_can = value; }
        }
        public string c_horini
        {
            get { return _c_horini; }
            set { _c_horini = value; }
        }
        public string c_horter
        {
            get { return _c_horter; }
            set { _c_horter = value; }
        }
        public DateTime d_fchfab
        {
            get { return _d_fchfab; }
            set { _d_fchfab = value; }
        }
        public int n_numper
        {
            get { return _n_numper; }
            set { _n_numper = value; }
        }
        public double n_impcostar
        {
            get { return _n_impcostar; }
            set { _n_impcostar = value; }
        }
    }
}
