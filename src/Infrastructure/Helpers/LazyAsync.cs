using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Helpers;
public class LazyAsync<T> {
    private readonly SemaphoreSlim? _semaphore;
    private T? _value;
    private volatile object? _isInitializedState;
    private readonly Func<CancellationToken, Task<T>> _constructor;
    private readonly LazyThreadSafetyMode _threadSafetyMode;
    public LazyAsync(Func<CancellationToken, Task<T>> constructor, LazyThreadSafetyMode threadSafetyMode = LazyThreadSafetyMode.ExecutionAndPublication) {
        _constructor = constructor ?? throw new ArgumentNullException(nameof(constructor));
        _threadSafetyMode = threadSafetyMode;
        if (threadSafetyMode != LazyThreadSafetyMode.None) {
            _semaphore = new SemaphoreSlim(1, 1);
        }
    }

    public async Task<T> GetValue(CancellationToken cancellationToken = default) {
        if (_isInitializedState != null) {
            return _value!;
        }

        async Task<T> CreateValue() {
            return await _constructor(cancellationToken);
        }

        if (_threadSafetyMode == LazyThreadSafetyMode.None || _semaphore == null) {
            _value = await CreateValue();
            _isInitializedState = new object();
            return _value;
        }

        await _semaphore.WaitAsync(cancellationToken);
        try {
            if (_isInitializedState != null) {
                return _value!;
            }

            _value = await CreateValue();
            _isInitializedState = new object();
            return _value;
        } finally {
            _semaphore.Release();
        }
    }
}