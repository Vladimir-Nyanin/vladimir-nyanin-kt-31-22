using VladimirNyaninKT_31_22.Models;

namespace VladimirNyaninKT_31_22.Tests
{
    public class TeacherTest
    {
        [Fact]
        public void IsValidTeacherFullName_Васильев_Роман_Николаевич_True()
        {
            var testTeacher = new Teacher
            {
                LastName="Васильев",
                FirstName="Роман",
                Patronymic="Николаевич"
            };

            var result = testTeacher.isValidFullName();

            Assert.True(result);

        }
    }
}
