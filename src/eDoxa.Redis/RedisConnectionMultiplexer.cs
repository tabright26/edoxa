// Filename: RedisConnectionMultiplexer.cs
// Date Created: 2020-02-10
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using StackExchange.Redis;

namespace eDoxa.Redis
{
    public static class RedisConnectionMultiplexer
    {
        public static IConnectionMultiplexer Connect(string connectionString)
        {
            return ConnectionMultiplexer.Connect(connectionString);
        }
    }
}
