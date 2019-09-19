using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Planillas
{
    public class BE_PLA_MARCACION
    {
        private int _n_idemp;
        private int _n_id;
        private DateTime _d_fchini;
        private DateTime _d_fchfin;
        private DateTime _d_fchimp;
        private int _n_idper;
        private int _n_nummar;

        public int n_idemp
        {
            get { return _n_idemp; }
            set { _n_idemp = value; }
        }
        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
        public DateTime d_fchini
        {
            get { return _d_fchini; }
            set { _d_fchini = value; }
        }
        public DateTime d_fchfin
        {
            get { return _d_fchfin; }
            set { _d_fchfin = value; }
        }
        public DateTime d_fchimp
        {
            get { return _d_fchimp; }
            set { _d_fchimp = value; }
        }
        public int n_idper
        {
            get { return _n_idper; }
            set { _n_idper = value; }
        }
        public int n_nummar
        {
            get { return _n_nummar; }
            set { _n_nummar = value; }
        }
    }
}
