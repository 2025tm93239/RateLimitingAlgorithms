# Question 3 — Token Bucket & Leaky Bucket (C#)

## Build and run

```bash
cd RateLimitingAlgorithms
dotnet build RateLimitingAlgorithms.sln
dotnet run --project RateLimitingAlgorithms.csproj
```

Open `RateLimitingAlgorithms.sln` (or `RateLimitingAlgorithms.slnx`) in Visual Studio.

Requires [.NET 8 SDK](https://dotnet.microsoft.com/download) or newer.

## Files

| File | Purpose |
|------|---------|
| `TokenBucketRateLimiter.cs` | Token bucket algorithm |
| `LeakyBucketRateLimiter.cs` | Leaky bucket algorithm |
| `IRateLimiter.cs` | Common `TryAcquire` contract |
| `Program.cs` | Demo showing allow/reject behavior |

## Algorithms (short)

**Token bucket:** The bucket holds up to `capacity` tokens. Tokens are added continuously at `refillRatePerSecond`. Each accepted request consumes one token. You can burst until tokens run out, then you are limited to the refill rate.

**Leaky bucket:** The bucket holds up to `capacity` units of “water.” Requests add water; water leaks at `leakRatePerSecond`. If a request would overflow the bucket, it is rejected. Traffic is smoothed toward the leak rate.

Both implementations are thread-safe (`lock`) and use `TryAcquire()` for non-blocking checks (suitable in API middleware).
