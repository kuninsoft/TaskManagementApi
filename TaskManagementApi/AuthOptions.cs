using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TaskManagementApi;

public static class AuthOptions
{
    private const string Key = "o2wCKvKCFerGuHN875smMafYG+ifadQCi++LDCiMzQExx+O6lLYh2goAjgS8s1Kx";
    
    public const string Issuer = "TaskApiServer";
    public const string Audience = "TaskApiClient";
    
    public static SymmetricSecurityKey SymmetricSecurityKey => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
}