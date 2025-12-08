using Microsoft.AspNetCore.Mvc;
using VladimirNyaninKT_31_22.Filters.SubjectFilters;
using VladimirNyaninKT_31_22.Interfaces;



namespace VladimirNyaninKT_31_22.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class SubjectsController : ControllerBase
    {
        private readonly ILogger<SubjectsController> _logger;
        private readonly ISubjectService _subjectService;

        public SubjectsController(ILogger<SubjectsController> logger, ISubjectService subjectService)
        {
            _logger = logger;
            _subjectService = subjectService;
        }

        [HttpPost("get-by-teacher")]
        public async Task<IActionResult> GetSubjectsByTeacherAsync(GetSubjectsByTeacherFilter filter, CancellationToken cancellationToken = default!)
        {
            var subjects = await _subjectService.GetSubjectsByTeacherAsync(filter, cancellationToken);

            return Ok(subjects);
        }

        [HttpPost("get-by-hours")]
        public async Task<IActionResult> GetSubjectsByHoursQuantityAsync(GetSubjectsByHoursQuantityFilter filter, CancellationToken cancellationToken = default!)
        {
            var subjects = await _subjectService.GetSubjectsByHoursQuantityAsync(filter, cancellationToken);

            return Ok(subjects);
        }

        [HttpPost("get-by-head")]
        public async Task<IActionResult> GetSubjectsByHeadAsync(GetSubjectsByHeadFilter filter, CancellationToken cancellationToken = default!)
        {
            var subjects = await _subjectService.GetSubjectsByHeadAsync(filter, cancellationToken);

            return Ok(subjects);
        }






    }
}
