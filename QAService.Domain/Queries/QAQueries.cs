using Dapper;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using QAService.Domain.AggregatesModel.RuleExecutionAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace QAService.Domain.Queries
{
    public class QAQueries : IQAQueries
    {
        private string _connectionString = string.Empty;

        public QAQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<IEnumerable<RuleExecution>> GetAllRuleExecutions()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"SELECT re.RuleExecutionId, re.ClientId, re.Accountid, re.[Event], re.[PatientID], re.[PatientVisitID], re.[PatientTransactionID]
	                , ree.RuleExecutionErrorId, ree.RuleErrorDescription, ree.RuleType, ree.RuleId, ree.RuleExecutionId
                    FROM RuleExecution re with (NOLOCK) 
	                INNER JOIN RuleExecutionError ree with (NOLOCK) ON re.RuleExecutionId = ree. RuleExecutionId";
                connection.Open();
                try
                {
                    var lookup = new Dictionary<Int64, RuleExecution>();
                    connection.Query<RuleExecution, RuleExecutionError, RuleExecution>(query, (re, ree) =>
                     {
                         RuleExecution ruleExecution;
                         if (!lookup.TryGetValue(ree.RuleExecutionId, out ruleExecution))
                         {
                             ruleExecution = re;
                             lookup.Add(re.RuleExecutionId, ruleExecution);
                         }

                         ruleExecution.RuleExecutionErrors.Add(ree);
                         return ruleExecution;
                     }, splitOn: "RuleExecutionErrorId");
                    return lookup.Values.ToList();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e);
                }
                return null;

            }
        }

        public async Task<IEnumerable<RuleExecution>> GetAllRuleExecutionsByClientId(Int64 clientId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"SELECT re.RuleExecutionId, re.ClientId, re.Accountid, re.[Event], re.[PatientID], re.[PatientVisitID], re.[PatientTransactionID]
	                , ree.RuleExecutionErrorId, ree.RuleErrorDescription, ree.RuleType, ree.RuleId, ree.RuleExecutionId
                    FROM RuleExecution re with (NOLOCK) 
	                INNER JOIN RuleExecutionError ree with (NOLOCK) ON re.RuleExecutionId = ree. RuleExecutionId
                    WHERE re.ClientId = @ClientId";
                connection.Open();
                try
                {
                    var result = connection.Query<RuleExecution, RuleExecutionError, RuleExecution>(query, (re, ree) =>
                    {
                        re.RuleExecutionErrors.Add(ree);
                        return re;
                    }, new { ClientId = clientId },
                    splitOn: "RuleExecutionErrorId").AsQueryable();
                    return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e);
                }
                return null;

            }
        }



        public async Task<IEnumerable<RuleExecution>> GetAllRuleExecutionsByPatientVisitId(Int64 patientVisitId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"SELECT re.RuleExecutionId, re.ClientId, re.Accountid, re.[Event], re.[PatientID], re.[PatientVisitID], re.[PatientTransactionID]
	                , ree.RuleExecutionErrorId, ree.RuleErrorDescription, ree.RuleType, ree.RuleId, ree.RuleExecutionId
                    FROM RuleExecution re with (NOLOCK) 
	                INNER JOIN RuleExecutionError ree with (NOLOCK) ON re.RuleExecutionId = ree. RuleExecutionId
                    WHERE re.PatientVisitId = @PatientVisitId";
                connection.Open();
                try
                {
                    var result = connection.Query<RuleExecution, RuleExecutionError, RuleExecution>(query, (re, ree) =>
                    {
                        re.RuleExecutionErrors.Add(ree);
                        return re;
                    }, new { PatientVisitId = patientVisitId },
                    splitOn: "RuleExecutionErrorId").AsQueryable();
                    return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e);
                }
                return null;

            }
        }
    }
}