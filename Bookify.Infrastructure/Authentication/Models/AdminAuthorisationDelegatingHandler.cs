using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace Bookify.Infrastructure.Authentication.Models
{
    public sealed class AdminAuthorisationDelegatingHandler : DelegatingHandler
    {
        private readonly KeycloakOptions _keycloakOptions;

        public AdminAuthorisationDelegatingHandler(IOptions<KeycloakOptions> keycloakOptions)
        {
            _keycloakOptions = keycloakOptions.Value;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var authorisationToken = await GetAuthorisationToken(cancellationToken);

            request.Headers.Authorization = new AuthenticationHeaderValue(
                JwtBearerDefaults.AuthenticationScheme,
                authorisationToken.AccessToken);

            var httpResponseMessage = await base.SendAsync(request, cancellationToken);

            httpResponseMessage.EnsureSuccessStatusCode();

            return httpResponseMessage;
        }

        private async Task<AuthorisationToken> GetAuthorisationToken(CancellationToken cancellationToken)
        {
            var authorisationRequestParameters = new KeyValuePair<string, string>[]
            {
            new("client_id", _keycloakOptions.AdminClientId),
            new("client_secret", _keycloakOptions.AdminClientSecret),
            new("scope", "openid email"),
            new("grant_type", "client_credentials")
            };

            var authorisationRequestContent = new FormUrlEncodedContent(authorisationRequestParameters);

            var authorisationRequest = new HttpRequestMessage(
                HttpMethod.Post,
                new Uri(_keycloakOptions.TokenUrl))
            {
                Content = authorisationRequestContent
            };

            var authorisationResponse = await base.SendAsync(authorisationRequest, cancellationToken);

            authorisationResponse.EnsureSuccessStatusCode();
            return await authorisationResponse.Content.ReadFromJsonAsync<AuthorisationToken>(cancellationToken) ??
                   throw new ApplicationException();
        }
    }
}
