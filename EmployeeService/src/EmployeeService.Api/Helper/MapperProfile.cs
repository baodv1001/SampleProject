using AutoMapper;

namespace EmployeeService.Api.Helper
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Infrastructure.Entities.Employee, Core.Models.Employee>();
        }
    }
}
