using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Collections;

namespace SIAC_Datos.Classes
{
    public class ObservableListSource<T> : ObservableCollection<T>, IListSource
            where T : ObjectBase
    {
        private IBindingList _bindingList;
        private List<T> _removeList = new List<T>();

        bool IListSource.ContainsListCollection { get { return false; } }

        IList IListSource.GetList()
        {
            return _bindingList ?? (_bindingList = this.ToBindingList());
        }

        public bool RemoveItem(T item)
        {
            _removeList.Add(item);
            return this.Remove(item);
        }

        public List<T> GetRemoveItems()
        {
            return _removeList;
        }

        public void RemoveAll()
        {
            foreach (var item in this)
            {
                _removeList.Add(item);
            }
            this.Clear();
        }
    }
}
