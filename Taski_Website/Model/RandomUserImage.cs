using System.Text.Json;

namespace Taski_Website.Model
{
    public static  class RandomUserImage
    {

        public static async Task<string> GetRandomUserPictureAsync()
        {
            string url = "https://randomuser.me/api/?results=1&lego";
            HttpClient client = new HttpClient();
            JsonElement root = await client.GetFromJsonAsync<JsonElement>(url);

            var jsonUser = root.GetProperty("results")[0];
            string userPictureUrl = jsonUser.GetProperty("picture").GetProperty("large").GetString();

            return userPictureUrl;
        }
    }
}
