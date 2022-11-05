using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using Application.Staffs.Dtos;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Persistence;

namespace Application.Staffs
{
    public class Edit
    {
        public class Command : IRequest<Result<StaffRDto>>
        {
            public StaffWDto Staff { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Staff.User.Username).NotEmpty();
            }
        }
        public class Handler : IRequestHandler<Command, Result<StaffRDto>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly IMapper _mapper;
            private readonly UserManager<AppUser> _userManager;
            private readonly IDocumentConverter _converter;
            public Handler(DataContext context, IUserAccessor userAccessor, IMapper mapper,
            IDocumentConverter converter, UserManager<AppUser> userManager)
            {
                _mapper = mapper;
                _context = context;
                _userAccessor = userAccessor;
                _userManager = userManager;
                _converter = converter;
            }

            public async Task<Result<StaffRDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

                try
                {
                    var userId = _userAccessor.GetUserId();
                    if (userId == null) return Result<StaffRDto>.Failure("Unauthorized operation",
                    (int)HttpStatusCode.Unauthorized);

                    var existingStaff = await _context.Staffs.FindAsync(request.Staff.Id);
                    if (existingStaff == null)
                        return Result<StaffRDto>.Failure("No matching staff record found", (int)HttpStatusCode.NotFound);

                    var school = await _context.Schools.FirstOrDefaultAsync(x => x.Id == request.Staff.SchoolId, cancellationToken);

                    var existingUser = await _userManager.FindByIdAsync(request.Staff.User.UserId);
                    if (existingUser.UserName == request.Staff.User.Username && existingUser.Id != request.Staff.User.UserId)
                        return Result<StaffRDto>.Failure("Username already exists", (int)HttpStatusCode.BadRequest);

                    if (existingUser.Email == request.Staff.User.Email && existingUser.Id != request.Staff.User.UserId)
                        return Result<StaffRDto>.Failure("Email already exists", (int)HttpStatusCode.BadRequest);

                    var folderPath = $"{school.Code}";
                    if (existingUser.ProfilePicture?.Length > 0)
                    {
                        var isDeleted = _converter.DeleteFile(existingUser.ProfilePicture, folderPath);
                    }
                    var path = "";
                    if (request.Staff.User.Avatar != null && request.Staff.User.Avatar.IndexOf("base64") != -1)
                    {
                        var fileName = $"{request.Staff.User.Username}_{school.Code}";
                        path = _converter.SaveImage(request.Staff.User.Avatar, folderPath, fileName);
                        existingUser.ProfilePicture = path != "" ? path : "";
                    }
                    existingUser.Email = request.Staff.User.Email.Trim();
                    existingUser.UserName = request.Staff.User.Username.Trim();
                    existingUser.FirstName = request.Staff.User.FirstName.Trim();
                    existingUser.MiddleName = request.Staff.User.MiddleName.Trim();
                    existingUser.LastName = request.Staff.User.LastName.Trim();
                    existingUser.PhoneNumber = request.Staff.User.PhoneNumber.Trim();
                    existingUser.ModifiedAt = System.DateTime.UtcNow;
                    existingUser.ModifiedBy = userId;

                    var result = await _userManager.UpdateAsync(existingUser);

                    if (result.Succeeded)
                    {
                        var roles = request.Staff.Roles;
                        var oldRoles = await _userManager.GetRolesAsync(existingUser);
                        if (oldRoles.Count > 0)
                            await _userManager.RemoveFromRolesAsync(existingUser, oldRoles);

                        await _userManager.AddToRolesAsync(existingUser, roles);

                        var staff = _mapper.Map<Staff>(request.Staff);

                        var staffType = await _context.StaffTypes.FirstOrDefaultAsync(x => x.Id == request.Staff.StaffTypeId);
                        existingStaff.School = school;
                        existingStaff.StaffType = staffType;
                        existingStaff.Title = staff.Title?.Trim();
                        existingStaff.Description = staff.Description?.Trim();
                        existingStaff.HaveMedicalCondition = staff.HaveMedicalCondition;
                        existingStaff.ConditionRemarks = (existingStaff.HaveMedicalCondition) ? staff.ConditionRemarks?.Trim() : "";
                        existingStaff.StreetAddress = staff.StreetAddress?.Trim();
                        existingStaff.PostalAddress = staff.PostalAddress?.Trim();
                        existingStaff.Code = $"{existingUser.UserName.Trim()}-{school.Code.Trim()}";
                        existingStaff.DOB = staff.DOB;
                        existingStaff.DateJoined = staff.DateJoined;
                        existingStaff.DateLeft = staff.DateLeft;


                        if(_context.Entry(existingStaff).State == EntityState.Modified){
                            var updated = await _context.SaveChangesAsync() > 0;

                          if (!updated) return Result<StaffRDto>.Failure("Failed to update Staff",
                           (int)HttpStatusCode.BadRequest);
                        }

                        await transaction.CommitAsync();

                        var updatedStaff = _mapper.Map<StaffRDto>(existingStaff);

                        updatedStaff.Roles = roles;
                        return Result<StaffRDto>.Success(updatedStaff, (int)HttpStatusCode.OK);
                    }
                    return Result<StaffRDto>.Failure("Cannot update the existing staff", (int)HttpStatusCode.BadRequest);
                }
                catch (System.Exception ex)
                {

                    System.Console.WriteLine(ex.Message);
                    await transaction.RollbackAsync();
                    return Result<StaffRDto>.Failure("Internal server error", (int)HttpStatusCode.InternalServerError);
                }

            }
        }
    }
}