using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VladimirNyaninKT_31_22.Helpers;
using VladimirNyaninKT_31_22.Models;

namespace VladimirNyaninKT_31_22.Configurations
{
    public class WorkloadConfiguration : IEntityTypeConfiguration<Workload>
    {
        public const string TableName = "workloads";

        public const string PrimaryKeyName = $"pk_{TableName}_workload_id";

        public const string WorkloadIdColumn = "workload_id";
        public const string HoursQuantityColumn = "hours_quantity";
        public const string IsDeletedColumn = "is_deleted";

        public const string UniqueHoursQuantityIndexName = $"uk_{TableName}_hours_quantity";
        public const string IsDeletedIndexName = $"ix_{TableName}_is_deleted";


        public void Configure(EntityTypeBuilder<Workload> builder)
        {
            builder
               .ToTable(TableName);

            builder
                .HasKey(p => p.WorkloadId)
                .HasName(PrimaryKeyName);

            builder.Property(p => p.WorkloadId)
                .HasColumnName(WorkloadIdColumn)
                .ValueGeneratedOnAdd()
                .HasComment("Идентификатор загруженности");

            builder.Property(p => p.HoursQuantity)
                .IsRequired()
                .HasColumnName(HoursQuantityColumn)
                .HasColumnType(ColumnType.Int)
                .HasComment("Загруженность в часах");

            builder.Property(p => p.IsDeleted)
                .IsRequired()
                .HasColumnName(IsDeletedColumn)
                .HasColumnType(ColumnType.Bool)
                .HasDefaultValue(false)
                .HasComment("Признак удаления");



            builder.HasIndex(p => p.HoursQuantity)
               .IsUnique()
               .HasDatabaseName(UniqueHoursQuantityIndexName);

            builder.HasIndex(p => p.IsDeleted)
                .HasDatabaseName(IsDeletedIndexName);


            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
