using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using UserManagementApp.IRepositories;
using UserManagementApp.Models;

namespace UserManagementApp.Repositories
{
    internal class UserRepository : IUserRepository
    {
        const string URL = "YOUR-FIREBASE-PROJECT-URL/user-list";
        HttpClient client = new HttpClient();
        public static Dictionary<string, User> users = new Dictionary<string, User>();
        public async Task<bool> LoadUsers()
        {
            var endpoint = new Uri(URL + ".json");
            var response = await client.GetAsync(endpoint);
            if ((int)response.StatusCode >= 400)
            {
                return false;
            }
            string result = await response.Content.ReadAsStringAsync();
            users = JsonConvert.DeserializeObject<Dictionary<string, User>>(result)!;
            if (users == null)
            {
                users = new Dictionary<string, User>();
            }
            return true;
        }

        public async Task AddUser(User user)
        {
            var endpoint = new Uri(URL + ".json");
            string json = JsonConvert.SerializeObject(user);
            var payload = new StringContent(json, Encoding.UTF8, "application/json");
            string result = await client.PostAsync(endpoint, payload).Result.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(result);
            string name = (string)jsonObject["name"]!;
            users.Add(name, new User { FirstName = user.FirstName, LastName = user.LastName, Gender = user.Gender });
            Console.WriteLine("New user added successfully!");
        }

        public void AllUsers()
        {
            int count = 1;
            if (users.Count > 0)
            {
                foreach (var item in users)
                {
                    string gender = item.Value.Gender == 0 ? "female" : "male";
                    Console.WriteLine($"{count++}.{item.Value.FirstName} {item.Value.LastName} {gender}");
                }
            }
            else
                Console.WriteLine("Nothing found!");
        }

        public async Task UpdateUser(int index, User user)
        {
            var endpoint = new Uri(URL + $"/{users.ElementAt(index).Key}.json");
            string json = JsonConvert.SerializeObject(user);
            var payload = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PutAsync(endpoint, payload);
            users[users.ElementAt(index).Key] = user;
            Console.WriteLine("The user updated successfully!");
        }

        public async Task DeleteUser(int index)
        {
            var endpoint = new Uri(URL + $"/{users.ElementAt(index).Key}.json");
            await client.DeleteAsync(endpoint);
            users.Remove(users.ElementAt(index).Key);
            Console.WriteLine("The user deleted successfully!");
        }

    }
}
