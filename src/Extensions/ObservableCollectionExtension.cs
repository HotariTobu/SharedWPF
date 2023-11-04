using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SharedWPF
{
    public static class ObservableCollectionExtension
    {
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> values)
        {
            foreach (T value in values)
            {
                collection.Add(value);
            }
        }

        public static void InsertRange<T>(this ObservableCollection<T> collection, int index, IEnumerable<T> values)
        {
            foreach (T value in values)
            {
                collection.Insert(index, value);
                index++;
            }
        }

        public static void RemoveRange<T>(this ObservableCollection<T> collection, IEnumerable<T> values)
        {
            foreach (T value in values)
            {
                collection.Remove(value);
            }
        }
    }
}
