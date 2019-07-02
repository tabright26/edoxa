// Filename: ICardQuery.cs
// Date Created: 2019-07-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.ViewModels;

namespace eDoxa.Cashier.Domain.Queries
{
    public interface ICardQuery
    {
        Task<IReadOnlyCollection<CardViewModel>> GetCardsAsync();
    }
}
