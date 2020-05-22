using QAService.Domain.AggregatesModel.RuleExecutionAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QAService.Domain.Queries
{
    public interface IQAQueries
    {
        Task<IEnumerable<RuleExecution>> GetAllRuleExecutions();
        Task<IEnumerable<RuleExecution>> GetAllRuleExecutionsByClientId(Int64 clientId);

        Task<IEnumerable<RuleExecution>> GetAllRuleExecutionsByPatientVisitId(Int64 PatientVisitId);
    }
}
