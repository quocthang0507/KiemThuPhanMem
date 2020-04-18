using Microsoft.EntityFrameworkCore;
using WideWorldImporters.API.Models;

namespace WideWorldImporters.API.UnitTests
{
	/// <summary>
	/// DbContextMocker creates an instance of WideWorldImportersDbContext using in memory database, the dbName parameter sets the name 
	/// for in memory database; then there is an invocation for Seed method, this method adds entities for WideWorldImportersDbContext 
	/// instance in order to provide results.
	/// </summary>
	public static class DbContextMocker
	{
		public static WideWorldImportersDbContext GetWideWorldImportersDbContext(string dbName)
		{
			var options = new DbContextOptionsBuilder<WideWorldImportersDbContext>().UseInMemoryDatabase(databaseName: dbName).Options;

			var dbContext = new WideWorldImportersDbContext(options);

			dbContext.Seed();

			return dbContext;
		}
	}
}
