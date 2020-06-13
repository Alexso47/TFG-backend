using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Arrival
{
    public class ArrivalRequestDTO
    {
        public string FID { get; set; }

        public DateTimeOffset ArrivalDate { get; set; }

        public string Comments { get; set; }

        public List<string> Serials { get; set; }
    }
}
