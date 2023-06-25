using FitnessGym.Application.Dtos.Gyms.Create;
using FitnessGym.Application.Dtos.Gyms.Update;
using FitnessGym.Application.Dtos.Identity;
using FitnessGym.Domain.Entities.Identity;
using FluentResults;

namespace FitnessGym.Application.Services.Interfaces.Identity
{
    public interface IIdentityService
    {
        public Task<Result<ApplicationUser>> Register(RegisterDto registerDto);
        public Task<Result<ApplicationUser>> Add(AddMemberDto addMemberDto);
        public Task<Result<TokenData>> Login(LoginDto loginDto);
        public Task<TokenData> GenerateToken(ApplicationUser user);
        public Task<Result<TokenData>> RefreshToken(TokenData tokenData);
        public Task<Result<TokenData>> Update(UpdateUserDto updateUserDto, string email);
        public Task<Result> UpdatePassword(UpdatePasswordDto updatePasswordDto, string email);
    }
}
