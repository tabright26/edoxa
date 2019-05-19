// Filename: DeleteBankAccountCommand.cs
// Date Created: 2019-05-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Commands.Abstractions;
using eDoxa.Commands.Result;
using eDoxa.Functional;
using eDoxa.Seedwork.Domain.Validations;

namespace eDoxa.Cashier.Application.Commands
{
    public sealed class DeleteBankAccountCommand : Command<Either<ValidationError, CommandResult>>
    {
    }
}
