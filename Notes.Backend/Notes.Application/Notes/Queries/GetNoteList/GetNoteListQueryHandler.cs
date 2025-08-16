using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Notes.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    public class GetNoteListQueryHandler : IRequestHandler<GetNoteListQuery, NoteListVm>
    {
        private readonly INotesDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetNoteListQueryHandler(INotesDbContext dbContext, IMapper mapper)
        { 
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<NoteListVm> Handle(GetNoteListQuery request, CancellationToken cancellationToken)
        {
            var notes = await _dbContext.Notes
                .Where(note => note.UserId == request.UserId)
                .ProjectTo<NoteLookUpDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

            return new NoteListVm { Notes = notes };
            
        }
    }
}
