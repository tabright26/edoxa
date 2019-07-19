// Filename: ModelStateDictionaryExtensions.cs
// Date Created: 2019-07-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain.Extensions;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace eDoxa.Identity.Api.Extensions
{
    public static class ModelStateDictionaryExtensions
    {
        public static void Bind(this ModelStateDictionary modelState, IdentityResult result)
        {
            result.Errors.ForEach(error => modelState.AddModelError(string.Empty, error.Description));
        }
    }
}
