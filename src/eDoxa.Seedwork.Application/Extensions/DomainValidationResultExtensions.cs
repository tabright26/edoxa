// Filename: DomainValidationResultExtensions.cs
// Date Created: 2019-11-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain;

using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace eDoxa.Seedwork.Application.Extensions
{
    public static class DomainValidationResultExtensions
    {
        public static void AddToModelState(this DomainValidationResult result, ModelStateDictionary modelState)
        {
            foreach (var error in result.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
        }
    }
}
