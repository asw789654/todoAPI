using Microsoft.AspNetCore.Http.HttpResults;
using todoAPI.Apis;

internal class Programm
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
       
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        TodoAPI.Map(app);

        app.Run();
    }
}


