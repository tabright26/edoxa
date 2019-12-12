// Filename: IdentityGrpcService.cs
// Date Created: 2019-12-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Grpc.Extensions;
using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Grpc.Protos.Identity.Responses;
using eDoxa.Grpc.Protos.Identity.Services;
using eDoxa.Identity.Api.Application.Services;
using eDoxa.Identity.Api.Services.Extensions;
using eDoxa.Identity.Domain.AggregateModels.DoxatagAggregate;
using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;

using Grpc.Core;

namespace eDoxa.Identity.Api.Services
{
    public sealed class IdentityGrpcService : IdentityService.IdentityServiceBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IDoxatagService _doxatagService;

        public IdentityGrpcService(IUserService userService, IRoleService roleService, IDoxatagService doxatagService)
        {
            _userService = userService;
            _roleService = roleService;
            _doxatagService = doxatagService;
        }

        public override async Task<AddUserClaimResponse> AddUserClaim(AddUserClaimRequest request, ServerCallContext context)
        {
            var user = await _userService.FindByIdAsync(request.UserId);

            if (user == null)
            {
                var detail = $"The user '{request.UserId}' wasn't found.";

                throw context.RpcException(new Status(StatusCode.NotFound, detail));
            }

            var claims = await _userService.GetClaimsAsync(user);

            var claim = new Claim(request.Claim.Type, request.Claim.Value);

            if (claims.Any(x => x.Type == claim.Type && x.Value == claim.Value))
            {
                var detail = $"The claim type '{claim.Type}' (value=\"{claim.Value}\") already exists in user '{user.Email}'. (userId=${user.Id})";

                return context.AlreadyExists(
                    new AddUserClaimResponse
                    {
                        Claim = new UserClaimDto
                        {
                            Type = claim.Type,
                            Value = claim.Value
                        }
                    },
                    detail);
            }

            var result = await _userService.AddClaimAsync(user, claim);

            if (result.Succeeded)
            {
                var detail = $"The claim type '{claim.Type}' (value=\"{claim.Value}\") as been added to the user '{user.Email}'. (userId=${user.Id})";

                return context.Ok(
                    new AddUserClaimResponse
                    {
                        Claim = new UserClaimDto
                        {
                            Type = claim.Type,
                            Value = claim.Value
                        }
                    },
                    detail);
            }

            var message = $"Failed to add the claim type '{claim.Type}' (value=\"{claim.Value}\") to the user '{user.Email}'. (userId=${user.Id})";

            context.AddIdentityResultToResponseTrailers(result);

            throw context.RpcException(new Status(StatusCode.FailedPrecondition, message));
        }

        public override async Task<RemoveUserClaimResponse> RemoveUserClaim(RemoveUserClaimRequest request, ServerCallContext context)
        {
            var user = await _userService.FindByIdAsync(request.UserId);

            if (user == null)
            {
                var detail = $"The user '{request.UserId}' wasn't found.";

                throw context.RpcException(new Status(StatusCode.NotFound, detail));
            }

            var claims = await _userService.GetClaimsAsync(user);

            var claim = new Claim(request.Claim.Type, request.Claim.Value);

            if (!claims.Any(x => x.Type == claim.Type && x.Value == claim.Value))
            {
                var detail = $"The claim type '{claim.Type}' (value=\"{claim.Value}\") not found in user '{user.Email}'. (userId=${user.Id})";

                throw context.RpcException(new Status(StatusCode.NotFound, detail));
            }

            var result = await _userService.RemoveClaimAsync(user, claim);

            if (result.Succeeded)
            {
                var detail = $"The claim type '{claim.Type}' (value=\"{claim.Value}\") as been removed to the user '{user.Email}'. (userId=${user.Id})";

                return context.Ok(
                    new RemoveUserClaimResponse
                    {
                        Claim = new UserClaimDto
                        {
                            Type = claim.Type,
                            Value = claim.Value
                        }
                    },
                    detail);
            }

            var message = $"Failed to remove the claim type '{claim.Type}' (value=\"{claim.Value}\") from the user '{user.Email}'. (userId=${user.Id})";

            context.AddIdentityResultToResponseTrailers(result);

            throw context.RpcException(new Status(StatusCode.FailedPrecondition, message));
        }

