using System;
using System.Collections.Generic;
using System.Text;

namespace API.dto.Dto
{
    public class ResponseResult
    {
        /// <summary>
        /// Gets or sets the request identifier.
        /// </summary>
        /// <value>
        /// The request identifier.
        /// </value>
        public string RequestId { get; private set; }

        /// <summary>
        /// Gets or sets the result.
        /// - 0. The are errors (check errors list)
        /// - 1. Complete succesfully
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public byte Result { get; set; }

        /// <summary>
        /// Gets or sets the errors.
        /// </summary>
        /// <value>
        /// The errors.
        /// </value>
        public List<ErrorDetail> Errors { get; set; }

        /// <summary>
        /// Gets or sets the process time (miliseconds).
        /// </summary>
        /// <value>
        /// The process time.
        /// </value>
        public int ProcessTime { get; set; }

        public string ConfirmationCode { get; set; }

        /// <summary>
        /// 0-InternalError
        /// 1-ExternalError
        /// 2-ExternalWarning
        /// </summary>
        public int ErrorType { get; set; }

        public ResponseResult(string requestId)
        {
            Errors = null;
            Result = 1;
            ProcessTime = 0;
            RequestId = requestId;
        }
    }
}
