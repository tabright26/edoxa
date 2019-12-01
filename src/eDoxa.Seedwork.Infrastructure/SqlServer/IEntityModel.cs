// Filename: IEntityModel.cs
// Date Created: 2019-11-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain;

namespace eDoxa.Seedwork.Infrastructure.SqlServer
{
    public interface IEntityModel
    {
        ICollection<IDomainEvent> DomainEvents { get; set; }
    }
}
