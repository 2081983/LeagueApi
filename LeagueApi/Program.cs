using LeagueApi.Dependency.IRepository;
using LeagueApi.Dependency.Repository;
using LeagueApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IGroupRepository,GroupRepository>();
builder.Services.AddTransient<ITeamRepository,TeamRepository>();
builder.Services.AddTransient<IMatchRepository,MatchRepository>();
builder.Services.AddDbContext<AppDb>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("cn"));
});
builder.Services.AddCors(x => x.AddPolicy("mypolicy", y =>
{
    y.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
}));

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = ActionContext =>
    {
        var errors = ActionContext.ModelState
       .Where(x => x.Value.Errors.Count > 0)
       .SelectMany(x => x.Value.Errors)
       .Select(x => x.ErrorMessage).ToArray();
        var toReturn = new { errors = errors };
        return new BadRequestObjectResult(toReturn);
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("mypolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
