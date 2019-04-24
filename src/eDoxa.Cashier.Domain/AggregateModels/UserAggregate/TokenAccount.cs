// Filename: TokenAccount.cs
// Date Created: --
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.
namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public class TokenAccount : IAccount<Token>
    {
        private Token _balance;
        private Token _pending;

        public TokenAccount()
        {
            _balance = Token.Zero;
            _pending = Token.Zero;
        }

        public Token Balance => _balance;

        public Token Pending => _pending;

        public void AddBalance(Token amount)
        {
            _balance = new Token((long) _balance + (long) amount);
        }

        public void AddPending(Token amount)
        {
            _pending = new Token((long) _pending + (long) amount);
        }

        public void SubtractBalance(Token amount)
        {
            _balance = new Token((long) _balance - (long) amount);
        }

        public void SubtractPending(Token amount)
        {
            _pending = new Token((long) _pending - (long) amount);
        }
    }
}