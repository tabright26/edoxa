// Filename: RoleGrpcService.cs
// Date Created: 2019-12-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Application.Services;
using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;
using eDoxa.Identity.Grpc.Protos;

using Grpc.Core;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using RoleService = eDoxa.Identity.Grpc.Protos.RoleService;

namespace eDoxa.Identity.Api.Services
{
    public sealed class RoleGrpcService : RoleService.RoleServiceBase
    {
        private readonly IRoleService _roleService;
        private readonly ILogger _logger;

        public RoleGrpcService(IRoleService roleService, ILogger<RoleGrpcService> logger)
        {
            _roleService = roleService;
            _logger = logger;
        }

        public override async Task<CreateRoleResponse> CreateRole(CreateRoleRequest request, ServerCallContext context)
        {
            if (!await _roleService.RoleExistsAsync(request.RoleName))
            {
                var existingRole = await _roleService.FindByNameAsync(request.RoleName);

                var detail = $"The role '{existingRole.Name}' already exists. (roleId=${existingRole.Id})";

                context.Status = new Status(StatusCode.AlreadyExists, detail);

                _logger.LogInformation(detail);

                return new CreateRoleResponse
                {
                    Status = new StatusDto
                    {
                        Code = (int) context.Status.StatusCode,
                        Message = context.Status.Detail
                    },
                    Role = new RoleDto
                    {
                        Id = existingRole.Id.ToString(),
                        Name = existingRole.Name
                    }
                };
            }

            var role = new Role
            {
                Id = Guid.NewGuid(),
                Name = request.RoleName
            };

            var result = await _roleService.CreateAsync(role);

            if (result.Succeeded)
            {
                var detail = $"The role '{role.Name}' as been created. (roleId=${role.Id})";

                context.Status = new Status(StatusCode.OK, detail);

                _logger.LogInformation(detail);

                return new CreateRoleResponse
                {
                    Status = new StatusDto
                    {
                        Code = (int) context.Status.StatusCode,
                        Message = context.Status.Detail
                    },
                    Role = new RoleDto
                    {
                        Id = role.Id.ToString(),
                        Name = role.Name
                    }
                };
            }

            var message = $"Failed to create the role '{role.Name}'. (roleId=${role.Id})";

            throw this.RpcExceptionWithDataLossStatus(result, message);
        }

        public override async Task<DeleteRoleResponse> DeleteRole(DeleteRoleRequest request, ServerCallContext context)
        {
            if (!await _roleService.RoleExistsAsync(request.RoleName))
            {
                var detail = $"The role '{request.RoleName}' wasn't found.";

                context.Status = new Status(StatusCode.NotFound, detail);

                _logger.LogWarning(detail);

                return new DeleteRoleResponse
                {
                    Status = new StatusDto
                    {
                        Code = (int) context.Status.StatusCode,
                        Message = context.Status.Detail
                    }
                };
            }

            var role = await _roleService.FindByNameAsync(request.RoleName);

            var result = await _roleService.DeleteAsync(role);

            if (result.Succeeded)
            {
                var detail = $"The role '{role.Name}' as been deleted. (roleId=${role.Id})";

                context.Status = new Status(StatusCode.OK, detail);

                _logger.LogInformation(detail);

                return new DeleteRoleResponse
                {
                    Status = new StatusDto
                    {
                        Code = (int) context.Status.StatusCode,
                        Message = context.Status.Detail
                    },
                    Role = new RoleDto
                    {
                        Id = role.Id.ToString(),
                        Name = role.Name
                    }
                };
            }

            var message = $"Failed to delete the role '{role.Name}'. (roleId=${role.Id})";

            throw this.RpcExceptionWithDataLossStatus(result, message);
        }

