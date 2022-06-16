using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.TodoAppNTier.Common.ResponseObjects
{
    public class Response<T> : Response , IResponse<T>
    {
        public T Data { get; set; }
        public Response(ResponseType response,T data) : base(response)
        {
            Data = data;
        }
        public Response(ResponseType response, string message) : base(response,message)
        {
           
        }
        public Response(ResponseType response, T data, List<CustomValidationErrors> customValidationErrors) : base(response)
        {
            ValidationErrors = customValidationErrors;
            Data = data;
        }
        public List<CustomValidationErrors> ValidationErrors { get; set; }
    }
}
