namespace NotificationServiceApi.Models;

public class NotificationRequest
{
    public string ChatId { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
