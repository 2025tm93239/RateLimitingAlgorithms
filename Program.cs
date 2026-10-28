using RateLimitingAlgorithms;

namespace RateLimitingAlgorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Implementedd TokenBucket and Leaky Bucket rate limiting algorithms in Java/Python/c#");
          
            Console.WriteLine("Token Bucket and Leaky Bucket Rate Limiting");
            
            Console.WriteLine("-------------------------------------------");

            TestTokenBucket();
            Console.WriteLine();
            TestLeakyBucket();

            Console.WriteLine();
            
            
            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }

        static void TestTokenBucket()
        {
            Console.WriteLine("\n--- Token Bucket ---");
            // bucket size 5, add 2 tokens every second
            TokenBucketRateLimiter limiter = new TokenBucketRateLimiter(5, 2);

            Console.WriteLine("Sending 8 requests at once:");
            for (int i = 1; i <= 8; i++)
            {
                bool ok = limiter.AllowRequest();
                if (ok)
                    Console.WriteLine("Request " + i + " : Allowed");
                else
                    Console.WriteLine("Request " + i + " : Blocked");
            }

            Console.WriteLine("Waiting 1 second...");
            Thread.Sleep(1000);

            for (int i = 9; i <= 11; i++)
            {
                bool ok = limiter.AllowRequest();
                if (ok)
                    Console.WriteLine("Request " + i + " : Allowed");
                else
                    Console.WriteLine("Request " + i + " : Blocked");
            }
        }

        static void TestLeakyBucket()
        {
            Console.WriteLine("\n--- Leaky Bucket ---");
            // max 3 in bucket, leaks 1 per second
            LeakyBucketRateLimiter limiter = new LeakyBucketRateLimiter(3, 1);

            Console.WriteLine("Sending 5 requests at once:");
            for (int i = 1; i <= 5; i++)
            {
                bool ok = limiter.AllowRequest();
                if (ok)
                    Console.WriteLine("Request " + i + " : Allowed");
                else
                    Console.WriteLine("Request " + i + " : Blocked");
            }

            Console.WriteLine("Waiting 2 seconds...");
            Thread.Sleep(2000);

            for (int i = 6; i <= 8; i++)
            {
                bool ok = limiter.AllowRequest();
                if (ok)
                    Console.WriteLine("Request " + i + " : Allowed");
                else
                    Console.WriteLine("Request " + i + " : Blocked");
            }
        }
    }
}
