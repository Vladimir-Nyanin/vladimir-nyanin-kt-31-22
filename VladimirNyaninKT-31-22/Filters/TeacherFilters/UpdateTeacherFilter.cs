using System.ComponentModel;

namespace VladimirNyaninKT_31_22.Filters.TeacherFilters
{
    public class UpdateTeacherFilter
    {
        public int TeacherId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }

        [DefaultValue(false)]
        public bool IsHead { get; set; }
        public int DegreeId { get; set; }
        public int PositionId { get; set; }
        public int DepartmentId { get; set; }
    }
}
