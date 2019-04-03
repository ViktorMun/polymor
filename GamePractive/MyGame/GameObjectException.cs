using System;
using System.Runtime.Serialization;

namespace MyGame
{
    [Serializable]
    internal class GameObjectException : Exception
    {
        public GameObjectException()
        {
        }

        public GameObjectException(string message) : base(message)
        {
            Console.WriteLine(message);
        }

        public GameObjectException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GameObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}