 
using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ReportViews;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI;
using Windows.UI.Xaml.Controls;
//using Microsoft.UI.Xaml.Navigation;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Freedom.UICore.Views.ReportViews
{
    public sealed partial class UnoWebPage : Page, IViewModel<UnoWebViewModel>
    {
        public UnoWebPage()
        {
            this.InitializeComponent();

            //ViewModel = AppEssential.ServiceProvider.GetRequiredService<UnoWebViewModel>();


            //try
            //{
            //    Uri uri = new Uri("https://translate.google.com.py/?hl=es&tab=rT&sl=es&tl=en&op=translate");
            //    webView.Source = uri;
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex.Message);
            //}
           // NavigateToGoogle();
        }

        public UnoWebViewModel ViewModel { get; set; }

        public   void InitializeWebView(string navigatePath)
        {
            //txtUrl.Text = navigatePath;
            string uri = @"file:///" + navigatePath;
            //await webView.EnsureCoreWebView2Async(null);
            //if (webView != null && webView.CoreWebView2 != null)
            //{
            //    webView.CoreWebView2.Navigate(uri);
            //}

            //await webView.EnsureCoreWebView2Async();

            //if (webView != null && webView.CoreWebView2 != null)
            //{
            //    webView.CoreWebView2.Navigate(uri);
            //}
        }


        public   void LoadPdf(byte[] parameter)
        {



//            string pdf64 = Convert.ToBase64String(parameter);




//            string html = $"<!DOCTYPE html><html><head></head><body><div>" +
//                $"<iframe width=100% height=500 src='data:Application/pdf;base64,{pdf64}'>" +
//                $"</iframe></div></body></html>";


//            await webView.EnsureCoreWebView2Async();

//            if (webView != null && webView.CoreWebView2 != null)
//{
//                webView.CoreWebView2.NavigateToString(html);
//            }
        }



        private   void OnlyHtml()
        {

            //var html = @"
            //            <html>
            //                <head>
            //                    <title>Basic Web Page</title>
            //                </head>
            //                <body>
            //                    Hello World!
            //                </body>
            //            </html>";

            //await webView.EnsureCoreWebView2Async();

            //if (webView != null && webView.CoreWebView2 != null)
            //{
            //    webView.CoreWebView2.Navigate("https://docs.microsoft.com/en-us/microsoft-edge/webview2/get-started/winui");
            //}


        }



        private async void NavigateToGoogle()
        {
            await Task.Delay(3000);

            Uri uri = new Uri("https://translate.google.com.py/?hl=es&tab=rT&sl=es&tl=en&op=translate");
            //webView.Source = uri;
        }

        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    base.OnNavigatedTo(e);

        //    //NavigateToGoogle();
        //    //InitializeWebView(e.Parameter.ToString());
        //    //LoadPdf((byte[])e.Parameter);
        //    // OnlyHtml();
        //    //Uri uri = new Uri("https://translate.google.com.py/?hl=es&tab=rT&sl=es&tl=en&op=translate");
        //    //webView.Source = uri;


        //}

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Uri uri = new Uri("https://translate.google.com.py/?hl=es&tab=rT&sl=es&tl=en&op=translate");
            //webView.Source = uri;
        }
    }
}