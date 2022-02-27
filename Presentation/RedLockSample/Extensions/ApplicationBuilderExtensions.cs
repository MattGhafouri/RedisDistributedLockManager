using Microsoft.Extensions.Hosting;
using RedLockSample.Caching.Redis;

namespace RedLockSample.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        
        /// <summary>
        /// Dispose any exist connection to the redis 
        /// </summary>
        /// <param name="lifeTime"></param>
        public static void DisposeLockFactory(this IHostApplicationLifetime lifeTime)
        {
            lifeTime.ApplicationStopping.Register(() => {
                RedLockProvider.RedLockFactoryObject.Dispose();
            });
        }
        // add other extension here
    }
}
