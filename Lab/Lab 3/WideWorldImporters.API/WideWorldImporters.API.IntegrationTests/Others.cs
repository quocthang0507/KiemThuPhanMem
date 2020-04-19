using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace WideWorldImporters.API.IntegrationTests
{
	public class Others : IClassFixture<TestFixture<Startup>>
	{
		private HttpClient Client;

		public Others(TestFixture<Startup> fixture)
		{
			Client = fixture.Client;
		}

		/// <summary>
		/// Tìm kiếm Stock Items theo các tham số như lastEditedBy, colorID, outerPackageID, supplierID, unitPackageID
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestGetStockItemsWithParams()
		{
			var request = "/api/v1/Warehouse/StockItem/5/35/7/7/1"; //StockItem/{supplierID}/{colorID}/{unitPackageID}/{outerPackageID}/{lastEditedBy}

			var response = await Client.GetAsync(request);

			response.EnsureSuccessStatusCode();
		}

		/// <summary>
		/// Lấy Stock Item với đầu vào là ID không tồn tại, kiểm tra Web API trả về trạng thái NotFound(404)
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestGetNonExistingStockItem()
		{
			var request = "/api/v1/Warehouse/StockItem/999";

			var response = await Client.GetAsync(request);

			Assert.False(response.StatusCode != System.Net.HttpStatusCode.NotFound);
		}

		/// <summary>
		/// Thêm một Stock Item mà mẫu tin đó đã tồn tại trong hệ thống, kiểm tra Web API trả về trạng thái BadRequest(400)
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestPostStockItemWithExistingName()
		{
			var request = new
			{
				Url = "/api/v1/Warehouse/StockItem",
				Body = new
				{
					StockItemName = "USB rocket launcher (Gray)"
				}
			};

			var response = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

			Assert.False(response.StatusCode != System.Net.HttpStatusCode.BadRequest);
		}

		/// <summary>
		/// Thêm vào một Stock Item mà bỏ trống các trường bắt buộc, kiểm tra Web API trả về trạng thái BadRequest(400)
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestPostStockItemWithoutRequiredFields()
		{
			var request = new
			{
				Url = "/api/v1/Warehouse/StockItem",
				Body = new
				{
				}
			};

			var response = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));

			Assert.False(response.StatusCode != System.Net.HttpStatusCode.BadRequest);
		}

		/// <summary>
		/// Cập nhật một Stock Item sử dụng một ID không tồn tại, kiểm tra Web API returns NotFound(404) status
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestPutNonExistingStockItem()
		{
			var request = new
			{
				Url = "/api/v1/Warehouse/StockItem/999",
				Body = new
				{
					StockItemName = string.Format("USB anime flash drive - Vegeta {0}", Guid.NewGuid()),
					SupplierID = 12,
					ColorID = 3,
					UnitPrice = 39.00m
				}
			};

			var response = await Client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));

			Assert.False(response.StatusCode != System.Net.HttpStatusCode.NotFound);
		}

		/// <summary>
		/// Cập nhật một Stock Item đã tồn tại mà bỏ trống các trường bắt buộc, kiểm tra Web API trả về trạng thái BadRequest(400)
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestPutExistingStockItemWithoutRequiredFields()
		{
			var request = new
			{
				Url = "/api/v1/Warehouse/StockItem/1",
				Body = new
				{
				}
			};

			var response = await Client.PutAsync(request.Url, ContentHelper.GetStringContent(request.Body));

			Assert.False(response.StatusCode != System.Net.HttpStatusCode.BadRequest);
		}

		/// <summary>
		/// Xóa một Stock Item sử dụng một ID không tồn tại và kiểm tra Web API trả về trạng thái NotFound(404)
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task TestDeleteNonExistingStockItem()
		{
			var deleteResponse = await Client.DeleteAsync(string.Format("/api/v1/Warehouse/StockItem/{0}", 999));

			Assert.False(deleteResponse.StatusCode != System.Net.HttpStatusCode.NotFound);
		}

	}
}
