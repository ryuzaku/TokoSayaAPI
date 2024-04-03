using Microsoft.EntityFrameworkCore;
using TokoSayaAPI.Data;
using TokoSayaAPI.Interfaces;
using TokoSayaAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITokoRepository, TokoRepository>();
builder.Services.AddScoped<IPenjualanProdukRepository, PenjualanProdukRepository>();
builder.Services.AddScoped<IPenjualanRepository, PenjualanRepository>();
builder.Services.AddScoped<IProdukRepository, ProdukRepository>();
builder.Services.AddScoped<IRegisterRepository, RegisterRepository>();
builder.Services.AddDbContext<TokoSayaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TokoSayaContext"))
);

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
