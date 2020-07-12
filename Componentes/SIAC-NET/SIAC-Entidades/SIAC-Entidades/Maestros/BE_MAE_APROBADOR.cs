using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Entidades.Maestros
{
    public class BE_MAE_APROBADOR
    {
        private int _n_id;
        private int _n_idemp;
        private int _n_idusu;
        private int _n_idfor;
        private int _n_idare;
        private int _n_activo;
        private string _desusuario;
        private string _desarea;

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

        public int n_idusu
        {
            get { return _n_idusu; }
            set { _n_idusu = value; }
        }

        public int n_idfor
        {
            get { return _n_idfor; }
            set { _n_idfor = value; }
        }

        public int n_idare
        {
            get { return _n_idare; }
            set { _n_idare = value; }
        }

        public int n_activo
        {
            get { return _n_activo; }
            set { _n_activo = value; }
        }

        public string desusuario
        {
            get { return _desusuario; }
            set { _desusuario = value; }
        }

        public string desarea
        {
            get { return _desarea; }
            set { _desarea = value; }
        }

    }
}
