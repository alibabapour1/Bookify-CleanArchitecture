using System.Net.Http.Json;
using Bookify.Application.Authentication;
using Bookify.Domain.Abstractions;
using Bookify.Infrastructure.Authentication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Bookify.Infrastructure.Authentication;

public class JwtService :IJwtService
{
    private static readonly Error AuthenticationFailed = new ("KeyCloak.AuthenticationFailed"
        ,"Failed to Acquire AccessToken Due to Authentication Failure. ");
    private readonly KeycloakOptions _options;
    private readonly HttpClient _httpClient;

    public JwtService(HttpClient httpClient, IOptions<KeycloakOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }


    public async Task<Result<string>> GetAccessTokenAsync(string userName, string password, CancellationToken cancellationToken)
    {
        try
        {
            var requestParameters = new KeyValuePair<string, string>[]
            {
                new("client_id", _options.AuthClientId),
                new("client_secret", _options.AuthClientSecret),
                new("scope", "openid email"),
                new("grant_type", "password"),
                new("username", userName),
                new("password", password)
            };

            var authenticationRequestContent = new FormUrlEncodedContent(requestParameters);
            var response =await _httpClient.PostAsync("", authenticationRequestContent, cancellationToken);
            response.EnsureSuccessStatusCode();
            var tokenValue = await response.Content.ReadFromJsonAsync<AuthorizationToken>(cancellationToken);
            if (tokenValue is null)
            {
                return Result.Failure<string>(AuthenticationFailed);
            }

            return tokenValue.AccessToken;
        }
        catch (HttpRequestException)
        {
            return Result.Failure<string>(AuthenticationFailed);
        }
    }
}