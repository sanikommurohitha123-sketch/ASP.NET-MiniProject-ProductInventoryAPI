using Microsoft.EntityFrameworkCore;
using ProductInventoryAPI.Data;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

namespace ProductInventoryAPI.Models
{
    public class Product
    {
        public int Id { get; set;}
        public String Name { get; set;} = string.Empty;
        public String Category {get; set;} =String.Empty;
        public decimal Price {get; set;} = 0;
        public int StockQuantity {get; set;} = 0;
    }
}