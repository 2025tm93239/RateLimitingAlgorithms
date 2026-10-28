namespace RateLimitingAlgorithms;

/// <summary>
/// Non-blocking rate limiter: returns whether a request may proceed.
/// </summary>
public interface IRateLimiter
{
    /// <summary>
    /// Attempt to consume one unit of capacity. Thread-safe.
    /// </summary>
    bool TryAcquire();

    /// <summary>
    /// Attempt to consume the given number of units. Thread-safe.
    /// </summary>
    bool TryAcquire(int permits);
}
