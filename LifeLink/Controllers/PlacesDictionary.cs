using LifeLink.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.IO;
using System.Net;
using GoogleMaps.LocationServices;

namespace LifeLink.Controllers
{

    class PlacesDictionary
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public void placesDictionary()
        { }

        public void GetPlaces(double lat, double lng)
        {

            string apiKey = "AIzaSyC0EV_dRHnElwBT7uk0fE98oBD6AkKW9cI";
            string googleUrl = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + lat + "," + lng + "&name=blood||plasma&keyword=health&radius=50000&key=" + apiKey;

            WebRequest request = WebRequest.Create(googleUrl);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            Result results = JsonConvert.DeserializeObject<Result>(responseFromServer);
            UpdateDatabase(results);
            reader.Close();
            dataStream.Close();
            response.Close();

        }
        public void UpdateDatabase(Result results)
        {



            foreach (var item in results.results)
            {
                if (item.types[0] == "health" && item.types[1] == "point_of_interest" && item.types[2] == "establishment")
                {
                    var query = (from L in db.Location where L.StreetAddress == item.vicinity select L).FirstOrDefault();
                    if (query == null)
                    {
                        
                        Location BloodBank = new Location();

                        if (item.geometry.locationlatlng.lat != null)
                        {
                            BloodBank.LocationLat = item.geometry.locationlatlng.lat;
                            BloodBank.LocationLong = item.geometry.locationlatlng.lng;
                        }
                        else
                        {
                            var location = item.vicinity;
                            var locationService = new GoogleLocationService();
                            var point = locationService.GetLatLongFromAddress(location);
                            BloodBank.LocationLat = point.Latitude;
                            BloodBank.LocationLong = point.Longitude;

                        }

                        BloodBank.Name = item.name;
                        BloodBank.StreetAddress = item.vicinity;

                        db.Location.Add(BloodBank);
                        db.SaveChanges();

                    }
                }

            }
        }
    }

}