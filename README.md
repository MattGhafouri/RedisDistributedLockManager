# RedisDistributedLockManager

Distributed locks are a very useful primitive in many environments where different processes must operate with shared resources in a mutually exclusive way.

This implementation use dot net C# and [Relock.Net](https://github.com/samcook/RedLock.net) to provide a more canonical algorithm to implement distributed locks with Redis which is called Redlock.

## Used Technologies
dotnet 6.0

Redis cache


## Resources
-Read full artcile about the detail [Here](https://m-qafouri.medium.com/distributed-locks-manager-c-and-redis-fd3d86cd1250)

- You can read my [Article](https://medium.com/@m-qafouri/serialize-access-to-a-shared-resource-in-distributed-systems-with-dlm-distributed-lock-manager-5abf5e393e15) about the concept of Distributed Lock manager to control exclusive access to a shared resource.


## Other Resources
- You can find more about Redis DLM on their official [website](https://redis.io/topics/distlock)

