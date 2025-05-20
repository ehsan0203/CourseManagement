using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Core.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";

        public string Instructor { get; set; } = "";
        public int Duration { get; set; } //به دقیقه

        public string Description { get; set; } = "";

    }
}
