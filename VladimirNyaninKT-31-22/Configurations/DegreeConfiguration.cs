using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VladimirNyaninKT_31_22.Helpers;
using VladimirNyaninKT_31_22.Models;

namespace VladimirNyaninKT_31_22.Configurations
{
    public class DegreeConfiguration : IEntityTypeConfiguration<Degree>
    {
        public const string TableName = "degrees";

        public const string PrimaryKeyName = $"pk_{TableName}_degree_id";

        public const string DegreeIdColumn = "degree_id";
        public const string NameColumn = "degree_name";
        public const string IsDeletedColumn = "is_deleted";

        public const string UniqueNameIndexName = $"uk_{TableName}_degree_name";
        public const string IsDeletedIndexName = $"ix_{TableName}_is_deleted";


        public void Configure(EntityTypeBuilder<Degree> builder)
        {
            builder
               .ToTable(TableName);

            builder
                .HasKey(p => p.DegreeId)
                .HasName(PrimaryKeyName);

            builder.Property(p => p.DegreeId)
                .HasColumnName(DegreeIdColumn)
                .ValueGeneratedOnAdd()
                .HasComment("Идентификатор учёной степени");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnName(NameColumn)
                .HasColumnType(ColumnType.String).HasMaxLength(100)
                .HasComment("Название учёной степени");

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
