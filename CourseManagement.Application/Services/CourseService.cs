using CourseManagement.Application.Interfaces;
using CourseManagement.Core.Entities;
using CourseManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _repository;
        public CourseService(ICourseRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Course>> GetAllAsync()
        {
           return await _repository.GetAllAsync();
        }

        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<Course> CreateAsync(Course course)
        {
            await _repository.AddAsync(course);
            return course;
        }

        public async Task<bool> UpdateAsync(int id, Course updateCourse)
        {
            var existing =await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Title = updateCourse.Title;
            existing.Description = updateCourse.Description;
            existing.Duration = updateCourse.Duration;
            existing.Description = updateCourse.Description;

            await _repository.UpdateAsync(existing);
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
           var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }




    }
}
