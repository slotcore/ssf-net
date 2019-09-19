using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Tesoreria
{
    public class BE_TES_CANJEDET
    {
        private int _n_idcan;
        private int _n_tipo;
        private int _n_iddoc;
        private double _n_impdoc;
        private double _n_saldo;
        private int _n_caniddoc;
        private double _n_canimpdoc;
        private double _n_canimpsal;

        public int n_idcan
        {
            get { return _n_idcan; }
            set { _n_idcan = value; }
        }
        public int n_tipo
        {
            get { return _n_tipo; }
            set { _n_tipo = value; }
        }
        public int n_iddoc
        {
            get { return _n_iddoc; }
            set { _n_iddoc = value; }
        }
        public double n_impdoc
        {
            get { return _n_impdoc; }
            set { _n_impdoc = value; }
        }
        public double n_saldo
        {
            get { return _n_saldo; }
            set { _n_saldo = value; }
        }
        public int n_caniddoc
        {
            get { return _n_caniddoc; }
            set { _n_caniddoc = value; }
        }
        public double n_canimpdoc
        {
            get { return _n_canimpdoc; }
            set { _n_canimpdoc = value; }
        }
        public double n_canimpsal
        {
            get { return _n_canimpsal; }
            set { _n_canimpsal = value; }
        }
    }
}
