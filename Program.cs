
using ToDoApi.Models;
using ToDoApi.Services;
using ToDoApi.Settings;

var myAllowSpecificOrigins = "myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins, builder => builder.WithOrigins("*", "http://localhost:3000"));
});

builder.Services.Configure<TasksDatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.Configure<UsersDatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

builder.Services.AddSingleton<TasksService>();
builder.Services.AddSingleton<UsersService>();

builder.Services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);

var app = builder.Build();

if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();
app.UseCors(myAllowSpecificOrigins);

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();