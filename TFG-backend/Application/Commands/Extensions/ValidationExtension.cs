using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Extensions
{
    public static class ValidationExtension
    {
        public static void ValidateObject(this object objectToValidate, string exceptionMessage)
        {
            if (objectToValidate == null)
            {
                throw new Exception(exceptionMessage);
            }
        }

        public static void ValidateString(this string stringToValidate, string exceptionMessage)
        {
            if (stringToValidate == null || stringToValidate == "")
            {
                throw new Exception(exceptionMessage);
            }
        }
    }
}
