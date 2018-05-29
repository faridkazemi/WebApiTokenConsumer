using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WebApiConsumer_TokenBased
{
    public class MyToken
    {
        public string access_token { get; set; }
    }
    public partial class Form1 : Form
    {
        public string Token = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task.Run(() => GetToken());

        }
        private void button2_Click(object sender, EventArgs e)
        {
            Task.Run(() => GetOrders());

        }

        private async Task<HttpResponseMessage> GetToken()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.BaseAddress = new Uri("http://localhost:60835");
                var nvc = new List<KeyValuePair<string, string>>();
                nvc.Add(new KeyValuePair<string, string>("grant_type", "password"));
                nvc.Add(new KeyValuePair<string, string>("username", "Farid"));
                nvc.Add(new KeyValuePair<string, string>("password", "abcd"));
                var a = new FormUrlEncodedContent(nvc);
                var req = new HttpRequestMessage(HttpMethod.Post, "/token") { Content = a };
                var res = await client.SendAsync(req);
                var str = await res.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<MyToken>(str);
                //label1.Text = token.access_token;
                Token = token.access_token;
                return res;
            }
            
        }

        private async Task<HttpResponseMessage> GetOrders()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.BaseAddress = new Uri("http://localhost:60835");

                    //"j730mmS3y4uaHXs6SI3ad-ziIQaCISs_6A2wHi0mPL32Uqkv5-ZaEOO_z4dQea-FqcsQ5MyE-qFZ_p1Or3v8j4_66QvdiNb1dzjMMNFHCWvYiNQ9Vyer6ebUXpWSG4HI3Fh9yrQfl81bFz27OfVL74SDl5RS27hIaUUOAqvTCoogB7pTk0-dMflij3nDd7Pn4DxiMKKc8eTJz_AskhsVfwcgN52Qj0Euc1KEn6BePUo";
                client.DefaultRequestHeaders.Add("Authorization", $@"Bearer {Token}");
                var result = await client.GetAsync("/api/Orders");
                var test = await result.Content.ReadAsStringAsync();
                return result;
            }

        }

        private async Task<HttpResponseMessage> useGoogleAPI()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.BaseAddress = new Uri("http://localhost:60835");
                Token =
                    "j730mmS3y4uaHXs6SI3ad-ziIQaCISs_6A2wHi0mPL32Uqkv5-ZaEOO_z4dQea-FqcsQ5MyE-qFZ_p1Or3v8j4_66QvdiNb1dzjMMNFHCWvYiNQ9Vyer6ebUXpWSG4HI3Fh9yrQfl81bFz27OfVL74SDl5RS27hIaUUOAqvTCoogB7pTk0-dMflij3nDd7Pn4DxiMKKc8eTJz_AskhsVfwcgN52Qj0Euc1KEn6BePUo";
                client.DefaultRequestHeaders.Add("Authorization", $@"Bearer {Token}");
                var result = await client.GetAsync("/api/Orders");
                var test = await result.Content.ReadAsStringAsync();
                return result;
            }

        }
    }
}
