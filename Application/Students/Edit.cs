using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Students
{
    public class Edit
    {
        public class Command : IRequest<Result<StudentDto>>
        {
            public StudentWDto Student { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Student.Code).NotEmpty();
                RuleFor(x => x.Student.FirstName).NotEmpty();
                RuleFor(x => x.Student.LastName).NotEmpty();
                RuleFor(x => x.Student.DateOfBirth).NotEmpty();
            }
        }
        public class Handler : IRequestHandler<Command, Result<StudentDto>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IUserAccessor userAccessor, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
                _userAccessor = userAccessor;
            }

            public async Task<Result<StudentDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

                try
                {
                    var userId = _userAccessor.GetUserId();

                    if (userId == null) return Result<StudentDto>.Failure("Unauthorized operation",
                    (int)HttpStatusCode.Unauthorized);

                    var student = await _context.Students.Include(x => x.Contact).Include(x => x.School)
                    .FirstOrDefaultAsync(x => x.Id == request.Student.Id
                    && x.DeletedAt == null);

                    var school = student.School;

                    if (student == null) return Result<StudentDto>.Failure("No matching student found",
                     (int)HttpStatusCode.NotFound);

                    var duplicateCode = await _context.Students
                       .FirstOrDefaultAsync(x => x.Code == request.Student.Code
                       && x.Id != request.Student.Id && x.SchoolId == request.Student.SchoolId);

                    if (duplicateCode != null)
                    {
                        return Result<StudentDto>.Failure("Student code must be unique",
                        (int)HttpStatusCode.BadRequest);

                    }
                    if (student.Contact == null || student.Contact.DeletedBy != null)
                        return Result<StudentDto>.Failure("No matching contact found",
                        (int)HttpStatusCode.NotFound);

                    var documents = await _context.Documents.
                        Where(x => x.StudentId == student.Id).ToListAsync(cancellationToken);

                    _context.Documents.RemoveRange(documents);
                    
                    await _context.SaveChangesAsync();
                    var contact = student.Contact;

                    _mapper.Map(request.Student.Contact, contact);
                    if (_context.Entry(contact).State == EntityState.Modified)
                    {
                        contact.ModifiedAt = DateTime.UtcNow;
                        contact.ModifiedBy = userId;
                    }

                    _mapper.Map(request.Student, student);

                    if (_context.Entry(student).State == EntityState.Modified)
                    {
                        student.ModifiedAt = DateTime.UtcNow;
                        student.ModifiedBy = userId;
                        student.School = school;
                    }
                    

                    foreach (var document in student.Documents)
                    {
                        document.Active = true;
                        document.CreatedBy = userId;
                        document.CreatedAt = DateTime.UtcNow;
                        document.StudentId = student.Id;
                        document.Id = Guid.NewGuid();

                        _context.Documents.Add(document);
                    }
                    var result = await _context.SaveChangesAsync() > 0;

                    if (!result) return Result<StudentDto>.Failure("Failed to update student",
                     (int)HttpStatusCode.BadRequest);


                    await transaction.CommitAsync();
                    var studentDto = _mapper.Map<StudentDto>(student);
                    return Result<StudentDto>.Success(studentDto, (int)HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    return Result<StudentDto>.Failure(ex.Message,
                     (int)HttpStatusCode.InternalServerError);
                }

            }
        }
    }
}