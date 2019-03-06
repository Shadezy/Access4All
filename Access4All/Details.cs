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
    class Details
    {


        private string infoTitle;
        private List<string> info;

        public Details(string intoTitle, List<string> info)
        {
            this.InfoTitle = InfoTitle;
            this.Info = info;
        }
        
        public string InfoTitle { get => infoTitle; set => infoTitle = value; }
        public List<string> Info { get => info; set => info = value; }
    }
}



/*
        private string locationName;
        private string locationAddress;
        private string phoneNumber;
        private string webURL;
        private string parkingInfo;
        private string transitInfo;
        private string pathInfo;
        private string entranceInfo;
        private string interiorInfo;
        private string seatingInfo;
        private string restroomInfo;
        private string communicationInfo;
        private string techInfo;

        public Details()
        {
            this.LocationName = "";
            this.locationAddress = "";
            this.phoneNumber = "";
            this.webURL = "";
            this.parkingInfo = "";
            this.transitInfo = "";
            this.pathInfo = "";
            this.entranceInfo = "";
            this.interiorInfo = "";
            this.seatingInfo = "";
            this.restroomInfo = "";
            this.communicationInfo = "";
            this.techInfo = "";
        }

        public Details(string locationName, string locationAddress, string phoneNumber, string webURL, string parkingInfo, string transitInfo, string pathInfo, string entranceInfo, string interiorInfo, string seatingInfo, string restroomInfo, string communicationInfo, string techInfo)
        {
            this.LocationName = locationName;
            this.locationAddress = locationAddress;
            this.phoneNumber = phoneNumber;
            this.webURL = webURL;
            this.parkingInfo = parkingInfo;
            this.transitInfo = transitInfo;
            this.pathInfo = pathInfo;
            this.entranceInfo = entranceInfo;
            this.interiorInfo = interiorInfo;
            this.seatingInfo = seatingInfo;
            this.restroomInfo = restroomInfo;
            this.communicationInfo = communicationInfo;
            this.techInfo = techInfo;
        }

        public string LocationName { get => locationName; set => locationName = value; }
        public string LocationAddress { get => locationAddress; set => locationAddress = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string WebURL { get => webURL; set => webURL = value; }
        public string ParkingInfo { get => parkingInfo; set => parkingInfo = value; }
        public string TransitInfo { get => transitInfo; set => transitInfo = value; }
        public string PathInfo { get => pathInfo; set => pathInfo = value; }
        public string EntranceInfo { get => entranceInfo; set => entranceInfo = value; }
        public string InteriorInfo { get => interiorInfo; set => interiorInfo = value; }
        public string SeatingInfo { get => seatingInfo; set => seatingInfo = value; }
        public string RestroomInfo { get => restroomInfo; set => restroomInfo = value; }
        public string CommunicationInfo { get => communicationInfo; set => communicationInfo = value; }
        public string TechInfo { get => techInfo; set => techInfo = value; }
*/