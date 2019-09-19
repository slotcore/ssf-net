using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Sistema
{
    public class BE_SYS_AUDITORIA
    {
        private int _n_idemp;
        private int _n_id;
        private int _n_idano;
        private int _n_idmes;
        private DateTime _d_fchreg;
        private DateTime _h_horreg;
        private int _n_idtipope;
        private int _n_idusu;
        public int n_idemp
        {
            get { return _n_idemp; }
            set { _n_idemp = value; }
        }
        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
        public int n_idano
        {
            get { return _n_idano; }
            set { _n_idano = value; }
        }
        public int n_idmes
        {
            get { return _n_idmes; }
            set { _n_idmes = value; }
        }
        public DateTime d_fchreg
        {
            get { return _d_fchreg; }
            set { _d_fchreg = value; }
        }
        public DateTime h_horreg
        {
            get { return _h_horreg; }
            set { _h_horreg = value; }
        }
        public int n_idtipope
        {
            get { return _n_idtipope; }
            set { _n_idtipope = value; }
        }
        public int n_idusu
        {
            get { return _n_idusu; }
            set { _n_idusu = value; }
        }
    }
}
