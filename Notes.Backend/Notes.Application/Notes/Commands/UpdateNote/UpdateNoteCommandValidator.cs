using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Notes.Application.Notes.Commands.UpdateNote
{
    public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
    {
        public UpdateNoteCommandValidator() 
        { 
            RuleFor(command => command.Id)
                .NotEqual(Guid.Empty).WithMessage("Note ID must not be empty.")
                .Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage("Invalid Note ID format.");
            RuleFor(command => command.UserId)
                .NotEqual(Guid.Empty).WithMessage("User ID must not be empty.")
                .Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage("Invalid User ID format.");
            RuleFor(command => command.Title)
                .NotEmpty().WithMessage("Title must not be empty.")
                .MaximumLength(250).WithMessage("Title must not exceed 250 characters.");
        }
    }
}
