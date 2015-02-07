using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO.IsolatedStorage;
using System.ComponentModel;

namespace OlaTravel
{
    public partial class RootPage : PhoneApplicationPage
    {
        IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
        public RootPage()
        {
            InitializeComponent();
            piv.IsLocked = true;
            
        }

        private void add_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/TourPage.xaml", UriKind.Relative));
        }
        private async Task loadtours()
        {
            var currenttours= await IsolatedStorageOperations.Load< List<Location> >("CurTourXML.xml");
            CurrTours_List.DataContext = currenttours;
            if (currenttours.Count == 0)
            {
                ImageBrush s = new ImageBrush();
                s.ImageSource = new BitmapImage(new Uri("/Assets/tour.jpg", UriKind.Relative));
                LayoutRoot.Background = s;
            }
            else if(Api.x==0)
            {
                //Nothinhg
            }
            else
                BookBut.Visibility = System.Windows.Visibility.Visible;
        }
        protected async override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await loadtours();
        }

        private void Search_StackPanel_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }
        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            Application.Current.Terminate();
        }
        private void logout_click(object sender, EventArgs e)
        {
            settings.Remove("Login"); //settings.Remove("email"); settings.Remove("pass");
            settings.Save();
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void BookBut_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/booktour.xaml", UriKind.Relative));
        }
     
    }
}