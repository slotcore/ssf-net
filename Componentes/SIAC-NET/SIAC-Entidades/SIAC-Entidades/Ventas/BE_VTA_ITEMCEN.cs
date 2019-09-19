
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Ventas
{
    public class BE_VTA_ITEMCEN
    {
        private int _n_idemp;
        private int _n_id;
        private int _n_idcli;
        private string _c_codcen;
        private string _c_descen;
        private int _n_iditem;

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
        public int n_idcli
        {
            get { return _n_idcli; }
            set { _n_idcli = value; }
        }
        public string c_codcen
        {
            get { return _c_codcen; }
            set { _c_codcen = value; }
        }
        public string c_descen
        {
            get { return _c_descen; }
            set { _c_descen = value; }
        }

        public int n_iditem
        {
            get { return _n_iditem; }
            set { _n_iditem = value; }
        }
    }
}
