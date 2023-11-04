using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace SharedWPF
{
    public static class DependencyObjectExtension
    {
        public static T? FindAncestor<T>(this DependencyObject d)
        {
            if (d is T t)
            {
                return t;
            }

            DependencyObject? child = VisualTreeHelper.GetParent(d);

            if (child != null)
            {
                return FindAncestor<T>(child);
            }

            child = (d as FrameworkElement)?.Parent;

            if (child != null)
            {
                return FindAncestor<T>(child);
            }

            return default;
        }

        public static IEnumerable<T> FindDescendant<T>(this DependencyObject d)
        {
            int count = VisualTreeHelper.GetChildrenCount(d);
            for (int i = 0; i < count; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(d, i);
                if (child is T t)
                {
                    yield return t;
                }

                foreach (T subT in FindDescendant<T>(child))
                {
                    yield return subT;
                }
            }
        }
    }
}
