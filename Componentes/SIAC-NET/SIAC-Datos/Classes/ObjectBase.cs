using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SIAC_Datos.Classes
{
    public class ObjectBase : INotifyPropertyChanged
    {

        #region Propiedades Privadas
        protected bool _IsNew;

        protected bool _IsOld;
        #endregion

        #region Propiedades Publicas
        public bool IsNew
        {
            get
            {
                return _IsNew;
            }

            set
            {
                if (value != _IsNew)
                {
                    _IsNew = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool IsOld
        {
            get
            {
                return _IsOld;
            }

            set
            {
                if (value != _IsOld)
                {
                    _IsOld = value;
                    NotifyPropertyChanged();
                }
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                IsOld = true;
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void Insert(MySqlConnection connection, MySqlTransaction mySqlTransaction) {
            throw new NotImplementedException();
        }

        protected virtual void Insert()
        {
            throw new NotImplementedException();
        }

        protected virtual void Update(MySqlConnection connection, MySqlTransaction mySqlTransaction) {
            throw new NotImplementedException();
        }

        protected virtual void Update() {
            throw new NotImplementedException();
        }

        public void Save(MySqlConnection connection, MySqlTransaction mySqlTransaction)
        {
            if (IsNew)
            {
                Insert(connection, mySqlTransaction);
            }
            else
            {
                if (IsOld)
                {
                    Update(connection, mySqlTransaction);
                }
            }
        }

        public void Save()
        {
            if (IsNew)
            {
                Insert();
            }
            else
            {
                if (IsOld)
                {
                    Update();
                }
            }
        }

        protected virtual void Delete(MySqlConnection connection, MySqlTransaction mySqlTransaction)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete()
        {
            throw new NotImplementedException();
        }
    }
}
