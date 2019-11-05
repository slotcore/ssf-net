using SIAC_Datos.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_Datos.Models.Maestros
{
    public class FinPeriodo: ObjectBase
    {

        private int _n_idfinperiodo;

        private string _c_descripcion;

        private string _c_codsun;

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

        public int n_idfinperiodo
        {
            get
            {
                return _n_idfinperiodo;
            }

            set
            {
                if (value != _n_idfinperiodo)
                {
                    _n_idfinperiodo = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
