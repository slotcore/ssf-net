using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Cooperativa
{
    public class BE_COO_CARGOSDET
    {
        private int _n_idemp;
        private int _n_idcar;
        private int _n_idsoc;
        private int _n_idpue;
        private int _n_idcon;
        private double _n_can;
        private double _n_impbru;
        private double _n_impnet;
        private double _n_imptotbru;
        private double _n_imptotnet;
        private int _n_idcor;
        private int _n_pagado;
        private int _n_iddocpag;

        public int n_idemp
        {
            get { return _n_idemp; }
            set { _n_idemp = value; }
        }
        public int n_idcar
        {
            get { return _n_idcar; }
            set { _n_idcar = value; }
        }
        public int n_idsoc
        {
            get { return _n_idsoc; }
            set { _n_idsoc = value; }
        }
        public int n_idpue
        {
            get { return _n_idpue; }
            set { _n_idpue = value; }
        }
        public int n_idcon
        {
            get { return _n_idcon; }
            set { _n_idcon = value; }
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
        public double n_impnet
        {
            get { return _n_impnet; }
            set { _n_impnet = value; }
        }
        public double n_imptotbru
        {
            get { return _n_imptotbru; }
            set { _n_imptotbru = value; }
        }
        public double n_imptotnet
        {
            get { return _n_imptotnet; }
            set { _n_imptotnet = value; }
        }
        public int n_idcor
        {
            get { return _n_idcor; }
            set { _n_idcor = value; }
        }
        public int n_pagado
        {
            get { return _n_pagado; }
            set { _n_pagado = value; }
        }
        public int n_iddocpag
        {
            get { return _n_iddocpag; }
            set { _n_iddocpag = value; }
        }
    }
}
