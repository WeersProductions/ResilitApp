using System;
using System.Collections.Generic;

namespace ResilITApp
{
    public class JSONTimetable
    {
        public string date { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public int timeInterval { get; set; }
        public List<JSONTrack> tracks { get; set; }
    }
}
