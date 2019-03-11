using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Android.Util;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;

namespace Access4All
{
    [Activity(Theme = "@style/AppTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        List<Categories> group = new List<Categories>();

        public static List<String> arts_and_entertainment_locations = new List<String>();
        public static List<String> automotive_locations = new List<String>();
        public static List<String> bank_and_finance_locations = new List<String>();
        public static List<String> education_locations = new List<String>();
        public static List<String> food_and_drink_locations = new List<String>();
        public static List<String> government_and_community_locations = new List<String>();
        public static List<String> healthcare_locations = new List<String>();
        public static List<String> news_and_media_locations = new List<String>();
        public static List<String> professional_services_locations = new List<String>();
        public static List<String> real_estate_locations = new List<String>();
        public static List<String> religion_locations = new List<String>();
        public static List<String> retail_locations = new List<String>();
        public static List<String> sports_and_recreation_locations = new List<String>();
        public static List<String> travel_locations = new List<String>();
        public static List<String> utilities_locations = new List<String>();
        public static List<String> other_locations = new List<String>();

        /** these don't appear in the db, but they are on the website **/
        public static List<String> business_locations = new List<String>();
        public static List<String> home_and_garden_locations = new List<String>();
        public static List<String> nightlife_locations = new List<String>();
        public static List<String> personal_services_locations = new List<String>();
        public static List<String> pet_locations = new List<String>();
        public static List<String> restaurant_and_coffee_shop_locations = new List<String>();
        //static readonly string TAG = "X:" + typeof(SplashActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
            //setTempData();

            //Log.Debug(TAG, "SplashActivity.OnCreate");
        }

        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
            setTempData();
        }

        async void SimulateStartup()
        {
            //Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
            await Task.Delay(500);
            //Log.Debug(TAG, "Startup work is finished - starting MainActivity.");
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }

        public override void OnBackPressed()
        {

        }

        private void setTempData()
        {
            string data = GetData();
            //Toast.MakeText(this.Activity, data, ToastLength.Short).Show();
            JArray jsonArray = JArray.Parse(data);
            //Toast.MakeText(this.Activity, jsonArray.ToString(), ToastLength.Short).Show();

            Console.WriteLine(jsonArray);

            business_locations.Add("not in db");
            home_and_garden_locations.Add("not in db");
            nightlife_locations.Add("not in db");
            personal_services_locations.Add("not in db");
            pet_locations.Add("not in db");
            restaurant_and_coffee_shop_locations.Add("not in db");



            for (int i = 0; i < jsonArray.Count; i++)
            {
                JToken json = jsonArray[i];

                if ((int)json["cat_id"] == 1)
                    arts_and_entertainment_locations.Add((String)json["name"]);
                else if ((int)json["cat_id"] == 2)
                    automotive_locations.Add((String)json["name"]);
                else if ((int)json["cat_id"] == 3)
                    bank_and_finance_locations.Add((String)json["name"]);
                else if ((int)json["cat_id"] == 4)
                    education_locations.Add((String)json["name"]);
                else if ((int)json["cat_id"] == 5)
                    food_and_drink_locations.Add((String)json["name"]);
                else if ((int)json["cat_id"] == 6)
                    government_and_community_locations.Add((String)json["name"]);
                else if ((int)json["cat_id"] == 7)
                    healthcare_locations.Add((String)json["name"]);
                else if ((int)json["cat_id"] == 8)
                    news_and_media_locations.Add((String)json["name"]);
                else if ((int)json["cat_id"] == 9)
                    professional_services_locations.Add((String)json["name"]);
                else if ((int)json["cat_id"] == 10)
                    real_estate_locations.Add((String)json["name"]);
                else if ((int)json["cat_id"] == 11)
                    religion_locations.Add((String)json["name"]);
                else if ((int)json["cat_id"] == 12)
                    retail_locations.Add((String)json["name"]);
                else if ((int)json["cat_id"] == 13)
                    sports_and_recreation_locations.Add((String)json["name"]);
                else if ((int)json["cat_id"] == 14)
                    travel_locations.Add((String)json["name"]);
                else if ((int)json["cat_id"] == 15)
                    utilities_locations.Add((String)json["name"]);
                else if ((int)json["cat_id"] == 16)
                    other_locations.Add((String)json["name"]);
            }

            group.Add(new Categories("Arts, Entertainment, Culture", arts_and_entertainment_locations));
            group.Add(new Categories("Automotive", automotive_locations));
            group.Add(new Categories("Business Services", business_locations));//not in db
            group.Add(new Categories("Education", education_locations));
            group.Add(new Categories("Financial Services", bank_and_finance_locations));
            group.Add(new Categories("Food, Groceries", food_and_drink_locations));
            group.Add(new Categories("Public Services, Government", government_and_community_locations));
            group.Add(new Categories("Health, Medical, Dental, Mobility aids", healthcare_locations));
            group.Add(new Categories("Home & Garden", home_and_garden_locations));//not in db
            group.Add(new Categories("Mass Media, Printing, Publishing", news_and_media_locations));
            group.Add(new Categories("Nightlife", nightlife_locations));//not in db
            group.Add(new Categories("Recreation, Fitness", sports_and_recreation_locations));
            group.Add(new Categories("Personal Services", personal_services_locations));//not in db
            group.Add(new Categories("Pets", pet_locations));//not in db
            group.Add(new Categories("Professional Services", professional_services_locations));
            group.Add(new Categories("Religious Organizations", religion_locations));
            group.Add(new Categories("Restaurants, Coffee Shops", restaurant_and_coffee_shop_locations));//conflicts with food & grocery/ not in db
            group.Add(new Categories("Shopping", retail_locations));//probably
            group.Add(new Categories("Travel, Hotel, Motel", travel_locations));
        }

        private string GetData()
        {
            var request = HttpWebRequest.Create(string.Format(@"http://access4allspokane.org/RESTapi/establishment"));
            request.ContentType = "application/json";
            request.Method = "GET";

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var content = reader.ReadToEnd();
                    if (string.IsNullOrWhiteSpace(content))
                    {
                        Console.Out.WriteLine("Response contained empty body...");
                    }
                    else
                    {
                        //Console.Out.WriteLine("Response Body: \r\n {0}", content);
                        return content;
                    }
                }
            }
            return "NULL";
        }
    }
}