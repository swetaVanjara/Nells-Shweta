using System;
namespace NellsPay.Send.ResponseModels
{
    public class JwtPayloadModel
    {
        public long exp { get; set; }
        public long iat { get; set; }
        public string jti { get; set; }
        public string iss { get; set; }
        public string aud { get; set; }
        public string sub { get; set; }
        public string typ { get; set; }
        public string azp { get; set; }
        public string sid { get; set; }
        public string acr { get; set; }
        public string[] allowed_origins { get; set; }

        public RealmAccess realm_access { get; set; }
        public ResourceAccess resource_access { get; set; }

        public string scope { get; set; }
        public bool email_verified { get; set; }
        public string name { get; set; }
        public string preferred_username { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string email { get; set; }
    }

    public class RealmAccess
    {
        public string[] roles { get; set; }
    }

    public class ResourceAccess
    {
        public AccountRoles account { get; set; }
    }

    public class AccountRoles
    {
        public string[] roles { get; set; }
    }
}

