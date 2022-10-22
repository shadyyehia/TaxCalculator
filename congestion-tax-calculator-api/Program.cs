using DBAccess;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers(x=>
//if the client asks for application/xml for example, it will not default to json representation. 
//we will get 406 error code instead
x.ReturnHttpNotAcceptable=true
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction =>
{
    var xmlcommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlcommentsFilePath = Path.Combine(AppContext.BaseDirectory, xmlcommentsFile);
    setupAction.IncludeXmlComments(xmlcommentsFilePath);
});

builder.Services.AddTransient<IDB_Manager, SqlLite_Manager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
