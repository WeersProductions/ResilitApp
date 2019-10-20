using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ResilITApp.Model
{
    public class JSONSpeakerInfoModel
    {
        public string name { get; set; }
        public string id { get; set; }
        public string image { get; set; }
        public string subject { get; set; }
        public string company { get; set; }
        public List<string> talk { get; set; }
        private List<string> _bio;
        public List<string> bio
        {
            get
            {
                return _bio;
            }
            set
            {
                _bio = value;
                for (int i = 0; i < _bio.Count; i++)
                {
                    // Remove html syntax.
                    _bio[i] = Regex.Replace(_bio[i], "<.*?>", string.Empty);
                    
                    fullDescription += _bio[i];
                    if (i < _bio.Count - 1)
                    {
                        fullDescription += System.Environment.NewLine;
                    }
                }
                if(_bio.Count > 0)
                {
                    string[] sentences = Regex.Split(_bio[0], @"(?<!\w\.\w.)(?<![A-Z][a-z][a-z][a-z]\.)(?<![A-Z][a-z]\.)(?<=\.|\?)\s");
                    description = sentences[0];
                }
                else
                {
                    description = "";
                }
            }
        }
        public bool presenter { get; set; }
        public string description { get; set; }
        public string fullDescription { get; set; }
    }
}
