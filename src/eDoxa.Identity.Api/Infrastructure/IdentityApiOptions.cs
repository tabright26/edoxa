// Filename: IdentityApiOptions.cs
// Date Created: 2019-07-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Identity.Api.Infrastructure
{
    public class IdentityApiOptions
    {
        public WebOptions Web { get; set; }
    }

    public class WebOptions
    {
        public SpaOptions Spa { get; set; }
    }

    public class SpaOptions
    {
        public string Url { get; set; }
    }
}
