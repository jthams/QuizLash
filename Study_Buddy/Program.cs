using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.KeyVault;
using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace Study_Buddy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }
        /* The Following code block changes the default secret manager to the Azure Key Vault
         * Without it deployment to a production enviornment **such as azure** is not possible
         * this is because when you change the working enviornment. 
         * Microsoft.Extensions.Configuration cannot find the key
         * values that are associated with the calls to Configuration[key].
         * the Azure Key names must be identical to those in your secrets.json file
         */
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
