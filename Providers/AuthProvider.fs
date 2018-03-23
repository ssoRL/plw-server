namespace PLW.Providers

open System;
open System.IdentityModel.Tokens.Jwt;
open System.Security.Claims;
open System.Text;
open Microsoft.AspNetCore.Mvc;
open Microsoft.IdentityModel.Tokens;

module AuthProvider =
    let IsValidUsernameAndPassword (username, password) =
        match username with
        | null -> false
        | _ -> true

    let GenerateToken (username) =
        let claims = [|
            Claim (ClaimTypes.Name, username)
            Claim (ClaimTypes.Version, "1")
            Claim (ClaimTypes.Expiration, DateTimeOffset(DateTime.Now.AddDays(1.0)).ToUnixTimeSeconds().ToString())
        |]
        let cred =
            new SigningCredentials(
                SymmetricSecurityKey(Encoding.UTF8.GetBytes("the secret that needs to be at least 16 characeters long for HmacSha256")),
                SecurityAlgorithms.HmacSha256
            )
        let token = JwtSecurityToken(JwtHeader(cred), JwtPayload(claims))
        JwtSecurityTokenHandler().WriteToken(token)