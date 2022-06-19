using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Freedom.UICore.Models
{
    public class MenuCategory
    {
        private Type _pageType;

        public MenuCategory()
        {
        }

        public MenuCategory(string name, string iconName, Type pageType, ObservableCollection<MenuCategory> childrens = null)
        {
            Name = name;
            IconName = iconName;
            PageType = pageType;
            if (childrens != null)
            {
                childrens.ToList().ForEach(child => ChildrenMenus.Add(child));
            }
        }

        public Type PageType
        {
            get => _pageType;
            set
            {
                _pageType = value;
                KeyName = PageType?.Name ?? "";
            }
        }

        public string KeyName { get; set; }

        public string Name { get; set; }

        public string IconName { get; set; }

        public ObservableCollection<MenuCategory> ChildrenMenus { get; set; } = new ObservableCollection<MenuCategory>();
    }
}