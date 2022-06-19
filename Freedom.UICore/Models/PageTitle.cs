using CommunityToolkit.Mvvm.ComponentModel;

namespace Freedom.UICore.Models
{
    public class PageTitle : ObservableObject
    {
        private string _title;
        private string _glyph;
        private bool _isVisible = true;

        public PageTitle()
        {
        }

        public PageTitle(string title, string glyph)
        {
            Title = title;
            Glyph = glyph;
            IsVisible = true;
        }

        public PageTitle(string title, string glyph, bool isVisible)
        {
            Title = title;
            Glyph = glyph;
            IsVisible = isVisible;
        }

        public string Title { get => _title; set => SetProperty(ref _title, value); }

        public string Glyph { get => _glyph; set => SetProperty(ref _glyph, value); }

        public bool IsVisible { get => _isVisible; set => SetProperty(ref _isVisible, value); }

        public void SetTitle(string title, string glyph, bool isVisible)
        {
            Title = title;
            Glyph = glyph;
            IsVisible = isVisible;
        }
    }
}