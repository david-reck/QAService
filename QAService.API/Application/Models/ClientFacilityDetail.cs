using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QAService.API.Application.Models
{
    public class ClientFacilityDetail
    {
        public Int64 ClientId { get; set; }



        public string ClientName { get; set; }
        public Int64 FacilityId { get; set; }
        public string FacilityCode { get; set; }
    }
}