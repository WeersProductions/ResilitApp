using System;
using System.Collections.Generic;

namespace ResilITApp
{
    public class JSONTrack
    {
        public string trackId { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public List<JSONTalk> talks { get; set; }
    }
}
