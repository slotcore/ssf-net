using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Tesoreria
{
    public class BE_TES_DOCUMENTOS
    {
        private int _n_id;
        private string _c_des;
        private string _c_abr;
        private int _n_tipo;
        private int _n_sel;
        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
        public string c_des
        {
            get { return _c_des; }
            set { _c_des = value; }
        }
        public string c_abr
        {
            get { return _c_abr; }
            set { _c_abr = value; }
        }
        public int n_tipo
        {
            get { return _n_tipo; }
            set { _n_tipo = value; }
        }
        public int n_sel
        {
            get { return _n_sel; }
            set { _n_sel = value; }
        }
    }
}
