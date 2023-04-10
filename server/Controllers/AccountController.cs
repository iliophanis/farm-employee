using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        return Ok(new { Message = "Logged in successfully" });
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login()
    {
    var googleAuthResult = await HttpContext.AuthenticateAsync("Google");
    var claims = new[]
    {
        new Claim(ClaimTypes.Name, googleAuthResult.Principal.Identity.Name)
    };

    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes("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");
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    var tokenString = tokenHandler.WriteToken(token);

    return Ok(new { Token = tokenString });
    }
}



