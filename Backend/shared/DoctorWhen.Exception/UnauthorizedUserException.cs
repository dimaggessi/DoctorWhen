using System.Runtime.Serialization;

namespace DoctorWhen.Exception;
public class UnauthorizedUserException : DoctorWhenException
{
    public UnauthorizedUserException(string message) : base(message)
    {
    }
    protected UnauthorizedUserException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}