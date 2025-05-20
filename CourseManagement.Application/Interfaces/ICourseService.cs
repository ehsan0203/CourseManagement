using CourseManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Application.Interfaces
{
    public interface ICourseService
    {
        Task<List<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync(int id);

        Task<Course> CreateAsync(Course course);

        Task<bool> UpdateAsync(int id,Course updateCourse);
        Task<bool> DeleteAsync(int id);
    }
}
