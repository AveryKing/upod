using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;

var myAllowSpecificOrigins = "myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins, builder => builder.WithOrigins("*", "http://localhost:3000"));
});
builder.Services.AddControllers();
builder.Services.AddDbContext<ToDoContext>(opt => { opt.UseInMemoryDatabase("ToDoList"); });

var app = builder.Build();

if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();
app.UseCors(myAllowSpecificOrigins);

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();