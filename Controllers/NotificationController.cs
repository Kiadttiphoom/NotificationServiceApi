using Microsoft.AspNetCore.Mvc;
using NotificationServiceApi.DTOs;
using NotificationServiceApi.Models;

namespace NotificationServiceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly ILogger<NotificationController> _logger;
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    public NotificationController(
        ILogger<NotificationController> logger, 
        IConfiguration configuration,
        IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendNotification([FromBody] NotificationRequest request)
    {
        var token = _configuration.GetValue<string>("NotificationSettings:TelegramToken");
        
        if (string.IsNullOrEmpty(token))
        {
            _logger.LogError("Telegram Token is not configured.");
            return StatusCode(500, ApiResponse<object>.ErrorResponse("Notification service is not properly configured."));
        }

        try
        {
            var client = _httpClientFactory.CreateClient();
            var url = $"https://api.telegram.org/bot{token}/sendMessage";
            
            var payload = new
            {
                chat_id = request.ChatId,
                text = request.Message
            };

            var responseMessage = await client.PostAsJsonAsync(url, payload);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorContent = await responseMessage.Content.ReadAsStringAsync();
                _logger.LogError("Failed to send Telegram message. Status: {Status}, Error: {Error}", 
                    responseMessage.StatusCode, errorContent);
                return BadRequest(ApiResponse<object>.ErrorResponse("Failed to send notification to Telegram."));
            }

            _logger.LogInformation("✅ Notification sent to Telegram. ChatId: {ChatId}", request.ChatId);

            var response = ApiResponse<object>.SuccessResponse(
                new { 
                    sentAt = DateTime.UtcNow,
                    chatId = request.ChatId
                }, 
                message: "Notification sent successfully"
            );

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while sending notification");
            return StatusCode(500, ApiResponse<object>.ErrorResponse("An unexpected error occurred."));
        }
    }
}
