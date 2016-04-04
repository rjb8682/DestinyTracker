using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using DestinyTracker.Helpers;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DestinyTracker
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignInPage : Page
    {
        public static event EventHandler loginSuccesful;
        public string CurrentGamerTag;

        public SignInPage()
        {
            this.InitializeComponent();
        }

        private async void SubmitButton_OnClick(object sender, RoutedEventArgs e)
        {
            CurrentGamerTag = GamerTagBox.Text;

            var memberId = string.Empty;
            bool gotMemberId;

            try
            {
                memberId = await Utilities.GetMemberId(CurrentGamerTag);
                gotMemberId = true;
            }
            catch (UnauthorizedAccessException ex)
            {
                gotMemberId = false;
                await new MessageDialog(ex.Message).ShowAsync();
            }

            if (gotMemberId)
            {
                // Got the correct MemberId
                App.MemberId = memberId;
                loginSuccesful?.Invoke(this, null);
            }
            else
            {
                // Couldn't get the memberId. Try again
                GamerTagBox.Text = string.Empty;
                memberId = string.Empty;
                GamerTagBox.Focus(FocusState.Keyboard);
            }
        }

        private void GamerTagBox_OnKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key != VirtualKey.Enter) return;

            SubmitButton_OnClick(null, null);
        }
    }
}
