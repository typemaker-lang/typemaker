using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace Typemaker.Compiler.Settings
{
	public static class SettingsFactory
	{
		static Task<string> ReadFile(string inputPath)
		{
			using (var input = new FileStream(inputPath, FileMode.Open, FileAccess.Read, FileShare.Read | FileShare.Delete, 4096, true))
			using (var reader = new StreamReader(input))
				return reader.ReadToEndAsync();
		}

		public static async Task<TypemakerSettings> DeserializeSettings(string inputPath, TypemakerSettings overrides)
		{
			if (inputPath == null)
				throw new ArgumentNullException(nameof(inputPath));

			var seenPaths = new List<string>();

			var json = 

			var serializerSettings = new JsonSerializerSettings()
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			};

			var topLevelJObject = JObject.Parse(json);
			var overridesJObject = overrides != null ? JObject.FromObject(overrides) : null;

			var basics = topLevelJObject.ToObject<BasicSettings>();
			if(basics.Extends != null)
			{

			}


			basics = finalJObject.ToObject<BasicSettings>();
			switch (basics.Version)
			{
				case 1:
					return finalJObject.ToObject<Version1>();
				case null:
					return null;
				default:
					throw new NotSupportedException(String.Format(CultureInfo.InvariantCulture, "The json version {0} is not supported!", basics.Version));
			}
		}
	}
}
