// Filename: ICommand.cs
// Date Created: 2019-04-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using MediatR;

namespace eDoxa.Commands.Abstractions
{
    public interface ICommand : IRequest, ICommand<Unit>
    {
    }

    public interface ICommand<out TResponse> : IRequest<TResponse>, IBaseCommand
    {
    }
}