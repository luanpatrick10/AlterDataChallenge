using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public static class SwaggerTokenConfig
{
    public static void AddTokenHeader(this SwaggerGenOptions options)
    {
        options.AddSecurityDefinition("X-Auth-Token", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Name = "X-Auth-Token",
            Type = SecuritySchemeType.ApiKey,
            Description = "Token for Manager or Member access"
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "X-Auth-Token"
                    }
                },
                new string[] {}
            }
        });
    }
}
