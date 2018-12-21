using CommandLine;
using System;
using System.IO;

namespace Typemaker.Compiler.Cli
{
	class Program
	{
		static int Main(string[] args)
		{
			/* TODO
             * Move this logic elsewhere
             */
			 /*
			CommandLineOptions options = null;

			CommandLine.Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsed(opt => options = opt).WithNotParsed(err =>
			{
				Console.WriteLine("Error reading command-line arguments. Quitting...");
				Environment.Exit(-1);
			});

			if (!File.Exists("typemaker.json"))
			{
				Console.WriteLine("No typemaker.json configuration found. Quitting...");
				return -1;
			}

			TypemakerOptions typemakerOptions = null;

			try
			{
				typemakerOptions = Newtonsoft.Json.JsonConvert.DeserializeObject<TypemakerOptions>(File.ReadAllText("typemaker.json"));
			}
			catch (Newtonsoft.Json.JsonException e)
			{
				Console.WriteLine("Error deserializing typemaker.json. Error message: {0}. Quitting...", e.Message);
				return -1;
			}

			if (options.DebugMode != null)
			{
				typemakerOptions.Debug = (bool)options.DebugMode;
			}
			*/
			return 0;
		}
	}
}
