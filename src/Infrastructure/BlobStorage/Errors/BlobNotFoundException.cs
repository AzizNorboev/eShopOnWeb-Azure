using System;
using System.Runtime.Serialization;

namespace Infrastructure.BlobStorage.Errors;

[Serializable]
public class BlobNotFoundException : Exception {

    public BlobNotFoundException() {
    }
    protected BlobNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) {
    }
    public BlobNotFoundException(string message) : base(message) {
    }
    public BlobNotFoundException(string message, Exception innerException) : base(message, innerException) {
    }
}