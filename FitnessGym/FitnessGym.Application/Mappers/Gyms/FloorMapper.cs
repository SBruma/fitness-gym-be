using FitnessGym.Application.Dtos.Gyms;
using FitnessGym.Domain.Entities.Gyms;
using Riok.Mapperly.Abstractions;

namespace FitnessGym.Application.Mappers.Gyms
{
    [Mapper]
    public partial class FloorMapper
    {
        [MapProperty(nameof(Floor.Level), nameof(FloorDto.Floor))]
        public partial FloorDto FloorDtoFloor(Floor floor);
        public partial List<FloorDto> FloorDtoFloor(List<Floor> floor);
    }
}
