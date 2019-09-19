using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Contabilidad
{
    public class BE_CON_DIARIO
    {
        private int _n_id;
        private int _n_idemp;
        private int _n_ano;
        private int _n_mes;
		private int _n_lib;
		private string _c_numasi;
		private int _n_idcue;
		private double _n_tc;
		private double _n_impdebsol;
		private double _n_imphabsol;
		private double _n_impdebdol;
		private double _n_imphabdol;
		private DateTime _d_fchasi;
        private DateTime _d_orifchdoc;
        private int _n_oriid;
        private int _n_oriidtipdoc;
        private int _n_oriidtipmon;
        private string _c_orinumdoc;
        private string _c_origlo;
        private string _c_oridestipmon;
        private string _c_oridestipdoc;
        private string _c_orinomcli;
        private string _c_orinumruc;

        public int n_id
        {
            get { return _n_id; }
            set { _n_id = value; }
        }
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
        public int n_lib
        {
            get { return _n_lib; }
            set { _n_lib = value; }
        }
        public string c_numasi
        {
            get { return _c_numasi; }
            set { _c_numasi = value; }
        }
        public int n_idcue
        {
            get { return _n_idcue; }
            set { _n_idcue = value; }
        }
        public double n_tc
        {
            get { return _n_tc; }
            set { _n_tc = value; }
        }
        public double n_impdebsol
        {
            get { return _n_impdebsol; }
            set { _n_impdebsol = value; }
        }
        public double n_imphabsol
        {
            get { return _n_imphabsol; }
            set { _n_imphabsol = value; }
        }
        public double n_impdebdol
        {
            get { return _n_impdebdol; }
            set { _n_impdebdol = value; }
        }
        public double n_imphabdol
        {
            get { return _n_imphabdol; }
            set { _n_imphabdol = value; }
        }
        public DateTime d_fchasi
        {
            get { return _d_fchasi; }
            set { _d_fchasi = value; }
        }
        public DateTime d_orifchdoc
        {
            get { return _d_orifchdoc; }
            set { _d_orifchdoc = value; }
        }
        public int n_oriid
        {
            get { return _n_oriid; }
            set { _n_oriid = value; }
        }
        public int n_oriidtipdoc
        {
            get { return _n_oriidtipdoc; }
            set { _n_oriidtipdoc = value; }
        }
        public int n_oriidtipmon
        {
            get { return _n_oriidtipmon; }
            set { _n_oriidtipmon = value; }
        }
        public string c_orinumdoc
        {
            get { return _c_orinumdoc; }
            set { _c_orinumdoc = value; }
        }
        public string c_origlo
        {
            get { return _c_origlo; }
            set { _c_origlo = value; }
        }
        public string c_oridestipmon
        {
            get { return _c_oridestipmon; }
            set { _c_oridestipmon = value; }
        }
        public string c_oridestipdoc
        {
            get { return _c_oridestipdoc; }
            set { _c_oridestipdoc = value; }
        }
        public string c_orinomcli
        {
            get { return _c_orinomcli; }
            set { _c_orinomcli = value; }
        }
        public string c_orinumruc
        {
            get { return _c_orinumruc; }
            set { _c_orinumruc = value; }
        }
    }
}
