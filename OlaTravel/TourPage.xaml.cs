using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Devices.Geolocation;
using System.Device.Location;
using System.Windows.Shapes;
using System.Windows.Media;
using Microsoft.Phone.Maps.Controls;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;

namespace OlaTravel
{
    public partial class TourPage : PhoneApplicationPage
    {
        string latitude = "";
        string longitude = "";
        public ObservableCollection<Location> Sresults;
        public List<Location> Tour  = new List<Location>();
        Location ss = new Location();
        IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
            
        public TourPage()
        {
            InitializeComponent();
            ShowMyLocationOnTheMap();
        }
        private async void ShowMyLocationOnTheMap()
        {
            pb.Visibility = System.Windows.Visibility.Visible;
            if(MyMap.Layers.Count!=0)
            MyMap.Layers.Remove((MapLayer)this.FindName("myLocationLayer"));
            
            Geolocator myGeolocator = new Geolocator();
            Geoposition myGeoposition = await myGeolocator.GetGeopositionAsync();
            Geocoordinate myGeocoordinate = myGeoposition.Coordinate;
            GeoCoordinate myGeoCoordinate =
            CoordinateConverter.ConvertGeocoordinate(myGeocoordinate);
            
            MyMap.Center = myGeoCoordinate;
            MyMap.ZoomLevel = 16;
            Ellipse myCircle = new Ellipse();
            myCircle.Fill = new SolidColorBrush(Colors.Blue);
            myCircle.Height = 20;
            myCircle.Width = 20;
            myCircle.Opacity = 50;
            // Create a MapOverlay to contain the circle.
            MapOverlay myLocationOverlay = new MapOverlay();
            myLocationOverlay.Content = myCircle;
            myLocationOverlay.PositionOrigin = new Point(0.5, 0.5);
            myLocationOverlay.GeoCoordinate = myGeoCoordinate;
            // Create a MapLayer to contain the MapOverlay.
            MapLayer myLocationLayer = new MapLayer();
            myLocationLayer.Add(myLocationOverlay);
            // Add the MapLayer to the Map.
            MyMap.Layers.Add(myLocationLayer);
            latitude = myGeoCoordinate.Latitude.ToString();
            longitude = myGeoCoordinate.Longitude.ToString();
            if (!settings.Contains("currlat"))
            {
                settings.Add("currlat", latitude);
                settings.Add("currlng", longitude);
            }
            else
            {
                settings["currlat"] = latitude;
                settings["currlng"] = longitude;
            }
            
            pb.Visibility = System.Windows.Visibility.Collapsed;
           
        }
       
        void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            MessageBox.Show(e.Result);
        }

        private void Rectangle_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ShowMyLocationOnTheMap();
        }

        private void Search_AutocompleteBox_Loaded(object sender, RoutedEventArgs e)
        {
            pb.Visibility = System.Windows.Visibility.Visible;

            try
            {
                Search_AutocompleteBox.FilterMode = AutoCompleteFilterMode.Contains;
                Search_AutocompleteBox.Populating += (s, args) =>
                    {

                        args.Cancel = true;

                        WebClient wc = new WebClient();

                        string prefix = HttpUtility.UrlEncode(args.Parameter);

                        Uri service = new Uri(Api.gautocomplete + "input=" + prefix + "&key=" + Api.apikey + "&location=" + latitude + "," + longitude + "&radius=1000000" );

                        wc.DownloadStringCompleted += DownloadStringCompleted;

                        wc.DownloadStringAsync(service, s);

                    };
            }
            catch(Exception )
            {
                //nothing
            }
        }

        private void DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            AutoCompleteBox acb = e.UserState as AutoCompleteBox;
            try
            {
                if (acb != null && e.Error == null && !e.Cancelled && !string.IsNullOrEmpty(e.Result))
                {

                    List<string> suggestions = new List<string>();
                    

                    var sa = JsonConvert.DeserializeObject<GAutoComplete>(e.Result);

                    foreach (var x in sa.predictions)
                    {

                        suggestions.Add(x.description);

                    }

                    if (suggestions.Count > 0)
                    {

                        acb.ItemsSource = suggestions;

                        acb.PopulateComplete();

                    }

                }
            }
            catch (Exception) { }
            pb.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Search_AutocompleteBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Enter)
            {
                Api.x += 1;
                WebClient wc = new WebClient();
                wc.DownloadStringAsync(new Uri(Api.geocode + "address=" + Search_AutocompleteBox.Text));
                wc.DownloadStringCompleted+=wc_DownloadStringCompleted1;
            }
            
        }

        private void wc_DownloadStringCompleted1(object sender, DownloadStringCompletedEventArgs e)
        {
             List<string> results = new List<string>();
             Sresults = new ObservableCollection<Location>();
            var sa = JsonConvert.DeserializeObject<GGeocode>(e.Result);
            foreach (var x in sa.results)
            {
                Location l = new Location();
                l.fullname = x.formatted_address;
                l.lat = x.geometry.location.lat;
                l.lng = x.geometry.location.lng;
                Sresults.Add(l);
                ss.lat = l.lat;
                ss.lng = l.lng;
                ss.fullname = l.fullname;
            }
            
            Search_List.DataContext = Sresults;
            double lat = sa.results[0].geometry.location.lat;
            double lng = sa.results[0].geometry.location.lng;
            GeoCoordinate g = new GeoCoordinate(lat, lng);
            MyMap.Center = g;
            MyMap.ZoomLevel = 15;
        }
            

        private void Search_StackPanel_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string lg = ((sender as StackPanel).FindName("lngg") as TextBlock).Text;
            string lt = ((sender as StackPanel).FindName("latt") as TextBlock).Text;
            double lat = Convert.ToDouble(lt);
            double lng = Convert.ToDouble(lg);
            string name = ((sender as StackPanel).FindName("Name") as TextBlock).Text;
            Api.x += 1;
            GeoCoordinate g = new GeoCoordinate(lat, lng);
            MyMap.Center = g;
            MyMap.ZoomLevel = 15;
            ss.fullname = name;
            ss.lat = lat;
            ss.lng = lng;
        }

        

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Location a = new Location();
            a = ss;
            Tour.Add(a);
            
           // MessageBox.Show(Tour.ElementAt(0).fullname+ Tour.ElementAt(1).fullname);
            CustomMessageBox messageBox = new CustomMessageBox()
            {
                Title = "Save or Add more?",
                Caption = "Meeting",
                Message = "Do you want to save the current tour or add more places to it?",
                LeftButtonContent = "save",
                RightButtonContent = "add more",
                IsFullScreen = false
            };
            messageBox.Show();
            messageBox.Dismissed += async(s1, e1) =>
           {
               switch (e1.Result)
               {
                   case CustomMessageBoxResult.LeftButton:
                      await Tour.Save("CurTourXML.xml");
                       NavigationService.Navigate(new Uri("/RootPage.xaml", UriKind.Relative));
                       break;
                   case CustomMessageBoxResult.RightButton:
                       MessageBox.Show("This feature is coming soon");
                       break;
                   default: break;
               }
           };

        }
    }
}