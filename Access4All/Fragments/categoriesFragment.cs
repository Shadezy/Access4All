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
            View v = inflater.Inflate(Resource.Layout.categoriesLayout, null);
            catAdapter adapter = new catAdapter(MainActivity.activity);
            ExpandableListView e = (ExpandableListView)v.FindViewById(Resource.Id.expandableListView1);
            e.SetAdapter(adapter);
            e.SetOnChildClickListener(onChildClick());
                


            return v;


            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        public bool onChildClick(ExpandableListView parent, View v, int groupPosition, int childPosition, long id)
        {
            catAdapter adapter = new catAdapter(MainActivity.activity);
            string catName = adapter.GetChild(groupPosition, childPosition);
            Categories temp = adapter.GetGroup(groupPosition);

            return true;
        }
    }
}