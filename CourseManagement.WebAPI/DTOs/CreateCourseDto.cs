namespace CourseManagement.WebAPI.DTOs
{
    public class CreateCourseDto
    {
        public string Title { get; set; } = "";
        public string Instructor { get; set; } = "";

        public int Duration { get; set; }

        public string Description { get; set; } = "";
    }
}
