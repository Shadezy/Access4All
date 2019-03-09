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
        ExpandableListView expandableListView;
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
            List<string> groupA = new List<string>();
            groupA.Add("A-1");
            groupA.Add("A-2");
            groupA.Add("A-3");

            List<string> groupB = new List<string>();
            groupB.Add("B-1");
            groupB.Add("B-2");
            groupB.Add("B-3");

            group.Add(new Details("Arts, Entertainment, Culture", groupA));


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

            ex.ChildClick += HandleSelect;


            return v;


            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        private void HandleSelect(object sender, EventArgs e)
        {
            /*Android.Support.V4.App.Fragment fragment = null;
            fragment = detailFragment.NewInstance();
            FragmentTransaction ft = (FragmentTransaction)FragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, fragment)
                .Commit();
                */
        }
    }
}