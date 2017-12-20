using System.Collections.Generic;
using Core.Domain.Health;

namespace Core.Services
{
    public interface IHealthService
    {
        string GetHealthViolationMessage();
        IEnumerable<HealthIssue> GetHealthIssues();
    }
}
