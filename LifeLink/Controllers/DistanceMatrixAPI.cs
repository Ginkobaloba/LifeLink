using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LifeLink.Models;

namespace LifeLink.Controllers
{
    
    class DistanceMatrixAPI
    {
        public  ApplicationDbContext db = new ApplicationDbContext();

        public void distanceMatriAapi()
            { }
            public void GetDistance(Address Origin, string UserId)
            {
            var query = (from l in db.Location select l).ToList();
            string originlatlng = Origin.Latitude + "," + Origin.Longitude;
            List<List<string>> location_distances = new List<List<string>>();

            foreach (Location item in query)
            {
                string destinationlatlng = item.LocationLat + "," + item.LocationLong;
                string apiKey = "AIzaSyC0EV_dRHnElwBT7uk0fE98oBD6AkKW9cI";
                string googleUrl;
                googleUrl = "https://maps.googleapis.com/maps/api/distancematrix/json?units=imperial&origins=" + originlatlng + "&destinations=" + destinationlatlng + "&key=" + apiKey;

                WebRequest request = WebRequest.Create(googleUrl);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                WebResponse response = request.GetResponse();
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                responseFromServer = responseFromServer.ToString();
                Distanceobject results = JsonConvert.DeserializeObject<Distanceobject>(responseFromServer);
                List<string> location_distance = new List<string>();
                location_distance.Add(results.rows[0].elements[0].distance.text.Substring(0, results.rows[0].elements[0].distance.text.IndexOf("mi")-1));
                location_distance.Add(item.Name);
                location_distances.Add(location_distance);

                             
                reader.Close();
                dataStream.Close();
                response.Close();

            }
            

            var Sorted = location_distances.OrderBy(x => Convert.ToDecimal(x[0])).ToList();


            
            var test2 = Sorted[0][1];
            var test = Sorted[0];


            var location = (from L in db.Location where L.Name == test2 select L).FirstOrDefault();
            var address = (from A in db.Address where A.UserId == UserId select A).FirstOrDefault();
            address.ClosestLocationId = location.LocationId;
            db.SaveChanges();
            
        }
        }
    }
