using System.Runtime.Serialization;

namespace AMOGUS.Core.Common.Exceptions {
    public class AuthFailureException : Exception {
        public AuthFailureException() {
        }

        public AuthFailureException(string? message) : base(message) {
        }

        public AuthFailureException(string? message, Exception? innerException) : base(message, innerException) {
        }

        protected AuthFailureException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}
