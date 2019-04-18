﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;

using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Views.InputMethods;
using static Access4All.Resource;
using Android.Speech;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Android.Text;
using Android.Text.Style;
using Android.Text.Method;
using System.Text.RegularExpressions;
using Android.Locations;
using System.Threading.Tasks;
using Plugin.Geolocator;

namespace Access4All.Fragments
{
    public class searchFragment : Fragment
    {
        ListView mTv;
        bool flagSearch = false;
        bool userLocationObtained = false;
        Plugin.Geolocator.Abstractions.Position Userposition;


        public override async void OnCreate(Bundle savedInstanceState)
        {
           

            base.OnCreate(savedInstanceState);
            //Check for user location permissions here to prevent long wait times for search function
            MainActivity act = (MainActivity)this.Activity;
            //Get user location
            if (act.CheckSelfPermission(Android.Manifest.Permission.AccessCoarseLocation) == (int)Android.Content.PM.Permission.Granted)
            {
                await getLocation();
                userLocationObtained = true;
            }

         

        }

        private void searchByText(object sender, EventArgs e)
        {
            //change focus off button
            Button mButton = (Button)sender;
            mButton.Focusable = false;

            //Get activity and the searchView. Set Listener on it
            MainActivity act = (MainActivity)this.Activity;
            SearchView searchV = (SearchView) act.FindViewById(Resource.Id.searchView1);
            //searchV.SetOnClickListener(this);

            //loses focus on search view if it already given focus before ((allows for backout without querying and pressing the button again))
            if (flagSearch)
            {
                searchV.Focusable = false;
                searchV.SetIconifiedByDefault(true);
                searchV.OnActionViewCollapsed();

            }

            searchV.RequestFocus();
            searchV.SetIconifiedByDefault(false);
            searchV.OnActionViewExpanded();
            flagSearch = true;

        

        } 
        
        public static searchFragment NewInstance()
        {
            var searchfrag = new searchFragment { Arguments = new Bundle() };
            return searchfrag;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //button listeners
            var view = LayoutInflater.Inflate(Resource.Layout.searchLayout, container, false);
            Button textButton = view.FindViewById<Button>(Resource.Id.textSearch);
            Button voiceButton = view.FindViewById<Button>(Resource.Id.voiceSearch);
            Button nearMeButton = view.FindViewById<Button>(Resource.Id.nearMe);
            SearchView searchV = view.FindViewById<SearchView>(Resource.Id.searchView1);

            mTv = view.FindViewById<ListView>(Resource.Id.searchResults);

            //set up listeners
            textButton.Click += searchByText;

            voiceButton.Click += searchByVoice;

            searchV.QueryTextSubmit += submitQueryListener;

            mTv.ItemClick += MTv_ItemClick;

            nearMeButton.Click += searchNearMe;

            return view;
        }

        private void searchNearMe(object sender, EventArgs e)
        {
            string data = GetData();

            JArray jsonArray = JArray.Parse(data);
            List<string> searched_Loc = new List<string>();
            List<AddressLocator> mAddresses = new List<AddressLocator>();
            AddressLocator tempAddress;

            MainActivity act = (MainActivity)this.Activity;

            for (int i = 0; i < jsonArray.Count; i++)
            {
                JToken json = jsonArray[i];
                    if (act.CheckSelfPermission(Android.Manifest.Permission.AccessCoarseLocation) == (int)Android.Content.PM.Permission.Granted)
                    {
                        Geocoder coder = new Geocoder(act);
                        IList<Address> address = new List<Address>();


                        address = coder.GetFromLocationName(((string)json["street"]) + " " + ((string)json["city"]) + " " + ((string)json["state"]), 5);

                        float lon = (float)address.ElementAt(0).Longitude;
                        float lat = (float)address.ElementAt(0).Latitude;

                        tempAddress = new AddressLocator((string)json["name"], ((string)json["street"]) + " " + ((string)json["city"]) + " " + ((string)json["state"]), lon, lat);
                        mAddresses.Add(tempAddress);


                        userLocationObtained = true;
                    }
                    else
                    {
                        Toast.MakeText(MainActivity.activity, "Cannot Calculate User Location. Please allow for the application to use location permissions.", ToastLength.Long).Show();
                    }
            }

            if (userLocationObtained)
            {
                //calculate distances
                CalculateAddressDistance(mAddresses);
                //sort distances
                mAddresses.Sort();

                for (int x = 0; x < mAddresses.Count; x++)
                {
                    if(mAddresses.ElementAt(x).Distance < 15.00)
                        searched_Loc.Add(mAddresses.ElementAt(x).Name + ": " + mAddresses.ElementAt(x).Address + " (" + mAddresses.ElementAt(x).Distance.ToString("n2") + " miles)");
                }
            }
            
            mTv = act.FindViewById<ListView>(Resource.Id.searchResults);
            ArrayAdapter<string> arrayAdapter = new ArrayAdapter<string>(act, Android.Resource.Layout.SimpleListItem1, searched_Loc);

            mTv.Adapter = arrayAdapter;
            mTv.SetFooterDividersEnabled(true);
            mTv.SetHeaderDividersEnabled(true);


        }

        private void MTv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            string location;
            string name;
            location = (string)mTv.GetItemAtPosition(e.Position);
            string[] stringArray = location.Split(": ");
            name = stringArray[0];
            Android.Support.V4.App.Fragment fragment = null;
            Bundle args = new Bundle();
            args.PutString("location", name);
            args.PutString("prevView", "search");
            fragment = detailFragment.NewInstance();
            fragment.Arguments = args;
            base.FragmentManager.BeginTransaction()
                        .Replace(Resource.Id.content_frame, fragment)
                        .Commit();
        }

