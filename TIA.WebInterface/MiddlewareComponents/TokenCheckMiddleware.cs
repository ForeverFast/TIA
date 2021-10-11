using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TIA.BusinessLogicLayer.WebInterface;
using TIA.WebInterface.Models;

namespace TIA.WebInterface.MiddlewareComponents
{
    public class TokenCheckMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenCheckMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var principal = context.User as ClaimsPrincipal;
            string accessToken = principal?.Claims
              .FirstOrDefault(c => c.Type == "token")?.Value;
            string refreshToken = principal?.Claims
              .FirstOrDefault(c => c.Type == "refreshToken")?.Value;

            if (context.Request.Path.Value == "/Account/Login")
            {
                await _next.Invoke(context);
                return;
            }

            if (!context.User.Identity.IsAuthenticated && context.Request.Path.Value != "/Account/Login")
            {
                context.Response.Redirect("/Account/Login");
                await _next(context);
                return;
            }

            if (string.IsNullOrEmpty(accessToken) && string.IsNullOrEmpty(refreshToken))
            {
                context.Response.Redirect("/Account/Login");
                foreach (var cookie in context.Request.Cookies.Keys)
                {
                    context.Response.Cookies.Delete(cookie);
                }
                await _next(context);
            }
            else
            {
                var checkTokenUrl = new RestClient($"http://localhost:18062/Account/check?token={accessToken}");
                checkTokenUrl.Timeout = -1;
                var checkTokenRequest = new RestRequest(Method.GET);
                IRestResponse checkTokenResponse = checkTokenUrl.Execute(checkTokenRequest);

                JsonCoreObject<bool> checkTokenContent = JsonConvert.DeserializeObject<JsonCoreObject<bool>>(checkTokenResponse.Content);

                if (checkTokenContent.Object)
                {
                    await _next.Invoke(context);
                }
                else
                {
                    if (string.IsNullOrEmpty(refreshToken))
                    {
                        context.Response.Redirect("/Account/Login");
                        foreach (var cookie in context.Request.Cookies.Keys)
                        {
                            context.Response.Cookies.Delete(cookie);
                        }
                        await _next(context);
                    }

                    var refreshTokenUrl = new RestClient("http://localhost:18062/Account/refresh");
                    refreshTokenUrl.Timeout = -1;
                    var refreshTokenRequest = new RestRequest(Method.POST);
                    refreshTokenRequest.AddHeader("Content-Type", "application/json");

                    TokenApiModel refreshTokenBodyData = new TokenApiModel
                    {
                        AccessToken = accessToken,
                        RefreshToken = refreshToken
                    };

                    var refreshTokenBody = JsonConvert.SerializeObject(refreshTokenBodyData);
                    refreshTokenRequest.AddParameter("application/json", refreshTokenBody, ParameterType.RequestBody);
                    IRestResponse refreshTokenResponse = refreshTokenUrl.Execute(refreshTokenRequest);

                    JsonCoreObject<TokenApiModel> refreshTokenResponseData = JsonConvert.DeserializeObject<JsonCoreObject<TokenApiModel>>(refreshTokenResponse.Content);

                    if (refreshTokenResponseData.Object != null)
                    {
                        //context.Response.Cookies.Delete("token");
                        //context.Response.Cookies.Append("token", refreshTokenResponseData.Object.AccessToken);
                        //context.Response.Cookies.Delete("refreshToken");
                        //context.Response.Cookies.Append("refreshToken", refreshTokenResponseData.Object.RefreshToken);

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimsIdentity.DefaultNameClaimType, principal.Identity.Name),
                            new Claim("token", refreshTokenResponseData.Object.AccessToken),
                            new Claim("refreshToken", refreshTokenResponseData.Object.RefreshToken)
                        };
                        ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie");

                        context.User = new ClaimsPrincipal(id);
                        
                        await _next.Invoke(context);
                    }
                    else
                    {
                        context.Response.Redirect("/Account/Login");
                        foreach (var cookie in context.Request.Cookies.Keys)
                        {
                            context.Response.Cookies.Delete(cookie);
                        }
                        await _next(context);
                    }
                }
            }
        }
    }
}
