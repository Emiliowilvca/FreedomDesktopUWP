using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Freedom.Core
{
    public class ObservableCollectionEx<T> : ObservableCollection<T> where T : INotifyPropertyChanged
    {
        public ObservableCollectionEx(IEnumerable<T> initialData) : base(initialData)
        {
            Init();
        }

        public ObservableCollectionEx()
        {
            Init();
        }

        private void Init()
        {
            foreach (T item in Items)
                item.PropertyChanged += ItemOnPropertyChanged;

            CollectionChanged += FullObservableCollectionCollectionChanged;
        }

        private void FullObservableCollectionCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (T item in e.NewItems)
                {
                    if (item != null)
                        item.PropertyChanged += ItemOnPropertyChanged;
                }
            }

            if (e.OldItems != null)
            {
                foreach (T item in e.OldItems)
                {
                    if (item != null)
                        item.PropertyChanged -= ItemOnPropertyChanged;
                }
            }
        }

        private void ItemOnPropertyChanged(object sender, PropertyChangedEventArgs e)
            => ItemChanged?.Invoke(sender, e);

        public event PropertyChangedEventHandler ItemChanged;
    }
}