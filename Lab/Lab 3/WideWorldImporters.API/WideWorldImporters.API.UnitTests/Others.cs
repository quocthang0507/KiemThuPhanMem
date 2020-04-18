using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WideWorldImporters.API.Controllers;
using WideWorldImporters.API.Models;
using Xunit;

namespace WideWorldImporters.API.UnitTests
{
	public class Others
	{
		/// <summary>
		/// Tìm kiếm Stock Item theo các tham số như lastEditedBy, colorID, outerPackageID, supplierID, unitPackageID
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestGetStockItem()
		{
			var dbContext = DbContextMocker.GetWideWorldImportersDbContext(nameof(TestGetStockItem));
			var controller = new WarehouseController(null, dbContext);

			int supplierID = 12;
			int colorID = 12;
			int unitPackageID = 7;
			int outerPackageID = 7;
			int lastEditedBy = 1;

			var response = await controller.GetStockItemAsync(supplierID, colorID, unitPackageID, outerPackageID, lastEditedBy) as ObjectResult;
			var value = response.Value as ISingleResponse<StockItem>;

			dbContext.Dispose();

			Assert.False(value.DidError);
		}

		/// <summary>
		/// Lấy Stock Item với đầu vào là ID không tồn tại, kiểm tra Web API trả về trạng thái NotFound(404)
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestGetNonExistingStockItem()
		{
			var dbContext = DbContextMocker.GetWideWorldImportersDbContext(nameof(TestGetStockItem));
			var controller = new WarehouseController(null, dbContext);

			int id = 999; // In stock items table, there are only 446 rows

			var response = await controller.GetStockItemAsync(id) as ObjectResult;

			dbContext.Dispose();

			Assert.False(response.StatusCode != 404);
		}

		/// <summary>
		/// Thêm một Stock Item mà mẫu tin đó đã tồn tại trong hệ thống, kiểm tra Web API trả về trạng thái BadRequest(400)
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestPostStockItemWithExistingName()
		{
			var dbContext = DbContextMocker.GetWideWorldImportersDbContext(nameof(TestPostStockItemWithExistingName));
			var controller = new WarehouseController(null, dbContext);

			var request = new PostStockItemsRequest
			{
				StockItemName = "USB rocket launcher (Gray)"
			};

			var response = await controller.PostStockItemAsync(request) as BadRequestResult;

			dbContext.Dispose();

			Assert.False(response.StatusCode != 400);
		}

		/// <summary>
		/// Thêm vào một Stock Item mà bỏ trống các trường bắt buộc, kiểm tra Web API trả về trạng thái BadRequest(400)
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestPostStockItemWithoutRequiredFields()
		{
			var dbContext = DbContextMocker.GetWideWorldImportersDbContext(nameof(TestPostStockItemWithoutRequiredFields));
			var controller = new WarehouseController(null, dbContext);

			var request = new PostStockItemsRequest();

			var response = await controller.PostStockItemAsync(request) as BadRequestResult;

			dbContext.Dispose();

			Assert.False(response.StatusCode != 400);
		}

		/// <summary>
		/// Cập nhật một Stock Item sử dụng một ID không tồn tại, kiểm tra Web API returns NotFound(404) status
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestPutNonExistingStockItem()
		{
			var dbContext = DbContextMocker.GetWideWorldImportersDbContext(nameof(TestPutNonExistingStockItem));
			var controller = new WarehouseController(null, dbContext);
			var id = 999;
			var request = new PutStockItemsRequest
			{
				StockItemName = "USB food flash drive (Update)",
				SupplierID = 12,
				ColorID = 3
			};

			var response = await controller.PutStockItemAsync(id, request) as NotFoundResult;

			dbContext.Dispose();

			Assert.False(response.StatusCode != 404);
		}

		/// <summary>
		/// Cập nhật một Stock Item đã tồn tại mà bỏ trống các trường bắt buộc, kiểm tra Web API trả về trạng thái BadRequest(400)
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestPutExistingStockItemWithoutRequiredFields()
		{
			var dbContext = DbContextMocker.GetWideWorldImportersDbContext(nameof(TestPutExistingStockItemWithoutRequiredFields));
			var controller = new WarehouseController(null, dbContext);
			var id = 12;
			var request = new PutStockItemsRequest();

			var response = await controller.PutStockItemAsync(id, request) as BadRequestResult;

			dbContext.Dispose();

			Assert.False(response.StatusCode != 400);
		}

		/// <summary>
		/// Xóa một Stock Item sử dụng một ID không tồn tại và kiểm tra Web API trả về trạng thái NotFound(404)
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestDeleteNonExistingStockItem()
		{
			var dbContext = DbContextMocker.GetWideWorldImportersDbContext(nameof(TestDeleteNonExistingStockItem));
			var controller = new WarehouseController(null, dbContext);
			var id = 999;

			var response = await controller.DeleteStockItemAsync(id) as NotFoundResult;

			dbContext.Dispose();

			Assert.False(response.StatusCode != 404);
		}
	}
}
