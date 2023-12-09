using System.Runtime.Serialization;

namespace DoctorWhen.Exception;
public class ValidatorErrorsException : DoctorWhenException
{
    public List<string> ErrorMessages { get; set; }

    public ValidatorErrorsException(List<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }

    protected ValidatorErrorsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}