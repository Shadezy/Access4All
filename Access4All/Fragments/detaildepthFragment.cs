using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Access4All.Fragments
{
    public class detaildepthFragment : Android.Support.V4.App.Fragment
    {
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
    }
}