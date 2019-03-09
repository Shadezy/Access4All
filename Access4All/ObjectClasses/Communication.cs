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
    class Communication
    {
        //private fields based on table's columns
        private int communication_id;
        private string public_phone;
        private string phone_clearance;
        private int num_phone;
        private string tty;
        private string staff_tty;
        private string assisted_listening;
        private string assisted_listen_type;
        private string listening_signage;
        private string staff_listening;
        private string acoustics;
        private string acoustics_level;
        private string alt_comm_methods;
        private string alt_com_type;
        private string staff_ASL;
        private string captioning_defualt;
        private string theatre_captioning;
        private string theatre_capt_type;
        private string auditory_info_visual;
        private string visual_info_auditory;
        private string website_text_reader;
        private string alt_contact;
        private string alt_contact_type;
        private string shopping_assist;
        private string assist_service;
        private string assist_fee;
        private string store_scooter;
        private string scooter_fee;
        private string scooter_location;
        private string restaurant_allergies;
        private string staff_disable_trained;
        private string staff_disable_trained_desc;
        private string items_reach;
        private string service_alt_manner;
        private string senior_discount;
        private string senior_age;
        private string annual_A4A_review;
        private string comment;
        private string recomendations;
        private int est_id;



        //constructors
        public Communication()
        {
            //sigh - set 0 for ints
            this.communication_id = 0;
            this.num_phone = 0;
            this.est_id = 0;
            //set strings to null
            this.acoustics = null;
            this.acoustics_level = null;
            this.alt_comm_methods = null;
            this.alt_com_type = null;
            this.alt_contact = null;
            this.alt_contact_type = null;
            this.annual_A4A_review = null;
            this.assisted_listening = null;
            this.assisted_listen_type = null;
            this.assist_fee = null;
            this.assist_service = null;
            this.auditory_info_visual = null;
            this.captioning_defualt = null;
            this.comment = null;
            this.items_reach = null;
            this.listening_signage = null;
            this.phone_clearance = null;
            this.public_phone = null;
            this.recomendations = null;
            this.restaurant_allergies = null;
            this.scooter_fee = null;
            this.scooter_location = null;
            this.senior_age = null;
            this.senior_discount = null;
            this.service_alt_manner = null;
            this.shopping_assist = null;
            this.staff_ASL = null;
            this.staff_disable_trained = null;
            this.staff_disable_trained_desc = null;
            this.staff_listening = null;
            this.staff_tty = null;
            this.store_scooter = null;
            this.theatre_captioning = null;
            this.theatre_capt_type = null;
            this.tty = null;
            this.visual_info_auditory = null;
            this.website_text_reader = null;
            
        }
        public Communication( int communication_id, string public_phone, string phone_clearance,int num_phone, string tty, string staff_tty,
         string assisted_listening,  string assisted_listen_type, string listening_signage, string staff_listening,string acoustics, string acoustics_level,
         string alt_comm_methods, string alt_com_type, string staff_ASL, string captioning_defualt, string theatre_captioning, string theatre_capt_type,
         string auditory_info_visual, string visual_info_auditory,string website_text_reader, string alt_contact, string alt_contact_type,string shopping_assist,
         string assist_service, string assist_fee, string store_scooter,string scooter_fee, string scooter_location, string restaurant_allergies, string staff_disable_trained,
         string staff_disable_trained_desc, string items_reach, string service_alt_manner, string senior_discount, string senior_age, string annual_A4A_review, string comment,
         string recomendations, int est_id)
        {
            //sigh - set 0 for ints
            this.communication_id = communication_id;
            this.num_phone = num_phone;
            this.est_id = est_id;
            //set strings to null
            this.acoustics = acoustics;
            this.acoustics_level = acoustics_level;
            this.alt_comm_methods = alt_comm_methods;
            this.alt_com_type = alt_comm_methods;
            this.alt_contact = alt_contact;
            this.alt_contact_type = alt_contact_type;
            this.annual_A4A_review = annual_A4A_review;
            this.assisted_listening = assisted_listening;
            this.assisted_listen_type = assisted_listen_type;
            this.assist_fee = assist_fee;
            this.assist_service = assist_service;
            this.auditory_info_visual = auditory_info_visual;
            this.captioning_defualt = captioning_defualt;
            this.comment = comment;
            this.items_reach = items_reach;
            this.listening_signage = listening_signage;
            this.phone_clearance = phone_clearance;
            this.public_phone = public_phone;
            this.recomendations = recomendations;
            this.restaurant_allergies = restaurant_allergies;
            this.scooter_fee = scooter_fee;
            this.scooter_location = scooter_location;
            this.senior_age = senior_age;
            this.senior_discount = senior_discount;
            this.service_alt_manner = service_alt_manner;
            this.shopping_assist = shopping_assist;
            this.staff_ASL = staff_ASL;
            this.staff_disable_trained = staff_disable_trained;
            this.staff_disable_trained_desc = staff_disable_trained_desc;
            this.staff_listening = staff_listening;
            this.staff_tty = staff_tty;
            this.store_scooter = store_scooter;
            this.theatre_captioning = theatre_captioning;
            this.theatre_capt_type = theatre_capt_type;
            this.tty = tty;
            this.visual_info_auditory = visual_info_auditory;
            this.website_text_reader = website_text_reader;
        }

        //getters and setters
        public int Communication_id
        {
            get
            {
                return this.communication_id;
            }
            set
            {
                this.communication_id = value;
            }
        }

        public int Num_phone
        {
            get
            {
                return this.num_phone;
            }
            set
            {
                this.num_phone = value;
            }
        }

        public int Est_id
        {
            get
            {
                return this.est_id;
            }
            set
            {
                this.est_id = value;
            }
        }

        public string Acoustics
        {
            get
            {
                return this.acoustics;
            }
            set
            {
                this.acoustics = value;
            }
        }

        public string Acoustics_level
        {
            get
            {
                return this.Acoustics_level;
            }
            set
            {
                this.Acoustics_level = value;
            }
        }

        public string Alt_comm_methods
        {
            get
            {
                return this.alt_comm_methods;
            }
            set
            {
                this.alt_comm_methods = value;
            }
        }

        public string Alt_com_type
        {
            get
            {
                return this.alt_com_type;
            }
            set
            {
                this.alt_com_type = value;
            }
        }

        public string Alt_contact
        {
            get
            {
                return this.alt_contact;
            }
            set
            {
                this.alt_contact = value;
            }
        }

        public string Alt_contact_type
        {
            get
            {
                return this.Alt_contact_type;
            }
            set
            {
                this.Alt_contact_type = value;
            }
        }

        public string Annual_A4A_review
        {
            get
            {
                return this.annual_A4A_review;
            }
            set
            {
                this.annual_A4A_review = value;
            }
        }

        public string Assisted_listening
        {
            get
            {
                return this.assisted_listening;
            }
            set
            {
                this.assisted_listening = value;
            }
        }

        public string Assisted_listened_type
        {
            get
            {
                return this.assisted_listen_type;
            }
            set
            {
                this.assisted_listen_type = value;
            }
        }

        public string Assist_fee
        {
            get
            {
                return this.assist_fee;
            }
            set
            {
                this.assist_fee = value;
            }
        }

        public string Assist_service
        {
            get
            {
                return this.assist_service;
            }
            set
            {
                this.assist_service = value;
            }
        }

        public string Auditory_info_visual
        {
            get
            {
                return this.auditory_info_visual;
            }
            set
            {
                this.auditory_info_visual = value;
            }
        }

        public string Captioning_defualt
        {
            get
            {
                return this.captioning_defualt;
            }
            set
            {
                this.captioning_defualt = value;
            }
        }

        public string Comment
        {
            get
            {
                return this.comment;
            }
            set
            {
                this.comment = value;
            }
        }

        public string Items_reach
        {
            get
            {
                return this.items_reach;
            }
            set
            {
                this.items_reach = value;
            }
        }

        public string Listening_signage
        {
            get
            {
                return this.listening_signage;
            }
            set
            {
                this.listening_signage = value;
            }
        }

        public string Phone_clearance
        {
            get
            {
                return this.phone_clearance;
            }
            set
            {
                this.phone_clearance = value;
            }
        }

        public string Public_phone
        {
            get
            {
                return this.public_phone;
            }
            set
            {
                this.public_phone = value;
            }
        }

        public string Recommendations
        {
            get
            {
                return this.recomendations;
            }
            set
            {
                this.recomendations = value;
            }
        }

        public string Restaurant_allergies
        {
            get
            {
                return this.restaurant_allergies;
            }
            set
            {
                this.restaurant_allergies = value;
            }
        }

        public string Scooter_fee
        {
            get
            {
                return this.scooter_fee;
            }
            set
            {
                this.scooter_fee = value;
            }
        }

        public string Scooter_location
        {
            get
            {
                return this.scooter_location;
            }
            set
            {
                this.scooter_location = value;
            }
        }

        public string Senior_age
        {
            get
            {
                return this.senior_age;
            }
            set
            {
                this.senior_age = value;
            }
        }

        public string Senior_discount
        {
            get
            {
                return this.senior_discount;
            }
            set
            {
                this.senior_discount = value;
            }
        }

        public string Service_alt_manner
        {
            get
            {
                return this.service_alt_manner;
            }
            set
            {
                this.service_alt_manner = value;
            }
        }

        public string Shopping_assist
        {
            get
            {
                return this.shopping_assist;
            }
            set
            {
                this.shopping_assist = value;
            }
        }

        public string Staff_ASL
        {
            get
            {
                return this.staff_ASL;
            }
            set
            {
                this.staff_ASL = value;
            }
        }

        public string Staff_disable_trained
        {
            get
            {
                return this.Staff_disable_trained;
            }
            set
            {
                this.staff_disable_trained = value;
            }
        }

        public string Staff_disabled_trianed_desc
        {
            get
            {
                return this.staff_disable_trained_desc;
            }
            set
            {
                this.staff_disable_trained_desc = value;
            }
        }

        public string Staff_listening
        {
            get
            {
                return this.staff_listening;
            }
            set
            {
                this.staff_listening = value;
            }
        }

        public string Staff_tty
        {
            get
            {
                return this.staff_tty;
            }
            set
            {
                this.staff_tty = value;
            }
        }

        public string Store_scooter
        {
            get
            {
                return this.store_scooter;
            }
            set
            {
                this.store_scooter = value;
            }
        }

        public string Theatre_captioning
        {
            get
            {
                return this.theatre_captioning;
            }
            set
            {
                this.theatre_captioning = value;
            }
        }

        public string Theatre_capt_type
        {
            get
            {
                return this.theatre_capt_type;
            }
            set
            {
                this.theatre_capt_type = value;
            }
        }

        public string Tty
        {
            get
            {
                return this.tty;
            }
            set
            {
                this.tty = value;
            }
        }

        public string Visual_info_auditory
        {
            get
            {
                return this.visual_info_auditory;
            }
            set
            {
                this.visual_info_auditory = value;
            }
        }

        public string Website_text_reader
        {
            get
            {
                return this.website_text_reader;
            }
            set
            {
                this.website_text_reader = value;
            }
        }


    }

   
}