using FitnessGym.Application.Dtos.Identity;
using FitnessGym.Domain.Entities.Identity;
using FluentResults;

namespace FitnessGym.Application.Services.Interfaces.Identity
{
    public interface IIdentityService
    {
        public Task<Result<ApplicationUser>> Register(RegisterDto registerDto);
        public Task<Result<TokenData>> Login(LoginDto loginDto);
        public Task<TokenData> GenerateTokenAsync(ApplicationUser user);
    }
}
