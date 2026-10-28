namespace RateLimitingAlgorithms;

public sealed class TokenBucketRateLimiter : IRateLimiter
{
    private readonly int _capacity;


    private readonly double _refillRatePerSecond;
    private double _availableTokens;
    private long _lastRefillTicks;
    private readonly object _gate = new();

    public TokenBucketRateLimiter(int capacity, double refillRatePerSecond)
    {
        if (capacity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be positive.");
        }


        if (refillRatePerSecond <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(refillRatePerSecond), "Refill rate must be positive.");
        }

        _capacity = capacity;
        _refillRatePerSecond = refillRatePerSecond;
        _availableTokens = capacity;
        _lastRefillTicks = Environment.TickCount64;
    }

    public int Capacity => _capacity;

    public double RefillRatePerSecond => _refillRatePerSecond;

    public bool TryAcquire() => TryAcquire(1);

    public bool TryAcquire(int permits)
    {
        if (permits <= 0)
            throw new ArgumentOutOfRangeException(nameof(permits), "Permits must be positive.");

        lock (_gate)
        {
            Refill();
            if (_availableTokens < permits)
                return false;

            _availableTokens -= permits;
            return true;
        }
    }

    private void Refill()
    {
        var now = Environment.TickCount64;
        var elapsedMs = now - _lastRefillTicks;
        if (elapsedMs <= 0)
            return;

        var added = elapsedMs / 1000.0 * _refillRatePerSecond;
        _availableTokens = Math.Min(_capacity, _availableTokens + added);
        _lastRefillTicks = now;
    }
}
