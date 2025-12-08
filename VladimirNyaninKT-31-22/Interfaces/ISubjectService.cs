using Microsoft.EntityFrameworkCore;
using VladimirNyaninKT_31_22.Models;
using VladimirNyaninKT_31_22.Database;
using VladimirNyaninKT_31_22.Filters.SubjectFilters;


namespace VladimirNyaninKT_31_22.Interfaces

{
    public interface ISubjectService
    {
        public Task<Subject[]> GetSubjectsByTeacherAsync(GetSubjectsByTeacherFilter filter, CancellationToken cancellationToken);
        public Task<Subject[]> GetSubjectsByHoursQuantityAsync(GetSubjectsByHoursQuantityFilter filter, CancellationToken cancellationToken);
        public Task<string[]> GetSubjectsByHeadAsync(GetSubjectsByHeadFilter filter, CancellationToken cancellationToken);

        //public Task<Subject[]> GetSubjectsByHeadAsync(SubjectsByHeadFilter filter, CancellationToken cancellationToken);

    }

    public class SubjectService : ISubjectService
    {
        private readonly UniversityDbContext _dbContext;
        public SubjectService(UniversityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Subject[]> GetSubjectsByTeacherAsync(GetSubjectsByTeacherFilter filter, CancellationToken cancellationToken = default)
        {
            var subjects = _dbContext.Set<Subject>()
                .Where(p => p.Teacher.TeacherId == filter.TeacherId)
                .ToArrayAsync(cancellationToken);

            return subjects;
        }

        public Task<Subject[]> GetSubjectsByHoursQuantityAsync(GetSubjectsByHoursQuantityFilter filter, CancellationToken cancellationToken = default)
        {
            var subjects = _dbContext.Set<Subject>()
                .Where(p => (p.Workload.HoursQuantity >= filter.MinHoursQuantity) && (p.Workload.HoursQuantity <= filter.MaxHoursQuantity))
                .ToArrayAsync(cancellationToken);

            return subjects;
        }

        public async Task<string[]> GetSubjectsByHeadAsync(GetSubjectsByHeadFilter filter, CancellationToken cancellationToken = default)
        {
            var departmentId = await _dbContext.Set<Teacher>()
                .Where(t => t.IsHead == true && t.LastName == filter.LastName)
                .Select(t => t.DepartmentId).FirstOrDefaultAsync(cancellationToken);



            var teachersOfDepartment = _dbContext.Set<Teacher>()
                .Where(t => t.DepartmentId == departmentId)
                .Select(t => t.TeacherId);



            var subjects = await _dbContext.Set<Subject>()            
                .Where(s => teachersOfDepartment.Contains(s.TeacherId))
                .Select(s => s.Name)
                .Distinct()
                .ToArrayAsync(cancellationToken);

            return subjects;



        }





        //public async Task<Subject[]> GetSubjectsByHeadAsync(SubjectsByHeadFilter filter, CancellationToken cancellationToken = default)
        //{
        //    var departmentId = await _dbContext.Set<Teacher>()
        //        .Where(t => t.IsHead == true && t.LastName == filter.LastName)
        //        .Select(t => t.DepartmentId).FirstOrDefaultAsync(cancellationToken);



        //    var teachersOfDepartment = _dbContext.Set<Teacher>()
        //        .Where(t => t.DepartmentId == departmentId)
        //        .Select(t => t.TeacherId);



        //    var subjects = await _dbContext.Set<Subject>()
        //        .Include(s => s.Teacher)
        //        .Include(s => s.Workload)
        //        .Where(s => teachersOfDepartment.Contains(s.TeacherId))             
        //        .ToArrayAsync(cancellationToken);

        //    return subjects;
        //}
    }


}
