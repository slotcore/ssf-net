﻿using MySql.Data.MySqlClient;
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
        protected bool _IsValid;

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

        public bool IsValid
        {
            get
            {
                return _IsValid;
            }

            set
            {
                if (value != _IsValid)
                {
                    _IsValid = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool HasOldOrNewChildren
        {
            get
            {
                return CheckHasOldOrNewChildren();
            }
        }
        #endregion

        #region EventHandler

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                IsOld = true;
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Virtual Methods

        protected virtual void Insert(MySqlConnection connection, MySqlTransaction mySqlTransaction)
        {
            throw new NotImplementedException();
        }

        protected virtual void Insert()
        {
            throw new NotImplementedException();
        }

        protected virtual void Update(MySqlConnection connection, MySqlTransaction mySqlTransaction)
        {
            throw new NotImplementedException();
        }

        protected virtual void Update()
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(MySqlConnection connection, MySqlTransaction mySqlTransaction)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete()
        {
            throw new NotImplementedException();
        }

        protected virtual bool CheckHasOldOrNewChildren()
        {
            return false;
        }

        #endregion

        #region Public Methods

        public void Save(MySqlConnection connection, MySqlTransaction mySqlTransaction)
        {
            if (IsNew)
            {
                Insert(connection, mySqlTransaction);
            }
            else
            {
                if (IsOld || HasOldOrNewChildren)
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
                if (IsOld || HasOldOrNewChildren)
                {
                    Update();
                }
            }
        }

        public void MarkAsOld()
        {
            _IsNew = false;
            _IsOld = true;
        }

        #endregion
    }
}
