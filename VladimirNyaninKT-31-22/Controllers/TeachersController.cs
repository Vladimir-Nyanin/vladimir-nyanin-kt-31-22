using Microsoft.AspNetCore.Mvc;
using VladimirNyaninKT_31_22.Filters.TeacherFilters;
using VladimirNyaninKT_31_22.Interfaces;


namespace VladimirNyaninKT_31_22.Controllers
{
    [ApiController]
    [Route("[controller]")]
  
    
    public class TeachersController : ControllerBase
    {
        private readonly ILogger<TeachersController> _logger;
        private readonly ITeacherService _teacherService;

        public TeachersController(ILogger<TeachersController> logger, ITeacherService teacherService)
        {
            _logger = logger;
            _teacherService = teacherService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddTeacherAsync(AddTeacherFilter filter, CancellationToken cancellationToken = default!)
        {
            var teacher = await _teacherService.AddTeacherAsync(filter, cancellationToken);

            return Ok(teacher);
        }


        [HttpPost("update")] 
        public async Task<IActionResult> UpdateTeacherAsync(UpdateTeacherFilter filter, CancellationToken cancellationToken = default!)
        {
            var teacher = await _teacherService.UpdateTeacherAsync(filter, cancellationToken);

            return Ok(teacher);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteTeacherAsync(DeleteTeacherFilter filter, CancellationToken cancellationToken = default!)
        {
            var teacher = await _teacherService.DeleteTeacherAsync(filter, cancellationToken);

            return Ok(teacher);
        }


    }
    
}
