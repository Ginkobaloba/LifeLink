using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;


namespace LifeLink.Controllers
{
    class PlacesDictionary
    {

        public void placesDictionary()
        { }

        public Result GetPlaces(double lat, double lng)
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
            Console.WriteLine(responseFromServer);
            Console.ReadLine();
            reader.Close();
            dataStream.Close();
            response.Close();

            return results;

        }
    }
}
