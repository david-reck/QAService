using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QAService.Application.Commands;
using QAService.Application.Models;
using QAService.Domain.AggregatesModel.RuleExecutionAggregate;
using QAService.Domain.Queries;

namespace QAService.Controllers
{
    [Route("api/qa")]
    [ApiController]
    public class QAController : ControllerBase
    {
        private IMediator _mediatr;
        private readonly ILogger<QAController> _logger;
        private readonly IQAQueries _qaQueries;

        public QAController(IMediator mediatr, ILogger<QAController> logger, IQAQueries qaQueries)
        {
            _mediatr = mediatr ?? throw new ArgumentNullException(nameof(mediatr));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _qaQueries = qaQueries ?? throw new ArgumentNullException(nameof(qaQueries));
        }

        // GET: api/QA
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RuleExecution>>> Get()
        {
            var result = await _qaQueries.GetAllRuleExecutions();
            return new JsonResult(result);
        }

        // GET: api/QA/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<IEnumerable<RuleExecution>>> GetRuleExecutionByClientId(long id)
        {

            var result = await _qaQueries.GetAllRuleExecutionsByClientId(id);
            return new JsonResult(result);
        }

        [Route("PatientVisitId/{patientvisitid:long}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RuleExecution>>> GetRuleExecutionByPatientVisitId(long patientvisitid)
        {
            var result = await _qaQueries.GetAllRuleExecutionsByPatientVisitId(patientvisitid);
            return new JsonResult(result);
        }

        public string Get(int id)
        {
            return "value";
        }

        // POST: api/QA
        [HttpPost]
        public async Task<ActionResult<bool>> RecordQAResults([FromBody] QAExecution dto)
        {
            bool commandResult = false;

            var command = new RecordQAResultsCommand(dto.ClientId, dto.AccountId, dto.Event, dto.RuleErrors);

            _logger.LogInformation("-----Sending command: RegistrationCommand");

            commandResult = await _mediatr.Send(command);

            if (!commandResult)
            {
                return BadRequest();
            }

            return Ok();


        }


        [Route("RunRegistrationRules")]
        [HttpPost]
        public async Task<ActionResult<bool>> RunRegistrationRules([FromBody] PatientDTO registrationDTO)
        {
            bool commandResult = false;

            var command = new RunRegistrationRulesCommand(registrationDTO.FirstName, registrationDTO.LastName, registrationDTO.BirthDate, registrationDTO.Gender);

            _logger.LogInformation("-----Sending command: RunRegistrationRulesCommand");

            commandResult = await _mediatr.Send(command);

            if (!commandResult)
            {
                return BadRequest();
            }

            return Ok();


        }

        // PUT: api/QA/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
