using GuestBookRazorPages.Models;
using GuestBookRazorPages.Repository;
using GuestBookRazorPages.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSession();

// Add Services 
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IHash, Hashing>();

// DataBase Connection
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<GuestBookContext>(options => options.UseSqlServer(connection));


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
