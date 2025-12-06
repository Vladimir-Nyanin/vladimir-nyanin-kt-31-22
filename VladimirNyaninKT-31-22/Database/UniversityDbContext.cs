using Microsoft.EntityFrameworkCore;
using VladimirNyaninKT_31_22.Models;
using VladimirNyaninKT_31_22.Configurations;

namespace VladimirNyaninKT_31_22.Database
{
    public class UniversityDbContext: DbContext
    {
        DbSet<Degree> Degrees { get; set; }
        DbSet<Department> Departments { get; set; }
        DbSet<Position> Positions { get; set; }
        DbSet<Subject> Subjects { get; set; }
        DbSet<Teacher> Teacher { get; set; }
        DbSet<Workload> Workloads { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DegreeConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new PositionConfiguration());
            modelBuilder.ApplyConfiguration(new SubjectConfiguration());
            modelBuilder.ApplyConfiguration(new TeacherConfiguration());
            modelBuilder.ApplyConfiguration(new WorkloadConfiguration());
        }


        public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options)
        { 
        }
    }
}
