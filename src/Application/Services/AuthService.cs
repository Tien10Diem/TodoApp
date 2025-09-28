using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.DTOs;
using Domain.Entities;
using Application.Helpers;

namespace Application.Services;

public class AuthService : IAuthService
{

    private readonly IUserRepository _user;
    private readonly IPasswordHasher _passhash;

    public AuthService(IUserRepository user, IPasswordHasher passhash)
    {
        _user = user;
        _passhash = passhash;
    }

    public async Task RegisterAsync(registerRequestDTO request, CancellationToken ct = default)
    {
            
        if (await _user.ExistsByUserOrEmailAsync(request.UserName, request.UserEmail, ct))
        {
            throw new BaseException("User or Email already exists");
        }
    
        var user = new User
        {
            UserName = request.UserName,
            UserEmail = request.UserEmail,
            UserPasswordHash = _passhash.Hash(request.UserPassword)
        };

        await _user.AddAsync(user, ct);

    }

    public async Task<User?> LoginAsync(loginRequestDTO request, CancellationToken ct = default)
    {
        var userFind = await _user.GetByUserOrEmailAsync(request.UserName, request.UserEmail);
        if (userFind is null) return null;

        var ok = _passhash.Verify(request.UserPassword, userFind.UserPasswordHash);
        if (ok) return userFind;
        else return null;
    }


}
