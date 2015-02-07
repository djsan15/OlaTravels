using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OlaTravel
{
  public static  class Api
    {
     public static string signup = "";
      public static string login = "";
      public static string apikey="AIzaSyAbKYXFihJxPILhrkb8m2r-dUBcSb0qNOw";
      public static string gautocomplete = "https://maps.googleapis.com/maps/api/place/queryautocomplete/json?";
      public static string geocode = "http://maps.googleapis.com/maps/api/geocode/json?";
      public static int x = 0;
    }

    [DataContract] 
  public class Location
  {
        [DataMember]
      public double lat { get; set; }
        [DataMember]
      public double lng { get; set; }
        [DataMember]
      public string fullname { get; set; }
  }

  public class Geometry
  {
      public Location location { get; set; }
  }

  public class OpeningHours
  {
      public bool open_now { get; set; }
      public List<object> weekday_text { get; set; }
  }

  public class Photo
  {
      public int height { get; set; }
      public List<object> html_attributions { get; set; }
      public string photo_reference { get; set; }
      public int width { get; set; }
  }

  public class Result
  {
      public string formatted_address { get; set; }
      public Geometry geometry { get; set; }
      public string icon { get; set; }
      public string id { get; set; }
      public string name { get; set; }
      public OpeningHours opening_hours { get; set; }
      public List<Photo> photos { get; set; }
      public string place_id { get; set; }
      public double rating { get; set; }
      public string reference { get; set; }
      public List<string> types { get; set; }
      public int? price_level { get; set; }
  }

  public class Gsearch
  {
      public List<object> html_attributions { get; set; }
      public string next_page_token { get; set; }
      public List<Result> results { get; set; }
      public string status { get; set; }
  }
    //Autocomplete
  public class Prediction
  {
      public string description { get; set; }
      public string id { get; set; }
     
      public string place_id { get; set; }
      public string reference { get; set; }
     
      public List<string> types { get; set; }
  }

  public class GAutoComplete
  {
      public List<Prediction> predictions { get; set; }
      public string status { get; set; }
  }
    //Search
  public class GGeocode
  {
      public List<Result1> results { get; set; }
      public string status { get; set; }
  }
  
 


  public class Result1
  {
     
      public string formatted_address { get; set; }
      public Geometry geometry { get; set; }
      public List<string> types { get; set; }
  }
}
