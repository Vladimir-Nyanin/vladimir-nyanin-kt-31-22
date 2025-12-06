using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VladimirNyaninKT_31_22.Helpers;
using VladimirNyaninKT_31_22.Models;

namespace VladimirNyaninKT_31_22.Configurations
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public const string TableName = "subjects";

        public const string PrimaryKeyName = $"pk_{TableName}_subject_id";
        public const string TeacherForeignKeyName = $"fk_{TableName}_teacher_id";
        public const string WorkloadForeignKeyName = $"fk_{TableName}_workload_id";

        public const string SubjectIdColumn = "subject_id";
        public const string NameColumn = "subject_name";
        public const string IsDeletedColumn = "is_deleted";

        public const string UniqueNameIndexName = $"ix_{TableName}_subject_name";
        public const string IsDeletedIndexName = $"ix_{TableName}_is_deleted";


        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder
                .ToTable(TableName);

            builder
                .HasKey(p => p.SubjectId)
                .HasName(PrimaryKeyName);


            builder
              .HasOne(p => p.Teacher)
              .WithMany()
              .HasForeignKey(p => p.TeacherId)
              .HasConstraintName(TeacherForeignKeyName)
              .OnDelete(DeleteBehavior.Cascade);

            builder
              .HasOne(p => p.Workload)
              .WithMany()
              .HasForeignKey(p => p.WorkloadId)
              .HasConstraintName(WorkloadForeignKeyName)
              .OnDelete(DeleteBehavior.Cascade);

        

            builder.Property(p => p.SubjectId)
                .HasColumnName(SubjectIdColumn)
                .ValueGeneratedOnAdd()
                .HasComment("Идентификатор дисциплины");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnName(NameColumn)
                .HasColumnType(ColumnType.String).HasMaxLength(100)
                .HasComment("Название дисциплины");
   
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


            builder.Navigation(p => p.Teacher)
                .AutoInclude();

            builder.Navigation(p => p.Workload)
                .AutoInclude();
     
        }

    }
}
