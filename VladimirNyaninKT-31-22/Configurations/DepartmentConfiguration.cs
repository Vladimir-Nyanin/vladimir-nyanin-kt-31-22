using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VladimirNyaninKT_31_22.Models;
using VladimirNyaninKT_31_22.Helpers;



namespace VladimirNyaninKT_31_22.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public const string TableName = "departments";

        public const string PrimaryKeyName = $"pk_{TableName}_department_id";

        public const string DepartmentIdColumn = "department_id";
        public const string NameColumn = "department_name";
        public const string IsDeletedColumn = "is_deleted";
  
        public const string UniqueNameIndexName = $"uk_{TableName}_department_name";
        public const string IsDeletedIndexName = $"ix_{TableName}_is_deleted";


        public void Configure(EntityTypeBuilder<Department> builder)
        {


            builder
                .ToTable(TableName);

            builder
               .HasKey(p => p.DepartmentId)
               .HasName(PrimaryKeyName);


            builder.Property(p => p.DepartmentId)
                .HasColumnName(DepartmentIdColumn)
                .ValueGeneratedOnAdd()
                .HasComment("Идентификатор кафедры");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnName(NameColumn)
                .HasColumnType(ColumnType.String).HasMaxLength(100)
                .HasComment("Название кафедры");

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
