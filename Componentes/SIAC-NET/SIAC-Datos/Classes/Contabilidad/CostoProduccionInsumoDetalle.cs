using SIAC_Datos.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_DATOS.Classes.Contabilidad
{
    public class CostoProduccionInsumoDetalle : ObjectBase
    {
        #region constructor
        public CostoProduccionInsumoDetalle()
        {
            _IsNew = true;
        }

        #endregion

        #region propiedades

        private int _n_idemp;
        private int _n_idite;
        private DateTime _FechMov;
        private string _CodItem;
        private string _DesItem;
        private string _Unidad;
        private double _Cantidad;
        private double _CostoUnitario;
        private double _CostoTotal;

        public int n_idemp
        {
            get
            {
                return _n_idemp;
            }

            set
            {
                if (value != _n_idemp)
                {
                    _n_idemp = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int n_idite
        {
            get
            {
                return _n_idite;
            }

            set
            {
                if (value != _n_idite)
                {
                    _n_idite = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime FechMov
        {
            get
            {
                return _FechMov;
            }

            set
            {
                if (value != _FechMov)
                {
                    _FechMov = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string CodItem
        {
            get
            {
                return _CodItem;
            }

            set
            {
                if (value != _CodItem)
                {
                    _CodItem = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string DesItem
        {
            get
            {
                return _DesItem;
            }

            set
            {
                if (value != _DesItem)
                {
                    _DesItem = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Unidad
        {
            get
            {
                return _Unidad;
            }

            set
            {
                if (value != _Unidad)
                {
                    _Unidad = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double Cantidad
        {
            get
            {
                return _Cantidad;
            }

            set
            {
                if (value != _Cantidad)
                {
                    _Cantidad = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double CostoUnitario
        {
            get
            {
                return _CostoUnitario;
            }

            set
            {
                if (value != _CostoUnitario)
                {
                    _CostoUnitario = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double CostoTotal
        {
            get
            {
                return _CostoTotal;
            }

            set
            {
                if (value != _CostoTotal)
                {
                    _CostoTotal = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion
    }
}
