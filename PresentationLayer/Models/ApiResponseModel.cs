namespace PresentationLayer.Models;

public record ApiResponseModel<T>(bool Success, string Message, T? Data);
