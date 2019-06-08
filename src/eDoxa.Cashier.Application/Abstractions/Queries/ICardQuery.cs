// Filename: ICardQuery.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Cashier.Application.ViewModels;

namespace eDoxa.Cashier.Application.Abstractions.Queries
{
    public interface ICardQuery
    {
        Task<IReadOnlyCollection<CardViewModel>> GetCardsAsync();
    }
}
