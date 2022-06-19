﻿using Freedom.UICore.Helpers;
using Freedom.UICore.Interface;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Implement
{
    public class FocusService : IFocusService
    {
        public void SetTextboxFocus(string textboxName)
        {
            var textbox = RootElementHelper.FindChild<TextBox>(AppEssential.MainWindow.Content, textboxName);

            if (textbox == null || !textbox.IsEnabled)
                return;
            textbox.Focus(FocusState.Keyboard);
        }
    }
}