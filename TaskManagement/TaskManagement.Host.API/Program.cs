using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using MongoDB.Driver;

using TaskManagement.Notifier;
using TaskManagement.Scheduler;
using TaskManagement.Services.EF;
using TaskManagement.Services.Contract;
using TaskManagement.Services.Mongo.Settings;
using TaskManagement.Business.Services;
using TaskManagement.Business.Workflow;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

if (configuration["MainDatabase"] == "SQLServer")
{
  builder.Services.AddDbContext<TaskManagementDBContext>(options => options.UseSqlServer(configuration.GetConnectionString("SQLServer")));

  builder.Services.AddScoped<IStudentsRepository, TaskManagement.Services.EF.Repositories.StudentsRepository>();
  builder.Services.AddScoped<ITeachersRepository, TaskManagement.Services.EF.Repositories.TeachersRepository>();
  builder.Services.AddScoped<ITaskRepository, TaskManagement.Services.EF.Repositories.TaskRepository>();
}
else
{
  // Mongo Services:
  builder.Services.Configure<TaskManagementDatabaseSettings>(configuration.GetSection(nameof(TaskManagementDatabaseSettings)));

  builder.Services.AddSingleton<ITaskManagementDatabaseSettings>(s => s.GetRequiredService<IOptions<TaskManagementDatabaseSettings>>().Value);

  builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(configuration.GetValue<string>("TaskManagementDatabaseSettings:URI")));

  builder.Services.AddScoped<IStudentsRepository, TaskManagement.Services.Mongo.Repositories.StudentsRepository>();
  builder.Services.AddScoped<ITeachersRepository, TaskManagement.Services.Mongo.Repositories.TeachersRepository>();
  builder.Services.AddScoped<ITaskRepository, TaskManagement.Services.Mongo.Repositories.TasksRepository>();
}

builder.Services.AddScoped<ITaskManagementService, TaskManagementService>();
builder.Services.AddScoped<ITaskWorkflow, TaskWorkflow>();
builder.Services.AddScoped<ITaskScheduler, TaskManagement.Scheduler.TaskScheduler>();

builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowAllHeaders", b =>
  {
    b.AllowAnyOrigin();
    b.AllowAnyHeader();
    b.AllowAnyMethod();
  });
});

builder.Services.AddControllers();
builder.Services.AddSignalR(options => options.EnableDetailedErrors = true);
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

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapHub<TaskManagementNotifier>("/notification");
app.Run();
