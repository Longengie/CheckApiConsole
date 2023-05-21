using RestSharp;
using RestSharpHelperConsole.Extensions.Interface;
using RestSharpHelperConsole.Model;

namespace RestSharpHelperConsole.Extensions
{
    public class HttpResponseExtension : IHttpResponseExtension
    {
        private readonly RestSharpConfigurationModel _configuration;
        public HttpResponseExtension(RestSharpConfigurationModel configuration)
        {
            _configuration = configuration;
        }
        #region Common
        /// <summary>
        /// Create Rest Request
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="query">Query</param>
        /// <param name="token">Token</param>
        /// <param name="tokenScheme">Token Scheame</param>
        /// <returns></returns>
        private static RestRequest CreateRestRequest(string url, List<QueryParamModel>? query, string? token = null, string? tokenScheme = null)
        {
            RestRequest request = new(url);
            #region Add Header
            if (!string.IsNullOrEmpty(token))
            {
                request = request.AddHeader("Authorization", $"{tokenScheme ?? "Bearer"} {token}");
            }
            request = request.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            #endregion
            #region Add Query Param
            if (query != null)
            {
                foreach (var item in query)
                {
                    if (!string.IsNullOrEmpty(item.Key))
                        request = request.AddQueryParameter(item.Key ?? string.Empty, item.Value);
                }
            }
            #endregion
            return request;
        }
        /// <summary>
        /// Create Json Rest Request
        /// </summary>
        /// <returns>Rest Request</returns>
        private RestRequest CreateJsonRestRequest()
        {
            RestRequest request = CreateRestRequest(
                _configuration.HostUrl,
                _configuration.QueryParam,
                _configuration.TokenSession,
                _configuration.TokenShceme);

            if (_configuration.BodyForm != null) request.AddJsonBody(_configuration.BodyForm);
            return request;
        }
        /// <summary>
        /// Create Form Rest Request
        /// </summary>
        /// <returns>Rest Request</returns>
        private RestRequest CreateFormRestRequest()
        {
            var request = CreateRestRequest(
                _configuration.HostUrl,
                _configuration.QueryParam,
                _configuration.TokenSession,
                _configuration.TokenShceme);

            // Check Body Params
            var bodypams = _configuration.BodyForm;
            if (bodypams == null) return request;

            // Add normaL Param
            if (bodypams.BodyForms != null)
            {
                foreach (var param in bodypams.BodyForms)
                {
                    request.AddParameter(Parameter.CreateParameter(param.Key, param.Value, ParameterType.GetOrPost));
                }
            }
            return request;
        }
        #endregion
        #region Get
        #region Response
        private T? CreateGetResponse<T>(RestRequest request)
        {
            RestClient RESTClient = new(_configuration.HostUrl ?? string.Empty);
            var response = RESTClient.Get<T>(request);

            return response;
        }
        /// <summary>
        /// Create Get Response Async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<T?> CreateGetResponseAsync<T>(RestRequest request)
        {
            RestClient RESTClient = new(_configuration.HostUrl ?? string.Empty);
            var response = await RESTClient.GetAsync<T>(request);

            return response;
        }
        #endregion
        #region Sync
        /// <summary>
        /// Get Json Respond
        /// </summary>
        /// <typeparam name="T">Respond Data Type</typeparam>
        /// <returns>Respond</returns>
        public T? GetJsonResponse<T>()
        {
            RestRequest request = CreateJsonRestRequest();
            T? response = CreateGetResponse<T>(request);
            return response;
        }
        /// <summary>
        /// Get Form Response
        /// </summary>
        /// <typeparam name="T">Respond Data Type</typeparam>
        /// <returns>Respond</returns>
        public T? GetFormResponse<T>()
        {
            var request = CreateFormRestRequest();
            var response = CreateGetResponse<T>(request);
            return response;
        }
        #endregion
        #region Async
        /// <summary>
        /// Get Json Respond Async
        /// </summary>
        /// <typeparam name="T">Respond Data Type</typeparam>
        /// <returns>Respond</returns>
        public async Task<T?> GetJsonResponseAsync<T>()
        {
            RestRequest request = CreateJsonRestRequest();
            T? response = await CreateGetResponseAsync<T>(request);
            return response;
        }
        /// <summary>
        /// Get Form Response Async
        /// </summary>
        /// <typeparam name="T">Respond Data Type</typeparam>
        /// <returns>Respond</returns>
        public async Task<T?> GetFormResponseAsync<T>()
        {
            var request = CreateFormRestRequest();
            var response = await CreateGetResponseAsync<T>(request);
            return response;
        }
        #endregion
        #endregion
        #region Post
        #region Response
        /// <summary>
        /// Create Post Response
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        private T? CreatePostResponse<T>(RestRequest request)
        {
            RestClient RESTClient = new(_configuration.HostUrl ?? string.Empty);
            var response = RESTClient.Post<T>(request);

            return response;
        }
        /// <summary>
        /// Create Post Response Async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<T?> CreatePostResponseAsync<T>(RestRequest request)
        {
            RestClient RESTClient = new(_configuration.HostUrl ?? string.Empty);
            var response = await RESTClient.PostAsync<T>(request);

            return response;
        }
        #endregion
        #region Sync
        /// <summary>
        /// Post Json Respond
        /// </summary>
        /// <typeparam name="T">Respond Data Type</typeparam>
        /// <returns>Respond</returns>
        public T? PostJsonResponse<T>()
        {
            RestRequest request = CreateJsonRestRequest();
            T? response = CreatePostResponse<T>(request);
            return response;
        }
        /// <summary>
        /// Post Form Response
        /// </summary>
        /// <typeparam name="T">Respond Data Type</typeparam>
        /// <returns>Respond</returns>
        public T? PostFormResponse<T>()
        {
            var request = CreateFormRestRequest();
            var response = CreatePostResponse<T>(request);
            return response;
        }
        #endregion
        #region Async
        /// <summary>
        /// Post Json Respond Async
        /// </summary>
        /// <typeparam name="T">Respond Data Type</typeparam>
        /// <returns>Respond</returns>
        public async Task<T?> PostJsonResponseAsync<T>()
        {
            RestRequest request = CreateJsonRestRequest();
            T? response = await CreatePostResponseAsync<T>(request);
            return response;
        }
        /// <summary>
        /// Post Form Response Async
        /// </summary>
        /// <typeparam name="T">Respond Data Type</typeparam>
        /// <returns>Respond</returns>
        public async Task<T?> PostFormResponseAsync<T>()
        {
            var request = CreateFormRestRequest();
            var response = await CreatePostResponseAsync<T>(request);
            return response;
        }
        #endregion
        #endregion
        #region Put
        #region Response
        /// <summary>
        /// Create Put Response
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        private T? CreatePutResponse<T>(RestRequest request)
        {
            RestClient RESTClient = new(_configuration.HostUrl ?? string.Empty);
            var response = RESTClient.Put<T>(request);

            return response;
        }
        /// <summary>
        /// Create Put Response Async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<T?> CreatePutResponseAsync<T>(RestRequest request)
        {
            RestClient RESTClient = new(_configuration.HostUrl ?? string.Empty);
            var response = await RESTClient.PutAsync<T>(request);

            return response;
        }
        #endregion
        #region Sync
        /// <summary>
        /// Put Json Respond
        /// </summary>
        /// <typeparam name="T">Respond Data Type</typeparam>
        /// <returns>Respond</returns>
        public T? PutJsonResponse<T>()
        {
            RestRequest request = CreateJsonRestRequest();
            T? response = CreatePutResponse<T>(request);
            return response;
        }
        /// <summary>
        /// Put Form Response
        /// </summary>
        /// <typeparam name="T">Respond Data Type</typeparam>
        /// <returns>Respond</returns>
        public T? PutFormResponse<T>()
        {
            var request = CreateFormRestRequest();
            var response = CreatePutResponse<T>(request);
            return response;
        }
        #endregion
        #region Async
        /// <summary>
        /// Put Json Respond Async
        /// </summary>
        /// <typeparam name="T">Respond Data Type</typeparam>
        /// <returns>Respond</returns>
        public async Task<T?> PutJsonResponseAsync<T>()
        {
            RestRequest request = CreateJsonRestRequest();
            T? response = await CreatePutResponseAsync<T>(request);
            return response;
        }
        /// <summary>
        /// Put Form Response Async
        /// </summary>
        /// <typeparam name="T">Respond Data Type</typeparam>
        /// <returns>Respond</returns>
        public async Task<T?> PutFormResponseAsync<T>()
        {
            var request = CreateFormRestRequest();
            var response = await CreatePutResponseAsync<T>(request);
            return response;
        }
        #endregion
        #endregion
        #region Patch
        #region Response
        /// <summary>
        /// Create Patch Response
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        private T? CreatePatchResponse<T>(RestRequest request)
        {
            RestClient RESTClient = new(_configuration.HostUrl ?? string.Empty);
            var response = RESTClient.Patch<T>(request);

            return response;
        }
        /// <summary>
        /// Create Patch Response Async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<T?> CreatePatchResponseAsync<T>(RestRequest request)
        {
            RestClient RESTClient = new(_configuration.HostUrl ?? string.Empty);
            var response = await RESTClient.PatchAsync<T>(request);

            return response;
        }
        #endregion
        #region Sync
        /// <summary>
        /// Patch Json Respond
        /// </summary>
        /// <typeparam name="T">Respond Data Type</typeparam>
        /// <returns>Respond</returns>
        public T? PatchJsonResponse<T>()
        {
            RestRequest request = CreateJsonRestRequest();
            T? response = CreatePatchResponse<T>(request);
            return response;
        }
        /// <summary>
        /// Patch Form Response
        /// </summary>
        /// <typeparam name="T">Respond Data Type</typeparam>
        /// <returns>Respond</returns>
        public T? PatchFormResponse<T>()
        {
            var request = CreateFormRestRequest();
            var response = CreatePatchResponse<T>(request);
            return response;
        }
        #endregion
        #region Async
        /// <summary>
        /// Patch Json Respond Async
        /// </summary>
        /// <typeparam name="T">Respond Data Type</typeparam>
        /// <returns>Respond</returns>
        public async Task<T?> PatchJsonResponseAsync<T>()
        {
            RestRequest request = CreateJsonRestRequest();
            T? response = await CreatePatchResponseAsync<T>(request);
            return response;
        }
        /// <summary>
        /// Put Form Response Async
        /// </summary>
        /// <typeparam name="T">Respond Data Type</typeparam>
        /// <returns>Respond</returns>
        public async Task<T?> PatchFormResponseAsync<T>()
        {
            var request = CreateFormRestRequest();
            var response = await CreatePatchResponseAsync<T>(request);
            return response;
        }
        #endregion
        #endregion
        #region Delete
        #region Response
        /// <summary>
        /// Create Delete Response
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        private T? CreateDeleteResponse<T>(RestRequest request)
        {
            RestClient RESTClient = new(_configuration.HostUrl ?? string.Empty);
            var response = RESTClient.Delete<T>(request);

            return response;
        }
        /// <summary>
        /// Create Delete Response Async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<T?> CreateDeleteResponseAsync<T>(RestRequest request)
        {
            RestClient RESTClient = new(_configuration.HostUrl ?? string.Empty);
            var response = await RESTClient.DeleteAsync<T>(request);

            return response;
        }
        #endregion
        #region Sync
        /// <summary>
        /// Delete Json Respond
        /// </summary>
        /// <typeparam name="T">Respond Data Type</typeparam>
        /// <returns>Respond</returns>
        public T? DeleteJsonResponse<T>()
        {
            RestRequest request = CreateJsonRestRequest();
            T? response = CreateDeleteResponse<T>(request);
            return response;
        }
        /// <summary>
        /// Delete Form Response
        /// </summary>
        /// <typeparam name="T">Respond Data Type</typeparam>
        /// <returns>Respond</returns>
        public T? DeleteFormResponse<T>()
        {
            var request = CreateFormRestRequest();
            var response = CreateDeleteResponse<T>(request);
            return response;
        }
        #endregion
        #region Async
        /// <summary>
        /// Delete Json Respond Async
        /// </summary>
        /// <typeparam name="T">Respond Data Type</typeparam>
        /// <returns>Respond</returns>
        public async Task<T?> DeleteJsonResponseAsync<T>()
        {
            RestRequest request = CreateJsonRestRequest();
            T? response = await CreateDeleteResponseAsync<T>(request);
            return response;
        }
        /// <summary>
        /// Delete Form Response Async
        /// </summary>
        /// <typeparam name="T">Respond Data Type</typeparam>
        /// <returns>Respond</returns>
        public async Task<T?> DeleteFormResponseAsync<T>()
        {
            var request = CreateFormRestRequest();
            var response = await CreateDeleteResponseAsync<T>(request);
            return response;
        }
        #endregion
        #endregion
        #region Get Respond
        public T? GetJsonRespond<T>()
        {
            return _configuration.RequestType switch
            {
                HttpType.Get => GetJsonResponse<T>(),
                HttpType.Post => PostJsonResponse<T>(),
                HttpType.Patch => PatchJsonResponse<T>(),
                HttpType.Put => PutJsonResponse<T>(),
                HttpType.Delete => DeleteJsonResponse<T>(),
                _ => throw new Exception(message: $"Missing of {nameof(_configuration.RequestType)}")
            };
        }
        public async Task<T?>? GetJsonRespondAsync<T>()
        {
            var respond = _configuration.RequestType switch
            {
                HttpType.Get => await GetJsonResponseAsync<T>(),
                HttpType.Post => await PostJsonResponseAsync<T>(),
                HttpType.Patch => await PatchJsonResponseAsync<T>(),
                HttpType.Put => await PutJsonResponseAsync<T>(),
                HttpType.Delete => await DeleteJsonResponseAsync<T>(),
                _ => throw new Exception(message: $"Missing of {nameof(_configuration.RequestType)}")
            };
            return respond;
        }
        public T? GetFormRespond<T>()
        {
            return _configuration.RequestType switch
            {
                HttpType.Get => GetFormResponse<T>(),
                HttpType.Post => PostFormResponse<T>(),
                HttpType.Patch => PatchFormResponse<T>(),
                HttpType.Put => PutFormResponse<T>(),
                HttpType.Delete => DeleteFormResponse<T>(),
                _ => throw new Exception(message: $"Missing of {nameof(_configuration.RequestType)}")
            };
        }
        public async Task<T>? GetFormRespondAsync<T>()
        {
            var respond = _configuration.RequestType switch
            {
                HttpType.Get => await GetFormResponseAsync<T>(),
                HttpType.Post => await PostFormResponseAsync<T>(),
                HttpType.Patch => await PatchFormResponseAsync<T>(),
                HttpType.Put => await PutFormResponseAsync<T>(),
                HttpType.Delete => await DeleteFormResponseAsync<T>(),
                _ => throw new Exception(message: $"Missing of {nameof(_configuration.RequestType)}")
            };
            return respond ?? throw new Exception(message: $"Missing of {nameof(_configuration.RequestType)}");
        }
        #endregion
    }
}
