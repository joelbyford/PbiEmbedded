using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//required for keyvault
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;
using System.Web.Configuration;


namespace PowerBIEmbedded_AppOwnsData.App_Start
{
    public class KeyVaultUtils
    {
        //this is an optional property to hold the secret after it is retrieved
        public static string Password { get; set; }

        //the method that will be provided to the KeyVaultClient
        public static async Task<string> GetToken(string authority, string resource, string scope)
        {
            var authContext = new AuthenticationContext(authority);
            ClientCredential clientCred = new ClientCredential(WebConfigurationManager.AppSettings["vaultId"],
                        WebConfigurationManager.AppSettings["vaultSecret"]);
            AuthenticationResult result = await authContext.AcquireTokenAsync(resource, clientCred);

            if (result == null)
                throw new InvalidOperationException("Failed to obtain the JWT token");

            return result.AccessToken;
        }
        // Using Client ID and Client Secret is a way to authenticate an Azure AD application.
        // Using it in your web application allows for a separation of duties and more control over your key management. 
        // However, it does rely on putting the Client Secret in your configuration settings.
        // For some people, this can be as risky as putting the secret in your configuration settings.

        

    }
}