using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Access4All.Fragments
{
    public class categoriesFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public static categoriesFragment NewInstance()
        {
            var catfrag = new categoriesFragment { Arguments = new Bundle() };
            return catfrag;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.categoriesLayout, null);

            //return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}