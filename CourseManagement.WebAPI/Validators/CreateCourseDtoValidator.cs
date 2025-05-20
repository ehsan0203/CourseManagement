using CourseManagement.WebAPI.DTOs;
using FluentValidation;

namespace CourseManagement.WebAPI.Validators
{
    public class CreateCourseDtoValidator : AbstractValidator<CreateCourseDto>
    {
        public CreateCourseDtoValidator() 
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("عنوان الزامی است.")
                .MaximumLength(100).WithMessage("حداکثر طول عنوان 100 کارکتر است.");

            RuleFor(x => x.Instructor)
                .NotEmpty().WithMessage("نام مدرس الزامی است.");

            RuleFor(x => x.Duration)
                .GreaterThan(0).WithMessage("مدت زمان باید بزرگتر از صفر باشد.");
        }
    }
}
