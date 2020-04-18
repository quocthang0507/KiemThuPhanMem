using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace WideWorldImporters.API.IntegrationTests
{
	/// <summary>
	/// ContentHelper class contains a helper method to create StringContent from request model as JSON, this applies for POST and PUT requests
	/// </summary>
	public static class ContentHelper
	{
		public static StringContent GetStringContent(object obj) => new StringContent(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json");
	}
}
