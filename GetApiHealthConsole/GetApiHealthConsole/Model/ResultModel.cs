using RestSharpHelperConsole.Model;
using System.Text.Json;

namespace GetApiHealthConsole.Model
{
    internal class ResultModel
    {
        public string Url { get; set; } = null!;
        public HttpType RequestType { get; set; }
        public object? Result { get; set; }

        public void WriteConsole()
        {
            Console.WriteLine("=================================");
            Console.WriteLine($"Host URL: {Url}");
            Console.WriteLine($"Request Type: {RequestType}");
            Console.WriteLine($"Result:");
            Console.WriteLine("---------------------------------");
            Console.WriteLine(JsonPrettify(Result));
            Console.WriteLine("=================================");
        }

        private static string JsonPrettify(object? json)
        {
            if (json == null) return string.Empty;
            return JsonSerializer.Serialize(json, new JsonSerializerOptions
            {
                WriteIndented = true
            });
        }
    }
}
