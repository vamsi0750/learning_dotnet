using WatchDog;
using WatchDog.src.Enums;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddWatchDogServices(opt =>
{
    opt.IsAutoClear = false;
    opt.SetExternalDbConnString = "Data Source=.;Initial Catalog=Product_Management; Integrated Security=true";
    opt.SqlDriverOption = WatchDogSqlDriverEnum.MSSQL;
});

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
app.UseWatchDogExceptionLogger();


app.UseWatchDog(opt =>
{
    opt.WatchPageUsername = "vamsi";
    opt.WatchPagePassword = "vamsi";
});
app.Run();
