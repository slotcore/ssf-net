using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Produccion
{
    public class BE_PRO_PRODUCCIONINSUMOSDET
    {
        private int _n_idpro;
        private int _n_idins;
        private int _n_idsolmat;
        private int _n_idite;
        private int _n_idunimed;
        private double _n_canteo;
        private double _n_canrea;
        private double _n_valdes;
        private double _n_pordes;
        private double _n_valite;
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
        public int n_idsolmat
        {
            get { return _n_idsolmat; }
            set { _n_idsolmat = value; }
        }
        public int n_idite
        {
            get { return _n_idite; }
            set { _n_idite = value; }
        }
        public int n_idunimed
        {
            get { return _n_idunimed; }
            set { _n_idunimed = value; }
        }
        public double n_canteo
        {
            get { return _n_canteo; }
            set { _n_canteo = value; }
        }
        public double n_canrea
        {
            get { return _n_canrea; }
            set { _n_canrea = value; }
        }
        public double n_valdes
        {
            get { return _n_valdes; }
            set { _n_valdes = value; }
        }
        public double n_pordes
        {
            get { return _n_pordes; }
            set { _n_pordes = value; }
        }
        public double n_valite
        {
            get { return _n_valite; }
            set { _n_valite = value; }
        }
    }
}
