namespace RateLimitingAlgorithms;

public sealed class LeakyBucketRateLimiter : IRateLimiter
{
    private readonly int _capacity;

    
    private readonly double _leakRatePerSecond;
    private double _waterLevel;
    private long _lastLeakTicks;

    private readonly object _gate = new();

    public LeakyBucketRateLimiter(int capacity, double leakRatePerSecond)
    {
        if (capacity <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be positive.");
        if (leakRatePerSecond <= 0)
            throw new ArgumentOutOfRangeException(nameof(leakRatePerSecond), "Leak rate must be positive.");

        _capacity = capacity;
        _leakRatePerSecond = leakRatePerSecond;
        _waterLevel = 0;
        _lastLeakTicks = Environment.TickCount64;

    }

    public int Capacity => _capacity;

    public double LeakRatePerSecond => _leakRatePerSecond;

    public bool TryAcquire() => TryAcquire(1);

    public bool TryAcquire(int permits)
    {
        if (permits <= 0)
            throw new ArgumentOutOfRangeException(nameof(permits), "Permits must be positive.");

        lock (_gate)
        {
            Leak();
            if (_waterLevel + permits > _capacity)
                return false;

            _waterLevel += permits;
            return true;
        }
    }

    private void Leak()
    {
        var now = Environment.TickCount64;
        var elapsedMs = now - _lastLeakTicks;
        if (elapsedMs <= 0)
            return;

        var leaked = elapsedMs / 1000.0 * _leakRatePerSecond;
        _waterLevel = Math.Max(0, _waterLevel - leaked);
        _lastLeakTicks = now;
    }
}

