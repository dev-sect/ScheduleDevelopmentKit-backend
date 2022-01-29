using System;

namespace ScheduleDevelopmentKit.Common.Exceptions
{
    public class RoomAlreadyAddedException : Exception
    {
        public RoomAlreadyAddedException()
        {
        }

        public RoomAlreadyAddedException(string message)
            : base(message)
        {
        }

        public RoomAlreadyAddedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}