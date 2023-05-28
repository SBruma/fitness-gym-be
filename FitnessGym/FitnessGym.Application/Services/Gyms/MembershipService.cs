using FitnessGym.Application.Dtos.Gyms;
using FitnessGym.Application.Dtos.Gyms.Create;
using FitnessGym.Application.Errors;
using FitnessGym.Application.Mappers;
using FitnessGym.Application.Services.Interfaces.Gyms;
using FitnessGym.Domain.Entities.Identity;
using FitnessGym.Domain.Entities.Members;
using FitnessGym.Infrastructure.Data.Interfaces;
using FluentResults;
using IronBarCode;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
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

            var isMembershipActive = await _unitOfWork.MembershipRepository.GetActiveMembership(member.Id);

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

            membership.QRCode = new QRCode(GenerateQRCode(GenerateQrCodeText(membership, member.Email)));
            _unitOfWork.MembershipRepository.Update(membership);
            var updateQRCodeResult = await _unitOfWork.SaveChangesAsync();

            return updateQRCodeResult.IsSuccess ? Result.Ok(_mapper.MembershipMapper.MapEntityToDto(membership)) :
                                                    Result.Fail(new NotCreatedError(typeof(Membership)));
        }

        public async Task<Result<List<MembershipDto>>> GetHistory(string userEmail)
        {
            var member = await _userManager.FindByEmailAsync(userEmail);

            if (member is null)
            {
                return Result.Fail(new NotFoundError(typeof(ApplicationUser)));
            }

            var membershipHistoryResult = await _unitOfWork.MembershipRepository.GetHistory(member.Id);

            if (membershipHistoryResult.IsFailed)
            {
                Result.Fail(new NotFoundError(typeof(List<Membership>)));
            }

            foreach (var item in membershipHistoryResult.Value)
            {
                if (item != membershipHistoryResult.Value.First())
                {
                    item.QRCode = new QRCode(new byte[0]);
                }
            }

            return Result.Ok(_mapper.MembershipMapper.MapEntityToDto(membershipHistoryResult.Value));
        }

        public async Task<Result<MembershipDto>> GetActiveMembership(string userEmail)
        {
            var member = await _userManager.FindByEmailAsync(userEmail);

            if (member is null)
            {
                return Result.Fail(new NotFoundError(typeof(ApplicationUser)));
            }

            var getResult = await _unitOfWork.MembershipRepository.GetActiveMembership(member.Id);

            return getResult.IsSuccess ? Result.Ok(_mapper.MembershipMapper.MapEntityToDto(getResult.Value)) :
                                            Result.Fail(new NotFoundError(typeof(Membership)));
        }

        public byte[] GenerateQRCode(string text, int size = 500)
        {
            // https://www.qrcode.com/en/about/version.html
            var QRCode = QRCodeWriter.CreateQrCode(text, size, QRCodeWriter.QrErrorCorrectionLevel.Highest, 10);

            return QRCode.ToWindowsBitmapBinaryData();
        }

        private string GenerateQrCodeText(Membership membership, string userEmail)
        {
            var QRCodeData = new QRMembershipData
            {
                Email = userEmail,
                Id = membership.Id.Value
            };

            return JsonSerializer.Serialize(QRCodeData);
        }
    }
}
