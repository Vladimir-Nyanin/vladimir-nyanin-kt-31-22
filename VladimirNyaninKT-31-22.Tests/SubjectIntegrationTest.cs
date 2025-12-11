using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VladimirNyaninKT_31_22.Database;
using VladimirNyaninKT_31_22.Interfaces;
using VladimirNyaninKT_31_22.Models;


namespace VladimirNyaninKT_31_22.Tests
{
    public class SubjectIntegrationTest
    {
        public readonly DbContextOptions<UniversityDbContext> _dbContextOptions;

        public SubjectIntegrationTest()
        {
            _dbContextOptions = new DbContextOptionsBuilder<UniversityDbContext>()
                .UseInMemoryDatabase(databaseName: "subject_db")
                .Options;
        }

        [Fact]
        public async Task GetSubjectsByTeacherId_2_TwoObjects()
        {
            var ctx = new UniversityDbContext(_dbContextOptions);

            ctx.Database.EnsureDeleted();
            await ctx.Database.EnsureCreatedAsync();

            var subjectService = new SubjectService(ctx);



            var departments = new List<Department>
            {
                new Department
                {
                    DepartmentId = 1,
                    Name = "Кафедра математики",
                    IsDeleted = false,
                }           
            };
            await ctx.Set<Department>().AddRangeAsync(departments);
            await ctx.SaveChangesAsync();


            var degrees = new List<Degree>
            {
                new Degree
                {
                    DegreeId = 1,
                    Name = "Кандидат наук",
                    IsDeleted = false,
                },
                 new Degree
                {
                    DegreeId = 2,
                    Name = "Доктор наук",
                    IsDeleted = false,
                }
            };
            await ctx.Set<Degree>().AddRangeAsync(degrees);
            await ctx.SaveChangesAsync();

            var positions = new List<Position>
            {
                new Position
                {
                    PositionId = 3,
                    Name = "Доцент",
                    IsDeleted = false,
                },
                 new Position
                {
                    PositionId = 4,
                    Name = "Профессор",
                    IsDeleted = false,
                }
            };
            await ctx.Set<Position>().AddRangeAsync(positions);
            await ctx.SaveChangesAsync();

            var teachers = new List<Teacher>
            {
                new Teacher
                {
                    TeacherId = 1,
                    LastName = "Купцов",
                    FirstName = "Владислав",
                    Patronymic = "Петрович",
                    IsHead = true,
                    IsDeleted = false,
                    DegreeId = 2,
                    PositionId = 4,
                    DepartmentId = 1
                },
                new Teacher
                {
                    TeacherId = 2,
                    LastName = "Галкина",
                    FirstName = "Светлана",
                    Patronymic = "Васильевна",
                    IsHead = false,
                    IsDeleted = false,
                    DegreeId = 1,
                    PositionId = 3,
                    DepartmentId = 1
                }
            };
            await ctx.Set<Teacher>().AddRangeAsync(teachers);
            await ctx.SaveChangesAsync();




            var workloads = new List<Workload>
            {
                new Workload
                {
                    WorkloadId = 2,
                    HoursQuantity = 26,
                    IsDeleted = false,
                },
                new Workload
                {
                    WorkloadId = 3,
                    HoursQuantity = 28,
                    IsDeleted = false,
                },
                new Workload
                {
                    WorkloadId = 5,
                    HoursQuantity = 32,
                    IsDeleted = false,
                },
            };
            await ctx.Set<Workload>().AddRangeAsync(workloads);
            await ctx.SaveChangesAsync();


            var subjects = new List<Subject>
            {
                new Subject
                {
                    SubjectId = 1,
                    Name = "Линейная алгебра",
                    IsDeleted = false,
                    TeacherId = 1,
                    WorkloadId = 5
                },
                new Subject
                {
                    SubjectId = 2,
                    Name = "Аналитическая геометрия",
                    IsDeleted = false,
                    TeacherId = 2,
                    WorkloadId = 3
                },
                new Subject
                {
                    SubjectId = 3,
                    Name = "Математическая Геометрия",
                    IsDeleted = false,
                    TeacherId = 2,
                    WorkloadId = 2
                }
            };
            await ctx.Set<Subject>().AddRangeAsync(subjects);
            await ctx.SaveChangesAsync();



            // Act
            var filter = new Filters.SubjectFilters.GetSubjectsByTeacherFilter
            {
                TeacherId = 2
            };
            var subjectsResult = await subjectService.GetSubjectsByTeacherAsync(filter, CancellationToken.None);

            // Assert
            Assert.Equal(2, subjectsResult.Length);
        }


    }

}