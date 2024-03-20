using E_Ticaret.Persistance;
using E_Ticaret.Application;
using ETicaret.Mapper;
using E_Ticaret.Application.Exceptions;
using E_Ticaret.Infrastucture;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "ETicaret API", Version = "v1", Description = "ETicaretAPI Swagger client" });
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    {
        Name="Authorization",
        Type=Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme="Bearer",
        BearerFormat="JWT",
        In=Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description="Bearer yazýp boþluk býraktýktan sonra Tokený girebilirsiniz."

    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


var env=builder.Environment;
builder.Configuration.SetBasePath(env.ContentRootPath).AddJsonFile("appsettings.json",optional:false).AddJsonFile($"appsettings.{env.EnvironmentName}.json",optional:true);
builder.Services.AddPersistanceRegister(builder.Configuration);//Bunu env belirlendikten sonra al çünkü configuration hangi envde çalýþtýðýný bir bulsun ondan sonra eklenebilsin dbcontext
builder.Services.AddApplicationRegister();
builder.Services.AddAutoMapperRegister();
builder.Services.AddInfrastructureRegister(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ConfigureExceptionHandlingMiddleware();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