        public override async Task<ReplaceUserClaimResponse> ReplaceUserClaim(ReplaceUserClaimRequest request, ServerCallContext context)
        {
            var user = await _userService.FindByIdAsync(request.UserId);

            if (user == null)
            {
                var detail = $"The user '{request.UserId}' wasn't found.";

                throw context.RpcException(new Status(StatusCode.NotFound, detail));
            }

            var claims = await _userService.GetClaimsAsync(user);

            var claim = claims.SingleOrDefault(x => x.Type == request.Claim.Type);

            if (claim == null)
            {
                var detail = $"The claim type '{request.Claim.Type}' (value=\"{request.Claim.Value}\") not found in user '{user.Email}'. (userId=${user.Id})";

                throw context.RpcException(new Status(StatusCode.NotFound, detail));
            }

            var newClaim = new Claim(request.Claim.Type, request.Claim.Value);

            var result = await _userService.ReplaceClaimAsync(user, claim, newClaim);

            if (result.Succeeded)
            {
                var detail = $"The claim type '{newClaim.Type}' (value=\"{newClaim.Value}\") as been replaced for the user '{user.Email}'. (userId=${user.Id})";

                return context.Ok(
                    new ReplaceUserClaimResponse
                    {
                        Claim = new UserClaimDto
                        {
                            Type = newClaim.Type,
                            Value = newClaim.Value
                        }
                    },
                    detail);
            }

            var message = $"Failed to replace the claim type '{newClaim.Type}' (value=\"{newClaim.Value}\") from the user '{user.Email}'. (userId=${user.Id})";

            context.AddIdentityResultToResponseTrailers(result);

            throw context.RpcException(new Status(StatusCode.FailedPrecondition, message));
        }

        public override async Task<AddUserToRoleResponse> AddUserToRole(AddUserToRoleRequest request, ServerCallContext context)
        {
            var user = await _userService.FindByIdAsync(request.UserId);

            if (user == null)
            {
                var detail = $"The user '{request.UserId}' wasn't found.";

                throw context.RpcException(new Status(StatusCode.NotFound, detail));
            }

            if (await _userService.IsInRoleAsync(user, request.RoleName))
            {
                var existingRole = await _roleService.FindByNameAsync(request.RoleName);

                var detail = $"The role '{existingRole.Name}' already exists. (roleId=${existingRole.Id})";

                return context.AlreadyExists(
                    new AddUserToRoleResponse
                    {
                        Role = new RoleDto
                        {
                            Id = existingRole.Id.ToString(),
                            Name = existingRole.Name
                        }
                    },
                    detail);
            }

            var result = await _userService.AddToRoleAsync(user, request.RoleName);

            if (result.Succeeded)
            {
                var role = await _roleService.FindByNameAsync(request.RoleName);

                var detail = $"The role '{role.Name}' as been created. (roleId=${role.Id})";

                return context.Ok(
                    new AddUserToRoleResponse
                    {
                        Role = new RoleDto
                        {
                            Id = role.Id.ToString(),
                            Name = role.Name
                        }
                    },
                    detail);
            }

            var message = "";

            context.AddIdentityResultToResponseTrailers(result);

            throw context.RpcException(new Status(StatusCode.FailedPrecondition, message));
        }

        public override async Task<RemoveUserFromRoleResponse> RemoveUserFromRole(RemoveUserFromRoleRequest request, ServerCallContext context)
        {
            var user = await _userService.FindByIdAsync(request.UserId);

            if (user == null)
            {
                var detail = $"The user '{request.UserId}' wasn't found.";

                throw context.RpcException(new Status(StatusCode.NotFound, detail));
            }

            if (!await _userService.IsInRoleAsync(user, request.RoleName))
            {
                var existingRole = await _roleService.FindByNameAsync(request.RoleName);

                var detail = $"The role '{existingRole.Name}' already exists. (roleId=${existingRole.Id})";

                throw context.RpcException(new Status(StatusCode.NotFound, detail));
            }

            var result = await _userService.RemoveFromRoleAsync(user, request.RoleName);

            if (result.Succeeded)
            {
                var role = await _roleService.FindByNameAsync(request.RoleName);

                var detail = $"The role '{role.Name}' as been created. (roleId=${role.Id})";

                return context.Ok(
                    new RemoveUserFromRoleResponse
                    {
                        Role = new RoleDto
                        {
                            Id = role.Id.ToString(),
                            Name = role.Name
                        }
                    },
                    detail);
            }

            var message = "";

            context.AddIdentityResultToResponseTrailers(result);

            throw context.RpcException(new Status(StatusCode.FailedPrecondition, message));
        }

        public override async Task<CreateRoleResponse> CreateRole(CreateRoleRequest request, ServerCallContext context)
        {
            if (!await _roleService.RoleExistsAsync(request.RoleName))
            {
                var existingRole = await _roleService.FindByNameAsync(request.RoleName);

                var detail = $"The role '{existingRole.Name}' already exists. (roleId=${existingRole.Id})";

                return context.AlreadyExists(
                    new CreateRoleResponse
                    {
                        Role = new RoleDto
                        {
                            Id = existingRole.Id.ToString(),
                            Name = existingRole.Name
                        }
                    },
                    detail);
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

                return context.Ok(
                    new CreateRoleResponse
                    {
                        Role = new RoleDto
                        {
                            Id = role.Id.ToString(),
                            Name = role.Name
                        }
                    },
                    detail);
            }

            var message = $"Failed to create the role '{role.Name}'. (roleId=${role.Id})";

            context.AddIdentityResultToResponseTrailers(result);

            throw context.RpcException(new Status(StatusCode.FailedPrecondition, message));
        }

