using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Logistica
{
    public class BE_LOG_COMPRASDET
    {
        int _n_idcom;
        int _n_iditem;
        int _n_idunimed;
        double _n_canpro;
        double _n_preunibru;
        double _n_idpordsc;
        double _n_impdsc;
        double _n_preuni;
        double _n_imptot;
        int _n_idtipafeigv;
        public int n_idcom
        {
            get { return _n_idcom; }
            set { _n_idcom = value; }
        }
        public int n_iditem
        {
            get { return _n_iditem; }
            set { _n_iditem = value; }
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
        public double n_preunibru
        {
            get { return _n_preunibru; }
            set { _n_preunibru = value; }
        }
        public double n_idpordsc
        {
            get { return _n_idpordsc; }
            set { _n_idpordsc = value; }
        }
        public double n_impdsc
        {
            get { return _n_impdsc; }
            set { _n_impdsc = value; }
        }
        public double n_preuni
        {
            get { return _n_preuni; }
            set { _n_preuni = value; }
        }
        public double n_imptot
        {
            get { return _n_imptot; }
            set { _n_imptot = value; }
        }
        public int n_idtipafeigv
        {
            get { return _n_idtipafeigv; }
            set { _n_idtipafeigv = value; }
        }
    }
}
