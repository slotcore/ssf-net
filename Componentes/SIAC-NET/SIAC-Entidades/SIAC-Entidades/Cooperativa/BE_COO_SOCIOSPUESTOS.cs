using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Cooperativa
{
    public class BE_COO_SOCIOSPUESTOS
    {
        private int _n_idemp;
        private int _n_id;
        private int _n_idsoc;
        private int _n_idpue;
        private string _c_puesto;
        private string _c_numctt;
        private DateTime ?_d_fchini;
        private DateTime ?_d_fchfin;
        private int _n_activo;
        private int _n_idtipdocemi;
        private DateTime ?_d_fchter;
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
        public string c_puesto
        {
            get { return _c_puesto; }
            set { _c_puesto = value; }
        }
        public string c_numctt
        {
            get { return _c_numctt; }
            set { _c_numctt = value; }
        }
        public DateTime ?d_fchini
        {
            get { return _d_fchini; }
            set { _d_fchini = value; }
        }
        public DateTime ?d_fchfin
        {
            get { return _d_fchfin; }
            set { _d_fchfin = value; }
        }
        public int n_activo
        {
            get { return _n_activo; }
            set { _n_activo = value; }
        }
        public int n_idtipdocemi
        {
            get { return _n_idtipdocemi; }
            set { _n_idtipdocemi = value; }
        }
        public DateTime ?d_fchter
        {
            get { return _d_fchter; }
            set { _d_fchter = value; }
        }
    }
}
