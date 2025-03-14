using EmployeeManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;


namespace EmployeeManagement.Services.Validators
{
    public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeDto>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Employee Name is Required!")
                .MaximumLength(100).WithMessage("Employee Name Shouldn't be more than 100 character!");

            RuleFor(x => x.Position)
                .NotEmpty().WithMessage("Position is Required!");

            RuleFor(x => x.Department)
                .NotEmpty().WithMessage("Department is Required!");

            RuleFor(x => x.Salary)
                .GreaterThan(0).WithMessage("Salary Must be more than 0!");
        }
    }
}
