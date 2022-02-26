# RedisDistributedLockManager

Distributed locks are a very useful primitive in many environments where different processes must operate with shared resources in a mutually exclusive way.

This implementation use dot net C# and [Relock.Net](https://github.com/samcook/RedLock.net) to provide a more canonical algorithm to implement distributed locks with Redis which is called Redlock.

You can find more about Redis DLm on their official [website](https://redis.io/topics/distlock)

You can also read my [Article](https://medium.com/@m-qafouri/serialize-access-to-a-shared-resource-in-distributed-systems-with-dlm-distributed-lock-manager-5abf5e393e15) about the concept of Distributed Lock manager to control exclusive access to a shared resource.
