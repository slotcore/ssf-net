using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Tesoreria
{
    public class BE_TES_CONCILIACION
    {
        int _n_idemp;
        int _n_id;
        int _n_idmes;
        int _n_idcue;
        double _n_saliniban;
        DateTime _d_fchcon;
        string _c_glo;
        int _n_idper;
        int _n_ano;
        double _n_salfin;
        double _n_toting;
        double _n_totegr;
        double _n_totingnocon;
        double _n_totegrnocon;
        double _n_salfinban;

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
        public int n_idmes
        {
            get { return _n_idmes; }
            set { _n_idmes = value; }
        }
        public int n_idcue
        {
            get { return _n_idcue; }
            set { _n_idcue = value; }
        }
        public double n_saliniban
        {
            get { return _n_saliniban; }
            set { _n_saliniban = value; }
        }
        public DateTime d_fchcon
        {
            get { return _d_fchcon; }
            set { _d_fchcon = value; }
        }
        public string c_glo
        {
            get { return _c_glo; }
            set { _c_glo = value; }
        }
        public int n_idper
        {
            get { return _n_idper; }
            set { _n_idper = value; }
        }
        public int n_ano
        {
            get { return _n_ano; }
            set { _n_ano = value; }
        }
        public double n_salfin
        {
            get { return _n_salfin; }
            set { _n_salfin = value; }
        }
        public double n_toting
        {
            get { return _n_toting; }
            set { _n_toting = value; }
        }
        public double n_totegr
        {
            get { return _n_totegr; }
            set { _n_totegr = value; }
        }
        public double n_totingnocon
        {
            get { return _n_totingnocon; }
            set { _n_totingnocon = value; }
        }
        public double n_totegrnocon
        {
            get { return _n_totegrnocon; }
            set { _n_totegrnocon = value; }
        }
        public double n_salfinban
        {
            get { return _n_salfinban; }
            set { _n_salfinban = value; }
        }
    }
}
