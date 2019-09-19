using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Estacionamiento
{
    public class BE_EST_MOVIMIENTOSDET
    {
        private int _n_idmov;
        private int _n_idser;
        private double _n_impbru;
        private double _n_impigv;
        private double _n_imptot;
        private int _n_idunimed;
        private double _n_can;
        private double _n_preuni;
        public int n_idmov
        {
            get { return _n_idmov; }
            set { _n_idmov = value; }
        }
        public int n_idser
        {
            get { return _n_idser; }
            set { _n_idser = value; }
        }
        public double n_impbru
        {
            get { return _n_impbru; }
            set { _n_impbru = value; }
        }
        public double n_impigv
        {
            get { return _n_impigv; }
            set { _n_impigv = value; }
        }
        public double n_imptot
        {
            get { return _n_imptot; }
            set { _n_imptot = value; }
        }
        public int n_idunimed
        {
            get { return _n_idunimed; }
            set { _n_idunimed = value; }
        }
        public double n_can
        {
            get { return _n_can; }
            set { _n_can = value; }
        }
        public double n_preuni
        {
            get { return _n_preuni; }
            set { _n_preuni = value; }
        }
    }
}