        public override async Task<AddRoleClaimResponse> AddRoleClaim(AddRoleClaimRequest request, ServerCallContext context)
        {
            if (!await _roleService.RoleExistsAsync(request.RoleName))
            {
                var detail = $"The role '{request.RoleName}' wasn't found.";

                context.Status = new Status(StatusCode.NotFound, detail);

                _logger.LogWarning(detail);

                return new AddRoleClaimResponse
                {
                    Status = new StatusDto
                    {
                        Code = (int) context.Status.StatusCode,
                        Message = context.Status.Detail
                    }
                };
            }

            var role = await _roleService.FindByNameAsync(request.RoleName);

            var claims = await _roleService.GetClaimsAsync(role);

            var claim = new Claim(request.Claim.Type, request.Claim.Value);

            if (claims.Any(x => x.Type == claim.Type && x.Value == claim.Value))
            {
                var detail = $"The claim type '{claim.Type}' (value=\"{claim.Value}\") already exists in role '{role.Name}'. (roleId=${role.Id})";

                context.Status = new Status(StatusCode.AlreadyExists, detail);

                _logger.LogWarning(detail);

                return new AddRoleClaimResponse
                {
                    Status = new StatusDto
                    {
                        Code = (int) context.Status.StatusCode,
                        Message = context.Status.Detail
                    },
                    Claim = new RoleClaimDto
                    {
                        Type = claim.Type,
                        Value = claim.Value
                    }
                };
            }

            var result = await _roleService.AddClaimAsync(role, claim);

            if (result.Succeeded)
            {
                var detail = $"The claim type '{claim.Type}' (value=\"{claim.Value}\") as been added to the role '{role.Name}'. (roleId=${role.Id})";

                context.Status = new Status(StatusCode.OK, detail);

                _logger.LogInformation(detail);

                return new AddRoleClaimResponse
                {
                    Status = new StatusDto
                    {
                        Code = (int) context.Status.StatusCode,
                        Message = context.Status.Detail
                    },
                    Claim = new RoleClaimDto
                    {
                        Type = claim.Type,
                        Value = claim.Value
                    }
                };
            }

            var message = $"Failed to add the claim type '{claim.Type}' (value=\"{claim.Value}\") to the role '{role.Name}'. (roleId=${role.Id})";

            throw this.RpcExceptionWithDataLossStatus(result, message);
        }

        public override async Task<RemoveRoleClaimResponse> RemoveRoleClaim(RemoveRoleClaimRequest request, ServerCallContext context)
        {
            if (!await _roleService.RoleExistsAsync(request.RoleName))
            {
                var detail = $"The role '{request.RoleName}' wasn't found.";

                context.Status = new Status(StatusCode.NotFound, detail);

                _logger.LogWarning(detail);

                return new RemoveRoleClaimResponse
                {
                    Status = new StatusDto
                    {
                        Code = (int) context.Status.StatusCode,
                        Message = context.Status.Detail
                    }
                };
            }

            var role = await _roleService.FindByNameAsync(request.RoleName);

            var claims = await _roleService.GetClaimsAsync(role);

            var claim = new Claim(request.Claim.Type, request.Claim.Value);

            if (!claims.Any(x => x.Type == claim.Type && x.Value == claim.Value))
            {
                var detail = $"The claim type '{claim.Type}' (value=\"{claim.Value}\") not found in role '{role.Name}'. (roleId=${role.Id})";

                context.Status = new Status(StatusCode.NotFound, detail);

                _logger.LogWarning(detail);

                return new RemoveRoleClaimResponse
                {
                    Status = new StatusDto
                    {
                        Code = (int) context.Status.StatusCode,
                        Message = context.Status.Detail
                    }
                };
            }

            var result = await _roleService.RemoveClaimAsync(role, claim);

            if (result.Succeeded)
            {
                var detail = $"The claim type '{claim.Type}' (value=\"{claim.Value}\") as been removed from the role '{role.Name}'. (roleId=${role.Id})";

                context.Status = new Status(StatusCode.OK, detail);

                _logger.LogInformation(detail);

                return new RemoveRoleClaimResponse
                {
                    Status = new StatusDto
                    {
                        Code = (int) context.Status.StatusCode,
                        Message = context.Status.Detail
                    },
                    Claim = new RoleClaimDto
                    {
                        Type = claim.Type,
                        Value = claim.Value
                    }
                };
            }

            var message = $"Failed to remove the claim type '{claim.Type}' (value=\"{claim.Value}\") from the role '{role.Name}'. (roleId=${role.Id})";

            throw this.RpcExceptionWithDataLossStatus(result, message);
        }

        private RpcException RpcExceptionWithDataLossStatus(IdentityResult result, string message)
        {
            var metadata = new Metadata
            {
                {nameof(IdentityResult), result.ToString()}
            };

            var exception = new RpcException(new Status(StatusCode.DataLoss, message), metadata);

            _logger.LogCritical(exception, message);

            return exception;
        }
    }
}
