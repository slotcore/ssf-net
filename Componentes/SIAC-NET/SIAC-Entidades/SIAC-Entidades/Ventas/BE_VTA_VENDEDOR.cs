using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Ventas
{
    public class BE_VTA_VENDEDOR
    {
        private int _n_id;
        private int _n_idemp;
        private int _n_idper;
        private double _n_impbas;
        private double _n_porcom;

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
        public int n_idper
        {
            get { return _n_idper; }
            set { _n_idper = value; }
        }
        public double n_impbas
        {
            get { return _n_impbas; }
            set { _n_impbas = value; }
        }
        public double n_porcom
        {
            get { return _n_porcom; }
            set { _n_porcom = value; }
        }
    }
}
