using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using DestinyTracker.Helpers;
using DestinyTracker.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DestinyTracker
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public User signedInUser;

        public MainPage()
        {
            this.InitializeComponent();

            InitializeUser();
        }

        public async void InitializeUser()
        {
            var userData = await Utilities.GetUserInformation(App.MemberId);

            signedInUser = JsonConvert.DeserializeObject<User>(userData);

            Char1Image.Source = new BitmapImage(new Uri(signedInUser.Characters[0].BackgroundUrl));
            Char1EmblemImage.Source = new BitmapImage(new Uri(signedInUser.Characters[0].EmblemUrl));
            Char2Image.Source = new BitmapImage(new Uri(signedInUser.Characters[1].BackgroundUrl));
            Char2EmblemImage.Source = new BitmapImage(new Uri(signedInUser.Characters[1].EmblemUrl));
            Char3Image.Source = new BitmapImage(new Uri(signedInUser.Characters[2].BackgroundUrl));
            Char3EmblemImage.Source = new BitmapImage(new Uri(signedInUser.Characters[2].EmblemUrl));
        }
    }
}
