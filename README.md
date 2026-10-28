# SEZG504 Assignment — Question 3

Token Bucket and Leaky Bucket rate limiting in C#.

## Files

- `TokenBucketRateLimiter.cs`
- `LeakyBucketRateLimiter.cs`
- `Program.cs` — console demo (Allowed / Blocked)
- `Screenshots and Demo video/` — assignment captures

## How to run

**Visual Studio:** open `RateLimitingAlgorithms.sln` → F5.

**CLI** (requires .NET 8 SDK):

```powershell
cd D:\Bits-pilani\API\RateLimitingAlgorithms
dotnet run --project RateLimitingAlgorithms.csproj
```

## Brief explanation

**Token Bucket:** Starts with a full bucket of tokens. Each allowed request uses one token; tokens refill at a fixed rate. No tokens → request blocked.

**Leaky Bucket:** Requests fill the bucket; output “leaks” at a fixed rate. Bucket full → new request blocked.

## Demo video

Local file: [`Screenshots and Demo video/Question 3 Short Demo Recording.mp4`](./Screenshots%20and%20Demo%20video/Question%203%20Short%20Demo%20Recording.mp4)

## Screenshots

Folder: [`Screenshots and Demo video/`](./Screenshots%20and%20Demo%20video/)

| | |
|:---:|:---:|
| ![Picture 26](./Screenshots%20and%20Demo%20video/Picture26.png) | ![Picture 27](./Screenshots%20and%20Demo%20video/Picture27.png) |
| ![Picture 28](./Screenshots%20and%20Demo%20video/Picture28.png) | ![Picture 29](./Screenshots%20and%20Demo%20video/Picture29.png) |
| ![Picture 30](./Screenshots%20and%20Demo%20video/Picture30.png) | ![Picture 31](./Screenshots%20and%20Demo%20video/Picture31.png) |

![Picture 32](./Screenshots%20and%20Demo%20video/Picture32.png)
