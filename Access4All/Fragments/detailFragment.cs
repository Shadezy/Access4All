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
    public class detailFragment : Fragment
    {
        ExpandableListView expandableListView;
        catAdapter mAdapter;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View v = inflater.Inflate(Resource.Layout.categoriesLayout, null);
            expandableListView = v.FindViewById<ExpandableListView>(Resource.Id.expandableListView1);
            ExpandableListView ex = (ExpandableListView)v.FindViewById(Resource.Id.expandableListView1);
            ex.SetAdapter(mAdapter);
            ex.ChildClick += (s, e) =>
            {
                Toast.MakeText(MainActivity.activity, "Clicked: " + mAdapter.GetChild(e.GroupPosition, e.ChildPosition).ToString(), ToastLength.Short).Show();
            };


            expandableListView.SetAdapter(mAdapter);

            return v;
        }
    }
}