using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FHG.AccessControl
{
    public class CloseVisits
    {
        private readonly ILogger _logger;

        public CloseVisits(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CloseVisits>();
        }

        [Function("CloseVisits")]
        public void Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            
            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }
            //sample change
        }
    }
}
