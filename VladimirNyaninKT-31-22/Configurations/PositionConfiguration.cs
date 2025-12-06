using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VladimirNyaninKT_31_22.Helpers;
using VladimirNyaninKT_31_22.Models;

namespace VladimirNyaninKT_31_22.Configurations
{
    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public const string TableName = "positions";

        public const string PrimaryKeyName = $"pk_{TableName}_position_id";

        public const string PositionIdColumn = "position_id";
        public const string NameColumn = "position_name";
        public const string IsDeletedColumn = "is_deleted";

        public const string UniqueNameIndexName = $"uk_{TableName}_position_name";
        public const string IsDeletedIndexName = $"ix_{TableName}_is_deleted";


        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder
               .ToTable(TableName);

            builder
                .HasKey(p => p.PositionId)
                .HasName(PrimaryKeyName);

            builder.Property(p => p.PositionId)
                .HasColumnName(PositionIdColumn)
                .ValueGeneratedOnAdd()
                .HasComment("Идентификатор должности");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnName(NameColumn)
                .HasColumnType(ColumnType.String).HasMaxLength(100)
                .HasComment("Название должности");

            builder.Property(p => p.IsDeleted)
                .IsRequired()
                .HasColumnName(IsDeletedColumn)
                .HasColumnType(ColumnType.Bool)
                .HasDefaultValue(false)
                .HasComment("Признак удаления");

            builder.HasIndex(p => p.Name)
               .IsUnique()
               .HasDatabaseName(UniqueNameIndexName);

            builder.HasIndex(p => p.IsDeleted)
                .HasDatabaseName(IsDeletedIndexName);


            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
