namespace ChildGrowth.API.Payload.Response.User;

public class SignInResponse
{
    public string AccessToken { get; set; }
    public string Role { get; set; }
    public int UserId { get; set; }
}