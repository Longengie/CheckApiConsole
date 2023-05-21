using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpHelperConsole.Extensions.Interface
{
    public interface IHttpResponseExtension
    {
        public T? GetJsonRespond<T>();
        public Task<T?>? GetJsonRespondAsync<T>();
        public T? GetFormRespond<T>();
        public Task<T>? GetFormRespondAsync<T>();
    }
}
