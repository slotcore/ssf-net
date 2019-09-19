using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Produccion
{
    public class BE_PRO_PRODUCCIONINS
    {
        private int _n_idpro;
        private int _n_idins;
        private double _n_canuti;
        private double _n_canent;
        private int _n_idunimed;
        private double _n_dife;
        private int _n_consumido;
        public int n_idpro
        {
            get { return _n_idpro; }
            set { _n_idpro = value; }
        }
        public int n_idins
        {
            get { return _n_idins; }
            set { _n_idins = value; }
        }
        public double n_canuti
        {
            get { return _n_canuti; }
            set { _n_canuti = value; }
        }
        public double n_canent
        {
            get { return _n_canent; }
            set { _n_canent = value; }
        }
        public int n_idunimed
        {
            get { return _n_idunimed; }
            set { _n_idunimed = value; }
        }
        public double n_dife
        {
            get { return _n_dife; }
            set { _n_dife = value; }
        }
        public int n_consumido
        {
            get { return _n_consumido; }
            set { _n_consumido = value; }
        }
    }
}
