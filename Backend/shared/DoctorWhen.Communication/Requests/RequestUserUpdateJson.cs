namespace DoctorWhen.Communication.Requests;
public class RequestUserUpdateJson
{
    public string Nome { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}
