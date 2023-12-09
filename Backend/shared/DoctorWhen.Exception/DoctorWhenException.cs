using System.Runtime.Serialization;

namespace DoctorWhen.Exception;
public class DoctorWhenException : SystemException
{
    public DoctorWhenException()
    {
    }

    public DoctorWhenException(string message) : base(message)
    {
    }

    protected DoctorWhenException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
