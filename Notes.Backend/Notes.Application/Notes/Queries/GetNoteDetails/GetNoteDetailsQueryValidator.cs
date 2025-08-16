using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Notes.Application.Notes.Queries.GetNoteDetails
{
    public class GetNoteDetailsQueryValidator : AbstractValidator<GetNoteDetailsQuery>
    {
        public GetNoteDetailsQueryValidator()
        {
            RuleFor(query => query.Id)
                .NotEqual(Guid.Empty).WithMessage("Note ID must not be empty.")
                .Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage("Invalid Note ID format.");
            RuleFor(query => query.UserId)
                .NotEqual(Guid.Empty).WithMessage("User ID must not be empty.")
                .Must(id => Guid.TryParse(id.ToString(), out _)).WithMessage("Invalid User ID format.");
        }
    }
}
