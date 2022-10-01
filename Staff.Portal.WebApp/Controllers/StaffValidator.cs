using FluentValidation;
using Staff.Portal.Models;

namespace Staff.Portal.WebApp.Controllers;

public class StaffValidator : AbstractValidator<StaffModel>
{

    [Obsolete]
    public StaffValidator()
    {
        RuleFor(s => s.employment_number).Cascade(CascadeMode.StopOnFirstFailure)
            .NotEmpty().WithMessage("{PropertyName} is empty")
            .Length(1, 20).WithMessage("Length ({TotalLength}) of {PropertyName} is invalid");


        RuleFor(s => s.first_name).Cascade(CascadeMode.StopOnFirstFailure)
           .NotEmpty().WithMessage("First name is empty")
           .Length(1, 50).WithMessage("Length ({TotalLength}) of first name is invalid")
           .Must(BeValidName).WithMessage("First name has invalid characters");

        RuleFor(s => s.last_name).Cascade(CascadeMode.StopOnFirstFailure)
        .NotEmpty().WithMessage("Last name is empty")
        .Length(1, 50).WithMessage("Length ({TotalLength}) of last name is invalid")
        .Must(BeValidName).WithMessage("Last name has invalid characters");

        RuleFor(s => s.birth_date).Cascade(CascadeMode.StopOnFirstFailure)
        .NotEmpty().WithMessage("Date of birth is empty")
        .Must(BeAValidAge).WithMessage("Date of birth is invalid");

        RuleFor(s => s.years_work_experience).Cascade(CascadeMode.StopOnFirstFailure)
      .NotEmpty().WithMessage("Work experience is empty")
      .Must(BeAValidExperience).WithMessage("Invalid work experience");

    }

    protected bool BeValidName(string Name)
    {
        Name = Name.Trim();
        Name = Name.Replace("-", "");
        return Name.All(char.IsLetter);
    }
    protected bool BeAValidAge(DateTime? date)
    {
        if (date <= DateTime.Now.AddYears(-10) && date >= DateTime.Now.AddYears(-120))
            return true;

        return false;
    }

    protected bool BeAValidExperience(int? Experience)
    {

        if (Experience >= 1 && Experience <= 80)
        {
            return true;
        }

        return false;
    }


}
