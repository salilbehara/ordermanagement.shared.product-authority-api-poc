using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ordermanagement.shared.product_authority_api
{
    public interface IJwtManager
    {
        string ExternalAuthScheme { get; }
        TokenValidationParameters TokenValidationParams { get; }
    }
}
