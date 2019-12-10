// Filename: UserGrpcService.cs
// Date Created: 2019-12-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Grpc.Protos.Identity.Responses;
using eDoxa.Grpc.Protos.Shared.Dtos;
using eDoxa.Identity.Api.Application.Services;

using Grpc.Core;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using UserService = eDoxa.Grpc.Protos.Identity.Services.UserService;

namespace eDoxa.Identity.Api.Services
{
    public sealed class UserGrpcService : UserService.UserServiceBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly ILogger _logger;

        public UserGrpcService(IUserService userService, IRoleService roleService, ILogger<UserGrpcService> logger)
        {
            _userService = userService;
            _roleService = roleService;
            _logger = logger;
        }

        public override async Task<AddUserClaimResponse> AddUserClaim(AddUserClaimRequest request, ServerCallContext context)
        {
            var user = await _userService.FindByIdAsync(request.UserId);

            if (user == null)
            {
                var detail = $"The user '{request.UserId}' wasn't found.";

                context.Status = new Status(StatusCode.NotFound, detail);

                _logger.LogWarning(detail);

                return new AddUserClaimResponse
                {
                    Status = new StatusDto
                    {
                        Code = (int) context.Status.StatusCode,
                        Message = context.Status.Detail
                    }
                };
            }

            var claims = await _userService.GetClaimsAsync(user);

            var claim = new Claim(request.Claim.Type, request.Claim.Value);

            if (claims.Any(x => x.Type == claim.Type && x.Value == claim.Value))
            {
                var detail = $"The claim type '{claim.Type}' (value=\"{claim.Value}\") already exists in user '{user.Email}'. (userId=${user.Id})";

                context.Status = new Status(StatusCode.AlreadyExists, detail);

                _logger.LogWarning(detail);

                return new AddUserClaimResponse
                {
                    Status = new StatusDto
                    {
                        Code = (int) context.Status.StatusCode,
                        Message = context.Status.Detail
                    },
                    Claim = new UserClaimDto
                    {
                        Type = claim.Type,
                        Value = claim.Value
                    }
                };
            }

            var result = await _userService.AddClaimAsync(user, claim);

            if (result.Succeeded)
            {
                var detail = $"The claim type '{claim.Type}' (value=\"{claim.Value}\") as been added to the user '{user.Email}'. (userId=${user.Id})";

                context.Status = new Status(StatusCode.OK, detail);

                _logger.LogInformation(detail);

                return new AddUserClaimResponse
                {
                    Status = new StatusDto
                    {
                        Code = (int) context.Status.StatusCode,
                        Message = context.Status.Detail
                    },
                    Claim = new UserClaimDto
                    {
                        Type = claim.Type,
                        Value = claim.Value
                    }
                };
            }

            var message = $"Failed to add the claim type '{claim.Type}' (value=\"{claim.Value}\") to the user '{user.Email}'. (userId=${user.Id})";

            throw this.RpcExceptionWithDataLossStatus(result, message);
        }

        public override async Task<RemoveUserClaimResponse> RemoveUserClaim(RemoveUserClaimRequest request, ServerCallContext context)
        {
            var user = await _userService.FindByIdAsync(request.UserId);

            if (user == null)
            {
                var detail = $"The user '{request.UserId}' wasn't found.";

                context.Status = new Status(StatusCode.NotFound, detail);

                _logger.LogWarning(detail);

                return new RemoveUserClaimResponse
                {
                    Status = new StatusDto
                    {
                        Code = (int) context.Status.StatusCode,
                        Message = context.Status.Detail
                    }
                };
            }

            var claims = await _userService.GetClaimsAsync(user);

            var claim = new Claim(request.Claim.Type, request.Claim.Value);

            if (!claims.Any(x => x.Type == claim.Type && x.Value == claim.Value))
            {
                var detail = $"The claim type '{claim.Type}' (value=\"{claim.Value}\") not found in user '{user.Email}'. (userId=${user.Id})";

                context.Status = new Status(StatusCode.NotFound, detail);

                _logger.LogWarning(detail);

                return new RemoveUserClaimResponse
                {
                    Status = new StatusDto
                    {
                        Code = (int) context.Status.StatusCode,
                        Message = context.Status.Detail
                    }
                };
            }

            var result = await _userService.RemoveClaimAsync(user, claim);

            if (result.Succeeded)
            {
                var detail = $"The claim type '{claim.Type}' (value=\"{claim.Value}\") as been removed to the user '{user.Email}'. (userId=${user.Id})";

                context.Status = new Status(StatusCode.OK, detail);

                _logger.LogInformation(detail);

                return new RemoveUserClaimResponse
                {
                    Status = new StatusDto
                    {
                        Code = (int) context.Status.StatusCode,
                        Message = context.Status.Detail
                    },
                    Claim = new UserClaimDto
                    {
                        Type = claim.Type,
                        Value = claim.Value
                    }
                };
            }

            var message = $"Failed to remove the claim type '{claim.Type}' (value=\"{claim.Value}\") from the user '{user.Email}'. (userId=${user.Id})";

            throw this.RpcExceptionWithDataLossStatus(result, message);
        }

