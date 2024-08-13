using ServerSideApp.DataContext;
using ServerSideApp.Services.CityService;
using ServerSideApp.Services.StudentService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") // Allow requests from this origin React.
                  .AllowAnyHeader() // Allow any headers
                  .AllowAnyMethod(); // Allow any HTTP methods
        });
});





// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Add service DbContext
builder.Services.AddSingleton<DbContext>();

// inject the services.
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IStudentService, StudentService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use the CORS policy
app.UseCors("AllowLocalhost3000");

app.UseAuthorization();

app.MapControllers();

app.Run();
