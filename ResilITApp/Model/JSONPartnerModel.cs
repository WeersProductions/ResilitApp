using System;
using Xamarin.Forms;

namespace ResilITApp.Model
{
    public class JSONPartnerModel
    {
        public string name { get; set; }
        public string website { get; set; }
        public string image { get; set; }
        public string description { get; set; }

        public ImageSource fullImage {
            get
            {
                Uri baseUri = new Uri(Login.URL);
                var myUri = new Uri(baseUri, image);
                return myUri;
            }
        }
    }
}