        public override async Task<ReplaceUserClaimResponse> ReplaceUserClaim(ReplaceUserClaimRequest request, ServerCallContext context)
        {
            var user = await _userService.FindByIdAsync(request.UserId);

            if (user == null)
            {
                var detail = $"The user '{request.UserId}' wasn't found.";

                context.Status = new Status(StatusCode.NotFound, detail);

                _logger.LogWarning(detail);

                return new ReplaceUserClaimResponse
                {
                    Status = new StatusDto
                    {
                        Code = (int) context.Status.StatusCode,
                        Message = context.Status.Detail
                    }
                };
            }

            var claims = await _userService.GetClaimsAsync(user);

            var claim = claims.SingleOrDefault(x => x.Type == request.Claim.Type);

            if (claim == null)
            {
                var detail = $"The claim type '{request.Claim.Type}' (value=\"{request.Claim.Value}\") not found in user '{user.Email}'. (userId=${user.Id})";

                context.Status = new Status(StatusCode.NotFound, detail);

                _logger.LogWarning(detail);

                return new ReplaceUserClaimResponse
                {
                    Status = new StatusDto
                    {
                        Code = (int) context.Status.StatusCode,
                        Message = context.Status.Detail
                    }
                };
            }

            var newClaim = new Claim(request.Claim.Type, request.Claim.Value);

            var result = await _userService.ReplaceClaimAsync(user, claim, newClaim);

            if (result.Succeeded)
            {
                var detail = $"The claim type '{newClaim.Type}' (value=\"{newClaim.Value}\") as been replaced for the user '{user.Email}'. (userId=${user.Id})";

                context.Status = new Status(StatusCode.OK, detail);

                _logger.LogInformation(detail);

                return new ReplaceUserClaimResponse
                {
                    Status = new StatusDto
                    {
                        Code = (int) context.Status.StatusCode,
                        Message = context.Status.Detail
                    },
                    Claim = new UserClaimDto
                    {
                        Type = newClaim.Type,
                        Value = newClaim.Value
                    }
                };
            }

            var message = $"Failed to replace the claim type '{newClaim.Type}' (value=\"{newClaim.Value}\") from the user '{user.Email}'. (userId=${user.Id})";

            throw this.RpcExceptionWithDataLossStatus(result, message);
        }

        public override async Task<AddUserToRoleResponse> AddUserToRole(AddUserToRoleRequest request, ServerCallContext context)
        {
            var user = await _userService.FindByIdAsync(request.UserId);

            if (user == null)
            {
                var detail = $"The user '{request.UserId}' wasn't found.";

                context.Status = new Status(StatusCode.NotFound, detail);

                _logger.LogWarning(detail);

                return new AddUserToRoleResponse
                {
                    Status = new StatusDto
                    {
                        Code = (int) context.Status.StatusCode,
                        Message = context.Status.Detail
                    }
                };
            }

            if (await _userService.IsInRoleAsync(user, request.RoleName))
            {
                var existingRole = await _roleService.FindByNameAsync(request.RoleName);

                var detail = $"The role '{existingRole.Name}' already exists. (roleId=${existingRole.Id})";

                context.Status = new Status(StatusCode.AlreadyExists, detail);

                _logger.LogInformation(detail);

                return new AddUserToRoleResponse
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

            var result = await _userService.AddToRoleAsync(user, request.RoleName);

            if (result.Succeeded)
            {
                var role = await _roleService.FindByNameAsync(request.RoleName);

                var detail = $"The role '{role.Name}' as been created. (roleId=${role.Id})";

                context.Status = new Status(StatusCode.OK, detail);

                _logger.LogInformation(detail);

                return new AddUserToRoleResponse
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

            var message = "";

            throw this.RpcExceptionWithDataLossStatus(result, message);
        }

        public override async Task<RemoveUserFromRoleResponse> RemoveUserFromRole(RemoveUserFromRoleRequest request, ServerCallContext context)
        {
            var user = await _userService.FindByIdAsync(request.UserId);

            if (user == null)
            {
                var detail = $"The user '{request.UserId}' wasn't found.";

                context.Status = new Status(StatusCode.NotFound, detail);

                _logger.LogWarning(detail);

                return new RemoveUserFromRoleResponse
                {
                    Status = new StatusDto
                    {
                        Code = (int) context.Status.StatusCode,
                        Message = context.Status.Detail
                    }
                };
            }

            if (!await _userService.IsInRoleAsync(user, request.RoleName))
            {
                var existingRole = await _roleService.FindByNameAsync(request.RoleName);

                var detail = $"The role '{existingRole.Name}' already exists. (roleId=${existingRole.Id})";

                context.Status = new Status(StatusCode.NotFound, detail);

                _logger.LogInformation(detail);

                return new RemoveUserFromRoleResponse
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

            var result = await _userService.RemoveFromRoleAsync(user, request.RoleName);

            if (result.Succeeded)
            {
                var role = await _roleService.FindByNameAsync(request.RoleName);

                var detail = $"The role '{role.Name}' as been created. (roleId=${role.Id})";

                context.Status = new Status(StatusCode.OK, detail);

                _logger.LogInformation(detail);

                return new RemoveUserFromRoleResponse
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

            var message = "";

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
