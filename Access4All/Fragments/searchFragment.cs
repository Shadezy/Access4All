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

namespace Access4All.Fragments
{
    public class searchFragment : Fragment, View.IOnClickListener
    {
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
            
            //set up listeners

            textButton.Click += searchByText;

            voiceButton.Click += searchByVoice;

            searchV.QueryTextSubmit += submitQueryListener;
            
            
        
            return view;

            

            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void submitQueryListener(object sender, SearchView.QueryTextSubmitEventArgs e)
        {
            MainActivity act = (MainActivity)this.Activity;
            SearchView searchView = (SearchView)act.FindViewById(Resource.Id.searchView1);
            string input = e.Query;

            //close keyboard and lose focus
            searchView.SetIconifiedByDefault(true);
            searchView.OnActionViewCollapsed();
            searchView.Focusable = false;

            //test toast to make sure we get the string we searched for
            Toast.MakeText(this.Activity, input, ToastLength.Short).Show();
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
               // intent.SetAction(Intent.ActionGetContent);
                StartActivityForResult(intent, 100);
                

            }catch (ActivityNotFoundException)
            {
                Toast t = Toast.MakeText(this.Activity, "Your device doesn't support Speech to Text", ToastLength.Short);
                t.Show();
            }
            
        }
        override
        public void OnActivityResult(int request_code, int result_code , Intent i)
        {
           // base.OnActivityResult(request_code, result_code, i);
            MainActivity act = (MainActivity)this.Activity;
            SearchView searchView = (SearchView)act.FindViewById(Resource.Id.searchView1);


            switch (request_code)
            {
                case 100: if (result_code == 100 && i != null)
                    {
                        ArrayList res = (ArrayList) i.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
                        
                        searchView.SetQuery(res.ToString(), true);

                        Toast t = Toast.MakeText(this.Activity, "You got this", ToastLength.Short);
                        t.Show();
                    }
                    break;
            }
        }

        public void OnClick(View v)
        {
            //throw new NotImplementedException();
        }
    }
}