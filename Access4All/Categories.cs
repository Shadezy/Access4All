﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Access4All
{
    class Categories
    {
        public string title;
        public List<Location> locations;

        public Categories(string title, List<Location> locations)
        {
            this.title = title;
            this.locations = locations;
        }

        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
            }
        }

        public List<Location> Locations
        {
            get
            {
                return this.locations;
            }
            set
            {
                this.locations = value;
            }
        }
        public override string ToString()///dont use this
        {
            string data = "";
            for (int i = 0; i < locations.Count; i++)
                data += locations.ToString() + ", ";

            return data;
        }
    }
}