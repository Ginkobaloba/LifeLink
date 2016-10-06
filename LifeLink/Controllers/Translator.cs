using System;
using System.IO;
using System.Net;


namespace LifeLink.Controllers
{
        public class Translator
        {
            private void translator()
            {

            }

            public string Translate(string targetLanguage, string textToTranslate)
            {
                int start;
                int finish;
                string apiKey = "AIzaSyC0EV_dRHnElwBT7uk0fE98oBD6AkKW9cI";
                string googleUrl;
                googleUrl = "https://www.googleapis.com/language/translate/v2?key=" + apiKey + "&q=" + textToTranslate + "&source=en&target=" + targetLanguage;

                WebRequest request = WebRequest.Create(googleUrl);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded";
                WebResponse response = request.GetResponse();
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                responseFromServer = responseFromServer.ToString();
                start = responseFromServer.IndexOf("Text") + 8;
                finish = responseFromServer.Length - start;
                string cleanResponse = responseFromServer.Substring(start, finish);
                start = 0;
                finish = cleanResponse.IndexOf("\n") - 3;
                cleanResponse = cleanResponse.Substring(start, finish);
                reader.Close();
                dataStream.Close();
                response.Close();
                return (cleanResponse);
            }
        }
    }
