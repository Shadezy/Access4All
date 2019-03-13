using System;
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

namespace Access4All.Fragments
{
    public class searchFragment : Fragment, View.IOnClickListener
    {
        ListView mTv;
        bool flagSearch = false;
        
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            // Create your fragment here

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
            SearchView searchV = view.FindViewById<SearchView>(Resource.Id.searchView1);

            mTv = view.FindViewById<ListView>(Resource.Id.searchResults);

            //set up listeners

            textButton.Click += searchByText;

            voiceButton.Click += searchByVoice;

            searchV.QueryTextSubmit += submitQueryListener;

            mTv.ItemClick += MTv_ItemClick;
            
            
        
            return view;

            

            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void MTv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            string location;
            location = (string)mTv.GetItemAtPosition(e.Position);
            Android.Support.V4.App.Fragment fragment = null;
            Bundle args = new Bundle();
            args.PutString("location", location);
            args.PutString("prevView", "search");
            fragment = detailFragment.NewInstance();
            fragment.Arguments = args;
            base.FragmentManager.BeginTransaction()
                        .Replace(Resource.Id.content_frame, fragment)
                        .Commit();
        }

        private void submitQueryListener(object sender, SearchView.QueryTextSubmitEventArgs e)
        {
            MainActivity act = (MainActivity)this.Activity;
            SearchView searchView = (SearchView)act.FindViewById(Resource.Id.searchView1);
            string input = e.Query;

           // TextView text = (TextView)act.FindViewById(Resource.Id.searchResults);

            //close keyboard and lose focus
            searchView.SetIconifiedByDefault(true);
            searchView.OnActionViewCollapsed();
            searchView.Focusable = false;

            //Ping database by name
            string data = GetData();
            
            JArray jsonArray = JArray.Parse(data);
            List<string> searched_Loc = new List<string>();

            string debugMe = "";

            for (int i = 0; i < jsonArray.Count; i++)
            {
                JToken json = jsonArray[i];
                if (((string)json["name"]).Equals(input, StringComparison.InvariantCultureIgnoreCase))
                {
                    //Toast.MakeText(this.Activity, "We have a match for " + input, ToastLength.Short).Show();
                    searched_Loc.Add(((string)json["name"]) + " " + ((string)json["street"]) + " " + ((string)json["city"]) + " " + ((string)json["state"]));
                    
                }
            }
            //debug
            for(int j = 0; j < searched_Loc.Count; j++)
            {
                debugMe += searched_Loc[j];
                debugMe += "\n";
            }
            //build spannable string

            /* I doubt this will work anymore. Keeping in case, but will try to find a better solution later 
             * 
             *
            SpannableString mySpan = new SpannableString(debugMe);
            var clickSpan = new MyClickableSpan();
            clickSpan.Click += v => StartActivity(new Intent(act, typeof(MainActivity)));//need to point to detail depth
            int StartCount = 0;
            int StopCount = 0;
            for (int x = 0; x < searched_Loc.Count; x++) {
                StopCount += (searched_Loc.ElementAt(x).Length);

                mySpan.SetSpan(clickSpan, StartCount, StopCount, SpanTypes.ExclusiveExclusive);

                StartCount += (searched_Loc.ElementAt(x).Length);
            }
            text.TextFormatted = mySpan;
            text.MovementMethod = new LinkMovementMethod();
            */
            //Listview stuff
            mTv = act.FindViewById<ListView>(Resource.Id.searchResults);
            ArrayAdapter<string> arrayAdapter = new ArrayAdapter<string>(act, Android.Resource.Layout.SimpleListItem1, searched_Loc);

            mTv.Adapter = arrayAdapter;

            
        }

        private void searchByVoice(object sender, EventArgs e)
        {
            //set up variables
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
                intent.PutExtra(RecognizerIntent.ActionRecognizeSpeech, "en-US");
                intent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
                intent.PutExtra(RecognizerIntent.ExtraLanguage, LocaleList.Default);
                intent.PutExtra(RecognizerIntent.ExtraPrompt, "Say Location Name");
             
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
                        //Console.Out.WriteLine("Response Body: \r\n {0}", content);
                        return content;
                    }
                }
            }
            return "NULL";
        }

        public void OnClick(View v)
        {
            //throw new NotImplementedException();
        }
    }
}