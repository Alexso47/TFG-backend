using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Serials
    {
        public int Id { get; set; }

        public string Serial { get; set; }

        public List<ArrivalSerials> ArrivalSerials { get; set; }
        public List<DispatchSerials> DispatchSerials { get; set; }
        public List<InvoiceSerials> InvoiceSerials { get; set; }
        public List<RequestSerials> RequestSerials { get; set; }
    }
}
