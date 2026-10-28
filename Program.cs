using RateLimitingAlgorithms;

Console.WriteLine("=== Question 3: Token Bucket & Leaky Bucket (C#) ===\n");

DemoTokenBucket();
Console.WriteLine();
DemoLeakyBucket();

static void DemoTokenBucket()
{
    // Capacity 5, refill 2 tokens/sec → burst of 5, then ~2 req/sec
    var limiter = new TokenBucketRateLimiter(capacity: 5, refillRatePerSecond: 2);

    Console.WriteLine("Token bucket (capacity=5, refill=2/sec)");
    Console.WriteLine("Sending 8 requests immediately:");

    for (var i = 1; i <= 8; i++)
    {
        var allowed = limiter.TryAcquire();
        Console.WriteLine($"  Request {i}: {(allowed ? "ALLOWED" : "REJECTED")}");
    }

    Console.WriteLine("Waiting 1 second for refill...");
    Thread.Sleep(1000);

    for (var i = 9; i <= 11; i++)
    {
        var allowed = limiter.TryAcquire();
        Console.WriteLine($"  Request {i}: {(allowed ? "ALLOWED" : "REJECTED")}");
    }
}

static void DemoLeakyBucket()
{
    // Capacity 3, leak 1 req/sec → smooth at 1/sec, small queue
    var limiter = new LeakyBucketRateLimiter(capacity: 3, leakRatePerSecond: 1);

    Console.WriteLine("Leaky bucket (capacity=3, leak=1/sec)");
    Console.WriteLine("Sending 5 requests immediately:");

    for (var i = 1; i <= 5; i++)
    {
        var allowed = limiter.TryAcquire();
        Console.WriteLine($"  Request {i}: {(allowed ? "ALLOWED" : "REJECTED")}");
    }

    Console.WriteLine("Waiting 2 seconds for leak...");
    Thread.Sleep(2000);

    for (var i = 6; i <= 8; i++)
    {
        var allowed = limiter.TryAcquire();
        Console.WriteLine($"  Request {i}: {(allowed ? "ALLOWED" : "REJECTED")}");
    }
}
