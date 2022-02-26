using RedLockSample.Contract;
using System.Threading.Tasks;

namespace RedLockSample.Service
{
    public interface IContributionService
    {
        Task AddContributionWihtoutDLM(int value);
        Task AddContributionWithDLM(int value);
    }
}