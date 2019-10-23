using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace ResilITApp.Model
{
    public class PartnersViewModel
    {
        private Command<JSONPartnerModel> readMoreCommand;

        public Command<JSONPartnerModel> ReadMoreCommand
        {
            get { return readMoreCommand; }
            set { readMoreCommand = value; }
        }

        private ObservableCollection<JSONPartnerModel> partners;
        public ObservableCollection<JSONPartnerModel> Partners
        {
            get
            {
                return partners;
            }
            set
            {
                partners = value;
            }
        }

        private Dictionary<string, JSONPartnerModel> idPartners;

        public PartnersViewModel()
        {
            readMoreCommand = new Command<JSONPartnerModel>(GoToReadMore);
            Partners = new ObservableCollection<JSONPartnerModel>();
            _ = GetPartners();
        }

        private async void GoToReadMore(JSONPartnerModel partner)
        {
            if(!string.IsNullOrEmpty(partner.description))
            {
                await Launcher.OpenAsync(new Uri($"{Login.URL}partners/{partner.name}"));
            }
            else
            {
                await Launcher.OpenAsync(new Uri(partner.website));
            }
        }

        internal async Task GetPartners()
        {
            idPartners = new Dictionary<string, JSONPartnerModel>();
            string partnerJSON = await Control.Partners.GetPartnersJSON();
            JSONPartnersModel result = JsonConvert.DeserializeObject<JSONPartnersModel>(partnerJSON);
            foreach(JSONPartnerModel partner in result.partners)
            {
                idPartners.Add(partner.name, partner);
            }

            foreach(string partnerName in result.platinum)
            {
                Partners.Add(idPartners[partnerName]);
            }

            foreach(string partnerName in result.gold)
            {
                Partners.Add(idPartners[partnerName]);
            }

            foreach (string partnerName in result.silver)
            {
                Partners.Add(idPartners[partnerName]);
            }

            foreach (string partnerName in result.bronze)
            {
                Partners.Add(idPartners[partnerName]);
            }
        }
    }
}
