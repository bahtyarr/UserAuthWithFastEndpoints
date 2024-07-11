using System.Security.Claims;
using System.Text;
using AuthProjects.Core.Constants;
using AuthProjects.Infrastructures;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NSwag;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddDbContext<CoreDbContext>(options =>
    options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(6, 0, 2)),
        option => option
            .EnableRetryOnFailure(10)
            .MigrationsAssembly("AuthProjects.Infrastructures")));

using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CoreDbContext>();
    dbContext.Database.Migrate();
}

builder.Services.AddRepositoriesScoped(configuration);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", x => x.RequireRole(RoleConstant.Admin).RequireClaim(ClaimTypes.Role));
});

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument(o =>
{
    o.DocumentSettings = s =>
    {
        s.DocumentName = "Initial-Release";
        s.Title = "Web API";
        s.Version = "v1.0";
        s.AddAuth("ApiKey", new()
        {
            Name = "api_key",
            In = OpenApiSecurityApiKeyLocation.Header,
            Type = OpenApiSecuritySchemeType.ApiKey,
        });
        s.AddAuth("Bearer", new()
        {
            Type = OpenApiSecuritySchemeType.Http,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            BearerFormat = "JWT",
        });
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors("AllowAll");

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints();
app.UseSwaggerGen();

app.Run();
