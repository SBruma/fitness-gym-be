﻿using FitnessGym.Application.Dtos;
using FitnessGym.Application.Dtos.Gyms;
using FitnessGym.Application.Dtos.Gyms.Create;
using FitnessGym.Domain.Entities.Gyms;
using FluentResults;

namespace FitnessGym.Application.Services.Interfaces.Gyms
{
    public interface IMembershipService
    {
        Task<Result<MembershipDto>> Create(CreateMembershipDto createMembershipDto);
        Task<Result<MembershipDto>> GetActiveMembership(GymId gymId, string userEmail);
        Task<Result<List<MembershipDto>>> GetHistory(GymId gymId, string userEmail);
        Task<Result<GymCheckInDto>> CheckInOut(QRCodeCheckInOutDto dto);
        Task<Result<List<GymCheckInHistoryDto>>> GetCheckInOutHistory(DateTime minimumDate, GymId gymId);
        Task<Result<int>> GetMembersInGym(GymId gymId);
    }
}
