using System;
using System.Security.Claims;
using System.Text;
using System.Threading;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using ordermanagement.shared.cronus_edge_api.Extensions;

namespace ordermanagement.shared.product_authority_api
{
    public class JwtManager : IJwtManager
    {
        private readonly byte[] _jwtSecret;
        private readonly TimeSpan _expirationTimeSpan;
        private const string _issuer = "CronusEdgeAPI";
        private const string _audience = "CronusEdgeAPI";
        private const string _headerKey = "CronusJwt";
        private const string _jwtSecretErrorMessage = "Cronus JWT secret must be overridden or set via environment variable.";
        public const string ProductAuthorityClaimsKey = "ProductAuthorityClaims";
        public const string JwtSecretKey = "EBSCONET_JWT_SECRET";

        /// <summary>
        /// Initializes the manager.
        /// </summary>
        /// <param name="expirationTimeSpan">The timespan the JWT is valid.</param>
        /// <param name="jwtSecretOverride">An override for the JWT secret key. The environment variable will be used by default.</param>
        public JwtManager(TimeSpan expirationTimeSpan, string jwtSecretOverride = null)
        {
            _expirationTimeSpan = expirationTimeSpan;
            _jwtSecret = jwtSecretOverride.IsNullOrWhitespace()
                ? Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable(JwtSecretKey))
                : Encoding.Unicode.GetBytes(jwtSecretOverride);
            if (_jwtSecret?.Length <= 0)
            {
                throw new ArgumentNullException(nameof(jwtSecretOverride), _jwtSecretErrorMessage);
            }
        }

        public TokenValidationParameters TokenValidationParams
        {
            get
            {
                var signingKey = new SymmetricSecurityKey(_jwtSecret);
                return new TokenValidationParameters
                {
                    // Clock skew compensates for server time drift.
                    // We recommend 5 minutes or less:
                    ClockSkew = TimeSpan.FromMinutes(2),
                    ValidateAudience = true,
                    ValidAudience = _audience,
                    ValidateIssuer = true,
                    ValidIssuer = _issuer,
                    IssuerSigningKey = signingKey,
                    ValidateIssuerSigningKey = true,
                    RequireSignedTokens = true,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                };
            }
        }

        public string ExternalAuthScheme { get; } = nameof(ExternalAuthScheme);

    }
}
