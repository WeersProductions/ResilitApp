using System;
using System.Net.Http;
using System.Threading.Tasks;
using ResilITApp.Model;

namespace ResilITApp.Control
{
    public static class Favorites
    {
        public async static Task<bool> AddFavorite(Talk talk)
        {
            HttpMessage response = await Login.Instance.DoPost($"api/favorite/add/{talk.Id}");
            return response.Success;
        }

        public async static Task<bool> RemoveFavorite(Talk talk)
        {
            HttpMessage response = await Login.Instance.DoPost($"api/favorite/remove/{talk.Id}");
            return response.Success;
        }

        public async static Task<int[]> GetFavorites()
        {
            UserModel user = await Login.Instance.GiveUser(true);
            return user != null ? user.favorites : new int[] { };
        }

        public async static Task<bool> IsFavorite(Talk talk)
        {
            int[] favorites = await GetFavorites();
            for(int i = 0; i < favorites.Length; i++)
            {
                if(favorites[i]==talk.Id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
