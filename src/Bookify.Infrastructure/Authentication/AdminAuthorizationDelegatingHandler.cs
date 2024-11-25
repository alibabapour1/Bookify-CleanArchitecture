using System.Net.Http.Headers;
using System.Net.Http.Json;
using Bookify.Infrastructure.Authentication.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace Bookify.Infrastructure.Authentication;

public class AdminAuthorizationDelegatingHandler: DelegatingHandler
{
    private readonly KeycloakOptions _keycloakOptions;

    public AdminAuthorizationDelegatingHandler(IOptions<KeycloakOptions> keycloakOptions)
    {
        _keycloakOptions = keycloakOptions.Value;
    }


    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var authorizationToken = await GetAuthorizationToken(cancellationToken);
        request.Headers.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme,
            authorizationToken.AccessToken);
        
        var httpResponseMessage = await base.SendAsync(request, cancellationToken);
        httpResponseMessage.EnsureSuccessStatusCode();

        return httpResponseMessage; 
    }

    private async Task<AuthorizationToken> GetAuthorizationToken(CancellationToken cancellationToken)
    {
        var authorizationRequestParameters = new KeyValuePair<string, string>[]
        {
            new ("client_id",_keycloakOptions.AdminClientId),
            new ("client_secret",_keycloakOptions.AdminClientSecret),
            new ("grant_type","client_credentials"),
            new ("scope","openid email")
        };

        var authorizationTokenContent = new FormUrlEncodedContent(authorizationRequestParameters);
        var request = new HttpRequestMessage(HttpMethod.Post, new Uri(_keycloakOptions.AdminUrl))
        {
            Content = authorizationTokenContent
        };

        var httpResponse = await base.SendAsync(request, cancellationToken);
        httpResponse.EnsureSuccessStatusCode();

        return await httpResponse.Content.ReadFromJsonAsync<AuthorizationToken>(cancellationToken) ??
               throw new ApplicationException("Application Exception");




    }
}