using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace akvwebapi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    private const string AppSecretsMountPath = "/mnt/secrets-store";

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        ReadSecretMountVolume(AppSecretsMountPath);
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
    // public void Read(string[] args)
    // {
    //     Thread.Sleep(1000);
    //     while (true)
    //     {
    //         ReadSecretMountVolume(AppSecretsMountPath);
    //         Thread.Sleep(5000);
    //     }
    // }

    private void ReadSecretMountVolume(string mountPath)
    {
        Console.WriteLine($"Processing: {AppSecretsMountPath}");

        var secretFolders = Directory.GetDirectories(AppSecretsMountPath);
        foreach (var folder in secretFolders)
        {
            var allSecretFiles = Directory.GetFiles(folder);
            foreach (var f in allSecretFiles)
            {
                Console.WriteLine($"Secret '{f}' has value '{System.IO.File.ReadAllText(f)}'");
            }
        }
    }
}
