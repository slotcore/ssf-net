using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Produccion
{
    public class BE_PRO_PROGRAMADETPRO
    {
        private int _n_idpro;
        private int _n_idordpro;
        private int _n_idite;
        private int _n_idrec;
        private int _n_idlin;
        private int _n_idunimed;
        private double _n_can;
        private DateTime ?_d_fchent;
        private DateTime ?_d_fchpro;
        private string _h_horini;
        private string _h_horfin;
        private int _n_idres;

        public int n_idpro
        {
            get { return _n_idpro; }
            set { _n_idpro = value; }
        }
        public int n_idordpro
        {
            get { return _n_idordpro; }
            set { _n_idordpro = value; }
        }
        public int n_idite
        {
            get { return _n_idite; }
            set { _n_idite = value; }
        }
        public int n_idrec
        {
            get { return _n_idrec; }
            set { _n_idrec = value; }
        }
        public int n_idlin
        {
            get { return _n_idlin; }
            set { _n_idlin = value; }
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
        public DateTime ?d_fchent
        {
            get { return _d_fchent; }
            set { _d_fchent = value; }
        }
        public DateTime ?d_fchpro
        {
            get { return _d_fchpro; }
            set { _d_fchpro = value; }
        }
        public string h_horini
        {
            get { return _h_horini; }
            set { _h_horini = value; }
        }
        public string h_horfin
        {
            get { return _h_horfin; }
            set { _h_horfin = value; }
        }
        public int n_idres
        {
            get { return _n_idres; }
            set { _n_idres = value; }
        }
    }
}
