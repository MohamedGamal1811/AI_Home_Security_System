namespace Server.Models.DTOs
{
    public record ServiceResponse(
        string Message=null!, bool Success = false);
}
