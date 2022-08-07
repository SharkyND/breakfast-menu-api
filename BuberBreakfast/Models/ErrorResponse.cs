namespace BuberBreakfast.Models;

class ErrorResponse
{
  public Boolean Success { get; set; }
  public string Message { get; set; }

  public ErrorResponse(bool success, string message)
  {
    Success = success;
    Message = message;
  }
}