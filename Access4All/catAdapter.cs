using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace Access4All
{
    class catAdapter : BaseExpandableListAdapter
    {
        Activity activity;
        Context context;
        List<Categories> c;

        public catAdapter(Activity act)
        {
            this.activity = act;
        }

        public override int GroupCount => throw new NotImplementedException();

        public override bool HasStableIds => throw new NotImplementedException();

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            return c[groupPosition].Locations[childPosition];
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            return c[groupPosition].locations.Count;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            throw new NotImplementedException();
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return c[groupPosition].title;
        }

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            throw new NotImplementedException();
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }
    }