using System;
using System.Collections.Generic;
using System.Device.Location; // Provides the GeoCoordinate class.
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;
using System.Threading.Tasks; //Provides the Geocoordinate class.
using System.Xml.Serialization;
using Windows.Devices.Geolocation;

namespace OlaTravel
{
    public static class CoordinateConverter
    {
        public static GeoCoordinate ConvertGeocoordinate(Geocoordinate geocoordinate)
        {
            return new GeoCoordinate
                (
                geocoordinate.Latitude,
                geocoordinate.Longitude,
                geocoordinate.Altitude ?? Double.NaN,
                geocoordinate.Accuracy,
                geocoordinate.AltitudeAccuracy ?? Double.NaN,
                geocoordinate.Speed ?? Double.NaN,
                geocoordinate.Heading ?? Double.NaN
                );
        }
    }
    public static class IsolatedStorageOperations
    {
        public static async Task Save<T>(this T obj, string file)
        {
            await Task.Run(() =>
            {
                IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication();
                IsolatedStorageFileStream stream = null;

                try
                {


                    stream = storage.CreateFile(file);
                       // stream = new IsolatedStorageFileStream(file, FileMode.Append, storage);
                        XmlSerializer serializer = new XmlSerializer(typeof(T));
                        serializer.Serialize(stream, obj);
                                        
                }
                catch (Exception)
                {
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Close();
                        stream.Dispose();
                    }
                }
            });
        }

        public static async Task<T> Load<T>(string file)
        {

            IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication();
            T obj = Activator.CreateInstance<T>();

            if (storage.FileExists(file))
            {
                IsolatedStorageFileStream stream = null;
                try
                {
                    stream = storage.OpenFile(file, FileMode.Open);
                   // stream = new IsolatedStorageFileStream(file, FileMode.Open, storage);
                    XmlSerializer serializer = new XmlSerializer(typeof(T));

                    obj = (T)serializer.Deserialize(stream);
                }
                catch (Exception)
                {
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Close();
                        stream.Dispose();
                    }
                }
                return obj;
            }
            await obj.Save(file);
            return obj;
        }
    }
}