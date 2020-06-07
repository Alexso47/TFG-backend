using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Product
    {
        [Key]
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }
    }
}
