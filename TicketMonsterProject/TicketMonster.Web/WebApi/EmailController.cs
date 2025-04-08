using Microsoft.AspNetCore.Mvc;
using TicketMonster.ApplicationCore.Extensions;

namespace TicketMonster.Web.WebApi;

[Route("api/[controller]/[action]")]
[ApiController]
public class EmailController : ControllerBase
{
    private readonly EmailSender _emailSender;
    //private readonly EmailTest _emailTest;

    public EmailController(EmailSender emailSender) => _emailSender = emailSender;
    //public EmailController(EmailTest emailTest) => _emailTest = emailTest;

    [HttpPost]
    public async Task<IActionResult> SendEmail(SendEmailRequest request)
    {
        if (request == null) { return BadRequest("Invalid request data."); }
        await _emailSender.SendEmail(request.RecipientName, request.RecipientEmail, request.Subject, request.Body);
        return Ok();
    }

    #region EmailTest
    //[Consumes("application/json")]
    //[HttpPost]
    //public IActionResult EmailTest([FromBody] SendEmailRequest request)
    //{
    //    try
    //    {
    //        if (request == null) { return BadRequest("Invalid request data."); }
    //        _emailTest.SendEmailTest(request.RecipientEmail, request.Subject, request.Body);
    //        return Ok("Email sent successfully.");
    //    }
    //    catch (Exception ex) { return StatusCode(500, $"Internal server error: {ex.Message}"); }
    //}
    #endregion

    public class SendEmailRequest
    {
        public string RecipientEmail { get; set; }
        public string RecipientName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
