using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Ventas
{
    public class BE_VTA_PEDIDOCLIDET
    {
        private int _n_idped;
        private int _n_idite;
        private int _n_idunimed;
        private double _n_can;
        private double _n_impbru;
        private double _n_impigv;
        private double _n_imptot;
        private DateTime _d_fchent;
        private int _n_entregado;
        
        public int n_idped
        {
            get { return _n_idped; }
            set { _n_idped = value; }
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
        public double n_can
        {
            get { return _n_can; }
            set { _n_can = value; }
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
        public DateTime d_fchent
        {
            get { return _d_fchent; }
            set { _d_fchent = value; }
        }
        public int n_entregado
        {
            get { return _n_entregado; }
            set { _n_entregado = value; }
        }
    }
}
