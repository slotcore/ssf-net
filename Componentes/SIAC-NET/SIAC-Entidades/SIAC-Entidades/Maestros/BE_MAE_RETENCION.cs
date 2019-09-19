using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades
{
    public class BE_MAE_RETENCION
    {
        private int _n_id;
        private string _c_des;
        private double? _n_tas = null;
        private int? _n_idcueconcom = null;
        private int? _n_idcueconven = null;
        
        public int n_idret
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
        public string c_des
        {
            get { return _c_des; }
            set { _c_des = value; }
        }
        public double? n_tas
        {
            get { return _n_tas; }
            set { _n_tas = value; }
        }
        public int? n_idcueconcom
        {
            get { return _n_idcueconcom; }
            set { _n_idcueconcom = value; }
        }
        public int? n_idcueconven
        {
            get { return _n_idcueconven; }
            set { _n_idcueconven = value; }
        }
    }
}
