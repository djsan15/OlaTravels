using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Net.NetworkInformation;
using System.Threading;
using System.IO.IsolatedStorage;

namespace OlaTravel
{
    public partial class splash : PhoneApplicationPage
    {
        bool network = NetworkInterface.GetIsNetworkAvailable();
        IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
        public splash()
        {
            InitializeComponent();
            this.Loaded += splash_Loaded;
        }

        void splash_Loaded(object sender, RoutedEventArgs e)
        {
            
            Thread.Sleep(2000);
            if (network)
            {
                if (!settings.Contains("Login"))
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                else
                    NavigationService.Navigate(new Uri("/RootPage.xaml", UriKind.Relative));
            }
            else
                MessageBox.Show("No network detected!");
        }
        
    }
}