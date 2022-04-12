using System.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ToDoApi.Settings;
using ToDoApi.Tasks;
using ToDoApi.Users;
using static System.Text.Encoding;

var myAllowSpecificOrigins = "myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins, b => b.WithOrigins("*", "http://localhost:3000").AllowAnyHeader().AllowAnyMethod());
});

builder.Services.Configure<TasksDbSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.Configure<UsersSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddSingleton<TasksService>();
builder.Services.AddSingleton<UsersService>();

builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(ASCII.GetBytes("chrbei8h9f3iuonf3ufdoujoidjowdoidjoicnwdwewdrw")),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);

var app = builder.Build();

if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();
app.UseCors(myAllowSpecificOrigins);
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();