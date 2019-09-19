using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Estacionamiento
{
    public class BE_EST_CARGOSDET
    {
        private int _n_idcar;
        private int _n_idcab;
        private int _n_idser;
        private int _n_idunimed;
        private double _n_impbru;
        private double _n_impigv;
        private double _n_imptot;
        public int n_idcar
        {
            get { return _n_idcar; }
            set { _n_idcar = value; }
        }
        public int n_idcab
        {
            get { return _n_idcab; }
            set { _n_idcab = value; }
        }
        public int n_idser
        {
            get { return _n_idser; }
            set { _n_idser = value; }
        }
        public int n_idunimed
        {
            get { return _n_idunimed; }
            set { _n_idunimed = value; }
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
    }
}
