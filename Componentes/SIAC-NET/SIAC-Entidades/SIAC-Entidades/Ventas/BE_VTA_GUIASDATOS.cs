using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Ventas
{
    public class BE_VTA_GUIASDATOS
    {
        public int _n_idgui;
        public int _n_idtipdoc;
        public string _c_facnumser;
        public string _c_facnumdoc;
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
        public string c_facnumser
        {
            get { return _c_facnumser; }
            set { _c_facnumser = value; }
        }
        public string c_facnumdoc
        {
            get { return _c_facnumdoc; }
            set { _c_facnumdoc = value; }
        }
    }
}
