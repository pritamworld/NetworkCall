using Android.App;
using Android.Widget;
using Android.OS;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;

namespace NetworkCall
{
    [Activity(Label = "NetworkCall", Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int count = 1;
        HttpClient client;
        Button button;
        TextView txtData;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            button = FindViewById<Button>(Resource.Id.myButton);
            txtData = FindViewById<TextView>(Resource.Id.textView1);

            button.Click += async delegate
            {
                button.Text = $"{count++} clicks!";
                using (var client1 = new HttpClient())
                {
                    // send a GET request  
                    var uri = "http://jsonplaceholder.typicode.com/posts";
                    var result = await client1.GetStringAsync(uri);

                    //handling the answer  
                    //var posts = JsonConvert.DeserializeObject<List<Post>>(result);

                    // generate the output  
                    //var post = posts.First();
                    txtData.Text = "First post:\n\n" + result;
                }
            };
            RefreshDataAsync();
        }

        public async Task RefreshDataAsync()
        {

            var RestUrl = "http://jsonplaceholder.typicode.com/posts";
            var uri = new Uri(string.Format(RestUrl, string.Empty));

            client = new HttpClient();
            //client.MaxResponseContentBufferSize = 256000;

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    txtData.Text = content;
                    //Items = JsonConvert.DeserializeObject<List<TodoItem>>(content);

                }
            }
            catch (Exception ee)
            {
                // Debug.WriteLine(@"ERROR {0}", ee.Message);
                txtData.Text = ee.Message;
            }

        }
    }
}

