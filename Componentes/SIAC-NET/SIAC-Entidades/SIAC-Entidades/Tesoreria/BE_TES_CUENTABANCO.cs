using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Tesoreria
{
    public class BE_TES_CUENTABANCO
    {
        private int _n_id;
        private int _n_idemp;
        private int _n_idban;
        private string _c_numcue;
        private int _n_idmon;
        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
        public int n_idemp
        {
            get { return _n_idemp; }
            set { _n_idemp = value; }
        }
        public int n_idban
        {
            get { return _n_idban; }
            set { _n_idban = value; }
        }
        public string c_numcue
        {
            get { return _c_numcue; }
            set { _c_numcue = value; }
        }
        public int n_idmon
        {
            get { return _n_idmon; }
            set { _n_idmon = value; }
        }
    }
}
