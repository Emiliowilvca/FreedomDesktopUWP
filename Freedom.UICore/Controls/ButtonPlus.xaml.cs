using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Freedom.UICore.Controls
{
    public sealed partial class ButtonPlus : Button
    {
        public ButtonPlus()
        {
            this.InitializeComponent();

            //if (string.IsNullOrEmpty(ContentText))
            //{
            //    FontImageMargin = new Thickness(0, 0, 0, 0);
            //}
        }

        public string ContentText
        {
            get { return (string)GetValue(ContentTextProperty); }
            set { SetValue(ContentTextProperty, value); }
        }

        public static readonly DependencyProperty ContentTextProperty =
            DependencyProperty.Register("ContentText", typeof(string), typeof(ButtonPlus),
                new PropertyMetadata(""));

        public Thickness FontImageMargin
        {
            get { return (Thickness)GetValue(FontImageMarginProperty); }
            set
            {
                SetValue(FontImageMarginProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for FontImageMargin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FontImageMarginProperty =
            DependencyProperty.Register("FontImageMargin", typeof(Thickness), typeof(ButtonPlus),
                new PropertyMetadata(new Thickness(0, 0, 0, 0)));

        public Brush BackgroundMouseOver
        {
            get { return (Brush)GetValue(BackgroundMouseOverProperty); }
            set { SetValue(BackgroundMouseOverProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BackgroundMouseOver.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackgroundMouseOverProperty =
            DependencyProperty.Register("BackgroundMouseOver", typeof(Brush), typeof(ButtonPlus),
                new PropertyMetadata(new SolidColorBrush(Colors.Gray)));

        public Brush ForegroundMouseOver
        {
            get { return (Brush)GetValue(ForegroundMouseOverProperty); }
            set { SetValue(ForegroundMouseOverProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ForegroundMouseOver.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ForegroundMouseOverProperty =
            DependencyProperty.Register("ForegroundMouseOver", typeof(Brush), typeof(ButtonPlus),
                new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        public Orientation FontImageOrientation
        {
            get { return (Orientation)GetValue(FontImageOrientationProperty); }
            set { SetValue(FontImageOrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FontImageOrientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FontImageOrientationProperty =
            DependencyProperty.Register("FontImageOrientation", typeof(Orientation), typeof(ButtonPlus),
                            new PropertyMetadata(Orientation.Horizontal));

        public FontFamily FontImageFamily
        {
            get { return (FontFamily)GetValue(FontImageFamilyProperty); }
            set { SetValue(FontImageFamilyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FontImageFamily.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FontImageFamilyProperty =
            DependencyProperty.Register("FontImageFamily", typeof(FontFamily), typeof(ButtonPlus),
                new PropertyMetadata(new FontFamily("Segoe MDL2 Assets")));

        //#Segoe MDL2 Assets
        //#Material Design Icons

        public string Glyph
        {
            get { return (string)GetValue(GlyphProperty); }
            set { SetValue(GlyphProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Glyph.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GlyphProperty =
            DependencyProperty.Register("Glyph", typeof(string), typeof(ButtonPlus),
                new PropertyMetadata(""));

        public double FontImageSize
        {
            get { return (double)GetValue(FontImageSizeProperty); }
            set { SetValue(FontImageSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FontImageSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FontImageSizeProperty =
            DependencyProperty.Register("FontImageSize", typeof(double), typeof(ButtonPlus), new PropertyMetadata(20.0));
    }
}