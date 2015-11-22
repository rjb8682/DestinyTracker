using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Automation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using DestinyTracker.CustomControls;
using DestinyTracker.Models;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DestinyTracker
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AppShell : Page
    {
        public Frame AppFrame => frame;
        public static bool HasHardwareButton;
        public static string DeviceFamily = string.Empty;

        private readonly ObservableCollection<NavMenuItem> _navigationList = new ObservableCollection<NavMenuItem>(
            new []
            {
                new NavMenuItem
                {
                    Symbol = Symbol.Home,
                    Label = "Home",
                    DestPage = typeof(MainPage)
                }, 
            }); 

        public AppShell()
        {
            this.InitializeComponent();

            SystemNavigationManager.GetForCurrentView().BackRequested += SystemNavigationManager_BackRequested;
            HasHardwareButton = ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons");
            DeviceFamily = Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily;

            NavMenuList.ItemsSource = _navigationList;
            NavMenuList.SelectedIndex = 0;
        }

        private void SystemNavigationManager_BackRequested(object sender, BackRequestedEventArgs e)
        {
            var handled = e.Handled;
            BackRequested(ref handled);
            e.Handled = handled;
        }

        private void BackRequested(ref bool handled)
        {
            // Get a hold of the current frame so that we can inspect the app back stack.

            if (AppFrame == null)
                return;

            // Check to see if this is the top-most page on the app back stack.
            if (!AppFrame.CanGoBack || handled) return;
            // If not, set the event to handled and go back to the previous page in the app.
            handled = true;
            AppFrame.GoBack(new CommonNavigationTransitionInfo());
        }

        private void NavMenuItemContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            if (!args.InRecycleQueue && args.Item != null && args.Item is NavMenuItem)
            {
                args.ItemContainer.SetValue(AutomationProperties.NameProperty, ((NavMenuItem)args.Item).Label);
            }
            else
            {
                args.ItemContainer.ClearValue(AutomationProperties.NameProperty);
            }
        }

        private void NavMenuList_ItemInvoked(object sender, ListViewItem listViewItem)
        {
            var item = (NavMenuItem)((NavMenuListView)sender).ItemFromContainer(listViewItem);

            NavMenuList.SelectedItem = null;

            if (item?.DestPage != null && item.DestPage != this.AppFrame.CurrentSourcePageType)
            {
                AppFrame.Navigate(item.DestPage, item.Arguments, new DrillInNavigationTransitionInfo());
            }
        }

        private void OnNavigatingToPage(object sender, NavigatingCancelEventArgs e)
        {

            if (e.NavigationMode == NavigationMode.Back)
            {
                //return;
                var item = (from p in this._navigationList where p.DestPage == e.SourcePageType select p).SingleOrDefault();
                if (item == null || this.AppFrame.BackStackDepth > 0)
                {
                    // In cases where a page drills into sub-pages then we'll highlight the most recent
                    // navigation menu item that appears in the BackStack
                    foreach (var entry in this.AppFrame.BackStack.Reverse())
                    {
                        item = (from p in this._navigationList where p.DestPage == entry.SourcePageType select p).SingleOrDefault();
                        if (item != null)
                            break;
                    }
                }

                var container = (ListViewItem)NavMenuList.ContainerFromItem(item);

                // While updating the selection state of the item prevent it from taking keyboard focus.  If a
                // user is invoking the back button via the keyboard causing the selected nav menu item to change 
                // then focus will remain on the back button.
                if (container != null) container.IsTabStop = false;
                NavMenuList.SetSelectedItem(container);
                if (container != null) container.IsTabStop = true;
            }
        }

        private void OnNavigatedToPage(object sender, NavigationEventArgs e)
        {
            // After a successful navigation set keyboard focus to the loaded page
            if (e.Content is Page && e.Content != null)
            {
                var control = (Page)e.Content;
                control.Loaded += Page_Loaded;
            }

            SetBackButtonStatus();
        }

        private void AppShell_OnKeyDown(object sender, KeyRoutedEventArgs e)
        {
            
        }

        private static void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ((Page)sender).Focus(FocusState.Programmatic);
            ((Page)sender).Loaded -= Page_Loaded;
        }

        private void SetBackButtonStatus()
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = (frame.CanGoBack && !HasHardwareButton) ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }
    }
}
