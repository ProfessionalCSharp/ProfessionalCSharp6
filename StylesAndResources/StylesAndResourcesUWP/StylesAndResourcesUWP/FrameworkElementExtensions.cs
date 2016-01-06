using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace StylesAndResourcesUWP
{
    public static class FrameworkElementExtensions
    {
        public static object TryFindResource(this FrameworkElement e, string key)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));
            if (key == null) throw new ArgumentNullException(nameof(key));

            if (e.Resources.ContainsKey(key))
            {
                return e.Resources[key];
            }
            else
            {
                var parent = e.Parent as FrameworkElement;
                if (parent == null) return null;
                return TryFindResource(parent, key);
            }
        }
    }

}
