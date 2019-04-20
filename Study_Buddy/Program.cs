using Microsoft.AspNetCore;
using Microsoft.Azure.KeyVault;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }


        // The Following code block changes the default secret manager to the Azure Key Vault 
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).ConfigureAppConfiguration((context, config) =>
            {
                if (context.HostingEnvironment.IsProduction())
                {
                    var keyVaultEndpoint = "https://StudyBuddyDevV2-0-kv.vault.azure.net";
                    var builtConfig = config.Build();
                    var azureServiceTokenProvider = new AzureServiceTokenProvider();
                    var keyVaultClient = new KeyVaultClient(
                        new KeyVaultClient.AuthenticationCallback(
                            azureServiceTokenProvider.KeyVaultTokenCallback));
                    config.AddAzureKeyVault(keyVaultEndpoint, keyVaultClient, new DefaultKeyVaultSecretManager());

                }
            })
            .UseStartup<Startup>(); 
    }
}
