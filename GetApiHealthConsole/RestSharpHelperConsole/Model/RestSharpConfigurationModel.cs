namespace RestSharpHelperConsole.Model
{
    /// <summary>
    /// Rest Sharp Configuration
    /// </summary>
    public class RestSharpConfigurationModel
    {
        /// <summary>
        /// Host Url
        /// </summary>
        public string HostUrl { get; set; } = null!;
        /// <summary>
        /// Request Type
        /// </summary>
        public HttpType RequestType { get; set; }
        /// <summary>
        /// Token Shceme
        /// </summary>
        public string? TokenShceme { get; set; }
        /// <summary>
        /// Token Session
        /// </summary>
        public string? TokenSession { get; set; }
        /// <summary>
        /// Query Param
        /// </summary>
        public List<QueryParamModel>? QueryParam { get; set; }
        /// <summary>
        /// Body Form
        /// </summary>
        public BodyFormModel? BodyForm { get; set; }
    }
    public enum HttpType
    {
        Get,
        Post,
        Patch,
        Put,
        Delete,
    }
}
