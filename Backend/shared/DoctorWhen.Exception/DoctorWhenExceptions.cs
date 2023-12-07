using System.Runtime.Serialization;

namespace DoctorWhen.Exception;
public class DoctorWhenExceptions : SystemException
{
    public DoctorWhenExceptions()
    {
    }

    public DoctorWhenExceptions(string message) : base(message)
    {
    }

    protected DoctorWhenExceptions(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
