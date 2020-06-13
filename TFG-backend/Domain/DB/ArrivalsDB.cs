using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DB
{
    public class ArrivalsDB
    {
        public int Id { get; set; }

        public string FID { get; set; }

        public DateTimeOffset ArrivalDate { get; set; }

        public string Comments { get; set; }
    }
}
