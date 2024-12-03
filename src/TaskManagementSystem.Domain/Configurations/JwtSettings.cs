﻿namespace TaskManagementSystem.Domain.Configurations
{
    public class JwtSettings
    {

        public required string Issuer { get; set; }

        public required string Audience { get; set; }

        public required string Key { get; set; }

        public TimeSpan Expires { get; set; }

    }
}
