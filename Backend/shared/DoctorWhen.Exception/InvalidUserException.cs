using System.Runtime.Serialization;

namespace DoctorWhen.Exception;

[Serializable]
public class InvalidUserException : DoctorWhenException
{
    public InvalidUserException(string message) : base(message)
    {
    }

    protected InvalidUserException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}