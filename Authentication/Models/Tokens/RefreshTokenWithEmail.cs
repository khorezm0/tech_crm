﻿namespace Authentication.Models.Tokens;
public class RefreshTokenWithEmail
{
	public int UserId { get; set; }
	public RefreshToken RefreshToken { get; set; }
}
