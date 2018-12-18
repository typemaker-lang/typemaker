using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
		static TypemakerSettings FullDeserialize(JObject typemakerObject, string relativePathToFix)
		{
			TypemakerSettings result;
			var versionRead = typemakerObject.ToObject<BasicSettings>();
			switch (versionRead.Version)
			{
				case 1:
					result = typemakerObject.ToObject<Version1>();
					break;
				case null:
					return null;
				default:
					throw new NotSupportedException(String.Format(CultureInfo.InvariantCulture, "The json version {0} is not supported!", versionRead.Version));
			}

			if (relativePathToFix != null)
				result.FixPaths(relativePathToFix);

			return result;
		}
		
		public static async Task<TypemakerSettings> DeserializeSettings(string inputPath, TypemakerSettings overrides)
		{
			if (inputPath == null)
				throw new ArgumentNullException(nameof(inputPath));

			var seenPaths = new List<string>();
			var seenSettings = new Stack<JObject>();

			var camelCase = new CamelCasePropertyNamesContractResolver();
			var serializerSettings = new JsonSerializerSettings()
			{
				ContractResolver = camelCase
			};
			
			if (overrides != null)
			{
				var overridesJObject = JObject.FromObject(overrides, new JsonSerializer()
				{
					ContractResolver = camelCase
				});
				seenSettings.Push(overridesJObject);
			}

			var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

			inputPath = Path.GetFullPath(inputPath);
			do
			{
				var inputDirectory = Path.GetDirectoryName(inputPath);
				var normalizedPath = isWindows ? inputPath.ToUpperInvariant() : inputPath;

				if (seenPaths.Any(x => x == normalizedPath))
					break;

				var json = await ReadFile(inputPath).ConfigureAwait(false);

				var jObject = JObject.Parse(json);
				var overridesJObject = overrides != null ? JObject.FromObject(overrides) : null;

				var entensionRead = FullDeserialize(jObject, inputDirectory);

				seenPaths.Add(normalizedPath);
				seenSettings.Push(jObject);

				inputPath = entensionRead.Extends;
			}
			while (inputPath != null);

			var finalJObject = new JObject();
			do
			{
				var currentSetting = seenSettings.Pop();
				finalJObject.Merge(currentSetting, new JsonMergeSettings
				{
					MergeArrayHandling = MergeArrayHandling.Replace,
					MergeNullValueHandling = MergeNullValueHandling.Merge
				});
			}
			while (seenSettings.Count > 0);

			return FullDeserialize(finalJObject, null);
		}
	}
}
