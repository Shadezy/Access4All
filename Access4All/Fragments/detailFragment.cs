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
    public class detailFragment : Android.Support.V4.App.Fragment
    {
        detailAdapter mAdapter;
        List<Details> group = new List<Details>();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            setTempData();


            mAdapter = new detailAdapter(this, group);
        }

        private void setTempData()
        {

            group.Add(new Details("Information"));
            group.Add(new Details("Parking on street"));
            group.Add(new Details("Access to transit"));
            group.Add(new Details("Exterior pathway & seating"));
            group.Add(new Details("Entrances"));
            group.Add(new Details("Interior"));
            group.Add(new Details("Seating"));
            group.Add(new Details("Restroom"));
            group.Add(new Details("Communication"));
            group.Add(new Details("Technologies & Customer Service"));



        }

        public static detailFragment NewInstance()
        {
            var detailfrag = new detailFragment { Arguments = new Bundle() };
            return detailfrag;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View v = inflater.Inflate(Resource.Layout.categoriesLayout, null);
            ExpandableListView ex = (ExpandableListView)v.FindViewById(Resource.Id.expandableListView1);
            ex.SetAdapter(mAdapter);
            /*ex.ChildClick += (s, e) =>
            {
                Toast.MakeText(MainActivity.activity, "Clicked: " + mAdapter.GetChild(e.GroupPosition, e.ChildPosition).ToString(), ToastLength.Short).Show();
            };*/

            ex.Click += HandleSelect;
            

            return v;


            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void HandleSelect(object sender, EventArgs e)
        {
            Android.Support.V4.App.Fragment fragment = null;
            fragment = detaildepthFragment.NewInstance();
            base.FragmentManager.BeginTransaction()
                        .Replace(Resource.Id.content_frame, fragment)
                        .Commit();

        }
    }
}