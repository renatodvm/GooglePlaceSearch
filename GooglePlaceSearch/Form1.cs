using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GooglePlaceSearch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var url = @"https://maps.googleapis.com/maps/api/place/textsearch/json?query=brasil&types=beauty_salon&hasNextPage=true&nextPage()=true&key=AIzaSyB-GrtUkpeska3NIDFhD5bxS_ha9mVCKMY";

            /*
             * Query 1: filtro por tipo, query e solicita geração do next_page_token
             * https://maps.googleapis.com/maps/api/place/textsearch/xml?query=brasil&types=beauty_salon&hasNextPage=true&nextPage()=true&key=AIzaSyB-GrtUkpeska3NIDFhD5bxS_ha9mVCKMY
             * 
             * Próxima página de resultado: igual a query anterior + o atributo next_page_token retornado, passando no parâmetro pagetoken
             * https://maps.googleapis.com/maps/api/place/textsearch/xml?query=brasil&types=beauty_salon&hasNextPage=true&nextPage()=true&key=AIzaSyB-GrtUkpeska3NIDFhD5bxS_ha9mVCKMY&pagetoken=CvQB4QAAAHT7S_wz0xOOWXSPpNHraLj_Z37ciwN2M4OdO8W_9pA7RS-h9IpIjhaBWwIpKhBT8zdouOkFIL86Rp7GW8Po1KztqTJjqXE6hDoRbfFFE4hESffe0WXg4Uym2vGHCZ99kBpOOZSoY8ADogI3TQ0MZgihWMQejXUGybOx0jtKTiHir4iwhU3mhX5JQ2PswGMORBsCKjWNiR4gh_udeDY04Gvi-nNTnhcPCRvU9-aJBbPi_xlH6nvRzUhnVPkOzoaUQudjGVsDxK03wuDY3Hn5bIkXyRz1W1yFmwRKROtX8QaKgNJggGrUx2A3ZXCkQibCyxIQcfTao1JUk2CaQIhfYV7GDxoUGtkPZA7MVC29rfpUHpDYRZRgHPs
            */

            // Exemplo 1
            //HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            //webRequest.Timeout = 20000;
            //webRequest.Method = "GET";

            //webRequest.BeginGetResponse(new AsyncCallback(RequestCompleted), webRequest);


            // Exemplo 2
            Get(url);
        }

        private async void Get(string url)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);
                var result = JsonConvert.DeserializeObject<PlacesApiQueryResponse>(response);
            }
        }

        // Exemplo 1
        //private void RequestCompleted(IAsyncResult result)
        //{
        //    var request = (HttpWebRequest)result.AsyncState;
        //    var response = (HttpWebResponse)request.EndGetResponse(result);
        //    using (var stream = response.GetResponseStream())
        //    {
        //        var r = new StreamReader(stream);
        //        var resp = r.ReadToEnd();
        //    }

        //}
    }

    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
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
        public List<string> html_attributions { get; set; }
        public string photo_reference { get; set; }
        public int width { get; set; }
    }

    public class Result
    {
        public Geometry geometry { get; set; }
        public string icon { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public OpeningHours opening_hours { get; set; }
        public List<Photo> photos { get; set; }
        public string place_id { get; set; }
        public double rating { get; set; }
        public string reference { get; set; }
        public string scope { get; set; }
        public List<string> types { get; set; }
        public string vicinity { get; set; }
    }

    public class PlacesApiQueryResponse
    {
        public List<object> html_attributions { get; set; }
        public List<Result> results { get; set; }
        public string status { get; set; }
    }
}
