using AzureAiServiceTrial;
using AzureAIServiceTrialApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var aiOptions = new AIOptions();
builder.Configuration.GetSection(nameof(AIOptions)).Bind(aiOptions);
builder.Services.AddSingleton(aiOptions);

builder.Services.AddSingleton<AIService>();
builder.Services.AddSingleton<AISearch>();
builder.Services.AddSingleton<BlobAccess>();

var blobAccessOptions = new BlobAccessOptions();
builder.Configuration.GetSection(nameof(BlobAccessOptions)).Bind(blobAccessOptions);
builder.Services.AddSingleton(blobAccessOptions);

//Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.WithOrigins("https://localhost:7255")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
