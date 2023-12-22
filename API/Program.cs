using API;
using API.Middleware;
using API.Modules.Catalog.Startup;
using API.Modules.UserAccess.Startup;

var builder = WebApplication.CreateBuilder(args);

//API Services
builder.Services.AddPresentation(builder.Configuration);

//Modules startup
builder.Services.AddUserAccess(builder.Configuration);
builder.Services.AddCatalog(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler("/Error");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.Run();