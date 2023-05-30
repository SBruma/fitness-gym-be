using FitnessGym.Application.Dtos.Gyms;
using FitnessGym.Application.Dtos.Gyms.Update;
using FitnessGym.Domain.Entities.Members;
using Microsoft.AspNetCore.Mvc;

namespace FitnessGym.Application.Mappers.Gyms
{
    public class StaffScheduleMapper
    {
        public List<StaffSchedule> MapDtoToEntities(StaffScheduleDto dto)
        {
            var dtos = dto.Schedules.Select(schedule => new StaffSchedule
            {
                StaffId = dto.MemberId,
                DayOfWeek = schedule.DayOfWeek,
                StartTime = schedule.StartTime?.ToTimeSpan(),
                EndTime = schedule.EndTime?.ToTimeSpan(),
                BreakStartTime = schedule.BreakStartTime?.ToTimeSpan(),
                BreakEndTime = schedule.BreakEndTime?.ToTimeSpan()
            })
            .ToList();

            return dtos;
        }

        public StaffScheduleDto MapEntitiesToDto(List<StaffSchedule> entities)
        {
            var dto = new StaffScheduleDto
            {
                MemberId = entities.First().StaffId,
                Schedules = entities.Select(schedule => new ScheduleDto
                {
                    StartTime = schedule.StartTime != null ? new TimeOnly(schedule.StartTime.Value.Ticks) : null,
                    EndTime = schedule.EndTime != null ? new TimeOnly(schedule.EndTime.Value.Ticks) : null,
                    BreakStartTime = schedule.BreakStartTime != null ? new TimeOnly(schedule.BreakStartTime.Value.Ticks) : null,
                    BreakEndTime = schedule.BreakEndTime != null ? new TimeOnly(schedule.BreakEndTime.Value.Ticks) : null
                })
                .ToList()
            };

            return dto;
        }

        public void MapUpdateEntities(UpdateStaffSchedule dto, List<StaffSchedule> entities)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                var scheduleDto = dto.Schedules.Where(dto => dto.DayOfWeek == entities[i].DayOfWeek).FirstOrDefault();
                entities[i].StaffId = entities[i].StaffId;
                entities[i].StartTime = scheduleDto?.StartTime != null ? scheduleDto.StartTime.Value.ToTimeSpan() : null;
                entities[i].EndTime = scheduleDto?.EndTime != null ? scheduleDto.EndTime.Value.ToTimeSpan() : null;
                entities[i].BreakStartTime = scheduleDto?.BreakStartTime != null ? scheduleDto.BreakStartTime.Value.ToTimeSpan() : null;
                entities[i].BreakEndTime = scheduleDto?.BreakEndTime != null ? scheduleDto.BreakEndTime.Value.ToTimeSpan() : null;
            }
        }
    }
}

