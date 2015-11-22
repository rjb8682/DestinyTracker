using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using DestinyTracker.Helpers;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DestinyTracker
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RootPage : Page
    {
        public Frame AppFrame => RootFrame;

        public RootPage()
        {
            this.InitializeComponent();

            var settings = new Settings("DestinyTracker");

            var apiKey = string.Empty;
            bool apiKeyExists;
            try
            {
                apiKey = settings.GetStringSetting("apiKey");
                apiKeyExists = true;
            }
            catch (KeyNotFoundException e)
            {
                apiKeyExists = false;
            }

            if (!apiKeyExists)
            {
                // Move on to the app
                App.ApiKey = apiKey;
                GoToApp(typeof(MainPage));
            }
            else
            {
                // Make the user sign in
                ShowSignInScreen();
            }
        }

        public void ShowSignInScreen()
        {
            
        }

        public void GoToApp(Type whereTo, string args = null)
        {
            RootFrame.BackStack.Clear();
            RootFrame.Navigate(typeof (AppShell));
            var shell = RootFrame.Content as AppShell;

            if (shell?.AppFrame.Content == null)
            {
                shell?.AppFrame.Navigate(whereTo, args, new DrillInNavigationTransitionInfo());
            }
        }
    }
}
