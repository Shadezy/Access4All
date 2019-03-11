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
    public class detaildepthFragment : Android.Support.V4.App.Fragment, MainActivity.IBackButtonListener
    {
        string curLocation;
        string selection;
        public override void OnCreate(Bundle savedInstanceState)
        {
            Bundle b = Arguments;
            curLocation = b.GetString("location");
            selection = b.GetString("selection");
            Toast.MakeText(MainActivity.activity, curLocation + selection, ToastLength.Short).Show();
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

        public void OnBackPressed()
        {
            Android.Support.V4.App.Fragment fragment = null;
            fragment = detailFragment.NewInstance();
            base.FragmentManager.BeginTransaction()
                        .Replace(Resource.Id.content_frame, fragment)
                        .Commit();
        }
    }
}