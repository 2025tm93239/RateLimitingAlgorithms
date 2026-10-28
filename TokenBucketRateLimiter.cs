namespace RateLimitingAlgorithms;

public class TokenBucketRateLimiter
{
    private int maxTokens;
    private double tokensPerSecond;
    private double currentTokens;
    private DateTime lastRefillTime;
    private object lockObj = new object();

    public TokenBucketRateLimiter(int bucketSize, double refillRate)
    {
        maxTokens = bucketSize;
        tokensPerSecond = refillRate;
        currentTokens = bucketSize;
        lastRefillTime = DateTime.Now;
    }

    public bool AllowRequest()
    {
        lock (lockObj)
        {
            RefillTokens();

            if (currentTokens >= 1)
            {
                currentTokens = currentTokens - 1;
                return true;
            }

            return false;
        }
    }

    private void RefillTokens()
    {
        DateTime now = DateTime.Now;
        double secondsPassed = (now - lastRefillTime).TotalSeconds;

        if (secondsPassed > 0)
        {
            currentTokens = currentTokens + (secondsPassed * tokensPerSecond);
            if (currentTokens > maxTokens)
            {
                currentTokens = maxTokens;
            }
            lastRefillTime = now;
        }
    }
}
