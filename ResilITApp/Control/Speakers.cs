using System;
using System.Threading.Tasks;
using ResilITApp.Model;

namespace ResilITApp.Control
{
    public class Speakers
    {
        public static async Task<string> GetSpeakersJSON()
        {
            HttpMessage response = await Login.Instance.DoGet($"api/speakers");
            if (!response.Success)
            {
                return null;
            }
            string json = await response.Response.Content.ReadAsStringAsync();
            return json;
        }
    }
}
