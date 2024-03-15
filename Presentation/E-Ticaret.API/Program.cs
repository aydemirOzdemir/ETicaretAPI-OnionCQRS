using E_Ticaret.Persistance;
using E_Ticaret.Application;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var env=builder.Environment;
builder.Configuration.SetBasePath(env.ContentRootPath).AddJsonFile("appsettings.json",optional:false).AddJsonFile($"appsettings.{env.EnvironmentName}.json",optional:true);
builder.Services.AddPersistanceRegister(builder.Configuration);//Bunu env belirlendikten sonra al ��nk� configuration hangi envde �al��t���n� bir bulsun ondan sonra eklenebilsin dbcontext
builder.Services.AddApplicationRegister();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
