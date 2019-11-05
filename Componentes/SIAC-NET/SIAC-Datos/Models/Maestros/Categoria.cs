using SIAC_Datos.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Datos.Models.Maestros
{
    public class Categoria: ObjectBase
    {
        public Categoria()
        {
            _IsNew = true;
        }


        private int _n_idcategoria;

        private string _c_descripcion;

        private string _c_codsun;

        private string _c_abrev;

        private bool _n_activo;

        public bool n_activo
        {
            get
            {
                return _n_activo;
            }

            set
            {
                if (value != _n_activo)
                {
                    _n_activo = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string c_abrev
        {
            get
            {
                return _c_abrev;
            }

            set
            {
                if (value != _c_abrev)
                {
                    _c_abrev = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string c_codsun
        {
            get
            {
                return _c_codsun;
            }

            set
            {
                if (value != _c_codsun)
                {
                    _c_codsun = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string c_descripcion
        {
            get
            {
                return _c_descripcion;
            }

            set
            {
                if (value != _c_descripcion)
                {
                    _c_descripcion = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_idcategoria
        {
            get
            {
                return _n_idcategoria;
            }

            set
            {
                if (value != _n_idcategoria)
                {
                    _n_idcategoria = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
