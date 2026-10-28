# SEZG504 Assignment - Question 3

Name: (your name)  
Roll No: (your roll number)

## Problem

Implement Token Bucket and Leaky Bucket rate limiting algorithms in C#.

## Files

- `TokenBucketRateLimiter.cs` - Token bucket logic
- `LeakyBucketRateLimiter.cs` - Leaky bucket logic
- `Program.cs` - small program to test both algorithms

## How to run (Visual Studio)

1. Open `RateLimitingAlgorithms.sln` in Visual Studio 2022
2. Press F5 to run
3. Check console output for Allowed / Blocked requests

## How to run (command line)

```
dotnet run --project RateLimitingAlgorithms.csproj
```

Need .NET 8 SDK installed.

## Brief explanation

**Token Bucket:** We start with full tokens. Each allowed request uses 1 token. Tokens are added back slowly (refill rate). If no tokens left, request is blocked.

**Leaky Bucket:** Each request adds water to the bucket. Water leaks out at a fixed rate. If bucket is full, new request is blocked.
