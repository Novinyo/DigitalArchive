using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Students
{
    public class Create
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
            }
        }

        public class Handler : IRequestHandler<Command, Result<StudentDto>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IUserAccessor userAccessor, IMapper mapper)
            {
                _context = context;
                _userAccessor = userAccessor;
                _mapper = mapper;
            }

            public async Task<Result<StudentDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
                try
                {
                    var userId = _userAccessor.GetUserId();

                    var user = await _context.Users.FirstOrDefaultAsync(
                        x => x.Id == userId
                    );

                    if (user == null) return Result<StudentDto>.Failure("Unauthorized operation", (int)HttpStatusCode.Unauthorized);
                    var staff = await _context.Staffs.FirstOrDefaultAsync(x => x.User.Id == user.Id);

                    Guid schoolId = staff.SchoolId;

                    var student = await _context.Students
                    .FirstOrDefaultAsync(x => x.Code == request.Student.Code 
                    && x.SchoolId == schoolId);

                    if (student != null)
                    {
                        if (student.Code == request.Student.Code)
                            return Result<StudentDto>.Failure("Student code must be unique", (int)HttpStatusCode.BadRequest);
                    }

                    var contact = _mapper.Map<Contact>(request.Student.Contact);
                    contact.CreatedAt = DateTime.UtcNow;
                    contact.CreatedBy = user.Id;
                    contact.Id = Guid.NewGuid();
                    _context.Contacts.Add(contact);

                    var newStudent = _mapper.Map<Student>(request.Student);
                    newStudent.CreatedAt = DateTime.UtcNow;
                    newStudent.CreatedBy = user.Id;
                    newStudent.Id = Guid.NewGuid();
                    newStudent.SchoolId = schoolId;
                    newStudent.Contact = contact;

                    _context.Students.Add(newStudent);

                    var result = await _context.SaveChangesAsync() > 0;

                    if (!result) return Result<StudentDto>.Failure("Failed to create a new Student", (int)HttpStatusCode.BadRequest);

                    await transaction.CommitAsync();

                    var studentDto = _mapper.Map<StudentDto>(newStudent);
                    return Result<StudentDto>.Success(studentDto, (int)HttpStatusCode.Created);
                }
                catch (System.Exception ex)
                {
                    await transaction.RollbackAsync();

                    return Result<StudentDto>.Failure(ex.Message,
                     (int)HttpStatusCode.InternalServerError);
                }

            }
        }
    }
}