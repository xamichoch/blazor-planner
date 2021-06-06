using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace blazor_planner
{
    public partial class Program
    {
        public class JwtAuthenticationStateProvider : AuthenticationStateProvider
        {
            private readonly ILocalStorageService _storage;

            public JwtAuthenticationStateProvider(ILocalStorageService storage)
            {
                _storage = storage;
            }

            public override async Task<AuthenticationState> GetAuthenticationStateAsync()
            {
                if (await _storage.ContainKeyAsync("access_token"))
                {
                    string tokenAsString = await _storage.GetItemAsStringAsync("access_token");
                    var jwt = new JwtSecurityTokenHandler();
                    var token = jwt.ReadJwtToken(tokenAsString);
                    var identity = new ClaimsIdentity(token.Claims, "Bearer");
                    var user = new ClaimsPrincipal(identity);
                    var authState = new AuthenticationState(user);

                    NotifyAuthenticationStateChanged(Task.FromResult(authState));

                    return authState;
                }

                return new AuthenticationState(new ClaimsPrincipal()); //Empty claim principal - read user - means there's no authorized user
            }
        }
    }
}
