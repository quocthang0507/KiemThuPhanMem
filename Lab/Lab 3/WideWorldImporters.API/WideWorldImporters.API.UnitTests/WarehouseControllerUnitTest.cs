using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WideWorldImporters.API.Controllers;
using WideWorldImporters.API.Models;
using Xunit;

namespace WideWorldImporters.API.UnitTests
{
	/// <summary>
	/// WarehouseControllerUnitTest class contains all tests for WarehouseController class.
	/// </summary>
	public class WarehouseControllerUnitTest
	{
		/// <summary>
		/// Retrieves the stock items
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestGetStockItemsAsync()
		{
			var dbContext = DbContextMocker.GetWideWorldImportersDbContext(nameof(TestGetStockItemsAsync));
			var controller = new WarehouseController(null, dbContext);

			var response = await controller.GetStockItemsAsync() as ObjectResult;
			var value = response.Value as IPagedResponse<StockItem>;

			dbContext.Dispose();

			Assert.False(value.DidError);
		}

		/// <summary>
		/// Retrieves an existing stock item by ID
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestGetStockItemAsync()
		{
			var dbContext = DbContextMocker.GetWideWorldImportersDbContext(nameof(TestGetStockItemAsync));
			var controller = new WarehouseController(null, dbContext);
			var id = 1;

			var response = await controller.GetStockItemAsync(id) as ObjectResult;
			var value = response.Value as ISingleResponse<StockItem>;

			dbContext.Dispose();

			Assert.False(value.DidError);
		}

		/// <summary>
		/// Creates a new stock item
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestPostStockItemAsync()
		{
			var dbContext = DbContextMocker.GetWideWorldImportersDbContext(nameof(TestPostStockItemAsync));
			var controller = new WarehouseController(null, dbContext);
			var request = new PostStockItemsRequest
			{
				StockItemID = 100,
				StockItemName = "USB anime flash drive - Goku",
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
				SearchDetails = "USB anime flash drive - Goku",
				LastEditedBy = 1,
				ValidFrom = DateTime.Now,
				ValidTo = DateTime.Now.AddYears(5)
			};

			var response = await controller.PostStockItemAsync(request) as ObjectResult;
			var value = response.Value as ISingleResponse<StockItem>;

			dbContext.Dispose();

			Assert.False(value.DidError);
		}

		/// <summary>
		/// Updates an existing stock item
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestPutStockItemAsync()
		{
			var dbContext = DbContextMocker.GetWideWorldImportersDbContext(nameof(TestPutStockItemAsync));
			var controller = new WarehouseController(null, dbContext);
			var id = 12;
			var request = new PutStockItemsRequest
			{
				StockItemName = "USB food flash drive (Update)",
				SupplierID = 12,
				ColorID = 3
			};

			var response = await controller.PutStockItemAsync(id, request) as ObjectResult;
			var value = response.Value as IResponse;

			dbContext.Dispose();

			Assert.False(value.DidError);
		}

		/// <summary>
		/// Deletes an existing stock item
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestDeleteStockItemAsync()
		{
			var dbContext = DbContextMocker.GetWideWorldImportersDbContext(nameof(TestDeleteStockItemAsync));
			var controller = new WarehouseController(null, dbContext);
			var id = 5;

			var response = await controller.DeleteStockItemAsync(id) as ObjectResult;
			var value = response.Value as IResponse;

			dbContext.Dispose();

			Assert.False(value.DidError);
		}
	}
}
