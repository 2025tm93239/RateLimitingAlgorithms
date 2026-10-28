namespace RateLimitingAlgorithms;

public class LeakyBucketRateLimiter
{
    private int maxCapacity;
    private double leakRatePerSec;
    private double waterInBucket;
    private DateTime lastLeakTime;
    private object lockObj = new object();

    public LeakyBucketRateLimiter(int bucketCapacity, double leakRate)
    {
        maxCapacity = bucketCapacity;
        leakRatePerSec = leakRate;
        waterInBucket = 0;
        lastLeakTime = DateTime.Now;
    }

    public bool AllowRequest()
    {
        lock (lockObj)
        {
            LeakWater();

            // one request = 1 unit of water
            if (waterInBucket + 1 <= maxCapacity)
            {
                waterInBucket = waterInBucket + 1;
                return true;
            }

            return false;
        }
    }

    private void LeakWater()
    {
        DateTime now = DateTime.Now;
        double secondsPassed = (now - lastLeakTime).TotalSeconds;

        if (secondsPassed > 0)
        {
            waterInBucket = waterInBucket - (secondsPassed * leakRatePerSec);
            if (waterInBucket < 0)
            {
                waterInBucket = 0;
            }
            lastLeakTime = now;
        }
    }
}
