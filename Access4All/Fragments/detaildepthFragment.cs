using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json.Linq;

namespace Access4All.Fragments
{
    public class detaildepthFragment : Android.Support.V4.App.Fragment
    {
        String table = "parking";
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public static detaildepthFragment NewInstance()
        {
            var detaildepthfrag = new detaildepthFragment { Arguments = new Bundle() };
            return detaildepthfrag;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View v = inflater.Inflate(Resource.Layout.detail_layout, null);
            TextView t = (TextView)v.FindViewById(Resource.Id.textView1);
            t.Text = "This is some example text";

            return v;
            
        }

        private void setTempData()
        {
            String data = GetData();
            JArray jsonArray = JArray.Parse(data);

            Console.WriteLine(jsonArray);

            List<String> arts_and_entertainment_locations = new List<String>();
            List<String> automotive_locations = new List<String>();
            List<String> bank_and_finance_locations = new List<String>();
            List<String> education_locations = new List<String>();
            List<String> food_and_drink_locations = new List<String>();
            List<String> government_and_community_locations = new List<String>();
            List<String> healthcare_locations = new List<String>();
            List<String> news_and_media_locations = new List<String>();
            List<String> professional_services_locations = new List<String>();
            List<String> real_estate_locations = new List<String>();
            List<String> religion_locations = new List<String>();
            List<String> retail_locations = new List<String>();
            List<String> sports_and_recreation_locations = new List<String>();
            List<String> travel_locations = new List<String>();
            List<String> utilities_locations = new List<String>();
            List<String> other_locations = new List<String>();

            /** these don't appear in the db, but they are on the website **/
            List<String> business_locations = new List<String>();
            List<String> home_and_garden_locations = new List<String>();
            List<String> nightlife_locations = new List<String>();
            List<String> personal_services_locations = new List<String>();
            List<String> pet_locations = new List<String>();
            List<String> restaurant_and_coffee_shop_locations = new List<String>();
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
            group.Add(new Categories("Arts, Entertainment, Culture", SplashActivity.arts_and_entertainment_locations));
            group.Add(new Categories("Automotive", SplashActivity.automotive_locations));
            group.Add(new Categories("Business Services", SplashActivity.business_locations));//not in db
            group.Add(new Categories("Education", SplashActivity.education_locations));
            group.Add(new Categories("Financial Services", SplashActivity.bank_and_finance_locations));
            group.Add(new Categories("Food, Groceries", SplashActivity.food_and_drink_locations));
            group.Add(new Categories("Public Services, Government", SplashActivity.government_and_community_locations));
            group.Add(new Categories("Health, Medical, Dental, Mobility aids", SplashActivity.healthcare_locations));
            group.Add(new Categories("Home & Garden", SplashActivity.home_and_garden_locations));//not in db
            group.Add(new Categories("Mass Media, Printing, Publishing", SplashActivity.news_and_media_locations));
            group.Add(new Categories("Nightlife", SplashActivity.nightlife_locations));//not in db
            group.Add(new Categories("Recreation, Fitness", SplashActivity.sports_and_recreation_locations));
            group.Add(new Categories("Personal Services", SplashActivity.personal_services_locations));//not in db
            group.Add(new Categories("Pets", SplashActivity.pet_locations));//not in db
            group.Add(new Categories("Professional Services", SplashActivity.professional_services_locations));
            group.Add(new Categories("Religious Organizations", SplashActivity.religion_locations));
            group.Add(new Categories("Restaurants, Coffee Shops", SplashActivity.restaurant_and_coffee_shop_locations));//conflicts with food & grocery/ not in db
            group.Add(new Categories("Shopping", SplashActivity.retail_locations));//probably
            group.Add(new Categories("Travel, Hotel, Motel", SplashActivity.travel_locations));

        }
        private string GetData()
        {
            
            var request = HttpWebRequest.Create(string.Format(@"http://access4allspokane.org/RESTapi/" + table));
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