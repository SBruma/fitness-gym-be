using Duende.IdentityServer.ResponseHandling;
using FitnessGym.Application.Dtos.Identity;
using FluentResults;

namespace FitnessGym.Application.Services.Interfaces.Identity
{
    public interface IIdentityService
    {
        public Task<Result> Register(RegisterDto registerDto);
        public Task<Result<TokenResponse>> Login(LoginDto loginDto);
    }
}
