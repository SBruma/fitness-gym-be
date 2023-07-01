using FitnessGym.Application.Dtos;
using FitnessGym.Application.Dtos.Gyms;
using FitnessGym.Application.Dtos.Gyms.Create;
using FitnessGym.Application.Errors;
using FitnessGym.Application.Mappers;
using FitnessGym.Application.Services.Interfaces.Gyms;
using FitnessGym.Domain.Entities.Gyms;
using FitnessGym.Domain.Entities.Identity;
using FitnessGym.Domain.Entities.Members;
using FitnessGym.Infrastructure.Data.Interfaces;
using FluentResults;
using IronBarCode;
using Microsoft.AspNetCore.Identity;
using Result = FluentResults.Result;

namespace FitnessGym.Application.Services.Gyms
{
    public class MembershipService : IMembershipService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public MembershipService(IMapper mapper,
                                IUnitOfWork unitOfWork,
                                UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<Result<MembershipDto>> Create(CreateMembershipDto createMembershipDto)
        {
            var member = await _userManager.FindByEmailAsync(createMembershipDto.Email);

            if (member is null)
            {
                return Result.Fail(new NotFoundError(typeof(ApplicationUser)));
            }

            var isMembershipActive = await _unitOfWork.MembershipRepository.GetActiveMembership(new GymId(createMembershipDto.GymId), member.Id);

            if (isMembershipActive.IsSuccess)
            {
                return Result.Fail(new NotCreatedError($"There is already an active membership for {member.Email}"));
            }

            var membership = _mapper.MembershipMapper.MapCreateDtoToEntity(createMembershipDto);
            membership.MemberId = member.Id;
            membership.QRCode = new QRCode(new byte[0]);
            membership.ExpirationDate = membership.RenewalDate.AddMonths(createMembershipDto.Months);
            await _unitOfWork.MembershipRepository.Add(membership);
            var createResult = await _unitOfWork.SaveChangesAsync();

            if (createResult.IsFailed)
            {
                return Result.Fail(new NotCreatedError(typeof(Membership)));
            }

            membership.QRCode = new QRCode(GenerateQRCode(GenerateQrCodeText(membership, member)));
            _unitOfWork.MembershipRepository.Update(membership);
            var updateQRCodeResult = await _unitOfWork.SaveChangesAsync();

            return updateQRCodeResult.IsSuccess ? Result.Ok(_mapper.MembershipMapper.MapEntityToDto(membership)) :
                                                    Result.Fail(new NotCreatedError(typeof(Membership)));
        }

        public async Task<Result<List<MembershipDto>>> GetHistory(GymId gymId, string userEmail)
        {
            var member = await _userManager.FindByEmailAsync(userEmail);

            if (member is null)
            {
                return Result.Fail(new NotFoundError(typeof(ApplicationUser)));
            }

            var membershipHistoryResult = await _unitOfWork.MembershipRepository.GetHistory(gymId, member.Id);

            if (membershipHistoryResult.IsFailed)
            {
                Result.Fail(new NotFoundError(typeof(List<Membership>)));
            }

            foreach (var item in membershipHistoryResult.Value)
            {
                if (item.ExpirationDate < DateTime.UtcNow)
                {
                    item.QRCode = new QRCode(new byte[0]);
                }
            }

            return Result.Ok(_mapper.MembershipMapper.MapEntityToDto(membershipHistoryResult.Value));
        }

        public async Task<Result<MembershipDto>> GetActiveMembership(GymId gymId, string userEmail)
        {
            var member = await _userManager.FindByEmailAsync(userEmail);

            if (member is null)
            {
                return Result.Fail(new NotFoundError(typeof(ApplicationUser)));
            }

            var getResult = await _unitOfWork.MembershipRepository.GetActiveMembership(gymId, member.Id);

            return getResult.IsSuccess ? Result.Ok(_mapper.MembershipMapper.MapEntityToDto(getResult.Value)) :
                                            Result.Fail(new NotFoundError(typeof(Membership)));
        }

        public async Task<Result<GymCheckInDto>> CheckInOut(QRCodeCheckInOutDto dto)
        {
            var membershipId = new MembershipId(dto.QRCodeId);
            var getActiveCheckInResult = await _unitOfWork.GymCheckInRepository.GetActive(membershipId);

            if (getActiveCheckInResult.IsFailed)
            {
                var checkIn = new GymCheckIn
                {
                    CheckInTime = DateTime.UtcNow,
                    MembershipId = membershipId,
                    CheckOutTime = null
                };

                await _unitOfWork.GymCheckInRepository.Add(checkIn);
                var createResult = await _unitOfWork.SaveChangesAsync();

                if (createResult.IsSuccess)
                {
                    return Result.Ok(new GymCheckInDto
                    {
                        Id = checkIn.Id,
                        CheckInTime = checkIn.CheckInTime,
                        CheckOutTime = null
                    });
                }
            }

            var activeCheckIn = getActiveCheckInResult.Value;
            var minimumTime = activeCheckIn.CheckInTime.AddSeconds(30);
            if (DateTime.UtcNow < minimumTime)
            {
                return Result.Fail(new Error("Wait atleast 30 seconds"));
            }

            activeCheckIn.CheckOutTime = DateTime.UtcNow;
            _unitOfWork.GymCheckInRepository.Update(activeCheckIn);
            await _unitOfWork.SaveChangesAsync();

            return Result.Ok(new GymCheckInDto
            {
                Id = activeCheckIn.Id,
                CheckInTime = activeCheckIn.CheckInTime,
                CheckOutTime = activeCheckIn.CheckOutTime
            });
        }

        public async Task<Result<List<GymCheckInHistoryDto>>> GetCheckInOutHistory(DateTime minimumDate, GymId gymId)
        {
            var checkInsHistoryResult = await _unitOfWork.GymCheckInRepository.GetHistory(minimumDate, gymId);

            return checkInsHistoryResult.IsSuccess ? checkInsHistoryResult.Value.Select(checkiIn => new GymCheckInHistoryDto
            {
                Id = checkiIn.Id,
                CheckInTime = checkiIn.CheckInTime,
                CheckOutTime = checkiIn.CheckOutTime,
                Email = checkiIn.Membership.Member.Email
            }).ToList() : Result.Fail(checkInsHistoryResult.Errors.First());
        }

        public async Task<Result<int>> GetMembersInGym(GymId gymId)
        {
            var getMembersInGymResult = await _unitOfWork.GymCheckInRepository.GetMembersInGym(gymId);

            return Result.Ok(getMembersInGymResult.Value);
        }

        private byte[] GenerateQRCode(string text)
        {
            var QRCode = QRCodeWriter.CreateQrCode(text, 200);

            return QRCode.ToWindowsBitmapBinaryData();
        }

        private string GenerateQrCodeText(Membership membership, ApplicationUser user)
        {
            var QRCodeData = new QRMembershipData
            {
                Email = user.Email,
                Id = membership.Id.Value
            };

            return System.Text.Json.JsonSerializer.Serialize(QRCodeData);
        }
    }
}
