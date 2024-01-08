using System.Runtime.Serialization;

namespace DoctorWhen.Exception;

[Serializable]
public class InvalidSearchException : DoctorWhenException
{
    public InvalidSearchException(string message) : base(message)
    {
    }

    protected InvalidSearchException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}