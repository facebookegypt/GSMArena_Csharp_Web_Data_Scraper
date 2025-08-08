using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsGsmar.Credentials
{
    public static class GraphConfig1
    {
        public static string ClientId = "CLIENT_ID"; //navigate to https://portal.azure.com/auth/login/ | Register App | Get Client_ID
        public static string TenantId = "consumers"; //  this is critical, use as is.
        public static string[] Scopes = new[] { "Files.ReadWrite", "User.Read" };   //Navigate to API Permissions and SET TO THOSE.
    }
}
