using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.Arrival
{
    public class ArrivalResult
    {
        public int Id { get; set; }

        public string FID { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string ZipCode { get; set; }

        public DateTimeOffset ArrivalDate { get; set; }

        public string Comments { get; set; }

        public List<Serials> Serials { get; set; }

        public string SerialsJson { get; set; }
    }
}
