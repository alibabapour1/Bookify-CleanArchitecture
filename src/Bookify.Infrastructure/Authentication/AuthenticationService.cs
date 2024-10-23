using System.Net.Http.Json;
using Bookify.Application.Authentication;
using Bookify.Domain.Users;
using Bookify.Infrastructure.Authentication.Models;

namespace Bookify.Infrastructure.Authentication;

public class AuthenticationService:IAuthenticationService   
{
    private const string PasswordCredentialType = "password";
    private readonly HttpClient _httpClient;

    public AuthenticationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> RegisterUserAsync(User user, string password, CancellationToken cancellationToken)
    {
        var userRepresentationModel = UserRepresentationModel.FromUser(user);
        userRepresentationModel.Credentials = new CredentialRepresentationModel[]
        {
            new()
            {
                Type = PasswordCredentialType,
                Value = password,
                Temporary = false
                
            }
        };
        var response = await _httpClient.PostAsJsonAsync("users",
            userRepresentationModel,
            cancellationToken);

        return ExtractIdentityIdFromLocationHeader(response);
    }

    private static string ExtractIdentityIdFromLocationHeader(HttpResponseMessage httpResponseMessage)
    {
        const string userSegmentName = "users/";
        var locationHeader = httpResponseMessage.Headers.Location?.PathAndQuery;
        if (locationHeader is null)
        {
            throw new InvalidOperationException("Location header cannot be null.");
        }

        var userSegmentValueIndex =
            locationHeader.IndexOf(userSegmentName, StringComparison.InvariantCultureIgnoreCase);
        var userIdentityId = locationHeader
            .Substring(userSegmentValueIndex + userSegmentName.Length);

        return userIdentityId;  
    }
}