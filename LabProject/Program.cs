using Microsoft.AspNetCore.Builder;

using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Hosting;

using System;

using Microsoft.Extensions.Logging; // Add this



var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddRazorPages();

builder.Services.AddTransient<ILoggerFactory, LoggerFactory>(); // Add this

builder.Services.AddTransient(typeof(ILogger<>), typeof(Logger<>));  // And this



// Configure session services

builder.Services.AddSession(options =>

{

    options.IdleTimeout = TimeSpan.FromMinutes(30);

    options.Cookie.HttpOnly = true;

    options.Cookie.IsEssential = true;

});



var app = builder.Build();



// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())

{

    app.UseExceptionHandler("/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.

    app.UseHsts();

}



app.UseHttpsRedirection();

app.UseStaticFiles(); // Make sure static files are served (CSS, JS, etc.)



app.UseRouting();  // Add routing middleware



app.UseSession(); // Enable session state



app.UseAuthorization(); // Make sure this is after UseRouting and UseSession



app.MapRazorPages(); // Maps Razor Pages



app.Run();