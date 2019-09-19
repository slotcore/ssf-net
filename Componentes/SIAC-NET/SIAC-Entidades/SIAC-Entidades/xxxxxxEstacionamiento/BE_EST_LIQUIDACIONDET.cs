using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Estacionamiento
{
    public class BE_EST_LIQUIDACIONDET
    {
        private int _n_idliq;
        private int _n_idven;
        private double _n_impcob;
        private int _n_idtip;
        private int _n_iddocori;
        public int n_idliq
        {
            get { return _n_idliq; }
            set { _n_idliq = value; }
        }
        public int n_idven
        {
            get { return _n_idven; }
            set { _n_idven = value; }
        }
        public double n_impcob
        {
            get { return _n_impcob; }
            set { _n_impcob = value; }
        }
        public int n_idtip
        {
            get { return _n_idtip; }
            set { _n_idtip = value; }
        }
        public int n_iddocori
        {
            get { return _n_iddocori; }
            set { _n_iddocori = value; }
        }
    }
}
