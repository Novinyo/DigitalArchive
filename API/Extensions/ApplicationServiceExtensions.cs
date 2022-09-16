using System.Collections.Generic;
using API.Utility;
using Application.Core;
using Application.Interfaces;
using Application.Schools;
using Domain;
using Infrastructure.Security;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Persistence;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
         IConfiguration config)
        {
            services.AddSwaggerGen(c =>
           {
               c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
               {
                   Description = @"JWT Authorization header using Bearer scheme.\r\n\n
                    Enter 'Bearer' [space] and your token in the text input below\r\n",
                   Name = "Authorization",
                   In = ParameterLocation.Header,
                   Type = SecuritySchemeType.ApiKey,
                   Scheme = "Bearer"
               });

               c.AddSecurityRequirement(new OpenApiSecurityRequirement()
               {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
               });

               c.SwaggerDoc("v1", new OpenApiInfo { Title = "School Archives API", Version = "v1" });
               c.OperationFilter<FileResultContentTypeOperationFilter>();
           });

            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(DataContext).Assembly.FullName));
            });

            services.AddIdentity<AppUser, IdentityRole>()
            .AddEntityFrameworkStores<DataContext>()
            .AddSignInManager<SignInManager<AppUser>>()
            .AddDefaultTokenProviders();

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithOrigins("http://localhost:3000");
                });
            });

            services.AddMediatR(typeof(List.Handler).Assembly);
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<IAuthService, AuthenticationSrvice>();
            services.AddSignalR();

            return services;
        }
    }
}