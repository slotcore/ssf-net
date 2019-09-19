using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Tesoreria
{
    public class BE_TES_DESTINO
    {
        private int _n_id;
        private int _n_idcue;
        private int _n_idmod;
        private int _n_tipo;
        private int _n_idmon;
        private int _n_detalla;
        private int _n_idemp;
        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
        public int n_idcue
        {
            get { return _n_idcue; }
            set { _n_idcue = value; }
        }
        public int n_idmod
        {
            get { return _n_idmod; }
            set { _n_idmod = value; }
        }
        public int n_tipo
        {
            get { return _n_tipo; }
            set { _n_tipo = value; }
        }
        public int n_idmon
        {
            get { return _n_idmon; }
            set { _n_idmon = value; }
        }
        public int n_detalla
        {
            get { return _n_detalla; }
            set { _n_detalla = value; }
        }
        public int n_idemp
        {
            get { return _n_idemp; }
            set { _n_idemp = value; }
        }
    }
}
