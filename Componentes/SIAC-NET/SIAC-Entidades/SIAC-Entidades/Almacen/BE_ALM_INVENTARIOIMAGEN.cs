using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Almacen
{
    public class BE_ALM_INVENTARIOIMAGEN
    {
        Int64 _n_idite;
        int _n_id;
        string _c_des;
        string _c_nomfil;

        public Int64 n_idite
        {
            get { return _n_idite; }
            set { _n_idite = value; }
        }
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
        public string c_nomfil
        {
            get { return _c_nomfil; }
            set { _c_nomfil = value; }
        }
    }
}
