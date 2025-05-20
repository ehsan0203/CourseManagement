using CourseManagement.Application.Interfaces;
using CourseManagement.Core.Entities;
using CourseManagement.Core.Interfaces;
using CourseManagement.WebAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagement.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _service;

        public CourseController(ICourseService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var course= await _service.GetByIdAsync(id);
            return course == null ? NotFound() : Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCourseDto dto)
        {
            var course = new Course
            {
                Title = dto.Title,
                Instructor = dto.Instructor,
                Duration = dto.Duration,
                Description = dto.Description,
            };

            var created =await _service.CreateAsync(course);
            return CreatedAtAction(nameof(Get), new { id = course.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateCourseDto dto)
        {
            var update = new Course
            {
                Title = dto.Title,
                Instructor = dto.Instructor,
                Duration = dto.Duration,
                Description= dto.Description,
            };

            var success = await _service.UpdateAsync(id, update);

            return success ? NoContent() : NotFound();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success= await _service.DeleteAsync(id);
            return success ? NoContent(): NotFound();
        }
    }
}
