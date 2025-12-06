using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VladimirNyaninKT_31_22.Helpers;
using VladimirNyaninKT_31_22.Models;

namespace VladimirNyaninKT_31_22.Configurations
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public const string TableName = "teachers";

        public const string PrimaryKeyName = $"pk_{TableName}_teacher_id";
        public const string DegreeForeignKeyName = $"fk_{TableName}_degree_id";
        public const string PositionForeignKeyName = $"fk_{TableName}_position_id";
        public const string DepartmentForeignKeyName = $"fk_{TableName}_department_id";

        public const string TeacherIdColumn = "teacher_id";
        public const string LastNameColumn = "teacher_last_name";
        public const string FirstNameColumn = "teacher_first_name";
        public const string PatronymicColumn = "teacher_patronymic";
        public const string IsHeadColumn = "is_head";
        public const string IsDeletedColumn = "is_deleted";

        public const string LastNameIndexName = $"ix_{TableName}_teacher_last_name";
        public const string FirstNameIndexName = $"ix_{TableName}_teacher_first_name";
        public const string PatronymicIndexName = $"ix_{TableName}_teacher_patronymic";
        public const string IsHeadIndexName = $"ix_{TableName}_is_head";
        public const string IsDeletedIndexName = $"ix_{TableName}_is_deleted";


        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder
                .ToTable(TableName);
                 
            builder
                .HasKey(p => p.TeacherId)
                .HasName(PrimaryKeyName);

            builder
              .HasOne(p => p.Degree)
              .WithMany()
              .HasForeignKey(p => p.DegreeId)
              .HasConstraintName(DegreeForeignKeyName)
              .OnDelete(DeleteBehavior.Cascade);

            builder
              .HasOne(p => p.Position)
              .WithMany()
              .HasForeignKey(p => p.PositionId)
              .HasConstraintName(PositionForeignKeyName)
              .OnDelete(DeleteBehavior.Cascade);

            builder
              .HasOne(p => p.Department)
              .WithMany()
              .HasForeignKey(p => p.DepartmentId)
              .HasConstraintName(DepartmentForeignKeyName)
              .OnDelete(DeleteBehavior.Cascade);




            builder.Property(p => p.TeacherId)
                .HasColumnName(TeacherIdColumn)
                .ValueGeneratedOnAdd()
                .HasComment("Идентификатор преподавателя");



            builder.Property(p => p.LastName)
                .IsRequired()
                .HasColumnName(LastNameColumn)
                .HasColumnType(ColumnType.String).HasMaxLength(100)
                .HasComment("Фамилия преподавателя");

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasColumnName(FirstNameColumn)
                .HasColumnType(ColumnType.String).HasMaxLength(100)
                .HasComment("Имя преподавателя");

            builder.Property(p => p.Patronymic)
                .IsRequired()
                .HasColumnName(PatronymicColumn)
                .HasColumnType(ColumnType.String).HasMaxLength(100)
                .HasComment("Отчество преподавателя");

            builder.Property(p => p.IsHead)
                .IsRequired()
                .HasColumnName(IsHeadColumn)
                .HasColumnType(ColumnType.Bool)
                .HasDefaultValue(false)
                .HasComment("Признак заведующего кафедрой");

            builder.Property(p => p.IsDeleted)
                .IsRequired()
                .HasColumnName(IsDeletedColumn)
                .HasColumnType(ColumnType.Bool)
                .HasDefaultValue(false)
                .HasComment("Признак удаления");







            builder.HasIndex(p => p.LastName)       
               .HasDatabaseName(LastNameIndexName);

            builder.HasIndex(p => p.FirstName)
              .HasDatabaseName(FirstNameIndexName);

            builder.HasIndex(p => p.Patronymic)
              .HasDatabaseName(PatronymicIndexName);

            builder.HasIndex(p => p.IsHead)
                .HasDatabaseName(IsHeadIndexName);

            builder.HasIndex(p => p.IsDeleted)
              .HasDatabaseName(IsDeletedIndexName);

            builder.HasQueryFilter(p => !p.IsDeleted);




            builder.Navigation(p => p.Degree)
                .AutoInclude();

            builder.Navigation(p => p.Position)
                .AutoInclude();

            builder.Navigation(p => p.Department)
                .AutoInclude();
        }
    }
}
