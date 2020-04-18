using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WideWorldImporters.API.Models;

namespace WideWorldImporters.API.Controllers
{
#pragma warning disable CS1591

	[ApiController]
	[Route("api/v1/[controller]")]
	public class WarehouseController : ControllerBase
	{
		protected readonly ILogger Logger;
		protected readonly WideWorldImportersDbContext DbContext;

		public WarehouseController(ILogger<WarehouseController> logger, WideWorldImportersDbContext dbContext)
		{
			Logger = logger;
			DbContext = dbContext;
		}
#pragma warning restore CS1591

		// GET
		// api/v1/Warehouse/StockItem

		/// <summary>
		/// Retrieves stock items
		/// </summary>
		/// <param name="pageSize">Page size</param>
		/// <param name="pageNumber">Page number</param>
		/// <param name="lastEditedBy">Last edit by (user id)</param>
		/// <param name="colorID">Color id</param>
		/// <param name="outerPackageID">Outer package id</param>
		/// <param name="supplierID">Supplier id</param>
		/// <param name="unitPackageID">Unit package id</param>
		/// <returns>A response with stock items list</returns>
		/// <response code="200">Returns the stock items list</response>
		/// <response code="500">If there was an internal server error</response>
		[HttpGet("StockItem")]
		[ProducesResponseType(200)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> GetStockItemsAsync(int pageSize = 10, int pageNumber = 1, int? lastEditedBy = null, int? colorID = null, int? outerPackageID = null, int? supplierID = null, int? unitPackageID = null)
		{
			Logger?.LogDebug("'{0}' has been invoked", nameof(GetStockItemsAsync));

			var response = new PagedResponse<StockItem>();

			try
			{
				var query = DbContext.GetStockItems();

				response.PageSize = pageSize;
				response.PageNumber = pageNumber;

				response.ItemsCount = await query.CountAsync();

				response.Model = await query.Paging(pageSize, pageNumber).ToListAsync();

				response.Message = string.Format("Page {0} of {1}, Total of products: {2}.", pageNumber, response.PageCount, response.ItemsCount);

				Logger?.LogInformation("The stock items have been retrieved successfully.");
			}
			catch (Exception ex)
			{
				response.DidError = true;
				response.ErrorMessage = "There was an internal error, please contact to technical support.";

				Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(GetStockItemsAsync), ex);
			}

			return response.ToHttpResponse();
		}

		// GET
		// api/v1/Warehouse/StockItem/5

