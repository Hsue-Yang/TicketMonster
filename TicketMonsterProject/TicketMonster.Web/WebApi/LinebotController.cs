using Microsoft.AspNetCore.Mvc;
using TicketMonster.Web.Services;

namespace TicketMonster.Web.WebApi;

public class LinebotController : isRock.LineBot.LineWebHookControllerBase
{
    private readonly string _apiKey;
    private readonly string _adminUserId;
    private readonly string _channelAccessToken;

    public LinebotController(IConfiguration configuration)
    {
        _apiKey = configuration.GetValue<string>("LinebotSettings:ApiKey");
        _adminUserId = configuration.GetValue<string>("LinebotSettings:AdminUserId");
        _channelAccessToken = configuration.GetValue<string>("LinebotSettings:ChannelAccessToken");
    }

    [Route("api/LinebotWebhook")]
    [HttpPost]
    public IActionResult POST()
    {
        try
        {
            this.ChannelAccessToken = _channelAccessToken;
            // Line Verify
            if (ReceivedMessage.events == null || ReceivedMessage.events.Count <= 0 ||
                ReceivedMessage.events.FirstOrDefault().replyToken == string.Join(Environment.NewLine, Enumerable.Repeat("0", 32))) return Ok();
            // get LineEvent
            var LineEvent = this.ReceivedMessage.events.FirstOrDefault();
            var responseMsg = "test";
            // responseMsg
            if (LineEvent?.type.ToLower() == "message" && LineEvent?.message.type.ToLower() == "text")
            {
                if (LineEvent.message.text.Contains("/reset", StringComparison.OrdinalIgnoreCase))
                {
                    this.ReplyMessage(LineEvent?.replyToken, "I lost my memory!");
                    new isRock.LineBot.Bot(ChannelAccessToken).PushMessage(LineEvent?.source.userId, 1070, 17870);
                    return Ok();
                }
                else
                {
                    responseMsg = Linebot.GetResponseFromLinebot(LineEvent.message.text, _apiKey);
                }
            }
            else if (LineEvent?.type.ToLower() == "message")
            {
                new isRock.LineBot.Bot(ChannelAccessToken).PushMessage(LineEvent?.source.userId, 6632, new Random().Next(11825374, 11825398));
                return Ok();
            }
            else responseMsg = $"{LineEvent?.type}";

            this.ReplyMessage(LineEvent?.replyToken, responseMsg);
            return Ok();
        }
        catch (Exception ex)
        {
            this.PushMessage(_adminUserId, "An error occurred:\n" + ex.Message);   
            return Ok();
        }
    }
}