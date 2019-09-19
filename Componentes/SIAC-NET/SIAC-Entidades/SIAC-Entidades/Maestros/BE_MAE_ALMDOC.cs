using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Maestros
{
    public class BE_MAE_ALMDOC
    {
        private int _n_idalm;
        private int _n_idtipdoc;
        private string _c_numser;
        private string _c_numdoc;
        private int _n_idemp;

        public int n_idalm
        {
            get { return _n_idalm; }
            set { _n_idalm = value; }
        }
        public int n_idtipdoc
        {
            get { return _n_idtipdoc; }
            set { _n_idtipdoc = value; }
        }
        public string c_numser
        {
            get { return _c_numser; }
            set { _c_numser = value; }
        }
        public string c_numdoc
        {
            get { return _c_numdoc; }
            set { _c_numdoc = value; }
        }
        public int n_idemp
        {
            get { return _n_idemp; }
            set { _n_idemp = value; }
        }
    }
}
