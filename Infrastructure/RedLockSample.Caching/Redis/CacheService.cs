using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RedLockNet;
using RedLockSample.Contract;
using RedLockSample.Service;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace RedLockSample.Caching.Redis
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase redisCache;
        private readonly IDistributedLockFactory distributedLockFactory;
        private readonly RedisConfiguration options;

        public CacheService(
            IDatabase database,
            IDistributedLockFactory distributedLockFactory,
            IOptions<RedisConfiguration> options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(RedisConfiguration));

            redisCache = database;
            this.distributedLockFactory = distributedLockFactory;
            this.options = options.Value;
        }



        public async Task<TEntity> GetAsync<TEntity>(string key)
        {
            var entity = await redisCache.StringGetAsync(key);

            if (entity.HasValue)
            {
                return Deserialize<TEntity>(entity);
            }
            else
            {
                return default;
            }
        }

        public async Task SetAsync<TEntity>(string key, TEntity value)
        {

            var stringEntity = Serialize(value);
            await redisCache.StringSetAsync(key, stringEntity);
        }


        public async Task<LockProcessResult> DoActionWithLockAsync(
         string lockKey,
         Func<Task> processor)
        {
            var processResult = new LockProcessResult();
            try
            {
                await using var redLock = await distributedLockFactory.CreateLockAsync
                      (lockKey, TimeSpan.FromSeconds(options.ExpiryTimeFromSeconds),
                      TimeSpan.FromSeconds(options.WaitTimeFromSeconds),
                      TimeSpan.FromMilliseconds(options.RetryTimeFromMilliseconds));

                if (redLock.IsAcquired)
                {
                    await processor();
                }
                else
                {
                    processResult.SetException(new Exception("The lock wasn't acquired"));
                }
            }
            catch (Exception ex)
            {
                processResult.SetException(ex);
            }

            return processResult;
        }


        public async Task<LockProcessResult<TInput>> DoActionWithLockAsync<TInput>(
           string lockKey,
           TInput parameter,
           Func<TInput, Task> processor)
        {
            var processResult = new LockProcessResult<TInput>();
            try
            {
                await using var redLock = await distributedLockFactory.CreateLockAsync
                      (lockKey, TimeSpan.FromSeconds(options.ExpiryTimeFromSeconds),
                      TimeSpan.FromSeconds(options.WaitTimeFromSeconds),
                      TimeSpan.FromMilliseconds(options.RetryTimeFromMilliseconds));

                if (redLock.IsAcquired)
                {
                    await processor(parameter);
                }
                else
                {
                    processResult.SetException(new Exception("The Lock was'nt aquired"));
                }
            }
            catch (Exception ex)
            {                
                processResult.SetException(ex);
            }

            return processResult;
        }


        public static string Serialize<T>(T obj) //where T : class
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T Deserialize<T>(string obj) //where T : class
        {
            return JsonConvert.DeserializeObject<T>(obj);
        }
    }
}
