using System.Collections.Generic;
using KiraNet.Camellia.Shared;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace KiraNet.Camellia.AuthorizationServer.Configuration
{
    public class IdentityServerConfig
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            var serviceConfig = ServiceConfiguration.Configs;
            return new List<ApiResource>
            {
                new ApiResource(serviceConfig.ApiName, serviceConfig.ServiceDisplay)
                {
                    UserClaims = { JwtClaimTypes.Name, JwtClaimTypes.PreferredUserName, JwtClaimTypes.Email, JwtClaimTypes.Role }
                },
                new ApiResource(serviceConfig.ClientName, serviceConfig.ServiceDisplay)
                {
                    UserClaims = { JwtClaimTypes.Name, JwtClaimTypes.PreferredUserName, JwtClaimTypes.Email, JwtClaimTypes.Role }
                },
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            var serviceConfig = ServiceConfiguration.Configs;
            return new List<Client>
            {
                new Client
                {
                    ClientId= serviceConfig.ApiId,
                    ClientName = serviceConfig.ApiName,
                    AllowedGrantTypes = GrantTypes.Implicit,  // 使用implicit flow
                    AllowAccessTokensViaBrowser = true, // 控制是否通过浏览器为该客户端传输access token (默认为 false)
                    AllowOfflineAccess = true, // 离线是指网站和用户之间断开了，这样就需要一个refresh token
                    AccessTokenLifetime = 60 * 60 * 2, // access token 有效时间
                    UpdateAccessTokenClaimsOnRefresh = true,
                    RedirectUris = { $"{serviceConfig.ApiBase}/login-callback" },
                    PostLogoutRedirectUris = { serviceConfig.ApiBase },
                    AllowedCorsOrigins = { serviceConfig.ApiBase },
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        serviceConfig.ApiName
                    }
                },
                    new Client
                    {
                        ClientId = serviceConfig.ClientId,
                        ClientName = serviceConfig.ClientName,
                        AllowedGrantTypes = GrantTypes.Implicit,
                        AllowAccessTokensViaBrowser = true,
                        AllowOfflineAccess = true,
                        AccessTokenLifetime = 60 * 60 *2,
                        UpdateAccessTokenClaimsOnRefresh = true,
                        RedirectUris = { $"http://localhost:5200/login-callback" },
                        PostLogoutRedirectUris = { "http://localhost:5200/logout-callback" },
                        AllowedCorsOrigins = { "http://localhost:5200" },
                        AlwaysIncludeUserClaimsInIdToken = true,
                        AllowedScopes =
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            IdentityServerConstants.StandardScopes.Email,
                            serviceConfig.ClientName
                        }
                    }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }
    }
}
