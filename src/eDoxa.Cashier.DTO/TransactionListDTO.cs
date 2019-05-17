// Filename: TransactionListDTO.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.AutoMapper;

namespace eDoxa.Cashier.DTO
{
    public class TransactionListDTO : ListDTO<TransactionDTO>
    {
        public static implicit operator TransactionListDTO(List<TransactionDTO> items)
        {
            return new TransactionListDTO
            {
                Items = items
            };
        }
    }
}