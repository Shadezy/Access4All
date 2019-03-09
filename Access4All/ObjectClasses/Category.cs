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

namespace Access4All.ObjectClasses
{
    class Category
    {
        //private fields
        private int cat_id;
        private string name;
        //Used for getting and setting Category Table in the Database
        public Category(int cat_id, string name)
        {
            //constructor
            this.cat_id = cat_id;
            this.name = name;
        }

        public Category()
        {
            this.cat_id = 0;
            this.name = null;
        }
        //getters and setters
        public int Cat_ID
        {
            get
            {
                return this.cat_id;
            }
            set
            {
                this.cat_id = value;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

    }
}