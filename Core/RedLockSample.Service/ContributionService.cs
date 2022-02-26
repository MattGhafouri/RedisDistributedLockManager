using RedLockSample.Common;
using RedLockSample.Contract;
using RedLockSample.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedLockSample.ApplicationService
{
    public class ContributionService : IContributionService
    {
        private readonly ICacheService cacheService;

        public ContributionService(ICacheService cacheService)
        {
            this.cacheService = cacheService;
        }

        
        public async Task AddContributionWihtoutDLM(int value)
        {
            await AddContributionToCache(value);
        }
        public async Task AddContributionWithDLM(int value)
        {
            var result = await cacheService.DoActionWithLockAsync<int>(
                LockKeyProvider.ContributionLockKey,
                value,
                async (arg) => await AddContributionToCache(value));

            if (!result.IsSuccessfullyProcessed)
            {
                var exception = result.Exception;
                //persist or somehow process the failed item
            }
        }

        private async Task AddContributionToCache(int value)
        {
            var cacheKey = CacheKeyProvider.GetAddContributionKey;

            var currentValue = await cacheService.GetAsync<int>(cacheKey);

            var newValue = currentValue + value;
            await cacheService.SetAsync(cacheKey, newValue);

        }
    }
}
