using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Models
{
    public class ViewItems
    {
        public ViewItems(string pageKey, string contentControlName, UserControl viewControl)
        {
            PageKey = pageKey;
            ContentControlName = contentControlName;
            ViewControl = viewControl;
        }

        public string PageKey { get; set; }

        public string ContentControlName { get; set; }

        public UserControl ViewControl { get; set; }
    }
}