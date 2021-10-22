using Newtonsoft.Json;
using System;
using System.Net;

namespace HepsiExpress.StudyCase.Api.Application.Common.Models
{
    public class ResponseException
    {
        public string Message { get; set; }
        public string ErrorType { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        internal static ResponseException Create(Exception ex)
        {
            return new ResponseException
            {
                Message = ex.Message,
                ErrorType = ex.GetType().Name
            };
        }
    }
}