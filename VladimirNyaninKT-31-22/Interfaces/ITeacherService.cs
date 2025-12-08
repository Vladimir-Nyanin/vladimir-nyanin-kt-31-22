using Microsoft.EntityFrameworkCore;
using VladimirNyaninKT_31_22.Database;
using VladimirNyaninKT_31_22.Filters.SubjectFilters;
using VladimirNyaninKT_31_22.Filters.TeacherFilters;
using VladimirNyaninKT_31_22.Models;

namespace VladimirNyaninKT_31_22.Interfaces
{
    public interface ITeacherService
    {
        
        public Task<Teacher> AddTeacherAsync(AddTeacherFilter filter, CancellationToken cancellationToken);
        public Task<Teacher> UpdateTeacherAsync(UpdateTeacherFilter filter, CancellationToken cancellationToken);

        public Task<bool> DeleteTeacherAsync(DeleteTeacherFilter filter, CancellationToken cancellationToken);


    }
    public class TeacherService : ITeacherService
    {
        private readonly UniversityDbContext _dbContext;
        public TeacherService(UniversityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    
        public async Task<Teacher> AddTeacherAsync(AddTeacherFilter filter, CancellationToken cancellationToken = default)
        {          
            if (string.IsNullOrWhiteSpace(filter.LastName))
                throw new ArgumentException("Фамилия обязательна для заполнения");

            if (string.IsNullOrWhiteSpace(filter.FirstName))
                throw new ArgumentException("Имя обязательно для заполнения");

            if (string.IsNullOrWhiteSpace(filter.Patronymic))
                throw new ArgumentException("Отчетство обязательно для заполнения");


       
            var degree = await _dbContext.Set<Degree>()
                .FirstOrDefaultAsync(d => d.DegreeId == filter.DegreeId, cancellationToken);

            if (degree == null)
                throw new ArgumentException($"Степень с ID {filter.DegreeId} не найдена");

            var position = await _dbContext.Set<Position>()
                .FirstOrDefaultAsync(p => p.PositionId == filter.PositionId, cancellationToken);

            if (position == null)
                throw new ArgumentException($"Должность с ID {filter.PositionId} не найдена");

            var department = await _dbContext.Set<Department>()
                .FirstOrDefaultAsync(d => d.DepartmentId == filter.DepartmentId, cancellationToken);

            if (department == null)
                throw new ArgumentException($"Кафедра с ID {filter.DepartmentId} не найдена");

       
            var teacher = new Teacher
            {
                LastName = filter.LastName,
                FirstName = filter.FirstName,
                Patronymic = filter.Patronymic,
                IsHead = filter.IsHead,
                DegreeId = filter.DegreeId,
                PositionId = filter.PositionId,
                DepartmentId = filter.DepartmentId
            };

         
            await _dbContext.Set<Teacher>().AddAsync(teacher, cancellationToken);
          
            await _dbContext.SaveChangesAsync(cancellationToken);


            await _dbContext.Entry(teacher)
               .Reference(t => t.Degree)
               .LoadAsync(cancellationToken);

            await _dbContext.Entry(teacher)
                .Reference(t => t.Position)
                .LoadAsync(cancellationToken);

            await _dbContext.Entry(teacher)
                .Reference(t => t.Department)
                .LoadAsync(cancellationToken);

            return teacher;
        }


        public async Task<Teacher> UpdateTeacherAsync(UpdateTeacherFilter filter, CancellationToken cancellationToken = default)
        {      
            if (filter.TeacherId <= 0)
                throw new ArgumentException("ID преподавателя обязателен для заполнения");

            if (string.IsNullOrWhiteSpace(filter.LastName))
                throw new ArgumentException("Фамилия обязательна для заполнения");

            if (string.IsNullOrWhiteSpace(filter.FirstName))
                throw new ArgumentException("Имя обязательно для заполнения");

            if (string.IsNullOrWhiteSpace(filter.Patronymic))
                throw new ArgumentException("Отчество обязательно для заполнения");

        
            var teacher = await _dbContext.Set<Teacher>()
                .FirstOrDefaultAsync(t => t.TeacherId == filter.TeacherId, cancellationToken);

            if (teacher == null)
                throw new ArgumentException($"Преподаватель с ID {filter.TeacherId} не найден");

       
            teacher.LastName = filter.LastName;
            teacher.FirstName = filter.FirstName;
            teacher.Patronymic = filter.Patronymic;
            teacher.IsHead = filter.IsHead;
            teacher.DegreeId = filter.DegreeId;
            teacher.PositionId = filter.PositionId;
            teacher.DepartmentId = filter.DepartmentId;

         
            await _dbContext.SaveChangesAsync(cancellationToken);


            await _dbContext.Entry(teacher)
               .Reference(t => t.Degree)
               .LoadAsync(cancellationToken);

            await _dbContext.Entry(teacher)
                .Reference(t => t.Position)
                .LoadAsync(cancellationToken);

            await _dbContext.Entry(teacher)
                .Reference(t => t.Department)
                .LoadAsync(cancellationToken);

            return teacher;
        }


        public async Task<bool> DeleteTeacherAsync(DeleteTeacherFilter filter, CancellationToken cancellationToken = default)
        {
            if (filter.TeacherId <= 0)
                throw new ArgumentException("ID преподавателя обязателен для заполнения");

            var teacher = await _dbContext.Set<Teacher>()
                .FirstOrDefaultAsync(t => t.TeacherId == filter.TeacherId && !t.IsDeleted, cancellationToken);

            if (teacher == null)
                throw new ArgumentException($"Преподаватель с ID {filter.TeacherId} не найден или уже удален");
        
            teacher.IsDeleted = true;
                 
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
