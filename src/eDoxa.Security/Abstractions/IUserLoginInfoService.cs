// Filename: IExternalLoginService.cs
// Date Created: 2019-05-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain.Enumerations;

namespace eDoxa.Security.Abstractions
{
    public interface IUserLoginInfoService
    {
        string GetExternalKey(Game game);
    }
}