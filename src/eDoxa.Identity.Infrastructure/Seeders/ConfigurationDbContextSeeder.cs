// Filename: ConfigurationDbContextSeeder.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace eDoxa.Identity.Infrastructure.Seeders
{
    public sealed class ConfigurationDbContextSeeder
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public ConfigurationDbContextSeeder(ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = loggerFactory?.CreateLogger<ConfigurationDbContextSeeder>() ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public async Task SeedAsync(ConfigurationDbContext context)
        {
            if (!context.IdentityResources.Any())
            {
                foreach (var identityResource in IdentityResources())
                {
                    context.IdentityResources.Add(identityResource.ToEntity());
                }

                await context.SaveChangesAsync();

                _logger.LogInformation("IdentityResources being populated.");
            }
            else
            {
                _logger.LogInformation("IdentityResources already populated.");
            }

            if (!context.ApiResources.Any())
            {
                foreach (var apiResource in this.ApiResources())
                {
                    context.ApiResources.Add(apiResource.ToEntity());
                }

                await context.SaveChangesAsync();

                _logger.LogInformation("ApiResources being populated.");
            }
            else
            {
                _logger.LogInformation("ApiResources already populated.");
            }

            if (!context.Clients.Any())
            {
                foreach (var client in this.Clients())
                {
                    context.Clients.Add(client.ToEntity());
                }

                await context.SaveChangesAsync();

                _logger.LogInformation("Clients being populated.");
            }
            else
            {
                _logger.LogInformation("Clients already populated.");
            }
        }

        private static IEnumerable<IdentityResource> IdentityResources()
        {
            yield return new IdentityResources.OpenId();
            yield return new IdentityResources.Profile();
            yield return new IdentityResources.Email();
            yield return new IdentityResources.Phone();
        }

        private IEnumerable<ApiResource> ApiResources()
        {
            yield return new ApiResource(
                _configuration["IdentityServer:ApiResources:Identity:Name"],
                _configuration["IdentityServer:ApiResources:Identity:DisplayName"]
            )
            {
                Description = _configuration["IdentityServer:ApiResources:Identity:Description"]
            };

            yield return new ApiResource(
                _configuration["IdentityServer:ApiResources:Challenges:Name"],
                _configuration["IdentityServer:ApiResources:Challenges:DisplayName"]
            )
            {
                Description = _configuration["IdentityServer:ApiResources:Challenges:Description"]
            };

            yield return new ApiResource(
                _configuration["IdentityServer:ApiResources:Cashier:Name"],
                _configuration["IdentityServer:ApiResources:Cashier:DisplayName"]
            )
            {
                Description = _configuration["IdentityServer:ApiResources:Cashier:Description"]
            };

            yield return new ApiResource(
                _configuration["IdentityServer:ApiResources:Notifications:Name"],
                _configuration["IdentityServer:ApiResources:Notifications:DisplayName"]
            )
            {
                Description = _configuration["IdentityServer:ApiResources:Notifications:Description"]
            };
        }

        private IEnumerable<Client> Clients()
        {
            yield return new Client
            {
                ClientId = _configuration["IdentityServer:Clients:Web:Spa:ClientId"],
                ClientName = _configuration["IdentityServer:Clients:Web:Spa:ClientName"],
                AllowedCorsOrigins =
                {
                    _configuration["IdentityServer:Clients:Web:Spa:AllowedCorsOrigins"]
                },
                PostLogoutRedirectUris =
                {
                    _configuration["IdentityServer:Clients:Web:Spa:PostLogoutRedirectUrl"]
                },
                RedirectUris =
                {
                    _configuration["IdentityServer:Clients:Web:Spa:RedirectUrl"]
                },
                RequireConsent = false,
                AllowAccessTokensViaBrowser = true,
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.Phone,
                    _configuration["IdentityServer:ApiResources:Identity:Name"],
                    _configuration["IdentityServer:ApiResources:Challenges:Name"],
                    _configuration["IdentityServer:ApiResources:Cashier:Name"],
                    _configuration["IdentityServer:ApiResources:Notifications:Name"]
                }
            };

            yield return new Client
            {
                AllowAccessTokensViaBrowser = true,
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowedScopes =
                {
                    _configuration["IdentityServer:ApiResources:Identity:Name"]
                },
                ClientId = _configuration["IdentityServer:Clients:Swagger:Identity:ClientId"],
                ClientName = _configuration["IdentityServer:Clients:Swagger:Identity:ClientName"],
                RedirectUris =
                {
                    _configuration["IdentityServer:Clients:Swagger:Identity:RedirectUrl"]
                },
                PostLogoutRedirectUris =
                {
                    _configuration["IdentityServer:Clients:Swagger:Identity:PostLogoutRedirectUrl"]
                }
            };

            yield return new Client
            {
                AllowAccessTokensViaBrowser = true,
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowedScopes =
                {
                    _configuration["IdentityServer:ApiResources:Challenges:Name"]
                },
                ClientId = _configuration["IdentityServer:Clients:Swagger:Challenges:ClientId"],
                ClientName = _configuration["IdentityServer:Clients:Swagger:Challenges:ClientName"],
                RedirectUris =
                {
                    _configuration["IdentityServer:Clients:Swagger:Challenges:RedirectUrl"]
                },
                PostLogoutRedirectUris =
                {
                    _configuration["IdentityServer:Clients:Swagger:Challenges:PostLogoutRedirectUrl"]
                }
            };

            yield return new Client
            {
                AllowAccessTokensViaBrowser = true,
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowedScopes =
                {
                    _configuration["IdentityServer:ApiResources:Cashier:Name"]
                },
                ClientId = _configuration["IdentityServer:Clients:Swagger:Cashier:ClientId"],
                ClientName = _configuration["IdentityServer:Clients:Swagger:Cashier:ClientName"],
                RedirectUris =
                {
                    _configuration["IdentityServer:Clients:Swagger:Cashier:RedirectUrl"]
                },
                PostLogoutRedirectUris =
                {
                    _configuration["IdentityServer:Clients:Swagger:Cashier:PostLogoutRedirectUrl"]
                }
            };

            yield return new Client
            {
                AllowAccessTokensViaBrowser = true,
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowedScopes =
                {
                    _configuration["IdentityServer:ApiResources:Notifications:Name"]
                },
                ClientId = _configuration["IdentityServer:Clients:Swagger:Notifications:ClientId"],
                ClientName = _configuration["IdentityServer:Clients:Swagger:Notifications:ClientName"],
                RedirectUris =
                {
                    _configuration["IdentityServer:Clients:Swagger:Notifications:RedirectUrl"]
                },
                PostLogoutRedirectUris =
                {
                    _configuration["IdentityServer:Clients:Swagger:Notifications:PostLogoutRedirectUrl"]
                }
            };
        }
    }
}