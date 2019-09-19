using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Tesoreria
{
    public class BE_TES_TESORERIA
    {
        private int _n_idemp;
        private int _n_ano;
        private int _n_mes;
        private int _n_idlib;
        private int _n_id;
        private string _c_numreg;
        private DateTime _d_fchope;
        private int _n_idmon;
        private string _c_glo;
        private int _n_conciliado;
        private double _n_tc;
        private int _n_tipreg;
        private int _n_dongen;
        public int n_idemp
        {
            get { return _n_idemp; }
            set { _n_idemp = value; }
        }
        public int n_ano
        {
            get { return _n_ano; }
            set { _n_ano = value; }
        }
        public int n_mes
        {
            get { return _n_mes; }
            set { _n_mes = value; }
        }
        public int n_idlib
        {
            get { return _n_idlib; }
            set { _n_idlib = value; }
        }
        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
        public string c_numreg
        {
            get { return _c_numreg; }
            set { _c_numreg = value; }
        }
        public DateTime d_fchope
        {
            get { return _d_fchope; }
            set { _d_fchope = value; }
        }
        public int n_idmon
        {
            get { return _n_idmon; }
            set { _n_idmon = value; }
        }
        public string c_glo
        {
            get { return _c_glo; }
            set { _c_glo = value; }
        }
        public int n_conciliado
        {
            get { return _n_conciliado; }
            set { _n_conciliado = value; }
        }
        public double n_tc
        {
            get { return _n_tc; }
            set { _n_tc = value; }
        }
        public int n_tipreg
        {
            get { return _n_tipreg; }
            set { _n_tipreg = value; }
        }
        public int n_dongen
        {
            get { return _n_dongen; }
            set { _n_dongen = value; }
        }
        
    }
}
