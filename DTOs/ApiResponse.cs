using System.Text.Json.Serialization;

namespace NotificationServiceApi.DTOs;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public int StatusCode { get; set; } = 200;
    public string Message { get; set; } = "";

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Data { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? ElapsedMilliseconds { get; set; }

    public static ApiResponse<T> SuccessResponse(T data, string message = "Success", int statusCode = 200)
    {
        return new ApiResponse<T>
        {
            Success = true,
            StatusCode = statusCode,
            Message = message,
            Data = data
        };
    }

    public static ApiResponse<T> ErrorResponse(string message, int statusCode = 500)
    {
        return new ApiResponse<T>
        {
            Success = false,
            StatusCode = statusCode,
            Message = message
        };
    }
}
