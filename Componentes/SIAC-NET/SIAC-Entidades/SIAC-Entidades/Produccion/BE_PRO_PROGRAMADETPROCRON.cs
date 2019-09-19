using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Produccion
{
    public class BE_PRO_PROGRAMADETPROCRON
    {
        private int _n_idpro;
        private int _n_idordpro;
        private int _n_idproducto;
        private DateTime _d_fchpro;
        private DateTime _d_fchent;
        private double _n_can;
        public int n_idpro
        {
            get { return _n_idpro; }
            set { _n_idpro = value; }
        }
        public int n_idordpro
        {
            get { return _n_idordpro; }
            set { _n_idordpro = value; }
        }
        public int n_idproducto
        {
            get { return _n_idproducto; }
            set { _n_idproducto = value; }
        }
        public DateTime d_fchpro
        {
            get { return _d_fchpro; }
            set { _d_fchpro = value; }
        }
        public DateTime d_fchent
        {
            get { return _d_fchent; }
            set { _d_fchent = value; }
        }
        public double n_can
        {
            get { return _n_can; }
            set { _n_can = value; }
        }
    }
}
