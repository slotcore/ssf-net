using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Gestion
{
    public class BE_GES_PLANVENTAS
    {
        int _n_idemp;
        int _n_id;
        int _n_idano;
        int _n_idmes;
        string _c_des;
        DateTime _d_fchcre;
        int _n_idmesini;
        int _n_activo;

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
        public string c_des
        {
            get { return _c_des; }
            set { _c_des = value; }
        }
        public DateTime d_fchcre
        {
            get { return _d_fchcre; }
            set { _d_fchcre = value; }
        }
        public int n_idmesini
        {
            get { return _n_idmesini; }
            set { _n_idmesini = value; }
        }
        public int n_activo
        {
            get { return _n_activo; }
            set { _n_activo = value; }
        }
    }
}
