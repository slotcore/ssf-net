using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Planillas
{
    public class BE_PLA_MARCACIONDET
    {
        private int _n_idmar;
        private string _c_codemp;
        private DateTime _d_fchmar;
        private string _c_hormar;

        public int n_idmar
        {
            get { return _n_idmar; }
            set { _n_idmar = value; }
        }
        public string c_codemp
        {
            get { return _c_codemp; }
            set { _c_codemp = value; }
        }
        public DateTime d_fchmar
        {
            get { return _d_fchmar; }
            set { _d_fchmar = value; }
        }
        public string c_hormar
        {
            get { return _c_hormar; }
            set { _c_hormar = value; }
        }
    }
}
