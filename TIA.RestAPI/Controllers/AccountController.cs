using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TIA.DataAccessLayer.Models;
using TIA.RestAPI.JwtLogic;
using TIA.RestAPI.Models;
using TIA.RestAPI.Services;

namespace TIA.RestAPI.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;

        public AccountController(ITokenService tokenService, UserManager<User> userManager)
        {
            _tokenService = tokenService;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("token")]
        public async Task<JsonCoreObject<object>> Token(string username, string password)
        {
            ClaimsIdentity identity = await _tokenService.GetIdentity(username, password);

            if (identity == null)
            {
                HttpContext.Response.StatusCode = 401;
                return new JsonCoreObject<object>() { Object = null };
            }

            string accessToken = _tokenService.GenerateAccessToken(identity.Claims);
            string refreshToken = _tokenService.GenerateRefreshToken();

            User user = await _userManager.FindByNameAsync(username);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            await _userManager.UpdateAsync(user);

            var response = new
            {
                access_token = accessToken,
                refresh_token = refreshToken,
                username = username
            };

            return new JsonCoreObject<object>() { Object = response };
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<JsonCoreObject<object>> Refresh([FromBody] TokenApiModel tokenApiModel)
        {
            if (tokenApiModel is null || string.IsNullOrEmpty(tokenApiModel.AccessToken))
            {
                return new JsonCoreObject<object>
                { 
                    Errors = new List<string> { "Invalid client request" }
                };
            }

            string accessToken = tokenApiModel.AccessToken;
            string refreshToken = tokenApiModel.RefreshToken;

            ClaimsPrincipal principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            string username = principal.Identity.Name; //this is mapped to the Name claim by default

            User user = await _userManager.FindByNameAsync(username);
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return new JsonCoreObject<object>
                {
                    Errors = new List<string> { "Invalid client request" }
                };
            }

            string newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
            string newRefreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            var response = (object)new
            {
                access_token = newAccessToken,
                refresh_token = newRefreshToken
            };

            return new JsonCoreObject<object>() { Object = response };
        }

        [HttpGet]
        [Route("check")]
        public JsonCoreObject<bool> Check([FromQuery]string token)
        {
            return new JsonCoreObject<bool>
            {
                Object = _tokenService.IsTokenValid(token)
            };
        }

        [HttpPost, Authorize]
        [Route("revoke")]
        public async Task<JsonCoreObject<object>> Revoke()
        {
            var username = User.Identity.Name;
            User user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                HttpContext.Response.StatusCode = 400;
                return new JsonCoreObject<object>() { Object = null };
            }
                
            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);

            HttpContext.Response.StatusCode = 204;
            return new JsonCoreObject<object>() { Object = null };
        }
    }
}
