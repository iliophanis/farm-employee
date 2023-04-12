using System.Reflection;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using server.Modules;
using Serilog;
using server.Services;
using server.Data.DataSeed;
using MediatR;
using FluentValidation;
using server.Modules.Common.Behaviours;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.Host.UseSerilog((hostContext, services, configuration) =>
{
    configuration.ReadFrom.Configuration(hostContext.Configuration);
});

//Configure Serivces
var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
// Add services to the container.

var connectionString = configuration.GetConnectionString("MySQLDbConnectionString");
builder.Services.AddDbContextPool<DataContext>(options => options
        .UseMySql(
            connectionString,
            ServerVersion.AutoDetect(connectionString)
        ));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    o.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
var origins = builder.Configuration.GetSection("Origins").Get<string[]>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(origins);
        });
});

// seed test data in db
builder.Services.AddScoped<DataSeeder>();

builder.Services.AddAuthorization();

builder.Services.AddAuthentication()
        .AddGoogle(options =>
        {
            options.ClientId = "835350653644-8e8j2lns21efkrdc3u746vflrj759v88.apps.googleusercontent.com";
            options.ClientSecret = "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";
        });
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<CurrentUserService>();
builder.Services.AddTransient<CurrentUserService>();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSerilogRequestLogging();
    using (var serviceScope = app.Services.CreateScope())
    {
        var services = serviceScope.ServiceProvider;
        var dataSeederService = services.GetRequiredService<DataSeeder>();
        dataSeederService.Seed();
    }
}

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.DisplayRequestDuration());
}

app.UseCors();
//Map endpoints Generic Class
app.MapEndpoints(app.Configuration);

app.Run();
