// Filename: AppSettings.cs
// Date Created: 2019-07-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel.DataAnnotations;

using IdentityServer4.Models;

namespace eDoxa.Seedwork.Application.Swagger
{
    public class AppSettings
    {
        [Required]
        public Authority Authority { get; set; }

        [Required]
        public ApiResource ApiResource { get; set; }

        [Required]
        public Swagger Swagger { get; set; }
    }

    public class Authority
    {
        [Required]
        public string PrivateUrl { get; set; }

        [Required]
        public string PublicUrl { get; set; }
    }

    public class Swagger
    {
        [Required]
        public bool Enabled { get; set; }
    }
}
