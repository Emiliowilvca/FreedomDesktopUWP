﻿using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class BankSearchPage : Page, IViewModel<BankSearchViewModel>
    {
        public BankSearchPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<BankSearchViewModel>();
            DataContext = ViewModel;
        }

        public BankSearchViewModel ViewModel { get; set; }
    }
}