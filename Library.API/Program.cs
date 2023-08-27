using Library.API.Data.Entities;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.OpenApi.Models;
using System.Net;
using Library.API.Data.Context;
using Library.API.Interface;
using Library.API.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();

    // swagger documentation setup
    builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Description = "Fast api",
            Name = "Authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
            BearerFormat = "JWT",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Scheme = "Bearer"
        });
        options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[]{}
            }
        });
    });

builder.Services.AddDbContext<LibraryContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBookRepository, BookRepository>();
    //builder.Services.AddScoped<IAuthService, AuthService>();

    // authentication scheme setup

    builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
    {
        //options.Password.RequiredUniqueChars = 0;
        //options.Password.RequireNonAlphanumeric = false;
    })
        .AddEntityFrameworkStores<LibraryContext>();
    // UserManager
    // RoleManager
    // SignInManager

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("CanAdd", policy => policy.RequireClaim("CanAdd", new List<string> { "true" }));
        options.AddPolicy("CanEdit", policy => policy.RequireClaim("CanEdit", new List<string> { "true" }));
        options.AddPolicy("CanDelete", policy => policy.RequireClaim("CanDelete", new List<string> { "true" }));
    });

    builder.Services.AddAutoMapper(typeof(Program));







    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    //app.UseAuthentication();
    app.UseAuthorization();  // Authorization is the act of allowing / prevent access to an endpoint depending on the user's auth level

    app.MapControllers();

    app.Run();

