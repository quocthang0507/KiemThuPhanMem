using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WideWorldImporters.API.Models;
using Xunit;

namespace WideWorldImporters.API.IntegrationTests
{
	/// <summary>
	/// WarehouseTests class contains all methods to send Http requests for Web API, the port number for Http client is 1234
	/// </summary>
	public class WarehouseTests : IClassFixture<TestFixture<Startup>>
	{
		private HttpClient Client;

		public WarehouseTests(TestFixture<Startup> fixture)
		{
			Client = fixture.Client;
		}

		/// <summary>
		/// Retrieves the stock item
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestGetStockItemsAsync()
		{
			var request = "/api/v1/Warehouse/StockItem";

			var response = await Client.GetAsync(request);

			response.EnsureSuccessStatusCode();
		}

		/// <summary>
		/// Retrieves an existing stock item by ID
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestGetStockItemAsync()
		{
			var request = "/api/v1/Warehouse/StockItem/1";

			var response = await Client.GetAsync(request);

			response.EnsureSuccessStatusCode();
		}

		/// <summary>
		/// Creates a new stock item
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestPostStockItemAsync()
		{
			var request = new
			{
				Url = "/api/v1/Warehouse/StockItem",
				Body = new
				{
					StockItemName = string.Format("USB anime flash drive - Vegeta {0}", Guid.NewGuid()),
					SupplierID = 12,
					UnitPackageID = 7,
					OuterPackageID = 7,
					LeadTimeDays = 14,
					QuantityPerOuter = 1,
					IsChillerStock = false,
					TaxRate = 15.000m,
					UnitPrice = 32.00m,
					RecommendedRetailPrice = 47.84m,
					TypicalWeightPerUnit = 0.050m,
					CustomFields = "{ \"CountryOfManufacture\": \"Japan\", \"Tags\": [\"32GB\",\"USB Powered\"] }",
					Tags = "[\"32GB\",\"USB Powered\"]",
					SearchDetails = "USB anime flash drive - Vegeta",
					LastEditedBy = 1,
					ValidFrom = DateTime.Now,
					ValidTo = DateTime.Now.AddYears(5)
				}
			};

			var response = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
			var value = await response.Content.ReadAsStringAsync();

			response.EnsureSuccessStatusCode();
		}

		/// <summary>
		/// Updates an existing stock item
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestPutStockItemAsync()
		{
			var request = new
			{
				Url = "/api/v1/Warehouse/StockItem/1",
				Body = new
				{
					StockItemName = string.Format("USB anime flash drive - Vegeta {0}", Guid.NewGuid()),
					SupplierID = 12,
					ColorID = 3,
					UnitPrice = 39.00m
				}
			};

			var response = await Client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));

			response.EnsureSuccessStatusCode();
		}

		/// <summary>
		/// Deletes an existing stock item
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestDeleteStockItemAsync()
		{
			var postRequest = new
			{
				Url = "/api/v1/Warehouse/StockItem",
				Body = new
				{
					StockItemName = string.Format("Product to delete {0}", Guid.NewGuid()),
					SupplierID = 12,
					UnitPackageID = 7,
					OuterPackageID = 7,
					LeadTimeDays = 14,
					QuantityPerOuter = 1,
					IsChillerStock = false,
					TaxRate = 10.000m,
					UnitPrice = 10.00m,
					RecommendedRetailPrice = 47.84m,
					TypicalWeightPerUnit = 0.050m,
					CustomFields = "{ \"CountryOfManufacture\": \"USA\", \"Tags\": [\"Sample\"] }",
					Tags = "[\"Sample\"]",
					SearchDetails = "Product to delete",
					LastEditedBy = 1,
					ValidFrom = DateTime.Now,
					ValidTo = DateTime.Now.AddYears(5)
				}
			};

			var postResponse = await Client.PostAsync(postRequest.Url, ContentHelper.GetStringContent(postRequest.Body));
			var jsonFromPostResponse = await postResponse.Content.ReadAsStringAsync();

			var singleResponse = JsonConvert.DeserializeObject<SingleResponse<StockItem>>(jsonFromPostResponse);

			var deleteResponse = await Client.DeleteAsync(string.Format("/api/v1/Warehouse/StockItem/{0}", singleResponse.Model.StockItemID));

			postResponse.EnsureSuccessStatusCode();

			Assert.False(singleResponse.DidError);

			deleteResponse.EnsureSuccessStatusCode();
		}
	}
}
