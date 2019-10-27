using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ResilITApp.Model;

namespace ResilITApp.Control
{
    public class Enroll
    {
        private static async Task<JSONEnroll> DoRoll(int id, bool enroll)
        {
            HttpMessage response = await Login.Instance.DoPost(enroll ? $"api/talks/enroll/{id}" : $"api/talks/unenroll/{id}");
            if (!response.Success)
            {
                return new JSONEnroll() { message = response.Message, success = response.Success };
            }
            string json = await response.Response.Content.ReadAsStringAsync();
            JSONEnroll canEnroll = JsonConvert.DeserializeObject<JSONEnroll>(json);
            return canEnroll;
        }

        public static async Task<JSONEnroll> EnrollTalk(int id)
        {
            return await DoRoll(id, true);
        }

        public static async Task<JSONEnroll> UnEnrollTalk(int id)
        {
            return await DoRoll(id, false);
        }

        public static async Task<JSONEnrolled> EnrolledTalk(int id)
        {
            HttpMessage response = await Login.Instance.DoGet($"api/talks/enrolled/{id}");
            if (!response.Success)
            {
                return new JSONEnrolled() { success = response.Success };
            }
            string json = await response.Response.Content.ReadAsStringAsync();
            JSONEnrolled canEnroll = JsonConvert.DeserializeObject<JSONEnrolled>(json);
            return canEnroll;
        }

        public static async Task<JSONEnrollFavorites> EnrollFavorites()
        {
            HttpMessage response = await Login.Instance.DoPost($"api/talks/enroll_favorites");
            if (!response.Success)
            {
                return new JSONEnrollFavorites() { success = response.Success };
            }
            string json = await response.Response.Content.ReadAsStringAsync();
            JSONEnrollFavorites canEnroll = JsonConvert.DeserializeObject<JSONEnrollFavorites>(json);
            return canEnroll;
        }

        public static async Task<bool> CanEnroll()
        {
            HttpMessage response = await Login.Instance.DoGet($"api/talks/canenroll");
            if (!response.Success)
            {
                return false;
            }
            string json = await response.Response.Content.ReadAsStringAsync();
            JSONCanEnroll canEnroll = JsonConvert.DeserializeObject<JSONCanEnroll>(json);
            return canEnroll.canEnroll;
        }
    }
}
