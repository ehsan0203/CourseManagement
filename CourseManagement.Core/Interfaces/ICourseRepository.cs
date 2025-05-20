using CourseManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Core.Interfaces
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetAllAsync();

        Task<Course?> GetByIdAsync(int id);

        Task AddAsync(Course course);

        Task UpdateAsync(Course course);

        Task DeleteAsync(int id);

    }
}
