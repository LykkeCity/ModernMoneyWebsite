using Core.Domain.Health;
using Core.Services;
using System.Collections.Generic;

namespace Lykke.ModernMoney.Services
{
    public class HealthService : IHealthService
    {
        public string GetHealthViolationMessage()
        {
            return null;
        }

        public IEnumerable<HealthIssue> GetHealthIssues()
        {
            var issues = new HealthIssuesCollection();
            return issues;
        }
    }
}
