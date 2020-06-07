using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class RequestHeader
    {
        /// <summary>
        /// Gets or sets the date request. Format YYYYMMDD
        /// </summary>
        /// <value>
        /// The date request.
        /// </value>
        public string DateRequest { get; set; }

        /// <summary>
        /// Gets or sets the time request. Format HHMMSS
        /// </summary>
        /// <value>
        /// The time request.
        /// </value>
        public string TimeRequest { get; set; }

        /// <summary>
        /// Gets or sets the request identifier.
        /// </summary>
        /// <value>
        /// The request identifier.
        /// </value>
        public string RequestId { get; set; }
    }
}
