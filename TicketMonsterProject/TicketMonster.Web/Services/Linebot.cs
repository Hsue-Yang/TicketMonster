using System.Text;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using static TicketMonster.Web.Services.Linebot;

namespace TicketMonster.Web.Services;

public class Linebot
{
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum Role { assistant, user, system }

    public static string CallOpenAIChatAPI(object requestData, string apiKey)
    {
        var client = new HttpClient();
        var apiUrl = $"https://api.openai.com/v1/chat/completions";
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var jsonRequestData = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);
        // Create HTTP request content
        var content = new StringContent(jsonRequestData, Encoding.UTF8, "application/json");
        // Send HTTP POST request
        var response = client.PostAsync(apiUrl, content).Result;
        // Get HTTP response content
        var responseContent = response.Content.ReadAsStringAsync().Result;
        var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseContent);
        return obj.choices[0].message.content.Value;
    }

    public static string GetResponseFromLinebot(string Message, string apiKey)
    {
        var messages = new List<ChatMessage>
        {
            new ChatMessage
            {
                role = Role.system,
                content = @"你是個叫TicketMonster票券網站的霸道客服，對於客戶非常沒禮貌、不過能夠安撫客戶的抱怨情緒，還會講笑話。
                                請檢視底下的客戶訊息，以在地台灣國語的方式回應。
                                但回應時，請注意以下幾點:
                                * 不能過度承諾,量力而為
                                * 記住你是線上即時客服,但不要暴露你的AI身份,不要把自己當語言模型
                                * 要能夠盡量解決客戶的問題,但僅限ticket範圍內之業務,不在其位不謀其事
                                * 當客戶給出 '/website' 回覆 'https://ticketmonsterfront.azurewebsites.net/' (只回覆網址其它廢話不要說)
                                ----------------------"
            },
            new ChatMessage() { role = Role.user, content = Message }
        };
        return Linebot.CallOpenAIChatAPI(new { model = "gpt-3.5-turbo", messages }, apiKey);
    }
}

public class ChatMessage
{
    public Role role { get; set; }
    public string content { get; set; }
}