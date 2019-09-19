using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Ventas
{
    public class XXXBE_VTA_PEDIDOCENDET
    {
        private int _n_idped;
        private int _n_idite;
        private string _c_codprocen;
        private int _n_idunimed;
        private double _n_canpro;
        private double _n_impuni;
        private double _n_impbru;
        private double _n_impigv;
        private double _n_imptot;
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
        public string c_codprocen
        {
            get { return _c_codprocen; }
            set { _c_codprocen = value; }
        }
        public int n_idunimed
        {
            get { return _n_idunimed; }
            set { _n_idunimed = value; }
        }
        public double n_canpro
        {
            get { return _n_canpro; }
            set { _n_canpro = value; }
        }
        public double n_impuni
        {
            get { return _n_impuni; }
            set { _n_impuni = value; }
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
