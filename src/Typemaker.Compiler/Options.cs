using Newtonsoft.Json;
using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Typemaker.Compiler
{

    public class TypemakerOptions
    {
        public static class JsonTree
        {
            public class ByondVersion
            {
                [JsonProperty("major")]
                public long Major { get; set; }

                [JsonProperty("minor")]
                public long Minor { get; set; }
            }

            public class LinterSettings
            {
                [JsonProperty("enforce_tabs")]
                public bool EnforceTabs { get; set; }

                [JsonProperty("allman_braces")]
                public bool AllmanBraces { get; set; }

                [JsonProperty("no_operator_overloading")]
                public bool NoOperatorOverloading { get; set; }

                [JsonProperty("no_single_line_blocks")]
                public bool NoSingleLineBlocks { get; set; }
            }

            public class Scripts
            {
                [JsonProperty("pre_transpile")]
                public Script PreTranspile { get; set; }

                [JsonProperty("pre_compile")]
                public Script PreCompile { get; set; }

                [JsonProperty("post_compile")]
                public Script PostCompile { get; set; }
            }

            public class Script
            {
                [JsonProperty("windows")]
                public string Windows { get; set; }

                [JsonProperty("linux")]
                public string Linux { get; set; }
            }
        }
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("code_root")]
        public string CodeRoot { get; set; }

        [JsonProperty("output_directory")]
        public string OutputDirectory { get; set; }

        [JsonProperty("byond_version")]
        public JsonTree.ByondVersion BYONDVersion { get; set; }

        [JsonProperty("debug")]
        public bool Debug { get; set; }

        [JsonProperty("scripts")]
        public JsonTree.Scripts ShellScripts { get; set; }

        [JsonProperty("dme")]
        public string Dme { get; set; }

        [JsonProperty("linter_settings")]
        public JsonTree.LinterSettings LinterSettings { get; set; }

    }
    public class CommandLineOptions
    {
        [Option('r', "read", Required = true, HelpText = "The TM file to compile.", Default = "test.tm")]
        public string TmFile { get; set; }

        [Option(HelpText = "Sets the DEBUG preprocessor macro.", Default = null)]
        public bool? DebugMode { get; set; }

        [Option('v', "verbose", Required = false, Default = false)]
        public bool Verbose { get; set; }
    }
}
