using WatchRate.Infrastucture;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPersistence(builder.Configuration);
}
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Run();

