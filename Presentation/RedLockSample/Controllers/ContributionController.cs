using Microsoft.AspNetCore.Mvc;
using RedLockSample.Common;
using RedLockSample.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedLockSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContributionController : ControllerBase
    {
        private readonly IContributionService contributionService;
        private readonly ICacheService cacheService;

        public ContributionController(
            IContributionService contributionService,
            ICacheService cacheService)
        {
            this.contributionService = contributionService;
            this.cacheService = cacheService;

            //clean the cache
             cacheService.SetAsync(CacheKeyProvider.GetAddContributionKey,0).ConfigureAwait(false).GetAwaiter();

        }

        //It means The AddContribution will be called concurrently 100 times,
        //each time the value in the cache should be plused by 1
        //The final value in the Cache should be 50
        //The Distributed Lock Manager controlls the concurrency for the shared resource (the value in the cache)
        //This shared resource can be anything like ( a value in Database(like SQL Server) or an in memory value.
        [HttpGet("With_DLM")]
        public async Task<int> AddContributionsWithDLM()
        {
            List<Task> addContributionTasks = new List<Task>();

            
            for (int i = 1; i <= 50; i++)
            {
                addContributionTasks.Add(contributionService.AddContributionWithDLM(1));
            }
            await Task.WhenAll(addContributionTasks);

            return await cacheService.GetAsync<int>(CacheKeyProvider.GetAddContributionKey); 
        }

        //The concurrency have not controlled. At the end of execution of this action, the value in the cache in not equal to 50
        //because the race condition occurres between requests.
        [HttpGet("Without_DLM")]
        public async Task<int> AddContributionsWithoutDLM()
        {
            List<Task> addContributionTasks = new List<Task>();


            for (int i = 1; i <= 50; i++)
            {
                addContributionTasks.Add(contributionService.AddContributionWihtoutDLM(1));
            }
            await Task.WhenAll(addContributionTasks);


            return await cacheService.GetAsync<int>(CacheKeyProvider.GetAddContributionKey);
        }

    }
}
