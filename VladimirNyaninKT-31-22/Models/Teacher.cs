using System.Text.RegularExpressions;

namespace VladimirNyaninKT_31_22.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public bool IsHead { get; set; }
        public bool IsDeleted { get; set; }


        public int DegreeId { get; set; }
        public int PositionId { get; set; }
        public int DepartmentId { get; set; }




        public Degree Degree { get; set; }
        public Position Position { get; set; }
        public Department Department { get; set; }


        public bool isValidFullName()
        {
            return Regex.Match(LastName, @"[а-яА-Я]+").Success &&
                   Regex.Match(FirstName, @"[а-яА-Я]+").Success &&
                   Regex.Match(Patronymic, @"[а-яА-Я]+").Success;
        }

    }
}
