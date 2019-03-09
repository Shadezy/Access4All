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

namespace Access4All
{
    class Location
    {
        /*
        private string est_id;
        private string name;
        private string website;
        private string subtype;
        private string date;
        private string street;
        private string city;
        private string state;
        private string zip;
        private string phone;
        private string tty;
        private string contact_fname;
        private string contact_lname;
        private string contact_title;
        private string contact_email;
        private string user_id;
        private string cat_id;
        private string config_id;
        private string config_comment;
        */

        //constructor
        public Location(string est_id, string name, string website, string subtrype, string date, string street, string city, string zip, string phone, string tty, string contact_fname,
                        string contact_lname, string contact_title, string contact_email, string user_id, string cat_id, string config_id, string config_comment)
        {
            this.cat_id = cat_id;
            this.city = city;
            this.config_comment = config_comment;
            this.config_id = config_id;
            this.contact_email = contact_email;
            this.contact_fname = contact_fname;
            this.contact_lname = contact_lname;
            this.contact_title = contact_title;
            this.date = date;
            this.est_id = est_id;
            this.name = name;
            this.phone = phone;
            this.state = state;
            this.street = street;
            this.subtype = subtype;
            this.tty = tty;
            this.user_id = user_id;
            this.website = website;
            this.zip = zip;

        }

        //getters and setters

        public string est_id { get; set; }
        public string name { get; set; }
        public string website { get; set; }
        public string subtype { get; set; }
        public string date { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string phone { get; set; }
        public string tty { get; set; }
        public string contact_fname { get; set; }
        public string contact_lname { get; set; }
        public string contact_title { get; set; }
        public string contact_email { get; set; }
        public string user_id { get; set; }
        public string cat_id { get; set; }
        public string config_id { get; set; }
        public string config_comment { get; set; }

    }
}