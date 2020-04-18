using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace WideWorldImporters.API.Models
{
#pragma warning disable CS1591
	/// <summary>
	/// WideWorldImportersDbContextExtensions contains extension methods to provide LINQ queries
	/// </summary>
	public static class WideWorldImportersDbContextExtensions
	{
		public static IQueryable<StockItem> GetStockItems(this WideWorldImportersDbContext dbContext, int pageSize = 10, int pageNumber = 1, int? lastEditedBy = null, int? colorID = null, int? outerPackageID = null, int? supplierID = null, int? unitPackageID = null)
		{
			var query = dbContext.StockItems.AsQueryable();

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

		public static async Task<StockItem> GetStockItemsAsync(this WideWorldImportersDbContext dbContext, StockItem entity) => await dbContext.StockItems.FirstOrDefaultAsync(item => item.StockItemID == entity.StockItemID);

		/// <summary>
		/// La Quoc Thang defined
		/// </summary>
		public static async Task<StockItem> GetStockItemsAsync(this WideWorldImportersDbContext dbContext, int supplierID, int colorID, int unitPackageID, int outerPackageID, int lastEditedBy) => await dbContext.StockItems.FirstOrDefaultAsync(item =>
			item.SupplierID == supplierID && item.ColorID == colorID && item.UnitPackageID == unitPackageID && item.OuterPackageID == outerPackageID && item.LastEditedBy == lastEditedBy);

		public static async Task<StockItem> GetStockItemsByStockItemNameAsync(this WideWorldImportersDbContext dbContext, StockItem entity) => await dbContext.StockItems.FirstOrDefaultAsync(item => item.StockItemName == entity.StockItemName);
	}

	/// <summary>
	/// IQueryableExtensions contains extension methods to allow paging in IQueryable instances
	/// </summary>
	public static class IQueryableExtensions
	{
		public static IQueryable<TModel> Paging<TModel>(this IQueryable<TModel> query, int pageSize = 0, int pageNumber = 0) where TModel : class
			=> pageSize > 0 && pageNumber > 0 ? query.Skip((pageNumber - 1) * pageSize).Take(pageSize) : query;
	}
#pragma warning restore CS1591

}

