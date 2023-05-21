using RestSharpHelperConsole.Model;

namespace GetApiHealthConsole.Process
{
    internal static class ReadFile
    {
        internal static Stack<RestSharpConfigurationModel> ReadFileProccess()
        {
            CheckAndCreateFile();
            Stack<RestSharpConfigurationModel> urlList = new();
            try
            {
                urlList = GetUrlLink();
            }
            catch (Exception ex)
            {
                EndProgram.StopProgramErrors("ReadFileProccess", ex.Message);
            }
            return urlList;
        }
        private static Stack<RestSharpConfigurationModel> GetUrlLink()
        {
            string[] urlFile = File.ReadAllLines("urllist.txt");
            Stack<RestSharpConfigurationModel> urlStack = new();
            foreach (var url in from string url in urlFile
                                where CheckURLValid(url)
                                select url)
            {
                urlStack.Push(new()
                {
                    HostUrl= url,
                    RequestType = HttpType.Get,
                });
            }

            if(urlStack.Count <= 0) EndProgram.StopProgramErrors("ReadFileProccess", "No Url in List");

            return urlStack;
        }
        private static bool CheckURLValid(string source)
            => Uri.TryCreate(source, UriKind.Absolute, out Uri? uriResult)
            && uriResult != null
            && (uriResult.Scheme == Uri.UriSchemeHttps
            || uriResult.Scheme == Uri.UriSchemeHttp);
        private static void CheckAndCreateFile()
        {
            if (File.Exists("urllist.txt")) return;

            Console.WriteLine("Start Create File urllist.txt for next use");
            File.Create("urllist.txt");
            EndProgram.StopProgramErrors("ReadFileProccess", "Please add URL to that file separate by file");
        }
    }
}
