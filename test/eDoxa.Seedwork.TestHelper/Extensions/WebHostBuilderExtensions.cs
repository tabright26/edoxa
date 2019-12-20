// Filename: WebHostBuilderExtensions.cs
// Date Created: 2019-12-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IO;

using Microsoft.AspNetCore.Hosting;

namespace eDoxa.Seedwork.TestHelper.Extensions
{
    public static class WebHostBuilderExtensions
    {
        public static void UseCustomContentRoot(this IWebHostBuilder builder, string path)
        {
            builder.UseContentRoot(Path.Combine(Directory.GetCurrentDirectory(), path));
        }
    }
}
