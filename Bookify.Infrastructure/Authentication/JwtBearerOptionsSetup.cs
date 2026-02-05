using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Bookify.Infrastructure.Authentication;

internal sealed class JwtBearerOptionsSetup
{
    private readonly AuthenticationOptions _authenticationOptions;

    public JwtBearerOptionsSetup(AuthenticationOptions authenticationOptions)
    {
        _authenticationOptions = authenticationOptions;
    }

    public void Configure(JwtBearerOptions options)
    {
        options.Audience = _authenticationOptions.Audience;
        options.MetadataAddress = _authenticationOptions.MetadataUrl;
        options.RequireHttpsMetadata = _authenticationOptions.RequireHttpsMetadata;
        options.TokenValidationParameters.ValidIssuer = _authenticationOptions.Issuer;
    }

    public void Configure(string name, JwtBearerOptions options)
    {
        Configure(options);
    }
}
