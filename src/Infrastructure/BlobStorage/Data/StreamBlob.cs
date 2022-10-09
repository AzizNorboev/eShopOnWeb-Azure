using System;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructure.BlobStorage.Data;
    public class StreamBlob : IAsyncDisposable, IDisposable {
        public StreamBlob(Stream stream, string contentType) {
            ContentType = contentType;
            Stream = stream;
        }

        public Stream Stream { get; }
        public string ContentType { get; }

        protected virtual void Dispose(bool disposing) {
            if (disposing) {
                Stream.Dispose();
            }
        }

        protected virtual async ValueTask DisposeAsyncCore() {
            await Stream.DisposeAsync();
        }

        public async ValueTask DisposeAsync() {
            await DisposeAsyncCore().ConfigureAwait(false);
            Dispose(false);

            GC.SuppressFinalize(this);
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
