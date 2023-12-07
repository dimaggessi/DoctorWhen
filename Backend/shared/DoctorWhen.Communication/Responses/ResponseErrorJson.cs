namespace DoctorWhen.Communication.Responses;
public class ResponseErrorJson
{
    public List<string> ErrorMessages { get; set; }

    public ResponseErrorJson(string errorMessage)
    {
        ErrorMessages.Add(errorMessage);
    }

    public ResponseErrorJson(List<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }
}
