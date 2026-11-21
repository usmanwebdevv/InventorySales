using System;
using InventorySales.Application.Interfaces;
using InventorySales.Infrastructure.Data;
using InventorySales.Domain.Entities;
using InventorySales.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace InventorySales.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly ITokenService _tokenService;

    public AuthService(AppDbContext context , ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
    {
        if(await _context.Users.AnyAsync(u => u.Username == dto.Username))
        {
            throw new InvalidOperationException("User Already Exits.");

        }
        var user = new User
        {
            Username = dto.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = dto.Role
        };


        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new AuthResponseDto
        {
            Token = _tokenService.GenerateToken(user),
            Username = user.Username,
            Role = user.Role,
            ExpiresAt = DateTime.UtcNow.AddMinutes(60)
        };
    }
    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u=> u.Username == dto.Username);
        if(user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid username or password");
        }
        return new AuthResponseDto
        {
            Token = _tokenService.GenerateToken(user),
            Username = user.Username,
            Role = user.Role,
            ExpiresAt = DateTime.UtcNow.AddMinutes(60)
        };
    }


}
