using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;

namespace OlaTravel
{
    public partial class booktour : PhoneApplicationPage
    {
        IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
        public booktour()
        {
            InitializeComponent();
            loaddetails();
        }
        private double deg2rad(double deg) {

          return (deg * Math.PI / 180.0);

            }
        private double rad2deg(double rad) {

             return (rad / Math.PI * 180.0);

           }


        private double distance(double lat1, double lon1, double lat2, double lon2, char unit) {

                double theta = lon1 - lon2;

                 double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));

                  dist = Math.Acos(dist);

                dist = rad2deg(dist);

                  dist = dist * 60 * 1.1515;

              if (unit == 'K') {

                 dist = dist * 1.609344;

              } else if (unit == 'N') {

                  dist = dist * 0.8684;

                  }

                 return (dist);

           }

        private async void loaddetails()
        {
            try
            {
                var currenttours = await IsolatedStorageOperations.Load<List<Location>>("CurTourXML.xml");
                destination.Text = currenttours.ElementAt(0).fullname;
                double lat2 = currenttours.ElementAt(0).lat;
                double lng2 = currenttours.ElementAt(0).lng;
                double lat1 = Convert.ToDouble(IsolatedStorageSettings.ApplicationSettings["currlat"].ToString());
                double lng1 = Convert.ToDouble(IsolatedStorageSettings.ApplicationSettings["currlng"].ToString());
                int dist = (int)distance(lat1, lng1, lat2, lng2, 'K');
                distanceblock.Text = dist.ToString() + " km";
                int f;
                if (dist <= 10)
                    f = 100;
                else
                    f = (int)((dist - 10) * 16 + 100);
                fare.Text = "Rs. " + f.ToString();
            }
            catch (Exception) { }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Your Booking has been confirmed!" +Environment.NewLine+  "ENJOY YOUR TOUR AND DO PROVIDE US WITH YOUR FEEDBACK.");
        }
        
    }
}