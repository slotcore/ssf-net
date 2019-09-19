using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Gestion
{
    public class BE_GES_PLANPRODUCCION
    {
        int _n_idemp;
        int _n_id;
        string _c_des;
        DateTime _d_fchini;
        DateTime _d_fchfin;
        int _n_mesini;
        int _n_ano;
        int _n_activo;
        int _n_idplaven;
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
        public string c_des
        {
            get { return _c_des; }
            set { _c_des = value; }
        }
        public DateTime d_fchini
        {
            get { return _d_fchini; }
            set { _d_fchini = value; }
        }
        public DateTime d_fchfin
        {
            get { return _d_fchfin; }
            set { _d_fchfin = value; }
        }
        public int n_mesini
        {
            get { return _n_mesini; }
            set { _n_mesini = value; }
        }
        public int n_ano
        {
            get { return _n_ano; }
            set { _n_ano = value; }
        }
        public int n_activo
        {
            get { return _n_activo; }
            set { _n_activo = value; }
        }
        public int n_idplaven
        {
            get { return _n_idplaven; }
            set { _n_idplaven = value; }
        }
    }
}
