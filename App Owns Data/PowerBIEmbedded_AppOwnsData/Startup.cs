using Microsoft.Azure.KeyVault;
using Microsoft.Owin;
using Owin;
using PowerBIEmbedded_AppOwnsData.App_Start;
using System;
using System.Configuration;

[assembly: OwinStartupAttribute(typeof(PowerBIEmbedded_AppOwnsData.Startup))]
namespace PowerBIEmbedded_AppOwnsData
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            getKeyVaultSecret();
        }

        /* ===============================
         * ADDED THIS TO SUPPORT KEYVAULT
         * ===============================*/
        public async void getKeyVaultSecret()
        {
            try
            {
                var kv = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(KeyVaultUtils.GetToken));
                var sec = await kv.GetSecretAsync(ConfigurationManager.AppSettings["vaultUri"]);
                KeyVaultUtils.Password = sec.Value;
            }
            catch (Exception exc)
            {
                KeyVaultUtils.Password = "invalid";
                //do some exception handling here
            }
        }
    }
}
