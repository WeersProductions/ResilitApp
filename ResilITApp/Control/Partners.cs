using System;
using System.Threading.Tasks;
using ResilITApp.Model;

namespace ResilITApp.Control
{
    public class Partners
    {
        public static async Task<string> GetPartnersJSON()
        {
            HttpMessage response = await Login.Instance.DoGet($"api/partners");
            if (!response.Success)
            {
                return null;
            }
            string json = await response.Response.Content.ReadAsStringAsync();
            return json;
        }
    }
}
