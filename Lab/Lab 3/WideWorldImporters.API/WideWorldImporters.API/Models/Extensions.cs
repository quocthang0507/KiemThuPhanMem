using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace WideWorldImporters.API.Models
{
#pragma warning disable CS1591
	/// <summary>
	/// WideWorldImportersDbContextExtensions tìm kiếm stock item bằng LINQ, chỉ cần đưa các tham số tương ứng vào
	/// </summary>
	public static class WideWorldImportersDbContextExtensions
	{
		public static IQueryable<StockItem> GetStockItems(this WideWorldImportersDbContext dbContext, int pageSize = 10, int pageNumber = 1, int? lastEditedBy = null, int? colorID = null, int? outerPackageID = null, int? supplierID = null, int? unitPackageID = null)
		{
			var query = dbContext.StockItems.AsQueryable();

			// Lọc các stock item theo từng tham số
			if (lastEditedBy.HasValue)
				query = query.Where(item => item.LastEditedBy == lastEditedBy);

			if (colorID.HasValue)
				query = query.Where(item => item.ColorID == colorID);

			if (outerPackageID.HasValue)
				query = query.Where(item => item.OuterPackageID == outerPackageID);

			if (supplierID.HasValue)
				query = query.Where(item => item.SupplierID == supplierID);

			if (unitPackageID.HasValue)
				query = query.Where(item => item.UnitPackageID == unitPackageID);

			return query;
		}

		/// <summary>
		/// Tìm kiếm stock item theo id
		/// </summary>
		/// <param name="dbContext"></param>
		/// <param name="entity"></param>
		/// <returns></returns>
		public static async Task<StockItem> GetStockItemsAsync(this WideWorldImportersDbContext dbContext, StockItem entity) => await dbContext.StockItems.FirstOrDefaultAsync(item => item.StockItemID == entity.StockItemID);

		/// <summary>
		/// Tìm kiếm stock item theo các yêu cầu của test
		/// </summary>
		/// <param name="dbContext"></param>
		/// <param name="supplierID"></param>
		/// <param name="colorID"></param>
		/// <param name="unitPackageID"></param>
		/// <param name="outerPackageID"></param>
		/// <param name="lastEditedBy"></param>
		/// <returns></returns>
		public static async Task<StockItem> GetStockItemsAsync(this WideWorldImportersDbContext dbContext, int supplierID, int colorID, int unitPackageID, int outerPackageID, int lastEditedBy) => await dbContext.StockItems.FirstOrDefaultAsync(item =>
			item.SupplierID == supplierID && item.ColorID == colorID && item.UnitPackageID == unitPackageID && item.OuterPackageID == outerPackageID && item.LastEditedBy == lastEditedBy);

		/// <summary>
		/// Tìm kiếm stock item theo tên
		/// </summary>
		/// <param name="dbContext"></param>
		/// <param name="entity"></param>
		/// <returns></returns>
		public static async Task<StockItem> GetStockItemsByStockItemNameAsync(this WideWorldImportersDbContext dbContext, StockItem entity) => await dbContext.StockItems.FirstOrDefaultAsync(item => item.StockItemName == entity.StockItemName);
	}

	/// <summary>
	/// IQueryableExtensions chứa các phương thức mở rộng phân trang
	/// </summary>
	public static class IQueryableExtensions
	{
		public static IQueryable<TModel> Paging<TModel>(this IQueryable<TModel> query, int pageSize = 0, int pageNumber = 0) where TModel : class
			=> pageSize > 0 && pageNumber > 0 ? query.Skip((pageNumber - 1) * pageSize).Take(pageSize) : query;
	}
#pragma warning restore CS1591

}

