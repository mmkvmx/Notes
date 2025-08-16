using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Notes.Application.Notes.Commands.CreateNote
{
    public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
    {
        public CreateNoteCommandValidator()
        {
            RuleFor(command => command.Title)
                .NotEmpty().WithMessage("Title must not be empty.")
                .MaximumLength(200).WithMessage("Title must not exceed 250 characters.");
            RuleFor(command => command.UserId).NotEqual(Guid.Empty)
                .WithMessage("User ID must not be empty.")
                .Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage("Invalid User ID format.");
        }
    }
}
