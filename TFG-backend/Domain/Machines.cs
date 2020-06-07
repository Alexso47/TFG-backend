using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Machines
    {
        public string Id { get; set; }

        public string EOID { get; set; }

        public string FID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Producer { get; set; }

        public DateTimeOffset ActiveFrom { get; set; }

        public string Serial { get; set; }
    }
}
