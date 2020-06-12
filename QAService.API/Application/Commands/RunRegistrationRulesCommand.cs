using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace QAService.Application.Commands
{
    [DataContract]
    public class RunRegistrationRulesCommand : IRequest<bool>
    {

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string MiddleName { get; set; }

        [DataMember]
        public DateTime BirthDate { get; set; }

        [DataMember]
        public string Gender { get; set; }

        [DataMember]
        public Int64 ClientId { get; set; }

        [DataMember]
        public Int64 PatientId { get; set; }

        [DataMember]
        public Int64 PatientVisitId { get; set; }

        [DataMember]
        public Int64 PatientTransactionId { get; set; }


        [DataMember]
        public List<PatientAddress> PatientAddresses { get; set; }

        [DataMember]
        public List<RuleError> RuleErrors { get; set; }


        public RunRegistrationRulesCommand()
        {

        }

        public RunRegistrationRulesCommand(string firstName, string lastName, DateTime birthDate, string gender)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Gender = gender;
        }
        public RunRegistrationRulesCommand(string firstName, string middleName, string lastName, DateTime birthDate, string gender, Int64 patientId,
            Int64 patientVisitId, Int64 patientTransactionId, Int64 clientId)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            BirthDate = birthDate;
            Gender = gender;
            PatientId = patientId;
            PatientVisitId = patientVisitId;
            ClientId = clientId;
            PatientTransactionId = patientTransactionId;
        }

    }

    public class PatientAddress
    {
        [DataMember]
        public string Street { get; set; }

        [DataMember]
        public string Street2 { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string State { get; set; }

        [DataMember]
        public string Zip { get; set; }

    }

    public class RuleError
    {
        [DataMember]
        public int RuleId { get; set; }

        [DataMember]
        public string RuleErrorDescription { get; set; }

        [DataMember]
        public string RuleType { get; set; }
    }
}

