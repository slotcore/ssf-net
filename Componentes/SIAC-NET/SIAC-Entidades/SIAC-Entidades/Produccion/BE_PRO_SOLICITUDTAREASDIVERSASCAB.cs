using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Produccion
{
    public class BE_PRO_SOLICITUDTAREASDIVERSASCAB
    {
        private int _n_idsol;
        private int _n_id;
        private int _n_idtar;
        private double _n_can;
        private string _h_horini;
        private string _h_horfin;
        private int _n_numper;
        private double _n_costar;
        private int _n_ord;
        private DateTime? _d_fchtra;
        public int n_idsol
        {
            get { return _n_idsol; }
            set { _n_idsol = value; }
        }
        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
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
        public int n_numper
        {
            get { return _n_numper; }
            set { _n_numper = value; }
        }
        public double n_costar
        {
            get { return _n_costar; }
            set { _n_costar = value; }
        }
        public int n_ord
        {
            get { return _n_ord; }
            set { _n_ord = value; }
        }
        public DateTime? d_fchtra
        {
            get { return _d_fchtra; }
            set { _d_fchtra = value; }
        }
    }
}
