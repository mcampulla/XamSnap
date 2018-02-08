using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamSnap.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace XamSnap.Azure
{
    public class AzureWebService : IWebService
    {
        private const string BaseUrl = "https://mcampulla-xamsnap.azurewebsites.net/api/";
        private const string ContentType = "application/json";
        private readonly HttpClient httpClient = new HttpClient();

        private async Task<HttpResponseMessage> Post(string url, string code, object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            var content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue(ContentType);
            var response = await httpClient.PostAsync(BaseUrl + url + "?code=" + code, content);
            response.EnsureSuccessStatusCode();
            return response;
        }

        private async Task<T> Post<T>(string url, string code, object obj)
        {
            var response = await Post(url, code, obj);
            string json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task<User> Login(string userName, string password)
        {
            await Post("login", "QM386zKzTaevd81MtAM31EBMMn6vX0P9vCpzwXybkekR/7feFU2Faw==", new
            {
                userName,
                password,
            });
            return new User
            {
                Username = userName,
                Password = password,
            };
        }

        public Task<User> Register(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User[]> GetFriends(string userName)
        {
            return Post<User[]>("friends", "c/Sn6hDQlQkvXkPRuWNzcgv87viSzZRjcpLkYXvza9wc5lbyuyOHKQ==", new
            {
                userName
            });
        }
        public async Task<User> AddFriend(
        string userName, string friendName)
        {
            await Post("addfriend", "84u0GvwFBpxO7x1zRI61vKyZJN7gxfW3j4eVsNg/QHwDnh2D3oSvEA==", new
            {
                userName,
                friendName
            });
            return new User
            {
                Username = friendName
            };
        }

        public Task<Conversation[]> GetConversations(string userName)
        {
            return Post<Conversation[]>("conversations", "yefjRFKJovEH4VexWS/bk/29/9JE9/ppKURaCPJ8DMP7VSwjTAR/vA==", new
            {
                userName
            });
        }

        public Task<SnapMessage[]> GetMessages(string conversation)
        {
            return Post<SnapMessage[]>("messages", "6g64MSgkKXUdUygV1HzRE0dnPnJE8wkTYyw3wd4hFQTvATwp3wI0Ug==", new
            {
                conversation
            });
        }

        public async Task<SnapMessage> SendMessage(SnapMessage message)
        {
            message.Id = Guid.NewGuid().ToString("N");
            await Post("sendmessage", "vxUico0Fygj2l8TW3Oa6MFr/e5oYxQx1TZ3cEhwkVhubZmYC3DF48g==", message);
            return message;
        }
    }
}

