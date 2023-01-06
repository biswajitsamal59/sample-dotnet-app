using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// SecretClientOptions options = new SecretClientOptions()
//     {
//         Retry =
//         {
//             Delay= TimeSpan.FromSeconds(2),
//             MaxDelay = TimeSpan.FromSeconds(16),
//             MaxRetries = 5,
//             Mode = RetryMode.Exponential
//          }
//     };
// var client = new SecretClient(new Uri("https://dev-e-aks-kv.vault.azure.net/"), new DefaultAzureCredential(),options);

// KeyVaultSecret secret = client.GetSecret("test");

// string secretValue = secret.Value;

app.MapGet("/", () => "Hello World!");

app.Run();
