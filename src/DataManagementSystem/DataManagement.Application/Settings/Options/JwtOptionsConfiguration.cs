﻿namespace DataManagement.Application.Settings.Options
{
	public class JwtOptionsConfiguration
	{
		public string SigningKey { get; set; }
		public string Audience { get; set; }
		public string Issuer { get; set; }
	}
}
