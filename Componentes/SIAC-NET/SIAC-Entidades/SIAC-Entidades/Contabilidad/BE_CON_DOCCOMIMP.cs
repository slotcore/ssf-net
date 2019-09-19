using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SIAC_Entidades.Contabilidad
{
    public class BE_CON_DOCCOMIMP
    {
        private int _n_id;
        private int _n_idimp;
        private int _n_idtipdoc;
        private int _n_idcuecom;
        private int _n_idcueven;
        private double _n_portas;
        private int _n_idemp;

        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
        public int n_idimp
        {
            get { return _n_idimp; }
            set { _n_idimp = value; }
        }
        public int n_idtipdoc
        {
            get { return _n_idtipdoc; }
            set { _n_idtipdoc = value; }
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
        public double n_portas
        {
            get { return _n_portas; }
            set { _n_portas = value; }
        }
        public int n_idemp
        {
            get { return _n_idemp; }
            set { _n_idemp = value; }
        }
    }
}
