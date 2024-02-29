using CropMeasureEligibility.Editor.Models;
using CropMeasureEligibility.Editor.Models.ListD;
using Newtonsoft.Json;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Channels;

namespace CropMeasureEligibility.Editor.Common
{
	public static class Helpers
	{
		// Serialize an object to a JSON file
		public static async Task SerializeToJsonFileAsync<T>(string filePath, T data)
		{
			try
			{
				using (FileStream fs = File.Create(filePath))
				{
					await System.Text.Json.JsonSerializer.SerializeAsync(fs, data, new JsonSerializerOptions
					{
						//WriteIndented = true // Optional: format the JSON for readability
					});
				}

				Console.WriteLine($"Data serialized and saved to {filePath}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
			}
		}

		// Deserialize an object from a JSON file
		public static async Task<T> DeserializeFromJsonFileAsync<T>(string filePath)
		{
			try
			{
				using (FileStream fs = File.OpenRead(filePath))
				{
					return await System.Text.Json.JsonSerializer.DeserializeAsync<T>(fs);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
				return default; // or throw an exception depending on your error handling strategy
			}
		}

		public static T DeserializeFromJsonString<T>(string jsonString)
		{
			try
			{

				//return JsonSerializer.Deserialize<T>(jsonString);

				return JsonConvert.DeserializeObject<T>(jsonString);

			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
				return default; // or throw an exception depending on your error handling strategy
			}
		}

		public static string SerializeToJsonString<T>(T objectDto)
		{
			try
			{

				//return JsonSerializer.Serialize(objectDto, new JsonSerializerOptions
				//{
				//	WriteIndented = true,
				//	Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
				//});

				return JsonConvert.SerializeObject(objectDto);

			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
				return default; // or throw an exception depending on your error handling strategy
			}
		}

		public static string GetProjectDirectory()
		{
			string currentDirectory = Directory.GetCurrentDirectory();
			int binIndex = currentDirectory.IndexOf("\\bin\\", StringComparison.OrdinalIgnoreCase);
			return binIndex >= 0 ? currentDirectory.Substring(0, binIndex) : currentDirectory;
		}
	}
}
