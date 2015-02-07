using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using OlaTravel.Resources;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Phone.Info;
using System.IO;
using Newtonsoft.Json;
using System.IO.IsolatedStorage;
using System.ComponentModel;

namespace OlaTravel
{
    public partial class MainPage : PhoneApplicationPage
    {
        IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
                       
        }

        private void PasswordLostFocus(object sender, RoutedEventArgs e)
        {
            var p = sender as PasswordBox;
            if (p.Name == "passwordbox")
            {
                var passwordEmpty = string.IsNullOrEmpty(passwordbox.Password);
                PasswordWatermark.Opacity = passwordEmpty ? 100 : 0;
                passwordbox.Opacity = passwordEmpty ? 0 : 100;

            }
            else
            {
                var passwordEmpty = string.IsNullOrEmpty(passwordbox1.Password);
                PasswordWatermark1.Opacity = passwordEmpty ? 100 : 0;
                passwordbox1.Opacity = passwordEmpty ? 0 : 100;

            }
        }

        private void PasswordGotFocus(object sender, RoutedEventArgs e)
        {
            var p = sender as PasswordBox;
            if (p.Name == "passwordbox")
            {
                PasswordWatermark.Opacity = 0;
                passwordbox.Opacity = 100;
            }
            else
            {
                PasswordWatermark1.Opacity = 0;
                passwordbox1.Opacity = 100;
            }
        }


        private void loginbut_Click(object sender, RoutedEventArgs e)
        {
            if(Loginpop.IsOpen== false)
            {
                Signuppop.IsOpen = false;
                Loginpop.IsOpen = true;
            }
            else 
            {
                if (passwordbox.Password == "" || email.Text == "")
                {
                    MessageBox.Show("Please enter all the fields.");
                }
                else
                {
                    string pass = passwordbox.Password;
                    string emal = email.Text;
                    if (IsolatedStorageSettings.ApplicationSettings.Contains("Signup"))
                    {
                      //  MessageBox.Show(IsolatedStorageSettings.ApplicationSettings["email"].ToString() + IsolatedStorageSettings.ApplicationSettings["pass"].ToString());

                        if (IsolatedStorageSettings.ApplicationSettings["email"].ToString() == emal && IsolatedStorageSettings.ApplicationSettings["pass"].ToString() == pass)
                        {
                            settings.Add("Login", true);
                            settings.Save();
                            NavigationService.Navigate(new Uri("/RootPage.xaml", UriKind.Relative));
                        }
                        else
                            MessageBox.Show("Wrong credentials.");
                    }
                    else MessageBox.Show("error please sign up again.");
                }
                   
            }
        }

        void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            string s = JsonConvert.DeserializeObject(e.Result).ToString();
            MessageBox.Show(s);
        }

        public static string Encrypt(string stringToEncrypt, string password, string salt)
        {
            string encryptedString = string.Empty;

            using (AesManaged aesEncryptor = new AesManaged())
            {
                // 1. AES algorith modification
                Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt), 10000);
                aesEncryptor.Key = rfc2898.GetBytes(32);
                aesEncryptor.IV = rfc2898.GetBytes(16);

                using (MemoryStream aesMemoryStream = new MemoryStream())
                {
                    using (CryptoStream aesCryptoStream = new CryptoStream(aesMemoryStream, aesEncryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        // 2. Encrypt data
                        byte[] data = Encoding.UTF8.GetBytes(stringToEncrypt);
                        aesCryptoStream.Write(data, 0, data.Length);
                        aesCryptoStream.FlushFinalBlock();

                        // 3. convert encrypted data to base64 string
                        encryptedString = Convert.ToBase64String(aesMemoryStream.ToArray());
                    }
                }
            }
            return encryptedString;
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            Application.Current.Terminate();
        }

        private void signup_Click(object sender, RoutedEventArgs e)
        {
            if(Signuppop.IsOpen==false)
            {
                Loginpop.IsOpen = false;
                Signuppop.IsOpen = true;
            }
            else
            {
                if (passwordbox1.Password == "" || email1.Text == "" || mobile.Text == "" || name.Text == "")
                    MessageBox.Show("Please enter all the fields.");
                else
                {
                    Signup sign = new Signup();
                    sign.password = passwordbox1.Password;
                    byte[] b = (byte[])DeviceExtendedProperties.GetValue("DeviceUniqueId");
                    sign.device_id = Convert.ToBase64String(b);
                    sign.email = email1.Text;
                    sign.mobile = mobile.Text;
                    sign.name = name.Text;
                    if (!settings.Contains("Login") && !settings.Contains("Signup"))
                    {
                        settings.Add("email", email1.Text);
                        settings.Add("pass", sign.password);
                    }
                    else
                    {
                        settings["email"] = sign.email;
                        settings["pass"] = sign.password;
                    }
                    settings.Save();
                    if(!settings.Contains("Signup"))
                    settings.Add("Signup", true);
                    Signuppop.IsOpen = false;
                    Loginpop.IsOpen = true;
                }
            }
        }

       
      
    }
}