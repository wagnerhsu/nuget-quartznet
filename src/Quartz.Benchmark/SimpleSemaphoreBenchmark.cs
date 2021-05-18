using System;
using System.Threading;
using System.Threading.Tasks;

using BenchmarkDotNet.Attributes;

using Quartz.Impl.AdoJobStore;

namespace Quartz.Benchmark
{
    [MemoryDiagnoser]
    public class SimpleSemaphoreBenchmark
    {
        private SimpleSemaphore semaphore;
        private Guid requestorId;

        [GlobalSetup]
        public void Setup()
        {
            semaphore = new SimpleSemaphore();
            requestorId = Guid.NewGuid();
        }

        [Benchmark]
        public async Task ObtainAndRelease()
        {
            await semaphore.ObtainLock(requestorId, null, JobStoreSupport.LockTriggerAccess, CancellationToken.None);
            await semaphore.ReleaseLock(requestorId, JobStoreSupport.LockTriggerAccess, CancellationToken.None);
        }
    }
}