using Serilog;
using Serilog.Example.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

SerilogExtension.AddSerilogApi(builder.Configuration);
builder.Host.UseSerilog(Log.Logger);
builder.Services.RefitRegister(builder.Configuration);
builder.Services.ApiConfig();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();