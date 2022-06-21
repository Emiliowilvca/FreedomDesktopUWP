using Freedom.Frontend.Models.Bindable;
using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.BankViews;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Uwp.UI.Controls;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.BankViews
{
    public sealed partial class BankDepositPage : Page, IViewModel<BankDepositViewModel>
    {
        public BankDepositPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<BankDepositViewModel>();
        }

        public BankDepositViewModel ViewModel { get; set; }

        private void HyperlinkButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var p = ((HyperlinkButton)sender).DataContext as BankDepositDetailBind;

            if (p != null)
            {
                ViewModel.DeleteItemCommand.Execute(p);
            }
        }

        private void _grid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            
        }

        //private static bool IsCtrlKeyPressed()
        //{
        //    var ctl = AppEssential.MainWindow.CoreWindow.GetKeyState(VirtualKey.Control);
        //   // var ctrlState = CoreWindow.GetForCurrentThread().GetKeyState(VirtualKey.Control);
        //    return (ctl & CoreVirtualKeyStates.Down) == CoreVirtualKeyStates.Down;
        //}

        private void _grid_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            //if (IsCtrlKeyPressed())
            //{
            //    switch (e.Key)
            //    {
            //        case VirtualKey.P:   break;
            //        case VirtualKey.A:
            //            _grid.BeginEdit();
            //            break;
            //        case VirtualKey.S: break;
            //    }
            //}
        }
    }
}