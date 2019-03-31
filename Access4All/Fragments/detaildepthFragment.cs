using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Text.Style;
using Android.Util;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json.Linq;
using Environment = System.Environment;

//HTML.FromHtml is deprecated,so it might stop being supported soonish

namespace Access4All.Fragments
{
    public class detaildepthFragment : Android.Support.V4.App.Fragment, MainActivity.IBackButtonListener
    {
        string curLocation;
        string selection;
        string prevView;
        List<Categories> group = new List<Categories>();
        TextView myTextTest;
        string table; //= "establishment";//change this later cuz parking dont work
        int est_id;

        public override void OnCreate(Bundle savedInstanceState)
        {
            Bundle b = Arguments;
            curLocation = b.GetString("location");
            selection = b.GetString("selection");
            prevView = b.GetString("prevView");
            string test = curLocation + " " + selection;
            //Toast.MakeText(MainActivity.activity, test, ToastLength.Short).Show();

           
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public static detaildepthFragment NewInstance()
        {
            var detaildepthfrag = new detaildepthFragment { Arguments = new Bundle() };
            return detaildepthfrag;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View v = inflater.Inflate(Resource.Layout.detail_layout, null);
            TextView t = (TextView)v.FindViewById(Resource.Id.textView1);
            myTextTest = (TextView)v.FindViewById(Resource.Id.textView1);
            //setTempData();
            //t.Text = data;

            /*var htmlCode = "<ul><li>Item 1</li><li>Item 2</li></ul>";
            var myTextView = FindViewById<TextView>(Resource.Id.myTextView);
            myTextView.TextFormatted = Android.Text.Html.FromHtml(htmlCode);*/

            string unparsedData,parsedData;
            //SpannableString parsed;
            Bundle b = Arguments;
            curLocation = b.GetString("location");
            selection = b.GetString("selection");
            string test = curLocation + " " + selection;
            
            for (int i = 0; i < SplashActivity.ALL_LOCATIONS.Count; i++)
            {
                if (curLocation.CompareTo(SplashActivity.ALL_LOCATIONS[i].name) == 0)
                    est_id = SplashActivity.ALL_LOCATIONS[i].est_id;
            }

            
            if (selection.CompareTo("Information") == 0)
            {
                Toast.MakeText(this.Activity, est_id.ToString(), ToastLength.Long).Show();
                //Toast.MakeText(MainActivity.activity, test, ToastLength.Short).Show();
                table = "establishment";
                unparsedData = GetData("");//getGeneralInformation(curLocation);
                parsedData = parseGeneralInformation(unparsedData, curLocation);
                t.Text = parsedData;
            }

            else if (selection.CompareTo("Parking on street") == 0)
            {
                table = "parking";
                unparsedData = GetData(est_id.ToString());
                //parsed = parseParkingInformation(unparsedData, curLocation);
                //t.TextFormatted = parsed;
                parsedData = parseParkingInformation(unparsedData, curLocation);
                //t.TextFormatted = Html.FromHtml(parsedData);
                t.Text = parsedData;
            }

            else if (selection.CompareTo("Access to transit") == 0)
            {
                Toast.MakeText(MainActivity.activity, test, ToastLength.Short).Show();
                unparsedData = getTransitData(curLocation);
                parsedData = parseTransitData(unparsedData);
                t.Text = parsedData;
            }

            else if (selection.CompareTo("Exterior pathway & seating") == 0)
            {
                Toast.MakeText(MainActivity.activity, test, ToastLength.Short).Show();
                table = "exterior_pathways";
                unparsedData = GetData(table);
                parsedData = parseExteriorData(unparsedData);
                t.Text = parsedData;
            }

            else if (selection.CompareTo("Entrances") == 0)
            {
                table = "main_entrance";
                unparsedData = GetData(curLocation);
                parsedData = parseMainEntrance(unparsedData, curLocation);
                t.Text = parsedData;
                Toast.MakeText(MainActivity.activity, test, ToastLength.Short).Show();
            }

            else if (selection.CompareTo("Elevators") == 0)
            {
                table = "elevator";
                unparsedData = GetData(curLocation);
                parsedData = parseElevators(unparsedData);
                t.Text = parsedData;
                Toast.MakeText(MainActivity.activity, test, ToastLength.Short).Show();
            }

            else if (selection.CompareTo("Interior") == 0)
            {
                table = "interior";
                unparsedData = GetData(table);
                parsedData = parseInterior(unparsedData);
                t.Text = parsedData;
                Toast.MakeText(MainActivity.activity, test, ToastLength.Short).Show();
            }

            else if (selection.CompareTo("Seating") == 0)
            {
                Toast.MakeText(MainActivity.activity, test, ToastLength.Short).Show();
            }

            else if (selection.CompareTo("Restroom") == 0)
            {
                Toast.MakeText(MainActivity.activity, test, ToastLength.Short).Show();
            }

            else if (selection.CompareTo("Communication, Technologies & Customer Service") == 0)
            {
                table = "communication";
                unparsedData = GetData(table);
                parsedData = parseCommunication(unparsedData);
                t.Text = parsedData;
                Toast.MakeText(MainActivity.activity, test, ToastLength.Short).Show();
            }

            else
            {
               
            }
            return v;   
        }

        private string parseElevators(string unparsedData)
        {
            JArray jsonArray = JArray.Parse(unparsedData);
            string data = "";

            string is_elevator;
            string location;
            string works;
            string no_assist;
            string button_height;
            string outside_btn_height;
            string inside_btn_height;
            string button_use_fist;
            string braille;
            string audible_tones;
            string lighting;
            string lighting_type;
            string elevator_depth;
            string comment;

            for (int i = 0; i < jsonArray.Count; i++)//this should only ever be one, but keep it here in case something goes wrong?
            {
                JToken json = jsonArray[i];

                if (((int)json["est_id"]) == est_id)
                {
                    is_elevator = (string)json["is_elevator"];
                    location = (string)json["location"];
                    works = (string)json["works"];
                    no_assist = (string)json["no_assist"];
                    button_height = (string)json["button_height"];
                    outside_btn_height = (string)json["outside_btn_height"];
                    inside_btn_height = (string)json["inside_btn_height"];
                    button_use_fist = (string)json["button_use_fist"];
                    braille = (string)json["braille"];
                    audible_tones = (string)json["audible_tones"];
                    lighting = (string)json["lighting"];
                    lighting_type = (string)json["lighting_type"];
                    elevator_depth = (string)json["elevator_depth"];
                    comment = (string)json["comment"];

                    if(is_elevator.ToLower().CompareTo("yes")==0)
                    {
                        data += "• There is an elevator at this establishment located at " + location + " and it " + works + "\n\r";
                    }
                    

                    if (no_assist.ToLower().CompareTo("yes")==0)
                    {
                        data += "• No assistance is provided for the elevator(s)" + "\n\r";
                    }
                    if(button_height.ToLower().CompareTo("yes")==0)
                    {
                        data += "• The outside button height is " + outside_btn_height + " inches, the inside button height is " + " inches, and the buttons are able to be pushed with a closed fist: " + button_use_fist + "\n\r";
                    }
                    if(braille.ToLower().CompareTo("yes")==0)
                    {
                        data += "• There is braille information for the elevator" + "\n\r";
                    }
                    if(audible_tones.ToLower().CompareTo("yes")==0)
                    {
                        data += "• There are audible tones in the elevator" + "\n\r";
                    }
                    if(lighting.ToLower().CompareTo("yes")==0)
                    {
                        data += "• Lighting level is " + lighting_type + " in daytime, and is adequate for mobility and reading signs" + "\n\r";
                    }
                    if(elevator_depth.ToLower().CompareTo("yes")==0)
                    {
                        data += "• Elevator depth is at least 51 inches" + "\n\r";
                    }
                    if(comment.CompareTo("")!=0)
                    {
                        data += "• " + comment + "\n\r"; 
                    }
                }
            }

            if (data.CompareTo("")==0)
            {
                data = "• No elevator or lift is needed as this business is on the ground level";
            }
            return data;
        }

        private string parseCommunication(string unparsedData)
        {
            JArray jsonArray = JArray.Parse(unparsedData);
            string data = "";

            string public_phone;
            string phone_clearance;
            string num_phone;
            string tty;
            string staff_tty;
            string assisted_listening;
            string assisted_listen_type;
            string assisted_listen_receiver;
            string listening_signage;
            string staff_listening;
            string acoustics;
            string acoustics_level;
            string alt_comm_methods;
            string alt_comm_type;
            string staff_ASL;
            string captioning_default;
            string theater_captioning;
            string theater_capt_type;
            string auditory_info_visual;
            string visual_info_auditory;
            string website_text_reader;
            string alt_contact;
            string alt_contact_type;
            string shopping_assist;
            string assist_service;
            string assist_fee;
            string store_scooter;
            string scooter_fee;
            string scooter_location;
            string restaurant_allergies;
            string staff_disable_trained;
            string staff_disable_trained_desc;
            string items_reach;
            string service_alt_manner;
            string senior_discount;
            string senior_age;
            string annual_A4A_review;
            string comment;

            for (int i = 0; i < jsonArray.Count; i++)//this should only ever be one, but keep it here in case something goes wrong?
            {
                JToken json = jsonArray[i];

                if (((int)json["est_id"]) == est_id)
                {
                    public_phone = (string)json["public_phone"];
                    phone_clearance = (string)json["phone_clearance"];
                    num_phone = (string)json["num_phone"];
                    tty = (string)json["tty"];
                    staff_tty = (string)json["staff_tty"];
                    assisted_listening = (string)json["assisted_listening"];
                    assisted_listen_type = (string)json["assisted_listen_type"];
                    assisted_listen_receiver = (string)json["assisted_listen_receiver"];
                    listening_signage = (string)json["listening_signage"];
                    staff_listening = (string)json["staff_listening"];
                    acoustics = (string)json["acoustics"];
                    acoustics_level = (string)json["acoustics_level"];
                    alt_comm_methods = (string)json["alt_comm_methods"];
                    alt_comm_type = (string)json["alt_comm_type"];
                    staff_ASL = (string)json["staff_ASL"];
                    captioning_default = (string)json["captioning_default"];
                    theater_captioning = (string)json["theater_captioning"];
                    theater_capt_type = (string)json["theater_capt_type"];
                    auditory_info_visual = (string)json["auditory_info_visual"];
                    visual_info_auditory = (string)json["visual_info_auditory"];
                    website_text_reader = (string)json["website_text_reader"];
                    alt_contact = (string)json["alt_contact"];
                    alt_contact_type = (string)json["alt_contact_type"];
                    shopping_assist = (string)json["shopping_assist"];
                    assist_service = (string)json["assist_service"];
                    assist_fee = (string)json["assist_fee"];
                    store_scooter = (string)json["store_scooter"];
                    scooter_fee = (string)json["scooter_fee"];
                    scooter_location = (string)json["scooter_location"];
                    restaurant_allergies = (string)json["restaurant_allergies"];
                    staff_disable_trained = (string)json["staff_disable_trained"];
                    staff_disable_trained_desc = (string)json["staff_disable_trained_desc"];
                    items_reach = (string)json["items_reach"];
                    service_alt_manner = (string)json["service_alt_manner"];
                    senior_discount = (string)json["senior_discount"];
                    senior_age = (string)json["senior_age"];
                    annual_A4A_review = (string)json["annual_A4A_review"];
                    comment = (string)json["comment"];


                    if(public_phone.ToLower().CompareTo("yes")==0)
                    {
                        data += "• One or more public phones are available w/adjustable volume control" + "\n\r";
                    }
                    if(phone_clearance.ToLower().CompareTo("yes")==0)
                    {
                        data += "• Public phones have controls min 48 inches from floor, protruding < 4 inches from wall" + "\n\r";
                    }
                    if(tty.ToLower().CompareTo("yes")==0)
                    {
                        data += "• TTY is available" + "\n\r";
                    }
                    if(staff_tty.ToLower().CompareTo("yes")==0)
                    {
                        data += "• TTY assistance from staff is available" + "\n\r";
                    }
                    if(assisted_listening.ToLower().CompareTo("yes")==0)
                    {
                        data += "• Assisted listening is available of type" + assisted_listen_type + " with assisted listen receiver " + assisted_listen_receiver + "\n\r";
                    }
                    if(listening_signage.ToLower().CompareTo("yes")==0)
                    {
                        data += "• Listening signage is available" + "\n\r";
                    }
                    if(staff_listening.ToLower().CompareTo("yes")==0)
                    {
                        data += "• Staff listening is available" + "\n\r";
                    }
                    if(acoustics.ToLower().CompareTo("yes")==0)
                    {
                        data += "• Acoustics are comfortable (no echoing, loud music, etc). Noise level = " + acoustics_level + "\n\r";
                    }
                    if(alt_comm_methods.ToLower().CompareTo("yes")==0)
                    {
                        data += "• If a customer is unable to hear, other forms of communication include: " + alt_comm_type + "\n\r";
                    }
                    if(staff_ASL.ToLower().CompareTo("yes")==0)
                    {
                        data += "• Some or all of the staff are proficient with sign language" + "\n\r";
                    }
                    if(captioning_default.ToLower().CompareTo("yes")==0)
                    {
                        data += "• By default, captioning is available" + "\n\r";
                    }
                    if(theater_captioning.ToLower().CompareTo("yes")==0)
                    {
                        data += "• Theater captioning is available of type " + theater_capt_type + "\n\r";
                    }
                    if(auditory_info_visual.ToLower().CompareTo("yes") == 0)
                    {
                        data += "• Auditory information is presented visually (special of the day written down)" + "\n\r";
                    }
                    if(visual_info_auditory.ToLower().CompareTo("yes")==0)
                    {
                        data += "• Visual information is presented audibly (employees will read information out load, etc.)" + "\n\r";
                    }
                    if(website_text_reader.ToLower().CompareTo("yes")==0)
                    {
                        data += "• The establishment’s website is accessible to users of screen text readers" + "\n\r";
                    }
                    if(alt_contact.ToLower().CompareTo("yes")==0)
                    {
                        data += "• The following alternate means are available for patrons to order, contact, or schedule: " + alt_contact_type + "\n\r";
                    }
                    if(shopping_assist.ToLower().CompareTo("yes")==0)
                    {
                        data += "• The establishment offers shopping assistance or delivery on a case-by-case basis" + "\n\r";
                    }
                    if(assist_service.ToLower().CompareTo("yes")==0)
                    {
                        data += "• Assistance services are available at a cost of $" + assist_fee + "\n\r";
                    }
                    if(store_scooter.ToLower().CompareTo("yes")==0)
                    {
                        data += "• The establishment provides scooters at a cost of $" + scooter_fee + " and are located at " + scooter_location + "\n\r";
                    }
                    if(restaurant_allergies.ToLower().CompareTo("yes")==0)
                    {
                        data += "• Information on food allergies sensitivities are available" + "\n\r";
                    }
                    if(staff_disable_trained.ToLower().CompareTo("yes")==0)
                    {
                        data += "• The staff are disability trained in: " + staff_disable_trained_desc + "\n\r";
                    }
                    if(items_reach.ToLower().CompareTo("yes")==0)
                    {
                        data += "• All items are within reach or assistance is offered to reach them" + "\n\r";
                    }
                    if(service_alt_manner.ToLower().CompareTo("yes")==0)
                    {
                        data += "• If goods and services are not accessible they are provided in alternative manner" + "\n\r";
                    }
                    if(senior_discount.ToLower().CompareTo("yes")==0)
                    {
                        data += "• The establishment offers a senior discount, beginning at age " + senior_age + "\n\r";
                    }
                    if(comment.CompareTo("")!=0)
                    {
                        data += "• " + comment + "\n\r";
                    }

                }
            }
            return data;
        }

        private string parseInterior(string unparsedData)
        {
            JArray jsonArray = JArray.Parse(unparsedData);
            string data = "";

            int interior_id;
            string int_door_open_clearance;
            double int_opening_measurement;
            string int_door_easy_open;
            double int_door_open_force;
            string int_door_use_with_fist;
            string five_second_close;
            string hallway_width;
            double narrowest_width;
            string wheelchair_turnaround;
            string hallway_obstacles;
            string hallway_clear;
            string lighting;
            string lighting_type;
            string service_counter;
            double counter_height;
            double writing_surface_height;
            string drinking_fountain;
            string comment;


            for (int i = 0; i < jsonArray.Count; i++)//this should only ever be one, but keep it here in case something goes wrong?
            {
                JToken json = jsonArray[i];

                if (((int)json["est_id"]) == est_id)
                {
                    interior_id = (int)json["interior_id"];
                    int_door_open_clearance = (string)json["int_door_open_clearance"];
                    int_opening_measurement = (double)json["int_opening_measurement"]; 
                    int_door_easy_open = (string)json["int_door_easy_open"]; 
                    int_door_open_force = (double)json["int_door_open_force"];
                    int_door_use_with_fist = (string)json["int_door_use_with_fist"];
                    five_second_close = (string)json["five_second_close"];
                    hallway_width = (string)json["hallway_width"];
                    narrowest_width = (double)json["narrowest_width"];
                    wheelchair_turnaround = (string)json["wheelchair_turnaround"];
                    hallway_obstacles = (string)json["hallway_obstacles"];
                    hallway_clear = (string)json["hallway_clear"];
                    lighting = (string)json["lighting"];
                    lighting_type = (string)json["lighting_type"]; 
                    service_counter = (string)json["service_counter"]; 
                    counter_height = (double)json["counter_height"]; 
                    writing_surface_height = (double)json["writing_surface_height"]; 
                    drinking_fountain = (string)json["drinking_fountain"];
                    comment = (string)json["comment"];


                    if(int_door_open_clearance.ToLower().CompareTo("yes")==0)
                    {
                        data += "• Interior doors (aside from restrooms) have at least 32 inches clearance when the door is open at 90 degrees" + "\n\r";
                    }

                    if(int_opening_measurement > 0)
                    {
                        data += "• Interior door is "+ int_opening_measurement + " inches wide\n\r";
                    }

                    if(int_door_easy_open.ToLower().CompareTo("yes")==0)
                    {
                        data += "• Interior doors are easy to open, requiring 5 lbs. or less of force(" + int_opening_measurement + " lbs)\n\r";
                    }

                    if(int_door_use_with_fist.ToLower().CompareTo("yes")==0)
                    {
                        data += "• Door handles can be operated with a closed fist" + "\n\r";
                    }

                    if(five_second_close.ToLower().CompareTo("yes")==0)
                    {
                        data += "• Doors stay open for at least five seconds" + "\n\r";
                    }

                    if(hallway_width.ToLower().CompareTo("yes")==0)
                    {
                        data += "• Hallways and aisles are at least 36 inches wide, or not less than " + narrowest_width + " inches for four foot intervals\n\r";
                    }

                    if(wheelchair_turnaround.ToLower().CompareTo("yes")==0)
                    {
                        data += "• There are locations that allow 60 inches space for a wheelchair to turn around" + "\n\r";
                    }

                    if(hallway_obstacles.ToLower().CompareTo("yes")==0)
                    {
                        data += "• Hallways and aisles are clear of obstacles, tripping hazards, objects protruding more than 4 inches or lower than 80 inches" + "\n\r";
                    }

                    if(lighting.ToLower().CompareTo("yes")==0)
                    {
                        data += "• Lighting level is " + lighting_type + " in daytime, and is adequate for mobility and reading signs" + "\n\r";
                    }

                    if(service_counter.ToLower().CompareTo("yes")==0)
                    {
                        data += "• Lowest service counter is no higher than " + counter_height + " inches with a clear view from a sitting position and a check writing surface is no higher than " + writing_surface_height + " inches\n\r";
                    }

                    if(drinking_fountain.ToLower().CompareTo("yes")==0)
                    {
                        data += "• Accessible drinking fountain has spout no higher than 36 inches from floor and easy to operate controls" + "\n\r";
                    }

                    if(comment.CompareTo("")!=0)
                    {
                        data += "• " + comment + "\n\r";
                    }
                }
            }
            return data;
        }

        private string parseMainEntrance(string unparsedData, string curLocation)
        {
            JArray jsonArray = JArray.Parse(unparsedData);
            string data = "";

            int main_ent_id;
            int total_num_public_entrances;
            string main_ent_accessible;
            string alt_ent_accessible;
            string accessable_signage;
            string ground_level;
            string threshold_level;
            string threshold_beveled;
            double beveled_height;
            string door_action;
            double opening_measurement;
            string door_open_clearance;
            double door_open_force;
            string door_easy_open;
            string door_use_with_fist;
            string lighting_type;
            string door_auto_open;
            string second_door_inside;
            string min_dist_between_doors;
            string lighting;
            string lighting_option;
            string comment;


            for (int i = 0; i < jsonArray.Count; i++)//this should only ever be one, but keep it here in case something goes wrong?
            {
                JToken json = jsonArray[i];

                if (((int)json["est_id"]) == est_id)
                {
                    main_ent_id = (int)json["main_ent_id"];
                    total_num_public_entrances = ((int)json["total_num_public_entrances"]);
                    main_ent_accessible = ((string)json["main_ent_accessible"]).ToLower();
                    alt_ent_accessible = ((string)json["alt_ent_accessible"]).ToLower();
                    accessable_signage = ((string)json["accessable_signage"]).ToLower();
                    ground_level = ((string)json["ground_level"]).ToLower();
                    threshold_level = ((string)json["threshold_level"]).ToLower();
                    threshold_beveled = ((string)json["threshold_beveled"]).ToLower();
                    beveled_height = ((double)json["beveled_height"]);
                    door_open_clearance = ((string)json["door_open_clearance"]).ToLower();
                    door_action = ((string)json["door_action"]).ToLower();
                    door_open_force = ((double)json["door_open_force"]);
                    door_easy_open = ((string)json["door_easy_open"]).ToLower();
                    door_use_with_fist = ((string)json["door_use_with_fist"]).ToLower();
                    door_auto_open = ((string)json["door_auto_open"]).ToLower();
                    second_door_inside = ((string)json["second_door_inside"]).ToLower();
                    min_dist_between_doors = ((string)json["min_dist_between_doors"]).ToLower();
                    lighting = ((string)json["lighting"]).ToLower();
                    opening_measurement = ((double)json["opening_measurement"]);
                    lighting_type = ((string)json["lighting_type"]).ToLower();
                    lighting_option = ((string)json["lighting_option"]).ToLower();
                    comment = ((string)json["comment"]).ToLower();

                    data += ("• " + "The establishment has " + total_num_public_entrances + " public entrances.\n\r");

                    if (main_ent_accessible.ToLower().CompareTo("yes") == 0)
                        data += ("• The main entrance is wheelchair accessible. \n\r");


                    if (ground_level.ToLower().CompareTo("yes") == 0)
                        data += ("• Ground floor is level inside and outside entrance door. \n\r");
                    else
                        data += ("• Ground floor is not level inside and outside entrance door. \n\r");


                    if (threshold_level.ToLower().CompareTo("yes") == 0)
                        data += ("• Threshold of door is level. \n\r");
                    else
                        data += ("• Threshold of door is not level. \n\r");

                    if (threshold_beveled.ToLower().CompareTo("yes") == 0)
                    {
                        if (beveled_height <= 0.5)
                            data += ("• Door threshold is no more than ½ inch high. \n\r");
                        else
                            data += ("• Door threshold is more than ½ inch high. (actual "+beveled_height+" of an inch). \n\r");
                    }


                    if (door_action.ToLower().CompareTo("open in") == 0)
                        data += ("• As you enter, door opens away from you. \n\r");
                    else if(door_action.ToLower().CompareTo("open out") == 0)
                        data += ("• As you enter, door opens toward you. \n\r");
                    else
                        data += ("• As you enter, door slides to the side. \n\r");

                    if (opening_measurement > 0.00)
                        data += ("• Door has at least "+opening_measurement+" inch clearance when door is open 90 degrees. \n\r");

                    if (door_easy_open.ToLower().CompareTo("yes") == 0)
                        data += ("• Door is easy to open, requiring 10 lbs or less of force ("+ door_open_force + " lbs). \n\r");
                    else
                        data += ("• Door is hard to open, requiring 10 lbs or more of force (" + door_open_force + " lbs). \n\r");

                    if (door_use_with_fist.ToLower().CompareTo("yes") == 0)
                        data += ("• Door handles can be operated with closed fist. \n\r");
                    else
                        data += ("• Door handles can not be operated with closed fist. \n\r");

                    if (lighting.ToLower().CompareTo("yes") == 0)
                        data += ("• Lighting level is"+lighting_type+" in "+lighting_option+"time, and is adequate for mobility and reading signs. \n\r");
                    else
                        data += ("• Lighting level is poor. \n\r");
                }
            }

            return data;
        }

        private string parseExteriorData(string unparsedData)
        {
            JArray jsonArray = JArray.Parse(unparsedData);
            string data = "";

            SpannableString result;

            //int ext_path_id;
            string service_animal;
            string service_animal_location;
            string has_exterior_path;
            string min_width;
            string pathway_surface;
            string pathway_curbs;
            string tactile_warning;
            string slope;
            string lighting;
            string lighting_option;
            string lighting_type;
            string comment;

            for (int i = 0; i < jsonArray.Count; i++)//this should only ever be one, but keep it here in case something goes wrong?
            {
                JToken json = jsonArray[i];

                if (((int)json["est_id"]) == est_id)
                {
                    service_animal = (string)json["service_animal"];
                    service_animal_location = (string)json["service_animal_location"];
                    has_exterior_path = (string)json["has_exterior_path"];
                    min_width = (string)json["min_width"];
                    pathway_surface = (string)json["pathway_surface"];
                    pathway_curbs = (string)json["pathway_curbs"];
                    tactile_warning = (string)json["tactile_warning"];
                    slope = (string)json["slope"];
                    lighting = (string)json["lighting"];
                    lighting_option = (string)json["lighting_option"];
                    lighting_type = (string)json["lighting_type"];
                    comment = (string)json["comment"];

                    if (has_exterior_path.ToLower().CompareTo("yes") == 0)
                        data += "• This establishment has exterior pathway";

                    if (min_width.ToLower().CompareTo("yes")==0)
                        data += "• Sidewalk pathway is minimum 44 inches wide" + "\n\r";

                    if (pathway_curbs.ToLower().CompareTo("yes") == 0)
                        data += "• Pathway has curb ramps and curb cuts where needed" + "\n\r";

                    if (pathway_surface.ToLower().CompareTo("yes") == 0)
                        data += "• Surface is slip resistant, free of obstacles" + "\n\r";

                    if (tactile_warning.ToLower().CompareTo("yes") == 0)
                        data += "• There are tactile warning strips or high contrast paint at curb ramps, stairwells, building entrances, parking areas and pedestrian crossings" + "\n\r";

                    /*if(slope.ToLower().CompareTo("yes")==0)
                    {
                        data += "• Exterior has a ramp to enter the establishment" + "\n\r";
                        data += "• Ramp is at least 36 inches wide between handrails" + "\n\r";
                        data += "• For each section of the ramp, the running slope is no greater than 1:12, i.e. for every inch of height change there are at least 12 inches of ramp run. (actual 13 inches height & 204 inches length)" + "\n\r";
                        data += "• There is a level landing that is at least 60 inches long and at least as wide as the ramp at top of ramp" + "\n\r";
                        data += "• The ramp is clear of obstacles and protrusions of 4 inches or more from the sides" + "\n\r";
                        data += "• Ramp surface is firm, slip-resistant, and unbroken" + "\n\r";
                    }*/
                    //This section needs to be parsed in the ramp section

                    if (service_animal.ToLower().CompareTo("yes")==0)
                        data += "• Service animal relief area at " + service_animal_location + "\n\r";

                    if(lighting.ToLower().CompareTo("yes")==0)
                    {
                        data += "• Lighting level is " + lighting_type + " in " + lighting_option + ", and is adequate for mobility and reading signs" + "\n\r";
                    }
                }
            }
            return data;
        }

        private string parseParkingInformation(string unparsedData, string loc)
        {
            JArray jsonArray = JArray.Parse(unparsedData);
            string data = "";

            SpannableString result;

            int park_id;
            string lot_type;
            string street_metered;
            string parking_type;
            string total_spaces;
            string reserved_spaces;
            string general_accessible_spaces;
            string van_accessible_spaces;
            string reserve_space_sign;
            string reserve_space_obstacles;
            string comment;
            string recommendations;

            for (int i = 0; i < jsonArray.Count; i++)//this should only ever be one, but keep it here in case something goes wrong?
            {
                JToken json = jsonArray[i];

                if (((int)json["est_id"]) == est_id)
                {
                    park_id = (int)json["park_id"];
                    lot_type = ((string)json["lot_free"]).ToLower();
                    street_metered = ((string)json["street_metered"]).ToLower();
                    parking_type = ((string)json["parking_type"]).ToLower();//if "other" then ignore
                    total_spaces = ((string)json["total_num_spaces"]).ToLower();
                    reserved_spaces = ((string)json["num_reserved_spaces"]).ToLower();
                    general_accessible_spaces = ((string)json["num_accessable_space"]).ToLower();//and so it begins
                    van_accessible_spaces = ((string)json["num_van_accessible"]).ToLower();
                    reserve_space_sign = ((string)json["reserve_space_sign"]).ToLower();
                    reserve_space_obstacles = ((string)json["reserve_space_obstacles"]).ToLower();
                    comment = ((string)json["comment"]).ToLower();

                    if (street_metered.CompareTo("not metered") == 0)//• = alt + 7 on numpad
                        street_metered = "free";

                    data += "• This establishment has the following types of parking: " + lot_type + " lot, " + street_metered + " street\n\r";
                    data += "• There are a total of " + total_spaces + " on the premises\n\r";

                    if (parking_type.CompareTo("other") != 0)
                        data += "• This establishment has " + parking_type + " parking\n\r";

                    data += "• " + general_accessible_spaces + " accessible parking spaces have a 5 foot loading aisle\n\r";
                    data += "• " + van_accessible_spaces + " ‘van accessible’ parking spaces have an 8 foot loading aisle\n\r";

                    if (reserve_space_sign.CompareTo("yes") == 0)
                        data += "• " + "Accessible parking spaces have signs that are not obstructed when a vehicle is parked there\n\r";

                    if (reserve_space_obstacles.CompareTo("no") == 0)//means free of obstacles, bad variable name  ->last team<-
                        data += "• Surface is not level, unbroken, firm, slip resistant, or free of obstacles\n\r";
                    else
                        data += "• Surface is level, unbroken, firm, slip resistant, and free of obstacles\n\r";

                    /**TODO: passenger_loading and route_from_parking**/

                    /*if (street_metered.CompareTo("not metered") == 0)
                        street_metered = "free";

                    data += "<ul>This establishment has the following types of parking: " + lot_type + " lot, " + street_metered + " street\n\r";
                    data += "<li>There are a total of " + total_spaces + " on the premises</li>";

                    if (parking_type.CompareTo("other") != 0)
                        data += "<li>This establishment has " + parking_type + " parking</li>";

                    data += "<li>" + general_accessible_spaces + " accessible parking spaces have 5’ access aisle</li>";*/
                }
            }

            //result = new SpannableString("Parking:\n•this is a test");
            //result.SetSpan(new BulletSpan(40, Color.Aqua), 10, 22, SpanTypes.ExclusiveExclusive);
            return data; //+"</ul>";
            //return result;
        }

        private string parseGeneralInformation(string unparsedData, string loc)
        {
            JArray jsonArray = JArray.Parse(unparsedData);
            string text = loc + Environment.NewLine;
            
            string website;
            for (int i = 0; i < jsonArray.Count; i++)
            {
                JToken json = jsonArray[i];

                if (((string)json["name"]).Equals(loc))
                {
                    website = ((string)json["website"]); 
                    loc += Environment.NewLine;
                    loc += (((string)json["street"]) +" "+ ((string)json["city"]) +", " +((string)json["state"])+" "+ ((string)json["zip"]) + Environment.NewLine);
                    loc += Environment.NewLine;
                    loc += (website + Environment.NewLine);
                    loc += Environment.NewLine;
                    loc += ((string)json["phone"]);
                }
                    
            }
           // Toast.MakeText(MainActivity.activity, id, ToastLength.Short).Show();
            return loc;
        }

       

        private string parseTransitData(string unparsedData)
        {
            //throw new NotImplementedException();
            string parsedData = null;

            return parsedData;
        }

        private string getTransitData(string curLocation)
        {
            //throw new NotImplementedException();
            string myData = null;

            return myData;
        }

        public void OnBackPressed()
        {
            //Get which object was selected
            Android.Support.V4.App.Fragment fragment = null;
            Bundle args = new Bundle();
            args.PutString("location", curLocation);
            args.PutString("selection", selection);
            args.PutString("prevView", prevView);
            fragment = detailFragment.NewInstance();
            fragment.Arguments = args;
            base.FragmentManager.BeginTransaction()
                        .Replace(Resource.Id.content_frame, fragment)
                        .Commit();
        }

        private void setTempData()
        {
            /*data = GetData("");
            JArray jsonArray = JArray.Parse(data);

            Console.WriteLine(jsonArray);

            List<string> arts_and_entertainment_locations = new List<string>();
            List<string> automotive_locations = new List<string>();
            List<string> bank_and_finance_locations = new List<string>();
            List<string> education_locations = new List<string>();
            List<string> food_and_drink_locations = new List<string>();
            List<string> government_and_community_locations = new List<string>();
            List<string> healthcare_locations = new List<string>();
            List<string> news_and_media_locations = new List<string>();
            List<string> professional_services_locations = new List<string>();
            List<string> real_estate_locations = new List<string>();
            List<string> religion_locations = new List<string>();
            List<string> retail_locations = new List<string>();
            List<string> sports_and_recreation_locations = new List<string>();
            List<string> travel_locations = new List<string>();
            List<string> utilities_locations = new List<string>();
            List<string> other_locations = new List<string>();

            List<string> business_locations = new List<string>();
            List<string> home_and_garden_locations = new List<string>();
            List<string> nightlife_locations = new List<string>();
            List<string> personal_services_locations = new List<string>();
            List<string> pet_locations = new List<string>();
            List<string> restaurant_and_coffee_shop_locations = new List<string>();
            business_locations.Add("not in db");
            home_and_garden_locations.Add("not in db");
            nightlife_locations.Add("not in db");
            personal_services_locations.Add("not in db");
            pet_locations.Add("not in db");
            restaurant_and_coffee_shop_locations.Add("not in db");



            for (int i = 0; i < jsonArray.Count; i++)
            {
                JToken json = jsonArray[i];

                if ((int)json["cat_id"] == 1)
                    arts_and_entertainment_locations.Add((string)json["name"]);

                else if ((int)json["cat_id"] == 2)
                    automotive_locations.Add((string)json["name"]);

                else if ((int)json["cat_id"] == 3)
                    bank_and_finance_locations.Add((string)json["name"]);

                else if ((int)json["cat_id"] == 4)
                    education_locations.Add((string)json["name"]);

                else if ((int)json["cat_id"] == 5)
                    food_and_drink_locations.Add((string)json["name"]);

                else if ((int)json["cat_id"] == 6)
                    government_and_community_locations.Add((string)json["name"]);

                else if ((int)json["cat_id"] == 7)
                    healthcare_locations.Add((string)json["name"]);

                else if ((int)json["cat_id"] == 8)
                    news_and_media_locations.Add((string)json["name"]);

                else if ((int)json["cat_id"] == 9)
                    professional_services_locations.Add((string)json["name"]);

                else if ((int)json["cat_id"] == 10)
                    real_estate_locations.Add((string)json["name"]);

                else if ((int)json["cat_id"] == 11)
                    religion_locations.Add((string)json["name"]);

                else if ((int)json["cat_id"] == 12)
                    retail_locations.Add((string)json["name"]);

                else if ((int)json["cat_id"] == 13)
                    sports_and_recreation_locations.Add((string)json["name"]);

                else if ((int)json["cat_id"] == 14)
                    travel_locations.Add((string)json["name"]);

                else if ((int)json["cat_id"] == 15)
                    utilities_locations.Add((string)json["name"]);

                else if ((int)json["cat_id"] == 16)
                    other_locations.Add((string)json["name"]);
            }

            group.Add(new Categories("Arts, Entertainment, Culture", SplashActivity.arts_and_entertainment_locations));
            group.Add(new Categories("Automotive", SplashActivity.automotive_locations));
            group.Add(new Categories("Business Services", SplashActivity.business_locations));//not in db
            group.Add(new Categories("Education", SplashActivity.education_locations));
            group.Add(new Categories("Financial Services", SplashActivity.bank_and_finance_locations));
            group.Add(new Categories("Food, Groceries", SplashActivity.food_and_drink_locations));
            group.Add(new Categories("Public Services, Government", SplashActivity.government_and_community_locations));
            group.Add(new Categories("Health, Medical, Dental, Mobility aids", SplashActivity.healthcare_locations));
            group.Add(new Categories("Home & Garden", SplashActivity.home_and_garden_locations));//not in db
            group.Add(new Categories("Mass Media, Printing, Publishing", SplashActivity.news_and_media_locations));
            group.Add(new Categories("Nightlife", SplashActivity.nightlife_locations));//not in db
            group.Add(new Categories("Recreation, Fitness", SplashActivity.sports_and_recreation_locations));
            group.Add(new Categories("Personal Services", SplashActivity.personal_services_locations));//not in db
            group.Add(new Categories("Pets", SplashActivity.pet_locations));//not in db
            group.Add(new Categories("Professional Services", SplashActivity.professional_services_locations));
            group.Add(new Categories("Religious Organizations", SplashActivity.religion_locations));
            group.Add(new Categories("Restaurants, Coffee Shops", SplashActivity.restaurant_and_coffee_shop_locations));//conflicts with food & grocery/ not in db
            group.Add(new Categories("Shopping", SplashActivity.retail_locations));//probably
            group.Add(new Categories("Travel, Hotel, Motel", SplashActivity.travel_locations));*/
        }

        private string GetData(string search_specifics)
        {

            var request = HttpWebRequest.Create(String.Format(@"http://access4allspokane.org/RESTapi/" + table + "/?" + search_specifics));
            request.ContentType = "application/json";
            request.Method = "GET";

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var content = reader.ReadToEnd();
                    if (String.IsNullOrWhiteSpace(content))
                    {
                        Console.Out.WriteLine("Response contained empty body...");
                    }
                    else
                    {
                        //Console.Out.WriteLine("Response Body: \r\n {0}", content);
                        return content;
                    }
                }
            }
            return "NULL";
        }
    }

}
