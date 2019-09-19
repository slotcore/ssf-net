using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Ventas
{
    public class BE_VTA_GUIASDOC
    {
        private int _n_idgui;
        private int _n_idtipdoc;
        private string _c_numdoc;
        private int _n_iddoc;

        public int n_idgui
        {
            get { return _n_idgui; }
            set { _n_idgui = value; }
        }
        public int n_idtipdoc
        {
            get { return _n_idtipdoc; }
            set { _n_idtipdoc = value; }
        }
        public string c_numdoc
        {
            get { return _c_numdoc; }
            set { _c_numdoc = value; }
        }
        public int n_iddoc
        {
            get { return _n_iddoc; }
            set { _n_iddoc = value; }
        }
    }
}
