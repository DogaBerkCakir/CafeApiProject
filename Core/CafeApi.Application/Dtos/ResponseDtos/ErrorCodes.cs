using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeApi.Application.Dtos.ResponseDtos
{
    public static class ErrorCodes
    {
        public const string NotFound = "NOT_FOUND";
        public const string Unauthorized = "UNAUTORIZED";
        public const string Exception = "EXCEPTION";
        public const string ValidationError = "VALIDATION_ERROR";
        public const string BadRequest = "BAD_REQUEST";
        public const string Forbidden = "FORBIDDEN";
        public const string Duplicate = "DUPLICATE"; //ctrl+shift+f kullanarak aratabilirsin
    }
}
