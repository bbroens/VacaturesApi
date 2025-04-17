using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using VacaturesApi.Features.Authentication.Login;
using VacaturesApi.Features.Authentication.Register;

namespace VacaturesApi.Features.Authentication;

/// <summary>
/// This repository exposes methods with API authentication operations.
/// Validation is done with FluentValidation on the command/query level.
/// </summary>

public class AuthRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    public async Task<AuthResponseDto> Register(RegisterDto registerUser)
    {
        var user = new ApplicationUser
        {
            UserName = registerUser.Email,
            Email = registerUser.Email,
            FirstName = registerUser.FirstName,
            LastName = registerUser.LastName
        };

        var result = await _userManager.CreateAsync(user, registerUser.Password);

        if (!result.Succeeded)
            throw new ApplicationException(string.Join(", ", result.Errors.Select(e => e.Description)));

        // Add user roles if they don't exist yet
        if (registerUser.Roles != null)
        {
            foreach (var role in registerUser.Roles)
            {
                if (! await _roleManager.RoleExistsAsync(role))
                    await _roleManager.CreateAsync(new IdentityRole(role));
                
                await _userManager.AddToRoleAsync(user, role);
            }
        }
        
        return await GenerateJwtToken(user);
    }

    public async Task<AuthResponseDto> Login(LoginDto login)
    {
        var user = await _userManager.FindByEmailAsync(login.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, login.Password))
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        return await GenerateJwtToken(user);
    }

    private async Task<AuthResponseDto> GenerateJwtToken(ApplicationUser user)
    {
        if (string.IsNullOrEmpty(user.Id) || string.IsNullOrEmpty(user.Email))
            throw new InvalidOperationException("User data is incomplete.");
        
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // Add roles to the token
        var roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"] ?? string.Empty)
        );
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expires = DateTime.UtcNow.AddHours(
            int.Parse(_configuration["Jwt:ExpirationHours"] ?? "24")
        );

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        return new AuthResponseDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expires
        };
    }
}