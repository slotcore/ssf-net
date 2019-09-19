using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Contabilidad
{
    public class BE_CON_TIPDOCCOMCUE
    {
        private int _n_id;
        private int _n_idtipdoc;
        private int _n_idmon;
        private int _n_idcuecom;
        private int _n_idcueven;
        private int _n_idemp;

        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
        public int n_idtipdoc
        {
            get { return _n_idtipdoc; }
            set { _n_idtipdoc = value; }
        }
        public int n_idmon
        {
            get { return _n_idmon; }
            set { _n_idmon = value; }
        }
        public int n_idcuecom
        {
            get { return _n_idcuecom; }
            set { _n_idcuecom = value; }
        }
        public int n_idcueven
        {
            get { return _n_idcueven; }
            set { _n_idcueven = value; }
        }
        public int n_idemp
        {
            get { return _n_idemp; }
            set { _n_idemp = value; }
        }
    }
}
