using CommandLine;
using System;
using System.IO;

namespace Typemaker.Compiler
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            /* TODO
             * Move this logic elsewhere
             */
            CommandLineOptions options = null;

            CommandLine.Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsed(opt => options = opt).WithNotParsed<CommandLineOptions>(err =>
            {
                Console.WriteLine("Error reading command-line arguments. Quitting...");
                Environment.Exit(-1);
            }
            );

            if (!File.Exists("typemaker.json"))
            {
                Console.WriteLine("No typemaker.json configuration found. Quitting...");
                Environment.Exit(-1);
            }

            TypemakerOptions typemakerOptions = null;

            try
            {
                typemakerOptions = Newtonsoft.Json.JsonConvert.DeserializeObject<TypemakerOptions>(File.ReadAllText("typemaker.json"));
            }
            catch (Newtonsoft.Json.JsonException e)
            {
                Console.WriteLine("Error deserializing typemaker.json. Error message: {0}. Quitting...", e.Message);
            }

            if (options.DebugMode != null)
            {
                typemakerOptions.Debug = (bool)options.DebugMode;
            }

            using (FileStream fs = new FileStream(options.TmFile, FileMode.Open, FileAccess.Read))
            {
                Parser.TypemakerLexer lexer = new Parser.TypemakerLexer(new Antlr4.Runtime.AntlrInputStream(fs));
                System.Collections.Generic.IList<Antlr4.Runtime.IToken> tokens = lexer.GetAllTokens();
            }
        }
    }
}
