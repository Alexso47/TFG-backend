using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.dto.Dto.EconomicOperator
{
    public class EORequest
    {
        public RequestHeader RequestHeader { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string ZipCode { get; set; }

        public DateTimeOffset ActiveFrom { get; set; }
    }
}
