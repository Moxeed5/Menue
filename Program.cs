using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Read the service account key path from appsettings.json
var serviceAccountKeyPath = builder.Configuration["Firebase:ServiceAccountKeyPath"];

// Initialize the Firebase Admin SDK
FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.FromFile(serviceAccountKeyPath),
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<FirebaseService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Register services


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1"));
}

// Other configurations like app.UseHttpsRedirection(), app.UseAuthorization(), etc.

app.MapControllers();

app.Run();