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
using eDoxa.Identity.Api.Extensions;
using eDoxa.Identity.Domain.AggregateModels.DoxatagAggregate;
using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;
using eDoxa.Identity.Domain.Services;

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
                throw context.NotFoundRpcException($"The user '{request.UserId}' wasn't found.");
            }

            var claims = await _userService.GetClaimsAsync(user);

            var claim = new Claim(request.Claim.Type, request.Claim.Value);

            if (claims.Any(x => x.Type == claim.Type && x.Value == claim.Value))
            {
                var detail = $"The claim type '{claim.Type}' (value=\"{claim.Value}\") already exists in user '{user.Email}'. (userId=${user.Id})";

                var response = new AddUserClaimResponse
                {
                    Claim = new UserClaimDto
                    {
                        Type = claim.Type,
                        Value = claim.Value
                    }
                };

                return context.AlreadyExists(response, detail);
            }

            var result = await _userService.AddClaimAsync(user, claim);

            if (result.Succeeded)
            {
                var detail = $"The claim type '{claim.Type}' (value=\"{claim.Value}\") as been added to the user '{user.Email}'. (userId=${user.Id})";

                var response = new AddUserClaimResponse
                {
                    Claim = new UserClaimDto
                    {
                        Type = claim.Type,
                        Value = claim.Value
                    }
                };

                return context.Ok(response, detail);
            }

            throw context.FailedPreconditionRpcException(
                result,
                $"Failed to add the claim type '{claim.Type}' (value=\"{claim.Value}\") to the user '{user.Email}'. (userId=${user.Id})");
        }

        public override async Task<RemoveUserClaimResponse> RemoveUserClaim(RemoveUserClaimRequest request, ServerCallContext context)
        {
            var user = await _userService.FindByIdAsync(request.UserId);

            if (user == null)
            {
                throw context.NotFoundRpcException($"The user '{request.UserId}' wasn't found.");
            }

            var claims = await _userService.GetClaimsAsync(user);

            var claim = new Claim(request.Claim.Type, request.Claim.Value);

            if (!claims.Any(x => x.Type == claim.Type && x.Value == claim.Value))
            {
                throw context.NotFoundRpcException(
                    $"The claim type '{claim.Type}' (value=\"{claim.Value}\") not found in user '{user.Email}'. (userId=${user.Id})");
            }

            var result = await _userService.RemoveClaimAsync(user, claim);

            if (result.Succeeded)
            {
                var detail = $"The claim type '{claim.Type}' (value=\"{claim.Value}\") as been removed to the user '{user.Email}'. (userId=${user.Id})";

                var response = new RemoveUserClaimResponse
                {
                    Claim = new UserClaimDto
                    {
                        Type = claim.Type,
                        Value = claim.Value
                    }
                };

                return context.Ok(response, detail);
            }

            throw context.FailedPreconditionRpcException(
                result,
                $"Failed to remove the claim type '{claim.Type}' (value=\"{claim.Value}\") from the user '{user.Email}'. (userId=${user.Id})");
        }

        public override async Task<ReplaceUserClaimResponse> ReplaceUserClaim(ReplaceUserClaimRequest request, ServerCallContext context)
        {
            var user = await _userService.FindByIdAsync(request.UserId);

            if (user == null)
            {
                throw context.NotFoundRpcException($"The user '{request.UserId}' wasn't found.");
            }

            var claims = await _userService.GetClaimsAsync(user);

            var claim = claims.SingleOrDefault(x => x.Type == request.Claim.Type);

            if (claim == null)
            {
                throw context.NotFoundRpcException(
                    $"The claim type '{request.Claim.Type}' (value=\"{request.Claim.Value}\") not found in user '{user.Email}'. (userId=${user.Id})");
            }

            var newClaim = new Claim(request.Claim.Type, request.Claim.Value);

            var result = await _userService.ReplaceClaimAsync(user, claim, newClaim);

            if (result.Succeeded)
            {
                var detail = $"The claim type '{newClaim.Type}' (value=\"{newClaim.Value}\") as been replaced for the user '{user.Email}'. (userId=${user.Id})";

                var response = new ReplaceUserClaimResponse
                {
                    Claim = new UserClaimDto
                    {
                        Type = newClaim.Type,
                        Value = newClaim.Value
                    }
                };

                return context.Ok(response, detail);
            }

            throw context.FailedPreconditionRpcException(
                result,
                $"Failed to replace the claim type '{newClaim.Type}' (value=\"{newClaim.Value}\") from the user '{user.Email}'. (userId=${user.Id})");
        }

        public override async Task<AddUserToRoleResponse> AddUserToRole(AddUserToRoleRequest request, ServerCallContext context)
        {
            var user = await _userService.FindByIdAsync(request.UserId);

            if (user == null)
            {
                throw context.NotFoundRpcException($"The user '{request.UserId}' wasn't found.");
            }

            if (await _userService.IsInRoleAsync(user, request.RoleName))
            {
                var existingRole = await _roleService.FindByNameAsync(request.RoleName);

                var detail = $"The role '{existingRole.Name}' already exists. (roleId=${existingRole.Id})";

                var response = new AddUserToRoleResponse
                {
                    Role = new RoleDto
                    {
                        Id = existingRole.Id.ToString(),
                        Name = existingRole.Name
                    }
                };

                return context.AlreadyExists(response, detail);
            }

            var result = await _userService.AddToRoleAsync(user, request.RoleName);

            if (result.Succeeded)
            {
                var role = await _roleService.FindByNameAsync(request.RoleName);

                var detail = $"The role '{role.Name}' as been created. (roleId=${role.Id})";

                var response = new AddUserToRoleResponse
                {
                    Role = new RoleDto
                    {
                        Id = role.Id.ToString(),
                        Name = role.Name
                    }
                };

                return context.Ok(response, detail);
            }

            throw context.FailedPreconditionRpcException(result, string.Empty);
        }

        public override async Task<RemoveUserFromRoleResponse> RemoveUserFromRole(RemoveUserFromRoleRequest request, ServerCallContext context)
        {
            var user = await _userService.FindByIdAsync(request.UserId);

            if (user == null)
            {
                throw context.NotFoundRpcException($"The user '{request.UserId}' wasn't found.");
            }

            if (!await _userService.IsInRoleAsync(user, request.RoleName))
            {
                var existingRole = await _roleService.FindByNameAsync(request.RoleName);

                throw context.NotFoundRpcException($"The role '{existingRole.Name}' already exists. (roleId=${existingRole.Id})");
            }

            var result = await _userService.RemoveFromRoleAsync(user, request.RoleName);

            if (result.Succeeded)
            {
                var role = await _roleService.FindByNameAsync(request.RoleName);

                var detail = $"The role '{role.Name}' as been created. (roleId=${role.Id})";

                var response = new RemoveUserFromRoleResponse
                {
                    Role = new RoleDto
                    {
                        Id = role.Id.ToString(),
                        Name = role.Name
                    }
                };

                return context.Ok(response, detail);
            }

            throw context.FailedPreconditionRpcException(result, string.Empty);
        }

        public override async Task<CreateRoleResponse> CreateRole(CreateRoleRequest request, ServerCallContext context)
        {
            if (!await _roleService.RoleExistsAsync(request.RoleName))
            {
                var existingRole = await _roleService.FindByNameAsync(request.RoleName);

                var detail = $"The role '{existingRole.Name}' already exists. (roleId=${existingRole.Id})";

                var response = new CreateRoleResponse
                {
                    Role = new RoleDto
                    {
                        Id = existingRole.Id.ToString(),
                        Name = existingRole.Name
                    }
                };

                return context.AlreadyExists(response, detail);
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

                var response = new CreateRoleResponse
                {
                    Role = new RoleDto
                    {
                        Id = role.Id.ToString(),
                        Name = role.Name
                    }
                };

                return context.Ok(response, detail);
            }

            throw context.FailedPreconditionRpcException(result, $"Failed to create the role '{role.Name}'. (roleId=${role.Id})");
        }

        public override async Task<DeleteRoleResponse> DeleteRole(DeleteRoleRequest request, ServerCallContext context)
        {
            if (!await _roleService.RoleExistsAsync(request.RoleName))
            {
                throw context.NotFoundRpcException($"The role '{request.RoleName}' wasn't found.");
            }

            var role = await _roleService.FindByNameAsync(request.RoleName);

            var result = await _roleService.DeleteAsync(role);

            if (result.Succeeded)
            {
                var detail = $"The role '{role.Name}' as been deleted. (roleId=${role.Id})";

                var response = new DeleteRoleResponse
                {
                    Role = new RoleDto
                    {
                        Id = role.Id.ToString(),
                        Name = role.Name
                    }
                };

                return context.Ok(response, detail);
            }

            throw context.FailedPreconditionRpcException(result, $"Failed to delete the role '{role.Name}'. (roleId=${role.Id})");
        }

        public override async Task<AddRoleClaimResponse> AddRoleClaim(AddRoleClaimRequest request, ServerCallContext context)
        {
            if (!await _roleService.RoleExistsAsync(request.RoleName))
            {
                throw context.NotFoundRpcException($"The role '{request.RoleName}' wasn't found.");
            }

            var role = await _roleService.FindByNameAsync(request.RoleName);

            var claims = await _roleService.GetClaimsAsync(role);

            var claim = new Claim(request.Claim.Type, request.Claim.Value);

            if (claims.Any(x => x.Type == claim.Type && x.Value == claim.Value))
            {
                var detail = $"The claim type '{claim.Type}' (value=\"{claim.Value}\") already exists in role '{role.Name}'. (roleId=${role.Id})";

                var response = new AddRoleClaimResponse
                {
                    Claim = new RoleClaimDto
                    {
                        Type = claim.Type,
                        Value = claim.Value
                    }
                };

                return context.AlreadyExists(response, detail);
            }

            var result = await _roleService.AddClaimAsync(role, claim);

            if (result.Succeeded)
            {
                var detail = $"The claim type '{claim.Type}' (value=\"{claim.Value}\") as been added to the role '{role.Name}'. (roleId=${role.Id})";

                var response = new AddRoleClaimResponse
                {
                    Claim = new RoleClaimDto
                    {
                        Type = claim.Type,
                        Value = claim.Value
                    }
                };

                return context.Ok(response, detail);
            }

            throw context.FailedPreconditionRpcException(
                result,
                $"Failed to add the claim type '{claim.Type}' (value=\"{claim.Value}\") to the role '{role.Name}'. (roleId=${role.Id})");
        }

        public override async Task<RemoveRoleClaimResponse> RemoveRoleClaim(RemoveRoleClaimRequest request, ServerCallContext context)
        {
            if (!await _roleService.RoleExistsAsync(request.RoleName))
            {
                throw context.NotFoundRpcException($"The role '{request.RoleName}' wasn't found.");
            }

            var role = await _roleService.FindByNameAsync(request.RoleName);

            var claims = await _roleService.GetClaimsAsync(role);

            var claim = new Claim(request.Claim.Type, request.Claim.Value);

            if (!claims.Any(x => x.Type == claim.Type && x.Value == claim.Value))
            {
                throw context.NotFoundRpcException(
                    $"The claim type '{claim.Type}' (value=\"{claim.Value}\") not found in role '{role.Name}'. (roleId=${role.Id})");
            }

            var result = await _roleService.RemoveClaimAsync(role, claim);

            if (result.Succeeded)
            {
                var detail = $"The claim type '{claim.Type}' (value=\"{claim.Value}\") as been removed from the role '{role.Name}'. (roleId=${role.Id})";

                var response = new RemoveRoleClaimResponse
                {
                    Claim = new RoleClaimDto
                    {
                        Type = claim.Type,
                        Value = claim.Value
                    }
                };

                return context.Ok(response, detail);
            }

            throw context.FailedPreconditionRpcException(
                result,
                $"Failed to remove the claim type '{claim.Type}' (value=\"{claim.Value}\") from the role '{role.Name}'. (roleId=${role.Id})");
        }

        public override async Task<FetchDoxatagsResponse> FetchDoxatags(FetchDoxatagsRequest request, ServerCallContext context)
        {
            var doxatags = await _doxatagService.FetchDoxatagsAsync();

            var response = new FetchDoxatagsResponse
            {
                Doxatags =
                {
                    doxatags.Select(MapDoxatag)
                }
            };

            return context.Ok(response, string.Empty);
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
