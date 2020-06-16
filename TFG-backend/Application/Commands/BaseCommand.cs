using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands
{
    public class BaseCommand
    {
        public BaseCommand(string requestObjectName)
        {
            RequestObjectName = requestObjectName;
        }

        public string RequestId { get; set; }

        public string RequestSerialized { get; set; }

        public string RequestObjectName { get; set; }

    }
}
