using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Estacionamiento
{
    public class BE_EST_LIQUIDACION
    {
        private int _n_idemp;
        private int _n_id;
        private int _n_idpla;
        private int _n_ano;
        private int _n_mes;
        private int _n_idcaj;
        private DateTime _d_fchemi;
        private DateTime _d_fchliq;
        private double _n_importe;
        private string _c_obs;
        private int _n_numdoccob;
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
        public int n_idpla
        {
            get { return _n_idpla; }
            set { _n_idpla = value; }
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
        public int n_idcaj
        {
            get { return _n_idcaj; }
            set { _n_idcaj = value; }
        }
        public DateTime d_fchemi
        {
            get { return _d_fchemi; }
            set { _d_fchemi = value; }
        }
        public DateTime d_fchliq
        {
            get { return _d_fchliq; }
            set { _d_fchliq = value; }
        }
        public double n_importe
        {
            get { return _n_importe; }
            set { _n_importe = value; }
        }
        public string c_obs
        {
            get { return _c_obs; }
            set { _c_obs = value; }
        }
        public int n_numdoccob
        {
            get { return _n_numdoccob; }
            set { _n_numdoccob = value; }
        }
    }
}
