// Filename: ApiSettings.cs
// Date Created: 2019-07-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.ComponentModel.DataAnnotations;

namespace eDoxa.Seedwork.Application.Swagger
{
    public class AppSettings
    {
        [Required]
        public Api Api { get; set; }

        [Required]
        public Swagger Swagger { get; set; }
    }

    public class Api
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
    }

    public class Swagger
    {
        [Required]
        public bool Enabled { get; set; }
    }
}
