// See https://aka.ms/new-console-template for more information
using GetApiHealthConsole.Process;

Console.WriteLine("Start Program");
var listUrl = ReadFile.ReadFileProccess();
new GetRequest(listUrl).GetRequestProccess();

