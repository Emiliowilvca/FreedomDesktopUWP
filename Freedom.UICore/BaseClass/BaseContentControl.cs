using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.BaseClass
{
    public abstract class BaseContentControl
    {
        private List<ViewItems> ViewCollection;
        private List<Type> TypesCollection;
        private Assembly[] _viewsAssemblies;
        private Frame _contentControl;

        //   public Action OnNavigate { get; set; }

        public BaseContentControl(Assembly[] viewsAssemblies)
        {
            ViewCollection = new List<ViewItems>();
            TypesCollection = new List<Type>();
            _viewsAssemblies = viewsAssemblies;
        }




        public string GetCurrentPageName()
        {
            var control = _contentControl.Content as Frame;
            return control != null ? control.Name : "";
        }

        //public void InitContentControl(contentControlType contentControlType)
        //{
        //    string _viewContentName = GetDescription(contentControlType);
            
        //    _contentControl = RootElementHelper.FindChild<ContentControl>(AppEssential.MainWindow.Content, _viewContentName);
        //}


        public void InitContentControl(Frame frame)
        {
            _contentControl = frame;            
        }




        public void ShowControl(string pageKey, string parameter)
        {
            CheckIfViewExist(pageKey);

            UserControl CurrentView = ViewCollection.Where(x => x.PageKey == pageKey)
                                                    .Select(s => s.ViewControl)
                                                    .FirstOrDefault();
            //if (_contentControl == null)
            //{
            //    Console.WriteLine("Contentent Control is null");
            //    return;
            //}
            _contentControl.Content = CurrentView;

            //  OnNavigate?.Invoke();
        }

        #region Methods

        private void CheckIfViewExist(string pageKey)
        {
            bool containt = ViewCollection.Where(x => x.PageKey == pageKey).Count() > 0;

            if (!containt)
            {
                UserControl newView = MagicallyCreateInstance(pageKey);
                ViewCollection.Add(new ViewItems(pageKey, newView));
            }
        }

        private string GetDescription(Enum enumValue)
        {
            var descriptionAttribute = enumValue.GetType()
                .GetField(enumValue.ToString())
                .GetCustomAttributes(false)
                .SingleOrDefault(attr => attr.GetType() == typeof(DescriptionAttribute)) as DescriptionAttribute;

            return descriptionAttribute?.Description ?? "";
        }

        private Type GetTypeByClassName(string className)
        {
            if (TypesCollection.Count < 1)
            {
                TypesCollection = _viewsAssemblies.Where(a => !a.IsDynamic)
                                                  .SelectMany(s => s.GetTypes())
                                                  .ToList();
            }
            Type xType = TypesCollection.FirstOrDefault(x => x.Name.Equals(className));
            return xType;
        }

        public UserControl MagicallyCreateInstance(string pageKey)
        {
            if (TypesCollection.Count < 1)
            {
                TypesCollection = _viewsAssemblies.Where(a => !a.IsDynamic)
                                                  .SelectMany(s => s.GetTypes())
                                                  .ToList();
            }
            Type xType = TypesCollection.FirstOrDefault(x => x.Name.Equals(pageKey));
            return (UserControl)Activator.CreateInstance(xType);
        }

        #endregion Methods

        private class ViewItems
        {
            public ViewItems(string pageKey, UserControl viewControl)
            {
                PageKey = pageKey;
                ViewControl = viewControl;
            }

            public string PageKey { get; set; }

            public UserControl ViewControl { get; set; }
        }
    }
}