        private async void submitQueryListener(object sender, SearchView.QueryTextSubmitEventArgs e)
        {
            MainActivity act = (MainActivity)this.Activity;
            SearchView searchView = (SearchView)act.FindViewById(Resource.Id.searchView1);
            string input = e.Query;
            searchView.SetIconifiedByDefault(true);
            searchView.OnActionViewCollapsed();
            searchView.Focusable = false;

            //Ping database by name
            string data = GetData();
            
            JArray jsonArray = JArray.Parse(data);
            List<string> searched_Loc = new List<string>();

            string debugMe = "";
            string tempinput = input;
            
            tempinput = RemoveSpecialCharacters(tempinput);
            tempinput = tempinput.Replace(" ", System.String.Empty);
            
            AddressLocator tempAddress;
            List<AddressLocator> mAddresses = new List<AddressLocator>();
            for (int i = 0; i < jsonArray.Count; i++)
            {
                JToken json = jsonArray[i];
                string temp = (string)json["name"];
                temp = RemoveSpecialCharacters(temp);
                temp = temp.Replace(" ", System.String.Empty);
                if (((string)json["name"]).Equals(input, StringComparison.InvariantCultureIgnoreCase) || temp.Equals(tempinput, StringComparison.InvariantCultureIgnoreCase))
                {
                    
                    
                    //Location stuff - make sure user location permissions were give
                    if (act.CheckSelfPermission(Android.Manifest.Permission.AccessCoarseLocation) == (int)Android.Content.PM.Permission.Granted)
                    {
                        Geocoder coder = new Geocoder(act);
                        IList<Address> address = new List<Address>();
                        

                        address = coder.GetFromLocationName(((string)json["street"]) + " " + ((string)json["city"]) + " " + ((string)json["state"]), 5);

                        float lon = (float)address.ElementAt(0).Longitude;
                        float lat = (float)address.ElementAt(0).Latitude;

                        tempAddress = new AddressLocator((string)json["name"], ((string)json["street"]) + " " + ((string)json["city"]) + " " + ((string)json["state"]), lon, lat);
                        mAddresses.Add(tempAddress);

                        
                        userLocationObtained = true;
                    }
                    else // if not, grab list like before.
                    {
                        searched_Loc.Add(((string)json["name"]) + ": " + ((string)json["street"]) + " " + ((string)json["city"]) + " " + ((string)json["state"]));
                    }
                    
                }
            }
            
            if (userLocationObtained)
            {
                //calculate distances
                CalculateAddressDistance(mAddresses);
                //sort distances
                mAddresses.Sort();

                for (int x = 0; x < mAddresses.Count; x++)
                {
                    searched_Loc.Add(mAddresses.ElementAt(x).Name + ": " + mAddresses.ElementAt(x).Address + " ("+mAddresses.ElementAt(x).Distance.ToString("n2")+" miles)");
                }
            }
            
            for(int j = 0; j < searched_Loc.Count; j++)
            {
                debugMe += searched_Loc[j];
                debugMe += "\n";
            }

            if(searched_Loc.Count == 0)
            {
                Toast.MakeText(MainActivity.activity, "No results found", ToastLength.Long).Show();
            }
            
            mTv = act.FindViewById<ListView>(Resource.Id.searchResults);
            ArrayAdapter<string> arrayAdapter = new ArrayAdapter<string>(act, Android.Resource.Layout.SimpleListItem1, searched_Loc);

            mTv.Adapter = arrayAdapter;
            mTv.SetFooterDividersEnabled(true);
            mTv.SetHeaderDividersEnabled(true);
            
            
        }

        private void CalculateAddressDistance(List<AddressLocator> mAddresses)
        {
            List<string> res = new List<string>();
            float[] results = new float[5];
            
            for (int i = 0; i < mAddresses.Count; i++) {
                Android.Locations.Location.DistanceBetween(mAddresses.ElementAt(i).Lat, mAddresses.ElementAt(i).Lon, Userposition.Latitude, Userposition.Longitude, results);
                mAddresses.ElementAt(i).Distance = (float)((double)results.ElementAt(0) / 1609.34); //dist in miles
            }
        }

        private async Task getLocation()
        {
            var locateMe = CrossGeolocator.Current;
            locateMe.DesiredAccuracy = 100;
            
            var pos = await locateMe.GetPositionAsync(TimeSpan.FromMilliseconds(10000));

            Userposition = pos;
            
        }

        private void searchByVoice(object sender, EventArgs e)
        {
            MainActivity act = (MainActivity)this.Activity;
            SearchView searchView = (SearchView)act.FindViewById(Resource.Id.searchView1);

            //If coming from the text search focus, get rid of it.
            if (flagSearch)
            {
                searchView.Focusable = false;
                searchView.SetIconifiedByDefault(true);
                searchView.OnActionViewCollapsed();

            }

            Intent intent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
            try
            {
                //Still not working with Talkback on.
                intent.PutExtra(RecognizerIntent.ActionRecognizeSpeech, "en-US");
                intent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
                intent.PutExtra(RecognizerIntent.ExtraLanguage, LocaleList.Default);
                intent.PutExtra(RecognizerIntent.ExtraPrompt, "Say Location Name");
                intent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 8000);
                intent.PutExtra(RecognizerIntent.ActionVoiceSearchHandsFree, true);
             
                act.StartActivityForResult(intent, 100);
                
                

            }catch (ActivityNotFoundException)
            {
                Toast t = Toast.MakeText(this.Activity, "Your device doesn't support Speech to Text", ToastLength.Short);
                t.Show();
            }
            
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
                        return content;
                    }
                }
            }
            return "NULL";
        }
        public string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}