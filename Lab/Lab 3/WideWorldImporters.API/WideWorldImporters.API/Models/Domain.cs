using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace WideWorldImporters.API.Models
{
#pragma warning disable CS1591

	/// <summary>
	/// Lớp ảnh xạ bảng Warehouse.StockItems
	/// </summary>
	public partial class StockItem
	{
		public int? StockItemID { get; set; }

		public string StockItemName { get; set; }

		public int? SupplierID { get; set; }

		public int? ColorID { get; set; }

		public int? UnitPackageID { get; set; }

		public int? OuterPackageID { get; set; }

		public string Brand { get; set; }

		public string Size { get; set; }

		public int? LeadTimeDays { get; set; }

		public int? QuantityPerOuter { get; set; }

		public bool? IsChillerStock { get; set; }

		public string Barcode { get; set; }

		public decimal? TaxRate { get; set; }

		public decimal? UnitPrice { get; set; }

		public decimal? RecommendedRetailPrice { get; set; }

		public decimal? TypicalWeightPerUnit { get; set; }

		public string MarketingComments { get; set; }

		public string InternalComments { get; set; }

		public string CustomFields { get; set; }

		public string Tags { get; set; }

		public string SearchDetails { get; set; }

		public int? LastEditedBy { get; set; }

		public DateTime? ValidFrom { get; set; }

		public DateTime? ValidTo { get; set; }

		public StockItem()
		{

		}

		public StockItem(int? stockItemID)
		{
			StockItemID = stockItemID;
		}
	}

	/// <summary>
	/// Lớp StockItemsConfiguration ánh xạ các kiểu dữ liệu của các cột trong SQL với kiểu dữ liệu của từng thuộc tính 
	/// </summary>
	public class StockItemsConfiguration : IEntityTypeConfiguration<StockItem>
	{
		public void Configure(EntityTypeBuilder<StockItem> builder)
		{
			builder.ToTable("StockItems", "Warehouse");

			builder.HasKey(p => p.StockItemID);

			builder.Property(p => p.StockItemName).HasColumnType("nvarchar(200)").IsRequired();
			builder.Property(p => p.SupplierID).HasColumnType("int").IsRequired();
			builder.Property(p => p.ColorID).HasColumnType("int");
			builder.Property(p => p.UnitPackageID).HasColumnType("int").IsRequired();
			builder.Property(p => p.OuterPackageID).HasColumnType("int").IsRequired();
			builder.Property(p => p.Brand).HasColumnType("nvarchar(100)");
			builder.Property(p => p.Size).HasColumnType("nvarchar(40)");
			builder.Property(p => p.LeadTimeDays).HasColumnType("int").IsRequired();
			builder.Property(p => p.QuantityPerOuter).HasColumnType("int").IsRequired();
			builder.Property(p => p.IsChillerStock).HasColumnType("bit").IsRequired();
			builder.Property(p => p.Barcode).HasColumnType("nvarchar(100)");
			builder.Property(p => p.TaxRate).HasColumnType("decimal(18, 3)").IsRequired();
			builder.Property(p => p.UnitPrice).HasColumnType("decimal(18, 2)").IsRequired();
			builder.Property(p => p.RecommendedRetailPrice).HasColumnType("decimal(18, 2)");
			builder.Property(p => p.TypicalWeightPerUnit).HasColumnType("decimal(18, 3)").IsRequired();
			builder.Property(p => p.MarketingComments).HasColumnType("nvarchar(max)");
			builder.Property(p => p.InternalComments).HasColumnType("nvarchar(max)");
			builder.Property(p => p.CustomFields).HasColumnType("nvarchar(max)");
			builder.Property(p => p.LastEditedBy).HasColumnType("int").IsRequired();
			builder.Property(p => p.StockItemID).HasColumnType("int").IsRequired().HasDefaultValueSql("NEXT VALUE FOR [Sequences].[StockItemID]"); // Giá trị mặc định, số tiếp theo của chuỗi id
			builder.Property(p => p.Tags).HasColumnType("nvarchar(max)").HasComputedColumnSql("json_query([CustomFields],N'$.Tags')"); // Chuyển CustomFields sang JSON
			builder.Property(p => p.SearchDetails).HasColumnType("nvarchar(max)").IsRequired().HasComputedColumnSql("concat([StockItemName],N' ',[MarketingComments])"); // Nối tên và các chữ tiếp thị lại
			builder.Property(p => p.ValidFrom).HasColumnType("datetime2").IsRequired().ValueGeneratedOnAddOrUpdate(); // Ngày hiện tại tạo stock item đó
			builder.Property(p => p.ValidTo).HasColumnType("datetime2").IsRequired().ValueGeneratedOnAddOrUpdate();
		}
	}

	/// <summary>
	/// Lớp WideWorldImportersDbContext nối C# với SQL, do đó nó xử lý các truy vấn và lưu thay đổi vào CSDL
	/// </summary>
	public class WideWorldImportersDbContext : DbContext
	{
		public WideWorldImportersDbContext(DbContextOptions<WideWorldImportersDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new StockItemsConfiguration());

			base.OnModelCreating(modelBuilder);
		}

		public DbSet<StockItem> StockItems { get; set; }

	}
#pragma warning restore CS1591

}
