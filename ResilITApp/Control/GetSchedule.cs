using System;
using System.Threading.Tasks;
using ResilITApp.Model;

namespace ResilITApp
{
    public static class GetSchedule
    {
        public static async Task<string> GetScheduleJSON()
        {
            HttpMessage response = await Login.Instance.DoGet($"api/timetable");
            if(!response.Success)
            {
                return null;
            }
            string json = await response.Response.Content.ReadAsStringAsync();
            return json;
        }
    }
}
