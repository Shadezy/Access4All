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
    public class categoriesFragment : Android.Support.V4.App.Fragment
    {
        catAdapter mAdapter;
        ExpandableListView expandableListView;
        List<Categories> group = new List<Categories>();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here

            setTempData();
           

            mAdapter = new catAdapter(this, group);
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

            group.Add(new Categories("Auto", groupA));
            group.Add(new Categories("Art", groupB));



        }

        public static categoriesFragment NewInstance()
        {
            var catfrag = new categoriesFragment { Arguments = new Bundle() };
            return catfrag;
        }
        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View v = inflater.Inflate(Resource.Layout.categoriesLayout, null);
            expandableListView = v.FindViewById<ExpandableListView>(Resource.Id.expandableListView1);
            //catAdapter adapter = new catAdapter(MainActivity.activity);
            //ExpandableListView e = (ExpandableListView)v.FindViewById(Resource.Id.expandableListView1);
            //e.SetAdapter(adapter);
            //e.SetOnChildClickListener(onChildClick());


            expandableListView.SetAdapter(mAdapter);

            return v;


            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        public bool onChildClick(ExpandableListView parent, View v, int groupPosition, int childPosition, long id)
        {
            //catAdapter adapter = new catAdapter(MainActivity.activity);
            //string catName = adapter.GetChild(groupPosition, childPosition);
            //Categories temp = adapter.GetGroup(groupPosition);

            return true;
        }
    }
}