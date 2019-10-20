using System;
using System.Collections.Generic;

namespace ResilITApp.Model
{
    public class JSONSpeakerModel
    {
        public List<JSONSpeakerInfoModel> speakers { get; set; }
        public List<JSONSpeakerInfoModel> presenters { get; set; }
        public JSONSpeakerIdsModel speakerids { get; set; }
        public JSONSpeakerSettingModel settings { get; set; }
    }
}
