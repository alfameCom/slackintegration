using Newtonsoft.Json;
using SlackIntegration.DTO.Slack;
using SlackIntegration.Services;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;

namespace SlackIntegration.Functions
{
    public abstract class FunctionBase
    {
        /// <summary>
        /// HttpResponseMessage ongelma serialisoinnin kanssa
        /// </summary>
        public class SimpleHttpResponse
        {
            public SimpleHttpResponse()
            {
            }

            public SimpleHttpResponse(HttpStatusCode statusCode)
            {
                StatusCode = statusCode;
            }

            public HttpStatusCode StatusCode { get; set; }
            public string ReasonPhrase { get; set; }
            public string Content { get; set; }
        }

        /// <summary>
        /// Luo oikea Http vastaus käyttämällä meidän omaa mallia
        /// </summary>
        /// <param name="myHttpResponse"></param>
        /// <returns></returns>
        public static HttpResponseMessage CreateHttpResponseMessage(SimpleHttpResponse myHttpResponse)
        {
            var response = new HttpResponseMessage
            {
                StatusCode = myHttpResponse.StatusCode
            };
            if (!string.IsNullOrEmpty(myHttpResponse.Content))
            {
                response.Content = new StringContent(
                    myHttpResponse.Content,
                    Encoding.UTF8,
                    "application/json");
            }
            return response;
        }

        /// <summary>
        /// Get slack user information
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static UserResponse GetUserInfo(string userId)
        {
            using (var httpClient = new HttpClient())
            {
                var accesToken = Configuration.SlackBotUserAccessToken;

                var url = $"https://slack.com/api/users.info?token={accesToken}&user={userId}&pretty=1";

                var response = httpClient.GetAsync(url).Result;

                var responseContent = response.Content.ReadAsStringAsync().Result;
                responseContent = HttpUtility.UrlDecode(responseContent);
                return JsonConvert.DeserializeObject<UserResponse>(responseContent);
            }
        }
    }
}