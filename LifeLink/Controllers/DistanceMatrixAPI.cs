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
            public void distanceMatriAapi()
            { }
            public void GetDistance(Address Origin)
            {
                string originlatlng = "43.0389,-87.9065";
                string destinationlatlng = "43.1789,-88.1173";
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
                Console.WriteLine(responseFromServer);
                Console.ReadLine();
                reader.Close();
                dataStream.Close();
                response.Close();
            }
        }
    }
