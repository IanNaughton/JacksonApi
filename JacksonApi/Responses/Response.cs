using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JacksonApi.Responses
{
    public class Response<T> 
    {
        public T Payload { get; set; }
        public string ErrorMessage { get; set; }
        public bool Successful { get; set; }
    }
}