using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Requests
    {
        public int Id { get; set; }

        public string RequestName { get; set; }
        
        public string EOID { get; set; }

        public string FID { get; set; }
       
        public string MID { get; set; }

        public string ProductName { get; set; }

        public string ProductType { get; set; }

        public string Description { get; set; }

        public string Market { get; set; }
        
        public string Route { get; set; }
        
        public int Quantity { get; set; }
        
        public string ManufacturingLine { get; set; }
        
        public int Comments { get; set; }
    }
}