        public override async Task<DeleteRoleResponse> DeleteRole(DeleteRoleRequest request, ServerCallContext context)
        {
            if (!await _roleService.RoleExistsAsync(request.RoleName))
            {
                var detail = $"The role '{request.RoleName}' wasn't found.";

                throw context.RpcException(new Status(StatusCode.NotFound, detail));
            }

            var role = await _roleService.FindByNameAsync(request.RoleName);

            var result = await _roleService.DeleteAsync(role);

            if (result.Succeeded)
            {
                var detail = $"The role '{role.Name}' as been deleted. (roleId=${role.Id})";

                return context.Ok(
                    new DeleteRoleResponse
                    {
                        Role = new RoleDto
                        {
                            Id = role.Id.ToString(),
                            Name = role.Name
                        }
                    },
                    detail);
            }

            var message = $"Failed to delete the role '{role.Name}'. (roleId=${role.Id})";

            context.AddIdentityResultToResponseTrailers(result);

            throw context.RpcException(new Status(StatusCode.FailedPrecondition, message));
        }

        public override async Task<AddRoleClaimResponse> AddRoleClaim(AddRoleClaimRequest request, ServerCallContext context)
        {
            if (!await _roleService.RoleExistsAsync(request.RoleName))
            {
                var detail = $"The role '{request.RoleName}' wasn't found.";

                throw context.RpcException(new Status(StatusCode.NotFound, detail));
            }

            var role = await _roleService.FindByNameAsync(request.RoleName);

            var claims = await _roleService.GetClaimsAsync(role);

            var claim = new Claim(request.Claim.Type, request.Claim.Value);

            if (claims.Any(x => x.Type == claim.Type && x.Value == claim.Value))
            {
                var detail = $"The claim type '{claim.Type}' (value=\"{claim.Value}\") already exists in role '{role.Name}'. (roleId=${role.Id})";

                return context.AlreadyExists(
                    new AddRoleClaimResponse
                    {
                        Claim = new RoleClaimDto
                        {
                            Type = claim.Type,
                            Value = claim.Value
                        }
                    },
                    detail);
            }

            var result = await _roleService.AddClaimAsync(role, claim);

            if (result.Succeeded)
            {
                var detail = $"The claim type '{claim.Type}' (value=\"{claim.Value}\") as been added to the role '{role.Name}'. (roleId=${role.Id})";

                return context.Ok(
                    new AddRoleClaimResponse
                    {
                        Claim = new RoleClaimDto
                        {
                            Type = claim.Type,
                            Value = claim.Value
                        }
                    },
                    detail);
            }

            var message = $"Failed to add the claim type '{claim.Type}' (value=\"{claim.Value}\") to the role '{role.Name}'. (roleId=${role.Id})";

            context.AddIdentityResultToResponseTrailers(result);

            throw context.RpcException(new Status(StatusCode.FailedPrecondition, message));
        }

        public override async Task<RemoveRoleClaimResponse> RemoveRoleClaim(RemoveRoleClaimRequest request, ServerCallContext context)
        {
            if (!await _roleService.RoleExistsAsync(request.RoleName))
            {
                var detail = $"The role '{request.RoleName}' wasn't found.";

                throw context.RpcException(new Status(StatusCode.NotFound, detail));
            }

            var role = await _roleService.FindByNameAsync(request.RoleName);

            var claims = await _roleService.GetClaimsAsync(role);

            var claim = new Claim(request.Claim.Type, request.Claim.Value);

            if (!claims.Any(x => x.Type == claim.Type && x.Value == claim.Value))
            {
                var detail = $"The claim type '{claim.Type}' (value=\"{claim.Value}\") not found in role '{role.Name}'. (roleId=${role.Id})";

                throw context.RpcException(new Status(StatusCode.NotFound, detail));
            }

            var result = await _roleService.RemoveClaimAsync(role, claim);

            if (result.Succeeded)
            {
                var detail = $"The claim type '{claim.Type}' (value=\"{claim.Value}\") as been removed from the role '{role.Name}'. (roleId=${role.Id})";

                return context.Ok(
                    new RemoveRoleClaimResponse
                    {
                        Claim = new RoleClaimDto
                        {
                            Type = claim.Type,
                            Value = claim.Value
                        }
                    },
                    detail);
            }

            var message = $"Failed to remove the claim type '{claim.Type}' (value=\"{claim.Value}\") from the role '{role.Name}'. (roleId=${role.Id})";

            context.AddIdentityResultToResponseTrailers(result);

            throw context.RpcException(new Status(StatusCode.FailedPrecondition, message));
        }

        public override async Task<FetchDoxatagsResponse> FetchDoxatags(FetchDoxatagsRequest request, ServerCallContext context)
        {
            var doxatags = await _doxatagService.FetchDoxatagsAsync();

            context.Status = new Status(StatusCode.OK, "");

            return new FetchDoxatagsResponse
            {
                Doxatags =
                {
                    doxatags.Select(MapDoxatag)
                }
            };
        }

        private static DoxatagDto MapDoxatag(Doxatag doxatag)
        {
            return new DoxatagDto
            {
                Code = doxatag.Code,
                Name = doxatag.Name,
                UserId = doxatag.UserId.ToString()
            };
        }
    }
}
