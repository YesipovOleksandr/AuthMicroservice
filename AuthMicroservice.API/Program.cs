using AuthMicroservice.API.MappingProfile;
using AuthMicroservice.BLL.Services;
using AuthMicroservice.DAL.Context;
using AuthMicroservice.DAL.MappingProfile;
using AuthMicroservice.DAL.Repository;
using AuthMicroservice.Domain.Abstract.Repository;
using AuthMicroservice.Domain.Abstract.Services;
using AuthMicroservice.Domain.Models.Settings;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(ApiMappingProfile), typeof(DataAccessMapingProfile));

builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection("JWT"));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddTransient<IHasher, Hasher>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IAuthService, AuthService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
