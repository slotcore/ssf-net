using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Tesoreria
{
    public class BE_TES_CONCILIACIONDET
    {
        int _n_idcon;
		int _n_idtes;
		int _n_idori;
        int _n_check;
        DateTime _d_opefch;
		string _c_opeglo;
		string _c_openumreg;
		string _c_openumdoc;
        double _n_opeimp;
        int _n_tipreg;

        public int n_idcon
        {
            get { return _n_idcon; }
            set { _n_idcon = value; }
        }
        public int n_idtes
        {
            get { return _n_idtes; }
            set { _n_idtes = value; }
        }
        public int n_idori
        {
            get { return _n_idori; }
            set { _n_idori = value; }
        }
        public int n_check
        {
            get { return _n_check; }
            set { _n_check = value; }
        }
        public DateTime d_opefch
        {
            get { return _d_opefch; }
            set { _d_opefch = value; }
        }
        public string c_opeglo
        {
            get { return _c_opeglo; }
            set { _c_opeglo = value; }
        }
        public string c_openumreg
        {
            get { return _c_openumreg; }
            set { _c_openumreg = value; }
        }
        public string c_openumdoc
        {
            get { return _c_openumdoc; }
            set { _c_openumdoc = value; }
        }
        public double n_opeimp
        {
            get { return _n_opeimp; }
            set { _n_opeimp = value; }
        }
        public int n_tipreg
        {
            get { return _n_tipreg; }
            set { _n_tipreg = value; }
        }
    }
}