		/// <summary>
		/// Retrieves a stock item by ID
		/// </summary>
		/// <param name="id">Stock item id</param>
		/// <returns>A response with stock item</returns>
		/// <response code="200">Returns the stock items list</response>
		/// <response code="404">If stock item is not exists</response>
		/// <response code="500">If there was an internal server error</response>
		[HttpGet("StockItem/{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> GetStockItemAsync(int id)
		{
			Logger?.LogDebug("'{0}' has been invoked", nameof(GetStockItemAsync));

			var response = new SingleResponse<StockItem>();

			try
			{
				response.Model = await DbContext.GetStockItemsAsync(new StockItem(id));
			}
			catch (Exception ex)
			{
				response.DidError = true;
				response.ErrorMessage = "There was an internal error, please contact to technical support.";

				Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(GetStockItemAsync), ex);
			}

			return response.ToHttpResponse();
		}

		// GET

		/// <summary>
		/// Tìm kiếm Stock Item theo các tham số như lastEditedBy, colorID, outerPackageID, supplierID, unitPackageID
		/// </summary>
		/// <param name="supplierID"></param>
		/// <param name="colorID"></param>
		/// <param name="unitPackageID"></param>
		/// <param name="outerPackageID"></param>
		/// <param name="lastEditedBy"></param>
		/// <returns></returns>
		/// <response code="200">Returns the stock items list</response>
		/// <response code="404">If stock item is not exists</response>
		/// <response code="500">If there was an internal server error</response>
		[HttpGet("StockItem/{supplierID}/{colorID}/{unitPackageID}/{outerPackageID}/{lastEditedBy}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> GetStockItemAsync(int supplierID, int colorID, int unitPackageID, int outerPackageID, int lastEditedBy)
		{
			Logger?.LogDebug("'{0}' has been invoked", nameof(GetStockItemAsync));

			var response = new SingleResponse<StockItem>();

			try
			{
				response.Model = await DbContext.GetStockItemsAsync(supplierID, colorID, unitPackageID, outerPackageID, lastEditedBy);
			}
			catch (Exception ex)
			{
				response.DidError = true;
				response.ErrorMessage = "There was an internal error, please contact to technical support.";

				Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(GetStockItemAsync), ex);
			}

			return response.ToHttpResponse();
		}


		// POST
		// api/v1/Warehouse/StockItem/

		/// <summary>
		/// Creates a new stock item
		/// </summary>
		/// <param name="request">Request model</param>
		/// <returns>A response with new stock item</returns>
		/// <response code="200">Returns the stock items list</response>
		/// <response code="201">A response as creation of stock item</response>
		/// <response code="400">For bad request</response>
		/// <response code="500">If there was an internal server error</response>
		[HttpPost("StockItem")]
		[ProducesResponseType(200)]
		[ProducesResponseType(201)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> PostStockItemAsync([FromBody]PostStockItemsRequest request)
		{
			Logger?.LogDebug("'{0}' has been invoked", nameof(PostStockItemAsync));

			var response = new SingleResponse<StockItem>();

			try
			{
				var existingEntity = await DbContext.GetStockItemsByStockItemNameAsync(new StockItem { StockItemName = request.StockItemName });

				if (existingEntity != null)
					ModelState.AddModelError("StockItemName", "Stock item name already exists");

				if (!ModelState.IsValid || request.StockItemName == null)
					return BadRequest();

				var entity = request.ToEntity();

				DbContext.Add(entity);

				await DbContext.SaveChangesAsync();

				response.Model = entity;
			}
			catch (Exception ex)
			{
				response.DidError = true;
				response.ErrorMessage = "There was an internal error, please contact to technical support.";

				Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(PostStockItemAsync), ex);
			}

			return response.ToHttpResponse();
		}

		// PUT
		// api/v1/Warehouse/StockItem/5

		/// <summary>
		/// Updates an existing stock item
		/// </summary>
		/// <param name="id">Stock item ID</param>
		/// <param name="request">Request model</param>
		/// <returns>A response as update stock item result</returns>
		/// <response code="200">If stock item was updated successfully</response>
		/// <response code="400">For bad request</response>
		/// <response code="500">If there was an internal server error</response>
		[HttpPut("StockItem/{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> PutStockItemAsync(int id, [FromBody]PutStockItemsRequest request)
		{
			Logger?.LogDebug("'{0}' has been invoked", nameof(PutStockItemAsync));

			var response = new Response();

			try
			{
				var entity = await DbContext.GetStockItemsAsync(new StockItem(id));

				if (entity == null)
					return NotFound();

				if (request.StockItemName == null || request.SupplierID == null)
					return BadRequest();

				entity.StockItemName = request.StockItemName;
				entity.SupplierID = request.SupplierID;
				entity.ColorID = request.ColorID;
				entity.UnitPrice = request.UnitPrice;

				DbContext.Update(entity);

				await DbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				response.DidError = true;
				response.ErrorMessage = "There was an internal error, please contact to technical support.";

				Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(PutStockItemAsync), ex);
			}

			return response.ToHttpResponse();
		}

		// DELETE
		// api/v1/Warehouse/StockItem/5

		/// <summary>
		/// Deletes an existing stock item
		/// </summary>
		/// <param name="id">Stock item ID</param>
		/// <returns>A response as delete stock item result</returns>
		/// <response code="200">If stock item was deleted successfully</response>
		/// <response code="500">If there was an internal server error</response>
		[HttpDelete("StockItem/{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> DeleteStockItemAsync(int id)
		{
			Logger?.LogDebug("'{0}' has been invoked", nameof(DeleteStockItemAsync));

			var response = new Response();

			try
			{
				var entity = await DbContext.GetStockItemsAsync(new StockItem(id));

				if (entity == null)
					return NotFound();

				DbContext.Remove(entity);

				await DbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				response.DidError = true;
				response.ErrorMessage = "There was an internal error, please contact to technical support.";

				Logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(DeleteStockItemAsync), ex);
			}

			return response.ToHttpResponse();
		}
	}
}