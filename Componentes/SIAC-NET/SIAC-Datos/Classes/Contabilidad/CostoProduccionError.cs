using MySql.Data.MySqlClient;
using SIAC_Datos.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIAC_DATOS.Classes.Contabilidad
{
    public class CostoProduccionError : ObjectBase
    {
        #region constructor
        public CostoProduccionError()
        {
            _IsNew = true;
        }

        #endregion

        #region propiedades

        private string _CodItem;
        private string _DesItem;
        private string _Error;
        private string _DesAlm;
        private string _DesFechMov;
        private string _DesMov;

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

        public string Error
        {
            get
            {
                return _Error;
            }

            set
            {
                if (value != _Error)
                {
                    _Error = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string DesAlm
        {
            get
            {
                return _DesAlm;
            }

            set
            {
                if (value != _DesAlm)
                {
                    _DesAlm = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string DesFechMov
        {
            get
            {
                return _DesFechMov;
            }

            set
            {
                if (value != _DesFechMov)
                {
                    _DesFechMov = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string DesMov
        {
            get
            {
                return _DesMov;
            }

            set
            {
                if (value != _DesMov)
                {
                    _DesMov = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion
    }
}
