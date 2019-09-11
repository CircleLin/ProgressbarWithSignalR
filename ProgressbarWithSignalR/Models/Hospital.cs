using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgressbarWithSignalR.Models
{
    public class Hospital
    {
        public int _id { get; set; }
        public string title { get; set; }
        public string address_for_display { get; set; }
        public string telephone { get; set; }
    }
}