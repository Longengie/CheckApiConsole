using GetApiHealthConsole.Model;
using RestSharpHelperConsole.Extensions;
using RestSharpHelperConsole.Extensions.Interface;
using RestSharpHelperConsole.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetApiHealthConsole.Process
{
    internal class GetRequest
    {
        private readonly Stack<string> _listError;
        private readonly Stack<ResultModel> _listSuccess;
        private readonly Stack<RestSharpConfigurationModel> _urlList;

        public GetRequest(Stack<RestSharpConfigurationModel> urlList)
        {
            _listError = new();
            _listSuccess = new();
            _urlList = urlList;
        }
        internal void GetRequestProccess()
        {
            GetReuestFromList();
            WriteSuccess();
            EndProccess();
        }
        private void GetReuestFromList()
        {
            foreach (var item in _urlList)
            {
                try
                {
                    IHttpResponseExtension httpResponse = new HttpResponseExtension(item);
                    var result = httpResponse.GetJsonRespond<object>();
                    if (result == null) continue;
                    _listSuccess.Push(new()
                    {
                        Url = item.HostUrl,
                        RequestType = item.RequestType,
                        Result = result
                    });
                }
                catch (Exception ex) { _listError.Push(ex.Message); }
            }
        }
        private void WriteSuccess()
        {
            Console.WriteLine("Result");
            foreach(ResultModel success in _listSuccess)
            {
                success.WriteConsole();
            }
        }
        private void EndProccess()
        {
            if (_listError.Count > 0)
                EndProgram.StopProgramErrors("Get Request Proccess", _listError.ToArray());
            EndProgram.StopProgram();
        }
    }

}
