using Microsoft.EntityFrameworkCore;
using ReviewApplication.Interface;
using ReviewApplication.MappingProfile;
using ReviewApplication.Services;
using ReviewInfrastructure.DBContext;
using ReviewInfrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program)); // hoặc assembly chứa profile


builder.Services.AddDbContext<ReviewDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ReviewDBConnection")));

builder.Services.AddAutoMapper(typeof(ReviewMappingProfile));


builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IImageService, ImageService>();

builder.Services.AddScoped<ReviewRepository>();

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
