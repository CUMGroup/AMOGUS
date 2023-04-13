
using System.Runtime.Serialization;

namespace AMOGUS.Core.Common.Exceptions {
    public class UserOperationException : Exception {
        public UserOperationException() {
        }

        public UserOperationException(string? message) : base(message) {
        }

        public UserOperationException(string? message, Exception? innerException) : base(message, innerException) {
        }

        protected UserOperationException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}
